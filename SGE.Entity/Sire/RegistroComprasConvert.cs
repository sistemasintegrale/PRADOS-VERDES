using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.Entity.Sire
{
    public class RegistroComprasDTO
    {
        public string Id { get; set; }
        public string NumRuc { get; set; }
        public string NomRazonSocial { get; set; }
        public string CodCar { get; set; }
        public string CodTipoCdp { get; set; }
        public string DesTipoCdp { get; set; }
        public string NumSerieCdp { get; set; }
        public string NumCdp { get; set; }
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
        //public TipoCambio TipoCambio { get; set; }
        public long IndCargaTipoCambio { get; set; }
        public decimal? MtoCambioMonedaExtranjera { get; set; }
        public decimal? MtoCambioMonedaDolares { get; set; }
        public decimal? MtoTipoCambio { get; set; }
        //public Montos Montos { get; set; }
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
        //public List<LisDocumentosMod> LisDocumentosMod { get; set; }
        public string FecEmisionMod { get; set; }
        public string CodTipoCdpMod { get; set; }
        public string NumSerieCdpMod { get; set; }
        public long NumCdpMod { get; set; }
        public object CamposLibres { get; set; }
        public string NumeroDetraccion { get; set; }
        public DateTime? FechaDetraccion { get; set; }
    }

    public class RegistroComprasConvert
    {

        public static List<RegistroComprasDTO> Convert(List<RegistroCompra> listaOrigen)
        {
            var lista = new List<RegistroComprasDTO>();
            listaOrigen.ForEach(x =>
            {
                lista.Add(new RegistroComprasDTO
                {
                    Id = x.Id,
                    NumRuc = x.NumRuc,

                    NomRazonSocial = x.NomRazonSocial,
                    CodCar = x.CodCar,
                    CodTipoCdp = x.CodTipoCdp,
                    DesTipoCdp = x.DesTipoCdp,
                    NumSerieCdp = x.NumSerieCdp,
                    NumCdp = x.NumCdp.ToString(),
                    FecEmision = x.FecEmision,
                    FecVencPag = x.FecVencPag,
                    NumCdpRangoFinal = x.NumCdpRangoFinal,
                    CodTipoDocIdentidadProveedor = x.CodTipoDocIdentidadProveedor,
                    NumDocIdentidadProveedor = x.NumDocIdentidadProveedor,
                    NomRazonSocialProveedor = x.NomRazonSocialProveedor,
                    CodTipoCarga = x.CodTipoCarga,
                    CodSituacion = x.CodSituacion,
                    CodMoneda = x.CodMoneda,
                    CodEstadoComprobante = x.CodEstadoComprobante,
                    DesEstadoComprobante = x.DesEstadoComprobante,
                    IndOperGratuita = x.IndOperGratuita,
                    CodTipoMotivoNota = x.CodTipoMotivoNota,
                    DesTipoMotivoNota = x.DesTipoMotivoNota,
                    IndEditable = x.IndEditable,
                    PerTributario = x.PerTributario,
                    NumInconsistencias = x.NumInconsistencias,
                    IndInfIncompleta = x.IndInfIncompleta,
                    IndModificadoContribuyente = x.IndModificadoContribuyente,
                    PlazoVisualizacion = x.PlazoVisualizacion,
                    IndDetraccion = x.IndDetraccion,
                    IndIncluExcluCar = x.IndIncluExcluCar,
                    PorParticipacion = x.PorParticipacion,
                    CodBbss = x.CodBbss,
                    CodIdProyecto = x.CodIdProyecto,
                    AnnCdp = x.AnnCdp,
                    CodDepAduanera = x.CodDepAduanera,
                    IndFuenteCp = x.IndFuenteCp,
                    LiscodInconsistencia = x.LiscodInconsistencia,
                    LisNumCasilla = x.LisNumCasilla,
                    PorTasaRetencion = x.PorTasaRetencion,
                    DesMsjOriginal = x.DesMsjOriginal,
                    NumCarIndIe = x.NumCarIndIe,
                    NumCorrelativo = x.NumCorrelativo,
                    PorTasaIgv = x.PorTasaIgv,
                    ArchivoCarga = x.ArchivoCarga,
                    // TipoCambio TipoCambio 
                    IndCargaTipoCambio = x.TipoCambio.IndCargaTipoCambio,
                    MtoCambioMonedaExtranjera = x.TipoCambio.MtoCambioMonedaExtranjera,
                    MtoCambioMonedaDolares = x.TipoCambio.MtoCambioMonedaDolares,
                    MtoTipoCambio = x.TipoCambio.MtoTipoCambio,
                    // Montos Montos               
                    MtoBiGravadaDg = x.Montos.MtoBiGravadaDg,
                    MtoIgvIpmDg = x.Montos.MtoIgvIpmDg,
                    MtoBiGravadaDgng = x.Montos.MtoBiGravadaDgng,
                    MtoIgvIpmDgng = x.Montos.MtoIgvIpmDgng,
                    MtoBiGravadaDng = x.Montos.MtoBiGravadaDng,
                    MtoIgvIpmDng = x.Montos.MtoIgvIpmDng,
                    MtoValorAdqNg = x.Montos.MtoValorAdqNg,
                    MtoIcbp = x.Montos.MtoIcbp,
                    MtoOtrosTrib = x.Montos.MtoOtrosTrib,
                    MtoTotalCp = x.Montos.MtoTotalCp,
                    MtoIsc = x.Montos.MtoIsc,
                    MtoImb = x.Montos.MtoImb,
                    MtoBiGravadaDgOriginal = x.Montos.MtoBiGravadaDgOriginal,
                    MtoIgvIpmDgOriginal = x.Montos.MtoIgvIpmDgOriginal,
                    // List<LisDocumentosMod> LisDocumentosMod 
                    FecEmisionMod = x.LisDocumentosMod.Count() == 0 ? "" : x.LisDocumentosMod[0].FecEmisionMod,
                    CodTipoCdpMod = x.LisDocumentosMod.Count() == 0 ? "" : x.LisDocumentosMod[0].CodTipoCdpMod,
                    NumSerieCdpMod = x.LisDocumentosMod.Count() == 0 ? "" : x.LisDocumentosMod[0].NumSerieCdpMod,
                    NumCdpMod = x.LisDocumentosMod.Count() == 0 ? 0 : x.LisDocumentosMod[0].NumCdpMod,
                    CamposLibres = x.CamposLibres

                });
            });
            return lista;

        }
    }
}
