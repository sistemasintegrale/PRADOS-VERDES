﻿using System.Collections.Generic;

namespace SGE.Entity
{
    public class EResumenElectronico
    {

        public int CodigoEstadoItem { get; set; }
        public string DocumentoRelacionado { get; set; }
        public string IdDocumento { get; set; }
        public string NroDocumentoReceptor { get; set; }
        public string TipoDocumentoReceptor { get; set; }
        public string TipoDocumentoRelacionado { get; set; }
        public int Id { get; set; }
        public string Serie { get; set; }
        public string TipoDocumento { get; set; }
        public int CorrelativoFin { get; set; }
        public int CorrelativoInicio { get; set; }
        public decimal Exoneradas { get; set; }
        public decimal Exportacion { get; set; }
        public decimal Gratuitas { get; set; }
        public decimal Gravadas { get; set; }
        public decimal Inafectas { get; set; }
        public string Moneda { get; set; }
        public decimal TotalDescuentos { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal TotalIsc { get; set; }
        public decimal TotalOtrosImpuestos { get; set; }

        public decimal TotalVenta { get; set; }
    }
}
