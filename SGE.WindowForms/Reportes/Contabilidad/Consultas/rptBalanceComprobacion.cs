using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;

namespace SGE.WindowForms.Reportes.Contabilidad.Consultas
{
    public partial class rptBalanceComprobacion : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBalanceComprobacion()
        {
            InitializeComponent();
        }
        public void cargar(List<EVoucherContableDet> Lista, string strEjercicio, string strMes,string strDigitos,string strMoneda, string codigo)
        {
            xrlblTitulo.Text = (codigo == "<RFRM13>") ? String.Format("BALANCE DE COMPROBACI�N EN {0} - AL DIA {1}", strMoneda, strMes.ToUpper()) :
                String.Format("BALANCE DE COMPROBACI�N EN {0} - AL MES DE {1} DE {2}", strMoneda, strMes.ToUpper(), strEjercicio);
            xrlblTitulo2.Text = String.Format("RESUMEN A {0} D�GITOS", strDigitos);
            xrlblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - A�O " + strEjercicio;
            lblRptCodigo.Text = codigo;

            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("cta_padre") });
            #region Header
            lblCtaPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cta_padre", "")});
            lblDescPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cta_vdespadre", "")});
            
            lblSaldoAntPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebitosPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblCreditosPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});

            #endregion
            #region Detail            
            lblCta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});
            lblDescCta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});
            lblSaldoAnt.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebitos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblCreditos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoAct.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});
            #endregion
            #region Footer
            lblSaldoAntTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "")});
            lblDebitosTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "")});
            lblCreditosTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "")});
            lblSaldoActTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "")});
            #endregion
            this.ShowPreview();
        }
    }
}
