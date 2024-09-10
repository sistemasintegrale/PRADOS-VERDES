using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;

namespace SGE.WindowForms.Reportes.Contabilidad.Consultas
{
    public partial class rptMayorCCostoMesSinDet : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMayorCCostoMesSinDet()
        {
            InitializeComponent();
        }
        public void Cargar(List<EVoucherContableDet> Lista, string Mes, string CuentaI, string CuentaF, string CCostoI, string CCostoF,string type,string codigo)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - A�O " + Parametros.intEjercicio;
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = String.Format("MAYOR {0} POR CENTRO DE COSTO EN M.N - {1} DE {2} ", type, Mes.ToUpper(), Parametros.intEjercicio.ToString());
            lblTitulo2.Text = "CUENTA : DE " + CuentaI + " A " + CuentaF + "C.COSTO : DE " + CCostoI + " A " + CCostoF;
            lblRptCodigo.Text = codigo;
            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("strCodCCosto") });


            #region GroupHeader
            lblCCosto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strCodCCosto", "")});
            lblCCDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "anac_cecoc_vdescripcion", "")});

            lblSaldoAntMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "")});
            lblDebeMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "")});
            lblHaberMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "")});
            lblSaldoActMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "")});

            lblSaldoAntME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_dol", "")});
            lblDebeME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "")});
            lblHaberME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "")});
            lblSaldoActME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_dol", "")});
            #endregion

            #region Detail
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});

            lblDescripcionCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});

            lblSaldoAntMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebeMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});

            lblSaldoAntME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_dol", "{0:N2}")});
            lblDebeME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});
            lblSaldoActME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_dol", "{0:N2}")});
            #endregion

            #region Footer
            lblSaldoAntMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "")});
            lblDebeMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "")});
            lblHaberMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "")});
            lblSaldoActMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "")});

            lblSaldoAntME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_dol", "")});
            lblDebeME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "")});
            lblHaberME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "")});
            lblSaldoActME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_dol", "")});
            #endregion
            this.ShowPreview();
        }
    }
}


