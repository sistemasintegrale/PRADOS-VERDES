using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EFacturaDet : EAuditoria
    {
        public int favd_icod_item_factura { get; set; }
        public int favc_icod_factura { get; set; }
        public int favd_iitem_factura { get; set; }
        public int? almac_icod_almacen { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal favd_ncantidad { get; set; }

        public string favd_vncantidad { get; set; }
        public string favd_vnprecio_unitario_item { get; set; }
        public string favd_vnprecio_total_item { get; set; }

        public string favd_vdescripcion { get; set; }
        public decimal favd_nprecio_unitario_item { get; set; }
        public decimal favd_nmonto_impuesto_item { get; set; }
        public decimal favd_nporcentaje_descuento_item { get; set; }
        public decimal favd_nprecio_total_item { get; set; }
        public int? kardc_icod_correlativo { get; set; }
        public int favd_iicod_tipo_pago { get; set; }
        /**/
        public string strCodProducto { get; set; }
        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strDesUM { get; set; }
        public string strDesProducto { get; set; }
        public string strMoneda { get; set; }
        public string strAlmacen { get; set; }       
        public decimal dblMontoDescuento { get; set; }
        public decimal? dblStockDisponible { get; set; }
        public int intTipoOperacion { get; set; }
        public bool flagPlanilla { get; set; }
        public string prdc_vpart_number { get; set; }
        public string favd_nobservaciones { get; set; }
        public int OrdenItemImprimir{ get; set; }
        public string strTipoServicio { get; set; }


        public string vnumero_factura { get; set; }

        public int tablc_iid_tipo_venta { get; set; }
        public string strTipoVenta { get; set; }
        //public string favd_vcaracteristicas { get; set; }

        public decimal favd_npor_imp_arroz { get; set; }
        public decimal favd_nmonto_imp_arroz { get; set; }
        public Boolean AfectoIVAP { get; set; }
        /*Planilla*/
        public int intClasificacionProducto { get; set; }
        public string strLinea { get; set; }
        public string strSubLinea { get; set; }
        public decimal favd_nneto_ivap { get; set; }
        public decimal favd_nneto_igv { get; set; }
        public decimal favd_nneto_exo { get; set; }
        public Boolean prdc_afecto_ivap { get; set; }
        public Boolean prdc_afecto_igv { get; set; }
        public string CodigoSUNAT { get; set; }
        /*Factura Electronica Detalle*/
        public int IdItems { get; set; }
        public int IdCabezera { get; set; }
        public int NumeroOrdenItem { get; set; }
        public decimal cantidad { get; set; }
        public string unidadMedida { get; set; }
        public decimal ValorVentaItem { get; set; }
        public int CodMotivoDescuentoItem { get; set; }
        public decimal FactorDescuentoItem { get; set; }
        public decimal DescuentoItem { get; set; }
        public decimal BaseDescuentotem { get; set; }
        public int CodMotivoCargoItem { get; set; }
        public decimal FactorCargoItem { get; set; }
        public decimal MontoCargoItem { get; set; }
        public decimal BaseCargoItem { get; set; }
        public decimal MontoTotalImpuestosItem { get; set; }
        public decimal MontoImpuestoIgvItem { get; set; }
        public decimal MontoAfectoImpuestoIgv { get; set; }
        public decimal PorcentajeIGVItem { get; set; }
        public decimal MontoInafectoItem { get; set; }
        public decimal MontoImpuestoISCItem { get; set; }
        public decimal MontoAfectoImpuestoIsc { get; set; }
        public decimal PorcentajeISCtem { get; set; }
        public decimal MontoImpuestoIVAPtem { get; set; }
        public decimal MontoAfectoImpuestoIVAPItem { get; set; }
        public decimal PorcentajeIVAPItem { get; set; }
        public string descripcion { get; set; }
        public string codigoItem { get; set; }
        public string ObservacionesItem { get; set; }
        public decimal ValorUnitarioItem { get; set; }
        public decimal PrecioVentaUnitarioItem { get; set; }
        public string tipoImpuesto { get; set; }

    }
}
