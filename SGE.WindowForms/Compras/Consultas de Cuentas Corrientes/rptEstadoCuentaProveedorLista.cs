using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class rptEstadoCuentaProveedorLista : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaProveedorLista()
        {
            InitializeComponent();
        }
        public void cargar(List<EProveedor> lista, string anio)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa+" - A�O " + anio;
            xrlblTitulo2.Text = "A�O " + anio;

            this.DataSource = lista;


            //Detalles
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcod_proveedor", "")});

            lblCliente.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vnombrecompleto", "")});

            lblMontoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo_soles", "{0:n}")});

            lblMontoME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo_dolares", "{0:n}")});

            //Enlace para sumatoria en el resumen
            lblTotalCobrarMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo_soles", "{0:n}")});

            lblTotalCobrarME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo_dolares", "{0:n}")});

            this.ShowPreview();
        }
    }
}
