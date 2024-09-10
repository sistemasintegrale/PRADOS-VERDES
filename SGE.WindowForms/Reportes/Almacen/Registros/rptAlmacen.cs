using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Reportes.Almacen.Registros
{
    public partial class rptAlmacen : DevExpress.XtraReports.UI.XtraReport
    {
        public rptAlmacen()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EAlmacen> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;
            this.DataSource = lista;

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "almac_iid_almacen", "{0:00}")});
          
            lbldescripccion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "almac_vdescripcion", "")});

            lbldireccion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "almac_vdireccion", "")});

            this.ShowPreview();

        }
    }
}