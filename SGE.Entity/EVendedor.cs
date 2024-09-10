using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EVendedor : EAuditoria
    {
        [DataMember]
        public int vendc_icod_vendedor { get; set; }
        [DataMember]
        public string vendc_iid_vendedor { get; set; }
        [DataMember]
        public string vendc_vnombre_vendedor { get; set; }
        [DataMember]
        public string vendc_vdireccion { get; set; }
        [DataMember]
        public string vendc_vnumero_telefono { get; set; }
        [DataMember]
        public string vendc_cnumero_dni { get; set; }
        [DataMember]
        public int? tablc_iid_situacion_vendedor { get; set; }
        [DataMember]
        public string vdescripcion_situacion_vendedor { get; set; }
        [DataMember]
        public int? vendc_tipo_vendedor { get; set; }
        [DataMember]
        public string TipoVendedor { get; set; }
        [DataMember]
        public string vendc_vpassword_vendedor { get; set; }
        [DataMember]
        public int? vendc_icod_pvt { get; set; }
        public string vendc_vcorreo { get; set; }
        public int zonc_icod_zona { get; set; }
        public string zonc_vdescripcion { get; set; }
        public string vendc_vcod_vendedor { get; set; }
    }
}
