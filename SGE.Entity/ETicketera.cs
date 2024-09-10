using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ETicketera : EAuditoria
    {
        public int tckc_icod_ticketera { get; set; }
        public int tckc_inumero_impresora { get; set; }
        public string tckc_vserie_impresora { get; set; }
        public string tckc_vnombre_local { get; set; }
        public string tckc_vdireccion { get; set; }
        public string tckc_vserie { get; set; }
        public string tckc_vcorrelativo { get; set; }
        public int tablc_iid_situacion { get; set; }
        public string Situacion { get; set; }

    }
}
