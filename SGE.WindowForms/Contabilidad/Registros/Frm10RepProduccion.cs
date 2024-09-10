using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;
using SGE.WindowForms.Otros.Costos;
using System.Threading;
using SGE.WindowForms.Otros.Contabilidad;


namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class Frm10RepProduccion : DevExpress.XtraEditors.XtraForm
    {
        //List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
        //List<EVoucherContableDet> lstDetalleGeneral = new List<EVoucherContableDet>();
        List<EVoucherContableCab> lstVCOCab = new List<EVoucherContableCab>();
        List<EVoucherContableDet> lstVCOdet = new List<EVoucherContableDet>();   
        List<EFacturaCab> lstFacturaCab = new List<EFacturaCab>();
        BContabilidad objBContabilidad = new BContabilidad();        
        int intMes;
        string strMes;
        int intIndicador = 0; // 1 = Cargar , 2 = Actualizar
        public Frm10RepProduccion()
        {
            InitializeComponent();
        }
        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            lkpMes.Enabled = !Enable;
            btnGenerar.Enabled = !Enable;
            mnu.Enabled = !Enable;           
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //lblMensaje.Text = "Espere mientras se cargan los datos...";
            ///**/
            //lstCabeceras.Clear();
            //lstDetalleGeneral.Clear();
            //viewCostoVentas.RefreshData();
            ///**/
            //controlEnable(true);
            //backgroundWorker1.RunWorkerAsync();   
            /*-------------------------------------*/
            limpiarListas();
            /*-------------------------------------*/
            intMes = Convert.ToInt32(lkpMes.EditValue);
            strMes = lkpMes.Text;
            intIndicador = 1;//Indicador 1 = Generar VCO (solamente)
            enableLoading(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void limpiarListas()
        {
            lstVCOCab.Clear();
            lstVCOdet.Clear();
            viewVCO.RefreshData();
        }
        private void Frm10RepProduccion_Load(object sender, EventArgs e)
        {
            //cargar();
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
        }
        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            lkpMes.Enabled = !flag;
            btnGenerar.Enabled = !flag;
            mnu.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }
        private void cargar()
        {
            try
            {
                var tuple = objBContabilidad.generarVouchersReporteProduccion(intMes, Valores.intUsuario, WindowsIdentity.GetCurrent().Name);
                lstVCOCab = tuple.Item1;
                lstVCOdet = tuple.Item2;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema");
            }
        }
        private void insertarVoucherContable()
        {
            try
            {
                #region Datatable Comprobante Cabecera
                DataTable dteComproCab = new DataTable();
                DataColumn column;
                DataRow row;
                int i = 1;

                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "anioc_iid_anio"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "mesec_iid_mes"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "vcocc_icod_vcontable"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "subdi_icod_subdiario"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "vcocc_numero_vcontable"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.DateTime"); column.ColumnName = "vcocc_fecha_vcontable"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "vcocc_glosa"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "vcocc_observacion"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "tarec_icorrelativo_situacion_vcontable"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "tarec_icorrelativo_origen_vcontable"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "tablc_iid_moneda"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "vcocc_tipo_cambio"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "vcocc_nmto_tot_debe_sol"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "vcocc_nmto_tot_haber_sol"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "vcocc_nmto_tot_debe_dol"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "vcocc_nmto_tot_haber_dol"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "doxpc_viid_correlativo"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "tbl_origen"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "tbl_origen_icod"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Boolean"); column.ColumnName = "vcocc_flag_estado"; dteComproCab.Columns.Add(column);

                foreach (var obj in lstVCOCab)
                {

                    row = dteComproCab.NewRow();
                    row["anioc_iid_anio"] = obj.anioc_iid_anio;
                    row["mesec_iid_mes"] = obj.mesec_iid_mes;
                    row["vcocc_icod_vcontable"] = obj.vcocc_icod_vcontable;
                    row["subdi_icod_subdiario"] = obj.subdi_icod_subdiario;
                    row["vcocc_numero_vcontable"] = string.Format("{0:00000}", i);
                    row["vcocc_fecha_vcontable"] = obj.vcocc_fecha_vcontable;
                    row["vcocc_glosa"] = obj.vcocc_glosa;
                    row["vcocc_observacion"] = obj.vcocc_observacion;
                    row["tarec_icorrelativo_situacion_vcontable"] = obj.tarec_icorrelativo_situacion_vcontable;
                    row["tarec_icorrelativo_origen_vcontable"] = obj.tarec_icorrelativo_origen_vcontable;
                    row["tablc_iid_moneda"] = obj.tablc_iid_moneda;
                    row["vcocc_tipo_cambio"] = obj.vcocc_tipo_cambio;
                    row["vcocc_nmto_tot_debe_sol"] = obj.vcocc_nmto_tot_debe_sol;
                    row["vcocc_nmto_tot_haber_sol"] = obj.vcocc_nmto_tot_haber_sol;
                    row["vcocc_nmto_tot_debe_dol"] = obj.vcocc_nmto_tot_debe_dol;
                    row["vcocc_nmto_tot_haber_dol"] = obj.vcocc_nmto_tot_haber_dol;
                    row["doxpc_viid_correlativo"] = obj.doxpc_viid_correlativo;
                    row["tbl_origen"] = obj.tbl_origen;
                    row["tbl_origen_icod"] = obj.tbl_origen_icod;
                    row["vcocc_flag_estado"] = true;
                    dteComproCab.Rows.Add(row);
                    i++;
                }
                #endregion

                #region Datatable Comprobante Det
                DataTable dteComproDet = new DataTable();
                DataColumn columnDet;
                DataRow rowDet;


                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "vcocc_icod_vcontable"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "vcocd_nro_item_det"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "tdocc_icod_tipo_doc"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = Type.GetType("System.String"); columnDet.ColumnName = "vcocd_numero_doc"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "ctacc_icod_cuenta_contable"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "cecoc_icod_centro_costo"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "tablc_iid_tipo_analitica"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "anad_icod_analitica"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = Type.GetType("System.String"); columnDet.ColumnName = "vcocd_vglosa_linea"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "tablc_iid_moneda"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Decimal"); columnDet.ColumnName = "vcocd_tipo_cambio"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Decimal"); columnDet.ColumnName = "vcocd_nmto_tot_debe_sol"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Decimal"); columnDet.ColumnName = "vcocd_nmto_tot_haber_sol"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Decimal"); columnDet.ColumnName = "vcocd_nmto_tot_debe_dol"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Decimal"); columnDet.ColumnName = "vcocd_nmto_tot_haber_dol"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "ctacc_iid_cuenta_contable_ref"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "tarec_icorrelativo_origen_vcontable"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Boolean"); columnDet.ColumnName = "vcocd_flag_estado"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "doxpc_icod_correlativo"; dteComproDet.Columns.Add(columnDet);
                columnDet = new DataColumn(); columnDet.DataType = System.Type.GetType("System.Int32"); columnDet.ColumnName = "doxcc_icod_correlativo"; dteComproDet.Columns.Add(columnDet);

                foreach (var _BE in lstVCOdet)
                {
                    if (_BE.vcocc_icod_vcontable == 879)
                    {
                        int l = 0;
                    }
                    row = dteComproDet.NewRow();
                    row["vcocc_icod_vcontable"] = _BE.vcocc_icod_vcontable;
                    row["vcocd_nro_item_det"] = _BE.vcocd_nro_item_det;
                    row["tdocc_icod_tipo_doc"] = _BE.tdocc_icod_tipo_doc;
                    row["vcocd_numero_doc"] = _BE.vcocd_numero_doc;
                    row["ctacc_icod_cuenta_contable"] = _BE.ctacc_icod_cuenta_contable;
                    row["cecoc_icod_centro_costo"] = (_BE.cecoc_icod_centro_costo == null) ? (object)DBNull.Value : _BE.cecoc_icod_centro_costo;
                    row["tablc_iid_tipo_analitica"] = (_BE.tablc_iid_tipo_analitica == null) ? (object)DBNull.Value : _BE.tablc_iid_tipo_analitica;
                    row["anad_icod_analitica"] = (_BE.anad_icod_analitica == null) ? (object)DBNull.Value : _BE.anad_icod_analitica;
                    row["vcocd_vglosa_linea"] = _BE.vcocd_vglosa_linea;
                    row["tablc_iid_moneda"] = _BE.tablc_iid_moneda;
                    row["vcocd_tipo_cambio"] = _BE.vcocd_tipo_cambio == null ? (object)DBNull.Value : _BE.vcocd_tipo_cambio; ;
                    row["vcocd_nmto_tot_debe_sol"] = _BE.vcocd_nmto_tot_debe_sol;
                    row["vcocd_nmto_tot_haber_sol"] = _BE.vcocd_nmto_tot_haber_sol;
                    row["vcocd_nmto_tot_debe_dol"] = _BE.vcocd_nmto_tot_debe_dol;
                    row["vcocd_nmto_tot_haber_dol"] = _BE.vcocd_nmto_tot_haber_dol;
                    row["ctacc_iid_cuenta_contable_ref"] = _BE.ctacc_iid_cuenta_contable_ref == null ? (object)DBNull.Value : _BE.ctacc_iid_cuenta_contable_ref;
                    row["tarec_icorrelativo_origen_vcontable"] = _BE.tarec_icorrelativo_origen_vcontable == null ? (object)DBNull.Value : _BE.tarec_icorrelativo_origen_vcontable;
                    row["vcocd_flag_estado"] = true;
                    row["doxpc_icod_correlativo"] = _BE.doxpc_icod_correlativo;
                    row["doxcc_icod_correlativo"] = _BE.doxcc_icod_correlativo;
                    dteComproDet.Rows.Add(row);
                    i++;
                }
                #endregion

                objBContabilidad.insertarVoucherContableListas(dteComproCab, dteComproDet, intMes, "REPOR-PRODUC");


            }
            catch (Exception ex)
            {
                backgroundWorker1.CancelAsync();
                throw ex;
            }
        }
        private void verDetalle()
        {
            try
            {
                EVoucherContableCab oBe = (EVoucherContableCab)viewVCO.GetRow(viewVCO.FocusedRowHandle);
                if (oBe == null)
                    return;
                frmMovimientoVoucher frm = new frmMovimientoVoucher();
                var lst = lstVCOdet.Where(a => a.vcocc_icod_vcontable == oBe.vcocc_icod_vcontable).ToList();
                frm.lstDetalle = lst;
                frm.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }      
        }       

        private void verDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                verDetalle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarComprobantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstVCOdet.Count > 0)
                {
                    intIndicador = 2;//Indicador 2 = Ingresar VCO (solamente)
                    enableLoading(true);
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                backgroundWorker1.CancelAsync();
            }         
        }
        //private void buscar()
        //{
        //    try
        //    {
        //        //lstFacturaCab = (new BCostoVenta()).listarFacturasCab(Parametros.intPeriodo, Convert.ToInt32(lkpMes.EditValue));
        //        intMes = Convert.ToInt32(lkpMes.EditValue);
        //        strMes = lkpMes.Text;
        //        var tuple = (new BCostoVenta()).listarVoucherCostoVentas(lstFacturaCab, Convert.ToInt32(lkpMes.EditValue), Valores.CodeUser, WindowsIdentity.GetCurrent().Name);
        //        //lstCabeceras = tuple.Item1;
        //        //lstDetalleGeneral = tuple.Item2;
        //        (new BCompras()).listarVoucherRepProduccion(lstCabeceras, lstDetalleGeneral, intMes, Parametros.intEjercicio, Valores.intUsuario, WindowsIdentity.GetCurrent().Name);
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
        //private void actualizarComprobantes()
        //{
        //    try
        //    {
        //        //BComprobantes objBComprobantes = new BusinessLogic.Contabilidad.BComprobantes();
        //        //List<string> lstOpcion = new List<string>();
        //        //lstOpcion.Add("REPOR-PRODUC");
        //        //objBComprobantes.eliminarComprobantesPorOpcion(Parametros.intEjercicio, intMes, lstOpcion);
        //        //objBComprobantes.insertarComprobantesListasUDTT(lstCabeceras, lstDetalleGeneral);
        //        //XtraMessageBox.Show("Los datos han sido actualizados correctamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //objBContabilidad.insertarVoucherContableListas(dteComproCab, dteComproDet, intMes, "COMPRAS");
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (intIndicador == 1)
                    cargar();
                if (intIndicador == 2)
                {
                    insertarVoucherContable();
                    if (backgroundWorker1.CancellationPending == true)
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (intIndicador == 1)
                {
                    enableLoading(false);
                    grdVCO.DataSource = lstVCOCab;
                }
                if (intIndicador == 2)
                {
                    enableLoading(false);
                    limpiarListas();
                    XtraMessageBox.Show("La actualización se realizó satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void viewCostoVentas_DoubleClick(object sender, EventArgs e)
        {
            verDetalle();
        }     
    }
}