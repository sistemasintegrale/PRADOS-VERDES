using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
   public class EPedidosPVTDetalle:EAuditoria
    {
        public int pdvd_icod_pedido_detalle { get; set; }
        public int pdvd_iid_pedido_detalle { get; set; }
        public int pdvc_icod_pedido { get; set; }
        public int prdc_icod_producto { get; set; }
        public string CodPro { get; set; }
        public string DesLarga { get; set; }
        public string DesCorta { get; set; }
        public string AbreUM { get; set; }
        public decimal pdvd_ncantidad { get; set; }
        public decimal pdvd_nprecio_unitario { get; set; }
        public decimal pdvd_nprecio_total { get; set; }
        public int Indicador { get; set; }

        public decimal PorcentajeIVAP { get; set; }
        public decimal monto_ivap { get; set; }

        /*Datos Planilla Boleta*/
        public int bovd_icod_item_boleta { get; set; }
        public int bovc_icod_boleta { get; set; }
        public int bovd_icod_kardex { get; set; }
        /*Datos Planilla factura*/
        public int favd_icod_item_factura { get; set; }
        public int favc_icod_factura { get; set; }
        public int favd_icod_kardex { get; set; }

    }
}
