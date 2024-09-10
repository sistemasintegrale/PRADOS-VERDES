using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class DocumentoComparacion
    {
        public string Ruc { get; set; }
        public string TipoDocumento { get; set; }
        public string Serie { get; set; }
        public string Correlativo { get; set; }
        public string RucProveedor { get; set; }
        public string Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaVencimiento { get; set; }
        public string Moneda { get; set; }
        public decimal? BaseImponible { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? NoGravado { get; set; }
        public decimal? MontoTotal { get; set; }
        public string TipoDocumentoReferencia { get; set; }
        public string SerieDocumentoReferencia { get; set; }
        public string NumeroDocumentoReferencia { get; set; }
        public decimal? OtrosImpuestos { get; set; }
    }
}
