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
    public partial class rptMovimientosProductos : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMovimientosProductos()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EKardex> detalle)
        {

            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = detalle;

            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strDocumento", "")});

            lblMotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strMotivo", "")});

            lblAlmacen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strAlmacen", "")});
            
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strCodProducto", "")});
            
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strProducto", "")});

            lblObservacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_observaciones", "")});
            
            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_beneficiario", "")});
            
            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblIngreso", "")});
            
            lblSalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblSalida", "")});
          
            this.ShowPreview();

        }
    }
}
