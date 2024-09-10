using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETablaVentaDet
    {
        [DataMember]
        public int tabvd_icorrelativo_venta_det { get; set; }
        [DataMember]
        public string tabvd_vdescripcion { get; set; }
        [DataMember]
        public string tabvd_vdesc_abreviado { get; set; }
        [DataMember]
        public int tabvd_iid_tabla_venta_det { get; set; }
        [DataMember]
        public int tabvc_iid_tipo_tabla { get; set; }
        [DataMember]
        public char tabvd_cestado { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        [DataMember]
        public int Anio_icod { get; set; }
        public int? tabvd_icod_ref { get; set; }
    }
}
