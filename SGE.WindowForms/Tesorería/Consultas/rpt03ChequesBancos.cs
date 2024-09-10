using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Tesorería.Consultas
{
    public partial class rpt03ChequesBancos : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt03ChequesBancos()
        {
            InitializeComponent();
        }
        public void carga(List<ELibroBancos> Lista, string banco, string cuenta)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "RELACIÓN DE CHEQUES DE " + Parametros.intEjercicio.ToString();
            lblTitulo2.Text = banco + " - " + cuenta;

            this.DataSource = Lista;
            
            lblNroCheque.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vnro_documento", "{0:0000000000}")});
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dfecha_movimiento", "{0:dd/MM/yyyy}")});
            lblBeneficiario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdescripcion_beneficiario", "")});
            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "TipoMoneda", "")});
            lblImporte.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Cargo", "")});
            lblConciliacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "sflag_conciliacion", "")});
            lblMes.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Mes", "")});
            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Situacion", "")});
            this.ShowPreview();
        }

    }
}
