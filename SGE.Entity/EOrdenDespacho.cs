using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EOrdenDespacho : EAuditoria
    {
        [DataMember]
        public int desac_icod_despacho { get; set; }
        [DataMember]
        public int? desac_ianio { get; set; }
        [DataMember]
        public string desac_vnumero_despacho { get; set; }
        [DataMember]
        public DateTime desac_sfecha_despacho { get; set; }
        [DataMember]
        public int? desac_iid_situacion_despacho { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public Boolean desac_sflag_cerrado { get; set; }
        [DataMember]
        public int? desac_icod_despacho_pedido { get; set; }
        [DataMember]
        public string vNumeroPedido { get; set; }
        [DataMember]
        public Boolean tipo_borden_despacho { get; set; }
        [DataMember]
        public int? tablc_iid_motivo_despacho { get; set; }
        [DataMember]
        public int almac_icod_almacen_salida { get; set; }
        [DataMember]
        public int almac_icod_almacen_salida_OD { get; set; }
        [DataMember]
        public string AlmacenSalida { get; set; }
        [DataMember]
        public int almac_icod_almacen_ingreso { get; set; }
        [DataMember]
        public string AlmacenIngreso { get; set; }
        [DataMember]
        public string desac_vatencion { get; set; }
        [DataMember]
        public string desac_ventregar { get; set; }
        [DataMember]
        public string desac_ventregar_arroba { get; set; }
        [DataMember]
        public string desac_vreferencia { get; set; }
        [DataMember]
        public string desac_vpartida { get; set; }
        [DataMember]
        public int? tranc_icod_transportista { get; set; }
        [DataMember]
        public string Transportista { get; set; }
        [DataMember]
        public string tranc_vnum_matricula { get; set; }
        [DataMember]
        public string tranc_vruc { get; set; }

        [DataMember]
        public string desac_vnumero_parte { get; set; }
        [DataMember]
        public DateTime? desac_sfecha_devolucion { get; set; }
        [DataMember]
        public int? remic_icod_remision { get; set; }
        [DataMember]
        public string desac_vguia_remision { get; set; }
        [DataMember]
        public string desac_vplaca_vehiculo { get; set; }
        [DataMember]
        public Boolean desac_bautoriza_modif_dev { get; set; }
        [DataMember]
        public Boolean desac_bmodi_doc { get; set; }
        [DataMember]
        public decimal KardexXRe { get; set; }
        [DataMember]
        public Boolean desac_bautoriza_modif { get; set; }

        [DataMember]
        public string Descripcion_Modifica { get; set; }
    }
}
