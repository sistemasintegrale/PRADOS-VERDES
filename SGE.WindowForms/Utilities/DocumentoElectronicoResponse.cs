using Newtonsoft.Json;
//using OpenInvoicePeru.Comun.Dto.Intercambio;
//using OpenInvoicePeru.Comun.Dto.Modelos;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using SIDE.COMUN.DTO.Modelos;
using SIDE.COMUN.DTO.Intercambio;
using SGE.Entity.FacturaElectronica;

namespace SGE.WindowForms
{
    public class DocumentoElectronicoResponse
    {
        //Metodo de facturacion electronica
        public async Task<EFacturaElectronicaResponse> FacturaElectronica(DocumentoElectronico objdocumento)
        {
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = Application.StartupPath;
            path = lstParametro.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicioFacturaElectronica;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    System.IO.File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));

                    //Firmar
                    objFirmar.CertificadoDigital = lstParametro.CertificadoDigital;
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{objFirmar.CertificadoDigital}"))),// objFirmar.CertificadoDigital,
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        if (lstParametro.IdServiceValidacion == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacion == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = objdocumento.TipoDocumento.Trim(),
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado, //objrespuestaFirma.TramaXmlFirmado
                            OSE = false,
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarDocumentoResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito && !string.IsNullOrEmpty(objrespuestaEnvioDoc.TramaZipCdr))
                            {
                                File.WriteAllBytes($"{path}{objrespuestaEnvioDoc.NombreArchivo}.xml",
                                    Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));

                                File.WriteAllBytes($"{path}R-{objrespuestaEnvioDoc.NombreArchivo}.zip",
                                Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));




                                response.TramaZipCdr = objrespuestaEnvioDoc.TramaZipCdr;
                                response.NroTicketCdr = objrespuestaEnvioDoc.NroTicketCdr;
                                response.MensajeRespuesta = objrespuestaEnvioDoc.MensajeRespuesta;
                                response.CodigoRespuesta = objrespuestaEnvioDoc.CodigoRespuesta;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }

        //Metodo de Nota credito
        public async Task<EFacturaElectronicaResponse> NotaCreditoElectronica(DocumentoElectronico objdocumento)
        {
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = string.Empty;
            path = lstParametro.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;

            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicioNotaCredito;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));

                    //Firmar
                    objFirmar.CertificadoDigital = lstParametro.CertificadoDigital;
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{objFirmar.CertificadoDigital}"))),// objFirmar.CertificadoDigital,
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificad
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {

                        if (lstParametro.IdServiceValidacion == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacion == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        string rutaArchivo_fir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                          $"{objdocumento.IdDocumento.Trim()}-firma.xml");

                        File.WriteAllBytes(rutaArchivo_fir, Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));


                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = objdocumento.TipoDocumento.Trim(),
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado //objrespuestaFirma.TramaXmlFirmado
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarDocumentoResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito && !string.IsNullOrEmpty(objrespuestaEnvioDoc.TramaZipCdr))
                            {

                                File.WriteAllBytes($"{path}{objrespuestaEnvioDoc.NombreArchivo}.xml",
                                    Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));

                                File.WriteAllBytes($"{path}R-{objrespuestaEnvioDoc.NombreArchivo}.zip",
                                Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));

                                response.TramaZipCdr = objrespuestaEnvioDoc.TramaZipCdr;
                                response.NroTicketCdr = objrespuestaEnvioDoc.NroTicketCdr;
                                response.MensajeRespuesta = objrespuestaEnvioDoc.MensajeRespuesta;
                                response.CodigoRespuesta = objrespuestaEnvioDoc.CodigoRespuesta;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }


        //Metodo  de Nota debito
        public async Task<EFacturaElectronicaResponse> NotaDebitoElectronica(DocumentoElectronico objdocumento)
        {
            string path = Application.StartupPath;
            path = path.Replace(@"bin\Debug", "");
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = ConfigurationManager.AppSettings.Get("urlServicioNotaDebito");
                string urlserviceFirmar = ConfigurationManager.AppSettings.Get("urlServicioFirma");
                string urlServiceEnviarDocumento = ConfigurationManager.AppSettings.Get("urlServicioEnviarDocumento");

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    System.IO.File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));

                    //Firmar
                    objFirmar.CertificadoDigital = ConfigurationManager.AppSettings.Get("CertificadoDigital");
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = objFirmar.CertificadoDigital,
                        PasswordCertificado = ConfigurationManager.AppSettings.Get("PasswordCertificado"),//Contraseña para el certificado
                        UnSoloNodoExtension = false
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        if (ConfigurationManager.AppSettings.Get("IdServiceValidacion") == "0")
                            EndPointUrl = ConfigurationManager.AppSettings.Get("EndPointUrlPrueba");
                        if (ConfigurationManager.AppSettings.Get("IdServiceValidacion") == "1")
                            EndPointUrl = ConfigurationManager.AppSettings.Get("EndPointUrlDesarrollo");

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = ConfigurationManager.AppSettings.Get("Ruc"),
                            UsuarioSol = ConfigurationManager.AppSettings.Get("UsuarioSol"),
                            ClaveSol = ConfigurationManager.AppSettings.Get("ClaveSol"),
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = objdocumento.TipoDocumento.Trim(),
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado //objrespuestaFirma.TramaXmlFirmado
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarDocumentoResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito && !string.IsNullOrEmpty(objrespuestaEnvioDoc.TramaZipCdr))
                            {
                                System.IO.File.WriteAllBytes($"{path}\\ResultadoSunat\\{objrespuestaEnvioDoc.NombreArchivo}.xml",
                                    Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));

                                System.IO.File.WriteAllBytes($"{path}\\ResultadoSunat\\R-{objrespuestaEnvioDoc.NombreArchivo}.zip",
                                Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));

                                response.TramaZipCdr = objrespuestaEnvioDoc.TramaZipCdr;
                                response.NroTicketCdr = objrespuestaEnvioDoc.NroTicketCdr;
                                response.MensajeRespuesta = objrespuestaEnvioDoc.MensajeRespuesta;
                                response.CodigoRespuesta = objrespuestaEnvioDoc.CodigoRespuesta;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }

        public async Task<ESunatDocumentosBajaResponse> DocumentoElectronicoBaja(ComunicacionBaja objdocumento)
        {
            string path = Application.StartupPath;
            path = path.Replace(@"bin\Debug", "");
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            ESunatDocumentosBajaResponse response = new ESunatDocumentosBajaResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicioDocumentoBaja;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnvioResumen; //lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    System.IO.File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));

                    //Firmar
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{lstParametro.CertificadoDigital}"))),
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        if (lstParametro.IdServiceValidacion == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacion == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = "",
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado //objrespuestaFirma.TramaXmlFirmado
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarResumenResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarResumenResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito)
                            {
                                //System.IO.File.WriteAllBytes($"{path}\\ResultadoSunat\\{objrespuestaEnvioDoc.NombreArchivo}.xml",
                                //    Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));

                                //System.IO.File.WriteAllBytes($"{path}\\ResultadoSunat\\R-{objrespuestaEnvioDoc.NombreArchivo}.zip",
                                //Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));


                                response.Pila = objrespuestaEnvioDoc.Pila;
                                response.NroTicket = objrespuestaEnvioDoc.NroTicket;
                                response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                                response.IdDocumento = objdocumento.IdDocumento;
                                //response.IdItems += $"{objrespuestaEnvioDoc.NombreArchivo},";
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }

        public async Task<EResumenResponse> EnviarResumen(ResumenDiarioNuevo objdocumento)
        {
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = Application.StartupPath;
            path = lstParametro.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            //EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            EResumenResponse response = new EResumenResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicoGenerarResumen;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnvioResumen; //lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest();
                string json = JsonConvert.SerializeObject(objdocumento);
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    System.IO.File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));


                    //Firmar
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{lstParametro.CertificadoDigital}"))),
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        string rutaResumen = new BAdministracionSistema().listarParametro()[0].pm_vruta_resumen;
                        System.IO.File.WriteAllBytes(rutaResumen+ objdocumento.IdDocumento.Trim()+ ".xml", Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                        if (lstParametro.IdServiceValidacionResumen == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacionResumen == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = "",
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado //objrespuestaFirma.TramaXmlFirmado
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarResumenResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarResumenResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {

                            response.Pila = objrespuestaEnvioDoc.Pila;
                            response.NroTicket = objrespuestaEnvioDoc.NroTicket;
                            response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;
                            response.Exito = objrespuestaEnvioDoc.Exito;
                            response.IdDocumento = objdocumento.IdDocumento;

                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }

        public async Task<EnviarDocumentoResponse> ConsultaTiket(ConsultaTicketRequest objdocumento, string path)
        {
            string EndPointUrl = string.Empty;
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            EnviarDocumentoResponse response = new EnviarDocumentoResponse();
            try
            {

                EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                var enviarConsultaTiketRequest = new ConsultaTicketRequest
                {
                    Ruc = lstParametro.Ruc,
                    UsuarioSol = lstParametro.UsuarioSol,
                    ClaveSol = lstParametro.ClaveSol,
                    EndPointUrl = EndPointUrl,
                    IdDocumento = "",
                    TipoDocumento = "",
                    NroTicket = objdocumento.NroTicket,
                    OSE = false
                };

                var respondeEnvioDoc = await HttpRequestFactory.Post(lstParametro.ServiceConsultaTiket, enviarConsultaTiketRequest);
                var s = (enviarConsultaTiketRequest);
                response = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);
                if (response.Exito)
                {
                    if (Directory.Exists(path))
                        File.WriteAllBytes($"{path}/{response.NombreArchivo.Replace(".xml", "")}.zip",
                                 Convert.FromBase64String(response.TramaZipCdr));
                }


            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }
    }
}