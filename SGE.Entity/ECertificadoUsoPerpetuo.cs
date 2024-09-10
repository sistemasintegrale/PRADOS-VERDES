using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity
{
    public class ECertificadoUsoPerpetuo : EAuditoria
    {
        public int cup_icod { get; set; }
        public int cntc_icod_contrato { get; set; }
        public string cup_vnumero { get; set; }
        public DateTime cup_sfecha_emision { get; set; }
        public DateTime? cup_sfecha_entrega { get; set; }
        public int cup_isituacion { get; set; }
        public bool cup_bautorizado { get; set; }
        public int? cntc_itipo_sepultura { get; set; }
        public int? cntc_icod_plataforma { get; set; }
        public int? cntc_icod_manzana { get; set; }
        public int? cntc_icod_isepultura { get; set; }
        public string strnivel { get; set; }
        public string cntc_vdocumento_contratante { get; set; }
        public string strNombreContratante { get; set; }
        public DateTime? cntc_sfecha_contrato { get; set; }
        public string cntc_vnumero_contrato { get; set; }
        public string strAutorizado { get { return cup_bautorizado ? "SI" : "NO";  } }
    }
}
