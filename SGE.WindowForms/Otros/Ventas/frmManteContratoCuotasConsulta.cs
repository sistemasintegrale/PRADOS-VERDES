using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using SGE.WindowForms.Ventas.Reporte;
using DevExpress.XtraReports.UI;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteContratoCuotasConsulta : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContratoCuotas));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContratoCuotas Obe = new EContratoCuotas();
        //public List<EEspacios> lstEspacios = new List<EEspacios>();

        public List<EContratoCuotas> lstDetalle = new List<EContratoCuotas>();
        public List<EContratoCuotas> lstDelete = new List<EContratoCuotas>();


        public EContrato ObeC = new EContrato();

        public frmManteContratoCuotasConsulta()
        {
            InitializeComponent();
        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtNroContrato.Enabled = Enabled;

            //lkpNivel.Enabled = !Enabled;
            //lkpSituacion.Enabled = !Enabled;           
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtNroContrato.Enabled = Enabled;


            if (Status == BSMaintenanceStatus.CreateNew)
                txtNroContrato.Enabled = Enabled;


        }
        public void setValues()

        {
            txtNroContrato.Text = ObeC.cntc_vnumero_contrato;
            dtFechaCuota.EditValue = ObeC.cntc_sfecha_cuota;
            txtNroCuotas.Text = ObeC.cntc_inro_cuotas.ToString();
            txtCuotaInicial.Text = ObeC.cntc_ncuota_inicial.ToString();
            txtMontoCuotas.Text = ObeC.cntc_nmonto_cuota.ToString();
            txtPrecioTotal.Text = ObeC.cntc_nprecio_total.ToString();

            lstDetalle = new BVentas().listarContratoCuotas(ObeC.cntc_icod_contrato);
            lstDetalle.ForEach(x =>
            {
                if (x.cntc_icod_documento > 0)
                {
                    x.cntc_flag_situacion = true;
                }
                if (x.cntc_sfecha_pago_cuota.ToString() == "01/01/0001 12:00:00 a. m.")
                {
                    x.cntc_sfecha_pago_cuota = (DateTime?)null;
                }
                if (x.strSituacion == "PENDIENTE")
                {
                    x.plnd_vnumero_doc = "";
                    x.cntc_sfecha_pago_cuota = (DateTime?)null;
                }
            });
            grdDetalle.DataSource = lstDetalle.OrderBy(x => x.cntc_itipo_cuota);

            if (lstDetalle.Count == 0)
            {
                Status = BSMaintenanceStatus.CreateNew;
            }
            else
            {
                Status = BSMaintenanceStatus.ModifyCurrent;
                btnGenerar.Enabled = false;
            }

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void cargar()
        {
            //BSControls.LoaderLook(lkpNivel, new BGeneral().listarTablaVentaDet(6), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            //BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(10), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            //BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaVentaDet(11), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);


        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                /*----------------------*/
                if (String.IsNullOrEmpty(txtNroContrato.Text))
                {
                    oBase = txtNroContrato;
                    throw new ArgumentException("Ingrese código de la Funeraria");
                }


                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BVentas().insertarCCuotas(lstDetalle);
                }
                else
                {
                    new BVentas().modificarCCuotas(lstDetalle, lstDelete);

                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.cntc_icod_contrato_cuotas);
                    this.Close();
                }
            }
        }
        public int espacio_codigo { get; set; }
        int[] x = new int[8];

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteFuneraria_Load(object sender, EventArgs e)
        {
            cargar();

        }





        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            try
            {

                using (frmManteContratoCuotasDet frm = new frmManteContratoCuotasDet())
                {
                    if (lstDetalle.Count > 0)
                        frm.txtNroCuotas.Text = String.Format("{0:0}", lstDetalle.Count + 1);
                    else
                        frm.txtNroCuotas.Text = "1";
                    frm.SetInsert();
                    frm.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                    frm.lstDetalle = lstDetalle;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteContratoCuotasDet frm = new frmManteContratoCuotasDet())
            {
                frm.oBe = oBe_;
                frm.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                frm.SetModify();
                frm.lstDetalle = lstDetalle;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.Focus();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            lstDelete.Add(oBe_);
            lstDetalle.Remove(oBe_);
            viewDetalle.RefreshData();
        }
        DateTime FechaAnterior;

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            grdDetalle.DataSource = 0;
            int NroCuotas = (Convert.ToInt32(txtNroCuotas.Text));

            for (int y = 0; y <= NroCuotas; y++)
            {
                EContratoCuotas EDet = new EContratoCuotas();
                if (y == 0)
                {
                    EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                    EDet.cntc_inro_cuotas = y;
                    EDet.cntc_sfecha_cuota = Convert.ToDateTime(dtFechaCuota.EditValue);
                    EDet.cntc_icod_tipo_cuota = 336;
                    EDet.strTipo = "C. INICIAL";
                    EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtCuotaInicial.Text);
                    EDet.cntc_icod_situacion = 338;
                    EDet.strSituacion = "PENDIENTE";
                    EDet.intUsuario = Valores.intUsuario;
                    EDet.strPc = WindowsIdentity.GetCurrent().Name;
                    lstDetalle.Add(EDet);
                    FechaAnterior = EDet.cntc_sfecha_cuota;
                }
                else
                {
                    EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                    EDet.cntc_inro_cuotas = y;
                    EDet.cntc_sfecha_cuota = FechaAnterior.AddMonths(1);
                    EDet.cntc_icod_tipo_cuota = 337;
                    EDet.strTipo = "CUOTA";
                    EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                    EDet.cntc_icod_situacion = 338;
                    EDet.strSituacion = "PENDIENTE";
                    EDet.intUsuario = Valores.intUsuario;
                    EDet.strPc = WindowsIdentity.GetCurrent().Name;
                    lstDetalle.Add(EDet);
                    FechaAnterior = EDet.cntc_sfecha_cuota;

                }

            }
            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
            btnGenerar.Enabled = false;
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_icod_documento == 0)
            {
                if (obe.cntc_flag_situacion == true)
                {
                    obe.cntc_icod_situacion = 338;
                    obe.cntc_flag_situacion = false;
                    //new BVentas().modificarContratoCuotas(obe);
                    cargar();
                }
                else
                {
                    obe.cntc_icod_situacion = 340;
                    obe.cntc_flag_situacion = true;
                    //new BVentas().modificarContratoCuotas(obe);
                    cargar();
                }
            }
            else
            {
                if (obe.cntc_flag_situacion == true)
                {
                    obe.cntc_flag_situacion = true;

                    viewDetalle.RefreshData();
                    grdDetalle.Refresh();
                }
                else
                {
                    obe.cntc_flag_situacion = false;
                    viewDetalle.RefreshData();
                    grdDetalle.Refresh();
                }
            }

        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_flag_situacion == true)
            {
                obe.cntc_icod_situacion = 338;
                obe.cntc_flag_situacion = false;
                obe.strSituacion = "PENDIENTE";
                //new BVentas().modificarContratoCuotas(obe);
                cargar();
            }
            else
            {
                obe.cntc_icod_situacion = 340;
                obe.cntc_flag_situacion = true;
                obe.strSituacion = "CANCELADO";
                //new BVentas().modificarContratoCuotas(obe);
                cargar();
            }
        }

        private void viewDetalle_Click(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_icod_documento > 0)
            {
                seleccionarToolStripMenuItem.Enabled = false;
            }
            else
            {
                seleccionarToolStripMenuItem.Enabled = true;
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void consultarDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            FrmConsultarDocumentoscs frm = new FrmConsultarDocumentoscs();
            frm.cod = obe.cntc_icod_contrato_cuotas;
            frm.Text = "Consulta de Documentos Por Cuota";
            frm.cargar();
            frm.Show();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<EContratoCuotas> mlisdet = new BVentas().listarContratoCuotas(ObeC.cntc_icod_contrato).OrderBy(x => x.strTipoCredito).ToList();
            mlisdet.ForEach(x =>
            {
                x.cntc_sfecha_pago_cuota = x.cntc_sfecha_pago_cuota == null ? x.cntc_sfecha_pago_cuota : x.cntc_sfecha_pago_cuota.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : x.cntc_sfecha_pago_cuota;

                x.cntc_sfecha_pago_cuota = x.cntc_sfecha_pago_cuota == null ? (DateTime?)null : x.cntc_sfecha_pago_cuota.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : x.cntc_sfecha_pago_cuota;


                Tuple<string, string> tupla = new BVentas().ObtenerDocumentos(x.cntc_icod_contrato_cuotas);

                x.strfechaDocumentos = tupla.Item2;
                x.plnd_vnumero_doc = tupla.Item1;
                x.tipo = x.cntc_inro_cuotas == 0 ? "CI" : x.cntc_itipo_cuota > 0 ? "RP" + x.cntc_itipo_cuota : "P";
                x.tipo = x.cntc_icod_tipo_cuota == 5430 ? "P" : x.tipo;
            });

            rptEstadoCuentaXCuotasDet rpt = new rptEstadoCuentaXCuotasDet();
            rpt.cargar(ObeC, mlisdet);
            rpt.ShowPreview();
            //rptCuotasPorContrato rpt = new rptCuotasPorContrato();
            //rpt.cargar(ObeC, mlisdet);
            //rpt.ShowPreview();
        }
    }
}