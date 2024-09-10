using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
//using OpenInvoicePeru.Comun.Dto.Intercambio;
//using OpenInvoicePeru.Comun.Dto.Modelos;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Utilities;
using SGE.WindowForms.Ventas.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIDE.COMUN.DTO.Modelos;
using SIDE.COMUN.DTO.Intercambio;
namespace SGE.WindowForms.Ventas.Factura_Electronica
{
    public partial class frm03ResumenAnulados : DevExpress.XtraEditors.XtraForm
    {
        private List<ESunatResumenDet> lstfacturaElectronica = new List<ESunatResumenDet>();
        private int xposition;
        private List<string> mensajeRespuesta = new List<string>();
        private int intIndicador = 0;

        public frm03ResumenAnulados()
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
                filtroBuscarGeneral();
            }
        }

        private void cargar()
        {

            grdFacturaElectronica.DataSource = listaFacturaElectronica();
            viewFacturaElectronica.Focus();

        }
        //private void cargaLkpTipoDoc()
        //{
        //    List<string> listaTD = new List<string>();
        //    listaTD = lstfacturaElectronica.OrderBy(ord => ord.StrTipoDoc).Select(sel => sel.StrTipoDoc).Distinct().ToList();
        //    listaTD.Add("TODOS");
        //    lkpTipoDoc.Properties.DataSource = listaTD;
        //    lkpTipoDoc.ItemIndex = listaTD.Count - 1;
        //}
        public class TipoEnvioSunat
        {
            public int intCodigo { get; set; }
            public string strTipo { get; set; }
        }
        private void cargaEstadoSunat()
        {
            List<TipoEstadoSunat> lst = new List<TipoEstadoSunat>();
            lst.Add(new TipoEstadoSunat { intCodigo = 1, strTipo = "ENVIADO SUNAT" });
            lst.Add(new TipoEstadoSunat { intCodigo = 2, strTipo = "APROBADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 3, strTipo = "RECHAZADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 4, strTipo = "PENDIENTES POR ENVIAR" });
            //lst.Add(new TipoEstadoSunat { intCodigo = 0, strTipo = "PENDIENTES POR ENVIAR" });
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



        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
        }
        private void imprimir()
        {
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            Obe = (EFacturaVentaElectronica)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            if (Obe != null)
            {
                mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);
                mlisdet.ForEach(x =>
                {
                    x.descripcion = "";
                    string[] partes = partes = x.descripcionO.Split('@');
                    for (int i = 0; i < partes.Length; i++)
                    {
                        if (partes[i].Trim() != "")
                            x.descripcion = x.descripcion + " ".ToString() + partes[i] + " ";
                    }
                });
                rptFacturaElectronico rptFcatura = new rptFacturaElectronico();
                rptBoletaElectronico rptBoleta = new rptBoletaElectronico();
                if (Obe.tipoDocumento == "01")
                {
                    //    List<EFacturaVentaCab> lstFV = new BVentas().listarfacturaVenta().Where(x => x.facv_icod_fac_venta == Obe.doc_icod_documento).ToList();
                    //    if (lstFV.Count > 0)
                    //    {
                    //        Obe.orpc_vnumero_orden_repara = lstFV[0].orpc_vnumero_orden_repara;
                    //        Obe.facv_vOrden_servicio = lstFV[0].facv_vOrden_servicio;
                    //        Obe.strMarca = lstFV[0].strMarca;
                    //        Obe.strModelo = lstFV[0].strModelo;
                    //        Obe.facv_vcolor = lstFV[0].facv_vcolor;
                    //        Obe.vehd_vplaca = lstFV[0].vehd_vplaca;
                    //        Obe.anioc_icod_anio = lstFV[0].anioc_icod_anio;
                    //        Obe.facv_vkilometraje = lstFV[0].facv_vkilometraje;
                    //        Obe.prsc_vnumero_presupuesto = lstFV[0].prsc_vnumero_presupuesto;
                    //    }

                    //    rptFcatura.cargar(Obe, mlisdet);
                    //    rptFcatura.ShowPreview();
                    //}
                    //if (Obe.tipoDocumento == "03")
                    //{
                    //    List<EBoletaVentaCab> lstBV = new BVentas().listarBoletaVenta().Where(x => x.fbolv_icod_bol_venta == Obe.doc_icod_documento).ToList();
                    //    if (lstBV.Count > 0)
                    //    {
                    //        Obe.orpc_vnumero_orden_repara = lstBV[0].orpc_vnumero_orden_repara;
                    //        Obe.strMarca = lstBV[0].strMarca;
                    //        Obe.strModelo = lstBV[0].strModelo;
                    //        Obe.facv_vcolor = lstBV[0].fbolv_vcolor;
                    //        Obe.vehd_vplaca = lstBV[0].vehd_vplaca;
                    //        Obe.anioc_icod_anio = lstBV[0].anioc_icod_anio;
                    //        Obe.facv_vkilometraje = lstBV[0].fbolv_vkilometraje;
                    //        Obe.prsc_vnumero_presupuesto = lstBV[0].prsc_vnumero_presupuesto;
                    //    }
                    //    rptBoleta.cargar(Obe, mlisdet);
                    //    rptBoleta.ShowPreview();
                }

            }
        }


        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intIndicador = 1;//Indicador 1 = Generar
            enableLoading(true);
            //FrmManteEnviarResumenCab frm = new FrmManteEnviarResumenCab();
            //frm.lstSunatResuemnDet = lstfacturaElectronica.Where(p => p.indicador_check == true).ToList();
            //frm.Show();
            EnviarSunat();

        }

        private async void EnviarSunat()
        {
            //EResumenResponse response = new EResumenResponse();
            //EnviarDocumentoResponse responseConsulta = new EnviarDocumentoResponse();
            //List<ESunatResumenDet> mlisdet = lstfacturaElectronica.Where(p => p.indicador_check == true).ToList();
            //if (mlisdet.Any())
            //{
            //    response = await eviarsunatresumen(mlisdet);
            //    mensajeRespuesta = new List<string>();
            //    foreach (var item in mlisdet)
            //    {
            //        Guardar Mensaje de respueta
            //        if (response.Exito)
            //        {
            //            this.mensajeRespuesta.Add($"{item.IdDocumento}; se anulo correctamente.");
            //        }
            //        else
            //        {
            //            this.mensajeRespuesta.Add($"{item.IdDocumento}; ocurrio un error.");
            //        }
            //        response.IdItems += $"{item.IdItems},";
            //    }

            //    new BVentas().insertarResumenDiarioResponse(response); FALTA

            //     Consulta de tiket
            //    ConsultaTicketRequest objConsulta = new ConsultaTicketRequest();
            //    objConsulta.NroTicket = response.NroTicket;
            //    objConsulta.IdDocumento = response.IdDocumento;
            //    responseConsulta = await ConsultarTiket(objConsulta);
            //}
            //else
            //{
            //    MessageBox.Show("Por favor seleccione registro(s)", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //backgroundWorker1.RunWorkerAsync();
        }
        //public async Task<EResumenResponse> eviarsunatresumen(List<ESunatResumenDet> mlisdet)
        //{
        //    DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
        //    ResumenDiarioNuevo objdocumento = new ResumenDiarioNuevo();
        //    objdocumento = FacturaElectronicaDto.DocumentoElectronicoResumen(mlisdet);
        //    EResumenResponse response = await model.EnviarResumen(objdocumento);
        //    return response;
        //}

        //public async Task<EnviarDocumentoResponse> ConsultarTiket(ConsultaTicketRequest objConsulta)
        //{
        //    DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
        //    EnviarDocumentoResponse response = await model.ConsultaTiket(objConsulta);
        //    return response;
        //}

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

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.imprimir();
        }



        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.imprimir();
        }

        #endregion Marca



        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.imprimir();
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            this.BuscarCriterio();
        }

        private void BuscarCriterio()
        {
            //grdFacturaElectronica.DataSource = lstfacturaElectronica.Where(obj => obj.idDocumento.Contains(txtpresupuesto.Text) &&
            //                                    obj.nombreLegalReceptor.ToUpper().Contains(txtCliente.Text.ToUpper())).ToList();
            //if (lkpTipoDoc.Text == "TODOS" && txtpresupuesto.Text.Trim() == "" && txtCliente.Text.TrimStart().TrimEnd() == "")
            //{
            //    grdFacturaElectronica.DataSource = lstfacturaElectronica;
            //}
            //else
            //{
            //    List<EFacturaVentaElectronica> listaTempDxC = new List<EFacturaVentaElectronica>();
            //    if (lkpTipoDoc.Text != "TODOS")
            //        listaTempDxC = lstfacturaElectronica.Where(ob => ob.StrTipoDoc == lkpTipoDoc.Text).ToList();
            //    else
            //        listaTempDxC = lstfacturaElectronica;
            //    listaTempDxC = listaTempDxC.Where(ob => ob.idDocumento.Contains(txtpresupuesto.Text.TrimStart().TrimEnd())).ToList();
            //    listaTempDxC = listaTempDxC.Where(ob => ob.nombreLegalReceptor.Contains(txtCliente.Text.TrimStart().TrimEnd())).ToList();
            //    grdFacturaElectronica.DataSource = listaTempDxC;
            //}
        }

        private void confirmarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }



        private void grdFacturaElectronica_Click(object sender, EventArgs e)
        {
        }

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

        private List<ESunatResumenDet> listaFacturaElectronica()
        {
            DateTime FechaActual;
            List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
            lstfacturaElectronica = new List<ESunatResumenDet>();
            //lstfacturaElectronica = new BVentas().listarSunatResumenDet();
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


        private void filtroBuscarGeneral()
        {
            //List<EFacturaVentaElectronica> listaTempEE = new List<EFacturaVentaElectronica>();
            //listaTempEE = lstfacturaElectronica.Where(ob => /*ob.EstadoEnvio == lkpEstadoEnvio.Text &&*/ ob.EstadoSunat == lkpEstadoSunat.Text).ToList();
            //grdFacturaElectronica.DataSource = listaTempEE;

            //if (lkpEstadoSunat.Text == "APROBADO" /*&& lkpEstadoEnvio.Text == "APROBADO"*/)
            //{
            //    SunatToolStripMenuItem.Enabled = false;
            //}
            //else
            //{
            //    SunatToolStripMenuItem.Enabled = true;
            //}
            ////}
        }



        private void lkpEstadoSunat_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpEstadoSunat.ContainsFocus)
                filtroBuscarGeneral();
        }

        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            ESunatResumenDet Obe = (ESunatResumenDet)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            Obe.indicador_check = true;
            //if (Obe.EstadoSunat == "ENVIADO SUNAT")
            //{
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowEdit = false;
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowFocus = false;
            //}
            //else
            //{
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowEdit = true;
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowFocus = true;
            //}
        }

        private void viewFacturaElectronica_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //DateTime FechaActual;
            //GridView View = sender as GridView;
            //if (e.RowHandle >= 0)
            //{

            //    string FechaEmision = View.GetRowCellDisplayText(e.RowHandle, View.Columns["FEmisionPresentacion"]);
            //    string strEnvioSunat = View.GetRowCellDisplayText(e.RowHandle, View.Columns["EstadoSunat"]);
            //    if (/*String.Compare(strEnvio,"ENVIADO")==0 &&*/ String.Compare(strEnvioSunat, "APROBADO") == 0)
            //    {
            //        e.Appearance.BackColor = Color.LightGreen;

            //    }
            //    if (strEnvioSunat == "APROBADO")
            //    {
            //        SunatToolStripMenuItem.Enabled = false;
            //    }
            //    else
            //    {
            //        SunatToolStripMenuItem.Enabled = true;
            //        FechaActual = DateTime.Now;
            //        TimeSpan ts = FechaActual - Convert.ToDateTime(FechaEmision);
            //        int Diferencia = ts.Days;

            //        if (Diferencia > 7)
            //        {
            //            e.Appearance.BackColor = Color.Salmon;
            //        }
            //    }


            //}
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkTodos.Checked == true)
            //{
            //    cargar();
            //    lkpEstadoSunat.Properties.DataSource = 0;
            //}
            //else
            //{
            //    cargaEstadoSunat();
            //    filtroBuscarGeneral();
            //}
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

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

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
    }
}