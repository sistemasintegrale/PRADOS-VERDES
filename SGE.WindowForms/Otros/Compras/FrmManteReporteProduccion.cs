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
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Tesoreria;
using SGE.WindowForms.Otros.Almacen.Listados;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteReporteProduccion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteReporteProduccion));
        List<EReporteProduccionDetalle> mListaDetalle = new List<EReporteProduccionDetalle>();
        List<EReporteProduccionDetalle> mListaDetalleEliminados = new List<EReporteProduccionDetalle>();
        List<ECostoReporteProduccion> mListaCostosProduccion = new List<ECostoReporteProduccion>();
        List<ECostoReporteProduccion> mListaCostosProduccionEliminados = new List<ECostoReporteProduccion>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        List<ECuentaContable> mListaCuenta = new List<ECuentaContable>();
        //private List<ENumeracionDocumento> mListaNumero = new List<ENumeracionDocumento>();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public int anio = 0;
        private int xposition = 0;
        public List<EDocPorPagar> oDetail;
        public BSMaintenanceStatus oState;
        private BCompras Obl;
        public int IdReporteProduccion = 0;
        public long IdKardex;
        public DateTime? FechaKardex;

        private BAdministracionSistema Otc;
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

        public enum Operacion
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3,
            Consultar = 4
        }

        #endregion

        #region "Eventos"

        public FrmManteReporteProduccion()
        {
            InitializeComponent();

        }

        private void FrmManteReporteProduccion_Load(object sender, EventArgs e)
        {
            CargaControles();
            Carga();
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProductoEspecifico();
        }

        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAlamcen();
        }

        private void btnSubProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                frmListarProducto Producto = new frmListarProducto();
                //Producto.Carga(Parametros.intSitProductoEspecificoNacionalizado);
                Producto.flag_sub_producto = true;
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btnSubProducto.Text = Producto._Be.prdc_vdescripcion_larga;
                    btnSubProducto.Tag = Producto._Be.prdc_icod_producto;
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void viewReporteProduccionDetalle_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (mListaDetalle.Count > 0)
                {
                    EReporteProduccionDetalle Obe = (EReporteProduccionDetalle)gvProd.GetRow(gvProd.FocusedRowHandle);
                    using (FrmManteReporteProduccionDetalle frm = new FrmManteReporteProduccionDetalle())
                    {
                        frm.MiEvento += new FrmManteReporteProduccionDetalle.DelegadoMensaje(form2_MiEvento);
                        frm.ShowDialog();
                        frm.SetCancel();
                        frm.txtItem.EditValue = Obe.rpd_item;
                        frm.btnAlmacen.Tag = Obe.almac_icod_almacen;
                        frm.btnAlmacen.Text = Obe.almac_vdescripcion;
                        frm.btnProducto.Tag = Obe.prdc_icod_producto;
                        frm.btnProducto.Text = Obe.prdc_vdescripcion_larga;
                        frm.txtCantidad.EditValue = Obe.rpd_ncant_pro;
                        frm.txtPrecioUnitario.EditValue = Obe.rpd_nmonto_unitario_costo_producto;
                        frm.txtMonto.EditValue = Obe.rpd_nmonto_total_costo_producto;
                        frm.btnGuardar.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevodetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmManteReporteProduccionDetalle frm = new FrmManteReporteProduccionDetalle())
                {
                    frm.dFecha_documento = Convert.ToDateTime(dtmFecha.EditValue);
                    frm.Correlativo = (mListaDetalle.Count == 0) ? 1 : mListaDetalle.Max(obe => obe.rpd_item) + 1;
                    frm.SetInsert();
                    gvProd.MoveLast();
                    frm.oDetail = mListaDetalle;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.oBE != null)
                        {
                            frm.oBE.TipOper = Convert.ToInt32(Operacion.Nuevo);
                            mListaDetalle.Add(frm.oBE);
                            gvProd.RefreshData();
                            this.MontosTotales();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificardetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mListaDetalle.Count > 0)
                {
                    using (FrmManteReporteProduccionDetalle frm = new FrmManteReporteProduccionDetalle())
                    {
                        EReporteProduccionDetalle Obe = (EReporteProduccionDetalle)gvProd.GetRow(gvProd.FocusedRowHandle);
                        frm.oBE = Obe;
                        frm.btnGuardar.Caption = "Modificar";
                        //frm.btnGuardar.Glyph = SGI.WindowsForm.Properties.Resources.doc_min_modificar;
                        frm.intIdReporteProduccion = Obe.rp_icod_produccion;
                        frm.intIdReporteProduccionDetalle = Obe.rpd_icod_produccion;
                        frm.intIdKardex = Obe.rpd_id_kardex_salida;
                        frm.SetModify();
                        frm.MiEvento += new FrmManteReporteProduccionDetalle.DelegadoMensaje(Modify);
                        frm.txtItem.EditValue = Obe.rpd_item;
                        frm.btnAlmacen.Tag = Obe.almac_icod_almacen;
                        frm.btnAlmacen.Text = Obe.almac_vdescripcion;
                        frm.btnProducto.Tag = Obe.prdc_icod_producto;
                        frm.btnProducto.Text = Obe.prdc_vdescripcion_larga;
                        frm.txtUM.Text = Obe.unidc_vabreviatura_unidad_medida;
                        frm.txtCantidad.EditValue = Obe.rpd_ncant_pro;
                        frm.txtPrecioUnitario.EditValue = Obe.rpd_nmonto_unitario_costo_producto;
                        frm.txtMonto.EditValue = Obe.rpd_nmonto_total_costo_producto;

                        frm.btnGuardar.Enabled = true;
                        frm.btnAlmacen.Enabled = false;
                        frm.btnProducto.Enabled = false;
                        frm.oDetail = mListaDetalle;
                        frm.txtCantidad.Focus();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            xposition = gvProd.FocusedRowHandle;
                            if (Obe.TipOper == Convert.ToInt32(Operacion.Consultar)) Obe.TipOper = Convert.ToInt32(Operacion.Modificar);
                            gvProd.RefreshData();
                            this.MontosTotales();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminardetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (mListaDetalle.Count > 0)
                {
                    if (XtraMessageBox.Show("Está de seguro de eliminar el detalle del reporte de producción?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EReporteProduccionDetalle Obe = (EReporteProduccionDetalle)gvProd.GetRow(gvProd.FocusedRowHandle);
                        Obe.rpd_iusuario_modifica = Valores.intUsuario;
                        Obe.rpd_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                        Obe.TipOper = Convert.ToInt32(Operacion.Eliminar);
                        mListaDetalleEliminados.Add(Obe);
                        gvProd.DeleteRow(gvProd.FocusedRowHandle);
                        gvProd.RefreshData();
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewCostos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (mListaCostosProduccion.Count > 0)
                {
                    using (FrmManteCostosReporteProduccion movDetalle = new FrmManteCostosReporteProduccion())
                    {
                        ECostoReporteProduccion Obe = (ECostoReporteProduccion)gvCostos.GetRow(gvCostos.FocusedRowHandle);
                        movDetalle.MiEvento += new FrmManteCostosReporteProduccion.DelegadoMensaje(form2_MiEvento);
                        movDetalle.ShowDialog();
                        movDetalle.SetCancel();
                        movDetalle.btnProveedor.Tag = Obe.proc_icod_proveedor;
                        movDetalle.btnProveedor.Text = Obe.proc_vcod_proveedor;
                        movDetalle.txtProveedor.Text = Obe.proc_vnombrecompleto;
                        movDetalle.txtDocumento.Text = Obe.tdocc_vabreviatura_tipo_doc;
                        movDetalle.deFechaDoc.EditValue = Obe.doxpc_sfecha_doc;
                        movDetalle.txtNumero.Text = Obe.doxpc_vnumero_doc;
                        movDetalle.lkpTipoCosto.EditValue = Obe.crp_tipo_costo;
                        movDetalle.lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
                        movDetalle.txtMonto.EditValue = Obe.crp_nmonto_pago;
                        movDetalle.btnGuardar.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevocostoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmManteCostosReporteProduccion frm = new FrmManteCostosReporteProduccion())
                {
                    frm.SetInsert();
                    gvCostos.MoveLast();
                    frm.oDetail = mListaCostosProduccion;
                    frm.icod_moneda = 3;//soles
                    frm.txtTipoDeCambio.Text = txtTipoDeCambio.Text;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.oBE != null)
                        {
                            frm.oBE.TipOper = Convert.ToInt32(Operacion.Nuevo);
                            mListaCostosProduccion.Add(frm.oBE);
                            gvCostos.RefreshData();
                            CalcularCostos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalcularCostos()
        {
            txtTransporte.Text = mListaCostosProduccion.Where(ob => ob.crp_tipo_costo == 654 && ob.TipOper != Convert.ToInt32(Operacion.Eliminar)).Sum(ob => ob.crp_nmonto_pago).ToString();
            txtAlmacenaje.Text = mListaCostosProduccion.Where(ob => ob.crp_tipo_costo == 655 && ob.TipOper != Convert.ToInt32(Operacion.Eliminar)).Sum(ob => ob.crp_nmonto_pago).ToString();
            txtMaquila.Text = mListaCostosProduccion.Where(ob => ob.crp_tipo_costo == 657 && ob.TipOper != Convert.ToInt32(Operacion.Eliminar)).Sum(ob => ob.crp_nmonto_pago).ToString();
            txtProceso.Text = mListaCostosProduccion.Where(ob => ob.crp_tipo_costo == 656 && ob.TipOper != Convert.ToInt32(Operacion.Eliminar)).Sum(ob => ob.crp_nmonto_pago).ToString();
        }

        private void modificarcostoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mListaCostosProduccion.Count > 0)
                {
                    using (FrmManteCostosReporteProduccion frm = new FrmManteCostosReporteProduccion())
                    {
                        ECostoReporteProduccion Obe = (ECostoReporteProduccion)gvCostos.GetRow(gvCostos.FocusedRowHandle);
                        frm.oBE = Obe;
                        frm.btnGuardar.Caption = "Modificar";
                        frm.btnGuardar.Glyph = SGE.WindowForms.Properties.Resources.doc_min_modificar;
                        frm.intIdReporteProduccion = Obe.rp_icod_produccion;
                        frm.intIdCostoReporteProduccion = Obe.crp_icod_costo;
                        frm.intIdDocumentoPorPagar = Obe.doxpc_icod_correlativo;
                        frm.SetModify();
                        frm.MiEvento += new FrmManteCostosReporteProduccion.DelegadoMensaje(Modify);
                        frm.btnProveedor.Tag = Obe.proc_icod_proveedor;
                        frm.btnProveedor.Text = Obe.proc_vcod_proveedor;
                        frm.txtProveedor.Text = Obe.proc_vnombrecompleto;
                        frm.txtDocumento.Text = Obe.tdocc_vabreviatura_tipo_doc;
                        frm.deFechaDoc.EditValue = Obe.doxpc_sfecha_doc;
                        frm.txtNumero.Text = Obe.doxpc_vnumero_doc;
                        frm.lkpTipoCosto.EditValue = Obe.crp_tipo_costo;
                        frm.lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
                        frm.txtMonto.EditValue = Obe.crp_nmonto_pago;
                        frm.oDetail = mListaCostosProduccion;
                        frm.txtTipoDeCambio.Text = txtTipoDeCambio.Text;
                        frm.btnProveedor.Enabled = false;
                        frm.lkpTipoCosto.Enabled = false;
                        frm.lkpMoneda.Enabled = false;
                        frm.txtTipoDeCambio.Enabled = false;
                        frm.btnDocumentoPorPagar.Enabled = false;
                        frm.txtMonto.Focus();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            xposition = gvCostos.FocusedRowHandle;
                            if (Obe.TipOper == Convert.ToInt32(Operacion.Consultar)) Obe.TipOper = Convert.ToInt32(Operacion.Modificar);
                            gvCostos.RefreshData();
                            CalcularCostos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarcostoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (mListaCostosProduccion.Count > 0)
                {
                    if (XtraMessageBox.Show("Está de seguro de eliminar el costo del reporte de producción?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ECostoReporteProduccion Obe = (ECostoReporteProduccion)gvCostos.GetRow(gvCostos.FocusedRowHandle);
                        Obe.crp_iid_usuario_modifica = Valores.intUsuario;
                        Obe.crp_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                        Obe.TipOper = Convert.ToInt32(Operacion.Eliminar);
                        mListaCostosProduccionEliminados.Add(Obe);
                        gvCostos.DeleteRow(gvCostos.FocusedRowHandle);
                        gvCostos.RefreshData();
                        CalcularCostos();
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            lkpTipo.Enabled = !Enabled;
            dtmFecha.Enabled = !Enabled;
            txtTipoDeCambio.Enabled = !Enabled;
            btnProducto.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            btnProveedor.Enabled = !Enabled;
            btnAlmacen.Enabled = !Enabled;
            txtObservaciones.Enabled = !Enabled;
            txtMontoSoles.Enabled = !Enabled;
            txtMontoDolares.Enabled = !Enabled;
            txtAlmacenaje.Enabled = !Enabled;
            txtMaquila.Enabled = !Enabled;
            txtProceso.Enabled = !Enabled;
            txtTransporte.Enabled = !Enabled;
            btnSubProducto.Enabled = !Enabled;
            txtCantidadSubProd.Enabled = !Enabled;
            txtCosto.Enabled = !Enabled;
            lkpTipo.Focus();
        }

        private void clearControl()
        {
            dtmFecha.EditValue = DateTime.Today;
            Otc = new BAdministracionSistema();
            Otc.listarTipoCambio().Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFecha.EditValue).ToShortDateString()).ToList().ForEach(obe =>
            {
                txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
            txtCantidad.Text= "0.00";
            btnAlmacen.Text = "";
            txtObservaciones.Text = "";
            txtMontoSoles.Text = "0.00";
            txtMontoDolares.Text = "0.00";
            txtAlmacenaje.Text = "0.00";
            txtMaquila.Text = "0.00";
            txtProceso.Text = "0.00";
            txtTransporte.Text = "0.00";
            btnSubProducto.Text = "";
            txtCantidadSubProd.Text = "0.00";
            txtCosto.Text= "0.00";
        }

        public void CargaControles()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(91), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            //BSControls.LoaderLook(lkpTipo, new BTablaRegistro().ListarTablaRegistro(Parametros.intTablaReporteProduccion), "Descripcion", "IdTablaRegistro", true); Falta Arreglar
            //ListaTipoCambio = new BLibroBancos().ListarTipoCambioEntidadFinanciera();

            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
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
        private bool VerificarEjercicio(DateTime sfecha)
        {
            bool Rpt;
            if (Parametros.intEjercicio == sfecha.Year)
            {
                Rpt = true;
            }
            else
            {
                Rpt = false;
            }
            return Rpt;
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EReporteProduccion oBe = new EReporteProduccion();
            Obl = new BCompras();
            try
            {
                if (string.IsNullOrEmpty(lkpTipo.Text))
                {
                    oBase = btnProveedor;
                    throw new ArgumentException("Seleccionar el tipo de presupuesto");
                }

                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    oBase = txtTipoDeCambio;
                    throw new ArgumentException("Ingresar el Tipo de Cambio");
                }

                if (string.IsNullOrEmpty(btnProducto.Text))
                {
                    oBase = btnProducto;
                    throw new ArgumentException("Ingresar el Producto");
                }

                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingresar el Cantidad");
                }

                if (string.IsNullOrEmpty(btnProveedor.Text))
                {
                    oBase = btnProveedor;
                    throw new ArgumentException("Ingresar el Proveedor");
                }

                if (string.IsNullOrEmpty(btnAlmacen.Text))
                {
                    oBase = btnAlmacen;
                    throw new ArgumentException("Ingresar el Almacen");
                }

                if (Convert.ToDecimal(txtMontoSoles.Text) == 0)
                {
                    oBase = txtMontoSoles;
                    throw new ArgumentException("Ingresar el monto soles");
                }

                if (Convert.ToDecimal(txtMontoDolares.Text) == 0)
                {
                    oBase = txtMontoDolares;
                    throw new ArgumentException("Ingresar el monto dolares");
                }
                if (VerificarEjercicio(Convert.ToDateTime(dtmFecha.EditValue)) == false)
                {
                    oBase = dtmFecha;
                    throw new ArgumentException("La fecha seleccionada no esta dentro del ejercicio" + Parametros.intEjercicio);
                }
                oBe.rp_icod_produccion = IdReporteProduccion;
                oBe.rp_iid_anio = Parametros.intEjercicio;
                oBe.rp_num_produccion = txtNumeroRP.Text;
                oBe.tablc_iid_tipo_rp = Convert.ToInt32(lkpTipo.EditValue);
                oBe.rp_sfecha_produccion = dtmFecha.DateTime;
                oBe.rp_ntipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                oBe.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBe.rp_ncant_pro = Convert.ToDecimal(txtCantidad.Text);
                oBe.proc_icod_proveedor =  Convert.ToInt32(btnProveedor.Tag);
                oBe.rp_voservaciones_produccion = txtObservaciones.Text;
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                oBe.rp_nmonto_total_soles =  Convert.ToDecimal(txtMontoSoles.Text);
                oBe.rp_nmonto_total_dolares =  Convert.ToDecimal(txtMontoDolares.Text);
                oBe.rp_nmonto_total_costo_almacenaje = Convert.ToDecimal(txtAlmacenaje.Text);
                oBe.rp_nmonto_total_costo_maquila = Convert.ToDecimal(txtMaquila.Text);
                oBe.rp_nmonto_total_costo_proceso = Convert.ToDecimal(txtProceso.Text);
                oBe.rp_nmonto_total_costo_transporte = Convert.ToDecimal(txtTransporte.Text);
                oBe.prdc_icod_sub_producto = Convert.ToInt32(btnSubProducto.Tag);
                oBe.rp_ncant_subpro = Convert.ToDecimal(txtCantidadSubProd.Text);
                oBe.rp_nmonto_costo_subprod = Convert.ToDecimal(txtCosto.Text);
                oBe.rp_id_kardex_producto_ingreso = IdKardex;
                oBe.rp_sfecha_ing_kardex = FechaKardex;
                oBe.rp_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                oBe.rp_iid_usuario_crea = Valores.intUsuario;
                oBe.rp_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                //oBe.rp_iid_usuario_modifica = Valores.int;
                //oBe.rp_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.rp_flag_estado = true;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //Traer el correlativo de Numeracion Documento
                    //string sNumero = "";
                    //mListaNumero = new BNumeracionDocumento().ListarObtenerNumero(Parametros.intPeriodo, Parametros.intTipoDocReporteProduccion);
                    //if (mListaNumero.Count > 0)
                    //{
                    //    sNumero = Utilitarios.AgregarCaracter((mListaNumero[0].numero + 1).ToString(), "0", 4);
                    //}
                    //string sPeriodo = Utilitarios.AgregarCaracter(Utilitarios.Right(Parametros.intPeriodo.ToString(), 2), "0", 3);
                    //oBe.rp_num_produccion = sPeriodo + "-" + sNumero;
                    Obl.InsertarReporteProduccion(oBe, mListaDetalle, mListaCostosProduccion);
                }
                else
                {
                    Obl.ActualizarReporteProduccion(oBe, mListaDetalle, mListaCostosProduccion, mListaDetalleEliminados, mListaCostosProduccionEliminados);
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
                Flag = false;
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Text = Proveedor._Be.vcod_proveedor;
                txtProveedor.Text = Proveedor._Be.vnombrecompleto;
            }
            btnAlmacen.Focus();
        }

        private void ListarAlamcen()
        {
            frmListarAlmacen Almacen = new frmListarAlmacen();
            //Almacen.cargar();
            if (Almacen.ShowDialog() == DialogResult.OK)
            {
                btnAlmacen.Tag = Almacen._Be.almac_icod_almacen;
                btnAlmacen.Text = Almacen._Be.almac_vdescripcion;
            }
            txtObservaciones.Focus();
        }

        private void ListarProductoEspecifico()
        {
            try
            {
                frmListarProducto Producto = new frmListarProducto();
                //Producto.Carga(Parametros.intSitProductoEspecificoNacionalizado);
                Producto.flag_producto_terminado = true;
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = Producto._Be.prdc_icod_producto;
                    btnProducto.Text = Producto._Be.prdc_vdescripcion_larga;
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            gvProd.FocusedRowHandle = xposition;
            gvCostos.FocusedRowHandle = xposition;
        }

        void AddEvent()
        {
            this.gvProd.DoubleClick += new System.EventHandler(this.viewReporteProduccionDetalle_DoubleClick);
            this.gvCostos.DoubleClick += new System.EventHandler(this.viewCostos_DoubleClick);
        }

        private void Carga()
        {
            // Detalle Producción
            mListaDetalle = new BCompras().ListarReporteProduccionDetalle(IdReporteProduccion);
            grdProd.DataSource = mListaDetalle;

            //Costos de Produccion
            mListaCostosProduccion = new BCompras().ListarCostoReporteProduccion(IdReporteProduccion);
            grdCostos.DataSource = mListaCostosProduccion;
        }

        #endregion

        public class ReporteProduccionDetalleBE
        {
            public int rpd_icod_produccion { get; set; }
            public int rp_icod_produccion { get; set; }
            public int rpd_item { get; set; }
            public int almac_icod_almacen { get; set; }
            public string almac_vdescripcion { get; set; }
            public int pespc_icod_producto_especifico { get; set; }
            public string prodc_vdescripcion { get; set; }
            public string unidc_vabreviatura_unidad_medida { get; set; }
            public decimal rpd_ncant_pro_especifico { get; set; }
            public decimal rpd_nmonto_unitario_costo_producto { get; set; }
            public decimal rpd_nmonto_total_costo_producto { get; set; }
            public long rpd_id_kardex_salida { get; set; }
            public bool rpd_flag_estado { get; set; }
            public int TipOper { get; set; } 

            public ReporteProduccionDetalleBE()
            {

            }
        }

        public class CostoReporteProduccionBE
        {
            public int crp_icod_costo { get; set; }
            public int rp_icod_produccion { get; set; }
            public int proc_icod_proveedor { get; set; }
            public string proc_vcod_proveedor { get; set; }
            public string proc_vnombrecompleto { get; set; }
            public long doxpc_icod_correlativo { get; set; }
            public string tdocc_vabreviatura_tipo_doc { get; set; }
            public string doxpc_vnumero_doc { get; set; }
            public DateTime doxpc_sfecha_doc { get; set; }
            public int crp_tipo_costo { get; set; }
            public string TipoCosto { get; set; }
            public int tablc_iid_tipo_moneda { get; set; }
            public string Moneda { get; set; }
            public decimal crp_nmonto_tipo_cambio { get; set; }
            public decimal crp_nmonto_pago { get; set; }
            public bool crp_flag_estado { get; set; }
            public int TipOper { get; set; }

            public CostoReporteProduccionBE()
            {

            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnProducto_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarProductoEspecifico();
        }

        private void btnProveedor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarProveedor();
        }

        private void btnAlmacen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarAlamcen();
        }

        private void btnSubProducto_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarProductoEspecifico();
        }
   
        public void MontosTotales()
        {
            decimal Monto;
            Monto = mListaDetalle.Sum(ob => ob.rpd_nmonto_total_costo_producto);
            txtMontoSoles.Text = Monto.ToString("n2");
            //txtMontoDolares.Text = Math.Round((Monto / Convert.ToDecimal(txtTipoDeCambio.Text)),2).ToString();
            txtMontoDolares.Text = (Monto / Convert.ToDecimal(txtTipoDeCambio.Text)).ToString("n2");
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            this.MontosTotales();
        }

        private void txtTipoDeCambio_EditValueChanged(object sender, EventArgs e)
        {
            this.MontosTotales();
        }

        private void txtMontoSoles_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            new BAdministracionSistema().listarTipoCambio().Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFecha.EditValue).ToShortDateString()).ToList().ForEach(obe =>
            {
                txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
        }

        
        

        

       
        

        
        

        

    }
}