using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EReciboCajaDetalle : EAuditoria
    {
        public int rcd_icod_recibo { get; set; }
        public int rcd_inro_item { get; set; }
        public int rc_icod_recibo { get; set; }
        public int rc_itipo_pago { get; set; }
        public int rcd_icantidad { get; set; }
        public decimal rcd_dprecio_unit { get; set; }
        public decimal rcd_dprecio_total { get; set; }
        public int? ccf_icod_cuota { get; set; }
        public string strNivel { get; set; }

        //INdicador  1 nuevo / 2 modificar
        public int TipoOperacion { get; set; }
        public string strDescripcion { get; set; }
    }
}
