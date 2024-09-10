using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EContratoTitular2 : EAuditoria
    {
        public int cntc_icod_contrato_titular2 { get; set; }
        public int cntc_icod_contrato { get; set; }
        public string cntc_vnombre_contratante { get; set; }
        public string cntc_vapellido_paterno_contratante { get; set; }
        public string cntc_vapellido_materno_contratante { get; set; }
        public string cntc_vdni_contratante { get; set; }
        public string cntc_vruc_contratante { get; set; }
        public DateTime? cntc_sfecha_nacimineto_contratante { get; set; }
        public string cntc_vtelefono_contratante { get; set; }
        public string cntc_vtelefono_contratante2 { get; set; }
        public string cntc_vdireccion_correo_contratante { get; set; }
        public string cntc_vdireccion_contratante { get; set; }
        public int cntc_inacionalidad_contratante { get; set; }
        public string cntc_vnacionalidad_cotratante { get; set; }
        public int cntc_itipo_documento_contratante { get; set; }
        public string cntc_vdocumento_contratante { get; set; }
    }
}
