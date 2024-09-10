using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EReporteConversionDet : EAuditoria
    {
        [DataMember]
        public int rcd_icod_reporte_conversion { get; set; }
        [DataMember]
        public int rcc_icod_reporte_conversion { get; set; }
        [DataMember]
        public int rcd_iitem_conversion { get; set; }
        [DataMember]
        public string rcd_Vitem_desempaque { get; set; }
        [DataMember]
        public int prdc_icod_producto { get; set; }
        [DataMember]
        public int kardc_icod_correlativo { get; set; }
        [DataMember]
        public decimal rcd_dcantidad_conversion { get; set; }
        [DataMember]
        public Boolean rcd_flag_estado { get; set; }
        [DataMember]
        public int intTipoOperacion { get; set; }


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

        public int almcc_icod_almacen { get; set; }
        public int ctacc_icod_cuenta_contable_producto { get; set; }
        public decimal monto_total { get; set; }
    }
}
