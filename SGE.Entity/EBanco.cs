using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EBanco : EAuditoria
    {
        [DataMember]
        public int bcoc_icod_banco { get; set; }
        [DataMember]
        public int? bcoc_iid_banco { get; set; }
        [DataMember]
        public int? bcoc_iid_tipo_banco { get; set; }
        [DataMember]
        public string bcoc_vnombre_banco { get; set; }
        [DataMember]
        public int bcoc_iid_situacion_banco { get; set; }
        [DataMember]
        public bool bcoc_flag_estado { get; set; }
        /*--------------------------------------------*/
        [DataMember]
        public string strSituacion { get; set; } 
    }
}
