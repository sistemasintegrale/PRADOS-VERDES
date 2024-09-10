using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ECuotaFoma : EAuditoria
    {
        public int ccf_icod_cuota { get; set; }
        public int cntc_icod_contrato { get; set; }
        public int? rc_icod_recibo { get; set; }
        public decimal ccf_nmonto_pagar { get; set; }
        public decimal? ccf_nmonto_pagado { get; set; }
        public DateTime? cff_sfecha_pago { get; set; }
        public string strNivel { get; set; }
        public string strNumRecibo { get; set; }
        public DateTime? dtFechaRecibo { get; set; }
        public string strEstado { get { return ccf_nmonto_pagar == ccf_nmonto_pagado ? "CANCELADO" :"GENERADO"; } }
        public bool select { get; set; }
        public int ? ccf_icod_nivel { get; set; }
    }
}
