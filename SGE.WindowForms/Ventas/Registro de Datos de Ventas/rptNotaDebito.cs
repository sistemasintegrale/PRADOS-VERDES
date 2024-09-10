using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptNotaDebito : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaDebito()
        {
            InitializeComponent();
        }
        public void cargar(ENotaDebito oBe,List<ENotaDebitoDet> mlisdetalle,string total,string Departamento, string Provincia, string Distrito)
        {            
            this.DataSource = mlisdetalle;

            #region Cabecera de la Factura
            string str_moneda = "";
            string str_moneda_letras = "";
            if (oBe.tablc_iid_tipo_moneda == 3)
            {
                str_moneda = "S/. ";
                str_moneda_letras = " NUEVOS SOLES.";
            }
            else
            {
                str_moneda = "$. ";
                str_moneda_letras = " DOLARES AMERICANOS.";
            }
            string tipo_doc="";
            if (oBe.tdocc_icod_tipo_doc == 9)
            {
                tipo_doc = "BOV ";
            }
            else
            {
                tipo_doc = "FAV ";
            }

            lblFecha.Text = oBe.ndebc_sfecha_debito.ToShortDateString();
            lblDesCliente.Text = oBe.strDesCliente;
            //lblDireccion.Text = oBe.favc_vdireccion_cliente;
            lblreferencia.Text = tipo_doc + oBe.ndebc_vnumero_documento;
            //if (Distrito != "")
            //{
            //    lblubigeo.Text = Departamento + "-" + Provincia + "-" + Distrito;
            //}
            lblRuc.Text = oBe.strRuc;

            lblporcentajeIgv.Text = oBe.ndebc_npor_imp_igv.ToString("N2") + " %";
            lblNroFactura.Text = oBe.ndebc_vnumero_debito;
            //lblObservaciones1.Text = oBe.favc_vobservacion;
            #endregion

            #region Pie de la Factura
            lblSubTotal.Text = str_moneda + oBe.ndebc_nmonto_neto.ToString("N2");
            lblIGV.Text = str_moneda + oBe.ndebc_npor_imp_igv.ToString("N2");
            lblTotalFactura.Text = str_moneda + oBe.ndebc_nmonto_total.ToString("N2");
     
            //lblMontoletras.Text = total + str_moneda_letras;
            #endregion


            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strDesProducto", "")
            });
           
            lblMontoBruto.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dcrec_vmonto_item", "")
            });


            lblMontoletras.Text = "SON: "+total;


            this.ShowPreview();
           
        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

       
    }
}
