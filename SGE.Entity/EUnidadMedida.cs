using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EUnidadMedida : EAuditoria
    {
        [DataMember]
        public int unidc_icod_unidad_medida { get; set; }
        [DataMember]
        public int unidc_iid_unidad_medida { get; set; }
        [DataMember]
        public string unidc_vabreviatura_unidad_medida { get; set; }
        [DataMember]
        public string unidc_vdescripcion { get; set; }
        [DataMember]
        public bool unidc_sflag_estado { get; set; }
    }
}
