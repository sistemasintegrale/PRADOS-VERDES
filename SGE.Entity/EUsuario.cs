using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EUsuario : EAuditoria
    {
        [DataMember]
        public int usua_icod_usuario { get; set; }
        [DataMember]
        public string usua_codigo_usuario { get; set; }
        [DataMember]
        public string usua_nombre_usuario { get; set; }
        [DataMember]
        public string usua_password_usuario { get; set; }
        [DataMember]
        public bool usua_iactivo { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        public Boolean usua_indicador_asesor { get; set; }
        public int vendc_icod_vendedor { get; set; }
        public bool usua_bweb { get; set; }
    }
}
