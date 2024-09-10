using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EEspaciosDet : EAuditoria
    {
        public int espad_iid_iespacios { get; set; }
        public int espac_iid_iespacios { get; set; }
        public string espad_vnivel { get; set; }
        public int espad_icod_isituacion { get; set; }
        public int espad_icod_iestado { get; set; }
        public string strsituacion { get; set; }
        public string strestado { get; set; }
        public int intTipoOperacion { get; set; }
        public int cntc_icod_contrato { get; set; }
        public string NumContrato { get; set; }
        public string NomContratante { get; set; }
        public string cntc_vdni_contratante { get; set; }
        public string cntc_vtelefono_contratante { get; set; }
        public string strplataforma { get; set; }
        public string strmanzana { get; set; }
        public string strsepultura { get; set; }
        public string espad_vnom_fallecido { get; set; }
        public string espad_vapellido_paterno_fallecido { get; set; }
        public string espad_vapellido_materno_fallecido { get; set; }
        public string espad_vdni_fallecido { get; set; }
        public DateTime? espad_sfecha_nac_fallecido { get; set; }
        public DateTime? espad_sfecha_fallecido { get; set; }
        public DateTime? espad_sfecha_entierro { get; set; }
        public int espad_inacionalidad { get; set; }
        public string espad_thora { get; set; }
        public string espad_vsolicitante { get; set; }
        public string espad_vnro_doc { get; set; }
        public string espad_vnom_ape_fallecido { get; set; }
        public string CodigoSepultura { get; set; }
        public string strdistrito { get; set; }
        public string strorigenventa { get; set; }
        public string strtiposepultura { get; set; }
        public string strSituacion { get; set; }
        public int cntc_icod_isepultura { get; set; }
        public string cntc_vnumero_contrato { get; set; }
        public string cntc_vnombre_contratante { get; set; }
        public string cntc_vapellido_paterno_contratante { get; set; }
        public string cntc_vapellido_materno_contratante { get; set; }
        public string cntc_vnombre_fallecido { get; set; }
        public string cntc_vapellido_paterno_fallecido { get; set; }
        public string cntc_vapellido_materno_fallecido { get; set; }
        public string disc_vdescripcion { get; set; }
        public DateTime? cntc_sfecha_contrato { get; set; }
        public DateTime? cntc_sfecha_nac_fallecido { get; set; }
        public DateTime? cntc_sfecha_fallecimiento { get; set; }
        public DateTime? cntc_sfecha_entierro { get; set; }
        public string cntc_vcodigo_sepultura { get; set; }
        public int espac_icod_imanzana { get; set; }
        public int espac_isepultura { get; set; }
        public int espac_icod_iplataforma { get; set; }
    }
}
