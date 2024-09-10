using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
//using OpenInvoicePeru.Comun.Dto.Intercambio;
//using OpenInvoicePeru.Comun.Dto.Modelos;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Utilities;
using SGE.WindowForms.Ventas.Reporte;
using SIDE.COMUN.DTO.Intercambio;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Factura_Electronica
{
    public partial class frm04ResumenDocumentosExcel : DevExpress.XtraEditors.XtraForm
    {
        private List<ESunatResumenDocumentosCab> lstfacturaElectronica = new List<ESunatResumenDocumentosCab>();

        private int xposition;
        private List<string> mensajeRespuesta = new List<string>();
        private int intIndicador = 0;

        public frm04ResumenDocumentosExcel()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {

            cargar();
            if (chkTodos.Checked == true)
            {
                cargar();
                lkpEstadoSunat.Properties.DataSource = 0;
            }
            else
            {
                cargaEstadoSunat();
            }
        }

        private void cargar()
        {

            grdFacturaElectronica.DataSource = listaFacturaElectronica();
            viewFacturaElectronica.Focus();

        }
        public class TipoEnvioSunat
        {
            public int intCodigo { get; set; }
            public string strTipo { get; set; }
        }
        private void cargaEstadoSunat()
        {
            List<TipoEstadoSunat> lst = new List<TipoEstadoSunat>();
            lst.Add(new TipoEstadoSunat { intCodigo = 1, strTipo = "APROBADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 2, strTipo = "RECHAZADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 3, strTipo = "PENDIENTES POR ENVIAR" });
            BSControls.LoaderLook(lkpEstadoSunat, lst, "strTipo", "intCodigo", true);
            lkpEstadoSunat.ItemIndex = lst.Count - 1;
        }
        public class TipoEstadoSunat
        {
            public int intCodigo { get; set; }
            public string strTipo { get; set; }
        }
        #region Marca
        private void Agregar(int cod)
        {
            cargar();
            viewFacturaElectronica.FocusedRowHandle = lstfacturaElectronica.Count - 1;
            viewFacturaElectronica.Focus();
        }

        private void reload(int cod)
        {
            cargar();
            viewFacturaElectronica.FocusedRowHandle = xposition;
            viewFacturaElectronica.Focus();
        }






        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intIndicador = 1;//Indicador 1 = Generar
            enableLoading(true);
            EnviarSunat();

        }

        private async void EnviarSunat()
        {

            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            List<ESunatResumenDocumentosDet> mlisdet = new List<ESunatResumenDocumentosDet>();
            ESunatResumenDocumentosCab Obe = new ESunatResumenDocumentosCab();
            EResumenResponse response = new EResumenResponse();
            EnviarDocumentoResponse responseConsulta = new EnviarDocumentoResponse();
       
            if (lstfacturaElectronica.Where(p => p.indicador_check).Any())
            {
                mensajeRespuesta = new List<string>();
                foreach (var item in lstfacturaElectronica.Where(p => p.indicador_check))
                {
                    Obe = lstfacturaElectronica.Where(p => p.IdCabecera == item.IdCabecera).FirstOrDefault();
                    mlisdet = new BVentas().listarSunatResumenDocumentosDet(Obe.IdCabecera);

                    response = await eviarsunatresumen(Obe, mlisdet);
                    mensajeRespuesta = new List<string>();
                    //foreach (var item in mlisdet)
                    //{
                        //Guardar Mensaje de respueta
                        if (response.Exito)
                        {
                            this.mensajeRespuesta.Add($"{item.IdDocumento}; Se Envio correctamente.");
                        mlisdet.ForEach(x=> 
                        {
                            List<EFacturaVentaElectronica> lstBolEstado = new List<EFacturaVentaElectronica>();
                            lstBolEstado = new BVentas().listarfacturaVentaElectronica(lstParametro.pm_sfecha_inicio).Where(b=> b.doc_icod_documento == x.doc_icod_documento).ToList();
                            lstBolEstado.ForEach(b=> 
                            {
                                new BVentas().actualizarFacturaElectronicaResponse(b.IdCabecera, (int)EstadoDocumento.aprobado);
                            });

                        });

                        }
                        else
                        {
                            this.mensajeRespuesta.Add($"{item.IdDocumento}; ocurrio un error.");
                        }
                        response.IdItems += $"{item.IdDocumento},";
                    //}

                    int idResponse = new BVentas().insertarResumenDiarioResponse(response);


                    if (idResponse == 0)
                    {
                        new BVentas().modificarFacturaElectronicaResponse(item.IdCabecera);
                        new BVentas().insertarResumenDiarioResponse(response);
                    }
                    if (response.Exito)
                    {
                        //new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.enviadoSunat);
                        int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                        if (codigoRespuesta <= 0)
                        {
                            new BVentas().actualizarResumenDocumentosResponse(Obe.IdCabecera, (int)EstadoDocumento.aprobadoResumen);
                        }
                    }
                    else
                    {
                        int estadoSunat = 0;

                        int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                        if (codigoRespuesta >= (int)EstadoDocumento.rangoExcepcionMin &&
                            codigoRespuesta <= (int)EstadoDocumento.rangoExcepcionMax)
                        {
                            /*Vuelve al estado de pendientes por enviar para ser modificado*/
                            estadoSunat = (int)EstadoDocumento.rechazadoResumen;
                        }
                        else if (codigoRespuesta >= (int)EstadoDocumento.rangoRechazadoMin &&
                            codigoRespuesta <= (int)EstadoDocumento.rangoRechazadoMax)
                        {
                            estadoSunat = (int)EstadoDocumento.rechazadoResumen;
                        }
                        else if (codigoRespuesta >= (int)EstadoDocumento.rangoObservadoMax)
                        {
                            /*Vuelve al estado de pendientes por enviar para ser modificado*/
                            estadoSunat = (int)EstadoDocumento.rechazadoResumen;
                        }
                        new BVentas().actualizarResumenDocumentosResponse(Obe.IdCabecera, estadoSunat);
                    }
                    ////Consulta de tiket
                    //ConsultaTicketRequest objConsulta = new ConsultaTicketRequest();
                    //objConsulta.NroTicket = response.NroTicket;
                    //objConsulta.IdDocumento = response.IdDocumento;
                    //responseConsulta = await ConsultarTiket(objConsulta);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione registro(s)", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            backgroundWorker1.RunWorkerAsync();
        }
        public async Task<EResumenResponse> eviarsunatresumen(ESunatResumenDocumentosCab Obe, List<ESunatResumenDocumentosDet> mlisdet)
        {
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
            ResumenDiarioNuevo objdocumento = new ResumenDiarioNuevo();
            objdocumento = FacturaElectronicaDto.DocumentoElectronicoResumenDocumentos(Obe,mlisdet);
            EResumenResponse response = await model.EnviarResumen(objdocumento);
            return response;
        }

       
        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            SunatToolStripMenuItem.Enabled = !flag;
            mnuMarca.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion Marca


        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            intIndicador = 1;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (intIndicador == 1)
                {
                    enableLoading(false);
                    grdFacturaElectronica.DataSource = listaFacturaElectronica();
                    viewFacturaElectronica.RefreshData();
                    grdFacturaElectronica.RefreshDataSource();
                    if (chkTodos.Checked == true)
                    {
                        cargar();
                        lkpEstadoSunat.Properties.DataSource = 0;
                    }
                    else
                    {
                        cargaEstadoSunat();
                        filtroBuscarGeneral();
                    }
                }
                RespuestaSunat form = new RespuestaSunat();
                form.ListaMensajeRespuesta = this.mensajeRespuesta;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        }

        private List<ESunatResumenDocumentosCab> listaFacturaElectronica()
        {
            DateTime FechaActual;
            List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
            lstfacturaElectronica = new List<ESunatResumenDocumentosCab>();
            lstfacturaElectronica = new BVentas().listarSunatResumenDocumentosExcel();
            ////lstfacturaElectronica = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.EstadoFacturacion != 0).ToList();
            //foreach (var item in lstfacturaElectronica)
            //{
            //    FechaActual = DateTime.Now;
            //    TimeSpan ts = FechaActual - Convert.ToDateTime(item.FEmisionPresentacion);
            //    int Dias = ts.Days;
            //    item.Dias = Dias;
            //    if (item.EstadoFacturacion == (int)EstadoDocumento.aprobado)
            //    {
            //        item.indicador_check = true;
            //        item.Dias = 0;
            //    }
            //    if (item.EstadoFacturacion == (int)EstadoDocumento.enviadoSunat)
            //    {
            //        item.indicador_check = true;
            //        item.Dias = 0;
            //    }
            //}

            return lstfacturaElectronica;
        }


        #region Metodos de documentos electronicos

        //Facturacion
        public async Task<EFacturaElectronicaResponse> EviarFacturaElectronica(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();

            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.DatosGuiaTransportista = null;
            objdocumento.DatoAdicionales = new List<DatoAdicional>();
            objdocumento.Relacionados = new List<DocumentoRelacionado>();
            objdocumento.OtrosDocumentosRelacionados = new List<DocumentoRelacionado>();
            objdocumento.Discrepancias = new List<Discrepancia>();

            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);

            EFacturaElectronicaResponse response = await model.FacturaElectronica(objdocumento);

            return response;
        }


        public async Task<EFacturaElectronicaResponse> EviarNotaCredito(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
            EFacturaElectronicaResponse response = null;
            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.DatosGuiaTransportista = null;
            objdocumento.DatoAdicionales = new List<DatoAdicional>();
            objdocumento.Relacionados = new List<DocumentoRelacionado>();
            objdocumento.OtrosDocumentosRelacionados = new List<DocumentoRelacionado>();


            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);
            objdocumento.Discrepancias.Add(new Discrepancia
            {
                NroReferencia = Obe.numDocRef,
                Tipo = "01",
                Descripcion = "Ejemplo"
            });
            if (objdocumento.TipoDocumento == "07")
            {
                response = await model.NotaCreditoElectronica(objdocumento);
            }
            else
            {
                response = await model.NotaDebitoElectronica(objdocumento);
            }

            return response;
        }
        #endregion


        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intIndicador = 1;//Indicador 1 = Generar
            enableLoading(true);
            AnularDocumentoElectronico();
        }

        private async void AnularDocumentoElectronico()
        {

            //EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            //List<EDocumentoElectronicoBaja> objLista = new List<EDocumentoElectronicoBaja>();
            //int id = 1;
            //if (lstfacturaElectronica.Where(p => p.indicador_check).Any())
            //{
            //    foreach (var item in lstfacturaElectronica)
            //    {
            //        if (item.indicador_check)
            //        {
            //            objLista.Add(new EDocumentoElectronicoBaja
            //            {
            //                Id = id,
            //                TipoDocumento = item.tipoDocumento,
            //                MotivoBaja = "ANULACION DE FACTURA 1", //EJEMPLO;
            //                Serie = item.idDocumento.Split('-')[0].ToString(),
            //                Correlativo = item.idDocumento.Split('-')[1].ToString()
            //            });
            //            id++;
            //        }
            //    }
            //    response = await EnviarBajaDocumentoElectronico(objLista);
            //}
            //else
            //{
            //    MessageBox.Show("Por favor seleccione registro(s)", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //backgroundWorker1.RunWorkerAsync();
        }


        //Documento de baja
        //public async Task<EFacturaElectronicaResponse> EnviarBajaDocumentoElectronico(List<EDocumentoElectronicoBaja> mlisdet)
        //{
        //    DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
        //    ComunicacionBaja objBaja = new ComunicacionBaja();

        //    objBaja = FacturaElectronicaDto.DocumentoElectronicoBaja(mlisdet);

        //    EFacturaElectronicaResponse response = await model.DocumentoElectronicoBaja(objBaja);

        //    return response;
        //}



        //Envio de correo

        public void EnviarEmail()
        {
            string template = Convertir.CrearTemplateEmail();



            AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(template,
                                        Encoding.UTF8,
                                        MediaTypeNames.Text.Html);

            var pathResource = string.Format(@"{0}Utilities\ImagenFirma\", AppDomain.CurrentDomain.BaseDirectory);

            string resourceId = "01-FIRMA";
            string urlResource = string.Format("{0}{1}.jpg", pathResource, resourceId);
            LinkedResource linkedResource = new LinkedResource(urlResource, MediaTypeNames.Image.Jpeg);
            linkedResource.ContentId = resourceId;
            htmlView.LinkedResources.Add(linkedResource);


            Attachment Attach_ = new Attachment(@"D:\Factura Electronica\20109714039-07-F005-00000006.pdf");

            Email.From("SIDEPERU@SIDE.somee.com")
                           .To("jfernandez-20@hotmail.com") //jferna
                           .CarbonCopy("jfernandez-20@hotmail.com")
                           .Subject("Ejemplo")
                           .AlternateView(htmlView)
                           // .Attach(Attach_)
                           //.UsingStringTemplateText("Mensaje de Prueba")
                           .Send();
        }

        private void nuevoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Nuevo();
        }
        private void Nuevo()
        {
            try
            {
                FrmManteResumenDocumentos p = new FrmManteResumenDocumentos();
                p.MiEvento += new FrmManteResumenDocumentos.DelegadoMensaje(Agregar);
                p.Show();
                p.SetInsert();
                if (lstfacturaElectronica.Count > 0)
                {
                    int correlativo = lstfacturaElectronica.Max(ob => Convert.ToInt32(ob.IdDocumento.Substring(12))) + 1;
                    p.txtNumeroNic.Text = correlativo.ToString();
                }
                else
                {
                    p.txtNumeroNic.Text = "00001";
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpEstadoSunat_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpEstadoSunat.ContainsFocus)
                filtroBuscarGeneral();
        }
        private void filtroBuscarGeneral()
        {
            List<ESunatResumenDocumentosCab> listaTempEE = new List<ESunatResumenDocumentosCab>();
            listaTempEE = lstfacturaElectronica.Where(ob => ob.EstadoSunat == lkpEstadoSunat.Text).ToList();
            grdFacturaElectronica.DataSource = listaTempEE;

            if (lkpEstadoSunat.Text == "APROBADO" /*&& lkpEstadoEnvio.Text == "APROBADO"*/)
            {
                SunatToolStripMenuItem.Enabled = false;
            }
            else
            {
                SunatToolStripMenuItem.Enabled = true;
            }
            //}
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked == true)
            {
                cargar();
                lkpEstadoSunat.Properties.DataSource = 0;
            }
            else
            {
                cargaEstadoSunat();
                filtroBuscarGeneral();
            }
        }

        private void eliminarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ESunatResumenDocumentosCab Obe = (ESunatResumenDocumentosCab)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            if (Obe.EstadoResumen == 1)
            {
                XtraMessageBox.Show("El Resumen fue Enviado a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BVentas().eliminarResumenDocumentos(Obe);
                cargar();
                reload(0);
            }
        }

        private void exporteExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {

                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdFacturaElectronica.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdFacturaElectronica.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    sfdRuta.FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}