using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
   public  class ELocalidades :EAuditoria
    {
        [DataMember]
        public int lafc_icod_localidades { set; get; }
        [DataMember]
        public string lafc_iid_localidades { set; get; }
        [DataMember]
        public string lafc_vdescripcion{ set; get; }
        [DataMember]
        public bool? lafc_flag_estado { set; get; }
    }
}
