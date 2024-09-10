using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EKardex : EAuditoria
    {
        public int kardc_icod_correlativo { get; set; }
        public int kardc_ianio { get; set; }
        public DateTime kardc_fecha_movimiento { get; set; }
        public int almac_icod_almacen { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal kardc_icantidad_prod { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public string kardc_numero_doc { get; set; }
        public int kardc_tipo_movimiento { get; set; }
        public int kardc_iid_motivo { get; set; }
        public string kardc_beneficiario { get; set; }
        public string kardc_observaciones { get; set; }
        public bool kardc_flag_pase { get; set; }
        /**/
        public decimal dblIngreso { get; set; }
        public decimal dblSalida { get; set; }
        public decimal dblSaldoAnterior { get; set; } 
        public decimal dblSaldo { get; set; }
        public string strDocumento { get; set; }
        public string strMotivo { get; set; }
        public string strAlmacen { get; set; }
        public string strCodProducto { get; set; }
        public string strProducto { get; set; }
        public string strUnidadMedida { get; set; }
        public string strTipoDoc { get; set; }

        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strSubCategoriaDos { get; set; }
        public string strEditorial { get; set; }

        public string strUbicacion { get; set; }

        public decimal dblSaldoReal { get; set; }
        public int? almac_icod_pvt { get; set; }

        public int AlmacenContable { get; set; }
        
    }
}
