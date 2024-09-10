using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EEspacios : EAuditoria
    {
        public int espac_iid_iespacios { get; set; }
        public string espac_icod_vespacios { get; set; }
        public int espac_icod_iplataforma { get; set; }
        public int espac_isepultura { get; set; }
        public int espac_icod_imanzana { get; set; }
        public int espac_icod_inivel { get; set; }
        public int espac_icod_isituacion { get; set; }
        public int espac_icod_iestado { get; set; }
        public string strplataforma { get; set; }
        public string strmanzana { get; set; }
        public string strnivel { get; set; }
        public string strsituacion { get; set; }
        public string strestado { get; set; }
        public string strsepultura { get; set; }
        public string codigo { get; set; }
    }
}
