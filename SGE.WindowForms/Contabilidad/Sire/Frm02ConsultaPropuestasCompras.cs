using DevExpress.XtraEditors;
using Newtonsoft.Json;
using SGE.BusinessLogic;
using SGE.Entity.Sire;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Sire
{
    public partial class Frm02ConsultaPropuestasCompras : XtraForm
    {
        public Frm02ConsultaPropuestasCompras()
        {
            InitializeComponent();
        }

        private void Frm02ConsultaPropuestasCompras_Load(object sender, EventArgs e) => CargarCombos();

        public List<ESire> lstSire = new List<ESire>();
        public List<Ejercicio> lstEjercicio = new List<Ejercicio>();
        public SireRequest request = new SireRequest();

        private async void CargarCombos()
        {
            btnPropuesta.Enabled = false;
            try
            {
                request = new SireRequest()
                {                    
                    operacion = "COMPRA",
                };

                var responseBody = await SireServiceImpl.ConsultarPeriodosActivos(request);
                lstEjercicio = JsonConvert.DeserializeObject<List<Ejercicio>>(responseBody.Data);

                BSControls.LoaderLook(lkpAnio, lstEjercicio, "numEjercicio", "numEjercicio", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                btnPropuesta.Enabled = true;
            }
        }

        private void lkpAnio_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, lstEjercicio.Where(x => Convert.ToInt32(x.numEjercicio) == Convert.ToInt32(lkpAnio.EditValue)).FirstOrDefault().lisPeriodos, "select", "perTributario", true);
        }

        private void btnPropuesta_Click(object sender, EventArgs e) => Consultar();

        public async void Consultar()
        {
            btnPropuesta.Enabled = false;
            try
            {
                int page = 0;
                request.periodo = lkpMes.EditValue.ToString();
                List<RegistroComprasDTO> lista = new List<RegistroComprasDTO>();
                bool seguir = true;
                while (seguir) {
                    request.page = ++page;
                    var responseBody = await SireServiceImpl.ConsultarPropuesta(request);
                    if (!responseBody.Succes)
                        throw new ArgumentException(responseBody.Data);
                    var Propuesta = JsonConvert.DeserializeObject<Propuesta<RegistroCompra>>(responseBody.Data);
                    lista.AddRange( RegistroComprasConvert.Convert(Propuesta.Registros));
                    seguir = !(lista.Count() == Propuesta.Paginacion.TotalRegistros);
                }

                
                grdLista.DataSource = lista;
                grdLista.RefreshDataSource();
                viewLista.BestFitColumns();
            }
            catch (Exception ex)
            {

                Services.MessageError(ex.Message) ;
            }
            finally
            {
                btnPropuesta.Enabled = true;
            }

        }

        private void descargarCSVToolStripMenuItem_Click(object sender, EventArgs e) => DescargarArchivo();

        private async void DescargarArchivo()
        {
            contextMenuStrip1.Enabled = false;
            btnPropuesta.Enabled = false;
            request.nombreArchivo = string.Empty;
            try
            {
                var responseBody = await SireServiceImpl.DescargarPropuesta(request);
                request.numeroTicket = (JsonConvert.DeserializeObject<TicketInfo>(responseBody.Data)).numTicket;

                while (string.IsNullOrEmpty(request.nombreArchivo))
                {
                    responseBody = await SireServiceImpl.ConsultarTicket(request);
                    var responseTicket = (JsonConvert.DeserializeObject<RespuestaTicket>(responseBody.Data)).Registros[0];
                    request.nombreArchivo = responseTicket.ArchivoReporte == null ? "" : responseTicket.ArchivoReporte[0].NomArchivoReporte;
                }
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = request.nombreArchivo;
                if (save.ShowDialog() == DialogResult.OK)
                {

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        save.FileName = save.FileName.Contains(".zip") ? save.FileName : save.FileName + ".zip";
                        var archivoStream = await SireServiceImpl.DescargarArchivo(request);
                        await archivoStream.Data.CopyToAsync(memoryStream);
                        byte[] datosBinarios = memoryStream.ToArray();
                        File.WriteAllBytes(save.FileName, datosBinarios);
                        Process.Start(save.FileName);
                    }
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                contextMenuStrip1.Enabled = true;
                btnPropuesta.Enabled = true;
            }
        }

        private void exportaListaExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                string ruta = save.FileName.Contains(".xlsx") ? save.FileName : save.FileName + ".xlsx";
                grdLista.ExportToXlsx(ruta);
                Process.Start(ruta);
            }
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            request.periodo = lkpMes.EditValue.ToString();
        }

        private void resumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmResumenDocumentosSire frm = new FrmResumenDocumentosSire())
            {
                frm.Text = "RESUMEN DOCUMENTOS DE COMPRA";
                frm.request = request;
                frm.ShowDialog();
            }
        }

        private void compararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((grdLista.DataSource as List<RegistroComprasDTO>).Count == 0)
            {
                Consultar();
            }

            FrmComparDocumentos frm = new FrmComparDocumentos();
            frm.listaSunat = GenerarListaSunat();
            frm.listaDocumentos = GenerarListaDocumentos();
            frm.Text = "Comparación de Registro de Compras";
            frm.Show();
        }

        public List<DocumentoComparacion> GenerarListaSunat()
        {
            var lista = new List<DocumentoComparacion>();
            var listaOrigen = grdLista.DataSource as List<RegistroComprasDTO>;

            listaOrigen.ForEach(x =>
            {
                lista.Add(new DocumentoComparacion
                {
                    Ruc = x.NumDocIdentidadProveedor,
                    TipoDocumento = x.CodTipoCdp,
                    Serie = x.NumSerieCdp,
                    Correlativo = x.NumCdp,
                    Proveedor = x.NomRazonSocialProveedor,
                    Fecha = Convert.ToDateTime(x.FecEmision),
                    BaseImponible = x.MtoBiGravadaDg,
                    Impuesto = x.MtoIgvIpmDg,
                    NoGravado = x.MtoBiGravadaDng,
                    MontoTotal = x.MtoTotalCp,
                    RucProveedor = x.NumDocIdentidadProveedor,
                    FechaVencimiento = string.IsNullOrEmpty(x.FecVencPag) ? x.FecEmision : x.FecVencPag,
                    Moneda = x.CodMoneda,
                    TipoDocumentoReferencia = x.CodTipoCdpMod,
                    SerieDocumentoReferencia = x.NumSerieCdpMod,
                    NumeroDocumentoReferencia = x.NumCdpMod.ToString(),
                    OtrosImpuestos = x.MtoOtrosTrib,
                });

            });

            return lista;
        }
        public List<DocumentoComparacion> GenerarListaDocumentos()
        {
            var lista = new List<DocumentoComparacion>();
            var listaOrigen = new BContabilidad().ListarRegistroDeCompras(Convert.ToInt32(lkpAnio.Text), Convert.ToInt32(lkpMes.EditValue.ToString().Substring(4))).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
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
    }
}