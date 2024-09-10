using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public partial class Propuesta<T>
    {
        public Paginacion Paginacion { get; set; }
        public List<T> Registros { get; set; }
        public Dictionary<string, double> Totales { get; set; }
    }

    public partial class Paginacion
    {
        public long? Page { get; set; }
        public long? PerPage { get; set; }
        public long? TotalRegistros { get; set; }
    }

   
}
