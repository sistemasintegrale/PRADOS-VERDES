using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;
using System.Linq;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmMantePrestamoPersonalCuotasDet : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EPrestamo oBe = new EPrestamo();
        public List<EPrestamo> lstDetalle = new List<EPrestamo>();
        public int prtpd_icod_prestamo;
        public frmMantePrestamoPersonalCuotasDet()
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
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    bteNroDocumento.Enabled = Enabled;
        }


        public void setValues()
        {
            prtpd_icod_prestamo = oBe.prtpd_icod_prestamo;
            txtNroCuotas.Text = oBe.prtpd_inro_cuota.ToString();
            dtFechaCuota.EditValue = oBe.prtpd_sfecha_cuota;
            txtMonto.Text = oBe.prtpd_nmonto_cuota.ToString();
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
            setValues();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try 
            {
                oBe.prtpd_icod_prestamo = prtpd_icod_prestamo;
                oBe.prtpd_inro_cuota = Convert.ToInt32(txtNroCuotas.Text);
                oBe.strTipoPago = "CUOTA";
                oBe.prtpd_sfecha_cuota = Convert.ToDateTime(dtFechaCuota.EditValue);
                oBe.prtpd_nmonto_cuota = Convert.ToDecimal(txtMonto.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.prtpd_icod_situacion = 157;
                oBe.prtpd_icod_tipo_cuota = 337;
                oBe.strSituacion = "PENDIENTE";

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
                
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
                Flag = false;
            }
            finally
            {
                if (Flag)                
                    this.DialogResult = DialogResult.OK;                
            }
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave(); 
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePercepcionDetalle_Load(object sender, EventArgs e)
        {
            
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                dtFechaCuota.EditValue = DateTime.Today;

            }
            else
            {
                dtFechaCuota.EditValue = oBe.prtpd_sfecha_cuota;
            }

        }        
    }
}