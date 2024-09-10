using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class SireRequest
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string operacion { get; set; }
        public string periodo { get; set; }
        public string numeroTicket { get; set; }
        public string nombreArchivo { get; set; }
        public int page { get; set; }
    }
}
