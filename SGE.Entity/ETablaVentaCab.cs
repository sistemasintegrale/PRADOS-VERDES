using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETablaVentaCab
    {
        [DataMember]
        public int tabvc_iid_tipo_tabla { get; set; }
        [DataMember]
        public string tabvc_vdescripcion { get; set; }
        [DataMember]
        public char tabvc_cestado { get; set; }
        [DataMember]
        public string strEstado { get; set; }
    }
}
