using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Registros
{
    public partial class rpt02Analiticas : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt02Analiticas()
        {
            InitializeComponent();
        }

        public void cargar(List<ETablaRegistro> lista)
        {
            lblEmpresa.Text = String.Format("{0} - A�O {1}", Modules.Valores.strNombreEmpresa, Parametros.intEjercicio);
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "LISTADO DE ANAL�TICAS";
            lblTitulo2.Text = "";
            this.DataSource = lista;
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tarec_icorrelativo_registro", "{0:00}")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tarec_vdescripcion", "")});
            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strEstado", "")});
            this.ShowPreview();
        }

    }
}
