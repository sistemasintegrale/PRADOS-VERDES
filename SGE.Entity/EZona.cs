using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EZona : EAuditoria
    {
        public object frm;

        public int zonc_icod_zona { get; set; }
        public int zonc_iid_zona { get; set; }
        public string zonc_vdescripcion { get; set; }
        public bool zonc_flag_estado { get; set; }
    }
}
