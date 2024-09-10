using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EPagoFomaFinanciamiento
    {
        public int pgs_icod_pagos { get; set; }
        public int pgs_icod_contrato { get; set; }
        public decimal pgs_nmonto { get; set; }
        public decimal pgs_nmonto_pagado { get; set; }
        public int pgs_itipo { get; set; }
        public int intusuario { get; set; }
        public string pgs_vpc { get; set; }
        public DateTime? pgs_sfecha_pago { get; set; }
        public string strTipoPago { get { return pgs_itipo == 12493 ? "FOMA" : "FINANCIAMIENTO"; } }
        public int? rc_icod_recibo { get; set; }
        public string rc_vnumero { get; set; }
    }
}
