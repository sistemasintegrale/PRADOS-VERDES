using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EGuiaRemision : EAuditoria
    {
        public int intCorrelativo { get; set; }
        public int remic_icod_remision { get; set; }
        public string remic_vnumero_remision { get; set; }
        public DateTime remic_sfecha_remision { get; set; }
        public int cliec_icod_cliente { get; set; }
        public string NomClie { get; set; }
        public string remic_vnombre_destinatario { get; set; }
        public string remic_vdireccion_destinatario { get; set; }
        public string remic_vruc_destinatario { get; set; }
        public string remic_vdireccion_procedencia { get; set; }
        public int almac_icod_almacen { get; set; }
        public int tablc_iid_motivo { get; set; }
        public int tranc_icod_transportista { get; set; }
       
        public string remic_vlicencia { get; set; }
        public string remic_vruc_transportista { get; set; }
        public int tablc_iid_situacion_remision { get; set; }
        public string remic_vreferencia { get; set; }
        public int? remic_tipo_documento { get;set; }
        public int ?remic_icod_doc_venta { get; set; }
        public string Vnumero_Documento { get; set; }
        public string str_Tipo_doc { get; set; }
        public string StrSitucion { get; set; }
        public int ubicc_icod_ubicacion { get; set; }
        public string strDesAlmacen { get; set; }
        public string strTransportista { get; set; }
        public DateTime remic_sfecha_inicio { get; set; }


        public string lpedi_Numerolista { get; set; }
        public string perc_vapellidos_nombres { get; set; }
        public string strFormaPago { get; set; }
        public string strMoneda { get; set; }

        public int? almac_icod_almacen_ingreso { get; set; }
        public string strDesAlmaceningreso { get; set; }
        public string evev_vnumero_evento_venta { get; set; }
        public string evev_vnombre_evento_venta { get; set; }
        public string cliec_vcod_cliente { get; set; }

        public int pryc_icod_proyecto { get; set; }
        public int cecoc_icod_centro_costo { get; set;}
		public string remic_vmarca_placa { get; set;}
        public string remic_vcertif_inscripcion { get; set; }

        public string CentroCosto { get; set; }
        public string CodProyecto { get; set; }

        public int intEjercicio { get; set; }
        public int intTipoDoc { get; set; }
        public decimal dcmlTipoCambio { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public string strTipoVenta { get; set; }
        public string favc_vobservacion { get; set; }
    }
}
