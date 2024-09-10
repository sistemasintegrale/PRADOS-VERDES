using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptEstadoCuentaXCuotas : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaXCuotas()
        {
            InitializeComponent();
        }

        public void cargar(List<EContrato> lista, string anio)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa+"- AÑO " + anio;
            xrlblTitulo1.Text = "LISTA DE ESTADO DE CUENTAS POR CUOTAS";


            this.DataSource = lista;           

            //Detalles
            lblNroContrato.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cntc_vnumero_contrato", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cntc_sfecha_contrato", "")});

            lblOrigenVenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strorigenventa", "")});

            lblNombreIEC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strNombreIEC", "")});

            lblFuneraria.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cntc_vnombre_comercial", "")});

            lblContratante.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strNombreContratante", "")});

            lblDNI.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cntc_vdni_contratante", "")});

            lblTipoSepultura.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strtiposepultura", "")});

            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strSituacion", "")});

            this.ShowPreview();
        }

    }
}
