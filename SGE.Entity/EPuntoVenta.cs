using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EPuntoVenta:EAuditoria
    {
        public int puvec_icod_punto_venta { get; set; }
        public int puvec_vcod_punto_venta { get; set; }
        public string puvec_vdescripcion { get; set; }
        public int tabl_iid_situacion { get; set; }
        public string Situacion { get; set; }

        public string puvec_vserie_factura { get; set; }
        public int puvec_icorrelativo_factura { get; set; }
        public string puvec_vserie_boleta { get; set; }
        public int puvec_icorrelativo_boleta { get; set; }
        public string puvec_vserie_nota_credito { get; set; }
        public int puvec_icorrelativo_nota_credito { get; set; }
    }
}
