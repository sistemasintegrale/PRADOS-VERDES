using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EPlantillaProgramacionDet : EAuditoria
    {
        public int plad_icod_plantilla_programacion_det { get; set; }
        public int plap_icod_plantilla_programacion { get; set; }
        public int plad_iorden { get; set; }
        public string plad_vhora_inicial { get; set; }
        public string plad_vhora_final { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
