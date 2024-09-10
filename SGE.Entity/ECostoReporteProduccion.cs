using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
   public class ECostoReporteProduccion : EAuditoria
    {
        [DataMember]
        public Int32 crp_icod_costo { get; set; }
        [DataMember]
        public Int32 rp_icod_produccion { get; set; }
        [DataMember]
        public Int32 proc_icod_proveedor { get; set; }
        [DataMember]
        public Int64 doxpc_icod_correlativo { get; set; }
        [DataMember]
        public Int32 crp_tipo_costo { get; set; }
        [DataMember]
        public Int32 tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public Decimal crp_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public Decimal crp_nmonto_pago { get; set; }
        [DataMember]
        public Decimal crp_nmonto_pago_soles { get; set; }
        [DataMember]
        public Decimal crp_nmonto_pago_dolares { get; set; }
        [DataMember]
        public Int32 crp_iid_usuario_crea { get; set; }
        [DataMember]
        public DateTime crp_sfecha_crea { get; set; }
        [DataMember]
        public String crp_vpc_crea { get; set; }
        [DataMember]
        public Int32 crp_iid_usuario_modifica { get; set; }
        [DataMember]
        public DateTime crp_sfecha_modifica { get; set; }
        [DataMember]
        public String crp_vpc_modifica { get; set; }
        [DataMember]
        public Boolean crp_flag_estado { get; set; }
        [DataMember]
        public string unidc_vabreviatura_unidad_medida { get; set; }

        [DataMember]
        public String doxpc_vnumero_doc { get; set; }
        [DataMember]
        public String tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public DateTime doxpc_sfecha_doc { get; set; }
        [DataMember]
        public String proc_vcod_proveedor { get; set; }
        [DataMember]
        public String proc_vnombrecompleto { get; set; }
        [DataMember]
        public String TipoCosto { get; set; }
        [DataMember]
        public String Moneda { get; set; }
        [DataMember]
        public Int32 TipOper { get; set; }
    }
}
