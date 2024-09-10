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
    public partial class rptBoletaChica : DevExpress.XtraReports.UI.XtraReport 
    {
        public rptBoletaChica()
        {
            InitializeComponent();
        }
        public void cargar(EBoletaCab oBe, List<EBoletaDet> mlisdetalle, string total, string Departamento, string Provincia, string Distrito)
        {
            //if (Distrito != "")
            //{
            //    lblubigeo.Text = Departamento + "-" + Provincia + "-" + Distrito;
            //}

            string Str_moneda = "";
            string Str_moneda_vdes = "";
            if (oBe.tablc_iid_tipo_moneda == 3)
            {
                Str_moneda = "S/. ";
                Str_moneda_vdes = "NUEVOS SOLES";
            }
            else
            {
                Str_moneda = "$. ";
                Str_moneda_vdes = "DOLARES AMERICANOS";
            }
            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura
            lblFecha.Text = oBe.bovc_sfecha_boleta.ToShortDateString();
            lblDesCliente.Text = oBe.cliec_vnombre_cliente;
            lblDireccion.Text = oBe.cliec_vdireccion_cliente;
            lblNroFactura.Text = oBe.bovc_vnumero_boleta;
            lblGuiaRemision.Text = oBe.remic_vnumero_remision;
            
            #endregion

            #region Pie de la Factura
            lblTotalFactura.Text = Str_moneda+ oBe.bovc_nmonto_total.ToString("N2");
            lblMontoletras.Text = "SON "+total + " " + Str_moneda_vdes+".";
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
            lblPrecioVenta.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "bovd_nprecio_total_item", "{0:n2}")
            });
            lblUme.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strDesUM", "")
            });
             lblDesc.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "bovd_nporcentaje_descuento_item", "{0:n2}")
            });
            
                

            this.ShowPreview();
           
        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

       
    }
}
