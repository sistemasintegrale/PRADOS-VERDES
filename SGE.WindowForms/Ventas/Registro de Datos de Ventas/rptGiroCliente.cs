using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;


namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptGiroCliente : DevExpress.XtraReports.UI.XtraReport
    {
        public rptGiroCliente()
        {
            InitializeComponent();
        }

        public void cargar(List<EGiro> lista, string anio)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + " - A�O " + anio;
            

            this.DataSource = lista;           

            //Detalles
            lblCliente.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "giroc_iid_giro", "")});

            lblDireccion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "giroc_vnombre_giro", "")});

            lblabrevia.DataBindings.AddRange (new XRBinding[]{
            new XRBinding("Text",lista,"indicador_expo_nextel","")});

            this.ShowPreview();
        }

    }
}
