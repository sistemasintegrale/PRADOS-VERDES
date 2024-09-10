using Newtonsoft.Json;
using SGE.Entity.Sire;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SGE.BusinessLogic
{
    public static class SireServiceImpl
    {
        public static string CLIENT_ID = "eac0cc03-2ed5-4434-8b8f-52e4d4053871";
        public static string CLIENT_SECRET = "DW+5oaocbz7Aywozk+bYNg==";
        public static string USER_NAME = "20500662159ROSIMIMO";
        public static string PASSWORD = "Rosi0613";
        public static async Task<BaseResponse<string>> ObtenerToken(SireRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                using (var client = new HttpClient())
                {

                    var datos = new Dictionary<string, string>
                    {
                        { "grant_type", "password" },
                        { "scope","https://api-sire.sunat.gob.pe" },
                        { "client_id", CLIENT_ID },
                        { "client_secret",CLIENT_SECRET },
                        { "username", USER_NAME },
                        { "password", PASSWORD }
                    };
                    ServicePointManager.UseNagleAlgorithm = true;
                    ServicePointManager.Expect100Continue = false;
                    ServicePointManager.CheckCertificateRevocationList = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var contenido = new FormUrlEncodedContent(datos);
                    var respuesta = await client.PostAsync(ValoresSire.URL_TOKEN(CLIENT_ID), contenido);
                    var contenidoRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var respuestaToken = JsonConvert.DeserializeObject<RespuestaToken>(contenidoRespuesta);
                    response.Data = respuestaToken.access_token;
                }
            }
            catch (Exception ex)
            {
                response.Succes = false;
                response.Msg = ex.Message;
            }
            return response;
        }

        public static async Task<BaseResponse<string>> ConsultarResumenDocumentos(SireRequest req)
        {
            var response = new BaseResponse<string>();
            var Token = await ObtenerToken(req);
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Data);

                HttpResponseMessage resp = await httpClient.GetAsync(ValoresSire.URL_CONSULTA_RESUMEN_DOCUMENTOS(req.operacion, req.periodo));

                if (resp.IsSuccessStatusCode)
                {
                    response.Data = await resp.Content.ReadAsStringAsync();
                    response.Succes = true;
                }
                else
                {
                    response.Data = $"Error en la solicitud. Código de estado: {resp.StatusCode}";
                    response.Succes = false;
                }
            }
            return response;
        }

        public static async Task<BaseResponse<string>> ConsultarPeriodosActivos(SireRequest req)
        {
            var response = new BaseResponse<string>();
            try
            {
                var Token = await ObtenerToken(req);
                ServicePointManager.UseNagleAlgorithm = true;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Data);

                    HttpResponseMessage resp = await httpClient.GetAsync(ValoresSire.URL_CONSULTA_PERIODOS(req.operacion));

                    if (resp.IsSuccessStatusCode)
                    {
                        response.Data = await resp.Content.ReadAsStringAsync();
                        response.Succes = true;
                    }
                    else
                    {
                        response.Data = $"Error en la solicitud. Código de estado: {resp.StatusCode}";
                        response.Succes = false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }
        public static async Task<BaseResponse<string>> DescargarPropuesta(SireRequest req)
        {
            var response = new BaseResponse<string>();
            var Token = await ObtenerToken(req);
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Data);

                HttpResponseMessage resp = await httpClient.GetAsync(ValoresSire.URL_DESCARGAR_PROPUESTA(req.operacion, req.periodo));

                if (resp.IsSuccessStatusCode)
                {
                    response.Data = await resp.Content.ReadAsStringAsync();
                    response.Succes = true;
                }
                else
                {
                    response.Data = $"Error en la solicitud. Código de estado: {resp.StatusCode}";
                    response.Succes = false;
                }
            }
            return response;
        }

        public static async Task<BaseResponse<string>> ConsultarTicket(SireRequest req)
        {
            var response = new BaseResponse<string>();
            var Token = await ObtenerToken(req);
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Data);

                HttpResponseMessage resp = await httpClient.GetAsync(ValoresSire.URL_CONSULTA_TICKET(req.operacion, req.periodo, req.numeroTicket));

                if (resp.IsSuccessStatusCode)
                {
                    response.Data = await resp.Content.ReadAsStringAsync();
                    response.Succes = true;
                }
                else
                {
                    response.Data = $"Error en la solicitud. Código de estado: {resp.StatusCode}";
                    response.Succes = false;
                }
            }
            return response;
        }


        public static async Task<BaseResponse<string>> ConsultarPropuesta(SireRequest req)
        {
            var response = new BaseResponse<string>();
            var Token = await ObtenerToken(req);
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Data);

                HttpResponseMessage resp = await httpClient.GetAsync(ValoresSire.URL_CONSULTA_PROPUESTA(req.operacion, req.periodo, req.page));

                if (resp.IsSuccessStatusCode)
                {
                    response.Data = await resp.Content.ReadAsStringAsync();
                    response.Succes = true;
                }
                else
                {
                    response.Data = $"Error en la solicitud. Código de estado: {resp.StatusCode}";
                    response.Succes = false;
                }
            }
            return response;
        }

        public static async Task<BaseResponse<Stream>> DescargarArchivo(SireRequest req)
        {
            var response = new BaseResponse<Stream>();
            var Token = await ObtenerToken(req);
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Data);

                HttpResponseMessage resp = await httpClient.GetAsync(ValoresSire.URL_DESCARGAR_ARCHIVO(req.operacion, req.nombreArchivo));

                if (resp.IsSuccessStatusCode)
                {
                    response.Data = await resp.Content.ReadAsStreamAsync();
                    response.Succes = true;
                }
                else
                {

                    response.Succes = false;
                }
            }
            return response;
        }
        public static async Task<BaseResponse<string>> AceptarPropuesta(SireRequest req)
        {
            throw new NotImplementedException();
        }
        public static async Task<BaseResponse<string>> ReemplazarPropuesta(SireRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
