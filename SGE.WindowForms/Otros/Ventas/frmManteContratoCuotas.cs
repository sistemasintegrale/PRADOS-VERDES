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
using static SGE.Common.TableVenta;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteContratoCuotas : DevExpress.XtraEditors.XtraForm
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
        object a;
        public frmManteContratoCuotas()
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
            txtAporteFondo.Text = ObeC.cntc_naporte_fondo == 0 ? ObeC.cntc_nmonto_foma.ToString() : ObeC.cntc_naporte_fondo.ToString();
            if (ObeC.cntc_itipo_pago != 0)
                lkpTipoPago.EditValue = ObeC.cntc_itipo_pago;
            else
            {
                if (ObeC.cntc_inro_cuotas != 0)
                {
                    lkpTipoPago.EditValue = 674;
                }
            }
            lstDetalle = new BVentas().listarContratoCuotas(ObeC.cntc_icod_contrato).Where(x => x.cntc_itipo_cuota == 0).ToList();
            lstDetalle.ForEach(x =>
            {
                if (x.cntc_icod_documento > 0)
                {
                    x.cntc_flag_situacion = true;
                }

            });
            grdDetalle.DataSource = lstDetalle.OrderBy(x => x.cntc_itipo_cuota);
            viewDetalle.BestFitColumns();
            btnReload.Visible = lstDetalle.Count() == 0 ? false : true;

            if (lstDetalle.Where(x => x.cntc_icod_tipo_cuota != 336).Count() == 0)
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
            btnReload.Visible = false;
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

            BSControls.LoaderLook(lkpTipoPago, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpTipoPago.Enabled = false;
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
                    this.MiEvento(ObeC.cntc_icod_contrato);
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
                    frm.txtCronograma.Text = "0";
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
            viewDetalle.RefreshData();
            grdDetalle.DataSource = lstDetalle;
            grdDetalle.RefreshDataSource();
            btnGenerar.Enabled = lstDetalle.Count() == 0 ? true : false;

        }


        private void btnGenerar_Click(object sender, EventArgs e)
        {
            bool existeCuotaInicial = lstDetalle.Where(x => x.cntc_icod_tipo_cuota == 336).Any();
            grdDetalle.DataSource = 0;
            if (Convert.ToInt32(lkpTipoPago.EditValue) == 674) // CREDITO
            {
                int NroCuotas = (Convert.ToInt32(txtNroCuotas.Text));

                for (int y = 0; y <= NroCuotas; y++)
                {
                    EContratoCuotas EDet = new EContratoCuotas();
                    if (y == 0)
                    {
                        if (!existeCuotaInicial)
                        {
                            EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                            EDet.cntc_inro_cuotas = y;
                            EDet.cntc_sfecha_cuota = Convert.ToDateTime(ObeC.cntc_sfecha_contrato);
                            EDet.cntc_icod_tipo_cuota = 336;
                            EDet.strTipo = "C. INICIAL";
                            EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtCuotaInicial.Text);
                            EDet.cntc_icod_situacion = 338;
                            EDet.strSituacion = "PENDIENTE";
                            EDet.intUsuario = Valores.intUsuario;
                            EDet.strPc = WindowsIdentity.GetCurrent().Name;
                            EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                            EDet.cntc_npagado = 0;
                            EDet.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                            EDet.strTipoCredito = "PRINCIPAL";
                            EDet.cntc_nmonto_mora = 0;
                            EDet.cntc_nmonto_mora_pago = 0;
                            EDet.intTipoOperacion = 1;
                            lstDetalle.Add(EDet);
                        }





                    }
                    else if (y == 1)
                    {
                        EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                        EDet.cntc_inro_cuotas = y;
                        EDet.cntc_sfecha_cuota = Convert.ToDateTime(dtFechaCuota.EditValue);
                        EDet.cntc_icod_tipo_cuota = 337;
                        EDet.strTipo = "CUOTA";
                        EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                        EDet.cntc_icod_situacion = 338;
                        EDet.strSituacion = "PENDIENTE";
                        EDet.intUsuario = Valores.intUsuario;
                        EDet.strPc = WindowsIdentity.GetCurrent().Name;
                        EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                        EDet.cntc_npagado = 0;
                        EDet.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                        EDet.strTipoCredito = "PRINCIPAL";
                        EDet.cntc_nmonto_mora = 0;
                        EDet.cntc_nmonto_mora_pago = 0;
                        EDet.intTipoOperacion = 1;
                        lstDetalle.Add(EDet);


                    }
                    else
                    {

                        EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                        EDet.cntc_inro_cuotas = y;
                        EDet.cntc_sfecha_cuota = Convert.ToDateTime(dtFechaCuota.DateTime.AddMonths(y - 1));
                        EDet.cntc_icod_tipo_cuota = 337;
                        EDet.strTipo = "CUOTA";
                        EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                        EDet.cntc_icod_situacion = 338;
                        EDet.strSituacion = "PENDIENTE";
                        EDet.intUsuario = Valores.intUsuario;
                        EDet.strPc = WindowsIdentity.GetCurrent().Name;
                        EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                        EDet.cntc_npagado = 0;
                        EDet.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                        EDet.strTipoCredito = "PRINCIPAL";
                        EDet.cntc_nmonto_mora = 0;
                        EDet.cntc_nmonto_mora_pago = 0;
                        EDet.intTipoOperacion = 1;
                        lstDetalle.Add(EDet);

                    }

                }


                grdDetalle.DataSource = lstDetalle;
                viewDetalle.RefreshData();
                viewDetalle.BestFitColumns();
                btnReload.Visible = false;
            }
            else
            {
                ETablaVentaDet obj = new BGeneral().listarTablaVentaDet(15).Where(x => x.tabvd_iid_tabla_venta_det == 5430).FirstOrDefault();
                EContratoCuotas EDetF = new EContratoCuotas();
                EDetF.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                EDetF.cntc_inro_cuotas = 1;
                EDetF.cntc_sfecha_cuota = Convert.ToDateTime(ObeC.cntc_sfecha_contrato);
                EDetF.cntc_icod_tipo_cuota = obj.tabvd_iid_tabla_venta_det;
                EDetF.strTipo = obj.tabvd_vdescripcion;
                EDetF.cntc_nmonto_cuota = Convert.ToDecimal(txtPrecioTotal.Text);
                EDetF.cntc_icod_situacion = 338;
                EDetF.strSituacion = "PENDIENTE";
                EDetF.intUsuario = Valores.intUsuario;
                EDetF.strPc = WindowsIdentity.GetCurrent().Name;
                EDetF.cntc_nsaldo = EDetF.cntc_nmonto_cuota;
                EDetF.cntc_npagado = 0;
                EDetF.cntc_nmonto_mora_pago = 0;
                EDetF.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                EDetF.strTipoCredito = "PRINCIPAL";
                EDetF.cntc_nmonto_mora = 0;
                EDetF.intTipoOperacion = 1;
                lstDetalle.Add(EDetF);

                grdDetalle.DataSource = lstDetalle;
                viewDetalle.RefreshData();
                btnGenerar.Enabled = false;
            }
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
            if (acceso(modificarDatosToolStripMenuItem.Name) == 0)
                return;
            if (obe.cntc_icod_situacion != 338)
            {
                obe.cntc_nsaldo = obe.cntc_nmonto_cuota;
                obe.cntc_npagado = Convert.ToDecimal(0);
                obe.cntc_icod_situacion = 338;
                obe.cntc_flag_situacion = false;
                obe.strSituacion = "PENDIENTE";
                obe.cntc_nmonto_mora_pago = Convert.ToDecimal(0);
                obe.cntc_sfecha_pago_cuota = (DateTime?)null;
                //new BVentas().modificarContratoCuotas(obe);
                //cargar();
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
                    //new BVentas().modificarContratoCuotas(obe);
                    //cargar();
                }
            }
            grdDetalle.Refresh();
            grdDetalle.RefreshDataSource();
            viewDetalle.RefreshData();

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
            frm.ShowDialog();
            var lstpago = new BVentas().Listar_Pagos_Documentos(obe.cntc_icod_contrato_cuotas);
            if (lstpago.Any() && obe.cntc_icod_situacion == 338)
            {
                obe.intUsuario = Valores.intUsuario;
                obe.cntc_npagado = lstpago.Sum(x => x.pgc_nmonto_pago);
                obe.cntc_nmonto_mora_pago = lstpago.Sum(x => x.pgc_nmonto_pago_mora);
                obe.cntc_nsaldo = obe.cntc_nmonto_cuota - obe.cntc_npagado;
                obe.cntc_icod_situacion = Math.Round(obe.cntc_nsaldo, 0, MidpointRounding.AwayFromZero) == 0 ? (int)EstadoCuota.Cancelado : (int)EstadoCuota.ParcialmentePagado;

                var listaAux = new List<EContratoCuotas>();
                listaAux.Add(obe);
                new BVentas().modificarCCuotas(listaAux, new List<EContratoCuotas>()); ;
                setValues();

            }
        }

        private void viewDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            setValues();
        }

        private void eliminarSeleccionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstDetalle.Count == 0)
                return;
            List<EContratoCuotas> listAux = new List<EContratoCuotas>();
            lstDetalle.ForEach(x =>
            {
                if (x.eliminar == true)
                {

                    if (x.cntc_icod_documento > 0 || x.strSituacion.ToUpper() == "CANCELADO" || x.strSituacion.ToUpper() == "PARCIALMENTE PAGADO" || x.strSituacion.ToUpper() == "REPROGRAMADO")
                    {
                        XtraMessageBox.Show(string.Format("No se Puede Eliminar la cuota {0}, su Situacion es {1}", x.cntc_inro_cuotas, x.strSituacion), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    else
                    {
                        lstDelete.Add(x);
                        listAux.Add(x);

                    }


                }
            });

            listAux.ForEach(x =>
            {
                lstDetalle.Remove(x);
            });
            viewDetalle.RefreshData();
            grdDetalle.DataSource = lstDetalle;
            grdDetalle.RefreshDataSource();
            btnGenerar.Enabled = lstDetalle.Count() == 0 ? true : false;
        }

        private void pagarCuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            FrmMantePagoCuotas frm = new FrmMantePagoCuotas();
            frm.oBe_ = oBe_;
            frm.Text = string.Format("Mora de Contrato {0} - Couta N° :{1} ", ObeC.cntc_vnumero_contrato, oBe_.cntc_inro_cuotas);
            frm.cargardatos();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                oBe_ = frm.oBe_;
                viewDetalle.RefreshData();
            }
        }

        private void mnu_Opening(object sender, CancelEventArgs e)
        {
            a = e;
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;

            if (new BVentas().Listar_Pagos_Documentos(obe.cntc_icod_contrato_cuotas).Count() != 0)
            {
                PendienteCanceladoToolStripMenuItem.Enabled = false;
                consultarDocumentosToolStripMenuItem.Enabled = true;
            }
            else
            {
                PendienteCanceladoToolStripMenuItem.Enabled = true;
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
            if (obe.strSituacion.ToUpper() == "REPROGRAMADO")
            {

                modificarToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem.Enabled = false;
                PendienteCanceladoToolStripMenuItem.Enabled = false;
                pagarCuotasToolStripMenuItem.Enabled = false;
            }


        }
        public int acceso(string form)
        {
            int flag;
            flag = Valores.lstAccesosUsuario.FindIndex(x => x.formc_vnombre_forms == form);
            if (flag >= 0)
                flag = 1;
            else
            {
                XtraMessageBox.Show("No Tiene Permiso Para Modifcar Mendiante esta Opcion", "Datos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                flag = 0;
            }

            return flag;
        }

        private void modificarDatosToolStripMenuItem_Click(object sender, EventArgs e)
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