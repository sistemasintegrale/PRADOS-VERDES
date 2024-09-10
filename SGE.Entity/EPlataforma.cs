using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EPlataforma : EAuditoria
    {
        public int pltc_icod_plataforma { get; set; }
        public int pltc_iid_plataforma { get; set; }
        public string pltc_vdescripcion { get; set; }
        public bool pltc_flag_estado { get; set; }
    }
}
