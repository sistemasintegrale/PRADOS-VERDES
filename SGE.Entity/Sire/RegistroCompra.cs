using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class RegistroCompra
    {
        public string Id { get; set; }
        public string NumRuc { get; set; }
        public string NomRazonSocial { get; set; }
        public string CodCar { get; set; }
        public string CodTipoCdp { get; set; }
        public string DesTipoCdp { get; set; }
        public string NumSerieCdp { get; set; }
        public long NumCdp { get; set; }
        public string FecEmision { get; set; }
        public string FecVencPag { get; set; }
        public object NumCdpRangoFinal { get; set; }
        public long CodTipoDocIdentidadProveedor { get; set; }
        public string NumDocIdentidadProveedor { get; set; }
        public string NomRazonSocialProveedor { get; set; }
        public long CodTipoCarga { get; set; }
        public long CodSituacion { get; set; }
        public string CodMoneda { get; set; }
        public long CodEstadoComprobante { get; set; }
        public string DesEstadoComprobante { get; set; }
        public string IndOperGratuita { get; set; }
        public string CodTipoMotivoNota { get; set; }
        public object DesTipoMotivoNota { get; set; }
        public object IndEditable { get; set; }
        public long PerTributario { get; set; }
        public long? NumInconsistencias { get; set; }
        public object IndInfIncompleta { get; set; }
        public object IndModificadoContribuyente { get; set; }
        public object PlazoVisualizacion { get; set; }
        public object IndDetraccion { get; set; }
        public long IndIncluExcluCar { get; set; }
        public object PorParticipacion { get; set; }
        public string CodBbss { get; set; }
        public object CodIdProyecto { get; set; }
        public object AnnCdp { get; set; }
        public object CodDepAduanera { get; set; }
        public long IndFuenteCp { get; set; }
        public List<string> LiscodInconsistencia { get; set; }
        public List<string> LisNumCasilla { get; set; }
        public object PorTasaRetencion { get; set; }
        public object DesMsjOriginal { get; set; }
        public object NumCarIndIe { get; set; }
        public long NumCorrelativo { get; set; }
        public decimal? PorTasaIgv { get; set; }
        public object ArchivoCarga { get; set; }
        public TipoCambio TipoCambio { get; set; }
        public Montos Montos { get; set; }
        public List<LisDocumentosMod> LisDocumentosMod { get; set; }
        public object CamposLibres { get; set; }
        public Auditoria Auditoria { get; set; }
    }
    public partial class TipoCambio
    {
        public long IndCargaTipoCambio { get; set; }
        public decimal? MtoCambioMonedaExtranjera { get; set; }
        public decimal? MtoCambioMonedaDolares { get; set; }
        public decimal? MtoTipoCambio { get; set; }
    }

    public partial class LisDocumentosMod
    {
        public string FecEmisionMod { get; set; }
        public string CodTipoCdpMod { get; set; }
        public string NumSerieCdpMod { get; set; }
        public long NumCdpMod { get; set; }
    }
    public partial class Auditoria
    {
        public string CodUsuRegis { get; set; }
        public DateTimeOffset FecRegis { get; set; }
        public string CodUsuModif { get; set; }
        public DateTimeOffset FecModif { get; set; }
    }

    public partial class Montos
    {
        public decimal? MtoBiGravadaDg { get; set; }
        public decimal? MtoIgvIpmDg { get; set; }
        public decimal? MtoBiGravadaDgng { get; set; }
        public decimal? MtoIgvIpmDgng { get; set; }
        public decimal? MtoBiGravadaDng { get; set; }
        public decimal? MtoIgvIpmDng { get; set; }
        public decimal? MtoValorAdqNg { get; set; }
        public decimal? MtoIcbp { get; set; }
        public decimal? MtoOtrosTrib { get; set; }
        public decimal? MtoTotalCp { get; set; }
        public decimal? MtoIsc { get; set; }
        public decimal? MtoImb { get; set; }
        public decimal? MtoBiGravadaDgOriginal { get; set; }
        public decimal? MtoIgvIpmDgOriginal { get; set; }
        public Auditoria Auditoria { get; set; }
    }
}
