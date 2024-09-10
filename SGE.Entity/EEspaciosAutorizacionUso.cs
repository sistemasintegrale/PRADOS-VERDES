using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EEspaciosAutorizacionUso : EAuditoria
    {
        public int espau_icod_autorizacion_uso { get; set; }
        public int espau_iid_autorizacion_uso { get; set; }
        public DateTime espau_sfecha { get; set; }
        public string espau_vnom_fallecido { get; set; }
        public string espau_vapellido_paterno_fallecido { get; set; }
        public string espau_vapellido_materno_fallecido { get; set; }
        public string espau_vdni_fallecido { get; set; }
        public DateTime? espau_sfecha_nac_fallecido { get; set; }
        public DateTime? espau_sfecha_fallecido { get; set; }
        public DateTime? espau_sfecha_entierro { get; set; }
        public int espau_inacionalidad { get; set; }
        public int cntc_icod_contrato { get; set; }
        public string NumContrato { get; set; }
        public int espac_iid_iespacios { get; set; }
        public int espad_iid_iespacios { get; set; }
        public string espad_vnivel { get; set; }
    }
}
