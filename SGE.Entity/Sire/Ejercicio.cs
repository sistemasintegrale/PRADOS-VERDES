using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class Ejercicio
    {
        public string numEjercicio { get; set; }
        public string desEstado { get; set; }
        
        public List<Periodo> lisPeriodos { get; set; }
    }

   
}
