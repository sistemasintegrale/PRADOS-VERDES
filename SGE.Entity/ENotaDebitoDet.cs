using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ENotaDebitoDet : EAuditoria
    {
        public int ddebc_icod_detalle_debito { get; set; }
        public int ndebc_icod_debito { get; set; }
        public Int16 ddebc_inro_item { get; set; }
        public decimal ddebc_ncantidad_producto { get; set; }
        public decimal ddebc_nmonto_unitario { get; set; }
        public string ddebc_vdescripcion { get; set; }
        public decimal ddebc_nmonto_item { get; set; }
        public string ddebc_vmonto_item { get; set; }
        public decimal ddebc_nmonto_impuesto { get; set; }
        public decimal ddebc_nporcentaje_impuesto { get; set; }
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

        public decimal ddebc_npor_imp_arroz { get; set; }
        public decimal ddebc_nmonto_imp_arroz { get; set; }
        public Boolean AfectoIVAP { get; set; }

        public decimal ddebc_nneto_ivap { get; set; }
        public decimal ddebc_nneto_igv { get; set; }
        public decimal ddebc_nneto_exo { get; set; }
        public Boolean prdc_afecto_ivap { get; set; }
        public Boolean prdc_afecto_igv { get; set; }
        public decimal ddebc_nmonto_total { get; set; }
        public string ObservacionesItem { get; set; }
    }
}
