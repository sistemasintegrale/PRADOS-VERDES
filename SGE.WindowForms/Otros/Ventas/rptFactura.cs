using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class rptFactura : DevExpress.XtraReports.UI.XtraReport
    {        
        public rptFactura()
        {
            InitializeComponent();
        }
        public void cargar(EFacturaCab oBe, List<EFacturaDet> mlisdetalle, string total, decimal monto_toal_doc)
        {            
            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura
            lblFecha.Text = oBe.favc_sfecha_factura.ToShortDateString();
            //lblCodCliente.Text = oBe.strCodCliente;
            lblDesCliente.Text = oBe.strDesCliente;
            lblDireccion.Text = oBe.favc_vdireccion_cliente;
            lblDistrito.Text = oBe.strDistritoCliente;
            lblKm.Text = oBe.favc_vkilometraje;
            lblRuc.Text = oBe.favc_vruc;
            //lblPlaca.Text = oBe.strPlaca;
            lblTelefono.Text = oBe.strTelefonoCliente;
            //lblAnio.Text = (oBe.intAnioVehiculo == 0) ? "" : oBe.intAnioVehiculo.ToString();

            lblNroFactura.Text = oBe.favc_vnumero_factura;
            lblObservaciones.Text = oBe.favc_vobservacion;
            #endregion

            #region Pie de la Factura
            lblImpBrutoTotal.Text = monto_toal_doc.ToString("N2");
            lblDescuentoTotal.Text = oBe.nmonto_nDescuento.ToString("N2");
            lblSubTotal.Text = oBe.favc_nmonto_neto.ToString("N2");
            lblIGV.Text = oBe.favc_nmonto_imp.ToString("N2");
            lblTotalFactura.Text = oBe.favc_nmonto_total.ToString("N2");
            lblMontoletras.Text = total + " NUEVOS SOLES.";

            lblFormaPago.Text = oBe.strFormaPago;
            lblFechaVencimiento.Text = oBe.favc_sfecha_vencim_factura.ToShortDateString();
            #endregion

            #region Totales en Servicios y Repuestos
            decimal dcmlServicios = Convert.ToDecimal(mlisdetalle.Where(x => x.intClasificacionProducto == 2).ToList().Sum(d => d.favd_nprecio_total_item));
            decimal dcmlRepuestos = Convert.ToDecimal(mlisdetalle.Where(x => x.intClasificacionProducto != 2).ToList().Sum(d => d.favd_nprecio_total_item));

            lblTotalServicios.Text = dcmlServicios.ToString("N2");
            lblTotalRepuestos.Text = dcmlRepuestos.ToString("N2");
            #endregion

            lblCodigo.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strCodProducto", "")
            });
            
            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_mostrarobservaciones", "")
            });
            lblCantidad.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_ncantidad", "{0:n2}")
            });
            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_nprecio_unitario_item", "{0:n2}")
            });
            lblMontoBruto.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_nprecio_total_item", "{0:n2}")
            });
            lblDescuento.DataBindings.Add(new XRBinding("Text", mlisdetalle, "descuento", "{0:n}"));
            lblPrecioVenta.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_nprecio_total_item", "{0:n2}")
            });
     
            this.ShowPreview();
           
        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

       
    }
}
