using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EPedidosPVT:EAuditoria
    {
        public int pdvc_icod_pedido { get; set; }
        public string pdvc_numero_pedido { get; set; }
        public DateTime pdvc_sfecha_pedido { get; set; }
        public int tablc_iid_situación { get; set; }
        public int cliec_icod_cliente { get; set; }
        public string pdvc_vcliente { get; set; }
        public string Situacion { get; set; }
        public string NomCliente { get; set; }
        public string pdvc_vobservaciones { get; set; }
        public string Ruc { get; set; }
    }
}
