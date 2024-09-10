using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ESunatResumenDocumentosExcel 
    {
        public int IdItems { get; set; }
        public int IdCabecera { get; set; }
        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public string IdDocumento { get; set; }
        public string Moneda { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal Gravadas { get; set; }
        public decimal Inafectas { get; set; }
        public string NroDoc { get; set; }
        public DateTime Fecha { get; set; }
    }
}
