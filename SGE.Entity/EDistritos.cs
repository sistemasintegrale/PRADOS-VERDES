using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EDistritos : EAuditoria
    {
        public int disc_icod_distrito { get; set; }
        public int disc_iid_distrito { get; set; }
        public string disc_vdescripcion { get; set; }
        public bool disc_flag_estado { get; set; }
    }
}
