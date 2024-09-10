﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EBoletaCab : EAuditoria
    {
        public int bovc_icod_boleta { get; set; }
        public string bovc_vnumero_boleta { get; set; }
        public DateTime bovc_sfecha_boleta { get; set; }
        public DateTime bovc_sfecha_vencim_boleta { get; set; }
        public Int64 doxcc_icod_correlativo { get; set; }
        public int cliec_icod_cliente { get; set; }
        public string cliec_vdireccion_cliente { get; set; }
        public string cliec_cruc { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public int tablc_iid_forma_pago { get; set; }
        public int tablc_iid_situacion { get; set; }
        public decimal bovc_npor_imp_igv { get; set; }
        public decimal bovc_nmonto_neto { get; set; }
        public decimal bovc_nmonto_imp { get; set; }
        public decimal bovc_nmonto_total { get; set; }
        public decimal bovc_nmonto_descuento { get; set; }
        public bool bovc_flag_estado { get; set; }
        public string bovc_vobservacion { get; set; }
        public string bovc_vnombre_cliente { get; set; }
        public int ubicc_icod_ubicacion { get; set; }
        public int cliec_nnumero_dias { get; set; }
        public string remic_vnumero_remision { get; set; }
        public int? remic_icod_remision { get; set; }
        public bool bfavc_bafecto_igv { get; set; }
        /**/
        public string cliec_vcod_cliente { get; set; }
        public string cliec_vnombre_cliente { get; set; }
        public string strDistritoCliente { get; set; }
        public string strFormaPago { get; set; }
        public string strSituacion { get; set; }
        public string strMoneda { get; set; }
        public string strTelefonoCliente { get; set; }

        public Boolean bovc_bind_arroz { get; set; }
        public decimal bovc_npor_imp_ivap { get; set; }
        public decimal bovc_nmonto_ivap { get; set; }

        /*Planilla Venta Diaria*/
        public int? orpc_iid_orden_trabajo { get; set; }
        public int? prsc_iid_presupuesto { get; set; }
        public int bovc_icod_cliente { get; set; }
        public string clic_vcod_cliente { get; set; }
        public string bovc_vdireccion_cliente { get; set; }
        public string bovc_vDNI_cliente { get; set; }
        public string bovc_vruc { get; set; }
        public int bovc_iid_vehiculo { get; set; }
        public string bovc_vkilometraje { get; set; }
        public string bovc_vcolor { get; set; }
        public Int64 dxcc_iid_doc_por_cobrar { get; set; }
        public string strDesCliente { get; set; }
        public string strNroOrdenTrabajo { get; set; }
        public decimal desc_nFactura_porc { get; set; }
        public decimal desc_nMonto { get; set; }
        public decimal nmonto_nDescuento { get; set; }
        public decimal nmonto_nSubTotal { get; set; }
        public int pdvc_icod_pedido { get; set; }
        public string pdvc_numero_pedido { get; set; }
        public string cliec_vnumero_doc_cli { get; set; }

        public int vendc_icod_vendedor { get; set; }
        public int bovc_icod_pvt { get; set; }
        public int anio_icod_anio { get; set; }
        public string NomVendedor { get; set; }
        public decimal bovc_nmonto_neto_ivap { get; set; }
        public decimal bovc_nmonto_neto_exo { get; set; }

        public int cntc_icod_contrato { get; set; }
        public int cntc_icod_contrato_cuotas { get; set; }
        public string cntc_vnumero_contrato { get; set; }
        public int cntc_inro_cuotas { get; set; }
        public int cntc_icod_contrato_cuotas_anterior { get; set; }

        /*Datos Factura Electronica*/
        public int IdCabecera { get; set; }
        public string idDocumento { get; set; }
        public string StrTipoDoc { get; set; }
        public string fechaEmision { get; set; }
        public string horaEmision { get; set; }
        public string fechaVencimiento { get; set; }
        public string tipoDocumento { get; set; }
        public string moneda { get; set; }
        public string CodMotivoNota { get; set; }
        public string DescripMotivoNota { get; set; }
        public string NroDocqModifica { get; set; }
        public string TipoDocqModifica { get; set; }
        public int cantidadItems { get; set; }
        public string nombreComercialEmisor { get; set; }
        public string direccionEmisor { get; set; }
        public string nombreLegalEmisor { get; set; }
        public string tipoDocumentoEmisor { get; set; }
        public string nroDocumentoEmisior { get; set; }
        public string CodLocalEmisor { get; set; }
        public string nroDocumentoReceptor { get; set; }
        public string tipoDocumentoReceptor { get; set; }
        public string nombreLegalReceptor { get; set; }
        public string direccionReceptor { get; set; }
        public int CodMotivoDescuento { get; set; }
        public decimal PorcDescuento { get; set; }
        public decimal MontoDescuentoGlobal { get; set; }
        public decimal BaseMontoDescuento { get; set; }
        public decimal MontoTotalImpuesto { get; set; }
        public decimal MontoGravadasIGV { get; set; }
        public int CodigoTributo { get; set; }
        public decimal MontoExonerado { get; set; }
        public decimal MontoInafecto { get; set; }
        public decimal MontoGratuitoImpuesto { get; set; }
        public decimal MontoBaseGratuito { get; set; }
        public decimal totalIgv { get; set; }
        public decimal MontoGravadosISC { get; set; }
        public decimal totalIsc { get; set; }
        public decimal MontoGravadosOtros { get; set; }
        public decimal totalOtrosTributos { get; set; }
        public decimal TotalValorVenta { get; set; }
        public decimal TotalPrecioVenta { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal MontoTotalCargo { get; set; }
        public decimal MontoTotalAnticipo { get; set; }
        public decimal ImporteTotalVenta { get; set; }
        public int doc_icod_documento { get; set; }
        public int EstadoFacturacion { get; set; }
        public string EAnulado { get; set; }
        public int EstadoAnulacion { get; set; }
        public string EstadoSunat { get; set; }
        public string FormaPagoS { get; set; }
        public decimal MontoTotalPago { get; set; }
        public string NroCuota { get; set; }
        public decimal MontoCuota { get; set; }
        public string FechaPago { get; set; }
    }
}
