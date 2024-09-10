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
namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteListaPrecio : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteListaPrecio));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        //MyKeyPress myKeyPressHandler = new MyKeyPress();

        public int precc_icod_precio;

        public FrmManteListaPrecio()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();            
        }
                
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        //private BListaPrecioAutoventas Obl;
        
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
            bteDProductoEspecifico.Enabled = !Enabled;
            LkpTipoMoneda.Enabled = !Enabled;            
            txtMontounitario.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteDProductoEspecifico.Enabled = Enabled;
            }
            //bteDProductoEspecifico.Focus();
        }

        private void clearControl()
        {
            bteDProductoEspecifico.Tag = null;
            LkpTipoMoneda.EditValue = null;
            txtMontounitario.Text = "";
        }

        private void cargar()
        {
            //BSControls.LoaderLook(LkpTipoMoneda, new BTipoMoneda().ListarTipoMoneda(), "descripcion", "idc_Tipo_Moneda", false);                        
        }
        
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            LkpTipoMoneda.ItemIndex = 0;
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
            EListaPrecio oBe = new EListaPrecio();
            try
            {
                if (bteDProductoEspecifico.Tag == null)
                {
                    oBase = bteDProductoEspecifico;
                    throw new ArgumentException("Seleccionar un producto");
                }
                if (LkpTipoMoneda.EditValue == null)
                {
                    oBase = LkpTipoMoneda;
                    throw new ArgumentException("Seleccionar un tipo de moneda");
                }
                if (string.IsNullOrEmpty(txtMontounitario.Text))
                {
                    oBase = txtMontounitario;
                    throw new ArgumentException("Ingresar el Monto Unitario");
                }
                
                oBe.prdc_icod_producto = Convert.ToInt32(bteDProductoEspecifico.Tag); 
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(LkpTipoMoneda.EditValue); 
                oBe.lprecc_nmonto_unitario = Convert.ToDecimal(txtMontounitario.Text);
                oBe.lprecc_indicador_rango = false;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BVentas().insertarListaPrecio(oBe);
                }
                else
                {
                    oBe.lprecc_icod_precio = precc_icod_precio;
                    new BVentas().modificarListaPrecio(oBe);
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
                    this.MiEvento(oBe.lprecc_icod_precio);
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
                

        private void txtdescripcion_KeyUp(object sender, KeyEventArgs e)
        {           
            if (e.KeyValue == (char)Keys.Escape)
            {
                cerrar_form(sender, e);
            }
        }
                
        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void FrmManteProductoPrecio_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void bteDProductoEspecifico_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 0)
            //{
            //    using (FrmListarProductoEspecificoBueno frmProducto = new FrmListarProductoEspecificoBueno())
            //    {
            //        frmProducto.IndicadorListaPrecio = 1;
            //        if (frmProducto.ShowDialog() == DialogResult.OK)
            //        {
            //            bteDProductoEspecifico.Text = frmProducto._Be.id_producto_generico + " " + frmProducto._Be.descripcion_producto + " " + frmProducto._Be.descripcion_unidad_medida;
            //            bteDProductoEspecifico.Tag = frmProducto._Be.id_producto_especifico;
            //        }
            //    }
            //}
        }

        

    }
}