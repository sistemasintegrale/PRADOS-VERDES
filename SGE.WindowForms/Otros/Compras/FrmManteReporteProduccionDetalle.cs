using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
//using SGE.BusinessLogic.Compras;
//using SGE.BusinessLogic.Contabilidad;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Tesoreria;
//using SGE.BusinessLogic.Tesoreria;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Otros.Almacen;
using SGE.WindowForms.Otros.Almacen.Listados;
//using SGE.BusinessLogic.Almacen;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteReporteProduccionDetalle : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteReporteProduccionDetalle));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public List<EReporteProduccionDetalle> oDetail = new List<EReporteProduccionDetalle>();
        public EReporteProduccionDetalle oBE = new EReporteProduccionDetalle();
        public BSMaintenanceStatus oState;
        public int intIdReporteProduccion = 0;
        public int intIdReporteProduccionDetalle = 0;
        public int Correlativo = 0;
        public long intIdKardex = 0;
        public DateTime dFecha_documento;

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
        
        #endregion

        #region "Eventos"

        public FrmManteReporteProduccionDetalle()
        {
            InitializeComponent();
        }

        private void FrmManteReporteProduccionDetalle_Load(object sender, EventArgs e)
        {

        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.ListarAlamcen();
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.ListarProductoEspecifico();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            CalcularMonto(Convert.ToDecimal(txtCantidad.EditValue), Convert.ToDecimal(txtPrecioUnitario.EditValue));
        }

        private void txtPrecioUnitario_EditValueChanged(object sender, EventArgs e)
        {
            CalcularMonto(Convert.ToDecimal(txtCantidad.EditValue), Convert.ToDecimal(txtPrecioUnitario.EditValue));
        }


      

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            btnAlmacen.Enabled = !Enabled;
            btnProducto.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            txtPrecioUnitario.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            btnAlmacen.Focus();
        }

        private void clearControl()
        {
            txtItem.EditValue = Correlativo;
            btnAlmacen.Text = "";
            txtUM.Text = "";
            btnProducto.Text = "";
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
            {
                {
                    BaseEdit oBase = null;
                    Boolean Flag = true;
                    try
                    {

                        if (string.IsNullOrEmpty(btnAlmacen.Text))
                        {
                            oBase = btnAlmacen;
                            throw new ArgumentException("Seleccionar un almacen.");
                        }

                        if (string.IsNullOrEmpty(btnProducto.Text))
                        {
                            oBase = btnProducto;
                            throw new ArgumentException("Seleccionar un producto.");
                        }

                        if (Convert.ToDecimal(txtCantidad.Text) == 0)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("La cantidad no puede ser 0.");
                        }

                        if (Convert.ToDecimal(txtPrecioUnitario.Text) == 0)
                        {
                            oBase = txtPrecioUnitario;
                            throw new ArgumentException("El precio unitario no puede ser 0.");
                        }

                        if (Convert.ToDecimal(txtMonto.Text) == 0)
                        {
                            oBase = txtMonto;
                            throw new ArgumentException("El monto no puede ser 0.");
                        }

                        if (Status == BSMaintenanceStatus.CreateNew)
                        {
                            var BuscarDocumento = oDetail.Where(oB => oB.prdc_icod_producto == Convert.ToInt32(btnProducto.Tag)).ToList();
                            if (BuscarDocumento.Count > 0)
                            {
                                oBase = btnAlmacen;
                                throw new ArgumentException("El producto ya existe en la factura de compra");
                            }
                        }

                        decimal dmlCantidad = 0, dmlPrecioUnitario = 0, dmlMonto = 0;

                        decimal.TryParse(txtCantidad.Text, out dmlCantidad);
                        decimal.TryParse(txtPrecioUnitario.Text, out dmlPrecioUnitario);
                        decimal.TryParse(txtMonto.Text, out dmlMonto);

                        oBE.rpd_icod_produccion = intIdReporteProduccionDetalle;
                        oBE.rp_icod_produccion = intIdReporteProduccion;
                        oBE.rpd_item = Convert.ToInt32(txtItem.EditValue);
                        oBE.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                        oBE.almac_vdescripcion = btnAlmacen.Text;
                        oBE.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                        oBE.prdc_vdescripcion_larga = btnProducto.Text;
                        oBE.unidc_vabreviatura_unidad_medida = txtUM.Text;
                        oBE.rpd_ncant_pro = dmlCantidad;
                        oBE.rpd_nmonto_unitario_costo_producto = dmlPrecioUnitario;
                        oBE.rpd_nmonto_total_costo_producto = dmlMonto;
                        oBE.rpd_id_kardex_salida = intIdKardex;
                        oBE.rpd_flag_estado = true;

                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        if (oBase != null)
                        {
                            oBase.Focus();
                            oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                            oBase.ErrorText = ex.Message;
                            oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                            XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Flag = false;
                        }
                    }
                    finally
                    {
                        if (Flag)
                        {
                            if (Status == BSMaintenanceStatus.CreateNew)
                                Status = BSMaintenanceStatus.View;
                            else
                                Status = BSMaintenanceStatus.View;

                            Status = BSMaintenanceStatus.View;
                            this.Close();
                        }
                    }
                }
            }
        }

        private void ListarAlamcen()
        {
            frmListarAlmacen Almacen = new frmListarAlmacen();
            //Almacen.Carga();
            if (Almacen.ShowDialog() == DialogResult.OK)
            {
                btnAlmacen.Tag = Almacen._Be.almac_icod_almacen;
                btnAlmacen.Text = Almacen._Be.almac_vdescripcion;
            }
            btnProducto.Focus();
        }

        private void ListarProductoEspecifico()
        {
            try
            {

                frmListarStockPorAlmacen Producto = new frmListarStockPorAlmacen();
                //Producto.intPeriodo = Parametros.intPeriodo;
                Producto.intAlmacen = Convert.ToInt32(btnAlmacen.Tag);
                Producto.intClasProd = true;
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = Producto._Be.prdc_icod_producto;
                    btnProducto.Text = Producto._Be.strDesProducto.ToString();
                    txtUM.Text = Producto._Be.strCodUM;
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

        private void CalcularMonto(decimal Cantidad, decimal Precio)
        {
            txtMonto.EditValue = Cantidad * Precio;
        }

        #endregion

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        
       
    }
}