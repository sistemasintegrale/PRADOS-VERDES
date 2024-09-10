using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDistritoZona : EAuditoria
    {
        public object frm;

        public int disd_icod_distrito_zona { get; set; }
        public int zonc_icod_zona { get; set; }
        public int disc_icod_distrito { get; set; }
        public bool disd_flag_estado { get; set; }
        public string disc_vdescripcion { get; set; }
    }
}
