using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EContratoFallecido : EAuditoria
    {
        public int cntc_icod_contrato_fallecido { get; set; }
        public int cntc_icod_contrato { get; set; }
        public string cntc_vnombre_fallecido { get; set; }
        public string cntc_vapellido_paterno_fallecido { get; set; }
        public string cntc_vapellido_materno_fallecido { get; set; }
        public string cntc_vdni_fallecido { get; set; }
        public DateTime? cntc_sfecha_nac_fallecido { get; set; }
        public DateTime? cntc_sfecha_fallecimiento { get; set; }
        public DateTime? cntc_sfecha_entierro { get; set; }
        public int cntc_itipo_documento_fallecido { get; set; }
        public string cntc_vdocumento_fallecido { get; set; }
        public int cntc_inacionalidad { get; set; }
        public string cntc_vdireccion_fallecido { get; set; }
        public int cntc_icod_indicador_espacios { get; set; }
        public int intTipoOperacion { get; set; }
        public int cntc_icod_religiones { get; set; }
        public int cntc_icod_tipo_deceso { get; set; }
        public string cntc_vobservacion { get; set; }
        public string strReligiones { get; set; }
        public string strTipoDeceso { get; set; }
        public DateTime? cntc_sfecha_accion { get; set; }
        public int cntc_icod_tamanio_lapida { get; set; }
        public string espad_vnivel { get; set; }
        public string cntc_vfrase { get; set; }
        public string cntc_vpensamiento { get; set; }

        public int? cntc_itipo_sepultura { get; set; }
        public int? cntc_icod_manzana { get; set; }
        public int? cntc_icod_isepultura { get; set; }
        public string strsepultura { get; set; }
        public int? espac_iid_iespacios { get; set; }
        public string espac_icod_vespacios { get; set; }
        public int? cntc_icod_plataforma { get; set; }

        public string NombreCompleto { get { return $"{cntc_vnombre_fallecido} {cntc_vapellido_paterno_fallecido} {cntc_vapellido_materno_fallecido}"; } }
    }
}
