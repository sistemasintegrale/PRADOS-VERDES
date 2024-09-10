using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EReporteProduccion : EAuditoria
    {
        [DataMember]
        public Int32 rp_icod_produccion { get; set; }
        [DataMember]
        public Int32 rp_iid_anio { get; set; }
        [DataMember]
        public String rp_num_produccion { get; set; }
        [DataMember]
        public Int32 tablc_iid_tipo_rp { get; set; }
        [DataMember]
        public String TipoReporte { get; set; }
        [DataMember]
        public DateTime rp_sfecha_produccion { get; set; }
        [DataMember]
        public Decimal rp_ntipo_cambio { get; set; }
        [DataMember]
        public Int32 prdc_icod_producto { get; set; }
        [DataMember]
        public String prdc_vdescripcion_larga { get; set; }
        [DataMember]
        public Decimal rp_ncant_pro { get; set; }
        [DataMember]
        public Int32 proc_icod_proveedor { get; set; }
        [DataMember]
        public String rp_voservaciones_produccion { get; set; }
        [DataMember]
        public Int32 almac_icod_almacen { get; set; }
        [DataMember]
        public String almac_vdescripcion { get; set; }
        [DataMember]
        public Decimal rp_nmonto_total_soles { get; set; }
        [DataMember]
        public Decimal rp_nmonto_total_dolares { get; set; }
        [DataMember]
        public Decimal rp_nmonto_total_costo_almacenaje { get; set; }
        [DataMember]
        public Decimal rp_nmonto_total_costo_maquila { get; set; }
        [DataMember]
        public Decimal rp_nmonto_total_costo_proceso { get; set; }
        [DataMember]
        public Decimal rp_nmonto_total_costo_transporte { get; set; }
        [DataMember]
        public Int32 prdc_icod_sub_producto { get; set; }
        [DataMember]
        public Decimal rp_ncant_subpro { get; set; }
        [DataMember]
        public Decimal rp_nmonto_costo_subprod { get; set; }
        [DataMember]
        public Int64? rp_id_kardex_producto_ingreso { get; set; }
        [DataMember]
        public DateTime? rp_sfecha_ing_kardex { get; set; }
        [DataMember]
        public Int32 rp_iid_situacion { get; set; }
        [DataMember]
        public Int32 rp_iid_usuario_crea { get; set; }
        [DataMember]
        public DateTime rp_sfecha_crea { get; set; }
        [DataMember]
        public String rp_vpc_crea { get; set; }
        [DataMember]
        public Int32 rp_iid_usuario_modifica { get; set; }
        [DataMember]
        public DateTime rp_sfecha_modifica { get; set; }
        [DataMember]
        public String rp_vpc_modifica { get; set; }
        [DataMember]
        public Boolean rp_flag_estado { get; set; }

        [DataMember]
        public String proc_vcod_proveedor { get; set; }
        [DataMember]
        public String proc_vnombrecompleto { get; set; }
        [DataMember]
        public String unidc_vabreviatura_unidad_medida { get; set; }
        [DataMember]
        public String situc_vdescripcion { get; set; }
        [DataMember]
        public String estac_vdescripcion { get; set; }
        [DataMember]
        public Decimal CostoTotal { get; set; }
        [DataMember]
        public Decimal MontoUnitario { get; set; }
        [DataMember]
        public Decimal MontoUnitarioDolares { get; set; }
        [DataMember]
        public String AlmacenIngreso { get; set; }
        [DataMember]
        public Decimal rp_ncant_pro_especifico_temp { get; set; }
        [DataMember]
        public String SubProductoEspecifico { get; set; }
        [DataMember]
        public string prdc_vcode_producto { get; set; }
        [DataMember]
        public int? clasc_vcuenta_contable_producto { get; set; }
        [DataMember]
        public int? clasc_vcuenta_contable_producto_sub_prod { get; set; }
        [DataMember]
        public Decimal? pcp_prod { get; set; }
        [DataMember]
        public Decimal? pcp_sub_prod { get; set; }

        public decimal MontoTotal { get; set; }
        public decimal MontoTotalDolares { get; set; }
    }
}
