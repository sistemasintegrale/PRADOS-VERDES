using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EOrdenCompraDetalle :EAuditoria
    {
        [DataMember]
        public int ocod_icod_detalle_oc { get; set; }
        [DataMember]
        public int ococ_icod_orden_compra { get; set; }
        [DataMember]
        public int ocod_iitem { get; set; }
        [DataMember]
        public int? prdc_icod_producto { get; set; }
        [DataMember]
        public int? mnoc_icod_mano_obra { get; set; }
        [DataMember]
        public string mnoc_vdescrip_corta { get; set; }
        [DataMember]
        public string mnoc_vdescrip_larga { get; set; }
        [DataMember]
        public decimal ocod_ncantidad { get; set; }
        public decimal ocod_ncantidad_facturada { get; set;}
        public decimal ocod_ncantidad_saldo { get; set; }
        [DataMember]
        public decimal ocod_ncunitario { get; set; }
        [DataMember]
        public decimal ocod_nmonto_total { get; set; }
        [DataMember]
        public long kardc_iid_correlativo { get; set; }
        [DataMember]
        public Boolean ocod_flag_estado { get; set; }
        [DataMember]
        public int operacion { get; set; }

        [DataMember]
        public decimal orpdi_nprecio_soles { get; set; }
         [DataMember]
        public decimal orpdi_nprecio_dolares { get; set; }
        
        [DataMember]
        public string strCodigoProducto { get; set; }
        [DataMember]
        public string strDescProducto { get; set; }
        [DataMember]
        public string strMedida { get; set; }
        [DataMember]
        public string strAbrevUniMed { get; set; }
        [DataMember]
        public string famic_vdescripcion { get; set; }
        [DataMember]
        public string famic_vabreviatura { get; set; }
        [DataMember]
        public string famid_vdescripcion { get; set; }
        [DataMember]
        public string famid_vabreviatura { get; set; }
        [DataMember]
        public decimal ocod_ncantidad_ant { get; set; }
        public string ocod_vdescripcion { get; set; }
		public decimal ocod_ndescuento_item { get; set; }
		public string ocod_vdireccion_documento { get; set; }
        public string ocod_vcaracteristicas { get; set; }
        public DateTime ocod_dfecha_entrega { get; set; }
        public string prdc_vcodigo_fabricante { get; set; }
    }
}
