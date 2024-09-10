using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ECuotasPrestamo : EAuditoria
    {
        public int prtpd_icod_prestamo_det { get; set; }
        public int prtpd_icod_prestamo { get; set; }
        public int prtpd_inro_cuota { get; set; }
        public DateTime prtpd_sfecha_cuota { get; set; }
        public decimal prtpd_nmonto_cuota { get; set; }
        public int prtpd_icod_tipo_cuota { get; set; }
        public int prtpd_icod_situacion { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public int cntc_icod_documento { get; set; }
        public bool prtpd_flag_situacion { get; set; }
        public int prtpd_iusuario_crea { get; set; }
        public DateTime prtpd_sfecha_crea { get; set; }
        public string  prtpd_vpc_crea { get; set; }
        public int prtpd_iusuario_modifica { get; set; }
        public DateTime prtpd_sfecha_modifica { get; set; }
        public string prtpd_vpc_modifica { get; set; }
        public int prtpd_iusuario_elimina { get; set; }
        public DateTime prtpd_sfecha_elimina { get; set; }
        public string prtpd_vpc_elimina { get; set; }
        public string strTipoPago { get; set; }
        public string strSituacion { get; set; }
        public string plnd_vnumero_doc { get; set; }
        public DateTime? plnd_sfecha_doc { get; set; }

        public string strMontoCuota { get { return "S/ " + prtpd_nmonto_cuota.ToString("N2"); } }
    }
}
