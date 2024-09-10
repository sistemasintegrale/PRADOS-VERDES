using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptComprasDaotxProvCD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptComprasDaotxProvCD()
        {
            InitializeComponent();
        }

        public void Cargar(List<EComprasDaotDetalle> ListaCD, string nom_prov)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - A�O " + DateTime.Now.Year;
            lblTitulo1.Text = "DOCUMENTOS DE COMPRA DEL A�O " + Parametros.intEjercicio.ToString("n2");
            lblTitulo2.Text = "DEL PROVEEDOR " + nom_prov;

            this.DataSource = ListaCD;


            #region Detalle
            lblTipoDoc.DataBindings.Add(new XRBinding("Text", null, "tdocc_vabreviatura_tipo_doc"));
            lblNumDoc.DataBindings.Add(new XRBinding("Text", null, "doxpc_vnumero_doc"));
            lblFecha.DataBindings.Add(new XRBinding("Text", null, "doxpc_sfecha_doc", "{0:dd/MM/yyyy}"));
            lblMoneda.DataBindings.Add(new XRBinding("Text", null, "simboloMoneda"));
            lblMontoTotal.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_total_documento", "{0:n2}"));
            lblMontoS.DataBindings.Add(new XRBinding("Text", null, "valor_compra_soles", "{0:n2}"));
            lblMontoD.DataBindings.Add(new XRBinding("Text", null, "valor_compra_dolares_str", "{0:n2}"));
            lblTC.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_tipo_cambio", "{0:n4}"));
            #endregion

            #region ReportFooter
            lblMontoST.DataBindings.Add(new XRBinding("Text", null, "valor_compra_soles"));
            #endregion

            this.ShowPreview();
        }
    }
}
