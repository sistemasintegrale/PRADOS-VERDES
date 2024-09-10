using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.Entity.Sire;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmComparDocumentos : XtraForm
    {
        public List<DocumentoComparacion> listaSunat;
        public List<DocumentoComparacion> listaDocumentos;
        public List<ResultadoComparacion> listaNoExiste;

        public FrmComparDocumentos() => InitializeComponent();
        private void exportarAExcelToolStripMenuItem_Click(object sender, System.EventArgs e) => ExportarExcel();
        private void FrmComparDocumentos_Load(object sender, System.EventArgs e) => CargaInicial();

        private void CargaInicial()
        {
            listaNoExiste = new List<ResultadoComparacion>();
            listaSunat.ForEach(x =>
            {
                var docEncontrado = listaDocumentos.Where(y => y.TipoDocumento == x.TipoDocumento && y.Serie == x.Serie && y.Correlativo == x.Correlativo &&  (y.Ruc.Contains(x.Ruc) || x.Ruc.Contains(y.Ruc))).FirstOrDefault();
                if (docEncontrado is null)
                {
                    listaNoExiste.Add(new ResultadoComparacion(x, "SUNAT"));
                } 

            });

            listaDocumentos.ForEach(x =>
            {
                var docEncontrado = listaSunat.Where(y => y.TipoDocumento == x.TipoDocumento && y.Serie == x.Serie && y.Correlativo == x.Correlativo &&  (x.Ruc.Contains(y.Ruc) || y.Ruc.Contains(x.Ruc))).FirstOrDefault();
                if (docEncontrado is null)
                {
                    listaNoExiste.Add(new ResultadoComparacion(x, "SISTEMA"));
                }

            });
            
             

            grdLista.DataSource = listaNoExiste;
            grdLista.RefreshDataSource();
            viewLista.BestFitColumns();

        }


        private void ExportarExcel()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string path = saveFile.FileName.Contains(".xlsx") ? saveFile.FileName : saveFile.FileName + ".xlsx";
                grdLista.ExportToXlsx(path);
                Process.Start(path);
            }
        }

        private void viewLista_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            GridView view = sender as GridView;

            var currentCell = view.GetRow(e.RowHandle) as ResultadoComparacion;
            e.Appearance.BackColor = currentCell.Origen != "SUNAT" ? Color.LightGreen : Color.Salmon;

        }

        private void viewLista_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var select = viewLista.GetFocusedRow() as ResultadoComparacion;
            if (select is null) return;

            clSelect.OptionsColumn.AllowEdit = select.Origen == "SUNAT";
            clSelect.OptionsColumn.AllowFocus = select.Origen == "SUNAT";
            clSelect.OptionsColumn.AllowIncrementalSearch = select.Origen == "SUNAT";
        }

        private async void btnImportar_Click(object sender, System.EventArgs e)
        {
            var listaSelect = listaNoExiste.Where(x => x.Origen == "SUNAT" && x.Importar).ToList();
            if (listaSelect.Count() == 0)
                XtraMessageBox.Show("No se seleccionó ningún registro", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                btnImportar.Enabled = false;
                var proveedores = await Task.Run(() => new BCompras().ListarProveedor());
                var listaDxP = new List<EDocPorPagar>();
                listaSelect.ForEach(x =>
                {
                    var DXP = new EDocPorPagar();
                    DXP.doxpc_anio = Parametros.intEjercicio.ToString();
                    DXP.anio = Parametros.intEjercicio;
                    DXP.doxpc_flag_estado = true;
                    DXP.mesec_iid_mes = Convert.ToDateTime(x.Fecha).Month;
                    DXP.tdocc_icod_tipo_doc = x.TipoDocumento == "01" ? Parametros.intTipoDocFacturaCompra : (x.TipoDocumento == "07" ? Parametros.intTipoDocNotaCreditoProveedor : Parametros.intTipoDocBoletaCompra);
                    DXP.tdodc_iid_correlativo = DXP.tdocc_icod_tipo_doc == Parametros.intTipoDocFacturaCompra ? Parametros.intClaseTipoDocFAC : (DXP.tdocc_icod_tipo_doc == Parametros.intTipoDocBoletaCompra ? Parametros.intClaseTipoDocBOC : Parametros.intClaseTipoDocNCP);
                    DXP.doxpc_vnumero_serio = x.Serie;
                    DXP.doxpc_vnumero_correlativo = x.Correlativo.PadLeft(8, '0');
                    DXP.doxpc_vnumero_doc = DXP.doxpc_vnumero_serio + DXP.doxpc_vnumero_correlativo;
                    DXP.doxpc_sfecha_doc = Convert.ToDateTime(x.Fecha);
                    DXP.doxpc_sfecha_vencimiento_doc = Convert.ToDateTime(x.FechaVencimiento);
                    DXP.proc_icod_proveedor = proveedores.Where(y => y.vruc == x.RucProveedor).First().iid_icod_proveedor;
                    if (DXP.proc_icod_proveedor == 0)
                        throw new ArgumentException($"No existe Proveedor {x.RucProveedor} registrado en el sistema");
                    DXP.tablc_iid_tipo_moneda = x.Moneda == "PEN" ? Parametros.intSoles : Parametros.intDolares;
                    DXP.doxpc_nmonto_tipo_cambio = new BContabilidad().getTipoCambioPorFecha(DXP.doxpc_sfecha_doc.Value);
                    DXP.doxpc_nmonto_destino_gravado = x.BaseImponible.Value;
                    DXP.doxpc_nmonto_imp_destino_gravado = x.Impuesto;
                    DXP.doxpc_nmonto_total_saldo = x.MontoTotal;
                    DXP.doxpc_nporcentaje_igv = 18;
                    DXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    DXP.doxpc_tipo_comprobante_referencia = x.TipoDocumentoReferencia == "" ? 0 : x.TipoDocumentoReferencia == "01" ? Parametros.intTipoDocFacturaCompra : (x.TipoDocumentoReferencia == "07" ? Parametros.intTipoDocNotaCreditoProveedor : Parametros.intTipoDocBoletaCompra);
                    DXP.doxpc_num_serie_referencia = x.SerieDocumentoReferencia;
                    DXP.doxpc_num_comprobante_referencia = x.NumeroDocumentoReferencia.PadLeft(8, '0');
                    DXP.doxpc_iid_correlativo = 0;
                    DXP.doxpc_origen = "2";
                    DXP.doxpc_numdoc_tipo = 1;
                    DXP.doxpc_itipo_adquisicion = 339;
                    DXP.intUsuario = Valores.intUsuario;
                    DXP.strPc = WindowsIdentity.GetCurrent().Name;
                    DXP.doxpc_nmonto_nogravado = x.NoGravado;
                    DXP.doxpc_nporcentaje_imp_renta = 0;
                    DXP.doxpc_nmonto_total_documento = x.MontoTotal;
                    DXP.doxpc_otros_impuestos = x.OtrosImpuestos.Value;
                    listaDxP.Add(DXP);
                });

                listaDxP.ForEach(obe =>
                {
                    using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                    {
                        new BCuentasPorPagar().InsertarEDocPorPagar(obe, null, null, null);
                        tx.Complete();
                    }
                });


                XtraMessageBox.Show("Los Documentos seleccionados fueron importados corretamente.", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listaDocumentos = GenerarListaDocumentos();
                CargaInicial();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally { btnImportar.Enabled = true; }

        }
        public int Mes;
        public List<DocumentoComparacion> GenerarListaDocumentos()
        {
            var lista = new List<DocumentoComparacion>();
            var listaOrigen = new BContabilidad().ListarRegistroDeCompras(Parametros.intEjercicio, Convert.ToInt32(Mes)).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
            listaOrigen.ForEach(x =>
            {
                lista.Add(new DocumentoComparacion
                {
                    Ruc = x.num_doc_proveedor,
                    TipoDocumento = x.tdocc_vcodigo_tipo_doc_sunat,
                    Serie = x.doxpc_vnumero_serio,
                    Correlativo = x.doxpc_vnumero_correlativo.TrimStart('0'),
                    Proveedor = x.proc_vnombrecompleto,
                    Fecha = Convert.ToDateTime(x.doxpc_sfecha_doc),
                    BaseImponible = x.doxpc_nmonto_destino_gravado,
                    Impuesto = x.doxpc_nmonto_imp_destino_gravado,
                    NoGravado = x.doxpc_nmonto_imp_destino_nogravado,
                    MontoTotal = x.doxpc_nmonto_total_documento

                });

            });

            return lista;
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Services.ExportarExcel(grdLista);
        }

        private void btnImportar_Click_1(object sender, EventArgs e)
        {

        }
    }
}