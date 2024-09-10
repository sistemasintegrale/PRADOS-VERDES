using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EReporteConversionCab : EAuditoria
    {
        [DataMember]
        public int rcc_icod_reporte_conversion { get; set; }
        [DataMember]
        public DateTime rcc_sfecha { get; set; }
        [DataMember]
        public string rcc_vnuemro_reporte_conversion { get; set; }
        [DataMember]
        public int rcc_inuemro_desempaque { get; set; }
        [DataMember]
        public int prdc_icod_producto { get; set; }
        [DataMember]
        public int kardc_icod_correlativo { get; set; }
        [DataMember]
        public int almac_icod_almacen { get; set; }
        [DataMember]
        public int tablc_itipo_conversion { get; set; }
        [DataMember]
        public string rcc_vobservaciones { get; set; }
        [DataMember]
        public decimal rcc_dcantidad_conversion { get; set; }
        [DataMember]
        public Boolean rcc_flag_estado { get; set; }
        //descripcion

        [DataMember]
        public string DescripTipo { get; set; }
        [DataMember]
        public string almac_vdescripcion { get; set; }
        [DataMember]
        public string prdc_vdescripcion_larga { get; set; }
        [DataMember]
        public string prdc_vpart_number { get; set; }
        [DataMember]
        public string prdc_vcode_producto { get; set; }
        [DataMember]
        public string unidc_vabreviatura_unidad_medida { get; set; }
        [DataMember]
        public string plemc_Vicod { get; set; }

        public int IcodCuentaContable { get; set; }
        public decimal monto_total { get; set; }
    }
}
