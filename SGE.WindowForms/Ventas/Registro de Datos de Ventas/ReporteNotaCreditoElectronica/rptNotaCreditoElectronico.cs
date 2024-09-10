using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptNotaCreditoElectronico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaCreditoElectronico()
        {
            InitializeComponent();
        }
        public void cargar(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            /*-------------------------------------*/

            /*
             * Datos de cliente 
            */

            lblCliente.Text = Obe.nombreLegalReceptor.ToUpper();
            lblDireccionCliente.Text = Obe.direccionReceptor;

            lblRucCliente.Text = Obe.nroDocumentoReceptor;
            lblFechaEmision.Text = Obe.FEmisionPresentacion;
            lblGuiaRemision.Text = "";
            //lblMoneda.Text = Obe.moneda;
            //lblTipoOperacion.Text = Obe.tipoOperacion;


            //string ruta = AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug\", "");

            //string RutaArchivoLogo = ruta + @"Resources\NOVAGLASS.png";

            /*
             * Datos de emisor
             */
            lblRuEmisor.Text = Obe.nroDocumentoEmisior;
            //lblNombreEmisor.Text = Obe.nombreComercialEmisor;
            lblNroDocumento.Text = Obe.idDocumento;
            //lblDireccionCliente.Text = Obe.direccionEmisor;
            //lblDireccionEmisor2.Text = Obe.departamentoEmisor;
            //lblDireccionEmisor3.Text = Obe.provinciaEmisor + " - " + Obe.distritoEmisor;
            //PictureBoxLogo.ImageUrl = RutaArchivoLogo;
            //lblGuiaRemision.Text = nomLineaServicio;
            lblFacturaReferencia.Text = Obe.NroDocqModifica.Insert(4,"-");
            PictureBoxQR.Image = Convertir.GenerarQR(Convertir.GenerarCodigoQR(Obe));
            lblMotivo.Text = Obe.DescripMotivoNota;
            string Descripcion_moneda = "";
            if (Obe.moneda == "PEN")
            {
                Descripcion_moneda = " SOLES";
                mon1.Text = "S/";
                mon2.Text = "S/";
                mon3.Text = "S/";
                mon4.Text = "S/";
                mon5.Text = "S/";
                mon6.Text = "S/";
                lblMoneda.Text = "SOLES";
            }
            else
            {
                Descripcion_moneda = " DOLARES AMERICANOS";
                mon1.Text = "$";
                mon2.Text = "$";
                mon3.Text = "$";
                mon4.Text = "$";
                mon5.Text = "$";
                mon6.Text = "$";
                lblMoneda.Text = "DOLARES";
            }
            lblMontoLentra.Text = "SON: " + Convertir.ConvertNumeroEnLetras(Obe.TotalPrecioVenta.ToString()) + Descripcion_moneda;


            lblSubTotal.Text = (Obe.TotalValorVenta ).ToString();
            lblOpGravadasIgv.Text = Obe.MontoGravadasIGV.ToString();
            lblOpExoneradas.Text = Obe.MontoExonerado.ToString();
            lblOpGratuito.Text = Obe.MontoGratuitoImpuesto.ToString();
            lblOpInafectadas.Text = (Obe.MontoInafecto).ToString();
            lblIgv.Text = Obe.totalIgv.ToString();
            lblTotal.Text = Obe.TotalPrecioVenta.ToString();

            this.DataSource = mlisdet;


            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cantidad", "")});

            lblmDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "descripcion", "")});

            lblmCaracteristicas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "favd_nobservaciones", "")});

            lblUnidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "unidadMedida", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "codigoItem", "")});

            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "ValorUnitarioItem", "{0:n2}")});


            lblValorTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "ValorVentaItem", "{0:n2}")});

            //lblImporteDesc.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", mlisdet, "descuento", "")});

            //lblImporteNeto.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", mlisdet, "impuesto", "{0:n2}")});

        

            this.ShowPreview();
        }


    }
}
