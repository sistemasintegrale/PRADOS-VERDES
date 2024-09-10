using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;


namespace SGE.WindowForms.Otros.Operaciones.Reportes
{
    public partial class rptRequerimientoMateriales : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRequerimientoMateriales()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, List<ERequerimientoMaterialesDetalle> lstDetalle, ERequerimientoMateriales Obe)
        {
            /*------------------------------------------------------*/
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "VENTAS";
            lblTitulo1.Text = strTitulo1;
            //lblTitulo2.Text = strTitulo2;
            /*------------------------------------------------------*/
            lblTipo.Text = Obe.Tipo;
            lblProyecto.Text = string.Format("{0:00000}", Obe.pryc_icod_proyecto);
            lblHojaCostos.Text = Obe.NumHojaCosto;
            lblObservaciones.Text = Obe.rqmc_vdescripcion;
            /*------------------------------------------------------*/
            this.DataSource = lstDetalle;
            
            lblItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "rqmd_vcodigo_item_requerim", "{0:000}")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "DescripcionRubro", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "Cantidad", "{0:N2}")});


            lblUM.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "Medida", "")});

            lblRubro.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "CodigoRubro", "")});  

            this.ShowPreview();

        }

        private void rptRequerimientoMateriales_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
