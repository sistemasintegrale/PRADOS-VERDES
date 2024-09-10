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
using SGE.WindowForms.Otros.bVentas;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmMantePrestamoPersonal : XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteContratoCuotas));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EPrestamo ObeC = new EPrestamo();

        public List<EPrestamo> lstDetalle = new List<EPrestamo>();
        public List<EPrestamo> lstDelete = new List<EPrestamo>();
        public int intcabe = 0;


        public EPrestamo ObeD = new EPrestamo();

        public frmMantePrestamoPersonal()
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

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                dtFechaPrestamo.EditValue = DateTime.Today;
                txtNroPrestamo.Text = new BPlanillas().ObtenerSeriePrestamos().PadLeft(5, '0');
                dteFecchaInicialPrestamo.EditValue = DateTime.Today;
            }



        }
        public void setValues()

        {
            intcabe = ObeC.prtpc_icod_prestamo;
            txtNroPrestamo.Text = ObeC.prtpc_vnumero_prestamo;
            dtFechaPrestamo.EditValue = ObeC.prtpc_sfecha_prestamo;
            txtNroCuotas.Text = ObeC.prtpc_inro_cuotas.ToString();
            txtMontoPrestamo.Text = ObeC.prtpc_nmonto_prestamo.ToString();
            txtMontoCuotas.Text = ObeC.prtpc_nmonto_cuota.ToString();
            btnPersonal.Tag = Convert.ToInt32(ObeC.prtpc_icod_personal);
            btnPersonal.Text = ObeC.strNombrePersonal;
            dteFecchaInicialPrestamo.EditValue = ObeC.prtpc_sfecha_inicio_prest;
            lkpTipoPago.EditValue = ObeC.prtpc_icod_tipo_pago;
            lstDetalle = new BPlanillas().ListarPrestamoCuotasPersonal(ObeC.prtpc_icod_prestamo);

            lstDetalle.ForEach(x =>
            {
                if (x.plnd_sfecha_doc.ToString() == "01/01/0001 12:00:00 a. m.")
                {
                    x.plnd_sfecha_doc = (DateTime?)null;
                }

                x.intTipoOperacion = 3;
            });
            calcular(lstDetalle);
            grdCuotas.DataSource = lstDetalle;
            if (lstDetalle.Count == 0)
            {
                btnGenerar.Enabled = true;
            }
            else
            {
                btnGenerar.Enabled = false;
                txtNroPrestamo.Properties.ReadOnly = true;
                txtMontoPrestamo.Properties.ReadOnly = true;
                btnPersonal.Enabled = false;
                txtMontoCuotas.Properties.ReadOnly = true;
                txtNroCuotas.Properties.ReadOnly = true;
                dtFechaPrestamo.Enabled = false;
                dteFecchaInicialPrestamo.Enabled = false;
                lkpTipoPago.Enabled = false;
                btnGenerar.Focus();

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

        private bool validar()
        {
            bool result = true;
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (String.IsNullOrEmpty(dteFecchaInicialPrestamo.Text))
                {
                    oBase = dteFecchaInicialPrestamo;

                    throw new Exception("Ingrese la Fecha Inicial del Prestamo");
                }
                if (String.IsNullOrEmpty(txtNroPrestamo.Text))
                {
                    oBase = txtNroPrestamo;

                    throw new Exception("Ingrese el N° de Préstamo");
                }

                if (String.IsNullOrEmpty(txtMontoPrestamo.Text))
                {
                    oBase = txtMontoPrestamo;

                    throw new Exception("Ingrese Monto del Préstamo ");
                }

                if (String.IsNullOrEmpty(txtMontoCuotas.Text))
                {
                    oBase = txtMontoCuotas;

                    throw new Exception("Ingrese el Monto de las Cuotas ");
                }

                if (String.IsNullOrEmpty(txtNroCuotas.Text))
                {
                    oBase = txtNroCuotas;

                    throw new Exception("Ingrese el N° de Cuotas");
                }

                if (String.IsNullOrEmpty(btnPersonal.Text))
                {
                    oBase = btnPersonal;

                    throw new Exception("Ingrese Personal ");
                }
                if (Convert.ToDecimal(txtMontoCuotas.Text) == 0)
                {
                    oBase = txtMontoCuotas;

                    throw new Exception("El Monto de Cuotas Debe ser mayor a 0");
                }
                if (Convert.ToDecimal(txtMontoPrestamo.Text) == 0)
                {
                    oBase = txtMontoPrestamo;

                    throw new Exception("El Monto de Préstamo Debe ser mayor a 0");
                }
                if (Convert.ToDecimal(txtNroCuotas.Text) == 0)
                {
                    oBase = txtNroCuotas;

                    throw new Exception("El Número de Cuotas Debe ser mayor a 0");
                }
                decimal cuotas = Convert.ToDecimal(txtNroCuotas.Text);
                decimal montoPrestamo = Convert.ToDecimal(txtMontoPrestamo.Text);
                decimal montoCuotas = Convert.ToDecimal(txtMontoCuotas.Text);
                decimal montototalCuotas = cuotas * montoCuotas;
                if (montototalCuotas != montoPrestamo)
                {
                    if (XtraMessageBox.Show("El Monto Total de Cuotas no Coincide con el Monto del Prestamo, ¿Desea Continuar? ", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        oBase = txtMontoCuotas;

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
                if (oBase != null)
                {
                    result = false;
                }

            }
            return result;

        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {


                ObeC.prtpc_icod_prestamo = intcabe;
                ObeC.prtpc_inro_cuotas = Convert.ToInt32(txtNroCuotas.Text);
                ObeC.prtpc_icod_personal = Convert.ToInt32(btnPersonal.Tag);
                ObeC.prtpc_vnumero_prestamo = txtNroPrestamo.Text;
                ObeC.prtpc_sfecha_prestamo = Convert.ToDateTime(dtFechaPrestamo.EditValue);
                ObeC.prtpc_nmonto_prestamo = Convert.ToDecimal(txtMontoPrestamo.Text);
                ObeC.prtpc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                ObeC.prtpc_icod_situacion = 218; //GENERADO
                ObeC.prtpc_flag_estado = true;
                ObeC.prtpc_iusuario_crea = Valores.intUsuario;
                ObeC.prtpc_vpc_crea = WindowsIdentity.GetCurrent().Name;
                ObeC.prtpc_iusuario_modifica = Valores.intUsuario;
                ObeC.prtpc_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                ObeC.prtpc_iusuario_elimina = Valores.intUsuario;
                ObeC.prtpc_vpc_elimina = WindowsIdentity.GetCurrent().Name;
                ObeC.prtpc_sfecha_inicio_prest = Convert.ToDateTime(dteFecchaInicialPrestamo.EditValue);
                ObeC.prtpc_icod_tipo_pago = Convert.ToInt32(lkpTipoPago.EditValue);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BPlanillas().InsertarPrestamoCuotasPersonal(ObeC, lstDetalle);
                }
                else
                {
                    new BPlanillas().ModificarPrestamoCuotasPersonal(ObeC, lstDetalle, lstDelete);

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
                    this.MiEvento(ObeC.prtpc_icod_prestamo);
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
            List<TipoPago> lstTipoPago = new List<TipoPago>();
            lstTipoPago.Add(new TipoPago() { id = 1, descripcion = "QUINCENAL" });
            lstTipoPago.Add(new TipoPago() { id = 2, descripcion = "MENSUAL" });
            txtMontoCuotas.Focus();
            BSControls.LoaderLook(lkpTipoPago, lstTipoPago, nameof(TipoPago.descripcion), nameof(TipoPago.id), true);
        }

        public class TipoPago
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }




        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            try
            {

                using (frmMantePrestamoPersonalCuotasDet frm = new frmMantePrestamoPersonalCuotasDet())
                {
                    if (lstDetalle.Count > 0)
                        frm.txtNroCuotas.Text = String.Format("{0:0}", lstDetalle.Count + 1);
                    else
                        frm.txtNroCuotas.Text = "1";
                    frm.SetInsert();
                    frm.prtpd_icod_prestamo = ObeC.prtpc_icod_prestamo;
                    frm.lstDetalle = lstDetalle;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewCuotas.RefreshData();
                        viewCuotas.Focus();
                    }
                    calcular(lstDetalle);
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

        private void calcular(List<EPrestamo> lstDetalle)
        {
            decimal monto = 0;
            lstDetalle.ForEach(x =>
            {
                monto = monto + x.prtpd_nmonto_cuota;
            });
            txtMontoTotalFooter.Text = monto.ToString();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPrestamo oBe_ = (EPrestamo)viewCuotas.GetRow(viewCuotas.FocusedRowHandle);
            if (oBe_ == null)
                return;
            if (oBe_.prtpd_icod_situacion == 348)
            {
                XtraMessageBox.Show("No se Puede Modificar la Cuota " + oBe_.prtpd_inro_cuota + ", ya se Encuentra con Pago", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            using (frmMantePrestamoPersonalCuotasDet frm = new frmMantePrestamoPersonalCuotasDet())
            {
                frm.oBe = oBe_;
                frm.prtpd_icod_prestamo = ObeC.prtpc_icod_prestamo;
                frm.SetModify();
                frm.lstDetalle = lstDetalle;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewCuotas.RefreshData();
                    viewCuotas.Focus();
                }
                calcular(lstDetalle);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPrestamo oBe_ = (EPrestamo)viewCuotas.GetRow(viewCuotas.FocusedRowHandle);
            if (oBe_ == null)
                return;
            if (oBe_.prtpd_icod_situacion == 348)
            {
                XtraMessageBox.Show("No se Puede Modificar la Cuota " + oBe_.prtpd_inro_cuota + ", ya se Encuentra con Pago", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            lstDelete.Add(oBe_);
            lstDetalle.Remove(oBe_);
            lstDetalle = reordenar(lstDetalle);
            viewCuotas.RefreshData();
            calcular(lstDetalle);
        }

        private List<EPrestamo> reordenar(List<EPrestamo> lstDetalle)
        {
            int numeroItem = 1;
            lstDetalle.ForEach(x =>
            {
                x.prtpd_inro_cuota = numeroItem;
                numeroItem++;

            });

            return lstDetalle;
        }

        DateTime FechaAnterior;

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!validar()) return;
            grdCuotas.DataSource = 0;
            int NroCuotas = Convert.ToInt32(txtNroCuotas.Text);
            FechaAnterior = Convert.ToDateTime(dteFecchaInicialPrestamo.EditValue);
            int diaIncio = Convert.ToInt32(FechaAnterior.Day);
            for (int y = 1; y <= NroCuotas; y++)
            {
                EPrestamo EDet = new EPrestamo();

                if (Convert.ToInt32(lkpTipoPago.EditValue) == 1)
                {



                    if (diaIncio <= 15) //SE REGISTRO ANTES DE LA QUINCENA
                    {
                        if ((y % 2) == 0)
                        {
                            EDet.prtpd_icod_prestamo = ObeC.prtpd_icod_prestamo;
                            EDet.prtpd_inro_cuota = y;
                            EDet.prtpd_sfecha_cuota = FechaAnterior.AddMonths(1).AddDays(-(FechaAnterior.Day));
                            EDet.prtpd_icod_tipo_cuota = 337;
                            EDet.strTipoPago = "CUOTA";
                            EDet.prtpd_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                            EDet.prtpd_icod_situacion = 157;
                            EDet.strSituacion = "PENDIENTE";
                            EDet.intUsuario = Valores.intUsuario;
                            EDet.strPc = WindowsIdentity.GetCurrent().Name;
                            lstDetalle.Add(EDet);
                            FechaAnterior = EDet.prtpd_sfecha_cuota;
                        }
                        else
                        {
                            EDet.prtpd_icod_prestamo = ObeC.prtpd_icod_prestamo;
                            EDet.prtpd_inro_cuota = y;
                            if (y == 1) // SOLO EN EL PRIMMER REGISTRO
                            {
                                int diasQuincena = 0;
                                diasQuincena = 15 - Convert.ToInt32(FechaAnterior.Day);
                                EDet.prtpd_sfecha_cuota = FechaAnterior.AddDays(diasQuincena);
                            }
                            else
                                EDet.prtpd_sfecha_cuota = FechaAnterior.AddDays(15);
                            EDet.prtpd_icod_tipo_cuota = 337;
                            EDet.strTipoPago = "CUOTA";
                            EDet.prtpd_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                            EDet.prtpd_icod_situacion = 157;
                            EDet.strSituacion = "PENDIENTE";
                            EDet.intUsuario = Valores.intUsuario;
                            EDet.strPc = WindowsIdentity.GetCurrent().Name;

                            lstDetalle.Add(EDet);
                            FechaAnterior = EDet.prtpd_sfecha_cuota;
                        }
                    }
                    else //SE REGISTRO DESPUES DE LA QUINCENA
                    {
                        if ((y % 2) == 0)
                        {
                            EDet.prtpd_icod_prestamo = ObeC.prtpd_icod_prestamo;
                            EDet.prtpd_inro_cuota = y;
                            EDet.prtpd_sfecha_cuota = FechaAnterior.AddDays(15);
                            EDet.prtpd_icod_tipo_cuota = 337;
                            EDet.strTipoPago = "CUOTA";
                            EDet.prtpd_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                            EDet.prtpd_icod_situacion = 157;
                            EDet.strSituacion = "PENDIENTE";
                            EDet.intUsuario = Valores.intUsuario;
                            EDet.strPc = WindowsIdentity.GetCurrent().Name;

                            lstDetalle.Add(EDet);
                            FechaAnterior = EDet.prtpd_sfecha_cuota;
                        }
                        else
                        {
                            EDet.prtpd_icod_prestamo = ObeC.prtpd_icod_prestamo;
                            EDet.prtpd_inro_cuota = y;
                            if (y == 1) // SOLO EN EL PRIMER REGISTRO
                            {
                                if (FechaAnterior.Day == FechaAnterior.AddMonths(1).AddDays(-(FechaAnterior.Day)).Day)//COMPRUEBA SI ES FIN DE MES
                                {
                                    EDet.prtpd_sfecha_cuota = FechaAnterior;
                                }
                                else
                                {
                                    EDet.prtpd_sfecha_cuota = FechaAnterior.AddMonths(1).AddDays(-(FechaAnterior.Day));
                                }

                            }
                            else
                                EDet.prtpd_sfecha_cuota = FechaAnterior.AddMonths(1).AddDays(-(FechaAnterior.Day));

                            EDet.prtpd_icod_tipo_cuota = 337;
                            EDet.strTipoPago = "CUOTA";
                            EDet.prtpd_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                            EDet.prtpd_icod_situacion = 157;
                            EDet.strSituacion = "PENDIENTE";
                            EDet.intUsuario = Valores.intUsuario;
                            EDet.strPc = WindowsIdentity.GetCurrent().Name;

                            lstDetalle.Add(EDet);
                            FechaAnterior = EDet.prtpd_sfecha_cuota;
                        }
                    }
                }
                else
                {
                    EDet.prtpd_icod_prestamo = ObeC.prtpd_icod_prestamo;
                    EDet.prtpd_inro_cuota = y;
                    EDet.prtpd_sfecha_cuota = FechaAnterior.AddMonths(y-1);
                    EDet.prtpd_icod_tipo_cuota = 337;
                    EDet.strTipoPago = "CUOTA";
                    EDet.prtpd_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                    EDet.prtpd_icod_situacion = 157;
                    EDet.strSituacion = "PENDIENTE";
                    EDet.intUsuario = Valores.intUsuario;
                    EDet.strPc = WindowsIdentity.GetCurrent().Name;

                    lstDetalle.Add(EDet);
                }
            }

            calcular(lstDetalle);
            grdCuotas.DataSource = lstDetalle;
            viewCuotas.RefreshData();
            btnGenerar.Enabled = false;
            txtNroPrestamo.Properties.ReadOnly = true;
            txtMontoPrestamo.Properties.ReadOnly = true;
            btnPersonal.Enabled = false;
            txtMontoCuotas.Properties.ReadOnly = true;
            txtNroCuotas.Properties.ReadOnly = true;
            dtFechaPrestamo.Enabled = false;
            dteFecchaInicialPrestamo.Enabled = false;
            lkpTipoPago.Enabled = false;
        }



        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewCuotas.GetRow(viewCuotas.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_icod_documento == 0)
            {
                if (obe.cntc_flag_situacion == true)
                {
                    obe.cntc_icod_situacion = 338;
                    obe.cntc_flag_situacion = false;

                }
                else
                {
                    obe.cntc_icod_situacion = 340;
                    obe.cntc_flag_situacion = true;

                }
            }
            else
            {
                if (obe.cntc_flag_situacion == true)
                {
                    obe.cntc_flag_situacion = true;

                    viewCuotas.RefreshData();
                    grdCuotas.Refresh();
                }
                else
                {
                    obe.cntc_flag_situacion = false;
                    viewCuotas.RefreshData();
                    grdCuotas.Refresh();
                }
            }

        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPrestamo obe = (EPrestamo)viewCuotas.GetRow(viewCuotas.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.prtpd_icod_situacion == 157)
            {
                obe.prtpd_icod_situacion = 348;
                obe.strSituacion = "CANCELADO";

            }
            else
            {
                obe.prtpd_icod_situacion = 157;
                obe.strSituacion = "PENDIENTE";

            }
            if (obe.intTipoOperacion == 3)
            {
                obe.intTipoOperacion = 2;
            }
        }

        private void viewDetalle_Click(object sender, EventArgs e)
        {
            EPrestamo obe = (EPrestamo)viewCuotas.GetRow(viewCuotas.FocusedRowHandle);
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

        private void dtFechaPrestamo_EditValueChanged(object sender, EventArgs e)
        {



        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            frmListarPersonalSimple frm = new frmListarPersonalSimple();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnPersonal.Text = frm._Be.ApellNomb;
                btnPersonal.Tag = frm._Be.perc_icod_personal;
            }
        }

        private void grdCuotas_Click(object sender, EventArgs e)
        {

        }
    }
}