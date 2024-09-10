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
    public partial class frmManteRegistroProgramacionDet : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public ERegistroProgramacionDet oBe = new ERegistroProgramacionDet();
        public List<ERegistroProgramacionDet> lstDetalle = new List<ERegistroProgramacionDet>();
        
        public frmManteRegistroProgramacionDet()
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
            txtOrden.Text = oBe.rpd_iorden.ToString();
            txtHoraInicial.Text = oBe.rpd_vhora_inicio;
            txtHoraFinal.Text = oBe.rpd_vhora_final;
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
                string HI = txtHoraInicial.Text;
                if (HI.Length < 5)
                {
                    oBase = txtHoraInicial;
                    throw new ArgumentException("Ingrese Hora Inicial Correctamente");
                }
                string HF = txtHoraFinal.Text;
                if (HF.Length < 5)
                {
                    oBase = txtHoraInicial;
                    throw new ArgumentException("Ingrese Hora Final Correctamente");
                }
                if (Convert.ToDateTime(txtHoraFinal.Text) < Convert.ToDateTime(txtHoraInicial.Text))
                {
                    //oBase = txtHoraInicial;
                    throw new ArgumentException("Hora Final no puede ser menor a Hora Inicial");
                }

                oBe.rpd_iorden = Convert.ToInt32(txtOrden.Text);
                oBe.rpd_vhora_inicio = txtHoraInicial.Text;
                oBe.rpd_vhora_final = txtHoraFinal.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

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
        }        
    }
}