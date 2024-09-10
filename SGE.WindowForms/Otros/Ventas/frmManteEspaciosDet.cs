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

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteEspaciosDet : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EEspaciosDet oBe = new EEspaciosDet();
        public List<EEspaciosDet> lstDetalle = new List<EEspaciosDet>();
        
        public frmManteEspaciosDet()
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
            txtNivel.Text = oBe.espad_vnivel;
            lkpSituacion.EditValue = oBe.espad_icod_isituacion;
            lkpEstado.EditValue = oBe.espad_icod_iestado;
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

                oBe.espad_vnivel = txtNivel.Text;
                oBe.espad_icod_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.espad_icod_iestado = Convert.ToInt32(lkpEstado.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                /**/
                oBe.strsituacion = lkpSituacion.Text;
                oBe.strestado = lkpEstado.Text;

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
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(10), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaVentaDet(11), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpEstado.EditValue = oBe.espad_icod_iestado;
                lkpSituacion.EditValue = oBe.espad_icod_isituacion;
            }
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    setValues();
        }        
    }
}