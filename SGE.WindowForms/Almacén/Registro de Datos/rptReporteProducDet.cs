using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class rptReporteProducDet : DevExpress.XtraReports.UI.XtraReport
    {
        public rptReporteProducDet()
        {
            InitializeComponent();
        }

        public void Cargar(EReporteProduccion obeRepProd, List<EReporteProduccionDetalle> ListaDet,List<ECostoReporteProduccion> ListaCosto)
        {
            this.DataSource = ListaDet;
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "REPORTE DE PRODUCCIÓN Nº " + obeRepProd.rp_num_produccion;
            lblTitulo2.Text = "DEL " + obeRepProd.rp_sfecha_produccion.ToString("dd/MM/yyyy");

            #region Cabecera
            lblProdI.Text = obeRepProd.prdc_vcode_producto + " - " + obeRepProd.prdc_vdescripcion_larga;
            lblCantidadI.Text = obeRepProd.rp_ncant_pro.ToString("n2");
            //lblEmpresaI.Text = obeRepProd.almac_vdescripcion;
            lblEmpresaI.Text = Modules.Valores.strNombreEmpresa;
            lblObserI.Text = obeRepProd.rp_voservaciones_produccion;
            lblTC.Text = obeRepProd.rp_ntipo_cambio.ToString("n4");

            lblSubProd.Text = obeRepProd.SubProductoEspecifico;
            lblCantidadSub.Text = obeRepProd.rp_ncant_subpro.ToString("n2");
            lblCosTotSub.Text = obeRepProd.rp_nmonto_costo_subprod.ToString("n2");
            lblCosUniSub.Text = (obeRepProd.rp_ncant_subpro == 0) ? "0.0000" : Math.Round(obeRepProd.rp_nmonto_costo_subprod / obeRepProd.rp_ncant_subpro, 4).ToString("n4");
            #endregion

            #region Detalle
            lblProdGen.DataBindings.Add(new XRBinding("Text", ListaDet, "prdc_vcode_producto"));
            lblProdDesc.DataBindings.Add(new XRBinding("Text", ListaDet, "prdc_vdescripcion_larga"));
            lblCantidadS.DataBindings.Add(new XRBinding("Text", ListaDet, "rpd_ncant_pro"));
            lblUM.DataBindings.Add(new XRBinding("Text", ListaDet, "unidc_vabreviatura_unidad_medida"));
            lblPCP.DataBindings.Add(new XRBinding("Text", ListaDet, "rpd_nmonto_unitario_costo_producto"));
            lblTotalS.DataBindings.Add(new XRBinding("Text", ListaDet, "rpd_nmonto_total_costo_producto"));
            lblAlmacenS.DataBindings.Add(new XRBinding("Text", ListaDet, "almac_vdescripcion"));
            #endregion

            #region Pie
            // SUBTOTALES
            lblSubTotal.Text = obeRepProd.rp_nmonto_total_soles.ToString("n2");
            lblSubTotalD.Text = obeRepProd.rp_nmonto_total_dolares.ToString("n2");
            //lblProces.Text = obeRepProd.rp_nmonto_total_costo_proceso.ToString("n2");
            //lblProcesD.Text = Math.Round(obeRepProd.rp_nmonto_total_costo_proceso / obeRepProd.rp_ntipo_cambio, 2).ToString("n2");
            //lblAlmac.Text = obeRepProd.rp_nmonto_total_costo_almacenaje.ToString("n2");
            //lblAlmacD.Text = Math.Round(obeRepProd.rp_nmonto_total_costo_almacenaje / obeRepProd.rp_ntipo_cambio, 2).ToString("n2");
            //lblTransp.Text = obeRepProd.rp_nmonto_total_costo_transporte.ToString("n2");
            //lblTranspD.Text = Math.Round(obeRepProd.rp_nmonto_total_costo_transporte / obeRepProd.rp_ntipo_cambio, 2).ToString("n2");
            //lblSubProdT.Text = obeRepProd.rp_nmonto_costo_subprod.ToString("n2");
            //lblSubProdTD.Text = (-1 * (obeRepProd.rp_nmonto_costo_subprod / obeRepProd.rp_ntipo_cambio)).ToString("n4");
            // TOTALES
            //lblTotalT.Text = (obeRepProd.rp_nmonto_total_soles + obeRepProd.rp_nmonto_total_costo_proceso + obeRepProd.rp_nmonto_total_costo_almacenaje
            //                    + obeRepProd.rp_nmonto_total_costo_transporte + obeRepProd.rp_nmonto_total_costo_maquila - obeRepProd.rp_nmonto_costo_subprod).ToString("n2");
            lblTotalT.Text = obeRepProd.MontoTotal.ToString("n2");
            //lblTotalTD.Text = ((obeRepProd.rp_nmonto_total_soles + obeRepProd.rp_nmonto_total_costo_proceso + obeRepProd.rp_nmonto_total_costo_almacenaje
            //                  + obeRepProd.rp_nmonto_total_costo_transporte + obeRepProd.rp_nmonto_total_costo_maquila - obeRepProd.rp_nmonto_costo_subprod) / obeRepProd.rp_ntipo_cambio).ToString("n2");
            lblTotalTD.Text = (obeRepProd.MontoTotal / obeRepProd.rp_ntipo_cambio).ToString("n2");
            //lblTotalTD.Text = (Convert.ToDecimal(lblSubTotalD.Text) + Convert.ToDecimal(lblProcesD.Text) + Convert.ToDecimal(lblAlmacD.Text)
            //                   + Convert.ToDecimal(lblTranspD.Text) - Convert.ToDecimal(lblSubProdTD.Text)).ToString("n2");
            lblUnitario.Text = obeRepProd.MontoUnitario.ToString("n2");
            lblUnitarioD.Text = obeRepProd.MontoUnitarioDolares.ToString("n2");
            #endregion

            this.ShowPreview();

            if (ListaCosto.Count > 0)
            {
                CalculoMontosCostos(ListaCosto);
                subRptRepProdCosto subRpt = new subRptRepProdCosto();
                subRpt.Cargar(ListaCosto);
                subReportCosto.ReportSource = subRpt;
            }
            if (ListaCosto.Count > 0)
            {
                CalculoMontosCostos(ListaCosto);
                subRptRepCostoDetalle subRpt = new subRptRepCostoDetalle();
                subRpt.Cargar(ListaCosto);
                SubCostoDetalle.ReportSource = subRpt;
            }
        }

        private void CalculoMontosCostos(List<ECostoReporteProduccion> ListaCosto)
        {
            foreach (ECostoReporteProduccion item in ListaCosto)
            {
                item.doxpc_vnumero_doc = item.tdocc_vabreviatura_tipo_doc + "-" + item.doxpc_vnumero_doc;
                if (item.tablc_iid_tipo_moneda == 3) // SOLES
                {
                    item.crp_nmonto_pago_soles = item.crp_nmonto_pago;
                    item.crp_nmonto_pago_dolares = Math.Round(item.crp_nmonto_pago / item.crp_nmonto_tipo_cambio, 2);
                }
                else // DÓLARES
                {
                    item.crp_nmonto_pago_soles = item.crp_nmonto_pago * item.crp_nmonto_tipo_cambio;
                    item.crp_nmonto_pago_dolares = item.crp_nmonto_pago;
                }
            }
        }
    }
}
