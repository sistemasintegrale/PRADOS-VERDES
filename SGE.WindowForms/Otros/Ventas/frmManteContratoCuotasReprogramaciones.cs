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
using System.Transactions;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteContratoCuotasReprogramaciones : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContratoCuotas));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContratoCuotas Obe = new EContratoCuotas();
        //public List<EEspacios> lstEspacios = new List<EEspacios>();

        public List<EContratoCuotas> lstDetalle = new List<EContratoCuotas>();
        public List<EContratoCuotas> lstDelete = new List<EContratoCuotas>();
        public List<EContratoCuotas> lstDetalleAnterior = new List<EContratoCuotas>();
        public EReprogramaciones obj = new EReprogramaciones();
        public bool modificar = false;


        public EContratoCuotas ObeC = new EContratoCuotas();

        public frmManteContratoCuotasReprogramaciones()
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
            txtNroReprogamacion.Enabled = Enabled;

            //lkpNivel.Enabled = !Enabled;
            //lkpSituacion.Enabled = !Enabled;           
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtNroReprogamacion.Enabled = Enabled;


            if (Status == BSMaintenanceStatus.CreateNew)
                txtNroReprogamacion.Enabled = Enabled;


        }
        public void setValues()

        {
            txtfinanciamiento.Text = obj.cntcr_nmonto_financiamiento.ToString();
            txtsaldoAnterior.Text = obj.cntcr_nmonto_saldo_anterior.ToString();
            txtNroCuotas.Text = obj.cntcr_inro_cuotas.ToString();
            txtMontoCuotas.Text = obj.cntcr_nmonto_cuota.ToString();
            txtNroReprogamacion.Text = obj.cntcr_iid_reprogramacion.ToString();
            dtFechaCuota.EditValue = obj.cntcr_sfecha_cuota;
            txtMontoTotal.Text = obj.cntcr_nmonto_total.ToString();
            txtvariacionInteres.Text = obj.cntcr_nvariacion_interes.ToString();
            lkpNombrePlan.EditValue = obj.cntcr_iplan;
            txtObservaciones.Text = obj.cntcr_vobservaciones;
            lstDetalle = new BVentas().listarContratoCuotas(obj.cntc_icod_contrato).Where(x => x.cntc_itipo_cuota == obj.cntcr_iid_reprogramacion).ToList();


            grdDetalle.DataSource = lstDetalle;
            grdDetalle.Refresh();


            if (lstDetalle.Count == 0)
            {
                Status = BSMaintenanceStatus.CreateNew;
            }
            else
            {
                Status = BSMaintenanceStatus.ModifyCurrent;
                btnGenerar.Enabled = true;
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

        void refresh()
        {
            lstDetalle = new BVentas().listarContratoCuotas(obj.cntc_icod_contrato).Where(x => x.cntc_itipo_cuota == obj.cntcr_iid_reprogramacion).ToList();
            lstDetalle.ForEach(x =>
            {
                x.cntc_sfecha_pago_cuota = x.cntc_sfecha_pago_cuota.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : x.cntc_sfecha_pago_cuota;
            });

            grdDetalle.DataSource = lstDetalle;
            grdDetalle.Refresh();
        }
        private void cargar()
        {

            if (modificar == false)
            {
                //lstDetalle = new BVentas().listarContratoCuotas(ObeC.cntc_icod_contrato).Where(x => x.cntc_itipo_cuota == 0).ToList();
                lstDetalle = new BVentas().listarContratoCuotas(ObeC.cntc_icod_contrato);
                if (lstDetalle.Count() == 0)
                {
                    XtraMessageBox.Show("No se Encontraron Cuotas para Reprogramar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                }
                else
                {

                    var reprograciones = new BVentas().ListarReprogramaciones(ObeC.cntc_icod_contrato);
                    txtNroReprogamacion.Text = reprograciones.Count == 0 ? "1" : Convert.ToInt32(reprograciones.Max(x => x.cntcr_iid_reprogramacion + 1)).ToString();
                    txtsaldoAnterior.Text = lstDetalle.Where(x => Convert.ToInt32(x.cntc_icod_situacion) == 338 || Convert.ToInt32(x.cntc_icod_situacion) == 339).Sum(x => x.cntc_nsaldo).ToString();
                    dtFechaCuota.EditValue = DateTime.Today.Date;
                    lstDetalle.Clear();
                }
            }
            else
            {
                txtNroCuotas.Text = ObeC.numero_cuotas.ToString();
                txtNroReprogamacion.Text = ObeC.cntc_itipo_cuota.ToString();
                txtsaldoAnterior.Text = ObeC.monto_total.ToString();
                txtMontoCuotas.Text = ObeC.monto_cuota.ToString();
                dtFechaCuota.EditValue = ObeC.cntc_sfecha_cuota;
                btnGenerar.Enabled = false;
                txtNroCuotas.Properties.ReadOnly = true;
                txtsaldoAnterior.ReadOnly = true;
                txtMontoCuotas.ReadOnly = true;
                dtFechaCuota.Enabled = false;

                lstDetalle = new BVentas().listarContratoCuotas(ObeC.cntc_icod_contrato).Where(x => x.cntc_itipo_cuota == ObeC.cntc_itipo_cuota).ToList();
                grdDetalle.DataSource = lstDetalle;
                grdDetalle.RefreshDataSource();
                grdDetalle.Refresh();
            }
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (lstDetalle.Count() == 0)
                {

                    throw new ArgumentException("Ingrese al Menos una Cuota");
                }
                if (String.IsNullOrEmpty(txtNroReprogamacion.Text))
                {
                    oBase = txtNroReprogamacion;
                    throw new ArgumentException("Ingrese código de la Funeraria");
                }

                obj.cntcr_iplan = Convert.ToInt32(lkpNombrePlan.EditValue);
                obj.cntcr_vobservaciones = txtObservaciones.Text;

                obj.cntcr_iid_reprogramacion = Convert.ToInt32(txtNroReprogamacion.Text);
                obj.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                obj.cntcr_inro_cuotas = Convert.ToInt32(txtNroCuotas.Text);
                obj.cntcr_sfecha_cuota = Convert.ToDateTime(dtFechaCuota.EditValue);
                obj.cntcr_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                obj.cntcr_iusuario_crea = Valores.intUsuario;
                obj.cntcr_vpc_crea = WindowsIdentity.GetCurrent().Name;
                obj.cntcr_iusuario_modifica = Valores.intUsuario;
                obj.cntcr_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                obj.cntcr_nmonto_saldo_anterior = Convert.ToDecimal(txtsaldoAnterior.Text);
                obj.cntcr_nmonto_cuota_total = lstDetalle.Sum(x => x.cntc_nmonto_cuota);
                obj.cntcr_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                obj.cntcr_nvariacion_interes = Convert.ToDecimal(txtvariacionInteres.Text);
                obj.cntcr_nmonto_financiamiento = Convert.ToDecimal(txtfinanciamiento.Text);
                lstDetalleAnterior = new BVentas().listarContratoCuotas(ObeC.cntc_icod_contrato);
                lstDetalleAnterior.ForEach(x =>
                {
                    if (x.cntc_icod_situacion == 338)
                    {
                        x.intTipoOperacion = 2;
                        x.cntc_icod_situacion = 6437; // REPROGAMADO
                    }
                });




                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    
                    if (obj.cntcr_nmonto_cuota_total != obj.cntcr_nmonto_total)
                    {
                        if (XtraMessageBox.Show("El Monto Total de Las Cuotas no Coincide con el Saldo Anterior, ¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            Flag = false;
                            return;
                        }
                    }

                    using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                    {
                        obj.cntcr_icod_reprogracion = new BVentas().InsertarReprogramacion(obj, lstDetalle);                                   

                        lstDetalleAnterior.Where(x => x.intTipoOperacion == 2).ToList().ForEach(x => {
                            new BVentas().modificarContratoCuotas(x);
                        });
                        new BVentas().ActualizarReprogramacion(obj.cntcr_icod_reprogracion, ObeC.cntc_icod_contrato, obj.cntcr_iid_reprogramacion);
                        tx.Complete();
                    }

                    
                }
                else
                {
                    using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                    {
                        new BVentas().ModificarReprogramacion(obj, lstDetalle, lstDelete);
                        new BVentas().ActualizarReprogramacion(obj.cntcr_icod_reprogracion, ObeC.cntc_icod_contrato, obj.cntcr_iid_reprogramacion);
                        tx.Complete();
                    }
                   

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
                    this.MiEvento(Convert.ToInt32(txtNroReprogamacion.Text));
                    this.Close();
                }
            }
        }



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
            BSControls.LoaderLook(lkpNombrePlan, new BGeneral().listarTablaVentaDet(13), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
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
                    {
                        frm.txtNroCuotas.Text = String.Format("{0:0}", lstDetalle.Max(x => x.cntc_inro_cuotas) + 1);
                        frm.correlativo = Convert.ToInt32(frm.txtNroCuotas.Text);
                    }
                    else
                    {
                        frm.txtNroCuotas.Text = "1";
                        frm.correlativo = 1;
                    }

                    frm.SetInsert();
                    frm.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                    frm.lstDetalle = lstDetalle;
                    frm.txtCronograma.Text = txtNroReprogamacion.Text;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        grdDetalle.DataSource = lstDetalle;
                        grdDetalle.Refresh();
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
                frm.txtCronograma.Text = txtNroReprogamacion.Text;
                frm.correlativo = oBe_.cntc_inro_cuotas;
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
            if (acceso(modificarDatosToolStripMenuItem.Name) == 0)
                return;
            lstDelete.Add(oBe_);
            lstDetalle.Remove(oBe_);
            grdDetalle.DataSource = lstDetalle;
            grdDetalle.Refresh();
            viewDetalle.RefreshData();
            btnGenerar.Enabled = lstDetalle.Count() == 0 ? true : false;


        }

        DateTime FechaAnterior;

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtNroCuotas.Text) == 0)
            {
                XtraMessageBox.Show("Ingrese el Número de Cuotas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToDecimal(txtMontoCuotas.Text) == 0)
            {
                XtraMessageBox.Show("Ingrese el Monto de las Cuotas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            grdDetalle.DataSource = 0;
            lstDetalle.Clear();
            int NroCuotas = (Convert.ToInt32(txtNroCuotas.Text));
            decimal total = Convert.ToDecimal(txtMontoTotal.Text);
            for (int y = 0; y < NroCuotas; y++)
            {
                if (y == (NroCuotas-1))
                {
                    decimal restante;
                    decimal acumulado = lstDetalle.Sum(x => x.cntc_nmonto_cuota);
                    restante = total - acumulado;
                    EContratoCuotas EDet = new EContratoCuotas();
                    EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                    EDet.cntc_inro_cuotas = y+1;
                    EDet.cntc_sfecha_cuota = dtFechaCuota.DateTime.AddMonths(y);
                    EDet.cntc_icod_tipo_cuota = 337;
                    EDet.strTipo = "CUOTA";
                    EDet.cntc_nmonto_cuota = restante;
                    EDet.cntc_icod_situacion = 338;
                    EDet.cntc_nsaldo = EDet.monto_cuota;
                    EDet.cntc_npagado = 0;
                    EDet.strSituacion = "PENDIENTE";
                    EDet.intUsuario = Valores.intUsuario;
                    EDet.strPc = WindowsIdentity.GetCurrent().Name;
                    EDet.cntc_itipo_cuota = Convert.ToInt32(txtNroReprogamacion.Text);
                    EDet.cntc_nmonto_mora = 0;
                    EDet.cntc_nmonto_mora_pago = 0;
                    lstDetalle.Add(EDet);
                }
                else
                {
                    EContratoCuotas EDet = new EContratoCuotas();
                    EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                    EDet.cntc_inro_cuotas = y+1;
                    EDet.cntc_sfecha_cuota = dtFechaCuota.DateTime.AddMonths(y);
                    EDet.cntc_icod_tipo_cuota = 337;
                    EDet.strTipo = "CUOTA";
                    EDet.cntc_nsaldo = EDet.monto_cuota;
                    EDet.cntc_npagado = 0;
                    EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                    EDet.cntc_icod_situacion = 338;
                    EDet.strSituacion = "PENDIENTE";
                    EDet.intUsuario = Valores.intUsuario;
                    EDet.strPc = WindowsIdentity.GetCurrent().Name;
                    EDet.cntc_itipo_cuota = Convert.ToInt32(txtNroReprogamacion.Text);
                    EDet.cntc_nmonto_mora = 0;
                    EDet.cntc_nmonto_mora_pago = 0;
                    lstDetalle.Add(EDet);
                }

            }
            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
            btnGenerar.Enabled = false;
        }

        internal void setview()
        {
            mnu.Enabled = false;
            btnGuardar.Enabled = false;
            btnGenerar.Enabled = false;
            txtMontoCuotas.Enabled = false;
            txtNroCuotas.Enabled = false;
            txtNroReprogamacion.Enabled = false;
            txtsaldoAnterior.Enabled = false;
            dtFechaCuota.Enabled = false;
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
            if (acceso(modificarDatosToolStripMenuItem.Name) == 0)
                return;
            if (obe.cntc_flag_situacion == true)
            {
              
                    obe.cntc_nsaldo = obe.cntc_nmonto_cuota;
                    obe.cntc_npagado = 0;
                    obe.cntc_icod_situacion = 338;
                    obe.cntc_flag_situacion = false;
                    obe.strSituacion = "PENDIENTE";
                    //new BVentas().modificarContratoCuotas(obe);
                    obe.cntc_nmonto_mora_pago = Convert.ToDecimal(0);
                    obe.cntc_sfecha_pago_cuota = (DateTime?)null;               
                
            }
            else
            {
                FrmIngresarFechaPagoCuota frm = new FrmIngresarFechaPagoCuota();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (Convert.ToDecimal(frm.txtMonto.Text) > obe.cntc_nmonto_cuota)
                    {
                        XtraMessageBox.Show("El Monto Pagado no Puede ser Mayor que el Monto de la Cuota ", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    obe.cntc_nsaldo = obe.cntc_nmonto_cuota - Convert.ToDecimal(frm.txtMonto.Text);
                    obe.cntc_npagado = Convert.ToDecimal(frm.txtMonto.Text);
                    obe.cntc_icod_situacion = obe.cntc_nsaldo == 0 ? 340 : 339;
                    obe.cntc_flag_situacion = true;
                    obe.strSituacion = obe.cntc_nsaldo == 0 ? "CANCELADO" : "PARCIALMENTE PAGADO";
                    obe.cntc_nmonto_mora_pago = obe.cntc_nmonto_mora;
                    obe.cntc_sfecha_pago_cuota = frm.dteFechaPago.DateTime;
                     
                }
            }
            grdDetalle.Refresh();
            grdDetalle.RefreshDataSource();
            viewDetalle.RefreshData();
        }

        private void viewDetalle_Click(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;

        }



        private void viewDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
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
        public void total()
        {
            var salAnterior = Convert.ToDecimal(txtsaldoAnterior.Text);
            var interes = Convert.ToDecimal(txtvariacionInteres.Text);
            decimal total = salAnterior + interes;
            txtMontoTotal.Text = total.ToString();
        }

        private void txtsaldoAnterior_EditValueChanged(object sender, EventArgs e)
        {
            total();
        }

        private void txtvariacionInteres_EditValueChanged(object sender, EventArgs e)
        {
            total();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void pagoMoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            FrmMantePagoCuotas frm = new FrmMantePagoCuotas();
            frm.oBe_ = oBe_;
            frm.Text = string.Format("Mora de ReProgramación {0} - Couta N° :{1} ", obj.cntcr_iid_reprogramacion, oBe_.cntc_inro_cuotas);
            frm.cargardatos();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                oBe_ = frm.oBe_;
                viewDetalle.RefreshData();
            }
        }

        private void mnu_Opening(object sender, CancelEventArgs e)
        {
            EContratoCuotas obje = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obje == null)
                return;

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (obje.plnd_vnumero_doc != null)
                {
                    seleccionarToolStripMenuItem.Enabled = false;
                    consultarDocumentosToolStripMenuItem.Enabled = true;
                }
                else
                {
                    seleccionarToolStripMenuItem.Enabled = true;
                    consultarDocumentosToolStripMenuItem.Enabled = false;
                }

                if (obje.strSituacion.ToUpper() == "CANCELADO" || obje.strSituacion.ToUpper() == "PARCIALMENTE PAGADO")
                {

                    modificarToolStripMenuItem.Enabled = false;
                    eliminarToolStripMenuItem.Enabled = false;
                }
                else
                {

                    modificarToolStripMenuItem.Enabled = true;
                    eliminarToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                EContratoCuotas obe = new BVentas().listarContratoCuotas(obj.cntc_icod_contrato).Where(x => x.cntc_icod_contrato_cuotas == obje.cntc_icod_contrato_cuotas).FirstOrDefault();
                if (obe == null) return;
                if (obe.plnd_vnumero_doc != null)
                {
                    seleccionarToolStripMenuItem.Enabled = false;
                    consultarDocumentosToolStripMenuItem.Enabled = true;
                }
                else
                {
                    seleccionarToolStripMenuItem.Enabled = true;
                    consultarDocumentosToolStripMenuItem.Enabled = false;
                }

                if (obe.strSituacion.ToUpper() == "CANCELADO" || obe.strSituacion.ToUpper() == "PARCIALMENTE PAGADO")
                {

                    modificarToolStripMenuItem.Enabled = false;
                    eliminarToolStripMenuItem.Enabled = false;
                }
                else
                {

                    modificarToolStripMenuItem.Enabled = true;
                    eliminarToolStripMenuItem.Enabled = true;
                }
            }
 
        }
        public int acceso(string form)
        {
            int flag;
            flag = Modules.Valores.lstAccesosUsuario.FindIndex(x => x.formc_vnombre_forms == form);
            if (flag >= 0)
                flag = 1;
            else
            {
                XtraMessageBox.Show("No Tiene Permiso Para Modifcar Mendiante esta Opcion", "Datos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                flag = 0;
            }

            return flag;
        }

        private void modificarDatosDeCuotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            if (acceso(modificarDatosToolStripMenuItem.Name) == 0)
                return;
            using (frmManteContratoCuotasDet frm = new frmManteContratoCuotasDet())
            {
                frm.oBe = oBe_;
                frm.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                frm.SetModify();
                frm.lstDetalle = lstDetalle;
                frm.txtCronograma.Text = txtNroReprogamacion.Text;
                frm.correlativo = oBe_.cntc_inro_cuotas;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.Focus();
                }
            }
        }
    }


}