using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EContratoBeneficiario : EAuditoria
    {
        public int cntc_icod_contrato_beneficiario { get; set; }
        public int cntc_icod_contrato { get; set; }
        public string cntc_vnombre_beneficiario { get; set; }
        public string cntc_vapellido_paterno_beneficiario { get; set; }
        public string cntc_vapellido_materno_beneficiario { get; set; }
        public string cntc_vdni_beneficiario { get; set; }
        public DateTime? cntc_sfecha_nacimiento_beneficiario { get; set; }
        public string cntc_vdireccion_beneficiario { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
