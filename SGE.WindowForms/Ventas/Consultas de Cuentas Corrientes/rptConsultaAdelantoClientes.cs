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
    public partial class rptConsultaAdelantoClientes : DevExpress.XtraReports.UI.XtraReport
    {
        public rptConsultaAdelantoClientes()
        {
            InitializeComponent();
        }
        public void cargar(List<EAdelantoCliente> lista, string anio, bool filtro)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + " - A�O " + anio;

            xrlblTitulo1.Text = "LISTA DE ADELANTOS DEL A�O " + anio;
            //xrlblTitulo2.Text = "(PENDIENTES POR PAGAR)";


            this.DataSource = lista;

            //Detalles
            lblN�Adelanto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vnumero_adelanto", "")});

            lblProveedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vnombrecliente", "")});

            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SimboloMoneda", "")});

            lblsituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Situacion", "")});

            lblfecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "adci_sfecha_adelanto", "{0:dd/MM/yyyy}")});

            lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vnro_documento", "")});

            lblMontoAdelanto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_adelanto", "")});

            lblMontoAutorizado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_pagado", "")});

            lblSaldoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_saldo", "")});

            //enlaces al grupo 1
            lblGGAdelantoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_adelanto", "")});

            lblGGMPagadoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_pagado", "")});

            lblGGSActualMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_saldo", "")});


            lblGGAdelantoME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_adelanto", "")});

            lblGGMPagadoME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_pagado", "")});

            lblGGSActualME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nmonto_saldo", "")});
            this.ShowPreview();
        }
        decimal SaldoMN = 0;
        decimal SaldoME = 0;
        decimal PagadoMN = 0;
        decimal PagadoME = 0;
        decimal PrecioMN = 0;
        decimal PrecioME = 0;

        private void lblGGSActualMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("iid_tipo_moneda")) == 1)
                SaldoMN = SaldoMN + Convert.ToDecimal(GetCurrentColumnValue("nmonto_saldo")); 
        }

        private void lblGGSActualMN_SummaryReset(object sender, EventArgs e)
        {
            SaldoMN = 0;
        }

        private void lblGGSActualMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoMN;
            e.Handled = true;
        }

        private void lblGGSActualME_SummaryReset(object sender, EventArgs e)
        {
            SaldoME = 0;
        }

        private void lblGGSActualME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("iid_tipo_moneda")) == 2)
                SaldoME = SaldoME + Convert.ToDecimal(GetCurrentColumnValue("nmonto_saldo")); 
        }

        private void lblGGSActualME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoME;
            e.Handled = true;
        }

        private void lblGGMPagadoMN_SummaryReset(object sender, EventArgs e)
        {
            PagadoMN = 0;
        }

        private void lblGGMPagadoMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("iid_tipo_moneda")) == 1)
                PagadoMN = PagadoMN + Convert.ToDecimal(GetCurrentColumnValue("nmonto_pagado")); 
        }

        private void lblGGMPagadoMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoMN;
            e.Handled = true;
        }

        private void lblGGMPagadoME_SummaryReset(object sender, EventArgs e)
        {
            PagadoME = 0;
        }

        private void lblGGMPagadoME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("iid_tipo_moneda")) == 2)
                PagadoME = PagadoME + Convert.ToDecimal(GetCurrentColumnValue("nmonto_pagado")); 
        }

        private void lblGGMPagadoME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoME;
            e.Handled = true;
        }

        private void lblGGAdelantoMN_SummaryReset(object sender, EventArgs e)
        {
            PrecioMN = 0;
        }

        private void lblGGAdelantoMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("iid_tipo_moneda")) == 1)
                PrecioMN = PrecioMN + Convert.ToDecimal(GetCurrentColumnValue("nmonto_adelanto")); 
        }

        private void lblGGAdelantoMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioMN;
            e.Handled = true;
        }

        private void lblGGAdelantoME_SummaryReset(object sender, EventArgs e)
        {
            PrecioME = 0;
        }

        private void lblGGAdelantoME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("iid_tipo_moneda")) == 2)
                PrecioME = PrecioME + Convert.ToDecimal(GetCurrentColumnValue("nmonto_adelanto")); 
        }

        private void lblGGAdelantoME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioME;
            e.Handled = true;
        }

      
    }
}
