using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using QRCoder;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptFacturaElectronico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFacturaElectronico()
        {
            InitializeComponent();
        }
        public void cargar(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet, DateTime? hora)
        {
            // Datos de cliente 
            lblHora.Text = hora != null ? hora.Value.ToString("dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture) : "";
            lblCliente.Text = Obe.nombreLegalReceptor.ToUpper();
            lblDireccionCliente.Text = Obe.direccionReceptor;

            if (Obe.direccionReceptor.Trim().Length < 75)
            {
                lblDireccionCliente.Text = Obe.direccionReceptor;
            }
            else
            {
                lblDireccionCliente.Text = Obe.direccionReceptor.Substring(0, 75);
            }


            int sobrante = Obe.direccionReceptor.Trim().Length - 75;
            if (sobrante > 0)
            {
                lblDireccionCliente2.Text = Obe.direccionReceptor.Substring(75);
            }


            lblRucCliente.Text = Obe.nroDocumentoReceptor;

            //Datos de emisor

            lblRuEmisor.Text = Obe.nroDocumentoEmisior;
            lblNombreEmisor.Text = "INVERSIONES Y DESARROLLO PRADOS VERDES S.A.C ";
            lblDireccion.Text = "Dirección: Av. Las Palmas N° 2020";
            lblDireccion1.Text = "Lima - Lima - Pachacamac";
            lblDireccion2.Text = "Telf. 675 4239 | 932 823 115 | 932 822 164";
            lblDireccion3.Text = "E-mail. info@parquesdelparaiso.com";

            //Datos del Documento

            lblFechaVencimiento.Text = Obe.fechaVencimiento;
            lblNroDocumento.Text = Obe.idDocumento;
            lblFechaEmision.Text = Obe.FEmisionPresentacion;
            lblGuiaRemision.Text = "";

            PictureBoxQR.Image = Convertir.GenerarQR(Convertir.GenerarCodigoQR(Obe));

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
                mon7.Text = "S/";
                mon8.Text = "S/";
                lblMoneda.Text = "SOLES";
            }
            else
            {
                Descripcion_moneda = " DOLARES AMERICANOS";
                mon1.Text = "US$";
                mon2.Text = "US$";
                mon3.Text = "US$";
                mon3.Text = "US$";
                mon4.Text = "US$";
                mon5.Text = "US$";
                mon6.Text = "US$";
                mon7.Text = "US$";
                mon8.Text = "US$";
                lblMoneda.Text = "DOLARES";
            }
            lblMontoLentra.Text = "SON: " + Convertir.ConvertNumeroEnLetras(Obe.TotalPrecioVenta.ToString()) + Descripcion_moneda;

            //Totalizacion de la Factura

            lblSubTotal.Text = Obe.TotalValorVenta.ToString();
            lblOpGravadas.Text = Obe.MontoGravadasIGV.ToString();
            lblOpExoneradas.Text = Obe.MontoExonerado.ToString();
            lblOpGratuito.Text = Obe.MontoGratuitoImpuesto.ToString();
            lblOpInafectadas.Text = Obe.MontoInafecto.ToString();
            lblDescuento.Text = Obe.MontoDescuentoGlobal.ToString();
            lblIgv.Text = Obe.totalIgv.ToString();
            lblTotal.Text = Obe.TotalPrecioVenta.ToString();

            //Detalle de la factura

            this.DataSource = mlisdet;

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cantidad", "")});

            lblmDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "descripcion", "")});

            lblmCaracteristicas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "caracteristicas", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "codigoItem", "")});

            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "ValorUnitarioItem", "{0:n2}")});

            lblValorTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "ValorVentaItem", "{0:n2}")});

            this.ShowPreview();
        }
    }
}
