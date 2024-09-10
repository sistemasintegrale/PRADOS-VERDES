using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EListaPrecioDetalle : EAuditoria
    {
        public int lprecd_icod_precio_detalle { get; set; }
        public int lprecc_icod_precio { get; set; }
        public int lprecd_icantidad_inicial { get; set; }
        public int lprecd_icantidad_final { get; set; }
        public decimal lprecd_nmonto_unitario { get; set; }
    }
}
