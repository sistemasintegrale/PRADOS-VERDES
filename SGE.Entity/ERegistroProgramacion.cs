using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ERegistroProgramacion : EAuditoria
    {
        public int rp_icod_registro_programacion { get; set; }
        public int rp_inumero_registro_programacion { get; set; }
        public DateTime rp_fecha { get; set; }
        public string rp_vobservaciones { get; set; }
        public int plap_icod_plantilla_programacion { get; set; }
        public int NumPlantilla { get; set; }
    }
}
