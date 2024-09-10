using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EFamiliaCab : EAuditoria
    {
        [DataMember]
        public int famic_icod_familia { get; set; }
        [DataMember]
        public int famic_iid_familia { get; set; }
        [DataMember]
        public string famic_vabreviatura { get; set; } 
        [DataMember]
        public string famic_vdescripcion { get; set; }
        [DataMember]
        public bool famic_flag_estado { get; set; }
        [DataMember]
        public int? clasc_icod_clasificacion { get; set; }

        public string strClasificacion { get; set; }

        public int famic_icod_tipo { get; set; }
        public string Tipo { get; set; }
        public int almcc_icod_almacen { get; set; }
        public string almcc_vdescripcion { get; set; }
        public int catf_icod_categoria { get; set; }

    }
}
