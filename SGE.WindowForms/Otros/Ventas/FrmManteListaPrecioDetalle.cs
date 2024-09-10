using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.BusinessLogic;
using System.Security.Principal;
using SGE.WindowForms.Modules;
using System.Linq;
namespace SGI.WindowsForm.Otros.Ventas
{
    public partial class FrmManteListaPrecioDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteListaPrecioDetalle));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        //MyKeyPress myKeyPressHandler = new MyKeyPress();

        public int lprecc_icod_precio = 0;

        public FrmManteListaPrecioDetalle()
        {
            InitializeComponent();            
        }
                
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        
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
            txtCantidadInicial.Enabled = !Enabled;
            txtCantidadFinal.Enabled = !Enabled;            
            txtMontounitario.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //bteDProductoEspecifico.Enabled = Enabled;
            }
            
        }

        private void clearControl()
        {
            txtCantidadInicial.Text = "";
            txtCantidadFinal.Text = "";
            txtMontounitario.Text = "";
        }

        private void cargar()
        {
                        
        }
        
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;            
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EListaPrecioDetalle oBe = new EListaPrecioDetalle();
            try
            {
              
                if (txtCantidadInicial.Text == "")
                {
                    oBase = txtCantidadInicial;
                    throw new ArgumentException("Ingresar Cantidad Inicial");
                }
                if (txtCantidadFinal.Text == "")
                {
                    oBase = txtCantidadFinal;
                    throw new ArgumentException("Ingresar Cantidad Final");
                }
                if (txtMontounitario.Text == "")
                {
                    oBase = txtMontounitario;
                    throw new ArgumentException("Ingresar el Monto Unitario");
                }
                oBe.lprecc_icod_precio = lprecc_icod_precio;
                oBe.lprecd_icantidad_inicial = Convert.ToInt32(txtCantidadInicial.Text);
                oBe.lprecd_icantidad_final = Convert.ToInt32(txtCantidadFinal.Text);
                oBe.lprecd_nmonto_unitario = Convert.ToDecimal(txtMontounitario.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BVentas().insertarListaPrecioDetalle(oBe);
                }
                //else
                //{
                //    oBe.lprecc_icod_precio = precc_icod_precio;
                //    Obl.ActualizarListaPrecioAutoventasDet(oBe);
                //}
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    //oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else                    
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    //if (Status == BSMaintenanceStatus.CreateNew)
                    //  Status = BSMaintenanceStatus.View;
                    // else
                    //     Status = BSMaintenanceStatus.View;
                      
                    //Status = BSMaintenanceStatus.View;
                    this.MiEvento(oBe.lprecd_icod_precio_detalle);
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


                

     

    }
}