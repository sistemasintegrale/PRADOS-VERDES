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
using SGE.WindowForms.Otros.Compras;
//using SGE.WindowForms.Otros.Exportacion;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class FrmReporteProduccion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"



        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteProduccion));
        private List<EReporteProduccion> mLista = new List<EReporteProduccion>();

        private int xposition = 0;

        
        #endregion

        #region "Eventos"

        public FrmReporteProduccion()
        {
            InitializeComponent();
        }

        private void FrmReporteProduccion_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewReporteProduccion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (mLista.Count > 0)
                {
                    FrmManteReporteProduccion Produccion = new FrmManteReporteProduccion();
                    Produccion.MiEvento += new FrmManteReporteProduccion.DelegadoMensaje(AddEvent);
                    EReporteProduccion Obe = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
                    Produccion.IdReporteProduccion = Convert.ToInt32(Obe.rp_icod_produccion);
                    Produccion.IdKardex = Convert.ToInt64(Obe.rp_id_kardex_producto_ingreso);
                    Produccion.SetCancel();
                    Produccion.Show();
                    Produccion.txtMontoSoles.EditValue = Obe.rp_nmonto_total_soles;
                    Produccion.txtMontoDolares.EditValue = Obe.rp_nmonto_total_dolares;
                    Produccion.txtNumeroRP.Text = Obe.rp_num_produccion;
                    Produccion.lkpTipo.EditValue = Obe.tablc_iid_tipo_rp;
                    Produccion.dtmFecha.EditValue = Obe.rp_sfecha_produccion;
                    Produccion.txtTipoDeCambio.EditValue = Obe.rp_ntipo_cambio;
                    Produccion.btnProducto.Tag = Obe.prdc_icod_producto;
                    Produccion.btnProducto.Text = Obe.prdc_vdescripcion_larga;
                    Produccion.txtCantidad.EditValue = Obe.rp_ncant_pro;
                    Produccion.btnProveedor.Tag = Obe.proc_icod_proveedor;
                    Produccion.btnProveedor.Text = Obe.proc_vcod_proveedor;
                    Produccion.txtProveedor.Text = Obe.proc_vnombrecompleto;
                    Produccion.btnAlmacen.Tag = Obe.almac_icod_almacen;
                    Produccion.btnAlmacen.Text = Obe.almac_vdescripcion;
                    Produccion.txtObservaciones.Text = Obe.rp_voservaciones_produccion;
                    //Produccion.txtMontoSoles.EditValue = Obe.rp_nmonto_total_soles;
                    //Produccion.txtMontoDolares.EditValue = Obe.rp_nmonto_total_dolares;
                    Produccion.txtAlmacenaje.EditValue = Obe.rp_nmonto_total_costo_almacenaje;
                    Produccion.txtMaquila.EditValue = Obe.rp_nmonto_total_costo_maquila;
                    Produccion.txtProceso.EditValue = Obe.rp_nmonto_total_costo_proceso;
                    Produccion.txtTransporte.EditValue = Obe.rp_nmonto_total_costo_transporte;
                    Produccion.btnSubProducto.Tag = Obe.prdc_icod_sub_producto;
                    Produccion.btnSubProducto.Text = Obe.SubProductoEspecifico;
                    Produccion.lkpTipo.Enabled = false;
                    Produccion.btnProveedor.Enabled = false;
                    Produccion.btnProducto.Enabled = false;
                    Produccion.btnAlmacen.Enabled = false;
                    Produccion.btnGuardar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmManteReporteProduccion Produccion = new FrmManteReporteProduccion();
                Produccion.MiEvento += new FrmManteReporteProduccion.DelegadoMensaje(form2_MiEvento);
                viewReporteProduccion.MoveLast();
                Produccion.Show();
                Produccion.SetInsert();

                if (mLista.Count == 0)
                {
                    Produccion.txtNumeroRP.Text = Parametros.intEjercicio.ToString() + "-0001";
                }
                else
                {
                    Produccion.txtNumeroRP.Text = Parametros.intEjercicio.ToString() + "-" + String.Format("{0:0000}", new BCompras().NumeroCorrelativoReporteProduccion(Parametros.intEjercicio));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mLista.Count > 0)
                {
                    EReporteProduccion Obe = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
                    if (Obe.rp_id_kardex_producto_ingreso == 0)
                    {
                        FrmManteReporteProduccion Produccion = new FrmManteReporteProduccion();
                        Produccion.MiEvento += new FrmManteReporteProduccion.DelegadoMensaje(Modify);
                        Produccion.IdReporteProduccion = Convert.ToInt32(Obe.rp_icod_produccion);
                        Produccion.IdKardex = Convert.ToInt64(Obe.rp_id_kardex_producto_ingreso);
                        Produccion.Show();
                        Produccion.SetModify();
                        xposition = viewReporteProduccion.FocusedRowHandle;
                        Produccion.txtMontoSoles.EditValue = Obe.rp_nmonto_total_soles;
                        Produccion.txtMontoDolares.EditValue = Obe.rp_nmonto_total_dolares;
                        Produccion.txtNumeroRP.Text = Obe.rp_num_produccion;
                        Produccion.lkpTipo.EditValue = Obe.tablc_iid_tipo_rp;
                        Produccion.dtmFecha.EditValue = Obe.rp_sfecha_produccion;
                        Produccion.txtTipoDeCambio.EditValue = Obe.rp_ntipo_cambio;
                        Produccion.btnProducto.Tag = Obe.prdc_icod_producto;
                        Produccion.btnProducto.Text = Obe.prdc_vdescripcion_larga;
                        Produccion.txtCantidad.EditValue = Obe.rp_ncant_pro;
                        Produccion.btnProveedor.Tag = Obe.proc_icod_proveedor;
                        Produccion.btnProveedor.Text = Obe.proc_vcod_proveedor;
                        Produccion.txtProveedor.Text = Obe.proc_vnombrecompleto;
                        Produccion.btnAlmacen.Tag = Obe.almac_icod_almacen;
                        Produccion.btnAlmacen.Text = Obe.almac_vdescripcion;
                        Produccion.txtObservaciones.Text = Obe.rp_voservaciones_produccion;
                        //Produccion.txtMontoSoles.EditValue = Obe.rp_nmonto_total_soles;
                        //Produccion.txtMontoDolares.EditValue = Obe.rp_nmonto_total_dolares;
                        Produccion.txtAlmacenaje.EditValue = Obe.rp_nmonto_total_costo_almacenaje;
                        Produccion.txtMaquila.EditValue = Obe.rp_nmonto_total_costo_maquila;
                        Produccion.txtProceso.EditValue = Obe.rp_nmonto_total_costo_proceso;
                        Produccion.txtTransporte.EditValue = Obe.rp_nmonto_total_costo_transporte;
                        Produccion.btnSubProducto.Tag = Obe.prdc_icod_sub_producto;
                        Produccion.btnSubProducto.Text = Obe.SubProductoEspecifico;
                        Produccion.lkpTipo.Enabled = true;
                        Produccion.btnProveedor.Enabled = true;
                        Produccion.btnProducto.Enabled = true;
                        Produccion.btnAlmacen.Enabled = true;
                        Produccion.btnGuardar.Enabled = true;
                        //Produccion.MontosTotales();
                    }
                    else
                    {
                        XtraMessageBox.Show("No se puede modificar, ya se realizó el ingreso al kardex", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mLista.Count > 0)
                {
                    int IdSituacion = int.Parse(viewReporteProduccion.GetFocusedRowCellValue("rp_iid_situacion").ToString());
                    if (IdSituacion == Parametros.intSitReporteProduccionGenerado)
                    {
                        int IdReporteProduccion = int.Parse(viewReporteProduccion.GetFocusedRowCellValue("rp_icod_produccion").ToString());

                        if (XtraMessageBox.Show("Desea eliminar el reporte de producción \n Con sus respectivos movimientos", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BCompras oBl = new BCompras();
                            EReporteProduccion objE_ReporteProduccion = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
                            oBl.EliminarReporteProduccion(objE_ReporteProduccion);
                            Carga();
                        }
                    }
                    else
                    {       
                        XtraMessageBox.Show("Eliminar Ingreso de Producto Terminado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void anulartoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mLista.Count > 0)
                {
                    int IdReporteProduccion = int.Parse(viewReporteProduccion.GetFocusedRowCellValue("rp_icod_produccion").ToString());
                    int IdSituacion = int.Parse(viewReporteProduccion.GetFocusedRowCellValue("rp_iid_situacion").ToString());
                    EReporteProduccion Obe = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
                    if (XtraMessageBox.Show("Desea anular el reporte de producción \n Con sus respectivos movimientos", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        /*Falta Completar*/
                        //if (IdSituacion == Parametros.intSitReporteProduccionGenerado)
                        //{
                            //if (Convertir.VerificarFechaBloqueo(1, Convert.ToDateTime(Obe.rp_sfecha_produccion)) == false)
                            //{
                            //    XtraMessageBox.Show("No puede eliminar este registro a consecuencia de la fecha de bloqueo, coordinar con el Área de Contabilidad", "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}

                            BCompras oBl = new BCompras();
                            EReporteProduccion objE_ReporteProduccion = new EReporteProduccion();
                            objE_ReporteProduccion.rp_icod_produccion = IdReporteProduccion;
                            oBl.AnularReporteProduccion(objE_ReporteProduccion);
                        //}
                        //else
                        //{
                        //    string sSituacion = "";
                        //    if (IdSituacion == Parametros.intSitReporteProduccionActualizado) { sSituacion = "Actualizado"; }
                        //    XtraMessageBox.Show("No se puede anular el reporte de produccion \n" + "Situación : " + sSituacion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Por Completar*/
            EReporteProduccion Obe = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
            List<EReporteProduccionDetalle> mListaDetalle = new BCompras().ListarReporteProduccionDetalle(Obe.rp_icod_produccion);
            List<ECostoReporteProduccion> mListaCostosProduccion = new BCompras().ListarCostoReporteProduccion(Obe.rp_icod_produccion);

            rptReporteProducDet rpt = new rptReporteProducDet();
            rpt.Cargar(Obe, mListaDetalle, mListaCostosProduccion);
        }

        private void ingresarPTAlmacentoolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (mLista.Count > 0)
                {
                    int IdSituacion = int.Parse(viewReporteProduccion.GetFocusedRowCellValue("rp_iid_situacion").ToString());
                    if (IdSituacion == Parametros.intSitReporteProduccionGenerado)
                    {
                        FrmManteIngresoReporteProduccion Reporte = new FrmManteIngresoReporteProduccion();
                        Reporte.MiEvento += new FrmManteIngresoReporteProduccion.DelegadoMensaje(form2_MiEvento);
                        EReporteProduccion Obe = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
                        Reporte.IdReporteProduccion = Obe.rp_icod_produccion;
                        Reporte.btnAlmacen.Tag = Convert.ToInt32(Obe.almac_icod_almacen);
                        Reporte.IdProductoEspecifico = Convert.ToInt32(Obe.prdc_icod_producto);
                        Reporte.NumeroRP = Obe.rp_num_produccion;
                        Reporte.Show();
                        Reporte.txtCodigo.Text = Obe.prdc_icod_producto.ToString();
                        Reporte.btnAlmacen.Text = Obe.almac_vdescripcion;
                        Reporte.txtCantidad.EditValue = Obe.rp_ncant_pro;
                        Reporte.dtmFecha.DateTime = Obe.rp_sfecha_produccion;
                    }
                    else
                    {
                        string sSituacion = "";
                        if (IdSituacion == Parametros.intSitReporteProduccionActualizado) { sSituacion = "Actualizado"; }
                        XtraMessageBox.Show("No se puede ingresar el producto al almacén \n" + "Situación : " + sSituacion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Metodos"

        private void Carga()
        {
            mLista = new BCompras().ListarReporteProduccion(Convert.ToInt32(Parametros.intEjercicio));
            grdReporteProduccion.DataSource = mLista;
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            viewReporteProduccion.FocusedRowHandle = xposition;
        }

        void AddEvent()
        {
            this.viewReporteProduccion.DoubleClick += new System.EventHandler(this.viewReporteProduccion_DoubleClick);
        }

        #endregion

        private void txtNúmero_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            grdReporteProduccion.DataSource = mLista.Where(obj => obj.rp_num_produccion.ToUpper().Contains(txtNúmero.Text.ToUpper())).ToList();
        }

        private void eliminarPTDelAlmacénToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Por Completar*/
            EReporteProduccion Obe = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
            if (Obe.rp_id_kardex_producto_ingreso != 0)
            {
                if (XtraMessageBox.Show("Desea eliminar el producto Terminado del almacén", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.rp_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                    new BCompras().EliminarPTKardex(Obe);
                }

            }
        }

        private void actualizarCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Por Completar*/
            try
            {
                if (mLista.Count > 0)
                {
                    EReporteProduccion Obe = (EReporteProduccion)viewReporteProduccion.GetRow(viewReporteProduccion.FocusedRowHandle);
                    // Detalle Producción
                    List<EReporteProduccionDetalle> mListaDetalle = new BCompras().ListarReporteProduccionDetalle(Obe.rp_icod_produccion);
                    //costo valorizado
                    List<EKardexValorizado> mListaKVal = new List<EKardexValorizado>();
                    Obe.rp_nmonto_total_soles = 0;
                    decimal MontoTotal = 0;
                    decimal pcp = 0;
                    DateTime fecha_inicio = Convert.ToDateTime("01/01/" + Parametros.intEjercicio);
                    foreach (EReporteProduccionDetalle item in mListaDetalle)
                    {
                        mListaKVal = new BAlmacen().ListarKardexValorizadoInventario(item.almcc_icod_almacen, item.prdc_icod_producto, fecha_inicio, Obe.rp_sfecha_produccion);
                        if (mListaKVal.Count != 0)
                        {
                            
                            int cont_max = mListaKVal.Max(ob => ob.Cont_registro_valorizado);
                            mListaKVal = mListaKVal.Where(o => o.Cont_registro_valorizado == cont_max).ToList();
                            item.rpd_nmonto_unitario_costo_producto = mListaKVal[0].kardv_precio_costo_promedio;
                            //item.rpd_nmonto_total_costo_producto =  item.rpd_ncant_pro * item.rpd_nmonto_unitario_costo_producto;
                            pcp = item.rpd_ncant_pro * item.rpd_nmonto_unitario_costo_producto;

                            item.rpd_nmonto_total_costo_producto = Math.Round(pcp,2);

                            //Obe.rp_nmonto_total_soles = Obe.rp_nmonto_total_soles + item.rpd_nmonto_total_costo_producto;
                            MontoTotal = MontoTotal + item.rpd_nmonto_total_costo_producto;
                            Obe.rp_nmonto_total_soles = Math.Round(MontoTotal, 2);
                        }
                    }
                    Obe.rp_nmonto_total_dolares = Obe.rp_nmonto_total_soles / Obe.rp_ntipo_cambio;
                    Obe.rp_flag_estado = true;
                    new BCompras().ActualizarReporteProduccionDetalleCostos(mListaDetalle);
                    new BCompras().ActualizarReporteProduccionMontoTotal(Obe);
                    XtraMessageBox.Show("Se actualizaron los costos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("No hay registros para actualizar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}