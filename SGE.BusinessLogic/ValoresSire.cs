namespace SGE.BusinessLogic
{
    public class ValoresSire
    {
        public static string BASE_URL = "https://api-sire.sunat.gob.pe/v1/contribuyente/migeigv/libros";

        public static string URL_TOKEN(string client_id)
        {
            return $"https://api-seguridad.sunat.gob.pe/v1/clientessol/{client_id}/oauth2/token/";
        }

        public static string URL_CONSULTA_PERIODOS(string tipoOperacion)
        {
            string URL = string.Empty;
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rvierce/padron/web/omisos/080000/periodos";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvierce/padron/web/omisos/140000/periodos";
                    break;
            }
            return URL;
        }

        public static string URL_DESCARGAR_PROPUESTA(string tipoOperacion, string periodo)
        {
            string URL = string.Empty;
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rce/propuesta/web/propuesta/{periodo}/exportacioncomprobantepropuesta?codTipoArchivo=0&codOrigenEnvio=1";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvie/propuesta/web/propuesta/{periodo}/exportapropuesta?codTipoArchivo=0";
                    break;
            }
            return URL;
        }

        public static string URL_CONSULTA_RESUMEN_DOCUMENTOS(string tipoOperacion, string periodo)
        {
            string URL = string.Empty;
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rvierce/resumen/web/resumen/{periodo}/resumencomprobantes/rce/?codTipoResumen=1";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvierce/resumen/web/resumen/{periodo}/resumencomprobantes/rvie/?codTipoResumen=1";
                    break;
            }
            return URL;
        }

        public static string URL_CONSULTA_TICKET(string tipoOperacion, string periodo, string numticket)
        {
            string URL = string.Empty;
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rvierce/gestionprocesosmasivos/web/masivo/consultaestadotickets?perIni={periodo}&perFin={periodo}&page=1&perPage=20&numTicket={numticket}";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvierce/gestionprocesosmasivos/web/masivo/consultaestadotickets?perIni={periodo}&perFin={periodo}&page=1&perPage=2000&numTicket={numticket}";
                    break;
            }
            return URL;
        }

        public static string URL_DESCARGAR_ARCHIVO(string tipoOperacion, string nombreArchivo)
        {
            string URL = string.Empty;
            nombreArchivo = nombreArchivo.Contains(".zip") ? nombreArchivo : nombreArchivo + ".zip";
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rvierce/gestionprocesosmasivos/web/masivo/archivoreporte?nomArchivoReporte={nombreArchivo}&codTipoArchivoReporte=01";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvierce/gestionprocesosmasivos/web/masivo/archivoreporte?nomArchivoReporte={nombreArchivo}&codTipoArchivoReporte=01&codLibro=140000";
                    break;
            }
            return URL;
        }


        public static string URL_ACEPTAR_PROPUESTA(string tipoOperacion, string periodo)
        {
            string URL = string.Empty;
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rce/propuesta/web/registroslibros/{periodo}/aceptarpropuesta";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvie/propuesta/web/propuesta/{periodo}/aceptapropuesta";
                    break;
            }
            return URL;
        }

        public static string URL_REEMPLAZAR_PROPUESTA(string tipoOperacion, string periodo)
        {
            string URL = string.Empty;
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rvierce/receptorpropuesta/web/propuesta/upload";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvierce/receptorpropuesta/web/propuesta/upload";
                    break;
            }
            return URL;
        }

        public static string URL_CONSULTA_PROPUESTA(string tipoOperacion, string periodo, int page)
        {
            string URL = string.Empty;
            switch (tipoOperacion)
            {
                case "COMPRA":
                    URL = $"{BASE_URL}/rce/propuesta/web/propuesta/{periodo}/busqueda?codTipoOpe=1&page={page}&perPage=100";
                    break;
                case "VENTA":
                    URL = $"{BASE_URL}/rvie/propuesta/web/propuesta/{periodo}/comprobantes?codTipoOpe=1&page={page}&perPage=100";

                    break;
            }
            return URL;
        }
    }
}
