using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EPagosCuotas : EAuditoria
    {
        public int pgc_icod_pago { get; set; }
        public int cntc_icod_contrato_cuotas { get; set; }
        public decimal pgc_nmonto_pago { get; set; }
        public DateTime pgc_sfecha_pago { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public int cntc_icod_documento { get; set; }
        public int pgc_iusuario_crea { get; set; }
        public DateTime pgc_sfecha_crea { get; set; }
        public string pgc_vpc_crea { get; set; }
        public int pgc_iusuario_modifica { get; set; }
        public DateTime pgc_sfecha_modifica { get; set; }
        public string pgc_vpc_modifica { get; set; }
        public bool pgc_flag_estado { get; set; }
        public string plnd_vnumero_doc { get; set; }
        public string strTipoDoc { get; set; }
        public DateTime plnd_sfecha_doc { get; set; }
        public decimal pgc_nmonto_pago_mora { get; set; }
    }
}
