using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace SGE.Entity
{
    [DataContract]
    public class EAuditoria
    {
        [DataMember]
        public int intUsuario { get; set; }
        [DataMember]
        public string strPc { get; set; }
        [DataMember]
        public int anio { get; set; }
        public bool flag { get; set; }

        public EAuditoria()
        {
            flag = true;
            strPc = WindowsIdentity.GetCurrent().Name;
        }
    }
}
