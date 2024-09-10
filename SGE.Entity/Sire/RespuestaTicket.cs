using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public partial class RespuestaTicket
    {
        public Paginacion Paginacion { get; set; }
        public List<Registro> Registros { get; set; }
    }

     

    public partial class Registro
    {
        public long ShowReporteDescarga { get; set; }
        public long PerTributario { get; set; }
        public string NumTicket { get; set; }
        public object FecCargaImportacion { get; set; }
        public DateTimeOffset FecInicioProceso { get; set; }
        public long CodProceso { get; set; }
        public string DesProceso { get; set; }
        public string CodEstadoProceso { get; set; }
        public string DesEstadoProceso { get; set; }
        public object NomArchivoImportacion { get; set; }
        public DetalleTicket DetalleTicket { get; set; }
        public List<ArchivoReporte> ArchivoReporte { get; set; }
    }

    public partial class ArchivoReporte
    {
        public object CodTipoAchivoReporte { get; set; }
        public string NomArchivoReporte { get; set; }
        public string NomArchivoContenido { get; set; }
    }

    public partial class DetalleTicket
    {
        public string NumTicket { get; set; }
        public DateTimeOffset FecCargaImportacion { get; set; }
        public DateTimeOffset HoraCargaImportacion { get; set; }
        public string CodEstadoEnvio { get; set; }
        public string DesEstadoEnvio { get; set; }
        public object NomArchivoReporte { get; set; }
        public long CntFilasvalidada { get; set; }
        public long CntCpError { get; set; }
        public long CntCpInformados { get; set; }
    }
}
