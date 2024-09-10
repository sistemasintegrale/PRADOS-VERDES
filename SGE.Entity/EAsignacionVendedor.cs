using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EAsignacionVendedor : EAuditoria
    {
        public int asigc_icod_asignacion { get; set; }
        public int cajac_icod_caja { get; set; }
        public int tablc_iid_turno { get; set; }
        public int vendc_icod_vendedor { get; set; }
        public string asigc_vpassword_vendedor { get; set; }
        public string Turno { get; set; }
        public string NomVendedor { get; set; }
        public string DesPedido { get; set; }
    }
}
