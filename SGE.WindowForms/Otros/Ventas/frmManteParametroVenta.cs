using System;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteParametroVenta : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteParametroVenta));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;      
        public EParametro oBe = new EParametro();

        public frmManteParametroVenta()
        {            
            InitializeComponent();
        }

        private void FrmVariables_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpAlmacen, new BAlmacen().listarAlmacenes(), "almac_vdescripcion", "almac_icod_almacen", true);
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
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

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;            
            try
            {
                //if (txtIGV.Text=="0.00")
                //{
                //    oBase = txtIGV;
                //    throw new ArgumentException("Ingrese IGV");
                //}
              
               
                //if (txtUIT.Text == "0.00")
                //{
                //    oBase = txtIGV;
                //    throw new ArgumentException("Ingrese UIT");
                //}
                //if (string.IsNullOrEmpty(txtEmpresa.Text))
                //{
                //    oBase = txtEmpresa;
                //    throw new ArgumentException("Ingrese nombre de empresa");
                //}
                //if (string.IsNullOrEmpty(txtDireccion.Text))
                //{
                //    oBase = txtDireccion;
                //    throw new ArgumentException("Ingrese dirección fiscal de la empresa");
                //}
                //if (string.IsNullOrEmpty(txtRuc.Text))
                //{
                //    oBase = txtRuc;
                //    throw new ArgumentException("Ingrese RUC de la empresa");
                //}                                
                
                //oBe.pm_nigv_parametro = Convert.ToDecimal(txtIGV.Text);
                //oBe.pm_nuit_parametro = Convert.ToDecimal(txtUIT.Text);
                //oBe.pm_ncategoria_parametro = Convert.ToDecimal(txt4taCategoria.Text);
                //oBe.pm_nombre_empresa = txtEmpresa.Text;
                //oBe.pm_direccion_empresa = txtDireccion.Text;
                //oBe.pm_vruc = txtRuc.Text;
                oBe.pmv_icod_almacen = Convert.ToInt32(lkpAlmacen.EditValue);
                oBe.intUsuario = Valores.intUsuario;

                new BVentas().modificarParametroVenta(oBe);
                
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {                   
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}