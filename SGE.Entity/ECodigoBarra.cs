using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class ECodigoBarra:EAuditoria
    {
        public int codb_icod_codigo_barra { get; set; }
        public int prdc_icod_producto { get; set; }
        public string codb_iid_codigo_barra { get; set; }
        public string DesProducto { get; set; }
        public int Indicador { get; set; }
    }
}
