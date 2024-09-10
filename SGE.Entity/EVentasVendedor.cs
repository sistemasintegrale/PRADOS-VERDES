using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EVentasVendedor : EAuditoria
    {
       public DateTime Fecha { get; set; }
       public string TipoDoc { get; set; }
       public string Numero { get; set; }
       public string Cliente { get; set; }
       public string Producto { get; set; }
       public decimal ImporteTotalVenta { get; set; }
       public string Vendedor { get; set; }
       public string Situacion { get; set; }
    }
}
