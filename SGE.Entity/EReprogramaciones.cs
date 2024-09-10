using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EReprogramaciones : EAuditoria
    {
        

        public int cntcr_icod_reprogracion { get; set; }
        public int cntcr_iid_reprogramacion { get; set; }
        public int cntc_icod_contrato { get; set; }
        public int cntcr_inro_cuotas { get; set; }
        public DateTime cntcr_sfecha_cuota { get; set; }
        public decimal cntcr_nmonto_cuota { get; set; }
        public int cntcr_icod_situacion { get; set; }
        public int cntcr_iusuario_crea { get; set; }
        public DateTime cntcr_sfecha_crea { get; set; }
        public string cntcr_vpc_crea { get; set; }
        public int cntcr_iusuario_modifica { get; set; }
        public DateTime cntcr_sfecha_modifica { get; set; }
        public string cntcr_vpc_modifica { get; set; }
        public bool cntcr_flag_estado { get; set; }
        public decimal cntcr_nmonto_cuota_total { get; set; }
        public string strSituacion { get; set; }
        public decimal cntcr_nmonto_total { get; set; }
        public decimal cntcr_nvariacion_interes { get; set; }
        public decimal cntcr_nmonto_financiamiento { get; set; }
        public int cntcr_iplan { get; set; }
        public string cntcr_vobservaciones { get; set; }
        public decimal cntcr_nmonto_saldo_anterior { get; set; }
    }
}
