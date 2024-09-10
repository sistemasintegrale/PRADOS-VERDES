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
    public partial class rptGuiaRemision : DevExpress.XtraReports.UI.XtraReport 
    {
        public rptGuiaRemision()
        {
            InitializeComponent();
        }
        public void cargar(EGuiaRemision oBe, List<EGuiaRemisionDet> mlisdetalle, string Departamento, string Provincia, string Distrito)
        {
          
            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura

            lblPuntoPartida.Text = oBe.remic_vdireccion_procedencia;
            lblpuntollegada.Text = oBe.remic_vdireccion_destinatario;
            lblcodCliente.Text = oBe.cliec_vcod_cliente;
            lbldestinatario.Text = oBe.remic_vnombre_destinatario;
            lbldireccion.Text = oBe.remic_vdireccion_destinatario;
            lblRUCdOCiDEN.Text = oBe.remic_vruc_destinatario;
            lblplazoPago.Text = oBe.strFormaPago;
            lblfechaEmision.Text = oBe.remic_sfecha_remision.ToString();
            lblReferencia.Text = oBe.remic_vreferencia;
            lblVendedor.Text = oBe.perc_vapellidos_nombres;
            lblTransportista.Text = oBe.strTransportista;
            LBLRUCC.Text = oBe.remic_vruc_transportista;

            lbltotal.Text = decimal.Round(mlisdetalle.Sum(ob => ob.dremc_nMonto_Total),2).ToString();
            //lblFecha.Text = oBe.remic_sfecha_remision.ToShortDateString();
            //lblDomicilioPartida.Text = oBe.remic_vdireccion_procedencia;
            //lblDomicilioLlegada.Text = oBe.remic_vdireccion_destinatario;
            //lblrazonSocial.Text = oBe.remic_vnombre_destinatario;
            //lblNroGuia.Text = oBe.remic_vnumero_remision;
            //lblRuc.Text = oBe.remic_vruc_destinatario;
            //if (Distrito != "")
            //{
            //    lblubigeo.Text = Departamento + "-" + Provincia + "-" + Distrito;
            //}
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
            new XRBinding("Text", mlisdetalle , "dremc_vcantidad_producto", "")
            });
            lblpLista.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dremc_nprecio_lista", "")
            });
            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dremc_nPrecio_Unitario", "")
            });
            lblPrecioVenta.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dremc_nMonto_Total", "")
            });
            this.ShowPreview();

           
        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

       
    }
}
