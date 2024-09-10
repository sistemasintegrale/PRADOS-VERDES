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
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Sire
{
    public partial class Frm02ConsultaPropuestaVentas : XtraForm
    {
        public Frm02ConsultaPropuestaVentas()
        {
            InitializeComponent();
        }

        private void Frm02ConsultaPropuestasCompras_Load(object sender, EventArgs e) => CargarCombos();

        public List<ESire> lstSire = new List<ESire>();
        public List<Ejercicio> lstEjercicio = new List<Ejercicio>();
        public SireRequest request = new SireRequest();

        private async Task CargarCombos()
        {
            btnPropuesta.Enabled = false;
            try
            {
                request = new SireRequest()
                {
                    operacion = "VENTA",
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

        private void btnPropuesta_Click(object sender, EventArgs e) => LeerPropuestaVenta();

        public async void Consultar()
        {
            btnPropuesta.Enabled = false;
            try
            {
                List<RegistroVenta> lista = new List<RegistroVenta>();
                int page = 0;
                request.periodo = lkpMes.EditValue.ToString();
                bool seguir = true;
                while (seguir)
                {
                    request.page = ++page;
                    var responseBody = await SireServiceImpl.ConsultarPropuesta(request);
                    var Propuesta = JsonConvert.DeserializeObject<Propuesta<RegistroVenta>>(responseBody.Data);
                    lista.AddRange(Propuesta.Registros);
                    seguir = !(lista.Count() == Propuesta.Paginacion.TotalRegistros);
                }
                var f = lista.Where(x => x.NumCdp.Contains("373")).ToList();
                grdLista.DataSource = lista;
                grdLista.RefreshDataSource();
                viewLista.BestFitColumns();
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

        private void exportaListaExcelToolStripMenuItem_Click(object sender, EventArgs e) => Services.ExportarExcel(grdLista);

        private void lkpMes_EditValueChanged(object sender, EventArgs e) => request.periodo = lkpMes.EditValue.ToString();

        private void resumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmResumenDocumentosSire frm = new FrmResumenDocumentosSire())
            {
                frm.Text = "RESUMEN DOCUMENTOS DE VENTA";
                frm.request = request;
                frm.ShowDialog();
            }
        }

        private void compararToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((grdLista.DataSource as List<RegistroVenta>).Count == 0)
            {
                Consultar();
            }

            FrmComparDocumentos frm = new FrmComparDocumentos();
            frm.listaSunat = GenerarListaSunat();
            frm.listaDocumentos = GenerarListaDocumentos();
            frm.Text = "Comparación de Registro de Ventas";
            frm.Show();
        }
        public List<DocumentoComparacion> GenerarListaSunat()
        {
            List<DocumentoComparacion> lista = new List<DocumentoComparacion>();
            List<RegistroVenta> listaOrigen = grdLista.DataSource as List<RegistroVenta>;

            listaOrigen.ForEach(x =>
            {
                lista.Add(new DocumentoComparacion
                {
                    Ruc = x.NumDocIdentidad,
                    TipoDocumento = x.CodTipoCdp,
                    Serie = x.NumSerieCdp,
                    Correlativo = x.NumCdp.ToString(),
                    Proveedor = x.NomRazonSocialCliente,
                    Fecha = Convert.ToDateTime(x.FecEmision),
                    BaseImponible = Convert.ToDecimal(x.MtoBiGravada),
                    Impuesto = x.MtoIgv,
                    NoGravado = x.MtoBiGravada,
                    MontoTotal = x.MtoTotalCp,
                    RucProveedor = x.NumDocIdentidad,
                    FechaVencimiento = string.IsNullOrEmpty(x.FecVencPag) ? x.FecEmision : x.FecVencPag,
                    Moneda = x.CodMoneda,
                    TipoDocumentoReferencia = x.CodTipoCDPMod,
                    SerieDocumentoReferencia = x.NumSerieCDPMod,
                    NumeroDocumentoReferencia = x.NumSerieCDPMod,
                    OtrosImpuestos = x.MtoOtrosTrib,
                });

            });

            return lista;
        }
        public List<DocumentoComparacion> GenerarListaDocumentos()
        {
            var lista = new List<DocumentoComparacion>();
            var listaOrigen = new BContabilidad().ListarRegistroDeVentas(Convert.ToInt32(lkpAnio.Text), Convert.ToInt32(lkpMes.EditValue.ToString().Substring(4))).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
            listaOrigen.ForEach(x =>
            {
                lista.Add(new DocumentoComparacion
                {
                    Ruc = x.num_doc_cliente,
                    TipoDocumento = x.tdocc_vcodigo_tipo_doc_sunat,
                    Serie = x.doxcc_vnumero_doc.Substring(0, 4),
                    Correlativo = x.doxcc_vnumero_doc.Substring(4).TrimStart('0'),
                    Proveedor = string.IsNullOrEmpty(x.cliec_vnombre_cliente_2)  ? x.cliec_vnombre_cliente : x.cliec_vnombre_cliente_2,
                    Fecha = Convert.ToDateTime(x.doxcc_sfecha_doc),
                    BaseImponible = x.doxcc_nmonto_afecto, //x.doxpc_nmonto_destino_gravado,
                    Impuesto = x.doxcc_nmonto_impuesto,
                    NoGravado = x.doxcc_nmonto_inafecto,
                    MontoTotal = x.doxcc_nmonto_total

                });

            });

            return lista;
        }

        public async void LeerPropuestaVenta()
        {
            contextMenuStrip1.Enabled = false;
            btnPropuesta.Enabled = false;
            request.nombreArchivo = string.Empty;
            List<RegistroVenta> lista = new List<RegistroVenta>();
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

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var archivoStream = await SireServiceImpl.DescargarArchivo(request);
                    await archivoStream.Data.CopyToAsync(memoryStream);
                    byte[] datosBinarios = memoryStream.ToArray();
                    File.WriteAllBytes(request.nombreArchivo, datosBinarios);
                    byte[] zipData = File.ReadAllBytes(request.nombreArchivo);
                    await memoryStream.WriteAsync(zipData, 0, zipData.Length);

                    // Descomprimir el archivo zip
                    using (ZipArchive archive = new ZipArchive(memoryStream))
                    {
                        // Iterar sobre cada entrada en el archivo zip
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            // Verificar si la entrada es un archivo txt
                            if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                            {
                                // Leer el contenido del archivo txt
                                using (StreamReader reader = new StreamReader(entry.Open()))
                                {
                                    string linea;
                                    // Ignoramos la primera línea ya que contiene los encabezados
                                    reader.ReadLine();
                                    while ((linea = reader.ReadLine()) != null)
                                    {
                                        // Dividir la línea usando el separador '|'
                                        string[] elementos = linea.Split('|');
                                        int index = 0;
                                        lista.Add(new RegistroVenta()
                                        {

                                            NumRuc = elementos[index++],
                                            NomRazonSocial = elementos[index++],
                                            PerPeriodoTributario = Convert.ToInt32(elementos[index++]),
                                            CodCar = elementos[index++],
                                            FecEmision = elementos[index++],
                                            FecVencPag = elementos[index++],
                                            CodTipoCdp = elementos[index++],
                                            NumSerieCdp = elementos[index++],
                                            NumCdp = elementos[8],
                                            CodTipoDocIdentidad = Convert.ToInt32(elementos[10]),
                                            NumDocIdentidad = elementos[11],
                                            NomRazonSocialCliente = elementos[12],
                                            MtoValFactExpo = Convert.ToDecimal(elementos[13]),
                                            MtoBiGravada = Convert.ToDecimal(elementos[14]),
                                            MtoDsctoBi = Convert.ToDecimal(elementos[15]),
                                            MtoIgv = Convert.ToDecimal(elementos[16]),
                                            MtoDsctoIgv = Convert.ToDecimal(elementos[17]),
                                            MtoExonerado = Convert.ToDecimal(elementos[18]),
                                            MtoInafecto = Convert.ToDecimal(elementos[19]),
                                            MtoIsc = Convert.ToDecimal(elementos[20]),
                                            MtoBiIvap = Convert.ToDecimal(elementos[21]),
                                            MtoIvap = Convert.ToDecimal(elementos[22]),
                                            MtoIcbp = Convert.ToDecimal(elementos[23]),
                                            MtoOtrosTrib = Convert.ToDecimal(elementos[24]),
                                            MtoTotalCp = Convert.ToDecimal(elementos[25]),
                                            CodMoneda = elementos[26],
                                            MtoTipoCambio = Convert.ToDecimal(elementos[27]),
                                            FecEmisionMod = elementos[28],
                                            CodTipoCDPMod = elementos[29],
                                            NumSerieCDPMod = elementos[30],
                                            NumCDPMod = elementos[31]
                                        });


                                    }
                                    grdLista.DataSource = lista;

                                    grdLista.RefreshDataSource();
                                }
                            }
                        }
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
    }
}