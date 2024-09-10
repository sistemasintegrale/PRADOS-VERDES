using System;

namespace SGE.Entity
{
    public class EFacturaVentaElectronica
    {
        public int IdCabecera { get; set; }
        public string idDocumento { get; set; }
        public string StrTipoDoc { get; set; }
        public string fechaEmision { get; set; }
        public DateTime horaEmision { get; set; }
        public string fechaVencimiento { get; set; }
        public string tipoDocumento { get; set; }
        public string moneda { get; set; }
        public string CodMotivoNota { get; set; }
        public string DescripMotivoNota { get; set; }
        public string NroDocqModifica { get; set; }
        public string TipoDocqModifica { get; set; }
        public int cantidadItems { get; set; }
        public string nombreComercialEmisor { get; set; }
        public string nombreLegalEmisor { get; set; }
        public string tipoDocumentoEmisor { get; set; }
        public string nroDocumentoEmisior { get; set; }
        public int CodLocalEmisor { get; set; }
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
        public decimal EstadoFacturacion { get; set; }
        public string EAnulado { get; set; }
        public int EstadoAnulacion { get; set; }
        public string FormaPagoS { get; set; }
        public decimal MontoTotalPago { get; set; }
        public string NroCuota { get; set; }
        public decimal MontoCuota { get; set; }
        public string FechaPago { get; set; }

        /**/
        public string EstadoSunat { get; set; }
        //public int? EstadoSunatInt { get; set; }
        //public int ? EstadoEnvioInt { get; set; }
        public string MensajeError { get; set; }
        public string MensajeRespuesta { get; set; }
        public string CodigoRespuesta { get; set; }
        public int Exito { get; set; }
        public bool indicador_check { get; set; }

        public string tipoDocRef { get; set; }
        public string numDocRef { get; set; }
        public string codigoMotivoRef { get; set; }
        public string desMotivoRef { get; set; }
        public string Mensaje { get; set; }
        public string FEmisionPresentacion { get; set; }
        public int Dias { get; set; }
        public string bovc_vDNI_cliente { get; set; }
        public DateTime? Hora { get; set; }
    }
}