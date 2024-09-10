using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;


namespace SGE.WindowForms.Reportes.Almacen.Registros
{
    public partial class rptNotaSalida : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaSalida()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<ENotaSalidaDetalle> lstDetalle, ENotaSalida Obe)
        {
            /*------------------------------------------------------*/
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;
            /*------------------------------------------------------*/
            lblMotivo.Text = Obe.strMotivo.ToUpper();
            lblReferencia.Text = Obe.nsalc_referencia;
            lblObservacion.Text = Obe.nsalc_observaciones;
            /*------------------------------------------------------*/
            this.DataSource = lstDetalle;

            lblItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "dnsalc_nro_item", "{0:000}")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strCodeProducto", "")});           

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strProducto", "")});

            lblUM.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strUnidadMedida", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "dnsalc_cantidad", "{0:N2}")});

            this.ShowPreview();

        }
    }
}
