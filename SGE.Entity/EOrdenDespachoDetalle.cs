using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
   public class EOrdenDespachoDetalle : EAuditoria
    {
        [DataMember]
        public int ddeac_icod_detalle_despacho { get; set; }
        [DataMember]
        public int desac_icod_despacho { get; set; }
        [DataMember]
        public short ddeac_inro_item { get; set; }
        [DataMember]
        public int pespc_icod_producto_especifico { get; set; }
        [DataMember]
        public string Producto { get; set; }
        [DataMember]
        public string UME { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Generico { get; set; }
        [DataMember]
        public decimal pespc_npeso_unitario { get; set; }
        [DataMember]
        public decimal ddeac_ncantidad_despachada { get; set; }
        [DataMember]
        public decimal? ddeac_ncantidad_devuelta { get; set; }
        [DataMember]
        public decimal ddeac_ncantidad_devuelta_ant { get; set; }
        [DataMember]
        public decimal orden_ncantidad_ant { get; set; }
        [DataMember]
        public int almac_icod_almacen_devuelta { get; set; }
        [DataMember]
        public string AlmacenDevuelta { get; set; }
        [DataMember]
        public long? kardc_iid_correlativo_despachada { get; set; }
        [DataMember]
        public long? kardc_iid_correlativo_ingreso_transito { get; set; }
        [DataMember]
        public long kardc_iid_correlativo_devuelta_salida_transito { get; set; }
        [DataMember]
        public long kardc_iid_correlativo_devuelta_ingreso_almacen { get; set; }
        [DataMember]
        public DateTime? ddeac_sfecha_devolucion { get; set; }
        [DataMember]
        public int? tablc_iid_sit_item_ord_despacho { get; set; }
        [DataMember]
        public decimal ddeac_npeso_total_item { get; set; }
        [DataMember]
        public int? usuario { get; set; }
        [DataMember]
        public string pc { get; set; }
        [DataMember]
        public int operacion { get; set; }

        [DataMember]
        public decimal ddeac_ncantidad_modificado { get; set; }
        [DataMember]
        public decimal ddeac_nProducto_Dev { get; set; }


        [DataMember]
        public Boolean sflag_comp_prod { get; set; }
    }
}
