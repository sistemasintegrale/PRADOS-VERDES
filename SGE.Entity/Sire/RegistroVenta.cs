using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class RegistroVenta
    {
        public string Id { get; set; }
        public string NumRuc { get; set; }
        public string NomRazonSocial { get; set; }
        public int? PerPeriodoTributario { get; set; }
        public string CodCar { get; set; }
        public string CodTipoCdp { get; set; }
        public string NumSerieCdp { get; set; }
        public string NumCdp { get; set; }
        public decimal? CodTipoCarga { get; set; }
        public decimal? CodSituacion { get; set; }
        public string FecEmision { get; set; }
        public int? CodTipoDocIdentidad { get; set; }
        public string NumDocIdentidad { get; set; }
        public string NomRazonSocialCliente { get; set; }
        public decimal? MtoValFactExpo { get; set; }
        public decimal MtoBiGravada { get; set; }
        public decimal? MtoDsctoBi { get; set; }
        public decimal MtoIgv { get; set; }
        public decimal? MtoDsctoIgv { get; set; }
        public decimal? MtoExonerado { get; set; }
        public decimal? MtoInafecto { get; set; }
        public decimal? MtoIsc { get; set; }
        public decimal? MtoBiIvap { get; set; }
        public decimal? MtoIvap { get; set; }
        public decimal? MtoIcbp { get; set; }
        public decimal? MtoOtrosTrib { get; set; }
        public decimal MtoTotalCp { get; set; }
        public string CodMoneda { get; set; }
        public decimal? MtoTipoCambio { get; set; }
        public decimal? CodEstadoComprobante { get; set; }
        public string DesEstadoComprobante { get; set; }
        public decimal? IndOperGratuita { get; set; }
        public decimal? MtoValorOpGratuitas { get; set; }
        public decimal? MtoValorFob { get; set; }
        public string IndTipoOperacion { get; set; }
        public decimal? MtoPorcParticipacion { get; set; }
        public decimal? MtoValorFobDolar { get; set; }
        public string FecEmisionMod { get; set; }
        public string CodTipoCDPMod { get; set; }
        public string NumSerieCDPMod { get; set; }
        public string NumCDPMod { get; set; }
        public string FecVencPag { get; set; }
    }

 
}
