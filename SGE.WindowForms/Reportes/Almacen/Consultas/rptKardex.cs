using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptKardex : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKardex()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EKardex> detalle,string strTituloAlmacen)
        {

            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;
            lblTituloAlmacen.Text = strTituloAlmacen;

            this.DataSource = detalle;
            #region Detalle
            lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strDocumento", "")});
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_fecha_movimiento", "{0:dd/MM/yyyy}")});
            lblmotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strMotivo", "")});
            lblobservacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_observaciones", "")});
            lblreferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_beneficiario", "")});
            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblIngreso", "")});
            lblsalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblSalida", "")});
            lblSaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblSaldo", "")});
            #endregion
            #region Footer
            lblIngresoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblIngreso", "")});
            lblSalidaTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblSalida", "")});
            #endregion
            this.ShowPreview();

        }
    }
}
