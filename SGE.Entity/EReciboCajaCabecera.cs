using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EReciboCajaCabecera :EAuditoria
    {
        public string strNumContrato { get; set; }

        public int rc_icod_recibo { get; set; }
        public string rc_vnumero { get; set; }
        public int rc_icod_cliente { get; set; }
        public DateTime rc_sfecha_recibo { get; set; }
        public int rc_itipo_moneda { get; set; }
        public int rc_isituacion { get; set; }
        public int rc_icod_contrato { get; set; }
        public decimal rc_dmonto_total { get; set; }
        public string rc_vnombre_cliente { get; set; }
        public string rc_vdireccion_cliente { get; set; }
        public string rc_vnro_doc_cliente { get; set; }
        public string rc_icod_foma_anulado { get; set; }

        //Datos Extra

        public string strNroDocCliente { get; set; }
        public string strCliente { get; set; }
        public string cliec_vnro_telefono { get; set; }
        public string cliec_vdireccion_cliente { get; set; }
        public string strContrato { get; set; }
        public string strRucCliente { get; set; }
        public DateTime? cntc_sfecha_contrato { get; set; }
    }
}
