using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Security.Principal;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class FrmManteReporteConversionDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteReporteConversionDetalle));

        public List<EReporteConversionDet> lstDetalle = new List<EReporteConversionDet>();
        public EReporteConversionDet oBe = new EReporteConversionDet();
        //public EEmpaquePlantilla obeCab = new EEmpaquePlantilla();
        public int intAlmacen;
        /*-----------------------------------------------------------------------------*/
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        string marca;
        string modelo;


        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public FrmManteReporteConversionDetalle()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngresoDetalle_Load(object sender, EventArgs e)
        {

        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProductoEspecifico();
        }
        private void ListarProductoEspecifico()
        {
            try
            {

                frmListarStockPorAlmacen Producto = new frmListarStockPorAlmacen();
                //Producto.intPeriodo = Parametros.intPeriodo;
                Producto.intAlmacen = intAlmacen;
                Producto.intClasProd = true;
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = Producto._Be.prdc_icod_producto;
                    btnProducto.Text = Producto._Be.strDesProducto.ToString();
                    txtUM.Text = Producto._Be.strCodUM;
                    txtDescripcion.Text = Producto._Be.strDesProducto;
                    //int alco_icod_almacen_contable = Producto._Be.alco_icod_almacen_contable; falta

                    //costo valorizado
                    List<EKardexValorizado> mLista = new List<EKardexValorizado>();
                    /*Falta Almacen Contable*/
                    //mLista = new BAlmacen().ListarKardexValorizadoInventario(alco_icod_almacen_contable, Producto._Be.prdc_icod_producto, dFecha_documento, dFecha_documento, Parametros.intEjercicio);
                    //if (mLista.Count != 0)
                    //{
                    //int cont_max=mLista.Max(ob=>ob.Cont_registro_valorizado);
                    //mLista = mLista.Where(o => o.Cont_registro_valorizado == cont_max).ToList();
                    //txtPrecioUnitario.Text = mLista[0].kardv_precio_costo_promedio.ToString();
                    //}
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            btnProducto.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                btnProducto.Enabled = Enabled;
            btnProducto.Focus();
        }

        public void setValues()
        {
            btnProducto.Tag = oBe.prdc_icod_producto;
            btnProducto.Text = oBe.prdc_vcode_producto;
            txtDescripcion.Text = oBe.prdc_vdescripcion_larga;
            txtUM.Text = oBe.unidc_vabreviatura_unidad_medida;
            txtCantidad.Text = oBe.rcd_dcantidad_conversion.ToString();
            txtitem.Text = oBe.rcd_iitem_conversion.ToString();
            
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
            Boolean flag = true;            
            try
            {
                if (string.IsNullOrEmpty(btnProducto.Text))
                {
                    oBase = btnProducto;
                    throw new ArgumentException("Seleccione producto");
                }
                if (verificarLista())
                {
                    oBase = btnProducto;
                    throw new ArgumentException("El producto seleccionado ya existe el detalle de la Nota de Ingreso");
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese la cantidad ");
                }
                oBe.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBe.prdc_vcode_producto = btnProducto.Text;
                oBe.prdc_vdescripcion_larga = txtDescripcion.Text;
                oBe.unidc_vabreviatura_unidad_medida = txtUM.Text;
                oBe.rcd_dcantidad_conversion = Convert.ToDecimal(txtCantidad.Text);
                oBe.rcd_iitem_conversion = Convert.ToInt32(txtitem.Text);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (oBe.intTipoOperacion == 0)
                        oBe.intTipoOperacion = 2;
                }
                //intro();
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
                flag = false;
            }
            finally
            {
                if (flag)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private bool verificarLista()
        {
            bool flag = false;
            if (Status == BSMaintenanceStatus.CreateNew)
            {                
                if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(btnProducto.Tag)).ToList().Count > 0)
                    flag = true;
            }
            else if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(btnProducto.Tag)).ToList()[0].rcd_iitem_conversion != oBe.rcd_iitem_conversion)
                    flag = true;
            }
            return flag;
        }
        private void intro()
        {
            var lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();
            lstProductos.ForEach(x => {
                EReporteConversionDet oBe_ = new EReporteConversionDet();
                oBe_.prdc_icod_producto = x.prdc_icod_producto;
                oBe_.prdc_vcode_producto = x.prdc_vcode_producto;
                oBe_.prdc_vdescripcion_larga = x.prdc_vdescripcion_larga;
                oBe_.unidc_vabreviatura_unidad_medida = x.StrUnidadMedida;
                oBe_.rcd_dcantidad_conversion = 1;
                oBe_.intUsuario = Valores.intUsuario;
                oBe_.strPc = WindowsIdentity.GetCurrent().Name;
                oBe_.rcd_iitem_conversion = lstDetalle.Count + 1;
                oBe_.intTipoOperacion = 1;
                lstDetalle.Add(oBe_);
            });
        }
       
        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                frm.flag_solo_prods = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = frm._Be.prdc_icod_producto;
                    btnProducto.Text = frm._Be.prdc_vcode_producto;
                    txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUM.Text = frm._Be.StrUnidadMedida;
                    //marca = frm._Be.strMarca;
                    //modelo = frm._Be.strModelo;
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }            
    }
}