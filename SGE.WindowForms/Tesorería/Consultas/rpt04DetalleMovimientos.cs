using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Tesorer�a.Consultas
{
    public partial class rpt04DetalleMovimientos : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt04DetalleMovimientos()
        {
            InitializeComponent();
        }
        public void carga(List<ELibroBancos> Lista, string FechaI, string FechaF)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - A�O " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "DETALLE DE MOVIMIENTOS DE BANCOS";
            lblTitulo2.Text = "DEL " + FechaI + " AL " + FechaF;
            
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("iid_correlativo") });
            
            lblCodBanco.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "iid_entidad_financiera", "{0:00}")});
            lblBanco.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Descripcion_Banco", "")});
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Numero_cuenta", "")});   
            this.DataSource = Lista;
            #region Detalle Grupo
            
            lblFechaRegistro.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dfecha_crea", "{0:dd/MM/yyyy}")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "documento", "")});
            lblFechaDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dfecha_movimiento", "{0:dd/MM/yyyy}")});
            lblBeneficiario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdescripcion_beneficiario", "")});
            lblGlosa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vglosa", "")});
            lblAbonos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Abono", "{0:N2}")});
            lblCargos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Cargo", "{0:N2}")});  
            #endregion          
            this.ShowPreview();
        }

    }
}
