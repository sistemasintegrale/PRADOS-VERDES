using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class ECaja: EAuditoria
    {
        public int cajac_icod_caja { get; set; }
        public int cajac_vcod_caja { get; set; }
        public int puvec_icod_punto_venta { get; set; }
        public int? puvec_vcod_punto_venta { get; set; }
        public string puvec_vdescripcion { get; set; }
        public string cajac_vdescripcion { get; set; }
        public int? cajac_inro_serie_factura { get; set; }
        public int? cajac_inro_serie_boleta { get; set; }
        public int? cajac_inro_serie_nota_credito { get; set; }
        public int? cajac_icorrelativo_factura { get; set; }
        public int? cajac_icorrelativo_boleta { get; set; }
        public int? cajac_icorrelativo_nota_credito { get; set; }
        public int? cajac_iNumCorrelTck { get; set; }
        public Boolean cajac_flag_estado { get; set; }

        //campo de tiketera
        public string cajac_vNombreLocal { get; set; }
        public string cajac_vDirecLocal { get; set; }
        public string cajac_vNumeroSerie { get; set; }
        public string cajac_vSerieImpres { get; set; }
    }
}
