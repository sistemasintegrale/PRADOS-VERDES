using System.Collections.Generic;

namespace SGE.Entity.Sire
{
    public class ResumenDocumentos
    {
        public List<RegistroResumen> Registros { get; set; }
        public Totales Totales { get; set; }
    }

    public class RegistroResumen
    {
        public string CodTipoCdp { get; set; }
        public string DesCodTipoCdp { get; set; }
        public decimal CntDocumentos { get; set; }
        public decimal MtoIsc { get; set; }
        public decimal MtoIcbper { get; set; }
        public decimal MtoOtrosTrib { get; set; }
        public decimal MtoTotalCp { get; set; }
        public decimal MtoBiGravadoDg { get; set; }
        public decimal MtoIgvIpmDg { get; set; }
        public decimal MtoBiGravadoDgng { get; set; }
        public decimal MtoIgvIpmDgng { get; set; }
        public decimal MtoBiGravadoDng { get; set; }
        public decimal MtoIgvIpmDng { get; set; }
        public decimal MtoValorAdqNg { get; set; }
        public decimal MtoTotalImb { get; set; } 
        public decimal MtoTotalValFactExpo { get; set; }
        public decimal MtoTotalBiGravada { get; set; }
        public decimal MtoTotalDsctoBi { get; set; }
        public decimal MtoTotalIgv { get; set; }
        public decimal MtoTotalDsctoIgv { get; set; }
        public decimal MtoTotalExonerado { get; set; }
        public decimal MtoTotalInafecto { get; set; }
        public decimal MtoTotalBiIvap { get; set; }
        public decimal MtoTotalIvap { get; set; }
    }

    public class Totales
    {
        public decimal CntDocumentos { get; set; }
        public decimal MtoBiGravadoDg { get; set; }
        public decimal MtoIgvIpmDg { get; set; }
        public decimal MtoBiGravadoDgng { get; set; }
        public decimal MtoIgvIpmDgng { get; set; }
        public decimal MtoBiGravadoDng { get; set; }
        public decimal MtoIgvIpmDng { get; set; }
        public decimal MtoValorAdqNg { get; set; }
        public decimal MtoIsc { get; set; }
        public decimal MtoIcbper { get; set; }
        public decimal MtoOtrosTribCargos { get; set; }
        public decimal MtoTotalCp { get; set; }
        public decimal MtoImb { get; set; }
        public decimal MtoValFactExpo { get; set; }
        public decimal MtoDsctoBi { get; set; }
        public decimal MtoDsctoIgv { get; set; }
        public decimal MtoExonerado { get; set; }
        public decimal MtoInafecto { get; set; }
        public decimal MtoBiIvap { get; set; }
        public decimal MtoIvap { get; set; }
        public decimal MtoBiGravada { get; set; }
        public decimal MtoIgv { get; set; }
    }
}
