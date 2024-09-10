using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class rptControlSepultura : DevExpress.XtraReports.UI.XtraReport
    {
        public rptControlSepultura()
        {
            InitializeComponent();
        }

        public void cargar(List<EEspaciosDet> lista, string anio)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa+"- AÑO " + anio;
            xrlblTitulo1.Text = "CONTROL DE SEPULTURA";

            //System.Text.StringBuilder lineas = new System.Text.StringBuilder();
            //lineas.Append("[");
            //if (situacion == "Todos")
            //    lineas.Append("ACTIVOS E INACTIVOS");
            //else
            //    lineas.Append(situacion);
            //lineas.Append("/GIRO: ");
            //lineas.Append(giro.ToUpper());
            //lineas.Append("/VENDEDOR: ");
            //lineas.Append(vendedor.ToUpper());
            //lineas.Append("]");
            //xrlblTitulo2.Text = lineas.ToString();

            this.DataSource = lista;           

            //Detalles
            lblPlataforma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strplataforma", "")});

            lblManzana.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strmanzana", "")});

            lblNivel.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "espad_vnivel", "")});

            lblSepultura.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strsepultura", "")});

            lblEstado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strestado", "")});

            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strSituacion", "")});

            lblNumContrato.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "NumContrato", "")});

            lblContratante.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "NomContratante", "")});

            this.ShowPreview();
        }

    }
}
