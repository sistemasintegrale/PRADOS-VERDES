using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class ECategoriaFamilia:EAuditoria
    {
        [DataMember]
        public int catf_icod_categoria { get; set; }
        [DataMember]
        public int catf_iid_categoria { get; set; }
        [DataMember]
        public string catf_vabreviatura { get; set; }
        [DataMember]
        public string catf_vdescripcion { get; set; }
        [DataMember]
        public bool catf_flag_estado { get; set; }
        [DataMember]
        public int? clasc_icod_clasificacion { get; set; }

        public string strClasificacion { get; set; }

        public int catf_icod_tipo { get; set; }
        public string Tipo { get; set; }
        public int almcc_icod_almacen { get; set; }
        public string almcc_vdescripcion { get; set; }


        //*REPORTE*//
        public int? famic_icod_familia { get; set; }
        public string famic_vdescripcion { get; set; }

        public int? famid_icod_familia { get; set; }
        public string famid_vdescripcion { get; set; }
    }
}
