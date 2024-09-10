using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class rptLineaProducto : DevExpress.XtraReports.UI.XtraReport
    {
        public rptLineaProducto()
        {
            InitializeComponent();
        }
        public void cargar(List<ECategoriaFamilia> lstDetalle, EFamiliaCab Obe)
        {
            /*------------------------------------------------------*/
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = "LISTA DE LINEAS DE PRODUCTOS";
            lblTitulo2.Text = "";
            /*------------------------------------------------------*/
            //lblTipo.Text = Obe.strTipoInventario;
            //lblObservacion.Text = Obe.invc_vobservaciones;
            /*------------------------------------------------------*/
            this.DataSource = lstDetalle;
            //this.DataSource = listfamilia;
            //this.DataSource = listfamiliaDet;

            GroupHeader2.GroupFields.AddRange(new GroupField[] { new GroupField("catf_vdescripcion") });
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("famic_vdescripcion") });
            /*------------------------------------------------------*/

            lblFamilia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "catf_vdescripcion", "")});
            
            /*------------------------------------------------------*/

            lblSubFamilia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "famic_vdescripcion", "")});
            /*------------------------------------------------------*/

           

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "famid_vdescripcion", "")});

           

         

            this.ShowPreview();
        }

    }
}
