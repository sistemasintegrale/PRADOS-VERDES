using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EListaPrecio : EAuditoria
    {
        public int lprecc_icod_precio { get; set; }
        public int prdc_icod_producto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal lprecc_nmonto_unitario { get; set; }
        public Boolean lprecc_indicador_rango { get; set; }
        public string IndicadorRango { get; set; }
    }
}
