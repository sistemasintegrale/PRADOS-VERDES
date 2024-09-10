using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EParametro : EAuditoria
    {
        [DataMember]
        public int pm_icod_parametro { get; set; }
        [DataMember]
        public decimal? pm_nigv_parametro { get; set; }
        [DataMember]
        public decimal? pm_ntope_parametro { get; set; }
        [DataMember]
        public decimal? pm_nuit_parametro { get; set; }
        [DataMember]
        public decimal? pm_ncategoria_parametro { get; set; }
        [DataMember]
        public decimal? pm_nivap_parametro { get; set; }
        [DataMember]
        public decimal? pm_nisc_parametro { get; set; }
        [DataMember]
        public string pm_nombre_empresa { get; set; }
        [DataMember]
        public string pm_direccion_empresa { get; set; }
        [DataMember]
        public string pm_vruc { get; set; }
        [DataMember]
        public long pm_correlativo_OR { get; set; }
        [DataMember]
        public decimal? pm_ntipo_cambio { get; set; }
        [DataMember]
        public int pmv_icod_almacen { get; set; }
        public string DesAlmacen { get; set; }
        /*Datos Facturacion Electronica*/
        public string urlServicioFacturaElectronica { get; set; }
        public string urlServicioNotaCredito { get; set; }
        public string urlServicioNotaDebito { get; set; }
        public string Ruc { get; set; }
        public string UsuarioSol { get; set; }
        public string ClaveSol { get; set; }
        public string EndPointUrlPrueba { get; set; }
        public string EndPointUrlDesarrollo { get; set; }
        public string PasswordCertificado { get; set; }
        public string CertificadoDigital { get; set; }
        public string urlServicioEnviarDocumento { get; set; }
        public string urlServicioFirma { get; set; }
        public string IdServiceValidacion { get; set; }
        public DateTime pm_sfecha_inicio { get; set; }
        public string DirecciónXML { get; set; }
        public string urlServicioEnvioResumen { get; set; }
        public string urlServicoGenerarResumen { get; set; }
        public string IdServiceValidacionResumen { get; set; }
        public string urlServicioDocumentoBaja { get; set; }
        public string ServiceConsultaTiket { get; set; }
        public string pm_vruta_resumen { get; set; }
    }
}
