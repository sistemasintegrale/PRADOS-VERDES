using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class rptEstadoCuentaClienteLista : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaClienteLista()
        {
            InitializeComponent();
        }

        public void cargar(List<ECliente> lista, string anio,string vfecha)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + " - A�O " + anio;
            xrlblTitulo1.Text = "ESTADO DE CUENTA DE LOS CLIENTES" + " " + vfecha;
            xrlblTitulo2.Text = "A�O " + anio ;

            this.DataSource = lista;
                        

            //Detalles
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vcod_cliente", "")});

            lblCliente.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vnombre_cliente", "")});

            lblMontoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxcc_nmonto_saldo_soles", "{0:n}")});

            lblMontoME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxcc_nmonto_saldo_dolares", "{0:n}")});

            //Enlace para sumatoria en el resumen
            lblTotalCobrarMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxcc_nmonto_saldo_soles", "{0:n}")});

            lblTotalCobrarME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxcc_nmonto_saldo_dolares", "{0:n}")});

            this.ShowPreview();
        }

    }
}
