using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
   public class EReporteProduccionDetalle : EAuditoria
    {
        [DataMember]
        public Int32 rpd_icod_produccion { get; set; }
        [DataMember]
        public Int32 rp_icod_produccion { get; set; }
        [DataMember]
        public Int32 rpd_item { get; set; }
        [DataMember]
        public Int32 almac_icod_almacen { get; set; }
        [DataMember]
        public int almcc_icod_almacen { get; set; }
        [DataMember]
        public String almac_vdescripcion { get; set; }
        [DataMember]
        public Int32 prdc_icod_producto { get; set; }
        [DataMember]
        public Decimal rpd_ncant_pro { get; set; }
        [DataMember]
        public Decimal rpd_nmonto_unitario_costo_producto { get; set; }
        [DataMember]
        public Decimal rpd_nmonto_total_costo_producto { get; set; }
        [DataMember]
        public Int64 rpd_id_kardex_salida { get; set; }
        [DataMember]
        public Int32 rpd_iusuario_crea { get; set; }
        [DataMember]
        public DateTime rpd_sfecha_crea { get; set; }
        [DataMember]
        public String rpd_vpc_crea { get; set; }
        [DataMember]
        public Int32 rpd_iusuario_modifica { get; set; }
        [DataMember]
        public DateTime rpd_sfecha_modifica { get; set; }
        [DataMember]
        public String rpd_vpc_modifica { get; set; }
        [DataMember]
        public Boolean rpd_flag_estado { get; set; }

        [DataMember]
        public String prdc_vdescripcion_larga { get; set; }
        [DataMember]
        public string prdc_vcode_producto { get; set; }
        [DataMember]
        public String unidc_vabreviatura_unidad_medida { get; set; }
        [DataMember]
        public Decimal rpd_ncant_pro_especifico_temp { get; set; }
        [DataMember]
        public Int32 TipOper { get; set; }
        [DataMember]
        public int? clasc_vcuenta_contable_producto;
        [DataMember]
        public Decimal? pcp { get; set; }
    }
}
