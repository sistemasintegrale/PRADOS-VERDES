using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EDetalleFacOC:EAuditoria
    {
        public string fcoc_num_doc { get; set; }
        public string proc_vnombrecompleto { get; set; }
        public DateTime fcoc_sfecha_doc { get; set; }
        public decimal fcod_ncantidad { get; set; }
    }
}
