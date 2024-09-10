using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ENotaCreditoDet : EAuditoria
    {
        public int dcrec_icod_detalle_credito { get; set; }
        public int ncrec_icod_credito { get; set; }
        public Int16 dcrec_inro_item { get; set; }
        public decimal dcrec_ncantidad_producto { get; set; }
        public decimal dcrec_nmonto_unitario { get; set; }
        public string dcrec_vdescripcion { get; set; }
        public decimal dcrec_nmonto_item { get; set; }
        public string dcrec_vmonto_item { get; set; }
        public decimal dcrec_nmonto_impuesto { get; set; }
        public decimal dcrec_nporcentaje_impuesto { get; set; }
        public int? prdc_icod_producto { get; set; }
        public int? almac_icod_almacen { get; set; }
        public int? tablc_iid_sit_item_nota_credito { get; set; }
        public Int64? kardc_iid_correlativo { get; set; }
        /**/
        public int intClasificacion { get; set; }
        public int intTipoOperacion { get; set; }
        public string strAlmacen { get; set; }
        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public string strDesProductoPresentar { get; set; }
        public string strDesUM { get; set; }
        public string strMoneda { get; set; }
        public string strLinea { get; set; }
        public string strSubLinea { get; set; }

        public decimal dcrec_npor_imp_arroz { get; set; }
        public decimal dcrec_nmonto_imp_arroz { get; set; }
        public Boolean AfectoIVAP { get; set; }

        public decimal dcrec_nneto_ivap { get; set; }
        public decimal dcrec_nneto_igv { get; set; }
        public decimal dcrec_nneto_exo { get; set; }
        public Boolean prdc_afecto_ivap { get; set; }
        public Boolean prdc_afecto_igv { get; set; }
        public decimal dcrec_nmonto_total { get; set; }



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
