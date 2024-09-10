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
    public partial class rptBoleta : DevExpress.XtraReports.UI.XtraReport 
    {
        public rptBoleta()
        {
            InitializeComponent();
        }
        public void cargar(EBoletaCab oBe,List<EBoletaDet> mlisdetalle,string total)
        {            
            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura
            lblFecha.Text = oBe.bovc_sfecha_boleta.ToShortDateString();
            //lblCodCliente.Text = oBe.strCodCliente;
            lblDesCliente.Text = oBe.strDesCliente;
            lblDireccion.Text = oBe.bovc_vdireccion_cliente;
            lblDistrito.Text = oBe.strDistritoCliente;
            lblKm.Text = oBe.bovc_vkilometraje;
            //lblRuc.Text = oBe.favc_vruc;
            //lblPlaca.Text = oBe.strPlaca;
            lblTelefono.Text = oBe.strTelefonoCliente;
            //lblAnio.Text = (oBe.intAnioVehiculo == 0) ? "" : oBe.intAnioVehiculo.ToString();
            //lblMarca.Text = oBe.strMarca;
            lblNroFactura.Text = oBe.bovc_vnumero_boleta;
            lblObservaciones.Text = oBe.bovc_vobservacion;
            #endregion

            #region Pie de la Factura
            lblImpBrutoTotal.Text = oBe.bovc_nmonto_neto.ToString("N2");
            lblDescuentoTotal.Text = oBe.nmonto_nDescuento.ToString("N2");
            lblSubTotal.Text = oBe.bovc_nmonto_neto.ToString("N2");
            lblIGV.Text = oBe.bovc_nmonto_imp.ToString("N2");
            lblTotalFactura.Text = oBe.bovc_nmonto_total.ToString("N2");
            lblMontoletras.Text = total + " NUEVOS SOLES.";

            lblFormaPago.Text = oBe.strFormaPago;
            lblFechaVencimiento.Text = oBe.bovc_sfecha_vencim_boleta.ToShortDateString();
            #endregion

            #region Totales en Servicios y Repuestos
            decimal dcmlServicios = Convert.ToDecimal(mlisdetalle.Where(x => x.intClasificacionProducto == 2).ToList().Sum(d => d.bovd_nprecio_total_item));
            decimal dcmlRepuestos = Convert.ToDecimal(mlisdetalle.Where(x => x.intClasificacionProducto != 2).ToList().Sum(d => d.bovd_nprecio_total_item));

            lblTotalServicios.Text = dcmlServicios.ToString("N2");
            lblTotalRepuestos.Text = dcmlRepuestos.ToString("N2");
            #endregion

            lblCodigo.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strCodProducto", "")
            });
            
            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strDesProducto", "")
            });
            lblCantidad.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "bovd_ncantidad", "{0:n2}")
            });
            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "bovd_nprecio_unitario_item", "{0:n2}")
            });
            lblMontoBruto.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "bovd_nprecio_total_item", "{0:n2}")
            });
            lblDescuento.DataBindings.Add(new XRBinding("Text", mlisdetalle, "descuento", "{0:n}"));
            lblPrecioVenta.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "bovd_nprecio_total_item", "{0:n2}")
            });
     
            this.ShowPreview();
           
        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

       
    }
}
