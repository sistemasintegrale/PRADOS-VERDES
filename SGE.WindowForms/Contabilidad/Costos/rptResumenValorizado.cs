using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class rptResumenValorizado : DevExpress.XtraReports.UI.XtraReport
    {
        public rptResumenValorizado()
        {
            InitializeComponent();
        }
        public void carga(List<EKardexValorizado> Lista, string Titulo1, string Titulo2)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - A�O " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "COSTOS";
            lblTitulo1.Text = Titulo1;
            lblTitulo2.Text = Titulo2;
            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("almcc_icod_almacen") });
            #region Header
            lblIidAlmacen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "intIidAlmaCont", "")});
            lblDesAlmacen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesAlmacenCtbl", "")});
            #endregion
            #region Detail
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "prdc_vcode_producto", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesProducto", "")});
            //lblEstado.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "estac_vdescripcion", "")});
            //lblSituacion.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "situc_vdescripcion", "")});
            lblUnidadMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "unidc_vabreviatura_unidad_medida", "")});
            /*-----------------------------------------------------------------------------------*/
            lblStockAnterior.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "StockAnterior", "{0:N2}")});
            lblIngresos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dcmlIngreso", "{0:N2}")});
            lblSalidas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dcmlSalida", "{0:N2}")});
            lblStockActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Stock", "{0:N2}")});
            /*-----------------------------------------------------------------------------------*/
            lblMtoAnterior.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "StockAnteriorCosto", "{0:N2}")});
            lblMtoIngresos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "IngresoCosto", "{0:N2}")});
            lblMtoSalidas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "SalidaCosto", "{0:N2}")});
            lblMtoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_saldo_valorizado", "{0:N2}")});
            /*-----------------------------------------------------------------------------------*/
            #endregion
            #region Footer
            lblTotalMtoAnterior.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "StockAnteriorCosto", "")});
            lblTotalMtoIngresos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "IngresoCosto", "")});
            lblTotalMtoSalidas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "SalidaCosto", "")});
            lblTotalMtoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_saldo_valorizado", "")});
            #endregion
            this.ShowPreview();

        }

      
    }
}
