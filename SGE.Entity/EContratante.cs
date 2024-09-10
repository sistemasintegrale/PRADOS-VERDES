using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EContratante
    {
        public int cntcc_icod_contratante { get; set; }
        public int cntc_icod_contrato { get; set; }
        public int cntcc_iid_contratante { get; set; }
        public string cntcc_vnombre_contratante { get; set; }
        public string cntcc_vapellido_paterno_contratante { get; set; }
        public string cntcc_vapellido_materno_contratante { get; set; }
        public string cntcc_vdni_contratante { get; set; }
        public string cntcc_vruc_contratante { get; set; }
        public DateTime? cntcc_sfecha_nacimineto_contratante { get; set; }
        public string cntcc_vtelefono_contratante { get; set; }
        public string cntcc_vdireccion_correo_contratante { get; set; }
        public string cntcc_vdireccion_contratante { get; set; }
        public int cntcc_inacionalidad_contratante { get; set; }
        public int cntcc_itipo_documento_contratante { get; set; }
        public int cntcc_iusuario_crea { get; set; }
        public DateTime cntcc_sfecha_crea { get; set; }
        public string cntcc_vpc_crea { get; set; }
        public int cntcc_iusuario_modifica { get; set; }
        public DateTime cntcc_sfecha_modifica { get; set; }
        public string cntcc_vpc_modifica { get; set; }
        public bool cntcc_bflag_estado { get; set; }
        public bool cntcc_bflag_seleccion { get; set; }
        public string strNombreCompleto { get; set; }
    }
}
