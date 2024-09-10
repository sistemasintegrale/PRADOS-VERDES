using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGE.Entity;
using SGE.DataAccess;
using System.Transactions;
using System.Security.Principal;
using SGE.Entity.FacturaElectronica;
using System.Data;
using static SGE.Common.TableVenta;

namespace SGE.BusinessLogic
{
    public class BVentas
    {
        AlmacenData objAlmacenData = new AlmacenData();
        VentasData objVentasData = new VentasData();
        TesoreriaData objTesoreriaDara = new TesoreriaData();

        public Tuple<string, string> ObtenerDocumentos(int cntc_icod_contrato_cuotas)
        {
            Tuple<string, string> tuple = new Tuple<string, string>("", "");
            try
            {
                tuple = objVentasData.ObtenerDocumentos(cntc_icod_contrato_cuotas);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return tuple;
        }

        public void PlanNecisidadSepulturaDetalleSave(EPlanNecesidadSepulturaDetalle obe)
        {
            objVentasData.PlanNecisidadSepulturaDetalleSave(obe);
        }

        public List<EPlanNecesidadSepulturaDetalle> PlanNecisidadSepulturaDetalleListar(int id)
        {
            return objVentasData.PlanNecisidadSepulturaDetalleListar(id);
        }

        public bool PlanNecisidadSepulturaValidar(EPlanNecesidadSepultura obe)
        {
            return objVentasData.PlanNecisidadSepulturaValidar(obe);
        }

        public List<ECertificadoUsoPerpetuo> CetificadoUsoPerpetuolistarConContrato() => objVentasData.CetificadoUsoPerpetuolistarConContrato();

        public List<EContrato> listarContratoPorFechas(DateTime fechaIncio, DateTime FechaFinal, int tipoContrato)
        {
            return objVentasData.listarContratoPorFechas(fechaIncio, FechaFinal, tipoContrato);
        }

        public void PlanNecisidadSepulturaGuardar(EPlanNecesidadSepultura obe)
        {
            objVentasData.PlanNecisidadSepulturaGuaradar(obe);
        }

        public EEspacios EspaciosGetById(int espac_iid_iespacios) => objVentasData.EspaciosGetById(espac_iid_iespacios);

        public List<ECertificadoUsoPerpetuo> CetificadoUsoPerpetuolistar(int cntc_icod_contrato) => objVentasData.CetificadoUsoPerpetuolistar(cntc_icod_contrato);

        public List<EContrato> ConsultaDatosFallecido(DateTime fechaInicio, DateTime fechaFin)
        {
            return objVentasData.ConsultaDatosFallecido(fechaInicio, fechaFin);
        }

        public void PlanNecisidadSepulturaEliminar(EPlanNecesidadSepultura select)
        {
            objVentasData.PlanNecisidadSepulturaEliminar(select);
        }

        public void PlanNecisidadSepulturaDetalleEliminar(EPlanNecesidadSepulturaDetalle obe)
        {
            objVentasData.PlanNecisidadSepulturaDetalleEliminar(obe);
        }

        public List<EReciboCajaCabecera> listar_recibos_caja_cabecera()
        {
            return objVentasData.listar_recibos_caja_cabecera();
        }

        public DataTable PlanNecisidadSepulturaReporte()
        {
            return objVentasData.PlanNecisidadSepulturaReporte();
        }

        public List<EContrato> ContratosListarPorNumeroContratanteDni(string numero, string contratante, string dni)
        {
            return objVentasData.ContratosListarPorNumeroContratanteDni(numero, contratante, dni);
        }

        public List<EPlanNecesidadSepultura> PlanNecisidadSepulturaListar(int tabvd_iid_tabla_venta_det, int icod_tipo_plan = 0, int icod_nombre_plan = 0)
        {
            return objVentasData.PlanNecisidadSepulturaListar(tabvd_iid_tabla_venta_det, icod_tipo_plan, icod_nombre_plan);
        }

        public List<EPagoFomaFinanciamiento> listarFomaFinanciamiento(int cntc_icod_contrato, EContrato obj)
        {
            List<EPagoFomaFinanciamiento> lista = new List<EPagoFomaFinanciamiento>();
            try
            {
                lista = objVentasData.listarFomaFinanciamiento(cntc_icod_contrato);

                lista.ForEach(obe =>
                {
                    if (obe.pgs_itipo == Parametros.intTipoFoma)//FOMA
                    {
                        obe.pgs_nmonto = obj.cntc_nmonto_foma;
                    }
                    if (obe.pgs_itipo == Parametros.intTipoFinanciamiento) //FINANCIAMIENTO
                    {
                        var Reprogramacion = new BVentas().ListarReprogramaciones(obj.cntc_icod_contrato).OrderByDescending(x => x.cntcr_iid_reprogramacion).FirstOrDefault();
                        obe.pgs_nmonto = Reprogramacion != null ? Reprogramacion.cntcr_nmonto_financiamiento : obj.cntc_nfinanciamientro;
                    }

                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }

        public List<EEspaciosDet> listarEspaciosConsultasIcodContrato(int icodcontrato) => objVentasData.listarEspaciosConsultasIcodContrato(icodcontrato);

        public List<EEspacios> Espacios() => objVentasData.Espacios();

        public void CetificadoUsoPerpetuoEliminar(int cup_icod) => objVentasData.CetificadoUsoPerpetuoEliminar(cup_icod);

        public List<EContrato> listarContratoSimple(int intContrato, int situacion)
        {
            return objVentasData.listarContratoSimple(intContrato, situacion);
        }

        public void anular_recibo_caja_cabecera(EReciboCajaCabecera objCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var listDetalle = new BVentas().listar_recibo_caja_detalle(objCabecera.rc_icod_recibo);
                    if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento))
                    {
                        var contrato = new BVentas().listarContratoPorIcod(objCabecera.rc_icod_contrato);
                        var lstpagosFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato);
                        if (lstpagosFinanciamiento.Count == 0)
                        {

                            EPagoFomaFinanciamiento objpag = new EPagoFomaFinanciamiento();
                            objpag.pgs_icod_contrato = contrato.cntc_icod_contrato;
                            objpag.pgs_itipo = Parametros.intTipoFinanciamiento;
                            objpag.pgs_sfecha_pago = null;
                            objpag.pgs_nmonto_pagado = 0;
                            objpag.rc_icod_recibo = null;
                            objpag.intusuario = objCabecera.intUsuario;
                            objpag.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            objpag.pgs_icod_pagos = new BVentas().FomaFinanciamientoInsertar(objpag);
                        }
                        else
                        {
                            var pagFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato).Where(x => x.pgs_itipo == Parametros.intTipoFinanciamiento).First();
                            pagFinanciamiento.rc_icod_recibo = null;
                            pagFinanciamiento.intusuario = objCabecera.intUsuario;
                            pagFinanciamiento.pgs_nmonto_pagado = 0;
                            pagFinanciamiento.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            pagFinanciamiento.pgs_sfecha_pago = null;
                            new BVentas().FomaFinanciamientoModificar(pagFinanciamiento);
                        }
                    }
                    if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFoma))
                    {
                        objCabecera.rc_icod_foma_anulado = string.Empty;
                        var listaFoma = new BVentas().CuotaFomaListar(objCabecera.rc_icod_contrato).Where(x => x.rc_icod_recibo == objCabecera.rc_icod_recibo).ToList();
                        listaFoma.ForEach(x =>
                        {
                            x.rc_icod_recibo = null;
                            x.cff_sfecha_pago = null;
                            x.ccf_nmonto_pagado = null;
                            x.intUsuario = objCabecera.intUsuario;
                            x.strPc = objCabecera.strPc;
                            objVentasData.CuotaFomaModificar(x);
                            objCabecera.rc_icod_foma_anulado = objCabecera.rc_icod_foma_anulado + " " + x.ccf_icod_cuota;
                        });
                    }


                    objVentasData.anular_recibo_caja_cabecera(objCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public EContratoWeb ContratoWebById(int cntc_icod_contrato)
        {
            return objVentasData.ContratoWebById(cntc_icod_contrato);
        }

        public List<EContrato> ContratoListarPorFechas(DateTime fechaincio, DateTime fechafinal, int intContrato)
        {
            return objVentasData.ContratoListarPorFechas(fechaincio, fechafinal, intContrato);
        }

        public List<EContrato> ContratoFallecidoListarPorFechas(DateTime fechaincio, DateTime fechafinal, int intContrato)
        {
            return objVentasData.ContratoFallecidoListarPorFechas(fechaincio, fechafinal, intContrato);
        }

        public List<EContrato> listarContratoNuevo(int intContrato)
        {
            //Se Cambio a return objVentasData.ContratoListarPorFechas(fechaincio, fechafinal, intContrato);
            return objVentasData.listarContratoNuevo(intContrato);
        }

        public void activar_recibo_caja_cabecera(EReciboCajaCabecera objCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificar_recibo_caja_cabecera(objCabecera);
                    var listDetalle = new BVentas().listar_recibo_caja_detalle(objCabecera.rc_icod_recibo);
                    if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento))
                    {
                        var contrato = new BVentas().listarContratoPorIcod(objCabecera.rc_icod_contrato);
                        var lstpagosFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato);
                        if (lstpagosFinanciamiento.Count == 0)
                        {

                            EPagoFomaFinanciamiento objpag = new EPagoFomaFinanciamiento();
                            objpag.pgs_icod_contrato = contrato.cntc_icod_contrato;
                            objpag.pgs_itipo = Parametros.intTipoFinanciamiento;
                            objpag.pgs_sfecha_pago = objCabecera.rc_sfecha_recibo;
                            objpag.pgs_nmonto_pagado = listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento).Select(x => x.rcd_dprecio_total).First();
                            objpag.rc_icod_recibo = objCabecera.rc_icod_recibo;
                            objpag.intusuario = objCabecera.intUsuario;
                            objpag.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            objpag.pgs_icod_pagos = new BVentas().FomaFinanciamientoInsertar(objpag);
                        }
                        else
                        {
                            var pagFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato).Where(x => x.pgs_itipo == Parametros.intTipoFinanciamiento).First();
                            pagFinanciamiento.rc_icod_recibo = objCabecera.rc_icod_recibo;
                            pagFinanciamiento.intusuario = objCabecera.intUsuario;
                            pagFinanciamiento.pgs_nmonto_pagado = listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento).Select(x => x.rcd_dprecio_total).First();
                            pagFinanciamiento.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            pagFinanciamiento.pgs_sfecha_pago = objCabecera.rc_sfecha_recibo;
                            new BVentas().FomaFinanciamientoModificar(pagFinanciamiento);
                        }
                    }
                    if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFoma))
                    {
                        var listaFomaAux = new BVentas().CuotaFomaListar(objCabecera.rc_icod_contrato);

                        var listaFoma = new List<ECuotaFoma>();
                        if (!string.IsNullOrWhiteSpace(objCabecera.rc_icod_foma_anulado))
                        {
                            string[] icods = objCabecera.rc_icod_foma_anulado.Split(' ');
                            for (int i = 0; i < icods.Length; i++)
                            {
                                int icod = string.IsNullOrWhiteSpace(icods[i]) ? 0 : Convert.ToInt32(icods[i]);
                                if (icod != 0)
                                    listaFoma.Add(listaFomaAux.Where(x => x.ccf_icod_cuota == icod).FirstOrDefault());
                            }
                        }
                        if (listaFoma.Exists(x => !string.IsNullOrWhiteSpace(x.strNumRecibo)))
                        {
                            throw new ArgumentException($"La FOMA que fué registrada previamente en este documuento ya se encuentra registrada en el Doc. {listaFoma.First().strNumRecibo}");
                        }


                        listaFoma.ForEach(x =>
                        {
                            x.rc_icod_recibo = objCabecera.rc_icod_recibo;
                            x.cff_sfecha_pago = objCabecera.rc_sfecha_recibo;
                            x.ccf_nmonto_pagado = x.ccf_nmonto_pagar;
                            x.intUsuario = objCabecera.intUsuario;
                            x.strPc = objCabecera.strPc;
                            objVentasData.CuotaFomaModificar(x);

                        });
                    }



                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public int ContratoWebGuardar(EContratoWeb obe, bool GuardarCuotas, List<EContratoCuotas> lstCuotas)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    obe.cntc_icod_contrato = objVentasData.ContratoWebGuardar(obe);
                    objVentasData.FallecidoWebGuardar(obe);
                    objVentasData.Contratante1WebGuardar(obe);
                    objVentasData.Contratante2WebGuardar(obe);
                    if (GuardarCuotas)
                    {
                        var cuotaAnterios = objVentasData.listarContratoCuotas(obe.cntc_icod_contrato);
                        cuotaAnterios.ForEach(x =>
                        {
                            x.intUsuario = (int)obe.cntc_iusuario_crea;
                            x.strPc = obe.cntc_vpc_crea;
                            objVentasData.eliminarContratoCuotas(x);
                        });
                        lstCuotas.ForEach(x =>
                        {
                            x.cntc_icod_contrato = obe.cntc_icod_contrato;
                            x.intUsuario = (int)obe.cntc_iusuario_crea;
                            x.strPc = obe.cntc_vpc_crea;
                            objVentasData.insertarContratoCuotas(x);
                        });

                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obe.cntc_icod_contrato;
        }

        public ContratoImpresion ContratoImpresion(int cntc_icod_contrato)
        {
            return objVentasData.ContratoImpresion(cntc_icod_contrato);
        }

        public void CetificadoUsoPerpetuoModificar(ECertificadoUsoPerpetuo objCertificado) => objVentasData.CetificadoUsoPerpetuoModificar(objCertificado);

        public int CetificadoUsoPerpetuoInsertar(ECertificadoUsoPerpetuo objCertificado) => objVentasData.CetificadoUsoPerpetuoInsertar(objCertificado);

        public void ResumenDocumentosCabMensajeRespuestaModificar(ESunatResumenDocumentosCab item, string mensajeRespuesta, DateTime? fechaEnvio)
        {
            objVentasData.ResumenDocumentosCabMensajeRespuestaModificar(item, mensajeRespuesta, fechaEnvio);
        }
        public void modificarResumenDiarioResponse(int id, EResumenResponse response)
        {
            objVentasData.modificarResumenDiarioResponse(id, response);
        }

        public List<EProyeccionVendedor> listar_informe_ventas_por_mes(int anio, int mes, int asesor)
        {
            List<EProyeccionVendedor> lista = new List<EProyeccionVendedor>();
            try
            {
                lista = objVentasData.listar_informe_ventas_por_mes(anio, mes, asesor);
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public ECuotaFoma CuotaFomaGetById(int? ccf_icod_cuota)
        {
            return objVentasData.CuotaFomaGetById(ccf_icod_cuota);
        }

        public int CuotaFomaInsertar(ECuotaFoma objCuota)
        {
            return objVentasData.CuotaFomaInsertar(objCuota);
        }

        public List<ECuotaFoma> CuotaFomaListar(int cntc_icod_contrato)
        {
            return objVentasData.CuotaFomaListar(cntc_icod_contrato);
        }

        public void CuotaFomaModificar(ECuotaFoma objCuota)
        {
            objVentasData.CuotaFomaModificar(objCuota);
        }

        public void eliminar_recibo_caja_cabecera(EReciboCajaCabecera objCabecera)
        {
            objVentasData.eliminar_recibo_caja_cabecera(objCabecera);
        }

        public void ProyeccionVentasEliminar(EProyeccionVendedor obe)
        {
            objVentasData.ProyeccionVentasEliminar(obe);
        }

        public List<EProyeccionVendedor> ProyeccionVentasListar(int anio, int mes)
        {
            return objVentasData.ProyeccionVentasListar(anio, mes);
        }

        public void SGEV_INGRESAR_PLAN_TIPO_SEPULTURA(EContrato obe)
        {
            objVentasData.SGEV_INGRESAR_PLAN_TIPO_SEPULTURA(obe);
        }

        public List<EReciboCajaDetalle> listar_recibo_caja_detalle(int rc_icod_recibo)
        {
            return objVentasData.listar_recibo_caja_detalle(rc_icod_recibo);
        }

        public EPlanillaCobranzaDet ObtenerDocumentoXid(int plnd_icod_documento)
        {
            return objVentasData.ObtenerDocumentoXid(plnd_icod_documento);
        }

        public void CuotaFomaEliminar(ECuotaFoma obj)
        {
            objVentasData.CuotaFomaEliminar(obj);
        }

        public List<EContratante> listarContratantes(int cntc_icod_contrato)
        {
            List<EContratante> lista = new List<EContratante>();
            try
            {
                lista = objVentasData.listarContratantes(cntc_icod_contrato);
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public void ProyeccionVentasModificar(EProyeccionVendedor obj)
        {
            objVentasData.ProyeccionVentasModificar(obj);
        }

        public int ProyeccionVentasInsertar(EProyeccionVendedor obj)
        {
            return objVentasData.ProyeccionVentasInsertar(obj);
        }

        public List<EFacturaCab> listarFacturaCabPlanilla(int intEjericio)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>();
            try
            {
                lista = objVentasData.listarFacturaCabPlanilla(intEjericio);
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public int FomaFinanciamientoInsertar(EPagoFomaFinanciamiento obj)
        {
            int icod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    icod = objVentasData.FomaFinanciamientoInsertar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return icod;
        }

        public List<EEspaciosDet> Control_Sepultura_Listar()
        {
            List<EEspaciosDet> lista = new List<EEspaciosDet>();
            try
            {
                lista = objVentasData.Control_Sepultura_Listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lista;
        }

        public void FomaFinanciamientoModificar(EPagoFomaFinanciamiento obe)
        {
            objVentasData.FomaFinanciamientoModificar(obe);
        }

        public int insertar_recibo_caja(EReciboCajaCabecera obj, List<EReciboCajaDetalle> listDetalle, List<ECuotaFoma> lstpagos)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    obj.rc_icod_recibo = objVentasData.insertar_recibo_caja_cabecera(obj);

                    listDetalle.ForEach(x =>
                    {
                        x.rc_icod_recibo = obj.rc_icod_recibo;
                        int icod = objVentasData.insertar_recibo_caja_detalle(x);
                    });

                    var oBe = new BVentas().listarRegistroParametro().FirstOrDefault();
                    oBe.rgpmc_icorrelativo_recibo_caja = Convert.ToInt32(obj.rc_vnumero.Substring(4));
                    new BVentas().modificarRegistroParametro(oBe);

                    lstpagos.ForEach(x =>
                    {
                        var data = new BVentas().CuotaFomaGetById(x.ccf_icod_cuota);
                        data.rc_icod_recibo = obj.rc_icod_recibo;
                        data.ccf_nmonto_pagado = data.ccf_nmonto_pagar;
                        data.intUsuario = obj.intUsuario;
                        data.strPc = obj.strPc;
                        data.cff_sfecha_pago = obj.rc_sfecha_recibo;
                        new BVentas().CuotaFomaModificar(data);

                    });

                    if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento))
                    {
                        var contrato = objVentasData.listarContratoPorIcod(obj.rc_icod_contrato);
                        var lstpagosFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato);
                        if (lstpagosFinanciamiento.Count == 0)
                        {

                            EPagoFomaFinanciamiento objpag = new EPagoFomaFinanciamiento();
                            objpag.pgs_icod_contrato = contrato.cntc_icod_contrato;
                            objpag.pgs_itipo = Parametros.intTipoFinanciamiento;
                            objpag.pgs_sfecha_pago = (DateTime?)null;
                            objpag.pgs_nmonto_pagado = listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento).First().rcd_dprecio_total;
                            objpag.rc_icod_recibo = obj.rc_icod_recibo;
                            objpag.intusuario = obj.intUsuario;
                            objpag.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            objpag.pgs_sfecha_pago = obj.rc_sfecha_recibo;
                            objpag.pgs_icod_pagos = objVentasData.FomaFinanciamientoInsertar(objpag);
                        }
                        else
                        {
                            var pagFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato).Where(x => x.pgs_itipo == Parametros.intTipoFinanciamiento).First();
                            pagFinanciamiento.rc_icod_recibo = obj.rc_icod_recibo;
                            pagFinanciamiento.intusuario = obj.intUsuario;
                            pagFinanciamiento.pgs_nmonto_pagado = listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento).First().rcd_dprecio_total;
                            pagFinanciamiento.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            pagFinanciamiento.pgs_sfecha_pago = obj.rc_sfecha_recibo;
                            objVentasData.FomaFinanciamientoModificar(pagFinanciamiento);
                        }
                    }


                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return obj.rc_icod_recibo;
        }

        public bool verificarSerieReciboCaja(string numero)
        {
            return objVentasData.verificarSerieReciboCaja(numero);
        }

        public EContratoFallecido ObnterFallecido(int cntc_icod_contrato_fallecido)
        {
            return objVentasData.ObnterFallecido(cntc_icod_contrato_fallecido);
        }

        public void modificar_recibo_caja(EReciboCajaCabecera obj, List<EReciboCajaDetalle> listDetalle, List<EReciboCajaDetalle> listDetalleEliminar, List<ECuotaFoma> lstpagos)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificar_recibo_caja_cabecera(obj);

                    listDetalle.ForEach(x =>
                    {
                        x.rc_icod_recibo = obj.rc_icod_recibo;
                        x.intUsuario = obj.intUsuario;
                        x.strPc = obj.strPc;
                        if (x.TipoOperacion == 1)
                        {

                            int icod = objVentasData.insertar_recibo_caja_detalle(x);

                        }
                        if (x.TipoOperacion == 2)
                        {
                            objVentasData.modificar_recibo_caja_detalle(x);

                        }



                    });

                    listDetalleEliminar.ForEach(x =>
                    {
                        objVentasData.eliminar_recibo_caja_detalle(x);

                    });
                    var listaFomaAux = new BVentas().CuotaFomaListar(obj.rc_icod_contrato);
                    listaFomaAux.ForEach(x =>
                    {
                        var data = new BVentas().CuotaFomaGetById(x.ccf_icod_cuota);
                        data.rc_icod_recibo = null;
                        data.ccf_nmonto_pagado = 0;
                        data.intUsuario = obj.intUsuario;
                        data.strPc = obj.strPc;
                        data.cff_sfecha_pago = null;
                        new BVentas().CuotaFomaModificar(data);
                    });


                    lstpagos.ForEach(x =>
                    {
                        var data = new BVentas().CuotaFomaGetById(x.ccf_icod_cuota);
                        data.rc_icod_recibo = obj.rc_icod_recibo;
                        data.ccf_nmonto_pagado = data.ccf_nmonto_pagar;
                        data.intUsuario = obj.intUsuario;
                        data.strPc = obj.strPc;
                        data.cff_sfecha_pago = obj.rc_sfecha_recibo;
                        new BVentas().CuotaFomaModificar(data);

                    });

                    if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento))
                    {
                        var contrato = objVentasData.listarContratoPorIcod(obj.rc_icod_contrato);
                        var lstpagosFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato);
                        if (lstpagosFinanciamiento.Count == 0)
                        {

                            EPagoFomaFinanciamiento objpag = new EPagoFomaFinanciamiento();
                            objpag.pgs_icod_contrato = contrato.cntc_icod_contrato;
                            objpag.pgs_itipo = Parametros.intTipoFinanciamiento;
                            objpag.pgs_sfecha_pago = obj.rc_sfecha_recibo;
                            objpag.pgs_nmonto_pagado = listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento).First().rcd_dprecio_total;
                            objpag.rc_icod_recibo = obj.rc_icod_recibo;
                            objpag.intusuario = obj.intUsuario;
                            objpag.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            objpag.pgs_icod_pagos = objVentasData.FomaFinanciamientoInsertar(objpag);
                        }
                        else
                        {
                            var pagFinanciamiento = new BVentas().listarFomaFinanciamiento(contrato.cntc_icod_contrato, contrato).Where(x => x.pgs_itipo == Parametros.intTipoFinanciamiento).First();
                            pagFinanciamiento.rc_icod_recibo = obj.rc_icod_recibo;
                            pagFinanciamiento.intusuario = obj.intUsuario;
                            pagFinanciamiento.pgs_nmonto_pagado = listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento).First().rcd_dprecio_total;
                            pagFinanciamiento.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                            pagFinanciamiento.pgs_sfecha_pago = obj.rc_sfecha_recibo;
                            objVentasData.FomaFinanciamientoModificar(pagFinanciamiento);
                        }
                    }

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EContrato contrato_get_foma_financiamiento(int rc_icod_contrato)
        {
            EContrato obj = new EContrato();
            obj = objVentasData.contrato_get_foma_financiamiento(rc_icod_contrato);
            var Reprogramacion = new BVentas().ListarReprogramaciones(rc_icod_contrato).OrderByDescending(x => x.cntcr_iid_reprogramacion).FirstOrDefault();
            obj.cntc_nfinanciamientro = Reprogramacion != null ? Reprogramacion.cntcr_nmonto_financiamiento : obj.cntc_nfinanciamientro;
            return obj;
        }

        public void MondificarMontosContrato(decimal monto, int rc_icod_contrato, bool indicador)
        {
            objVentasData.MondificarMontosContrato(monto, rc_icod_contrato, indicador);
        }

        #region Cliente
        public List<ECliente> ListarCliente()
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                lista = objVentasData.ListarCliente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EEntregaFormulario> listarEntregaFormularios(DateTime dtIninio, DateTime dtfinal, int asesor)
        {
            List<EEntregaFormulario> lista = new List<EEntregaFormulario>();
            try
            {
                lista = objVentasData.listarEntregaFormularios(dtIninio, dtfinal, asesor);
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<EEntregaFormularioDetalle> listarEntregaFormulariosDetalle(int entf_icod_entrega)
        {
            List<EEntregaFormularioDetalle> lista = new List<EEntregaFormularioDetalle>();
            try
            {
                lista = objVentasData.listarEntregaFormulariosDetalle(entf_icod_entrega);
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public void eliminarEntregaFormulario(EEntregaFormulario obe)
        {
            try
            {
                objVentasData.eliminarEntregaFormulario(obe);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ExistenciaSerieEnEntregas(string serie, out bool existe, out string mensaje)
        {

            objVentasData.ExistenciaSerieEnEntregas(serie, out existe, out mensaje);

        }

        public List<EBoletaCab> listarBoletaCabPlanilla(int v)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>();
            try
            {
                lista = objVentasData.listarBoletaCabPlanilla(v);
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public List<EPagosCuotas> Listar_Pagos_Documentos(int cod)
        {

            List<EPagosCuotas> lista = new List<EPagosCuotas>();
            try
            {
                lista = objVentasData.Listar_Pagos_Documentos(cod);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }

        public int insertarEntregaFormulario(EEntregaFormulario obj, List<EEntregaFormularioDetalle> lstdetalle)
        {
            int codigo = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    codigo = objVentasData.insertarEntregaFormulario(obj);
                    lstdetalle.ForEach(x =>
                    {
                        x.entf_icod_entrega = codigo;
                        objVentasData.insertarEntregaFormularioDetalle(x);
                    });

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return codigo;
        }

        public List<EEntregaFormularioDetalle> listarDetalleEntregaFormularrio(int entf_icod_entrega)
        {
            throw new NotImplementedException();
        }

        public void modificarEntregaFormulario(EEntregaFormulario obj, List<EEntregaFormularioDetalle> lstdetalle, List<EEntregaFormularioDetalle> lstdetalleElimina)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarEntregaFormulario(obj);

                    lstdetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {

                            x.entf_icod_entrega = obj.entf_icod_entrega;
                            objVentasData.insertarEntregaFormularioDetalle(x);

                        }
                        if (x.intTipoOperacion == 2)
                        {
                            x.entfd_iusuario_modifa = obj.entf_iusuario_modifica;
                            x.entfd_vpc_modifica = obj.entf_vpc_modifica;
                            objVentasData.modificarEntregaFormularioDetalle(x);
                        }

                    });

                    lstdetalleElimina.ForEach(x =>
                    {
                        x.entfd_iusuario_modifa = obj.entf_iusuario_modifica;
                        x.entfd_vpc_modifica = obj.entf_vpc_modifica;
                        objVentasData.eliminarEntregaFormularioDetalle(x);
                    });
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void modificarEntregaFormulario(EEntregaFormulario obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarEntregaFormulario(obj);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int InsertarCliente(ECliente objECliente)
        {
            int id_cliente = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    id_cliente = objVentasData.InsertarCliente(objECliente);
                    tx.Complete();
                }
                return id_cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCliente(ECliente objECliente)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarCliente(objECliente);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarClienteAnalitica(ECliente objECliente)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarClienteAnalitica(objECliente);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCliente(int cliec_icod_cliente, int usuario, string pc, int anac_icod_analitica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarCliente(cliec_icod_cliente, usuario, pc, anac_icod_analitica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarContratatante(EContratante obj)
        {
            try
            {
                objVentasData.modificarContratatante(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int insertarContratante(EContratante obj)
        {
            int codigo = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    codigo = objVentasData.insertarContratante(obj);
                    ts.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return codigo;
        }

        public EContratoCuotas ListarContratoCronogramas(int cntc_icod_contrato, int i)
        {
            EContratoCuotas obe = new EContratoCuotas();
            try
            {
                obe = objVentasData.ListarContratoCronogramas(cntc_icod_contrato, i);
            }
            catch (Exception)
            {

                throw;
            }
            return obe;
        }

        public List<EReprogramaciones> ListarReprogramaciones(int cntc_icod_contrato)
        {
            List<EReprogramaciones> list = new List<EReprogramaciones>();
            try
            {
                list = objVentasData.ListarReprogramaciones(cntc_icod_contrato);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }

        #endregion
        #region Ubicacion
        public List<EUbicacion> ListarUbicacion()
        {
            List<EUbicacion> lista = null;
            try
            {
                lista = objVentasData.ListarUbicacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarUbicacion(EUbicacion objEUbicaion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.InsertarUbicacion(objEUbicaion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarUbicacion(EUbicacion objEUbicacion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarUbicacion(objEUbicacion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarUbicacion(int ubicc_icod_ubicacion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarUbicacion(ubicc_icod_ubicacion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EContratoCuotas> listarPagos(int cntc_icod_contrato)
        {
            List<EContratoCuotas> lista = new List<EContratoCuotas>();

            try
            {
                lista = objVentasData.listarPagos(cntc_icod_contrato);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }
        #endregion
        #region Giro
        public List<EGiro> ListarGiro()
        {
            List<EGiro> lista = null;
            try
            {
                lista = objVentasData.ListarGiro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void Actualizar_Cuotas_Reprogramacion_Eliminada(int reprogramacionAnterior, int cntc_icod_contrato)
        {

            try
            {
                objVentasData.Actualizar_Cuotas_Reprogramacion_Eliminada(reprogramacionAnterior, cntc_icod_contrato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EGiro> ListarGiroFiltro()
        {
            List<EGiro> lista = null;
            try
            {
                lista = objVentasData.ListarGiro();
                EGiro entidad = new EGiro();
                entidad.giroc_icod_giro = 0;
                entidad.giroc_vnombre_giro = "Todos";
                lista.Insert(0, entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarGiro(EGiro objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.InsertarGiro(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarGiro(EGiro objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarGiro(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarReprogramacion(EReprogramaciones obj, List<EContratoCuotas> lstDetalle, List<EContratoCuotas> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ModificarReprogramacion(obj);

                    lstDelete.ForEach(x =>
                    {
                        new BVentas().eliminarContratoCuotas(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            new BVentas().insertarContratoCuotas(x);
                        }
                        else
                        {
                            new BVentas().modificarContratoCuotas(x);
                        }
                        new BVentas().ActualizarContrato(x.cntc_icod_contrato);

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EliminarReprogramacion(EReprogramaciones obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarReprogramacion(obj);
                    objVentasData.EliminarDetReprogramacion(obj.cntcr_iid_reprogramacion, obj.cntc_icod_contrato);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertarReprogramacion(EReprogramaciones obj, List<EContratoCuotas> lstDetalle)
        {
            int codigo = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    codigo = objVentasData.InsertarReprogramacion(obj);
                    lstDetalle.ForEach(x =>
                    {
                        new BVentas().insertarContratoCuotas(x);
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return codigo;
        }

        public void ActualizarReprogramacion(int cntcr_icod_reprogracion, int cntc_icod_contrato, int cntcr_iid_reprogramacion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarReprogramacion(cntcr_icod_reprogracion, cntc_icod_contrato, cntcr_iid_reprogramacion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EliminarGiro(int girc_icod_giro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarGiro(girc_icod_giro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  Factura venta electronica
        public List<EFacturaVentaElectronica> listarfacturaVentaElectronica(DateTime fechaInicio)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronica(fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        public List<EFacturaVentaElectronica> FacturaVentaElectronicaObtenerDoc(int doc_icod_documento, string tipoDocumento)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.FacturaVentaElectronicaObtenerDoc(doc_icod_documento, tipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaAnular(DateTime fechaInicio)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronicaAnular(fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFacturaElectronicaResponse(EFacturaElectronicaResponse obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarFacturaElectronicaResponse(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public void modificarFacturaElectronicaResponse(int idCabecera)
        {
            try
            {
                objVentasData.modificarFacturaElectronicaResponse(idCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarFacturaElectronicaResponseAnular(EFacturaElectronicaResponse obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarFacturaElectronicaResponseAnular(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public void modificarFacturaElectronicaResponseAnular(int idCabecera)
        {
            try
            {
                objVentasData.modificarFacturaElectronicaResponseAnular(idCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EContrato listarContratoPorIcod(int cntc_icod_contrato)
        {
            EContrato obj = new EContrato();
            try
            {
                obj = objVentasData.listarContratoPorIcod(cntc_icod_contrato);
            }
            catch (Exception)
            {

                throw;
            }
            return obj;
        }

        public void eliminarFacturaVentaElectronica(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaVentaElectronica(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoNoComercialElectronica(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaCreditoNoComercialElectronica(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitolElectronica(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaDebitolElectronica(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoElectronica(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaCreditoElectronica(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaElectronica(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarBoletaElectronica(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarFacturaElectronicaResponse(int id, int estadoSunat)
        {
            try
            {
                objVentasData.ActualizarFacturaElectronicaEstado(id, estadoSunat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarFacturaElectronicaResponseAnulacion(int id, int estadoSunat)
        {
            try
            {
                objVentasData.actualizarFacturaElectronicaResponseAnulacion(id, estadoSunat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarfacturaVentaElectronicaAnulado(EFacturaCab oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarfacturaVentaElectronicaAnulado(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ESunatResumenDet> listarSunatResumenDet()
        {
            List<ESunatResumenDet> lista = new List<ESunatResumenDet>();
            try
            {
                lista = objVentasData.listarSunatResumenDet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarSunatResumenDet(ESunatResumenDet obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarSunatResumenDet(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public int insertarResumenDiarioResponse(EResumenResponse obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarResumenDiariaResponse(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public int insertarDocumentosBajaResponse(ESunatDocumentosBajaResponse obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarDocumentosBajaResponse(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public List<ESunatDocumentosBaja> listarSunatDocumentosBajaCab(DateTime fechaInicio)
        {
            List<ESunatDocumentosBaja> lista = new List<ESunatDocumentosBaja>();
            try
            {
                lista = objVentasData.listarSunatDocumentosBajaCab(fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarSunatDocumentosBajaCab(ESunatDocumentosBaja obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarSunatDocumentosBajaCab(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public void actualizarDocumentosBajaResponse(int id, int estadoSunat)
        {
            try
            {
                objVentasData.actualizarDocumentosBajaResponse(id, estadoSunat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Factura Venta Electronica Detalle
        public List<EFacturaVentaDetalleElectronico> listarfacturaVentaElectronicaDetalle(int facvd_icod_fac_venta)
        {
            List<EFacturaVentaDetalleElectronico> lista = new List<EFacturaVentaDetalleElectronico>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronicaDetalle(facvd_icod_fac_venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarfacturaVentaElectronicaDetalle(EFacturaDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarfacturaVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarBoletaVentaElectronicaDetalle(EBoletaDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarBoletaVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarNotaCreditoVentaElectronicaDetalle(ENotaCreditoDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarNotaCreditoVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarNotaCreditoVentaNoComercialElectronicaDetalle(ENotaCreditoDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarNotaCreditoVentaNoComercialElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarNotaDebitoVentaElectronicaDetalle(ENotaDebitoDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarNotaDebitoVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Elimina_Pago(int pgc_icod_pago)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.Elimina_Pago(pgc_icod_pago);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminarFacturaVentaElectronicaDetalle(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaVentaElectronicaDetalle(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoVentaElectronicaDetalle(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaCreditoVentaElectronicaDetalle(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaElectronicaDetalle(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarBoletaElectronicaDetalle(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoNoComercialVentaElectronicaDetalle(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaCreditoNoComercialVentaElectronicaDetalle(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitoVentaElectronicaDetalle(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaDebitoVentaElectronicaDetalle(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Factura
        public List<EFacturaCab> getFacturaCab(int id_factura)
        {
            List<EFacturaCab> lst = new List<EFacturaCab>();
            try
            {
                lst = objVentasData.getFacturaCab(id_factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public void Actualizar_Pagos(int cntc_icod_contrato_cuotas, int pgc_icod_pago)
        {
            try
            {
                objVentasData.Actualizar_Pagos(cntc_icod_contrato_cuotas, pgc_icod_pago);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Insertar_Pagos(EPagosCuotas objPg)
        {
            int codigo = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    codigo = objVentasData.Insertar_Pagos(objPg);
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return codigo;
        }

        public List<EFacturaCab> listarFacturaCab(int intEjericio)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>();
            try
            {
                lista = objVentasData.listarFacturaCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFactura(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet)
        {

            int intIcodE = 0;
            try
            {
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = 6;//Mercaderia
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.doxcc_nporcentaje_ivap = objEFactura.favc_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = objEFactura.favc_nmonto_ivap;
                    objDXC.vendc_icod_vendedor = objEFactura.vendc_icod_vendedor;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEFactura.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;

                    #endregion
                    #region Factura Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS
                    //var lst = objAdminSistemaData.getCorrelativoTipoDocumento(Parametros.intTipoDocFacturaVenta,);

                    //objEFactura.favc_vnumero_factura = String.Format("{0:000}{1:0000000}", lst[0].tdocc_nro_serie, (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1));
                    objEFactura.favc_icod_factura = objVentasData.insertarFactura(objEFactura);
                    objEFactura.doc_icod_documento = objEFactura.favc_icod_factura;
                    intIcodE = objVentasData.insertarfacturaVentaElectronica(objEFactura);
                    //ACTUALIZAR EL CORRELATIVO DE LA FACTURA
                    //objAdminSistemaData.updateCorrelativoTipoDocumento(Parametros.intTipoDocFacturaVenta, Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(3, 7)), 1);
                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(objEFactura.favc_icod_pvt, Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)), 1);
                    //SI LA FACTURA REFERENCIA A UNA OT, LA OT DEBE DE CAMBIAR DE SITUACION A FACTURADA


                    #endregion

                    #region Factura Det. Insertar
                    if (objEFactura.remic_icod_remision == 0)
                    {
                        lstFacDet.ForEach(x =>
                        {

                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                            obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            obKardex.kardc_beneficiario = objEFactura.cliec_vnombre_cliente;
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = objEFactura.intUsuario;
                            obKardex.strPc = objEFactura.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            //verificar stock del producto
                            decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, Convert.ToInt32(x.almac_icod_almacen), x.prdc_icod_producto);
                            if (Stock_Producto < Convert.ToDecimal(x.favd_ncantidad))
                            {
                                throw new Exception("Stock insuficiente para el producto: " + x.strDesProducto + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                            }

                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.favd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            x.favc_icod_factura = intIcodE;
                            insertarfacturaVentaElectronicaDetalle(x);
                            x.favc_icod_factura = objEFactura.favc_icod_factura;
                            insertarFacturaDetalle(x);
                        });
                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                       {
                           x.favc_icod_factura = intIcodE;
                           insertarfacturaVentaElectronicaDetalle(x);
                           x.kardc_icod_correlativo = null;
                           x.favc_icod_factura = objEFactura.favc_icod_factura;
                           insertarFacturaDetalle(x);
                       });

                    }
                    #endregion

                    if (objEFactura.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                            26, //TIPO DE DOCUMENTO
                            objEFactura.favc_icod_factura, //ICOD_DOCUEMTNO
                            219, //FACTURADO
                            objEFactura.intUsuario,
                            objEFactura.strPc);

                    }
                    tx.Complete();
                }
                return objEFactura.favc_icod_factura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EVendedor ObtenerIECDesdeFormatos(string Formato)
        {
            EVendedor obj = new EVendedor();
            try
            {
                obj = objVentasData.ObtenerIECDesdeFormatos(Formato);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public void Modificar_Pagos(EContratoCuotas obj)
        {
            try
            {
                if (obj.pgc_icod_pago > 0)// MODIFICA
                {
                    EPagosCuotas objPg = new EPagosCuotas();

                    objPg.pgc_icod_pago = obj.pgc_icod_pago;
                    objPg.cntc_icod_contrato_cuotas = obj.cntc_icod_contrato_cuotas;
                    objPg.pgc_nmonto_pago = obj.monto_pagar;
                    objPg.pgc_sfecha_pago = Convert.ToDateTime(obj.cntc_sfecha_pago_cuota);
                    objPg.tdocc_icod_tipo_doc = obj.tdocc_icod_tipo_doc;
                    objPg.cntc_icod_documento = obj.cntc_icod_documento;
                    objPg.pgc_iusuario_modifica = obj.intUsuario;
                    objPg.pgc_iusuario_modifica = obj.intUsuario;
                    objPg.pgc_nmonto_pago_mora = obj.cntc_nmonto_mora_pago;
                    objVentasData.Modificar_Pagos(objPg);
                    objVentasData.Actualizar_Pagos(objPg.cntc_icod_contrato_cuotas, objPg.pgc_icod_pago);
                    objVentasData.ActualizarContrato(obj.cntc_icod_contrato);

                }
                else // INSERTA
                {
                    EPagosCuotas objPg = new EPagosCuotas();

                    objPg.cntc_icod_contrato_cuotas = obj.cntc_icod_contrato_cuotas;
                    objPg.pgc_nmonto_pago = obj.monto_pagar;
                    objPg.pgc_sfecha_pago = Convert.ToDateTime(obj.cntc_sfecha_pago_cuota);
                    objPg.tdocc_icod_tipo_doc = obj.tdocc_icod_tipo_doc;
                    objPg.cntc_icod_documento = obj.cntc_icod_documento;
                    objPg.pgc_iusuario_modifica = obj.intUsuario;
                    objPg.pgc_nmonto_pago_mora = obj.cntc_nmonto_mora_pago;
                    objPg.pgc_icod_pago = objVentasData.Insertar_Pagos(objPg);
                    objVentasData.Actualizar_Pagos(objPg.cntc_icod_contrato_cuotas, objPg.pgc_icod_pago);
                    objVentasData.ActualizarContrato(obj.cntc_icod_contrato);
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ActualizarContrato(int contrato)
        {
            try
            {
                objVentasData.ActualizarContrato(contrato);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int insertarFacturaServio(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet)
        {

            int intIcodE = 0;
            try
            {
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEFactura.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;

                    #endregion
                    #region Factura Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS
                    //var lst = objAdminSistemaData.getCorrelativoTipoDocumento(Parametros.intTipoDocFacturaVenta,);

                    //objEFactura.favc_vnumero_factura = String.Format("{0:000}{1:0000000}", lst[0].tdocc_nro_serie, (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1));
                    objEFactura.favc_icod_factura = objVentasData.insertarFactura(objEFactura);
                    objEFactura.doc_icod_documento = objEFactura.favc_icod_factura;
                    intIcodE = objVentasData.insertarfacturaVentaElectronica(objEFactura);
                    //ACTUALIZAR EL CORRELATIVO DE LA FACTURA
                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(objEFactura.favc_icod_pvt, Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)), 1);
                    //SI LA FACTURA REFERENCIA A UNA OT, LA OT DEBE DE CAMBIAR DE SITUACION A FACTURADA


                    #endregion

                    #region Factura Det. Insertar
                    if (objEFactura.remic_icod_remision == 0)
                    {
                        lstFacDet.ForEach(x =>
                        {

                            //#region Salida de Kardex
                            //EKardex obKardex = new EKardex();
                            //obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                            //obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                            //obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            //obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            //obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                            //obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            //obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                            //obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            //obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            //obKardex.kardc_beneficiario = objEFactura.cliec_vnombre_cliente;
                            //obKardex.kardc_observaciones = "";
                            //obKardex.intUsuario = objEFactura.intUsuario;
                            //obKardex.strPc = objEFactura.strPc;
                            //x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            //#endregion
                            ////verificar stock del producto
                            //decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, Convert.ToInt32(x.almac_icod_almacen), x.prdc_icod_producto);
                            //if (Stock_Producto < Convert.ToDecimal(x.favd_ncantidad))
                            //{
                            //    throw new Exception("Stock insuficiente para el producto: " + x.strDesProducto + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                            //}

                            //#region Actualizando Stock
                            //EStock stck = new EStock();
                            //stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                            //stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            //stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            //stck.stocc_stock_producto = x.favd_ncantidad;
                            //stck.intTipoMovimiento = 0;
                            //objAlmacenData.actualizarStock(stck);
                            //#endregion

                            x.favc_icod_factura = intIcodE;
                            insertarfacturaVentaElectronicaDetalle(x);
                            x.favc_icod_factura = objEFactura.favc_icod_factura;
                            insertarFacturaServicioDetalle(x);
                        });
                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                        {
                            x.kardc_icod_correlativo = null;
                            x.favc_icod_factura = objEFactura.favc_icod_factura;
                            insertarFacturaServicioDetalle(x);
                        });

                    }
                    #endregion

                    if (objEFactura.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                            26, //TIPO DE DOCUMENTO
                            objEFactura.favc_icod_factura, //ICOD_DOCUEMTNO
                            219, //FACTURADO
                            objEFactura.intUsuario,
                            objEFactura.strPc);

                    }
                    tx.Complete();
                }
                return objEFactura.favc_icod_factura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ObtenerExistenciaSerie(string serie)
        {
            bool existe = false;
            try
            {
                existe = objVentasData.ObtenerExistenciaSerie(serie);
            }
            catch (Exception)
            {

                throw;
            }
            return existe;
        }

        public int insertarFacturaDesdePlanilla(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, ref EPlanillaCobranzaCab oBePlnCab,
           EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos, List<EContratoCuotas> lstContrato)
        {
            //ING. EDGAR ALFARO
            //FECHA: 08/01/2014
            TesoreriaData objTesoreriaData = new TesoreriaData();
            AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
            OperacionesData objOperacionesData = new OperacionesData();
            ContabilidadData objContabilidadData = new ContabilidadData();
            CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //


                    #region Factura
                    #region Factura Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS
                    var lst = new BVentas().getCorrelativoRP(oBePlnCab.plnc_icod_pvt);
                    objEFactura.favc_icod_factura = objVentasData.insertarFacturaPlanilla(objEFactura);
                    objEFactura.doc_icod_documento = objEFactura.favc_icod_factura;
                    intIcodE = objVentasData.insertarfacturaVentaElectronica(objEFactura);
                    if (objEFactura.favc_icod_factura == 0)
                    {
                        throw new Exception("El número de la Factura ya fue utilizado, intente con un número de Factura superior");
                    }
                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(1, Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)), 1);
                    //List<EContratoCuotas> lstCuotas = new BVentas().listarContratoCuotas(objEFactura.cntc_icod_contrato).Where(x=> x.cntc_icod_contrato_cuotas == objEFactura.cntc_icod_contrato_cuotas).ToList();
                    //if (lstCuotas.Count > 0)
                    //{
                    //    lstCuotas.ForEach(x =>
                    //    {
                    //        if (objEFactura.favc_nmonto_total >= x.cntc_nmonto_cuota)
                    //        {
                    //            x.cntc_icod_situacion = 340;
                    //            new BVentas().modificarContratoCuotasSituacion(x);
                    //        }
                    //        else if (objEFactura.favc_nmonto_total > 0)
                    //        {
                    //            x.cntc_icod_situacion = 339;
                    //            new BVentas().modificarContratoCuotasSituacion(x);
                    //        }

                    //    });
                    //}
                    lstContrato.Where(x => x.flag_multiple == true).ToList().ForEach(x =>
                     {
                         //if (objEFactura.favc_nmonto_total >= x.cntc_nmonto_cuota)
                         //{
                         x.tdocc_icod_tipo_doc = Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc);
                         x.cntc_icod_documento = objEFactura.favc_icod_factura;
                         x.cntc_icod_situacion = 340;
                         new BVentas().modificarContratoCuotasSituacion(x);
                         new BVentas().modificarContratoCuotasDocumentos(x);
                         //}
                         //else if (objEFactura.favc_nmonto_total > 0)
                         //{
                         //    x.cntc_icod_situacion = 339;
                         //    new BVentas().modificarContratoCuotasSituacion(x);
                         //}

                     });



                    //ACTUALIZAR EL CORRELATIVO DE LA FACTURA


                    #endregion
                    #region Factura Det. Insertar
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    lstFacDet.ForEach(x =>
                    {
                        if (x.intClasificacionProducto != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            //SI LA FACTURA <<NO>> REFERENCIA A UNA <<OT>> ENTONCES ....
                            if (Convert.ToInt32(objEFactura.orpc_iid_orden_trabajo) == 0)
                            {
                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                                obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                                obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                                obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = objEFactura.intUsuario;
                                obKardex.strPc = objEFactura.strPc;
                                //x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.favd_ncantidad;
                                stck.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                            }
                            //SI LA FACTURA <<SI>> REFERENCIA A UNA <<OT>> ENTONCES ....


                        }
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = x.strDesProducto;
                        }
                        x.IdCabezera = intIcodE;
                        insertarfacturaVentaElectronicaDetalle(x);
                        x.favc_icod_factura = objEFactura.favc_icod_factura;
                        insertarFacturaDetallePlanilla(x);
                    });
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.strDesCliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    //objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    //objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    //objDXC.doxcc_nmonto_exportacion = null;
                    //objDXC.doxcc_nmonto_servicio_no_domic = null;
                    objDXC.doxcc_nmonto_afecto = 0;
                    objDXC.doxcc_nmonto_inafecto = objEFactura.favc_nmonto_neto;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    //objDXC.doxcc_nporcentaje_isc = null;
                    //objDXC.doxcc_nmonto_isc = null;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    //objDXC.tabl_iid_tipo_indicador_motivo_cta_xcobrar = null;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    //objDXC.doxcc_icod_vendedor = null;
                    //objDXC.doxcc_nmonto_iva = null;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEFactura.dxcc_iid_doc_por_cobrar = objCuentaCobrarData.insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEFactura.dxcc_iid_doc_por_cobrar;
                    //EL ID DEL DOC POR COBRAR SE ASIGNA A LA FACTURA, PARA ESTO SE REALIZA UN PEQUEÑO UPDATE
                    objVentasData.ActualizarIdDxcFactura(objEFactura.favc_icod_factura, objEFactura.dxcc_iid_doc_por_cobrar);
                    #endregion

                    #region Pagos de la factura
                    lstPagos.ForEach(x =>
                    {
                        //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                        x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocFacturaVenta;
                        x.pgoc_icod_documento = objEFactura.favc_icod_factura;
                        x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                        x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                        x.pgoc_vnumero_planilla = strNroPlanCab;
                        x.pgoc_icod_cliente = objEFactura.favc_icod_cliente;
                        //TOMAR EN CUENTA LOS TIPOS DE PAGO
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta de Credito
                            //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            var lstTipoTarjeta = listarTipoTarjeta();
                            ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                            oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "PVD";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            //oBeBcoMovCab.mobac_origen_regitro = null;

                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                            oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //
                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBePagoDXC1.pdxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.insertarDXCPago(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            //EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            //x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //
                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            oBePagoDXC1.doxcc_icod_correlativo = objEFactura.dxcc_iid_doc_por_cobrar;
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                            oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                            oBePagoDXC1.pdxcc_sfecha_cobro = objEFactura.favc_sfecha_factura;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarDXCPago(oBePagoDXC1);
                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(objEFactura.dxcc_iid_doc_por_cobrar, 0);
                            //adelanto pago
                            //hola
                            //EAdelantoPago oBe = new EAdelantoPago();
                            //oBe.doxcc_icod_correlativo = objEFactura.dxcc_iid_doc_por_cobrar; //el documento a pagar
                            //oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(x.pgoc_icod_anticipo); //correlativo del adelanto
                            //oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta; //tipo documento(adelanto)
                            //oBe.tdocc_iid_correlativo = Convert.ToInt32(oBePagoDXC1.tdodc_iid_correlativo); //clase del documento
                            //oBe.adpac_vnumero_doc_adelanto = objEFactura.favc_vnumero_factura; //ndoc del adelanto con que se va a pagar
                            //oBe.cliec_icod_cliente = Convert.ToInt32(x.pgoc_icod_cliente); //código del cliente
                            //oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                            //oBe.adpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                            //oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEFactura.favc_sfecha_factura); //tipo de cambio de la fecha seleccionada
                            //oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", x.pgoc_vnumero_planilla, x.pgoc_descripcion.ToUpper());
                            //oBe.adpac_sfecha_pago = Convert.ToDateTime(objEFactura.favc_sfecha_factura); //fecha del pago
                            //oBe.adpac_iorigen = "P"; //Adelanto
                            //oBe.adpac_iusuario_crea = x.intUsuario;
                            //oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            //oBe.adpac_iusuario_modifica = x.intUsuario;
                            //oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                            //oBe.adpac_isituacion = 108;
                            //oBe.adpac_flag_estado = true;
                            //oBe.SaldoDXCAdelanto = Convert.ToDecimal(x.pgoc_nmonto);
                            //oBe.doxcc_nmonto_pagado = 0;
                            //x.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                            //new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(x.pgoc_icod_anticipo), 0);

                        }

                        //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                        objVentasData.insertarPago(x);
                    });
                    #endregion
                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEFactura.dxcc_iid_doc_por_cobrar, 0);
                    #endregion
                    #region Planilla Cab
                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }
                    //INSERTAR EL DET. DE LA PLANILLA
                    #endregion
                    #region Planilla Det
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    oBePlnDet.tablc_iid_tipo_mov = Parametros.intPlnFacturacion;
                    oBePlnDet.plnd_icod_documento = objEFactura.favc_icod_factura;
                    //INGRESAR EL REGISTRO DETALLE DE LA PLANILLA (Ej. Facturacion, pago o anticipo - En este caso es Facturacion)
                    oBePlnDet.plnd_icod_detalle = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);
                    lstContrato.Where(x => x.flag_multiple == true).ToList().ForEach(x =>
                    {
                        EPagosCuotas objPg = new EPagosCuotas();
                        objPg.cntc_icod_contrato_cuotas = x.cntc_icod_contrato_cuotas;
                        objPg.pgc_nmonto_pago = x.cntc_nmonto_cuota;
                        objPg.pgc_sfecha_pago = Convert.ToDateTime(objEFactura.fechaEmision);
                        objPg.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        objPg.cntc_icod_documento = Convert.ToInt32(oBePlnDet.plnd_icod_documento);
                        objPg.pgc_iusuario_crea = objEFactura.intUsuario;
                        objPg.pgc_vpc_crea = WindowsIdentity.GetCurrent().Name;
                        objPg.pgc_nmonto_pago_mora = x.cntc_nmonto_mora_pago;
                        objPg.pgc_icod_pago = new BVentas().Insertar_Pagos(objPg);
                        //ACTUALIZAR PAGOS CUOTAS
                        new BVentas().Actualizar_Pagos(objPg.cntc_icod_contrato_cuotas, objPg.pgc_icod_pago);
                    });
                    #endregion
                    tx.Complete();
                    return oBePlnDet.plnd_icod_detalle;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarFacturaDesdePlanilla(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, List<EFacturaDet> lstFacDelete,
             EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos, List<EPagoDocVenta> lstDeletePagos, List<EContratoCuotas> lstContrato)
        {
            try
            {
                //ING. EDGAR ALFARO
                //FECHA: 08/01/2014
                TesoreriaData objTesoreriaData = new TesoreriaData();
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //
                    #region Factura
                    //MODIFICAR LA CAB. DE LA FACTURA
                    objVentasData.modificarFacturaPlanilla(objEFactura);

                    //lstContrato.ForEach(x =>
                    //{
                    //    if (x.flag_multiple != x.flag_multiple_anterior)
                    //    {
                    //        x.tdocc_icod_tipo_doc = 0;
                    //        x.cntc_icod_documento = 0;
                    //        x.cntc_icod_situacion = 338;
                    //        new BVentas().modificarContratoCuotasSituacion(x);
                    //        new BVentas().modificarContratoCuotasDocumentos(x);
                    //    }

                    //});

                    //lstContrato.Where(x => x.flag_multiple == true).ToList().ForEach(x =>
                    //{
                    //    x.tdocc_icod_tipo_doc = Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc);
                    //    x.cntc_icod_documento = objEFactura.favc_icod_factura;
                    //    x.cntc_icod_situacion = 340;
                    //    new BVentas().modificarContratoCuotasSituacion(x);
                    //    new BVentas().modificarContratoCuotasDocumentos(x);

                    //});

                    //
                    #region Factura Det.

                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    lstFacDet.ForEach(x =>
                    {
                        if (x.intClasificacionProducto != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            if (x.intTipoOperacion == 1)
                            {

                                //SE INSERTA EL ITEM DE LA FACTURA
                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaDetallePlanilla(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {

                                objVentasData.modificarFacturaDetallePlanilla(x);

                            }
                            if (i == 1)
                            {
                                vdescripcion_1erProducto = x.strDesProducto;
                            }

                        }

                    });
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.strDesCliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;

                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;

                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;

                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;

                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    #region Pagos de la factura
                    //PRIMERO SE DEBE ELIMINAR, TODOS LOS PAGOS PARA REINGRESAR LOS PAGOS
                    var lstPagosAux = new BVentas().listarPago(Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc), Convert.ToInt32(oBePlnDet.plnd_icod_documento));
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });
                    lstPagos.ForEach(x =>
                    {
                        //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                        x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocFacturaVenta;
                        x.pgoc_icod_documento = objEFactura.favc_icod_factura;
                        x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                        x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                        x.pgoc_vnumero_planilla = strNroPlanCab;
                        x.pgoc_icod_cliente = objEFactura.favc_icod_cliente;
                        //TOMAR EN CUENTA LOS TIPOS DE PAGO
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta de Credito
                            //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            var lstTipoTarjeta = listarTipoTarjeta();
                            ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                            oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "PVD";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            oBeBcoMovCab.mobac_origen_regitro = null;

                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                            oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //
                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();

                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBePagoDXC1.pdxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.insertarDXCPago(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR

                            //
                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();

                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(objEFactura.dxcc_iid_doc_por_cobrar);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                            oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                            oBePagoDXC1.pdxcc_sfecha_cobro = objEFactura.favc_sfecha_factura;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarDXCPago(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_anticipo), 0);


                        }

                        //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                        objVentasData.insertarPago(x);
                    });
                    #endregion
                    //opcional 

                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEFactura.doxcc_icod_correlativo, 0);
                    #endregion
                    #region Planilla Det.
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    //MODIFICANDO LA PLANILLA DET.
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);


                    #endregion

                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().FacturaVentaElectronicaObtenerDoc(objEFactura.favc_icod_factura,"01").ToList();
                    objEFactura.IdCabecera = lstCab[0].IdCabecera;
                    objVentasData.modificarfacturaVentaElectronica(objEFactura);
                    if (lstCab.Count > 0)
                    {
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstFacDet)
                        {
                            ob.IdCabezera = lstCab[0].IdCabecera;
                            insertarfacturaVentaElectronicaDetalle(ob);
                        }
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void modificarFactura(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, List<EFacturaDet> lstFacDelete)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion

                    objVentasData.modificarFactura(objEFactura);

                    #region Factura Det.
                    if (objEFactura.remic_icod_remision == 0)
                    {
                        lstFacDelete.ForEach(x =>
                        {

                            objVentasData.eliminarFacturaDetalle(x);
                        });

                        lstFacDet.ForEach(x =>
                        {

                            if (x.intTipoOperacion == 1)
                            {

                                //SE INSERTA EL ITEM DE LA FACTURA
                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {



                                //SE MODIFICA EL ITEM DEL DETALLE DE LA FACTURA
                                objVentasData.modificarFacturaDetalle(x);

                            }


                        });
                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                        {
                            if (x.intTipoOperacion == 1)
                            {

                                x.kardc_icod_correlativo = null;
                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                objVentasData.modificarFacturaDetalle(x);

                            }
                        });

                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.vendc_icod_vendedor = objEFactura.vendc_icod_vendedor;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;

                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;

                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == objEFactura.doc_icod_documento).ToList();
                    if (lstCab.Count > 0)
                    {
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstFacDet)
                        {
                            ob.IdCabezera = lstCab[0].IdCabecera;
                            insertarfacturaVentaElectronicaDetalle(ob);
                        }
                    }
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaNumero(EFacturaCab objEFactura)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA

                    objVentasData.modificarFactura(objEFactura);

                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;

                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxcc_nporcentaje_ivap = objEFactura.favc_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = objEFactura.favc_nmonto_ivap;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;

                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaVenta(EFacturaCab objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFactura(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaServicios(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, List<EFacturaDet> lstFacDelete)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion

                    objVentasData.modificarFactura(objEFactura);

                    #region Factura Det.
                    if (objEFactura.remic_icod_remision == 0)
                    {
                        lstFacDelete.ForEach(x =>
                        {



                            objVentasData.eliminarFacturaDetalle(x);
                        });

                        lstFacDet.ForEach(x =>
                        {

                            if (x.intTipoOperacion == 1)
                            {


                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {


                                objVentasData.modificarFacturaDetalle(x);

                            }


                        });
                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                x.almac_icod_almacen = null;
                                x.kardc_icod_correlativo = null;
                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                objVentasData.modificarFacturaDetalle(x);

                            }
                        });

                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;

                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == objEFactura.doc_icod_documento).ToList();
                    if (lstCab.Count > 0)
                    {
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstFacDet)
                        {
                            ob.favc_icod_factura = lstCab[0].IdCabecera;
                            insertarfacturaVentaElectronicaDetalle(ob);
                        }
                    }
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularFacturaVenta(EFacturaCab objEFactura)
        {
            #region Eliminar DXC
            EDocXCobrar objDXC = new EDocXCobrar();
            objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
            objDXC.intUsuario = objEFactura.intUsuario;
            objDXC.strPc = objEFactura.strPc;
            new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
            #endregion Eliminar DXC

            List<EFacturaDet> Mlist = new List<EFacturaDet>();
            Mlist = listarFacturaDetalle(objEFactura.favc_icod_factura);
            foreach (var x in Mlist)
            {

                #region Eliminar Kardex
                EKardex obKardexDel = new EKardex();
                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                obKardexDel.intUsuario = objEFactura.intUsuario;
                obKardexDel.strPc = objEFactura.strPc;
                objAlmacenData.eliminarKardex(obKardexDel);
                #endregion
                #region Actualizando Stock
                EStock stck = new EStock();
                stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                stck.stocc_stock_producto = x.favd_ncantidad;
                stck.intTipoMovimiento = 0;
                objAlmacenData.actualizarStock(stck);
                #endregion

                eliminarFacturaDetalle(x);
            }
            //ELIMAINAR LA RELACION CON LA GUIA DE REMISION
            if (objEFactura.remic_icod_remision != 0)
            {
                objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                    0, //TIPO DE DOCUMENTO
                    0, //ICOD_DOCUEMTNO
                    218, //GENERADO
                    objEFactura.intUsuario,
                    objEFactura.strPc);

            }
            //------------------------------

            objVentasData.anularFactura(objEFactura);
        }
        public void anularFactura(EFacturaCab objEFactura)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocFacturaVenta, objEFactura.favc_icod_factura);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DE LA NOTA DE CREDITO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });

                    List<EFacturaDet> Mlist = new List<EFacturaDet>();
                    Mlist = listarFacturaDetalle(objEFactura.favc_icod_factura);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEFactura.intUsuario;
                        obKardexDel.strPc = objEFactura.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.favd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);

                        #endregion
                        objVentasData.eliminarFacturaDetalle(x);
                    }

                    objVentasData.anularFactura(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFacturaVenta(EFacturaCab objEFactura)
        {
            #region Eliminar DXC
            EDocXCobrar objDXC = new EDocXCobrar();
            objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
            objDXC.intUsuario = objEFactura.intUsuario;
            objDXC.strPc = objEFactura.strPc;
            new CuentasPorCobrarData().eliminarDxc(objDXC);
            #endregion Eliminar DXC

            List<EFacturaDet> Mlist = new List<EFacturaDet>();
            Mlist = listarFacturaServicioDetalle(objEFactura.favc_icod_factura);
            foreach (var x in Mlist)
            {

                #region Eliminar Kardex
                EKardex obKardexDel = new EKardex();
                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                obKardexDel.intUsuario = objEFactura.intUsuario;
                obKardexDel.strPc = objEFactura.strPc;
                objAlmacenData.eliminarKardex(obKardexDel);
                #endregion
                #region Actualizando Stock
                EStock stck = new EStock();
                stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                stck.stocc_stock_producto = x.favd_ncantidad;
                stck.intTipoMovimiento = 0;
                objAlmacenData.actualizarStock(stck);
                #endregion

                eliminarFacturaDetalle(x);
            }
            //eliminar la relacion con la Guia de Remision
            if (objEFactura.remic_icod_remision != 0)
            {
                objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                    0, //TIPO DE DOCUMENTO
                    0, //ICOD_DOCUEMTNO
                    218, //GENERADO
                    objEFactura.intUsuario,
                    objEFactura.strPc);

            }
            //------------------------------


            objVentasData.eliminarFactura(objEFactura);
        }
        public void eliminarFactura(EFacturaCab objEFactura)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.dxcc_iid_doc_por_cobrar;
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;

                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;

                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion

                    List<EFacturaDet> listaFactura = listarFacturaDetallePlanilla(objEFactura.favc_icod_factura);
                    foreach (var x in listaFactura)
                    {
                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEFactura.intUsuario;
                        obKardexDel.strPc = objEFactura.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = Parametros.intEjercicio;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.favd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                        objVentasData.eliminarFacturaDetallePlanilla(x);
                    }

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocFacturaVenta, objEFactura.favc_icod_factura);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DE LA NOTA DE CREDITO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });

                    objVentasData.eliminarFacturaPlanilla(objEFactura);


                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EFacturaDet> listarFacturaDetalle(int intFactura)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                lista = objVentasData.listarFacturaDetalle(intFactura, Parametros.intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        public List<EFacturaDet> listarFacturaDetalle_NTC(int intFactura)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                lista = objVentasData.listarFacturaDetalle_NTC(intFactura, Parametros.intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EFacturaDet> listarFacturaServicioDetalle(int intFactura)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                lista = objVentasData.listarFacturaServicioDetalle(intFactura, Parametros.intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarFacturaDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarFacturaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarFacturaServicioDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarFacturaServicioDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFacturaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaServicioDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFacturaServicioDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaServicioDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaServicioDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarAfectoFactura(int favc_icod_factura, bool favc_bafecto_igv)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarAfectoFactura(favc_icod_factura, favc_bafecto_igv);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Evento 
        public List<EEventoVenta> ListarEventoVenta()
        {
            List<EEventoVenta> lista = null;
            try
            {
                lista = objVentasData.ListarEventoVenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarEventoVenta(EEventoVenta objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.InsertarEventoVenta(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarEventoVenta(EEventoVenta objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarEventoVenta(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarEventoVenta(int evev_icod_evento_venta, int intUsuario, string strPc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarEventoVenta(evev_icod_evento_venta, intUsuario, strPc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Boleta
        public List<EBoletaCab> getBoletaCab(int id_boleta)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>();
            try
            {
                lista = objVentasData.getBoletaCab(id_boleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EBoletaCab> listarBoletaCab(int intEjericio)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>();
            try
            {
                lista = objVentasData.listarBoletaCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarBoletaDesdePlanilla(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet, ref EPlanillaCobranzaCab oBePlnCab,
        EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos, List<EContratoCuotas> lstContrato)
        {
            //ING. EDGAR ALFARO
            //FECHA: 08/01/2014
            TesoreriaData objTesoreriaData = new TesoreriaData();
            AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
            OperacionesData objOperacionesData = new OperacionesData();
            ContabilidadData objContabilidadData = new ContabilidadData();
            CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //
                    #region Boleta
                    #region Boleta Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS

                    var lst = new BVentas().getCorrelativoRP(oBePlnCab.plnc_icod_pvt);
                    objEBoleta.bovc_icod_boleta = objVentasData.insertarBoletaPlanilla(objEBoleta);
                    objEBoleta.doc_icod_documento = objEBoleta.bovc_icod_boleta;
                    intIcodE = objVentasData.insertarBoletaVentaElectronica(objEBoleta);
                    if (objEBoleta.bovc_icod_boleta == 0)
                    {
                        throw new Exception("El número de la Boleta ya fue utilizado, intente con un número de Factura superior");
                    }
                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(1, Convert.ToInt32(objEBoleta.bovc_vnumero_boleta.Substring(4, 8)), 2);

                    lstContrato.Where(x => x.flag_multiple == true).ToList().ForEach(x =>
                    {
                        x.tdocc_icod_tipo_doc = Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc);
                        x.cntc_icod_documento = objEBoleta.bovc_icod_boleta;
                        x.cntc_icod_situacion = 340;
                        new BVentas().modificarContratoCuotasSituacion(x);
                        new BVentas().modificarContratoCuotasDocumentos(x);

                    });



                    #endregion
                    #region Boleta Det. Insertar
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    lstBolDet.ForEach(x =>
                    {
                        if (x.intClasificacionProducto != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            //SI LA FACTURA <<NO>> REFERENCIA A UNA <<OT>> ENTONCES ....

                            //SI LA FACTURA <<SI>> REFERENCIA A UNA <<OT>> ENTONCES ....

                        }
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = x.strDesProducto;
                        }
                        x.IdCabezera = intIcodE;
                        insertarBoletaVentaElectronicaDetalle(x);
                        x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                        objVentasData.insertarBoletaDetallePlanilla(x);
                    });
                    #endregion
                    #region Doc. Por Cobrar de la boleta
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = 9;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.strDesCliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la boleta, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    //objDXC.doxcc_nmonto_exportacion = null;
                    //objDXC.doxcc_nmonto_servicio_no_domic = null;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    //objDXC.doxcc_nporcentaje_isc = null;
                    //objDXC.doxcc_nmonto_isc = null;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    //objDXC.tabl_iid_tipo_indicador_motivo_cta_xcobrar = null;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    //objDXC.doxcc_icod_vendedor = null;
                    //objDXC.doxcc_nmonto_iva = null;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    objDXC.docxc_icod_documento = objEBoleta.bovc_icod_boleta;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEBoleta.dxcc_iid_doc_por_cobrar = objCuentaCobrarData.insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                    //EL ID DEL DOC POR COBRAR SE ASIGNA A LA BOLETA, PARA ESTO SE REALIZA UN PEQUEÑO UPDATE
                    objVentasData.ActualizarIdDxcBoleta(objEBoleta.bovc_icod_boleta, objEBoleta.dxcc_iid_doc_por_cobrar);
                    #endregion
                    #region Pagos de la boleta
                    lstPagos.ForEach(x =>
                    {
                        //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                        x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocBoletaVenta;
                        x.pgoc_icod_documento = objEBoleta.bovc_icod_boleta;
                        x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                        x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                        x.pgoc_vnumero_planilla = strNroPlanCab;
                        x.pgoc_icod_cliente = objEBoleta.bovc_icod_cliente;
                        //TOMAR EN CUENTA LOS TIPOS DE PAGO
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta Credito
                            //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            var lstTipoTarjeta = listarTipoTarjeta();
                            ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                            oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.bovc_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "PVD";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            oBeBcoMovCab.mobac_origen_regitro = null;

                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocBoletaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.bovc_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //
                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            oBePagoDXC1.pdxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.insertarDXCPago(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {

                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(objEBoleta.dxcc_iid_doc_por_cobrar);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                            oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.insertarDXCPago(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(objEBoleta.dxcc_iid_doc_por_cobrar), 0);

                            //EAdelantoPago oBe = new EAdelantoPago();
                            //oBe.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar; //el documento a pagar
                            //oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(x.pgoc_icod_anticipo); //correlativo del adelanto
                            //oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta; //tipo documento(adelanto)
                            //oBe.tdocc_iid_correlativo = Convert.ToInt32(oBePagoDXC1.tdodc_iid_correlativo); //clase del documento
                            //oBe.adpac_vnumero_doc_adelanto = objEBoleta.bovc_vnumero_boleta; //ndoc del adelanto con que se va a pagar
                            //oBe.cliec_icod_cliente = Convert.ToInt32(x.pgoc_icod_cliente); //código del cliente
                            //oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                            //oBe.adpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                            //oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                            //oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", x.pgoc_vnumero_planilla, x.pgoc_descripcion.ToUpper());
                            //oBe.adpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                            //oBe.adpac_iorigen = "P"; //Adelanto
                            //oBe.adpac_iusuario_crea = x.intUsuario;
                            //oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            //oBe.adpac_iusuario_modifica = x.intUsuario;
                            //oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                            //oBe.adpac_isituacion = 108;
                            //oBe.adpac_flag_estado = true;
                            //oBe.SaldoDXCAdelanto = Convert.ToDecimal(x.pgoc_nmonto);
                            //oBe.doxcc_nmonto_pagado = 0;
                            //x.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                            //new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(x.pgoc_icod_anticipo), 0);

                            ////EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            //EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            //x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //
                        }

                        //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                        objVentasData.insertarPago(x);
                    });
                    #endregion
                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEBoleta.dxcc_iid_doc_por_cobrar, 0);
                    #endregion
                    #region Planilla Cab
                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }
                    //INSERTAR EL DET. DE LA PLANILLA
                    #endregion
                    #region Planilla Det
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    oBePlnDet.tablc_iid_tipo_mov = Parametros.intPlnFacturacion;
                    oBePlnDet.plnd_icod_documento = objEBoleta.bovc_icod_boleta;
                    //INGRESAR EL REGISTRO DETALLE DE LA PLANILLA (Ej. Facturacion, pago o anticipo - En este caso es Facturacion)
                    oBePlnDet.plnd_icod_detalle = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);
                    lstContrato.Where(x => x.flag_multiple == true).ToList().ForEach(x =>
                    {
                        EPagosCuotas objPg = new EPagosCuotas();
                        objPg.cntc_icod_contrato_cuotas = x.cntc_icod_contrato_cuotas;
                        objPg.pgc_nmonto_pago = x.monto_pagar;
                        objPg.pgc_sfecha_pago = Convert.ToDateTime(objEBoleta.fechaEmision);
                        objPg.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                        objPg.cntc_icod_documento = Convert.ToInt32(oBePlnDet.plnd_icod_documento);
                        objPg.pgc_iusuario_crea = objEBoleta.intUsuario;
                        objPg.pgc_vpc_crea = WindowsIdentity.GetCurrent().Name;
                        objPg.pgc_nmonto_pago_mora = x.cntc_nmonto_mora_pago;
                        objPg.pgc_icod_pago = new BVentas().Insertar_Pagos(objPg);
                        //ACTUALIZAR PAGOS CUOTAS
                        new BVentas().Actualizar_Pagos(objPg.cntc_icod_contrato_cuotas, objPg.pgc_icod_pago);
                    });

                    #endregion
                    tx.Complete();
                    return oBePlnDet.plnd_icod_detalle;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarBoletaDesdePlanilla(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet, List<EBoletaDet> lstBolDelete,
              EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos, List<EPagoDocVenta> lstDeletePagos, List<EContratoCuotas> lstContrato)
        {
            try
            {
                //ING. EDGAR ALFARO
                //FECHA: 08/01/2014
                TesoreriaData objTesoreriaData = new TesoreriaData();
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //
                    #region Boleta
                    //MODIFICAR LA CAB. DE LA FACTURA
                    objVentasData.modificarBoletaPlanilla(objEBoleta);

                    //lstContrato.ForEach(x =>
                    //{
                    //    if (x.flag_multiple != x.flag_multiple_anterior)
                    //    {
                    //        x.tdocc_icod_tipo_doc = 0;
                    //        x.cntc_icod_documento = 0;
                    //        x.cntc_icod_situacion = 338;
                    //        new BVentas().modificarContratoCuotasSituacion(x);
                    //        new BVentas().modificarContratoCuotasDocumentos(x);
                    //    }

                    //});

                    //lstContrato.Where(x => x.flag_multiple == true).ToList().ForEach(x =>
                    //{
                    //    x.tdocc_icod_tipo_doc = Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc);
                    //    x.cntc_icod_documento = objEBoleta.bovc_icod_boleta;
                    //    x.cntc_icod_situacion = 340;
                    //    new BVentas().modificarContratoCuotasSituacion(x);
                    //    new BVentas().modificarContratoCuotasDocumentos(x);

                    //});

                    //
                    #region Boleta Det.
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    lstBolDet.ForEach(x =>
                    {
                        if (x.intClasificacionProducto != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            if (x.intTipoOperacion == 1)
                            {

                                //SE INSERTA EL ITEM DE LA FACTURA
                                x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                                objVentasData.insertarBoletaDetallePlanilla(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                //SI LA FACTURA <<NO>> REFERENCIA A UNA <<OT>> ENTONCES ....

                                //SE MODIFICA EL ITEM DEL DETALLE DE LA FACTURA
                                objVentasData.modificarBoletaDetallePlanilla(x);

                            }
                        }
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = x.strDesProducto;
                        }

                    });
                    #endregion
                    #region Doc. Por Cobrar de la Boleta
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = 9;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.strDesCliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.docxc_icod_documento = objEBoleta.bovc_icod_boleta;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    #region Pagos de la boleta
                    //PRIMERO SE DEBE ELIMINAR, TODOS LOS PAGOS PARA REINGRESAR LOS PAGOS
                    var lstPagosAux = new BVentas().listarPago(Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc), Convert.ToInt32(oBePlnDet.plnd_icod_documento));
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA BOLETA)
                        objVentasData.eliminarPago(x);
                    });
                    lstPagos.ForEach(x =>
                    {
                        //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                        x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocBoletaVenta;
                        x.pgoc_icod_documento = objEBoleta.bovc_icod_boleta;
                        x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                        x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                        x.pgoc_vnumero_planilla = strNroPlanCab;
                        x.pgoc_icod_cliente = objEBoleta.bovc_icod_cliente;
                        //TOMAR EN CUENTA LOS TIPOS DE PAGO
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta Credito
                            //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            var lstTipoTarjeta = listarTipoTarjeta();
                            ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                            oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.bovc_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "PVD";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            oBeBcoMovCab.mobac_origen_regitro = null;

                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocBoletaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.bovc_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //
                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            oBePagoDXC1.pdxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.insertarDXCPago(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //////EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            ////EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            ////x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //
                            ////////////EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //////////////oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            ////////////oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_anticipo);
                            ////////////oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            ////////////oBePagoDXC1.pdxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            ////////////oBePagoDXC1.pdxcc_sfecha_cobro = objEBoleta.bovc_sfecha_boleta;
                            ////////////oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            ////////////oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            ////////////oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                            ////////////oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            ////////////oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            ////////////oBePagoDXC1.pdxcc_vorigen = "P";
                            ////////////oBePagoDXC1.intUsuario = x.intUsuario;
                            ////////////oBePagoDXC1.strPc = x.strPc;
                            ////////////oBePagoDXC1.pdxcc_flag_estado = true;
                            ////////////x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarDXCPago(oBePagoDXC1);
                            ////////////new TesoreriaData().ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_anticipo), 0);

                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(objEBoleta.dxcc_iid_doc_por_cobrar);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                            oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.insertarDXCPago(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(objEBoleta.dxcc_iid_doc_por_cobrar), 0);

                            //EAdelantoPago oBe = new EAdelantoPago();
                            //oBe.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar; //el documento a pagar
                            //oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(x.pgoc_icod_anticipo); //correlativo del adelanto
                            //oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta; //tipo documento(adelanto)
                            //oBe.tdocc_iid_correlativo = Convert.ToInt32(oBePagoDXC1.tdodc_iid_correlativo); //clase del documento
                            //oBe.adpac_vnumero_doc_adelanto = objEBoleta.bovc_vnumero_boleta; //ndoc del adelanto con que se va a pagar
                            //oBe.cliec_icod_cliente = Convert.ToInt32(x.pgoc_icod_cliente); //código del cliente
                            //oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                            //oBe.adpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                            //oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                            //oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", x.pgoc_vnumero_planilla, x.pgoc_descripcion.ToUpper());
                            //oBe.adpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                            //oBe.adpac_iorigen = "P"; //Adelanto
                            //oBe.adpac_iusuario_crea = x.intUsuario;
                            //oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            //oBe.adpac_iusuario_modifica = x.intUsuario;
                            //oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                            //oBe.adpac_isituacion = 108;
                            //oBe.adpac_flag_estado = true;
                            //oBe.SaldoDXCAdelanto = Convert.ToDecimal(x.pgoc_nmonto);
                            //oBe.doxcc_nmonto_pagado = 0;
                            //x.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                            //new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(x.pgoc_icod_anticipo), 0);
                        }

                        //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                        objVentasData.insertarPago(x);
                    });
                    #endregion
                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEBoleta.doxcc_icod_correlativo, 0);
                    #endregion
                    #region Planilla Det.
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    //MODIFICANDO LA PLANILLA DET.
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);
                    //objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);

                    #endregion
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().FacturaVentaElectronicaObtenerDoc(objEBoleta.doc_icod_documento, "03").ToList();
                    if (lstCab.Count > 0)
                    {


                        objEBoleta.IdCabecera = lstCab[0].IdCabecera;
                        objVentasData.modificarBoletaVentaElectronica(objEBoleta);
                        if (lstCab.Count > 0)
                        {
                            new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                            foreach (var ob in lstBolDet)
                            {
                                ob.IdCabezera = lstCab[0].IdCabecera;
                                insertarBoletaVentaElectronicaDetalle(ob);
                            }
                        }
                    }
                    else
                    {
                        int icod = objVentasData.insertarBoletaVentaElectronica(objEBoleta);
                        foreach (var ob in lstBolDet)
                        {
                            ob.IdCabezera = icod;
                            insertarBoletaVentaElectronicaDetalle(ob);
                        }
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarBoleta(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet)
        {
            //ING. EDGAR ALFARO
            //FECHA: 08/01/2014
            int intIcodE = 0;
            try
            {
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Descripcion
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _des in lstBolDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _des.strDesProducto;
                        }
                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA BOLETA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = 9; //Clase Mercaderia
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la boleta, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxcc_nporcentaje_ivap = objEBoleta.bovc_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = objEBoleta.bovc_nmonto_ivap;
                    objDXC.vendc_icod_vendedor = objEBoleta.vendc_icod_vendedor;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEBoleta.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    #endregion
                    #region Boleta Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS
                    //var lst = objAdminSistemaData.getCorrelativoTipoDocumento(Parametros.intTipoDocBoletaVenta);
                    objEBoleta.bovc_icod_boleta = objVentasData.insertarBoleta(objEBoleta);
                    objEBoleta.doc_icod_documento = objEBoleta.bovc_icod_boleta;
                    intIcodE = objVentasData.insertarBoletaVentaElectronica(objEBoleta);
                    //ACTUALIZAR EL CORRELATIVO DE LA BOLETA
                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(objEBoleta.bovc_icod_pvt, Convert.ToInt32(objEBoleta.bovc_vnumero_boleta.Substring(4, 8)), 1);

                    #endregion
                    #region Boleta Det. Insertar
                    if (objEBoleta.remic_icod_remision == 0)
                    {
                        lstBolDet.ForEach(x =>
                    {
                        //    #region Salida de Kardex
                        //    EKardex obKardex = new EKardex();
                        //    obKardex.kardc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                        //    obKardex.kardc_fecha_movimiento = objEBoleta.bovc_sfecha_boleta;
                        //    obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        //    obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        //    obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bovd_ncantidad);
                        //    obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                        //    obKardex.kardc_numero_doc = objEBoleta.bovc_vnumero_boleta;
                        //    obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        //    obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                        //    obKardex.kardc_beneficiario = objEBoleta.cliec_vnombre_cliente;
                        //    obKardex.kardc_observaciones = "";
                        //    obKardex.intUsuario = objEBoleta.intUsuario;
                        //    obKardex.strPc = objEBoleta.strPc;
                        //    x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                        //    #endregion
                        //    //verificar stock del producto
                        //    decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, Convert.ToInt32(x.almac_icod_almacen), x.prdc_icod_producto);
                        //    if (Stock_Producto < Convert.ToDecimal(x.bovd_ncantidad))
                        //    {
                        //        throw new Exception("Stock insuficiente para el producto: " + x.strDesProducto + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                        //    }
                        //    #region Actualizando Stock
                        //    EStock stck = new EStock();
                        //    stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                        //    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        //    stck.stocc_stock_producto = x.bovd_ncantidad;
                        //    stck.intTipoMovimiento = 0;
                        //    objAlmacenData.actualizarStock(stck);
                        //#endregion

                        x.bovc_icod_boleta = intIcodE;
                        insertarBoletaVentaElectronicaDetalle(x);
                        x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                        insertarBoletaDetalle(x);
                    });
                    }
                    else
                    {
                        lstBolDet.ForEach(x =>
                    {
                        x.kardc_icod_correlativo = null;
                        x.almac_icod_almacen = null;
                        x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                        insertarBoletaDetalle(x);
                    });
                    }

                    #endregion

                    if (objEBoleta.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEBoleta.remic_icod_remision),
                            9, //TIPO DE DOCUMENTO
                            objEBoleta.bovc_icod_boleta, //ICOD_DOCUEMTNO
                            219, //FACTURADO
                            objEBoleta.intUsuario,
                            objEBoleta.strPc);

                    }

                    tx.Complete();
                }
                return objEBoleta.bovc_icod_boleta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private EDocXCobrarPago getPagoDXC(EPagoDocVenta oBePago)
        {
            try
            {
                EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                oBePagoDXC.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                oBePagoDXC.doxcc_icod_correlativo = oBePago.pgoc_dxc_icod_doc;


                if (oBePago.pgoc_tipo_pago == 1)//EFECTIVO
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;
                }
                if (oBePago.pgoc_tipo_pago == 2)//TARJETA
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;
                }
                if (oBePago.pgoc_tipo_pago == 3)//NOTA DE CREDITO
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.strNroNotaCredito;
                }
                if (oBePago.pgoc_tipo_pago == 4)//CHEQUE
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoPgoCheque;
                oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;


                if (oBePago.pgoc_tipo_pago == 5)//TRANSFERENCIA BANCARIA
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoPgoTransfBancaria;
                oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;


                if (oBePago.pgoc_tipo_pago == 6)//CREDITO
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;
                if (oBePago.pgoc_tipo_pago == 7)//ANTICIPO
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.strNroAnticipo;
                }

                oBePagoDXC.pdxcc_sfecha_cobro = oBePago.pgoc_sfecha_pago;
                oBePagoDXC.tablc_iid_tipo_moneda = oBePago.pgoc_icod_tipo_moneda;
                oBePagoDXC.pdxcc_nmonto_cobro = oBePago.pgoc_nmonto;
                oBePagoDXC.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePago.pgoc_sfecha_pago);
                if (Convert.ToDecimal(oBePagoDXC.pdxcc_nmonto_tipo_cambio) == 0)
                {
                    throw new ArgumentException(String.Format("No existe registro de tipo de cambio para la fecha {0}", oBePago.pgoc_sfecha_pago));
                }
                oBePagoDXC.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0} - {1} ", oBePago.pgoc_vnumero_planilla, oBePago.pgoc_descripcion.ToUpper());
                oBePagoDXC.cliec_icod_cliente = oBePago.pgoc_icod_cliente;
                oBePagoDXC.pdxcc_vorigen = "P";
                oBePagoDXC.intUsuario = oBePago.intUsuario;
                oBePagoDXC.strPc = oBePago.strPc;
                oBePagoDXC.pdxcc_flag_estado = true;

                return oBePagoDXC;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarBoleta(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet, List<EBoletaDet> lstBolDelete)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    #region Descripcion
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _des in lstBolDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _des.strDesProducto;
                        }
                    }
                    #endregion

                    //MODIFICAR LA BOLETA
                    objVentasData.modificarBoleta(objEBoleta);

                    #region Factura Det.

                    lstBolDelete.ForEach(x =>
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);

                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.bovd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);
                        #endregion


                        objVentasData.eliminarBoletaDetalle(x);
                    });

                    if (objEBoleta.remic_icod_remision == 0)
                    {

                        lstBolDet.ForEach(x =>
                        {

                            if (x.intTipoOperacion == 1)
                            {
                                //#region Salida de Kardex
                                //EKardex obKardex = new EKardex();
                                //obKardex.kardc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                                //obKardex.kardc_fecha_movimiento = objEBoleta.bovc_sfecha_boleta;
                                //obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                //obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                //obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bovd_ncantidad);
                                //obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                                //obKardex.kardc_numero_doc = objEBoleta.bovc_vnumero_boleta;
                                //obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                //obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                                //obKardex.kardc_beneficiario = objEBoleta.cliec_vnombre_cliente;
                                //obKardex.kardc_observaciones = "";
                                //obKardex.intUsuario = objEBoleta.intUsuario;
                                //obKardex.strPc = objEBoleta.strPc;
                                //x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                //#endregion
                                //#region Actualizando Stock
                                //EStock stck = new EStock();
                                //stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                                //stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                //stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                //stck.stocc_stock_producto = x.bovd_ncantidad;
                                //stck.intTipoMovimiento = 0;
                                //objAlmacenData.actualizarStock(stck);
                                //#endregion
                                x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                                objVentasData.insertarBoletaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                //#region Eliminar Kardex Previo
                                //EKardex obKardexDel = new EKardex();
                                //obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                                //obKardexDel.intUsuario = objEBoleta.intUsuario;
                                //obKardexDel.strPc = objEBoleta.strPc;
                                //objAlmacenData.eliminarKardex(obKardexDel);
                                //#endregion
                                //#region Salida de Kardex
                                //EKardex obKardex = new EKardex();
                                //obKardex.kardc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                                //obKardex.kardc_fecha_movimiento = objEBoleta.bovc_sfecha_boleta;
                                //obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                //obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                //obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bovd_ncantidad);
                                //obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                                //obKardex.kardc_numero_doc = objEBoleta.bovc_vnumero_boleta;
                                //obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                //obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                                //obKardex.kardc_beneficiario = objEBoleta.cliec_vnombre_cliente;
                                //obKardex.kardc_observaciones = "";
                                //obKardex.intUsuario = objEBoleta.intUsuario;
                                //obKardex.strPc = objEBoleta.strPc;
                                //x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                //#endregion
                                //#region Actualizando Stock
                                //EStock stck = new EStock();
                                //stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                                //stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                //stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                //stck.stocc_stock_producto = x.bovd_ncantidad;
                                //stck.intTipoMovimiento = 0;
                                //objAlmacenData.actualizarStock(stck);
                                //#endregion

                                objVentasData.modificarBoletaDetalle(x);

                            }

                        });
                    }
                    else
                    {

                        lstBolDet.ForEach(x =>
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                x.almac_icod_almacen = null;
                                x.kardc_icod_correlativo = null;
                                x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                                objVentasData.insertarBoletaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                objVentasData.modificarBoletaDetalle(x);

                            }
                        });



                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA BOLETA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    ////objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocBoletaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la boleta, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    // objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.vendc_icod_vendedor = objEBoleta.vendc_icod_vendedor;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxcc_nporcentaje_ivap = objEBoleta.bovc_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = objEBoleta.bovc_nmonto_ivap;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEBoleta.bovc_icod_boleta;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == objEBoleta.doc_icod_documento).ToList();
                    if (lstCab.Count > 0)
                    {
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstBolDet)
                        {
                            ob.bovc_icod_boleta = lstCab[0].IdCabecera;
                            insertarBoletaVentaElectronicaDetalle(ob);
                        }
                    }
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaNumero(EBoletaCab objEBoleta)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    //MODIFICAR LA BOLETA
                    objVentasData.modificarBoleta(objEBoleta);

                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA BOLETA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocBoletaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la boleta, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    //objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxcc_nporcentaje_ivap = objEBoleta.bovc_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = objEBoleta.bovc_nmonto_ivap;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEBoleta.bovc_icod_boleta;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaVenta(EBoletaCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarBoleta(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int verificarDocVentaPlanilla(int intTipoDoc, int intIcodDoc)
        {
            int intFlag = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intFlag = objVentasData.verificarDocVentaPlanilla(intTipoDoc, intIcodDoc);
                    tx.Complete();
                }
                return intFlag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AnularBoletaVenta(EBoletaCab objEFactura)
        {
            #region Eliminar DXC
            EDocXCobrar objDXC = new EDocXCobrar();
            objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
            objDXC.intUsuario = objEFactura.intUsuario;
            objDXC.strPc = objEFactura.strPc;
            new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
            #endregion Eliminar DXC

            List<EBoletaDet> Mlist = new List<EBoletaDet>();
            Mlist = listarBoletaDetalle(objEFactura.bovc_icod_boleta);
            foreach (var x in Mlist)
            {

                #region Eliminar Kardex
                EKardex obKardexDel = new EKardex();
                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                obKardexDel.intUsuario = objEFactura.intUsuario;
                obKardexDel.strPc = objEFactura.strPc;
                objAlmacenData.eliminarKardex(obKardexDel);
                #endregion
                #region Actualizando Stock
                EStock stck = new EStock();
                stck.stocc_ianio = Parametros.intEjercicio;
                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                stck.stocc_stock_producto = x.bovd_ncantidad;
                stck.intTipoMovimiento = 0;
                objAlmacenData.actualizarStock(stck);
                #endregion

                eliminarBoletaDetalle(x);
            }

            if (objEFactura.remic_icod_remision != 0)
            {
                objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                    0, //TIPO DE DOCUMENTO
                    0, //ICOD_DOCUEMTNO
                    218, //generado
                    objEFactura.intUsuario,
                    objEFactura.strPc);

            }
            objVentasData.anularBoleta(objEFactura);
        }

        public void anularBoleta(EBoletaCab objEBoleta)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocBoletaVenta, objEBoleta.bovc_icod_boleta);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DE LA NOTA DE CREDITO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });
                    List<EBoletaDet> Mlist = new List<EBoletaDet>();
                    Mlist = listarBoletaDetalle(objEBoleta.bovc_icod_boleta);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.bovd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);

                        #endregion
                        objVentasData.eliminarBoletaDetalle(x);
                    }
                    objVentasData.anularBoleta(objEBoleta);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaVenta(EBoletaCab objEBoleta)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminar DXC
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion Eliminar DXC
                    List<EBoletaDet> Mlist = new List<EBoletaDet>();
                    Mlist = listarBoletaDetalle(objEBoleta.bovc_icod_boleta);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = Parametros.intEjercicio;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.bovd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);
                        #endregion

                        eliminarBoletaDetalle(x);
                    }

                    if (objEBoleta.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEBoleta.remic_icod_remision),
                            0, //TIPO DE DOCUMENTO
                            0, //ICOD_DOCUEMTNO
                            218, //FACTURADO
                            objEBoleta.intUsuario,
                            objEBoleta.strPc);

                    }

                    objVentasData.eliminarBoleta(objEBoleta);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoleta(EBoletaCab objEBoleta)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                #region Doc. Por Cobrar de la Factura
                //INSERTAR LA FACTURA EN DOC. POR COBRAR
                EDocXCobrar objDXC = new EDocXCobrar();
                objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;

                objDXC.intUsuario = objEBoleta.intUsuario;
                objDXC.strPc = objEBoleta.strPc;

                List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                new CuentasPorCobrarData().eliminarDxc(objDXC);
                #endregion

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EBoletaDet> listaFactura = listarBoletaDetallePlanilla(objEBoleta.bovc_icod_boleta);
                    foreach (var x in listaFactura)
                    {
                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = Parametros.intEjercicio;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.bovd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                        objVentasData.eliminarBoletaDetallePlanilla(x);
                    }

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocBoletaVenta, objEBoleta.bovc_icod_boleta);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DE LA NOTA DE CREDITO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.eliminarDXCPago(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });
                    objVentasData.eliminarBoletaPlanilla(objEBoleta);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EBoletaDet> listarBoletaDetalle(int intBoleta)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>();
            try
            {
                lista = objVentasData.listarBoletaDetalle(intBoleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EBoletaDet> listarBoletaDetalleNTC(int intBoleta)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>();
            try
            {
                lista = objVentasData.listarBoletaDetalleNTC(intBoleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarBoletaDetalle(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarBoletaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaDetalle(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarBoletaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaDetalle(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarBoletaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarAfecto(int bovc_icod_boleta, bool bfavc_bafecto_igv)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarAfecto(bovc_icod_boleta, bfavc_bafecto_igv);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Venta Directa
        public List<EVentaDirecta> listarVentaDirecta(int intEjericio)
        {
            List<EVentaDirecta> lista = new List<EVentaDirecta>();
            try
            {
                lista = objVentasData.listarVentaDirecta(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarVentaDirecta(EVentaDirecta obj, List<EVentaDirectaDet> lstDetalle)
        {
            int id_doc = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    id_doc = objVentasData.insertarVentaDirecta(obj);
                    lstDetalle.ForEach(x =>
                    {
                        x.dvdc_icod_doc_venta_directa = id_doc;
                        objVentasData.insertarVentaDirectaDetalle(x);
                    });
                    tx.Complete();
                }
                return id_doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirecta(EVentaDirecta obj, List<EVentaDirectaDet> lstDetalle, List<EVentaDirectaDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarVentaDirecta(obj);
                    //
                    lstDelete.ForEach(x =>
                    {
                        objVentasData.eliminarVentaDirectaDetalle(x);
                    });
                    //
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.dvdc_icod_doc_venta_directa = obj.dvdc_icod_doc_venta_directa;
                            objVentasData.insertarVentaDirectaDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                            objVentasData.modificarVentaDirectaDetalle(x);
                    });
                    //
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirecta(EVentaDirecta obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarVentaDirecta(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVentaDirectaDet> listarVentaDirectaDetalle(int intVentaDirecta)
        {
            List<EVentaDirectaDet> lista = new List<EVentaDirectaDet>();
            try
            {
                lista = objVentasData.listarVentaDirectaDetalle(intVentaDirecta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarVentaDirectaDetalle(EVentaDirectaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarVentaDirectaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirectaDetalle(EVentaDirectaDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarVentaDirectaDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirectaDetalle(EVentaDirectaDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarVentaDirectaDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Pagos de Documentos de Venta
        public List<EPagoDocVenta> listarPago(int intTipoDoc, int intIcodDoc)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                lista = objVentasData.listarPago(intTipoDoc, intIcodDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EPagoDocVenta> getDatosPago(int intPago)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                lista = objVentasData.getDatosPago(intPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarPago(EPagoDocVenta oBe)
        {
            int idPago = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    idPago = objVentasData.insertarPago(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idPago;

        }
        public void modificarPago(EPagoDocVenta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPago(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPago(EPagoDocVenta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPago(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Planilla de Cobranza
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaCab(int intEjercicio)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>();
            try
            {
                lista = objVentasData.listarPlanillaCobranzaCab(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void cerrarPlanilla(ELibroBancos oBe1, ELibroBancos oBe2, int intPlanilla)
        {

            int? intIcodMovSol = null;
            int? intIcodMovDol = null;

            decimal intSoles = 0;
            decimal intDolares = 0;
            DateTime? fecha = null;

            if (oBe1 != null)
            {
                intDolares = 0;
                intSoles = oBe1.nmonto_movimiento;
                fecha = oBe1.dfecha_movimiento;
            }
            else
            {
                intSoles = 0;
                intDolares = oBe2.nmonto_movimiento_dolares;
                fecha = oBe2.dfecha_movimiento;
            }


            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                /************************************************************************/
                if (oBe1 != null)
                    intIcodMovSol = objTesoreriaDara.InsertarMovimientoBancos(oBe1);
                /************************************************************************/
                if (oBe2 != null)
                    intIcodMovDol = objTesoreriaDara.InsertarMovimientoBancos(oBe2);
                /************************************************************************/
                objVentasData.actualizarIcodMovimientoPlanilla(intPlanilla, intIcodMovSol, intIcodMovDol, intSoles, intDolares, fecha);
                /************************************************************************/
                tx.Complete();
            }
        }

        public void revertirCierre(int intPlanilla)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                objVentasData.revertirCierre(intPlanilla);
                tx.Complete();
            }
        }

        public Tuple<decimal, decimal> getTotalEfectivoPlanilla(int intPlanilla)
        {
            try
            {
                decimal dcmlEfectivoSol = 0;
                decimal dcmlEfectivoDol = 0;
                dcmlEfectivoSol = objVentasData.getTotalEfectivoPlanilla(intPlanilla).Item1;
                dcmlEfectivoDol = objVentasData.getTotalEfectivoPlanilla(intPlanilla).Item2;
                return new Tuple<decimal, decimal>(dcmlEfectivoSol, dcmlEfectivoDol);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>();
            try
            {
                lista = objVentasData.listarPlanillaCobranzaDetalle(intPlanilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaImpresionDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>();
            try
            {
                lista = objVentasData.listarPlanillaCobranzaImpresionDetalle(intPlanilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarPlanillaCab(EPlanillaCobranzaCab oBePlnCab)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public void eliminarPlanillaCab(EPlanillaCobranzaCab obeplacab)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPlanillaCobranzaCab(obeplacab);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Pagos desde Planilla de Cobranza
        public int insertarPagoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EPagoDocVenta oBePgo,
            EDocXCobrar oBeDXC)
        {
            try
            {
                EFacturaCab objFactCab = new EFacturaCab();

                ContabilidadData objContabilidadData = new ContabilidadData();
                TesoreriaData objTesoreriaData = new TesoreriaData();
                CuentasPorCobrarData CuentaCobrarData = new CuentasPorCobrarData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        #region DXP Pago
                        //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBePgo.pgoc_icod_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {

                        #region Ingreso del pago Dxp Pago
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        #endregion
                        #region Insertar Pago Nota de Credito
                        //EL PAGO CON LA NOTA DE CREDITO
                        ENotaCreditoPago oBe = new ENotaCreditoPago();
                        oBe.doxcc_icod_correlativo_pago = oBePgo.pgoc_dxc_icod_doc; //el documento a pagar
                        oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(oBePgo.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                        oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                        oBe.ncpac_iorigen = "P"; //Nota de credito
                        oBe.ncpac_flag_estado = true;
                        oBe.ncpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.ncpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                        oBe.pdxcc_icod_correlativo = oBePgo.pgoc_dxc_icod_pago;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                        #endregion
                        #region Actualizacion de Estados
                        TesoreriaData objTesoreriaData1 = new TesoreriaData();
                        objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                        objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(oBePgo.pgoc_dxc_icod_doc), 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    {
                        #region Pago Cheque
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(oBePgo.bcod_icod_banco_cuenta);
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CHEQUE";
                        oBeBcoMovCab.vdescripcion_beneficiario = Convert.ToString(oBeDXC.cliec_vnombre_cliente);
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla));
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "CHQ";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        //oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    {

                        #region Tranferencia Bancaria
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(oBePgo.bcod_icod_banco_cuenta);
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "TRANSFERENCIA BANCARIA";
                        oBeBcoMovCab.vdescripcion_beneficiario = Convert.ToString(oBeDXC.cliec_vnombre_cliente);
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla));
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "TRANS";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        //oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Pago DxC

                        EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                        oBePagoDXC1.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBePagoDXC1.pdxcc_vnumero_doc = oBePgo.strNroAnticipo;
                        oBePagoDXC1.pdxcc_sfecha_cobro = oBeDXC.doxcc_sfecha_doc;
                        oBePagoDXC1.tablc_iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBePagoDXC1.pdxcc_nmonto_cobro = oBePgo.pgoc_nmonto;
                        oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnCab.plnc_sfecha_planilla);
                        oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBePagoDXC1.cliec_icod_cliente = oBePgo.pgoc_icod_cliente;
                        oBePagoDXC1.pdxcc_vorigen = "P";
                        oBePagoDXC1.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC1.strPc = oBePgo.strPc;
                        oBePagoDXC1.pdxcc_flag_estado = true;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        #endregion
                        #region Pago Adelanto Pago
                        EAdelantoPago oBe = new EAdelantoPago();
                        oBe.doxcc_icod_correlativo_pago = oBeDXC.doxcc_icod_correlativo; //el documento a pagar
                        oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(oBePgo.pgoc_icod_anticipo); //correlativo del adelanto
                        oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta; //tipo documento(adelanto)
                        oBe.cliec_icod_cliente = Convert.ToInt32(oBePgo.pgoc_icod_cliente); //código del cliente
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc)); //tipo de cambio de la fecha seleccionada
                        oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", oBePgo.pgoc_vnumero_planilla, oBePgo.pgoc_descripcion.ToUpper());
                        oBe.adpac_sfecha_pago = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc); //fecha del pago
                        oBe.adpac_iorigen = "P"; //Adelanto
                        oBe.adpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_flag_estado = true;
                        oBe.SaldoDXCAdelanto = Convert.ToDecimal(oBePgo.pgoc_nmonto);
                        oBe.doxcc_nmonto_pagado = 0;
                        oBePgo.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                        new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(oBePgo.pgoc_icod_anticipo), 0);
                        #endregion

                    }

                    #region Planilla Cab
                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }
                    //INSERTAR EL DET. DE LA PLANILLA
                    #endregion

                    oBePlnDet.plnd_nmonto_pagado = oBePgo.pgoc_nmonto;
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.pgoc_icod_pago = objVentasData.insertarPago(oBePgo);
                    oBePlnDet.plnd_icod_detalle = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);

                    tx.Complete();
                }
                return oBePlnDet.plnd_icod_detalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarPagoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EPagoDocVenta oBePgo, EDocXCobrar oBeDXC)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //SE OBTIENEN TODOS LOS DATOS DEL DXC, PARA POSTERIOR USO (AL REINGRESAR)
                    oBeDXC = objCuentaCobrarData.getDXCDatos(oBePgo.pgoc_dxc_icod_doc);
                    //
                    #region Eliminar el pago, para su reingreso
                    //OBTERNER TODOS LOS DATOS DEL PAGO
                    var x = objVentasData.getDatosPago(oBePgo.pgoc_icod_pago)[0];
                    //
                    if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        #region DXP Pago
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta de Credito
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {
                        #region Nota de Credito
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePlnCab.intUsuario;
                        oBePagoDXC.strPc = oBePlnCab.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);

                        ENotaCreditoPago _beNCP = new ENotaCreditoPago();
                        _beNCP.ncpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        _beNCP.ncpac_vpc_modifica = oBePlnCab.strPc;
                        _beNCP.ncpac_iusuario_modifica = oBePlnCab.intUsuario;
                        objCuentaCobrarData.eliminarNCPago(_beNCP);
                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Adelanto Cliente
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL PAGO DEL ANTICIPO
                        EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                        oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                        oBePagoDXCAnt.intUsuario = oBePgo.intUsuario;
                        oBePagoDXCAnt.strPc = oBePgo.strPc;
                        new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                        objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        #endregion

                    }
                    //FINALMENTE SE ELIMINA EL PAGO Y EL DET DEL PLANILLA
                    objVentasData.eliminarPago(oBePgo);
                    #endregion
                    #region Reingresar el pago
                    if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        #region DXP Pago
                        //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta de Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBePgo.pgoc_icod_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {

                        #region Ingreso del pago Dxp Pago
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        #endregion
                        #region Insertar Pago Nota de Credito
                        //EL PAGO CON LA NOTA DE CREDITO
                        ENotaCreditoPago oBe = new ENotaCreditoPago();
                        oBe.doxcc_icod_correlativo_pago = oBePgo.pgoc_dxc_icod_doc; //el documento a pagar
                        oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(oBePgo.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                        oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                        oBe.ncpac_iorigen = "P"; //Nota de credito
                        oBe.ncpac_flag_estado = true;
                        oBe.ncpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.ncpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                        oBe.pdxcc_icod_correlativo = oBePgo.pgoc_dxc_icod_pago;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                        #endregion
                        #region Actualizacion de Estados
                        TesoreriaData objTesoreriaData1 = new TesoreriaData();
                        objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                        objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(oBePgo.pgoc_dxc_icod_doc), 0);
                        #endregion

                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Pago DxC

                        EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                        oBePagoDXC1.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBePagoDXC1.pdxcc_vnumero_doc = oBePgo.strNroAnticipo;
                        oBePagoDXC1.pdxcc_sfecha_cobro = oBeDXC.doxcc_sfecha_doc;
                        oBePagoDXC1.tablc_iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBePagoDXC1.pdxcc_nmonto_cobro = oBePgo.pgoc_nmonto;
                        oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnCab.plnc_sfecha_planilla);
                        oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBePagoDXC1.cliec_icod_cliente = oBePgo.pgoc_icod_cliente;
                        oBePagoDXC1.pdxcc_vorigen = "P";
                        oBePagoDXC1.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC1.strPc = oBePgo.strPc;
                        oBePagoDXC1.pdxcc_flag_estado = true;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        #endregion
                        #region Pago Adelanto Pago
                        EAdelantoPago oBe = new EAdelantoPago();
                        oBe.doxcc_icod_correlativo_pago = oBeDXC.doxcc_icod_correlativo; //el documento a pagar
                        oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(oBePgo.pgoc_icod_anticipo); //correlativo del adelanto
                        oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta; //tipo documento(adelanto)
                        oBe.cliec_icod_cliente = Convert.ToInt32(oBePgo.pgoc_icod_cliente); //código del cliente
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc)); //tipo de cambio de la fecha seleccionada
                        oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", oBePgo.pgoc_vnumero_planilla, oBePgo.pgoc_descripcion.ToUpper());
                        oBe.adpac_sfecha_pago = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc); //fecha del pago
                        oBe.adpac_iorigen = "P"; //Adelanto
                        oBe.adpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_flag_estado = true;
                        oBe.SaldoDXCAdelanto = Convert.ToDecimal(oBePgo.pgoc_nmonto);
                        oBe.doxcc_nmonto_pagado = 0;
                        oBePgo.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                        new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(oBePgo.pgoc_icod_anticipo), 0);
                        #endregion
                    }
                    #endregion

                    oBePlnDet.pgoc_icod_pago = objVentasData.insertarPago(oBePgo);
                    oBePlnDet.pgoc_icod_pago = oBePlnDet.pgoc_icod_pago;
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarPagoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EPagoDocVenta oBePgo)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //OBTERNER TODOS LOS DATOS DEL PAGO
                    var x = objVentasData.getDatosPago(oBePgo.pgoc_icod_pago)[0];
                    //
                    if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {
                        #region Nota de Credito
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePlnCab.intUsuario;
                        oBePagoDXC.strPc = oBePlnCab.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);

                        ENotaCreditoPago _beNCP = new ENotaCreditoPago();
                        _beNCP.ncpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        _beNCP.ncpac_vpc_modifica = oBePlnCab.strPc;
                        _beNCP.ncpac_iusuario_modifica = oBePlnCab.intUsuario;
                        objCuentaCobrarData.eliminarNCPago(_beNCP);
                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        #endregion

                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));

                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Adelanto Cliente
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL PAGO DEL ANTICIPO
                        EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                        oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                        oBePagoDXCAnt.intUsuario = oBePgo.intUsuario;
                        oBePagoDXCAnt.strPc = oBePgo.strPc;
                        new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                        objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        #endregion
                    }

                    //FINALMENTE SE ELIMINA EL PAGO Y EL DET DEL PLANILLA
                    objVentasData.eliminarPago(oBePgo);
                    //
                    objVentasData.eliminarPlanillaCobranzaDetalle(oBePlnDet);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Anticipos desde Planilla de Cobranza
        public int insertarAnticipoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EAnticipo oBeAntc)
        {
            int id_pln_det = 0;
            ContabilidadData objContabilidadData = new ContabilidadData();
            TesoreriaData objTesoreriaData = new TesoreriaData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region 1 Insertando Cabecera de Planilla

                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }

                    #endregion
                    #region 2 Insertando el Adelanto
                    //En esta parte se inserta el adelanto del cliente

                    EAdelantoCliente oBeAdelanto = new EAdelantoCliente();
                    oBeAdelanto.icod_correlativo = 0;//Este es el id del adelanto, pero como recién se esta insertando no tiene.
                    oBeAdelanto.icod_correlativo_cabecera = 0;//Cuando es desde bancos aqui va el correlativo del mov. de bancos.
                    oBeAdelanto.icod_cliente = oBeAntc.antc_icod_cliente;
                    oBeAdelanto.iid_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    oBeAdelanto.sfecha_adelanto = oBeAntc.antc_sfecha_anticipo;
                    oBeAdelanto.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                    /**/
                    oBeAdelanto.nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBeAntc.antc_sfecha_anticipo);
                    if (oBeAdelanto.nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    /**/

                    oBeAdelanto.nmonto_adelanto = oBeAntc.antc_nmonto_anticipo;
                    oBeAdelanto.nmonto_pagado = 0;
                    oBeAdelanto.vobservacion = oBeAntc.antc_observaciones;
                    oBeAdelanto.nsituacion_adelanto_cliente = Parametros.intSitClienteGenerado;
                    oBeAdelanto.iusuario_crea = oBeAntc.intUsuario;
                    oBeAdelanto.vpc_crea = oBeAntc.strPc;
                    oBeAdelanto.flag_estado = true;
                    var lstAdeCliente = new BCuentasPorCobrar().ListarAdelantoClietneXAñoTodos(Parametros.intEjercicio);
                    if (lstAdeCliente.Count == 0)
                        oBeAdelanto.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + "000001";
                    else
                        oBeAdelanto.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + string.Format("{0:000000}", (Convert.ToInt32(lstAdeCliente.Max(max => max.vnumero_adelanto).Remove(0, 3)) + 1));
                    oBeAdelanto.icod_correlativo = new TesoreriaData().insertarAdelantoCliente(oBeAdelanto);
                    //
                    #endregion
                    #region 3 Insertando el DXC
                    //En esta parte se inserta el dxc del adelanto
                    EDocXCobrar obj_DXC = new EDocXCobrar();
                    obj_DXC.doxcc_icod_correlativo = 0;//el id del dxc, pero aún no existe
                    obj_DXC.doxcc_vnumero_doc = oBeAdelanto.vnumero_adelanto;
                    obj_DXC.anio = Parametros.intEjercicio;
                    obj_DXC.mesec_iid_mes = Convert.ToInt16(oBeAdelanto.sfecha_adelanto.Month);
                    obj_DXC.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    obj_DXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                    obj_DXC.doxcc_sfecha_doc = oBeAdelanto.sfecha_adelanto;
                    obj_DXC.cliec_icod_cliente = oBeAdelanto.icod_cliente;
                    obj_DXC.cliec_vnombre_cliente = oBePlnDet.strCliente;
                    obj_DXC.tablc_iid_tipo_moneda = oBeAdelanto.iid_tipo_moneda;
                    obj_DXC.tablc_iid_tipo_pago = 1;
                    obj_DXC.doxcc_nmonto_tipo_cambio = oBeAdelanto.nmonto_tipo_cambio;
                    obj_DXC.doxcc_vdescrip_transaccion = oBeAdelanto.vobservacion;
                    obj_DXC.doxcc_nmonto_total = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_saldo = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_pagado = 0;
                    obj_DXC.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                    obj_DXC.doxcc_vobservaciones = oBeAdelanto.vobservacion;
                    obj_DXC.doxc_bind_cuenta_corriente = false;
                    obj_DXC.doxcc_bind_impresion_nogerencia = false;
                    obj_DXC.doxc_bind_situacion_legal = false;
                    obj_DXC.doxc_bind_cierre_cuenta_corriente = false;
                    obj_DXC.intUsuario = oBeAntc.intUsuario;
                    obj_DXC.strPc = oBeAntc.strPc;
                    //obj_DXC.docxc_icod_documento = oBeAdelanto.icod_correlativo;
                    obj_DXC.doxcc_flag_estado = true;
                    obj_DXC.doxcc_origen = "P";

                    obj_DXC.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDocumentoXCobrar(obj_DXC);
                    //
                    #endregion
                    #region 4 Ingreso a Bancos si el anticipo se realizó con Tarjeta de Crédito
                    if (oBeAntc.tablc_iid_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        //EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        //x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBeAntc.tablc_iid_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBeAntc.antc_sfecha_anticipo.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "ANTICIPO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBeAntc.antc_sfecha_anticipo);
                        oBeBcoMovCab.nmonto_movimiento = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBeAntc.antc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBeAntc.antc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                        oBeBcoMovDet.doxcc_sfecha_doc = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovDet.vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBeAntc.antc_nmonto_anticipo / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("ANTICIPO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = obj_DXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        //oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        #endregion
                    }
                    #endregion
                    #region 5 Insertando el Anticipo
                    //En esta parte se inserta el anticipo
                    oBeAntc.antc_icod_adelanto_cliente = oBeAdelanto.icod_correlativo;
                    oBeAntc.antc_icod_dxc_adelanto = obj_DXC.doxcc_icod_correlativo;
                    oBePlnDet.antc_icod_anticipo = objVentasData.insertarAnticipo(oBeAntc);                    //

                    #endregion
                    #region 6 Insertando Planilla Detalle
                    //En esta parte se insertar el detalle de la planilla y modificamos la cabecera de la pln, si no es el primer registro
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.plnd_icod_documento = oBeAdelanto.icod_correlativo;
                    oBePlnDet.plnd_vnumero_doc = oBeAdelanto.vnumero_adelanto;
                    id_pln_det = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);
                    #endregion

                    tx.Complete();
                }
                return id_pln_det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarAnticipoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EAnticipo oBeAntc)
        {
            ContabilidadData objContabilidadData = new ContabilidadData();
            TesoreriaData objTesoreriaData = new TesoreriaData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region 1 Modificando el Adelanto
                    //En esta parte se modifica el adelanto del cliente                
                    EAdelantoCliente oBeAdelanto = new EAdelantoCliente();
                    var lstADC = new TesoreriaData().getAdelantoClienteCab(oBeAntc.antc_icod_adelanto_cliente);
                    if (lstADC.Count == 0)
                        throw new ArgumentException("Error de datos, no se encontró ADC...");
                    oBeAdelanto = lstADC[0];
                    oBeAdelanto.icod_correlativo = oBeAntc.antc_icod_adelanto_cliente;
                    oBeAdelanto.icod_cliente = oBeAntc.antc_icod_cliente;
                    oBeAdelanto.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                    oBeAdelanto.nmonto_adelanto = oBeAntc.antc_nmonto_anticipo;
                    oBeAdelanto.vobservacion = oBeAntc.antc_observaciones;
                    oBeAdelanto.iusuario_modifica = oBeAntc.intUsuario;
                    oBeAdelanto.vpc_modifica = oBeAntc.strPc;
                    new TesoreriaData().modificarAdelantoCliente(oBeAdelanto);
                    //
                    #endregion                    
                    #region 2 Modificando el DXC
                    //En esta parte se modifica el dxc del adelanto
                    EDocXCobrar obj_DXC = new EDocXCobrar();
                    obj_DXC.doxcc_icod_correlativo = oBeAntc.antc_icod_dxc_adelanto;
                    obj_DXC.doxcc_vnumero_doc = oBeAdelanto.vnumero_adelanto;
                    obj_DXC.anio = Parametros.intEjercicio;
                    obj_DXC.mesec_iid_mes = Convert.ToInt16(oBeAdelanto.sfecha_adelanto.Month);
                    obj_DXC.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    obj_DXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                    obj_DXC.doxcc_sfecha_doc = oBeAdelanto.sfecha_adelanto;
                    obj_DXC.cliec_icod_cliente = oBeAdelanto.icod_cliente;
                    obj_DXC.cliec_vnombre_cliente = oBePlnDet.strCliente;
                    obj_DXC.tablc_iid_tipo_moneda = oBeAdelanto.iid_tipo_moneda;
                    obj_DXC.tablc_iid_tipo_pago = 1;
                    obj_DXC.doxcc_nmonto_tipo_cambio = oBeAdelanto.nmonto_tipo_cambio;
                    obj_DXC.doxcc_vdescrip_transaccion = oBeAdelanto.vobservacion;
                    obj_DXC.doxcc_nmonto_total = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_saldo = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_pagado = 0;
                    obj_DXC.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                    obj_DXC.doxcc_vobservaciones = oBeAdelanto.vobservacion;
                    obj_DXC.doxc_bind_cuenta_corriente = false;
                    obj_DXC.doxcc_bind_impresion_nogerencia = false;
                    obj_DXC.doxc_bind_situacion_legal = false;
                    obj_DXC.doxc_bind_cierre_cuenta_corriente = false;
                    obj_DXC.intUsuario = oBeAntc.intUsuario;
                    obj_DXC.strPc = oBeAntc.strPc;
                    //obj_DXC.docxc_icod_documento = oBeAdelanto.icod_correlativo;
                    obj_DXC.doxcc_flag_estado = true;
                    obj_DXC.doxcc_origen = "P";

                    new CuentasPorCobrarData().modificarDocumentoXCobrar(obj_DXC);
                    //
                    #endregion
                    #region 3 Eliminar el depósito de bancos si existe
                    if (Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov) > 0)
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov));
                    #endregion
                    #region 4 Ingreso a Bancos si el anticipo se realizó con Tarjeta de Crédito
                    if (oBeAntc.tablc_iid_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        //EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        //x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBeAntc.tablc_iid_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBeAntc.antc_sfecha_anticipo.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "ANTICIPO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBeAntc.antc_sfecha_anticipo);
                        oBeBcoMovCab.nmonto_movimiento = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBeAntc.antc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBeAntc.antc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                        oBeBcoMovDet.doxcc_sfecha_doc = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovDet.vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBeAntc.antc_nmonto_anticipo / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("ANTICIPO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = obj_DXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        //oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        #endregion
                    }
                    #endregion
                    #region 5 Modificando el Anticipo
                    //En esta parte se modifica el anticipo
                    objVentasData.modificarAnticipo(oBeAntc);
                    //
                    #endregion
                    #region 6 Modificando Planilla Detalle
                    //En esta parte se modifica el detalle de la planilla y modificamos la cabecera de la pln, si no es el primer registro
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);
                    objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);
                    //                                      
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarAnticipoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EAnticipo oBeAntc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    TesoreriaData objTesoreriaData = new TesoreriaData();

                    //En esta parte se elimina el adelanto del cliente y dcx a la vez en el mismo procedimiento
                    EAdelantoCliente oBeAdelanto = new EAdelantoCliente();
                    oBeAdelanto.icod_correlativo = oBeAntc.antc_icod_adelanto_cliente;
                    oBeAdelanto.iusuario_modifica = oBeAntc.intUsuario;
                    oBeAdelanto.vpc_modifica = oBeAntc.strPc;
                    objTesoreriaData.eliminarAdelantoCliente(oBeAdelanto);
                    //

                    #region 3 Eliminar el depósito de bancos si existe
                    if (Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov) > 0)
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov));
                    #endregion

                    //En esta parte se inserta el anticipo
                    objVentasData.eliminarAnticipo(oBeAntc);
                    //

                    //En esta parte se insertar el detalle de la planilla e modificamos la cabecera de la pln, si no es el primer registro
                    objVentasData.eliminarPlanillaCobranzaDetalle(oBePlnDet);
                    objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);
                    //                                      

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Retención
        public List<ERetencion> listarRetencionCab(int intEjericio, int? intPeriodo)
        {
            List<ERetencion> lista = new List<ERetencion>();
            try
            {
                lista = objVentasData.listarRetencionCab(intEjericio, intPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRetencionCab(ERetencion oBe_Ret, List<ERetencionDet> lstDetalle)
        {
            int id_retencion = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe_Ret.retc_icod_comprobante_retencion = objVentasData.insertarRetencionCab(oBe_Ret);
                    lstDetalle.ForEach(x =>
                    {
                        #region Pago DXC
                        /*LA RETENCION SE DEBE GRABAR COMO PAGO DEL DOCUMENTO*/
                        EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                        obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.intIcodDXC); //IdDocumentoPorCobrar
                        obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocRetencion;
                        obj_DXC_Pago.pdxcc_vnumero_doc = oBe_Ret.retc_vnumero_comprob_reten;
                        obj_DXC_Pago.pdxcc_sfecha_cobro = oBe_Ret.retc_sfec_comprob_reten;
                        obj_DXC_Pago.tablc_iid_tipo_moneda = oBe_Ret.tablc_iid_moneda;
                        obj_DXC_Pago.pdxcc_nmonto_cobro = x.retd_nmto_retencion;
                        obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = oBe_Ret.retc_nmto_tipo_cambio;
                        obj_DXC_Pago.pdxcc_vobservacion = String.Format("RETENCIÓN N°:{0}", oBe_Ret.retc_vnumero_comprob_reten);
                        //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                        obj_DXC_Pago.cliec_icod_cliente = oBe_Ret.proc_icod_cliente;
                        //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                        //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                        //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                        //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                        obj_DXC_Pago.pdxcc_vorigen = "R";
                        obj_DXC_Pago.intUsuario = x.intUsuario;
                        obj_DXC_Pago.strPc = x.strPc;
                        obj_DXC_Pago.pdxcc_flag_estado = true;
                        x.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                        /***************************************************************************/
                        #endregion
                        x.retc_icod_comprobante_retencion = oBe_Ret.retc_icod_comprobante_retencion;
                        objVentasData.insertarRetencionDet(x);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                    });
                    tx.Complete();
                }
                return id_retencion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int modificarRetencionCab(ERetencion oBe_Ret, List<ERetencionDet> lstDetalle, List<ERetencionDet> lstDelete)
        {
            int id_retencion = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarRetencionCab(oBe_Ret);

                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            #region Pago DXC
                            /*LA RETENCION SE DEBE GRABAR COMO PAGO DEL DOCUMENTO*/
                            EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                            obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.intIcodDXC); //IdDocumentoPorCobrar
                            obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocRetencion;
                            obj_DXC_Pago.pdxcc_vnumero_doc = oBe_Ret.retc_vnumero_comprob_reten;
                            obj_DXC_Pago.pdxcc_sfecha_cobro = oBe_Ret.retc_sfec_comprob_reten;
                            obj_DXC_Pago.tablc_iid_tipo_moneda = oBe_Ret.tablc_iid_moneda;
                            obj_DXC_Pago.pdxcc_nmonto_cobro = x.retd_nmto_retencion;
                            obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = oBe_Ret.retc_nmto_tipo_cambio;
                            obj_DXC_Pago.pdxcc_vobservacion = String.Format("RETENCIÓN N°:{0}", oBe_Ret.retc_vnumero_comprob_reten);
                            //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                            obj_DXC_Pago.cliec_icod_cliente = oBe_Ret.proc_icod_cliente;
                            //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                            //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                            //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                            //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;

                            obj_DXC_Pago.pdxcc_vorigen = "R";
                            obj_DXC_Pago.intUsuario = x.intUsuario;
                            obj_DXC_Pago.strPc = x.strPc;
                            obj_DXC_Pago.pdxcc_flag_estado = true;
                            x.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                            /***************************************************************************/
                            #endregion
                            x.retc_icod_comprobante_retencion = oBe_Ret.retc_icod_comprobante_retencion;


                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);

                            objVentasData.insertarRetencionDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            /*ELIMINAR PAGO ANTERIOR*/
                            EDocXCobrarPago obj_DXC_PagoDel = new EDocXCobrarPago();
                            obj_DXC_PagoDel.pdxcc_icod_correlativo = x.pdxpc_icod_correlativo;
                            obj_DXC_PagoDel.intUsuario = x.intUsuario;
                            obj_DXC_PagoDel.strPc = x.strPc;
                            new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(obj_DXC_PagoDel);
                            #region Pago DXC
                            /*LA RETENCION SE DEBE GRABAR COMO PAGO DEL DOCUMENTO*/
                            EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                            obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.intIcodDXC); //IdDocumentoPorCobrar
                            obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocRetencion;
                            obj_DXC_Pago.pdxcc_vnumero_doc = oBe_Ret.retc_vnumero_comprob_reten;
                            obj_DXC_Pago.pdxcc_sfecha_cobro = oBe_Ret.retc_sfec_comprob_reten;
                            obj_DXC_Pago.tablc_iid_tipo_moneda = oBe_Ret.tablc_iid_moneda;
                            obj_DXC_Pago.pdxcc_nmonto_cobro = x.retd_nmto_retencion;
                            obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = oBe_Ret.retc_nmto_tipo_cambio;
                            obj_DXC_Pago.pdxcc_vobservacion = String.Format("RETENCIÓN N°:{0}", oBe_Ret.retc_vnumero_comprob_reten);
                            //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                            obj_DXC_Pago.cliec_icod_cliente = oBe_Ret.proc_icod_cliente;
                            //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                            //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                            //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                            //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                            obj_DXC_Pago.pdxcc_vorigen = "R";
                            obj_DXC_Pago.intUsuario = x.intUsuario;
                            obj_DXC_Pago.strPc = x.strPc;
                            obj_DXC_Pago.pdxcc_flag_estado = true;
                            x.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                            /***************************************************************************/
                            #endregion
                            objVentasData.modificarRetencionDet(x);
                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                        }
                    });

                    lstDelete.ForEach(x =>
                    {
                        EDocXCobrarPago obj_DXC_PagoDel = new EDocXCobrarPago();
                        obj_DXC_PagoDel.doxcc_icod_correlativo = x.intIcodDXC;
                        obj_DXC_PagoDel.pdxcc_icod_correlativo = x.pdxpc_icod_correlativo;
                        obj_DXC_PagoDel.intUsuario = x.intUsuario;
                        obj_DXC_PagoDel.strPc = x.strPc;
                        new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(obj_DXC_PagoDel);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_PagoDel.doxcc_icod_correlativo, obj_DXC_PagoDel.tablc_iid_tipo_moneda);
                        objVentasData.eliminarRetencionDet(x);
                    });
                    tx.Complete();
                }
                return id_retencion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRetencionCab(ERetencion oBe_Ret)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lstDelete = listarRetencionDet(oBe_Ret.retc_icod_comprobante_retencion);
                    lstDelete.ForEach(x =>
                    {
                        EDocXCobrarPago obj_DXC_PagoDel = new EDocXCobrarPago();
                        obj_DXC_PagoDel.doxcc_icod_correlativo = x.intIcodDXC;
                        obj_DXC_PagoDel.pdxcc_icod_correlativo = x.pdxpc_icod_correlativo;
                        obj_DXC_PagoDel.intUsuario = x.intUsuario;
                        obj_DXC_PagoDel.strPc = x.strPc;
                        new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(obj_DXC_PagoDel);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_PagoDel.doxcc_icod_correlativo, obj_DXC_PagoDel.tablc_iid_tipo_moneda);
                        objVentasData.eliminarRetencionDet(x);
                    });
                    objVentasData.eliminarRetencionCab(oBe_Ret);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ERetencionDet> listarRetencionDet(int intRetencion)
        {
            List<ERetencionDet> lista = new List<ERetencionDet>();
            try
            {
                lista = objVentasData.listarRetencionDet(intRetencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Nota Crédito Clientes
        public List<ENotaCredito> listarNotaCreditoClienteCab(int intEjericio)
        {
            List<ENotaCredito> lista = new List<ENotaCredito>();
            try
            {
                lista = objVentasData.listarNotaCreditoClienteCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarNotaCreditoNoComercialClienteCab(ENotaCredito oBe, List<ENotaCreditoDet> lstDetalle)
        {
            AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe.ncrec_icod_credito = objVentasData.insertarNotaCreditoClienteCab(oBe);
                    oBe.doc_icod_documento = oBe.ncrec_icod_credito;
                    intIcodE = objVentasData.insertarNotaCreditoVentaNoComercialElectronica(oBe);
                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(1, Convert.ToInt32(oBe.ncrec_vnumero_credito.Substring(4, 8)), 3);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    //objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    if (oBe.ncrec_tipo_nota_credito == 1)
                    {
                        objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    }
                    else
                    {
                        objDXC.tdodc_iid_correlativo = oBe.tdodc_iid_correlativo;
                    }

                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_ivap = oBe.ncrec_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = oBe.ncrec_nmonto_ivap;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    if (oBe.ncrec_npor_imp_ivap > 0)
                    {
                        objDXC.doxcc_nmonto_impuesto = 0;
                    }
                    else
                    {
                        objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    }
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.vendc_icod_vendedor = Convert.ToInt32(oBe.vendc_icod_vendedor);
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    //objDXC.docxc_icod_documento = oBe.ncrec_icod_credito;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    objDXC.doxcc_icod_pvt = 1;
                    objDXC.doxcc_tipo_comprobante_referencia = Convert.ToInt32(oBe.tdocc_icod_tipo_doc);
                    objDXC.doxcc_num_serie_referencia = oBe.ncrec_vnumero_documento.Substring(0, 4);
                    objDXC.doxcc_num_comprobante_referencia = oBe.ncrec_vnumero_documento.Substring(4);
                    objDXC.doxcc_sfecha_emision_referencia = oBe.ncrec_sfecha_documento;


                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    oBe.ncrec_icod_dxc = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    #endregion
                    objVentasData.modificarNotaCreditoClienteCab(oBe);//se modifica el registro, solo para poder contar con el icod del dxc
                    #region Detalle de la NC...

                    lstDetalle.ForEach(x =>
                    {
                        if (oBe.ncrec_tipo_nota_credito == 1)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                            {
                                #region krd...
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = oBe.ncrec_sfecha_credito.Year;
                                obKardex.kardc_fecha_movimiento = oBe.ncrec_sfecha_credito;
                                obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dcrec_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                                obKardex.kardc_numero_doc = oBe.ncrec_vnumero_credito;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Stock...
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                x.ncrec_icod_credito = intIcodE;
                                insertarNotaCreditoVentaNoComercialElectronicaDetalle(x);
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaCreditoClienteDet(x);

                            }
                            else
                            {
                                //    x.almac_icod_almacen = null;
                                //    x.kardc_iid_correlativo = null;
                                x.IdCabezera = intIcodE;
                                insertarNotaCreditoVentaNoComercialElectronicaDetalle(x);
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaCreditoClienteDet(x);
                            }
                        }
                        else
                        {
                            x.almac_icod_almacen = null;
                            x.kardc_iid_correlativo = null;
                            x.prdc_icod_producto = null;
                            x.IdCabezera = intIcodE;
                            insertarNotaCreditoVentaNoComercialElectronicaDetalle(x);
                            x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                            objVentasData.insertarNotaCreditoClienteDet(x);
                        }

                    });

                    #endregion

                    //VOLVER A ESTADO GENERADO LAS CUOTAS DEL CONTRATO PARAGADAS POR DOCUMENTO DE REFERENCIA

                    objVentasData.EliminarPagosCuota(oBe.tdocc_icod_tipo_doc, oBe.ncrec_vnumero_documento);

                    tx.Complete();
                }
                return oBe.ncrec_icod_credito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarNotaCreditoClienteCab(ENotaCredito oBe, List<ENotaCreditoDet> lstDetalle)
        {
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                    oBe.ncrec_icod_credito = objVentasData.insertarNotaCreditoClienteCab(oBe);
                    oBe.doc_icod_documento = oBe.ncrec_icod_credito;
                    intIcodE = objVentasData.insertarNotaCreditoVentaElectronica(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    //objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    if (oBe.ncrec_tipo_nota_credito == 1)
                    {
                        objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    }
                    else
                    {
                        objDXC.tdodc_iid_correlativo = oBe.tdodc_iid_correlativo;
                    }

                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_ivap = oBe.ncrec_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = oBe.ncrec_nmonto_ivap;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    if (oBe.ncrec_npor_imp_ivap > 0)
                    {
                        objDXC.doxcc_nmonto_impuesto = 0;
                    }
                    else
                    {
                        objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    }
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.vendc_icod_vendedor = Convert.ToInt32(oBe.vendc_icod_vendedor);
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    //objDXC.docxc_icod_documento = oBe.ncrec_icod_credito;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    objDXC.doxcc_icod_pvt = 1;
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    oBe.ncrec_icod_dxc = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    #endregion
                    objVentasData.modificarNotaCreditoClienteCab(oBe);//se modifica el registro, solo para poder contar con el icod del dxc
                    #region Detalle de la NC...

                    lstDetalle.ForEach(x =>
                    {
                        if (oBe.ncrec_tipo_nota_credito == 1)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                            {
                                #region krd...
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = oBe.ncrec_sfecha_credito.Year;
                                obKardex.kardc_fecha_movimiento = oBe.ncrec_sfecha_credito;
                                obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dcrec_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                                obKardex.kardc_numero_doc = oBe.ncrec_vnumero_credito;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Stock...
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                x.ncrec_icod_credito = intIcodE;
                                insertarNotaCreditoVentaElectronicaDetalle(x);
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaCreditoClienteDet(x);

                            }
                            else
                            {
                                //    x.almac_icod_almacen = null;
                                //    x.kardc_iid_correlativo = null;
                                x.ncrec_icod_credito = intIcodE;
                                insertarNotaCreditoVentaElectronicaDetalle(x);
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaCreditoClienteDet(x);
                            }
                        }
                        else
                        {
                            x.almac_icod_almacen = null;
                            x.kardc_iid_correlativo = null;
                            x.prdc_icod_producto = null;
                            x.ncrec_icod_credito = intIcodE;
                            insertarNotaCreditoVentaElectronicaDetalle(x);
                            x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                            objVentasData.insertarNotaCreditoClienteDet(x);
                        }

                    });

                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(1, Convert.ToInt32(oBe.ncrec_vnumero_credito.Substring(4, 8)), 3);
                    #endregion

                    tx.Complete();
                }
                return oBe.ncrec_icod_credito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoClienteCab(ENotaCredito oBe, List<ENotaCreditoDet> lstDetalle, List<ENotaCreditoDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    //objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    if (oBe.ncrec_tipo_nota_credito == 1)
                    {
                        objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    }
                    else
                    {
                        objDXC.tdodc_iid_correlativo = oBe.tdodc_iid_correlativo;
                    }
                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_ivap = oBe.ncrec_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = oBe.ncrec_nmonto_ivap;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    if (oBe.ncrec_npor_imp_ivap > 0)
                    {
                        objDXC.doxcc_nmonto_impuesto = 0;
                    }
                    else
                    {
                        objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    }

                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.vendc_icod_vendedor = Convert.ToInt32(oBe.vendc_icod_vendedor);
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    objDXC.strPc = oBe.strPc;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    objDXC.doxcc_tipo_comprobante_referencia = Convert.ToInt32(oBe.tdocc_icod_tipo_doc);
                    objDXC.doxcc_num_serie_referencia = oBe.ncrec_vnumero_documento.Substring(0, 4);
                    objDXC.doxcc_num_comprobante_referencia = oBe.ncrec_vnumero_documento.Substring(4);
                    objDXC.doxcc_sfecha_emision_referencia = oBe.ncrec_sfecha_documento;
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    //se elimina los items
                    lstDelete.ForEach(x =>
                    {
                        if (oBe.ncrec_tipo_nota_credito == 1)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                            {
                                #region krd...
                                EKardex obKardexDel = new EKardex();
                                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                                obKardexDel.intUsuario = oBe.intUsuario;
                                obKardexDel.strPc = oBe.strPc;
                                objAlmacenData.eliminarKardex(obKardexDel);
                                #endregion
                                #region Stock...
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                            }
                        }
                        #region Nc det...
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaCreditoClienteDet(x);
                        #endregion
                    });
                    //se ingresa o modifica los items
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                if (oBe.ncrec_tipo_nota_credito == 1)
                                {
                                    #region Registra el ingreso al krd...
                                    EKardex obKardex = new EKardex();
                                    obKardex.kardc_ianio = oBe.ncrec_sfecha_credito.Year;
                                    obKardex.kardc_fecha_movimiento = oBe.ncrec_sfecha_credito;
                                    obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dcrec_ncantidad_producto);
                                    obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                                    obKardex.kardc_numero_doc = oBe.ncrec_vnumero_credito;
                                    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                                    obKardex.kardc_beneficiario = "";
                                    obKardex.kardc_observaciones = "";
                                    obKardex.intUsuario = oBe.intUsuario;
                                    obKardex.strPc = oBe.strPc;
                                    x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                                    #endregion
                                    #region Actualiza el stock...
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                                    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion
                                }
                                else
                                {
                                    x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                    objVentasData.insertarNotaCreditoClienteDet(x);
                                    //x.kardc_iid_correlativo = null;
                                    //x.almac_icod_almacen = null;
                                }


                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                if (oBe.ncrec_tipo_nota_credito == 1)
                                {
                                    #region elimina krd y stock...
                                    EKardex obKardexDel = new EKardex();
                                    obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                                    obKardexDel.intUsuario = oBe.intUsuario;
                                    obKardexDel.strPc = oBe.strPc;
                                    objAlmacenData.eliminarKardex(obKardexDel);
                                    #endregion
                                    #region Actualiza el stock...
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                                    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion

                                    #region Registra el ingreso modificado al krd...
                                    EKardex obKardex = new EKardex();
                                    obKardex.kardc_ianio = oBe.ncrec_sfecha_credito.Year;
                                    obKardex.kardc_fecha_movimiento = oBe.ncrec_sfecha_credito;
                                    obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dcrec_ncantidad_producto);
                                    obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                                    obKardex.kardc_numero_doc = oBe.ncrec_vnumero_credito;
                                    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                                    obKardex.kardc_beneficiario = "";
                                    obKardex.kardc_observaciones = "";
                                    obKardex.intUsuario = oBe.intUsuario;
                                    obKardex.strPc = oBe.strPc;
                                    x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                                    #endregion
                                    #region Actualiza el stock...
                                    EStock stckk = new EStock();
                                    stckk.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                                    stckk.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    stckk.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    objAlmacenData.actualizarStock(stckk);
                                    #endregion
                                }
                                objVentasData.modificarNotaCreditoClienteDet(x);

                            }

                        }
                        else
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                //x.almac_icod_almacen = null;
                                //x.kardc_iid_correlativo = null;
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaCreditoClienteDet(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                //x.almac_icod_almacen = null;
                                //x.kardc_iid_correlativo = null;
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.modificarNotaCreditoClienteDet(x);
                            }
                        }

                    });
                    EParametro Parametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
                    EFacturaVentaElectronica Cab = new BVentas().listarfacturaVentaElectronica(Parametro.pm_sfecha_inicio).Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "07").FirstOrDefault();

                    if (Cab != null)
                    {
                        oBe.IdCabecera = Cab.IdCabecera;
                        objVentasData.modificarNotaCreditoVentaElectronica(oBe);

                        new BVentas().eliminarFacturaVentaElectronicaDetalle(Cab.IdCabecera);
                        foreach (var ob in lstDetalle)
                        {
                            ob.IdCabezera = Cab.IdCabecera;
                            insertarNotaCreditoVentaElectronicaDetalle(ob);
                        }
                    }
                    else
                    {
                        int cab = objVentasData.insertarNotaCreditoVentaNoComercialElectronica(oBe);
                        foreach (var ob in lstDetalle)
                        {
                            ob.IdCabezera = cab;
                            insertarNotaCreditoVentaElectronicaDetalle(ob);
                        }
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoClienteCab(ENotaCredito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaCreditoClienteDet(oBe.ncrec_icod_credito);
                    lstDelete.ForEach(x =>
                    {

                        #region elimina krd y stock...
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Stock...
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        objAlmacenData.actualizarStock(stck);
                        #endregion

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaCreditoClienteDet(x);
                    });
                    //se ingresa o modifica los items

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void anularNotaCreditoClienteCab(ENotaCredito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.anularNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaCreditoClienteDet(oBe.ncrec_icod_credito);
                    lstDelete.ForEach(x =>
                    {
                        #region elimina krd y stock...
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Stock...
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaCreditoClienteDet(x);
                    });

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaCreditoDet> listarNotaCreditoClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>();
            try
            {
                lista = objVentasData.listarNotaCreditoClienteDet(intNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ENotaCreditoDet> listarNotaCreditoNoComercialClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>();
            try
            {
                lista = objVentasData.listarNotaCreditoNoComercialClienteDet(intNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Nota Debito Clientes
        public List<ENotaDebito> listarNotaDebitoClienteCab(int intEjericio)
        {
            List<ENotaDebito> lista = new List<ENotaDebito>();
            try
            {
                lista = objVentasData.listarNotaDebitoClienteCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarNotaDebitoClienteCab(ENotaDebito oBe, List<ENotaDebitoDet> lstDetalle)
        {
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();

                    oBe.ndebc_icod_debito = objVentasData.insertarNotaDebitoClienteCab(oBe);
                    oBe.doc_icod_documento = oBe.ndebc_icod_debito;
                    intIcodE = objVentasData.insertarNotaDebitoVentaElectronica(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    //objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ndebc_sfecha_debito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    if (oBe.ndebc_tipo_nota_debito == 1)
                    {
                        objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    }
                    else
                    {
                        objDXC.tdodc_iid_correlativo = oBe.tdodc_iid_correlativo;
                    }

                    objDXC.doxcc_vnumero_doc = oBe.ndebc_vnumero_debito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ndebc_sfecha_debito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ndebc_sfecha_debito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ndebc_sfecha_debito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ndebc_npor_imp_igv) > 0) ? oBe.ndebc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ndebc_npor_imp_igv) == 0) ? oBe.ndebc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_ivap = oBe.ndebc_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = oBe.ndebc_nmonto_ivap;
                    objDXC.doxcc_nporcentaje_igv = oBe.ndebc_npor_imp_igv;
                    if (oBe.ndebc_npor_imp_ivap > 0)
                    {
                        objDXC.doxcc_nmonto_impuesto = 0;
                    }
                    else
                    {
                        objDXC.doxcc_nmonto_impuesto = oBe.ndebc_nmonto_total - oBe.ndebc_nmonto_neto;
                    }
                    objDXC.doxcc_nmonto_total = oBe.ndebc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ndebc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.vendc_icod_vendedor = Convert.ToInt32(oBe.vendc_icod_vendedor);
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    //objDXC.docxc_icod_documento = oBe.ncrec_icod_credito;
                    objDXC.anio = oBe.ndebc_sfecha_debito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    objDXC.doxcc_icod_pvt = 1;
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    oBe.ndebc_icod_dxc = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    #endregion
                    objVentasData.modificarNotaDebitoClienteCab(oBe);//se modifica el registro, solo para poder contar con el icod del dxc
                    #region Detalle de la NC...

                    lstDetalle.ForEach(x =>
                    {
                        if (oBe.ndebc_tipo_nota_debito == 1)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                            {
                                #region krd...
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = oBe.ndebc_sfecha_debito.Year;
                                obKardex.kardc_fecha_movimiento = oBe.ndebc_sfecha_debito;
                                obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.ddebc_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                                obKardex.kardc_numero_doc = oBe.ndebc_vnumero_debito;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Stock...
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.ndebc_sfecha_debito.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                x.ndebc_icod_debito = intIcodE;
                                insertarNotaDebitoVentaElectronicaDetalle(x);
                                x.ndebc_icod_debito = oBe.ndebc_icod_debito;
                                objVentasData.insertarNotaDebitoClienteDet(x);

                            }
                            else
                            {
                                //    x.almac_icod_almacen = null;
                                //    x.kardc_iid_correlativo = null;
                                x.ndebc_icod_debito = intIcodE;
                                insertarNotaDebitoVentaElectronicaDetalle(x);
                                x.ndebc_icod_debito = oBe.ndebc_icod_debito;
                                objVentasData.insertarNotaDebitoClienteDet(x);
                            }
                        }
                        else
                        {
                            x.almac_icod_almacen = null;
                            x.kardc_iid_correlativo = null;
                            x.prdc_icod_producto = null;
                            x.ndebc_icod_debito = intIcodE;
                            insertarNotaDebitoVentaElectronicaDetalle(x);
                            x.ndebc_icod_debito = oBe.ndebc_icod_debito;
                            objVentasData.insertarNotaDebitoClienteDet(x);
                        }

                    });

                    #endregion
                    objAdminSistemaData.updateCorrelativoTipoDocumentoRP(1, Convert.ToInt32(oBe.ndebc_vnumero_debito.Substring(4, 8)), 4);

                    tx.Complete();
                }
                return oBe.ndebc_icod_debito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaDebitoClienteCab(ENotaDebito oBe, List<ENotaDebitoDet> lstDetalle, List<ENotaDebitoDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarNotaDebitoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ndebc_icod_dxc;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ndebc_sfecha_debito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    //objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    if (oBe.ndebc_tipo_nota_debito == 1)
                    {
                        objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    }
                    else
                    {
                        objDXC.tdodc_iid_correlativo = oBe.tdodc_iid_correlativo;
                    }
                    objDXC.doxcc_vnumero_doc = oBe.ndebc_vnumero_debito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ndebc_sfecha_debito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ndebc_sfecha_debito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ndebc_sfecha_debito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ndebc_npor_imp_igv) > 0) ? oBe.ndebc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ndebc_npor_imp_igv) == 0) ? oBe.ndebc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_ivap = oBe.ndebc_npor_imp_ivap;
                    objDXC.doxcc_nmonto_ivap = oBe.ndebc_nmonto_ivap;
                    objDXC.doxcc_nporcentaje_igv = oBe.ndebc_npor_imp_igv;
                    if (oBe.ndebc_npor_imp_ivap > 0)
                    {
                        objDXC.doxcc_nmonto_impuesto = 0;
                    }
                    else
                    {
                        objDXC.doxcc_nmonto_impuesto = oBe.ndebc_nmonto_total - oBe.ndebc_nmonto_neto;
                    }

                    objDXC.doxcc_nmonto_total = oBe.ndebc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ndebc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.vendc_icod_vendedor = Convert.ToInt32(oBe.vendc_icod_vendedor);
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    objDXC.strPc = oBe.strPc;
                    objDXC.anio = oBe.ndebc_sfecha_debito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    //se elimina los items
                    lstDelete.ForEach(x =>
                    {
                        if (oBe.ndebc_tipo_nota_debito == 1)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                            {
                                #region krd...
                                EKardex obKardexDel = new EKardex();
                                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                                obKardexDel.intUsuario = oBe.intUsuario;
                                obKardexDel.strPc = oBe.strPc;
                                objAlmacenData.eliminarKardex(obKardexDel);
                                #endregion
                                #region Stock...
                                EStock stck = new EStock();
                                stck.stocc_ianio = oBe.ndebc_sfecha_debito.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                            }
                        }
                        #region Nc det...
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaDebitoClienteDet(x);
                        #endregion
                    });
                    //se ingresa o modifica los items
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                if (oBe.ndebc_tipo_nota_debito == 1)
                                {
                                    #region Registra el ingreso al krd...
                                    EKardex obKardex = new EKardex();
                                    obKardex.kardc_ianio = oBe.ndebc_sfecha_debito.Year;
                                    obKardex.kardc_fecha_movimiento = oBe.ndebc_sfecha_debito;
                                    obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.ddebc_ncantidad_producto);
                                    obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                                    obKardex.kardc_numero_doc = oBe.ndebc_vnumero_debito;
                                    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                                    obKardex.kardc_beneficiario = "";
                                    obKardex.kardc_observaciones = "";
                                    obKardex.intUsuario = oBe.intUsuario;
                                    obKardex.strPc = oBe.strPc;
                                    x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                                    #endregion
                                    #region Actualiza el stock...
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = oBe.ndebc_sfecha_debito.Year;
                                    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion
                                }
                                else
                                {
                                    x.ndebc_icod_debito = oBe.ndebc_icod_debito;
                                    objVentasData.insertarNotaDebitoClienteDet(x);
                                    //x.kardc_iid_correlativo = null;
                                    //x.almac_icod_almacen = null;
                                }


                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                if (oBe.ndebc_tipo_nota_debito == 1)
                                {
                                    #region elimina krd y stock...
                                    EKardex obKardexDel = new EKardex();
                                    obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                                    obKardexDel.intUsuario = oBe.intUsuario;
                                    obKardexDel.strPc = oBe.strPc;
                                    objAlmacenData.eliminarKardex(obKardexDel);
                                    #endregion
                                    #region Actualiza el stock...
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = oBe.ndebc_sfecha_debito.Year;
                                    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion

                                    #region Registra el ingreso modificado al krd...
                                    EKardex obKardex = new EKardex();
                                    obKardex.kardc_ianio = oBe.ndebc_sfecha_debito.Year;
                                    obKardex.kardc_fecha_movimiento = oBe.ndebc_sfecha_debito;
                                    obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.ddebc_ncantidad_producto);
                                    obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                                    obKardex.kardc_numero_doc = oBe.ndebc_vnumero_debito;
                                    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                                    obKardex.kardc_beneficiario = "";
                                    obKardex.kardc_observaciones = "";
                                    obKardex.intUsuario = oBe.intUsuario;
                                    obKardex.strPc = oBe.strPc;
                                    x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                                    #endregion
                                    #region Actualiza el stock...
                                    EStock stckk = new EStock();
                                    stckk.stocc_ianio = oBe.ndebc_sfecha_debito.Year;
                                    stckk.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                    stckk.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    objAlmacenData.actualizarStock(stckk);
                                    #endregion
                                }
                                objVentasData.modificarNotaDebitoClienteDet(x);

                            }

                        }
                        else
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                //x.almac_icod_almacen = null;
                                //x.kardc_iid_correlativo = null;
                                x.ndebc_icod_debito = oBe.ndebc_icod_debito;
                                objVentasData.insertarNotaDebitoClienteDet(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                //x.almac_icod_almacen = null;
                                //x.kardc_iid_correlativo = null;
                                x.ndebc_icod_debito = oBe.ndebc_icod_debito;
                                objVentasData.modificarNotaDebitoClienteDet(x);
                            }
                        }

                    });
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == oBe.doc_icod_documento).ToList();
                    if (lstCab.Count > 0)
                    {
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstDelete)
                        {
                            ob.ndebc_icod_debito = lstCab[0].IdCabecera;
                            insertarNotaDebitoVentaElectronicaDetalle(ob);
                        }
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitoClienteCab(ENotaDebito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaDebitoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ndebc_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaDebitoClienteDet(oBe.ndebc_icod_debito);
                    lstDelete.ForEach(x =>
                    {

                        #region elimina krd y stock...
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Stock...
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ndebc_sfecha_debito.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        objAlmacenData.actualizarStock(stck);
                        #endregion

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaDebitoClienteDet(x);
                    });
                    //se ingresa o modifica los items

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void anularNotaDebitoClienteCab(ENotaDebito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.anularNotaDebitoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ndebc_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaDebitoClienteDet(oBe.ndebc_icod_debito);
                    lstDelete.ForEach(x =>
                    {
                        #region elimina krd y stock...
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Stock...
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ndebc_sfecha_debito.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaDebitoClienteDet(x);
                    });

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaDebitoDet> listarNotaDebitoClienteDet(int intNotaDebito)
        {
            List<ENotaDebitoDet> lista = new List<ENotaDebitoDet>();
            try
            {
                lista = objVentasData.listarNotaDebitoClienteDet(intNotaDebito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Tipo Tarjeta
        public List<ETipoTarjeta> listarTipoTarjeta()
        {
            List<ETipoTarjeta> lista = null;
            try
            {
                lista = objVentasData.listarTipoTarjeta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoTarjeta(ETipoTarjeta oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarTipoTarjeta(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoTarjeta(ETipoTarjeta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarTipoTarjeta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoTarjeta(ETipoTarjeta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarTipoTarjeta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Transportista
        public List<ETransportista> listarTransportista()
        {
            List<ETransportista> lista = null;
            try
            {
                lista = objVentasData.listarTransportista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTransportista(ETransportista oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarTransportista(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTransportista(ETransportista oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarTransportista(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTransportista(ETransportista oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarTransportista(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Guía Remisión
        public EGuiaRemision listarGuiaRemisionxID(int remic_icod_remision)
        {
            EGuiaRemision lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemisionxID(remic_icod_remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EGuiaRemision> listarGuiaRemision(int intEjercicio)
        {
            List<EGuiaRemision> lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemision(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EGuiaRemision> getGRCabVerificarNumero(string vnumero)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>();
            try
            {
                lista = objVentasData.getGRCabVerificarNumero(vnumero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDet)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarGuiaRemision(oBe);


                    #region Factura Det. Insertar
                    lstDet.ForEach(x =>
                    {

                        #region Salida de Kardex
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = Parametros.intEjercicio;
                        obKardex.kardc_fecha_movimiento = Convert.ToDateTime(oBe.remic_sfecha_inicio);
                        obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                        obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                        obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                        obKardex.kardc_beneficiario = "";
                        obKardex.kardc_observaciones = "";
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                        /*--------------------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        if (oBe.tablc_iid_motivo == 226)
                        {
                            #region Ingreso de Kardex
                            EKardex obKardexIngreso = new EKardex();
                            obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                            obKardexIngreso.kardc_fecha_movimiento = Convert.ToDateTime(oBe.remic_sfecha_inicio);
                            obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                            obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                            obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                            obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                            obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                            obKardexIngreso.kardc_beneficiario = "";
                            obKardexIngreso.kardc_observaciones = "";
                            obKardexIngreso.intUsuario = oBe.intUsuario;
                            obKardexIngreso.strPc = oBe.strPc;
                            x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                            /*-----------------------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            #endregion
                        }

                        x.remic_icod_remision = intIcod;
                        objVentasData.insertarGuiaRemisionDet(x);
                    });
                    #endregion

                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDet, List<EGuiaRemisionDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarGuiaRemision(oBe);
                    lstDelete.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*--------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        if (oBe.tablc_iid_motivo == 226)
                        {
                            #region Eliminar Kardex Ingreso
                            EKardex obKardexDelIngreso = new EKardex();
                            obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                            obKardexDelIngreso.intUsuario = oBe.intUsuario;
                            obKardexDelIngreso.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDelIngreso);
                            /*----------------------------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            #endregion
                        }

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionDet(x);
                    });

                    lstDet.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {

                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                            obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                            obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            /*------------------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stckS.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            //verificar stock del producto
                            //decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, Convert.ToInt32(oBe.almac_icod_almacen), x.prdc_icod_producto);
                            //if (Stock_Producto < Convert.ToDecimal(x.dremc_ncantidad_producto))
                            //{
                            //    throw new Exception("Stock insuficiente para el producto: " + x.strDesProducto + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                            //}
                            if (oBe.tablc_iid_motivo == 226)
                            {
                                #region Ingreso de Kardex
                                EKardex obKardexIngreso = new EKardex();
                                obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                obKardexIngreso.kardc_beneficiario = "";
                                obKardexIngreso.kardc_observaciones = "";
                                obKardexIngreso.intUsuario = oBe.intUsuario;
                                obKardexIngreso.strPc = oBe.strPc;
                                x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                                /*-------------------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = Parametros.intEjercicio;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                #endregion
                            }


                            x.remic_icod_remision = oBe.remic_icod_remision;
                            objVentasData.insertarGuiaRemisionDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                            #region Eliminar Kardex Previo
                            //EKardex obKardexDel = new EKardex();
                            //obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            //obKardexDel.intUsuario = oBe.intUsuario;
                            //obKardexDel.strPc = oBe.strPc;
                            //objAlmacenData.eliminarKardex(obKardexDel);
                            ///*---------------------------------------------------------------------*/

                            #endregion
                            if (oBe.tablc_iid_motivo == 226)
                            {
                                #region Eliminar Kardex Ingreso
                                //EKardex obKardexDelIngreso = new EKardex();
                                //obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                //obKardexDelIngreso.intUsuario = oBe.intUsuario;
                                //obKardexDelIngreso.strPc = oBe.strPc;
                                //objAlmacenData.eliminarKardex(obKardexDelIngreso);
                                /*---------------------------------------------------------------*/
                                EKardex obKardexIngreso = new EKardex();
                                obKardexIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                obKardexIngreso.kardc_beneficiario = "";
                                obKardexIngreso.kardc_observaciones = "";
                                obKardexIngreso.intUsuario = oBe.intUsuario;
                                obKardexIngreso.strPc = oBe.strPc;
                                objAlmacenData.modificarKardex(obKardexIngreso);
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = Parametros.intEjercicio;
                                stck.almac_icod_almacen = Convert.ToInt32(obKardexIngreso.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                #endregion
                            }
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                            obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                            obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            objAlmacenData.modificarKardex(obKardex);
                            /*----------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(obKardex.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stckS.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objVentasData.modificarGuiaRemisionDet(x);
                        }
                        else if (x.intTipoOperacion == 0 || x.intTipoOperacion == null)
                        {
                            //#region Eliminar Kardex Previo
                            //EKardex obKardexDel = new EKardex();
                            //obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            //obKardexDel.intUsuario = oBe.intUsuario;
                            //obKardexDel.strPc = oBe.strPc;
                            //objAlmacenData.eliminarKardex(obKardexDel);
                            ///*---------------------------------------------------------------------*/

                            //#endregion
                            if (oBe.tablc_iid_motivo == 226)
                            {
                                #region Eliminar Kardex Ingreso
                                //EKardex obKardexDelIngreso = new EKardex();
                                //obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                //obKardexDelIngreso.intUsuario = oBe.intUsuario;
                                //obKardexDelIngreso.strPc = oBe.strPc;
                                //objAlmacenData.eliminarKardex(obKardexDelIngreso);
                                /*---------------------------------------------------------------*/
                                EKardex obKardexIngreso = new EKardex();
                                obKardexIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                obKardexIngreso.kardc_beneficiario = "";
                                obKardexIngreso.kardc_observaciones = "";
                                obKardexIngreso.intUsuario = oBe.intUsuario;
                                obKardexIngreso.strPc = oBe.strPc;
                                objAlmacenData.modificarKardex(obKardexIngreso);
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = Parametros.intEjercicio;
                                stck.almac_icod_almacen = Convert.ToInt32(obKardexIngreso.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                #endregion
                            }
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                            obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                            obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            objAlmacenData.modificarKardex(obKardex);
                            /*----------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(obKardex.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stckS.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objVentasData.modificarGuiaRemisionDet(x);
                        }

                    });

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarGuiaRemision(oBe);

                    lstDetalle.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        if (oBe.tablc_iid_motivo == 226)
                        {
                            #region Eliminar Kardex Ingreso
                            EKardex obKardexDelIngreso = new EKardex();
                            obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                            obKardexDelIngreso.intUsuario = oBe.intUsuario;
                            obKardexDelIngreso.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDelIngreso);
                            /*--------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            #endregion
                        }

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionDet(x);
                    });

                    //lstDelete.ForEach(x =>
                    //{
                    //    x.intUsuario = oBe.intUsuario;
                    //    x.strPc = oBe.strPc;
                    //    objVentasData.eliminarGuiaRemisionDet(x);
                    //});

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    lstDetalle.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        if (oBe.tablc_iid_motivo == 226)
                        {
                            #region Eliminar Kardex Ingreso
                            EKardex obKardexDelIngreso = new EKardex();
                            obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                            obKardexDelIngreso.intUsuario = oBe.intUsuario;
                            obKardexDelIngreso.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDelIngreso);
                            /*--------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            #endregion
                        }

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionDet(x);
                    });
                    objVentasData.anularGuiaRemision(oBe);
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EGuiaRemisionDet> listarGuiaRemisionDet(int intIcod, int intEjericio)
        {
            List<EGuiaRemisionDet> lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemisionDet(intIcod, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        public void ActualizarDescripcionDXCFAC(List<EFacturaCab> Mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var oBe in Mlist)
                    {
                        int i = 1;
                        string vdescripcionProd = "";
                        List<EFacturaDet> lstDetalle = new List<EFacturaDet>();
                        lstDetalle = new BVentas().listarFacturaDetalle(oBe.favc_icod_factura);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.strDesProducto;
                                i++;
                            }
                            ////List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                            ////lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(_be.prd_icod_producto), Parametros.intEjercicio);
                            ////if (lstProdPrecioDet.Count() > 0)
                            ////{
                            ////    new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(_be.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                            ////}
                        }
                        new CuentasPorCobrarData().modificarDocumentoXCobrarDescripcion(oBe.doxcc_icod_correlativo, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public void ActualizarCuentasDXCFAC(List<EFacturaCab> Mlist)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var oBe in Mlist)
                    {
                        int i = 1;
                        string vdescripcionProd = "";
                        List<EFacturaDet> lstDetalle = new List<EFacturaDet>();
                        lstDetalle = new BVentas().listarFacturaDetalle(oBe.favc_icod_factura);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.strDesProducto;
                                i++;
                            }

                        }
                        new CuentasPorCobrarData().modificarDocumentoXCobrarDescripcion(oBe.doxcc_icod_correlativo, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public void ActualizarDescripcionDXCBov(List<EBoletaCab> Mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var oBe in Mlist)
                    {
                        int i = 1;
                        string vdescripcionProd = "";
                        List<EBoletaDet> lstDetalle = new List<EBoletaDet>();
                        lstDetalle = new BVentas().listarBoletaDetalle(oBe.bovc_icod_boleta);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.strDesProducto;
                                i++;
                            }
                            ////List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                            ////lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(_be.prd_icod_producto), Parametros.intEjercicio);
                            ////if (lstProdPrecioDet.Count() > 0)
                            ////{
                            ////    new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(_be.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                            ////}
                        }
                        //new CuentasPorCobrarData().modificarDocumentoXCobrarDescripcion(oBe.ka, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public List<EOrdenCompra> getOCLCabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                lista = objVentasData.getOCLCabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompraServicio> getOCSCabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                lista = objVentasData.getOCSCabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompraImportacion> getOCICabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                lista = objVentasData.getOCICabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #region Lista de Precio Venta
        public List<EPedidoClienCab> listarPedidoVenta()
        {
            List<EPedidoClienCab> lista = null;
            try
            {
                lista = (objVentasData).listarPedidoVenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarPedidoVenta(EPedidoClienCab oBe, List<EPedidoClienDet> lstDetalle)
        {
            int lpedi_icod_cliente = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lpedi_icod_cliente = objVentasData.insertarPedidoVenta(oBe);

                    #region Detalle
                    lstDetalle.ForEach(x =>
                    {
                        x.lpedi_icod_cliente = lpedi_icod_cliente;
                        objVentasData.insertarPedidoVentaDet(x);
                    });
                    #endregion
                    tx.Complete();
                }
                return lpedi_icod_cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPedidoVenta(EPedidoClienCab oBe, List<EPedidoClienDet> lstDetalle,
            List<EPedidoClienDet> lstDelete)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPedidoVenta(oBe);

                    #region Eliminar
                    lstDelete.ForEach(x =>
                    {
                        objVentasData.eliminarPedidoVentaDet(x);
                    });
                    #endregion
                    #region Detalle
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.lpedi_icod_cliente = oBe.lpedi_icod_cliente;
                            objVentasData.insertarPedidoVentaDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            objVentasData.modificarPedidoVentaDet(x);
                        }
                    });
                    #endregion

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPedidoVenta(EPedidoClienCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación
                    var lst = listarPedidoVentaDet(obj.lpedi_icod_cliente, Parametros.intEjercicio);
                    lst.ForEach(x =>
                    {
                        objVentasData.eliminarPedidoVentaDet(x);
                    });
                    #endregion
                    objVentasData.eliminarPedidoVenta(obj);


                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Lista de Pedido Compra Det
        public List<EPedidoClienDet> listarPedidoVentaDet(int lpedi_icod_proveedor, int Anio)
        {
            List<EPedidoClienDet> lista = null;
            try
            {
                lista = (objVentasData).listarPedidoVentaDet(lpedi_icod_proveedor, Anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void insertarPedidoVentaDet(EPedidoClienDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarPedidoVentaDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPedidoVentaDet(EPedidoClienDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPedidoVentaDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPedidoVentaDet(EPedidoClienDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPedidoVentaDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion



        #region Archivos
        public List<EArchivos> listarArchivos(int codigo)
        {
            List<EArchivos> lista = null;
            try
            {
                lista = objVentasData.listarArchivos(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarArchivos(EArchivos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarArchivos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarArchivos(EArchivos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarArchivos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarArchivos(EArchivos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarArchivos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Hoja Costos
        public List<EHojaCostos> listarHojaCostos()
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                lista = objVentasData.listarHojaCostos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostos(EHojaCostos oBe, List<EHojaCostosConceptos> lstHojaCostosConceptos, List<EhojaCostosSubConceptos> lstHojaCostosSubConcepstos, List<EHojaCostosRubros> lstHojaCostosRubros)
        {
            int intIcod = 0;
            int intIcodC = 0;
            int intIcodR = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Insertar
                    intIcod = objVentasData.insertarHojaCostos(oBe);

                    lstHojaCostosConceptos.ForEach(x =>
                    {
                        x.hjcc_icod_hoja_costo = intIcod;
                        intIcodC = insertarHojaCostosConceptos(x);
                    });
                    lstHojaCostosSubConcepstos.ForEach(xs =>
                    {
                        xs.hjcc_icod_hoja_costo = intIcod;
                        List<EHojaCostosConceptos> lstHojaCostosConceptos1 = new List<EHojaCostosConceptos>();
                        lstHojaCostosConceptos1 = listarHojaCostosConceptos(intIcod).Where(x => x.hjcd1_vcodigo_concepto_hc == xs.hjcc1_iiten).ToList();
                        lstHojaCostosConceptos1.ForEach(xc =>
                        {
                            xs.hjcd1_icod_concepto_hc = xc.hjcd1_icod_detalle_hc;
                        });
                        intIcodR = insertarHojaCostosSubConceptos(xs);
                    });
                    lstHojaCostosRubros.ForEach(xr =>
                    {
                        xr.hjcc_icod_hoja_costo = intIcod;
                        List<EhojaCostosSubConceptos> lstHojaCostosSubConceptos1 = new List<EhojaCostosSubConceptos>();
                        lstHojaCostosSubConceptos1 = listarHojaCostosSubConceptos(intIcod).Where(x => x.hjcd2_vcodigo_concepto_hc == xr.hjcd2_iitem).ToList();
                        lstHojaCostosSubConceptos1.ForEach(xsc =>
                        {
                            xr.hjcd2_icod_subconcepto_hc = xsc.hjcd2_icod_subconcepto_hc;
                        });
                        insertarHojaCostosRubros(xr);
                    });

                    #endregion

                    tx.Complete();

                }
                return intIcod;
                return intIcodC;
                return intIcodR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostos(EHojaCostos oBe, List<EHojaCostosConceptos> lstHojaCostosConceptos, List<EHojaCostosConceptos> lstDeleteHCConceptos, List<EhojaCostosSubConceptos> lstHojaCostosSubConcepstos,
            List<EhojaCostosSubConceptos> lstDeleteHojaCostosSubConcepstos, List<EHojaCostosRubros> lstHojaCostosRubros, List<EHojaCostosRubros> lstDeleteHojaCostosRubros)
        {

            int intIcod = 0;
            int intIcodC = 0;
            int intIcodR = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostos(oBe);
                    #region Eliminar Detalle
                    /*Elimiar Detalle*/
                    lstDeleteHCConceptos.ForEach(x =>
                    {
                        eliminarHojaCostosConceptos(x);

                    });
                    lstDeleteHojaCostosSubConcepstos.ForEach(x =>
                    {
                        eliminarHojaCostosSubConceptos(x);

                    });
                    lstDeleteHojaCostosRubros.ForEach(x =>
                    {
                        eliminarHojaCostosRubros(x);

                    });
                    #endregion
                    #region Insertar Detalle
                    /*Insertar Detalle*/
                    lstHojaCostosConceptos.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {

                            x.hjcc_icod_hoja_costo = oBe.hjcc_icod_hoja_costo;
                            intIcodC = insertarHojaCostosConceptos(x);

                        }
                        else
                        {
                            modificarHojaCostosConceptos(x);
                        }
                    });
                    lstHojaCostosSubConcepstos.ForEach(x =>
                    {
                        List<EHojaCostosConceptos> lstHojaCostosConcepstos2 = new List<EHojaCostosConceptos>();
                        lstHojaCostosConcepstos2 = listarHojaCostosConceptos(oBe.hjcc_icod_hoja_costo).Where(xp => x.hjcc1_iiten == xp.hjcd1_vcodigo_concepto_hc).ToList();
                        if (x.intTipoOperacion == 1)
                        {
                            x.hjcc_icod_hoja_costo = oBe.hjcc_icod_hoja_costo;
                            lstHojaCostosConcepstos2.ForEach(x2 =>
                            {
                                x.hjcd1_icod_concepto_hc = x2.hjcd1_icod_detalle_hc;
                            });
                            intIcodR = insertarHojaCostosSubConceptos(x);
                        }
                        else
                        {
                            modificarHojaCostosSubConceptos(x);
                        }
                    });
                    lstHojaCostosRubros.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            List<EhojaCostosSubConceptos> lstHojaSubConceptos2 = new List<EhojaCostosSubConceptos>();
                            lstHojaSubConceptos2 = listarHojaCostosSubConceptos(oBe.hjcc_icod_hoja_costo).Where(xsc => x.hjcd2_iitem == xsc.hjcd2_vcodigo_concepto_hc).ToList();
                            x.hjcc_icod_hoja_costo = oBe.hjcc_icod_hoja_costo;
                            lstHojaSubConceptos2.ForEach(xs =>
                            {
                                x.hjcd2_icod_subconcepto_hc = xs.hjcd2_icod_subconcepto_hc;
                            });
                            insertarHojaCostosRubros(x);
                        }
                        else
                        {
                            modificarHojaCostosRubros(x);
                        }
                    });
                    #endregion

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostos(EHojaCostos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var listaHCC = listarHojaCostosConceptos(oBe.hjcc_icod_hoja_costo);
                    listaHCC.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarHojaCostosConceptos(x);
                    });
                    var listaHCS = listarHojaCostosSubConceptos(oBe.hjcc_icod_hoja_costo);
                    listaHCS.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarHojaCostosSubConceptos(x);
                    });
                    var listaHCR = listarHojaCostosRubros(oBe.hjcc_icod_hoja_costo);
                    listaHCR.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarHojaCostosRubros(x);
                    });
                    objVentasData.eliminarHojaCostos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EHojaCostos> getHCCabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                lista = objVentasData.getHCCabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Hoja Costos Conceptos
        public List<EHojaCostosConceptos> listarHojaCostosConceptos(int Concepto)
        {
            List<EHojaCostosConceptos> lista = new List<EHojaCostosConceptos>();
            try
            {
                lista = objVentasData.listarHojaCostosConceptos(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosConceptos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Hoja Costos Sub-Conceptos
        public List<EhojaCostosSubConceptos> listarHojaCostosSubConceptos(int SubConcepto)
        {
            List<EhojaCostosSubConceptos> lista = new List<EhojaCostosSubConceptos>();
            try
            {
                lista = objVentasData.listarHojaCostosSubConceptos(SubConcepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosSubConceptos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosSubConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosSubConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Hoja Costos Rubros
        public List<EHojaCostosRubros> listarHojaCostosRubros(int Rubros)
        {
            List<EHojaCostosRubros> lista = new List<EHojaCostosRubros>();
            try
            {
                lista = objVentasData.listarHojaCostosRubros(Rubros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EHojaCostosRubros> listarHojaCostosRubrosRQM(int Rubros)
        {
            List<EHojaCostosRubros> lista = new List<EHojaCostosRubros>();
            try
            {
                lista = objVentasData.listarHojaCostosRubrosRQM(Rubros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosRubros(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosRubros(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosRubros(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Requerimiento Materiales
        public List<ERequerimientoMateriales> listarRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                lista = objVentasData.listarRequerimientoMateriales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ERequerimientoMateriales> listarAutorizacionRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                lista = objVentasData.listarAutorizacionRequerimientoMateriales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ERequerimientoMateriales> listarVerificacionStockRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                lista = objVentasData.listarVerificacionStockRequerimientoMateriales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRequerimientoMateriales(ERequerimientoMateriales oBe, List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarRequerimientoMateriales(oBe);

                    lstRequerimientoMaterialesDetalle.ForEach(x =>
                    {
                        decimal Cantidad_Saldo = objVentasData.listarCantidadRequeidaRQM(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                        if (Cantidad_Saldo < x.rqmd_cantidad_pedida)
                        {
                            throw new Exception("La Cantidad Requerida, Sobrepasa al Saldo de la Hoja de Costo");
                        }
                        x.rqmc_icod_requerimiento_materiales = intIcod;
                        insertarRequerimientoMaterialesDetalle(x);
                        ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRequerimientoMateriales(ERequerimientoMateriales oBe, List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle, List<ERequerimientoMaterialesDetalle> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.modificarRequerimientoMateriales(oBe);
                    lstRequerimientoMaterialesDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 2)
                        {
                            modificarRequerimientoMaterialesDetalle(x);
                            ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                        }
                        else
                        {
                            x.rqmd_cantidad_aprobada = x.rqmd_cantidad_pedida;
                            modificarRequerimientoMaterialesDetalle(x);
                            ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                        }

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRequerimientoMateriales(ERequerimientoMateriales oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<ERequerimientoMaterialesDetalle> lsrRQMDetalle = new List<ERequerimientoMaterialesDetalle>();
                    lsrRQMDetalle = listarAutorizacionRequerimientoMaterialesDetalle(oBe.rqmc_icod_requerimiento_materiales);
                    lsrRQMDetalle.ForEach(x =>
                    {
                        new BVentas().eliminarRequerimientoMaterialesDetalle(x);
                        ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                    });
                    objVentasData.eliminarRequerimientoMateriales(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularRequerimientoMateriales(ERequerimientoMateriales obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.AnularRequerimientoMateriales(obj.rqmc_icod_requerimiento_materiales, 312);//Parametros.intSituacOCAnulado);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AutorizacionRequerimientoMaterialesEliminar(ERequerimientoMateriales oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.AutorizacionRequerimientoMaterialesEliminar(oBe);
                    List<ERequerimientoMaterialesDetalle> lstDetalleRQ = new List<ERequerimientoMaterialesDetalle>();
                    lstDetalleRQ = new BVentas().listarRequerimientoMaterialesDetalle(oBe.rqmc_icod_requerimiento_materiales);
                    lstDetalleRQ.ForEach(x =>
                    {
                        AutorizacionRequerimientoMaterialesDetalleEliminar(x);
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Requerimiento Materiales Detalle
        public List<ERequerimientoMaterialesDetalle> listarRequerimientoMaterialesDetalle(int Concepto)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                lista = objVentasData.listarRequerimientoMaterialesDetalle(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ERequerimientoMaterialesDetalle> listarAutorizacionRequerimientoMaterialesDetalle(int Concepto)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                lista = objVentasData.listarAutorizacionRequerimientoMaterialesDetalle(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarRequerimientoMaterialesDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarRequerimientoMaterialesDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarRequerimientoMaterialesDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AutorizacionRequerimientoMaterialesDetalleEliminar(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.AutorizacionRequerimientoMaterialesDetalleEliminar(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Concepto Costos
        public List<EConceptosCostos> listarConceptosCostos()
        {
            List<EConceptosCostos> lista = new List<EConceptosCostos>();
            try
            {
                lista = objVentasData.listarConceptosCostos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarConceptosCostos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarConceptosCostos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarConceptosCostos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Concepto Costos Detalle
        public List<EConceptosCostosDetalle> listarConceptosCostosDetalle(int SubConcepto)
        {
            List<EConceptosCostosDetalle> lista = new List<EConceptosCostosDetalle>();
            try
            {
                lista = objVentasData.listarConceptosCostosDetalle(SubConcepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EConceptosCostosDetalle> listarConceptosCostosDetalle2()
        {
            List<EConceptosCostosDetalle> lista = new List<EConceptosCostosDetalle>();
            try
            {
                lista = objVentasData.listarConceptosCostosDetalle2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarConceptosCostosDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarConceptosCostosDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarConceptosCostosDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Suma Cantidad Requerida
        public void ActualizarCantidadRequerida(int hjcd3_icod_rubros_hc, int prdc_icod_producto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarCantidadRequerida(hjcd3_icod_rubros_hc, prdc_icod_producto);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Garantia Clientes
        public List<EGarantiaClientes> listarGarantiaClientes()
        {
            List<EGarantiaClientes> lista = null;
            try
            {
                lista = objVentasData.listarGarantiaClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGarantiaClientes(EGarantiaClientes oBe)
        {
            CuentasPorCobrarData objCuentasPorPagarDataGC = new CuentasPorCobrarData();
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarGarantiaClientes(oBe);
                    oBe.garc_icod_garantia = intIcod;
                    #region Generación de Doc Por Pagar
                    EDocXCobrar objDXP = crearDocPorPagarGarantiaClientes(oBe);
                    oBe.doxpc_icod_correlativo = objCuentasPorPagarDataGC.insertarDocumentoXCobrar(objDXP);
                    #endregion
                    #region Pago de DXC
                    EDocXCobrarPago obj_DXC_pago = new EDocXCobrarPago();
                    obj_DXC_pago.doxcc_icod_correlativo = oBe.favc_icod_factura;
                    obj_DXC_pago.tdocc_icod_tipo_doc = 112;
                    obj_DXC_pago.pdxcc_vnumero_doc = oBe.garc_vnumero_garantia;
                    obj_DXC_pago.pdxcc_sfecha_cobro = oBe.garc_sfecha_garantia;
                    obj_DXC_pago.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj_DXC_pago.pdxcc_nmonto_cobro = oBe.garc_nmonto;
                    obj_DXC_pago.pdxcc_nmonto_tipo_cambio = objDXP.doxcc_nmonto_tipo_cambio;
                    obj_DXC_pago.pdxcc_vobservacion = String.Format("GC N° : {0}", oBe.garc_vnumero_garantia);
                    //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                    obj_DXC_pago.pdxcc_vorigen = "L";
                    //obj_DXC_pago.doxcc_icod_correlativo = null;
                    //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                    //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                    //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                    obj_DXC_pago.intUsuario = oBe.intUsuario;
                    obj_DXC_pago.strPc = oBe.strPc;
                    //obj_DXC_pago.pdxpc_mes = oBe.garc_sfecha_garantia.Month;
                    //obj_DXC_pago.anio = oBe.garc_sfecha_garantia.Year;
                    obj_DXC_pago.pdxcc_flag_estado = true;

                    oBe.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_pago);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBe.favc_icod_factura, obj_DXC_pago.tablc_iid_tipo_moneda);
                    #endregion
                    objVentasData.modificarGarantiaClientes(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGarantiaClientes(EGarantiaClientes oBe)
        {
            CuentasPorCobrarData objCuentasPorPagarDataGC = new CuentasPorCobrarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe.doxpc_icod_correlativo = oBe.intDXP;
                    objVentasData.modificarGarantiaClientes(oBe);
                    #region Modificación del Doc Por Pagar
                    EDocXCobrar objDXP = crearDocPorPagarGarantiaClientes(oBe);
                    objCuentasPorPagarDataGC.modificarDocumentoXCobrar(objDXP);
                    #endregion
                    #region Pago de DXC
                    EDocXCobrarPago obj_DXC_pago = new EDocXCobrarPago();
                    obj_DXC_pago.doxcc_icod_correlativo = oBe.favc_icod_factura;
                    obj_DXC_pago.tdocc_icod_tipo_doc = 112;
                    obj_DXC_pago.pdxcc_vnumero_doc = oBe.garc_vnumero_garantia;
                    obj_DXC_pago.pdxcc_sfecha_cobro = oBe.garc_sfecha_garantia;
                    obj_DXC_pago.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj_DXC_pago.pdxcc_nmonto_cobro = oBe.garc_nmonto;
                    obj_DXC_pago.pdxcc_nmonto_tipo_cambio = objDXP.doxcc_nmonto_tipo_cambio;
                    obj_DXC_pago.pdxcc_vobservacion = String.Format("GC N° : {0}", oBe.garc_vnumero_garantia);
                    //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                    obj_DXC_pago.pdxcc_vorigen = "L";
                    //obj_DXC_pago.doxcc_icod_correlativo = null;
                    //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                    //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                    //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                    obj_DXC_pago.intUsuario = oBe.intUsuario;
                    obj_DXC_pago.strPc = oBe.strPc;
                    //obj_DXC_pago.pdxpc_mes = oBe.garc_sfecha_garantia.Month;
                    //obj_DXC_pago.anio = oBe.garc_sfecha_garantia.Year;
                    obj_DXC_pago.pdxcc_flag_estado = true;

                    new CuentasPorCobrarData().ActualizarPagoDirectoDocumentoXCobrar(obj_DXC_pago);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBe.favc_icod_factura, obj_DXC_pago.tablc_iid_tipo_moneda);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGarantiaClientes(EGarantiaClientes oBe)
        {
            CuentasPorCobrarData objCuentasPorPagarDataGC = new CuentasPorCobrarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarGarantiaClientes(oBe);

                    //#region Eliminación de Doc Por Pagar
                    //EDocXCobrar objDXP = crearDocPorPagarGarantiaClientes(oBe);
                    //objCuentasPorPagarDataGC.EliminarDocumentoXCobrar(objDXP);
                    //#endregion
                    EDocXCobrar objDXP = new EDocXCobrar();
                    objDXP.doxcc_icod_correlativo = oBe.intDXP;
                    objDXP.intUsuario = oBe.intUsuario;
                    objDXP.strPc = oBe.strPc;
                    new CuentasPorCobrarData().EliminarDocumentoXCobrar(objDXP);
                    #region Eliminar Pago DXP
                    EDocXCobrarPago oBeDXCPago = new EDocXCobrarPago();
                    oBeDXCPago.pdxcc_icod_correlativo = Convert.ToInt64(oBe.pdxpc_icod_correlativo);
                    oBeDXCPago.intUsuario = oBe.intUsuario;
                    oBeDXCPago.strPc = oBe.strPc;
                    new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(oBeDXCPago);
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(oBe.favc_icod_factura), oBe.tablc_iid_tipo_moneda);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public EDocXCobrar crearDocPorPagarGarantiaClientes(EGarantiaClientes obj)
        {
            EDocXCobrar objDXC = new EDocXCobrar();
            try
            {
                #region
                //objDXP.doxpc_icod_correlativo = obj.intDXP;
                //objDXP.anio = obj.garp_sfecha_garantia.Year;
                //objDXP.mesec_iid_mes = obj.garp_sfecha_garantia.Month;
                //objDXP.tdocc_icod_tipo_doc = 113;//TIPO DOCUMENTO 113 DE GARANTIA PROVEEDORES
                //objDXP.tdodc_iid_correlativo = 84; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                //objDXP.doxpc_iid_correlativo = 0;
                //objDXP.doxpc_vnumero_doc = obj.garap_vnumero_garantia;
                //objDXP.doxpc_sfecha_doc = obj.garp_sfecha_garantia;
                //objDXP.doxpc_sfecha_vencimiento_doc = obj.garp_sfecha_garantia;
                //objDXP.proc_icod_proveedor = obj.proc_icod_proveedor;

                //objDXP.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;

                //objDXP.doxpc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.garp_sfecha_garantia);
                //if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                //    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                //objDXP.doxpc_vdescrip_transaccion = "GARANTIA POR SERVICIO";
                //objDXP.doxpc_nmonto_destino_gravado = 0;
                //objDXP.doxpc_nmonto_destino_mixto = 0;
                //objDXP.doxpc_nmonto_destino_nogravado = 0;
                //objDXP.doxpc_nmonto_nogravado = obj.garp_nmonto;
                //objDXP.doxpc_nmonto_imp_destino_gravado = 0;
                //objDXP.doxpc_nmonto_imp_destino_mixto = 0;
                //objDXP.doxpc_nmonto_imp_destino_nogravado = 0;
                //objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(obj.garp_nmonto);
                //objDXP.doxpc_nmonto_total_pagado = 0;
                //objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.garp_nmonto);
                //objDXP.doxpc_nporcentaje_igv = 0;
                //objDXP.tablc_iid_situacion_documento = 8;
                //objDXP.doxpc_tipo_comprobante_referencia = 0;
                //objDXP.doxpc_num_serie_referencia = "";
                //objDXP.doxpc_num_comprobante_referencia = "";
                //objDXP.doxpc_sfecha_emision_referencia = null;
                //objDXP.doxpc_nporcentaje_isc = 0;
                //objDXP.doxpc_nmonto_isc = 0;
                //objDXP.doxpc_nmonto_referencial_cif = 0;
                //objDXP.doxpc_nmonto_retenido = 0;
                //objDXP.doxpc_nmonto_retencion_rh = 0;
                //objDXP.doxpc_nmonto_servicio_no_domic = 0;
                //objDXP.doxpc_nporcentaje_imp_renta = 0;
                //objDXP.doxpc_vnro_deposito_detraccion = null;
                //objDXP.doxpc_sfec_deposito_detraccion = null;
                //objDXP.intUsuario = obj.intUsuario;
                //objDXP.strPc = obj.strPc;
                //objDXP.doxpc_origen = "9";
                //objDXP.doxpc_flag_estado = true;
                //objDXP.doxpc_numdoc_tipo = 2;
                #endregion
                objDXC.mesec_iid_mes = Convert.ToInt16(obj.garc_sfecha_garantia.Month);
                objDXC.tdocc_icod_tipo_doc = 112;
                objDXC.tdodc_iid_correlativo = 83;
                objDXC.doxcc_vnumero_doc = obj.garc_vnumero_garantia;
                objDXC.cliec_icod_cliente = obj.cliec_icod_cliente;
                objDXC.cliec_vnombre_cliente = obj.NomClie;
                objDXC.doxcc_sfecha_doc = obj.garc_sfecha_garantia;
                objDXC.doxcc_sfecha_vencimiento_doc = obj.garc_sfecha_garantia;
                objDXC.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;
                objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.garc_sfecha_garantia);
                if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXC.tablc_iid_tipo_pago = 174;
                objDXC.doxcc_vdescrip_transaccion = "GARANTIA POR SERVICIO";
                objDXC.doxcc_nmonto_afecto = 0;
                objDXC.doxcc_nmonto_inafecto = obj.garc_nmonto;
                objDXC.doxcc_nporcentaje_igv = 0;
                objDXC.doxcc_nmonto_impuesto = 0;
                objDXC.doxcc_nmonto_total = obj.garc_nmonto;
                objDXC.doxcc_nmonto_saldo = obj.garc_nmonto;
                objDXC.doxcc_nmonto_pagado = 0;
                objDXC.tablc_iid_situacion_documento = 8;
                objDXC.doxcc_vobservaciones = "";
                objDXC.doxc_bind_cuenta_corriente = false;
                objDXC.doxcc_sfecha_entrega = null;
                objDXC.doxcc_bind_impresion_nogerencia = false;
                objDXC.doxc_bind_situacion_legal = false;
                objDXC.doxc_bind_cierre_cuenta_corriente = false;
                objDXC.intUsuario = obj.intUsuario;
                objDXC.strPc = obj.strPc;
                objDXC.doxcc_tipo_comprobante_referencia = 0;
                objDXC.doxcc_num_serie_referencia = "";
                objDXC.doxcc_num_comprobante_referencia = "";
                objDXC.doxcc_sfecha_emision_referencia = null;
                objDXC.docxc_icod_documento = obj.garc_icod_garantia;
                objDXC.anio = obj.garc_sfecha_garantia.Year;
                objDXC.doxcc_flag_estado = true;
                objDXC.doxcc_origen = "9";
                return objDXC;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region VentasDaot

        public List<EVentasDaot> ListarVentasDaot(decimal monto, int anio)
        {
            List<EVentasDaot> lista = null;
            try
            {
                lista = (objVentasData).ListarVentasDaot(monto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EVentasDaotDetalle> ListarVentasDaotDetallexCliente(long proc_icod_proveedor, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                lista = (objVentasData).ListarVentasDaotDetallexCliente(proc_icod_proveedor, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EVentasDaotDetalle> ListarVentasDaotDetalle(decimal monto, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                lista = (objVentasData).ListarVentasDaotDetalle(monto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion


        #region Caja
        public List<ECaja> listarCaja()
        {
            List<ECaja> lista = null;
            try
            {
                lista = objVentasData.listarCaja();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarCaja(ECaja oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarCaja(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCaja(ECaja oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarCaja(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarCaja(ECaja oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarCaja(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ValidarCodigoCaja()
        {
            int Codcaja;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Codcaja = objVentasData.ValidarCodigoCaja();
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Codcaja;
        }
        #endregion
        #region Vendedor
        public List<EVendedor> listarVendedor()
        {
            List<EVendedor> lista = null;
            try
            {
                lista = objVentasData.listarVendedor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarVendedor(EVendedor objEVendedor)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarVendedor(objEVendedor);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVendedor(EVendedor objEVendedor)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarVendedor(objEVendedor);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVendedor(EVendedor objEVendedor)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarVendedor(objEVendedor);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registro Pedidos PVT
        public List<EPedidosPVT> listarPedidosPVT()
        {
            List<EPedidosPVT> lista = new List<EPedidosPVT>();
            try
            {
                lista = objVentasData.listarPedidosPVT();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPedidosPVT(EPedidosPVT oBe, List<EPedidosPVTDetalle> lstPedidosPVTDetalle)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarPedidosPVT(oBe);

                    lstPedidosPVTDetalle.ForEach(x =>
                    {
                        x.pdvc_icod_pedido = intIcod;
                        insertarPedidosPVTDetalle(x);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPedidosPVT(EPedidosPVT oBe, List<EPedidosPVTDetalle> lstPedidosPVTDetalle, List<EPedidosPVTDetalle> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.modificarPedidosPVT(oBe);
                    lstPedidosPVTDetalle.ForEach(x =>
                    {

                        modificarPedidosPVTDetalle(x);
                        //if (x.intTipoOperacion == 2)
                        //{
                        //    modificarRequerimientoMaterialesDetalle(x);

                        //}
                        //else
                        //{

                        //    modificarRequerimientoMaterialesDetalle(x);
                        //    ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                        //}

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPedidosPVT(EPedidosPVT oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EPedidosPVTDetalle> lstPVTDetalle = new List<EPedidosPVTDetalle>();
                    lstPVTDetalle = listarPedidosPVTDetalle(oBe.pdvc_icod_pedido);
                    lstPVTDetalle.ForEach(x =>
                    {
                        new BVentas().eliminarPedidosPVTDetalle(x);
                    });
                    objVentasData.eliminarPedidosPVT(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registro Pedidos PVT Detalle
        public List<EPedidosPVTDetalle> listarPedidosPVTDetalle(int pdvc_icod_pedido)
        {
            List<EPedidosPVTDetalle> lista = new List<EPedidosPVTDetalle>();
            try
            {
                lista = objVentasData.listarPedidosPVTDetalle(pdvc_icod_pedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPedidosPVTDetalle(EPedidosPVTDetalle oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarPedidosPVTDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPedidosPVTDetalle(EPedidosPVTDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPedidosPVTDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPedidosPVTDetalle(EPedidosPVTDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPedidosPVTDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Planilla Venta Diaria
        public List<EFacturaDet> listarFacturaDetallePlanilla(int intFactura)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                lista = objVentasData.listarFacturaDetallePlanilla(intFactura, Parametros.intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarFacturaDetallePlanilla(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarFacturaDetallePlanilla(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaDetallePlanilla(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFacturaDetallePlanilla(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaDetallePlanilla(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaDetallePlanilla(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EBoletaDet> listarBoletaDetallePlanilla(int intBoleta)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>();
            try
            {
                lista = objVentasData.listarBoletaDetallePlanilla(intBoleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarBoletaDetallePlanilla(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarBoletaDetallePlanilla(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaDetallePlanilla(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarBoletaDetallePlanilla(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaDetallePlanilla(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarBoletaDetallePlanilla(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registro Lista Precio 
        public List<EListaPrecio> listarListaPrecio()
        {
            List<EListaPrecio> lista = new List<EListaPrecio>();
            try
            {
                lista = (objVentasData).listarListaPrecio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarListaPrecio(EListaPrecio obe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarListaPrecio(obe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarListaPrecio(EListaPrecio obe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarListaPrecio(obe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarListaPrecio(EListaPrecio obe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarListaPrecio(obe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Lista Precio Detalle
        public List<EListaPrecioDetalle> listarListaPrecioDetalle(int lprecc_icod_precio)
        {
            List<EListaPrecioDetalle> lista = new List<EListaPrecioDetalle>(); ;
            try
            {
                lista = (objVentasData).listarListaPrecioDetalle(lprecc_icod_precio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarListaPrecioDetalle(EListaPrecioDetalle obe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarListaPrecioDetalle(obe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarListaPrecioDetalle(EListaPrecioDetalle obe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarListaPrecioDetalle(obe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarListaPrecioDetalle(EListaPrecioDetalle obe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarListaPrecioDetalle(obe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Ticketera
        public List<ETicketera> listarTicketera()
        {
            List<ETicketera> lista = null;
            try
            {
                lista = objVentasData.listarTicketera();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTicketera(ETicketera oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarTicketera(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTicketera(ETicketera oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarTicketera(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTicketera(ETicketera oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarTicketera(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Asignacion Vendedor
        public List<EAsignacionVendedor> listarAsignacionVendedor(int cajac_icod_caja)
        {
            List<EAsignacionVendedor> lista = null;
            try
            {
                lista = objVentasData.listarAsignacionVendedor(cajac_icod_caja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAsignacionVendedor(EAsignacionVendedor oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarAsignacionVendedor(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAsignacionVendedor(EAsignacionVendedor oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarAsignacionVendedor(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAsignacionVendedor(EAsignacionVendedor oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarAsignacionVendedor(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Parametros de Ventas
        public List<EParametro> listarParametroVenta()
        {
            List<EParametro> lista = null;
            try
            {
                lista = objVentasData.listarParametroVenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarParametroVenta(EParametro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarParametroVenta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Orden de Despacho
        public List<EOrdenDespacho> listarOrdenDespachoxMes(int año, int mes)
        {
            List<EOrdenDespacho> lista = null;
            try
            {
                lista = objVentasData.listarOrdenDespachoxMes(año, mes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarOrdenDespacho(EOrdenDespacho oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarOrdenDespacho(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarOrdenDespachoAutoventa(EOrdenDespacho oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarOrdenDespachoAutoventa(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarOrdenDespacho(EOrdenDespacho oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarOrdenDespacho(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string generarNumeroOrdenDespacho(int año)
        {
            string numOrdenDespachoAutoventa = null;
            try
            {
                numOrdenDespachoAutoventa = (objVentasData).generarNumeroOrdenDespacho(año);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return numOrdenDespachoAutoventa;
        }
        public EOrdenDespacho listarOrdenDespachoXId(int desac_icod_despacho)
        {
            EOrdenDespacho lista = null;
            try
            {
                lista = (objVentasData).listarOrdenDespachoXId(desac_icod_despacho);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region registro de Almacenes
        public List<ERegistroParametro> listarRegistroParametro()
        {
            List<ERegistroParametro> lista = null;
            try
            {
                lista = objVentasData.listarRegistroParametro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRegistroParametro(ERegistroParametro oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarRegistroParametro(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRegistroParametro(ERegistroParametro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarRegistroParametro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRegistroParametro(ERegistroParametro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarRegistroParametro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Espacios
        public List<EEspacios> listarEspacios()
        {
            List<EEspacios> lista = null;
            try
            {
                lista = objVentasData.listarEspacios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarEspacios(EEspacios oBe, List<EEspaciosDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarEspacios(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        x.espac_iid_iespacios = intIcod;
                        new BVentas().insertarEspaciosDet(x);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarEspacios(EEspacios oBe, List<EEspaciosDet> lstDetalle, List<EEspaciosDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarEspacios(oBe);
                    lstDelete.ForEach(x =>
                    {
                        new BVentas().eliminarEspaciosDet(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.espac_iid_iespacios = oBe.espac_iid_iespacios;
                            new BVentas().insertarEspaciosDet(x);
                        }
                        else
                        {
                            new BVentas().modificarEspaciosDet(x);
                        }

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarEspacios(EEspacios oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarEspacios(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Espacios Det
        public List<EEspaciosDet> listarEspaciosDet(int espac_iid_iespacios)
        {
            List<EEspaciosDet> lista = null;
            try
            {
                lista = objVentasData.listarEspaciosDet(espac_iid_iespacios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarEspaciosDet(EEspaciosDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarEspaciosDet(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarEspaciosDet(EEspaciosDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarEspaciosDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarEspaciosDet(EEspaciosDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarEspaciosDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarEspaciosDet(EEspaciosDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.actualizarEspaciosDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarEspaciosDetEstado(EEspaciosDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.actualizarEspaciosDetEstado(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EEspaciosDet> listarEspaciosConsultas()
        {
            List<EEspaciosDet> lista = null;
            try
            {
                lista = objVentasData.listarEspaciosConsultas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarEspaciosDetConsultas(EEspaciosDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarEspaciosDetConsultas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Espacios Autorizacion Uso
        public List<EEspaciosAutorizacionUso> listarEspaciosAutorizacionUso()
        {
            List<EEspaciosAutorizacionUso> lista = null;
            try
            {
                lista = objVentasData.listarEspaciosAutorizacionUso();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarEspaciosAutorizacionUso(EEspaciosAutorizacionUso oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarEspaciosAutorizacionUso(oBe);

                    List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(oBe.espac_iid_iespacios).Where(x => x.espad_iid_iespacios == oBe.espad_iid_iespacios).ToList();
                    lstEspacioDet.ForEach(x =>
                    {
                        x.espad_vnom_fallecido = oBe.espau_vnom_fallecido;
                        x.espad_vapellido_paterno_fallecido = oBe.espau_vapellido_paterno_fallecido;
                        x.espad_vapellido_materno_fallecido = oBe.espau_vapellido_materno_fallecido;
                        x.espad_vdni_fallecido = oBe.espau_vdni_fallecido;
                        x.espad_sfecha_nac_fallecido = oBe.espau_sfecha_nac_fallecido;
                        x.espad_sfecha_fallecido = oBe.espau_sfecha_fallecido;
                        x.espad_sfecha_entierro = oBe.espau_sfecha_entierro;
                        x.espad_inacionalidad = oBe.espau_inacionalidad;
                        x.espad_icod_iestado = 16;
                        new BVentas().modificarEspaciosDetConsultas(x);
                    });

                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarEspaciosAutorizacionUso(EEspaciosAutorizacionUso oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarEspaciosAutorizacionUso(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarEspaciosAutorizacionUso(EEspaciosAutorizacionUso oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarEspaciosAutorizacionUso(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Plantilla Programacion
        public List<EPlantillaProgramacion> listarPlantillaProgramacion()
        {
            List<EPlantillaProgramacion> lista = null;
            try
            {
                lista = objVentasData.listarPlantillaProgramacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPlantillaProgramacion(EPlantillaProgramacion oBe, List<EPlantillaProgramacionDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarPlantillaProgramacion(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        x.plap_icod_plantilla_programacion = intIcod;
                        new BVentas().insertarPlantillaProgramacionDet(x);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlantillaProgramacion(EPlantillaProgramacion oBe, List<EPlantillaProgramacionDet> lstDetalle, List<EPlantillaProgramacionDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPlantillaProgramacion(oBe);
                    lstDelete.ForEach(x =>
                    {
                        new BVentas().eliminarPlantillaProgramacionDet(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.plap_icod_plantilla_programacion = oBe.plap_icod_plantilla_programacion;
                            new BVentas().insertarPlantillaProgramacionDet(x);
                        }
                        else
                        {
                            new BVentas().modificarPlantillaProgramacionDet(x);
                        }

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlantillaProgramacion(EPlantillaProgramacion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPlantillaProgramacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Plantilla Programacion Det
        public List<EPlantillaProgramacionDet> listarPlantillaProgramacionDet(int espac_iid_iespacios)
        {
            List<EPlantillaProgramacionDet> lista = null;
            try
            {
                lista = objVentasData.listarPlantillaProgramacionDet(espac_iid_iespacios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPlantillaProgramacionDet(EPlantillaProgramacionDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarPlantillaProgramacionDet(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlantillaProgramacionDet(EPlantillaProgramacionDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPlantillaProgramacionDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlantillaProgramacionDet(EPlantillaProgramacionDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPlantillaProgramacionDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Registro Programacion
        public List<ERegistroProgramacion> listarRegistroProgramacion()
        {
            List<ERegistroProgramacion> lista = null;
            try
            {
                lista = objVentasData.listarRegistroProgramacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRegistroProgramacion(ERegistroProgramacion oBe, List<ERegistroProgramacionDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarRegistroProgramacion(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        x.rp_icod_registro_programacion = intIcod;
                        new BVentas().insertarRegistroProgramacionDet(x);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRegistroProgramacion(ERegistroProgramacion oBe, List<ERegistroProgramacionDet> lstDetalle, List<ERegistroProgramacionDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarRegistroProgramacion(oBe);
                    lstDelete.ForEach(x =>
                    {
                        new BVentas().eliminarRegistroProgramacionDet(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.rp_icod_registro_programacion = oBe.rp_icod_registro_programacion;
                            new BVentas().insertarRegistroProgramacionDet(x);
                        }
                        else
                        {
                            new BVentas().modificarRegistroProgramacionDet(x);
                        }

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRegistroProgramacion(ERegistroProgramacion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarRegistroProgramacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registro Programacion Det
        public List<ERegistroProgramacionDet> listarRegistroProgramacionDet(int espac_iid_iespacios)
        {
            List<ERegistroProgramacionDet> lista = null;
            try
            {
                lista = objVentasData.listarRegistroProgramacionDet(espac_iid_iespacios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRegistroProgramacionDet(ERegistroProgramacionDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarRegistroProgramacionDet(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRegistroProgramacionDet(ERegistroProgramacionDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarRegistroProgramacionDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRegistroProgramacionDet(ERegistroProgramacionDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarRegistroProgramacionDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<EProgramacion> listarProgramacion(DateTime fecha)
        {
            List<EProgramacion> lista = null;
            try
            {
                lista = objVentasData.listarProgramacion(fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarProgramacion(EProgramacion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarProgramacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ERegistroParametro> getCorrelativoRP(int intRP)
        {
            try
            {
                var lst = objVentasData.getCorrelativoRP(intRP);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EVentasVendedor> listarVentasVendedor(int anio, int IcodVendedor, DateTime FInicio, DateTime FFin)
        {
            List<EVentasVendedor> lista = null;
            try
            {
                lista = objVentasData.listarVentasVendedor(anio, IcodVendedor, FInicio, FFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        #region Zona
        public List<EZona> listarZona()
        {
            List<EZona> lista = null;
            try
            {
                lista = objVentasData.listarZona();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarZona(EZona oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarZona(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarZona(EZona oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarZona(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarZona(EZona oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarZona(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<ECategoriaFamilia> listarCategoria_Famili_detalle_Todo()
        //{
        //    List<ECategoriaFamilia> lista = null;
        //    try
        //    {
        //        lista = new AlmacenData().listarCategoria_Famili_detalle_Todo();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        #endregion
        #region Distrito Zona
        public List<EDistritoZona> listarDistritoZona(int intIcodZona)
        {
            List<EDistritoZona> lista = null;
            try
            {
                lista = objVentasData.listarDistritoZona(intIcodZona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EDistritoZona> listarVerificarDistrito(int inticoddistrito)
        {
            List<EDistritoZona> lista = null;
            try
            {
                lista = objVentasData.listarVerificarDistrito(inticoddistrito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarDistritoZona(EDistritoZona oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarDistritoZona(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDistritoZona(EDistritoZona oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarDistritoZona(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDistritoZona(EDistritoZona oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarDistritoZona(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Distritos
        public List<EDistritos> listarDistrito()
        {
            List<EDistritos> lista = null;
            try
            {
                lista = objVentasData.listarDistrito();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarDistrito(EDistritos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarDistrito(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDistrito(EDistritos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarDistrito(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDistrito(EDistritos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarDistrito(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Funerarias
        public List<EFunerarias> listarFuneraria()
        {
            List<EFunerarias> lista = null;
            try
            {
                lista = objVentasData.listarFuneraria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFuneraria(EFunerarias oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarFuneraria(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFuneraria(EFunerarias oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFuneraria(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFuneraria(EFunerarias oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFuneraria(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Reportes de Ventas
        public List<EReporteVentas> listarReporteVentas()
        {
            List<EReporteVentas> lista = null;
            try
            {
                lista = objVentasData.listarReporteVentas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarReporteVentas(EReporteVentas oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarReporteVentas(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarReporteVentas(EReporteVentas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarReporteVentas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarReporteVentas(EReporteVentas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarReporteVentas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Resumen de Documentos
        public int InsertarResumenDocumentos(ESunatResumenDocumentosCab oBe, List<ESunatResumenDocumentosDet> mlistDetalle)
        {
            int intIcod = 0;
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    intIcod = objVentasData.insertarSunatResumenDocumentosCab(oBe);

                    foreach (var ob in mlistDetalle)
                    {
                        ob.IdCabecera = intIcod;
                        insertarSunatResumenDocumentosDet(ob);
                    }


                    tx.Complete();
                }
                return intIcod;
                return intIcodE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarSunatResumenDocumentosDet(ESunatResumenDocumentosDet obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarSunatResumenDocumentosDet(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaResumen(DateTime fechaInicio, DateTime fechaEmision)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronicaResumen(fechaInicio, fechaEmision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ESunatResumenDocumentosDet> listarSunatResumenDocumentosDet(int idCabecera)
        {
            List<ESunatResumenDocumentosDet> lista = new List<ESunatResumenDocumentosDet>();
            try
            {
                lista = objVentasData.listarSunatResumenDocumentosDet(idCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void actualizarResumenDocumentosResponse(int id, int estadoSunat)
        {
            try
            {
                objVentasData.actualizarResumenDocumentosResponse(id, estadoSunat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ESunatResumenDocumentosCab> listarSunatResumenDocumentosCab(DateTime fechaInicio)
        {
            List<ESunatResumenDocumentosCab> lista = new List<ESunatResumenDocumentosCab>();
            try
            {
                lista = objVentasData.listarSunatResumenDocumentosCab(fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ESunatResumenDocumentosCab> listarSunatResumenDocumentosExcel()
        {
            List<ESunatResumenDocumentosCab> lista = new List<ESunatResumenDocumentosCab>();
            try
            {
                lista = objVentasData.listarSunatResumenDocumentosExcel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void eliminarResumenDocumentos(ESunatResumenDocumentosCab obe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    List<ESunatResumenDocumentosDet> mlistFac = new List<ESunatResumenDocumentosDet>();
                    mlistFac = listarSunatResumenDocumentosDet(obe.IdCabecera);
                    foreach (var ob in mlistFac)
                    {
                        objVentasData.eliminarSunatResumenDocumentosDetalle(ob.IdCabecera);
                    }
                    objVentasData.eliminarSunatResumenDocumento(obe.IdCabecera);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Plataforma
        public List<EPlataforma> listarPlataforma()
        {
            List<EPlataforma> lista = null;
            try
            {
                lista = objVentasData.listarPlataforma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPlataforma(EPlataforma oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarPlataforma(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlataforma(EPlataforma oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPlataforma(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlataforma(EPlataforma oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPlataforma(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Contratos
        public List<EContrato> listarContrato(int cntc_iindicador_contr_sol)
        {
            List<EContrato> lista = null;
            try
            {
                lista = objVentasData.listarContrato(cntc_iindicador_contr_sol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EContrato> listar_ubicaciones_fallecidos(DateTime inicio, DateTime final)
        {
            return objVentasData.listar_ubicaciones_fallecidos(inicio, final).OrderBy(x => x.cntc_icod_contrato_fallecido).ToList();
        }

        public int SolicitudContratoInsertar(EContrato oBe)
        {
            var parametro = objVentasData.listarRegistroParametro()[0];
            int correlativo = Convert.ToInt32(oBe.cntc_vnumero_solicitud.Substring(4));
            parametro.rgpmc_icorrelativo_solicitud = correlativo;
            objVentasData.modificarRegistroParametro(parametro);
            return objVentasData.insertarContrato(oBe);
        }

        public void SolicitudContratoModificar(EContrato oBe)
        {
            objVentasData.modificarContrato(oBe);
        }
        public int insertarContrato(EContrato oBe, List<EContratoFallecido> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarContrato(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        x.cntc_icod_contrato = intIcod;
                        new BVentas().insertarContratoFallecido(x);
                    });

                    //ACTUALIZAR CORRELATIVO
                    var parametro = objVentasData.listarRegistroParametro()[0];
                    if (!string.IsNullOrEmpty(oBe.cntc_vnumero_contrato_corr))
                    {
                        int correlativo = Convert.ToInt32(oBe.cntc_vnumero_contrato_corr.Substring(4));
                        parametro.rgpmc_icorrelativo_contrato = correlativo;
                        objVentasData.modificarRegistroParametro(parametro);
                    }


                    List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(oBe.espac_iid_iespacios).Where(x => x.espad_iid_iespacios == oBe.cntc_icod_indicador_espacios).ToList();
                    lstEspacioDet.ForEach(x =>
                    {
                        x.espad_vnom_fallecido = oBe.cntc_vnombre_fallecido;
                        x.espad_vapellido_paterno_fallecido = oBe.cntc_vapellido_paterno_fallecido;
                        x.espad_vapellido_materno_fallecido = oBe.cntc_vapellido_materno_fallecido;
                        x.espad_vdni_fallecido = oBe.cntc_vdni_fallecido;
                        x.espad_sfecha_nac_fallecido = oBe.cntc_sfecha_nac_fallecido;
                        x.espad_sfecha_fallecido = oBe.cntc_sfecha_fallecimiento;
                        x.espad_sfecha_entierro = oBe.cntc_sfecha_entierro;
                        x.espad_inacionalidad = oBe.cntc_inacionalidad;
                        x.espad_icod_iestado = 16;
                        new BVentas().modificarEspaciosDetConsultas(x);
                    });


                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int insertarContrato(EContrato oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarContrato(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarContrato(EContrato oBe, List<EContratoFallecido> lstDetalle, List<EContratoFallecido> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContrato(oBe);
                    lstDelete.ForEach(x =>
                    {
                        new BVentas().eliminarContratoFallecido(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.cntc_icod_contrato = oBe.cntc_icod_contrato;
                            new BVentas().insertarContratoFallecido(x);
                        }
                        else
                        {
                            new BVentas().modificarContratoFallecido(x);
                        }

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarContrato(EContrato oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarContrato(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularContrato(EContrato oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.anularContrato(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Contratos Beneficiario
        public List<EContratoBeneficiario> listarContratoBeneficiario(int cntc_icod_contrato)
        {
            List<EContratoBeneficiario> lista = null;
            try
            {
                lista = objVentasData.listarContratoBeneficiario(cntc_icod_contrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarContratoBeneficiario(EContratoBeneficiario oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarContratoBeneficiario(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContratoBeneficiario(EContratoBeneficiario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoBeneficiario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarContratoBeneficiario(EContratoBeneficiario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarContratoBeneficiario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Contratos Fallecido
        public List<EContratoFallecido> listarContratoFallecido(int cntc_icod_contrato)
        {
            List<EContratoFallecido> lista = null;
            try
            {
                lista = objVentasData.listarContratoFallecido(cntc_icod_contrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarContratoFallecido(EContratoFallecido oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarContratoFallecido(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContratoFallecido(EContratoFallecido oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoFallecido(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarContratoFallecido(EContratoFallecido oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarContratoFallecido(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Contratos Cuotas
        public List<EContratoCuotas> listarContratoCuotas(int cntc_icod_contrato)
        {
            List<EContratoCuotas> lista = null;
            try
            {
                var parametros = objVentasData.listarRegistroParametro().First();
                lista = objVentasData.listarContratoCuotas(cntc_icod_contrato);

                var pendientes = lista
                                    .Where(
                                            x => x.cntc_icod_situacion == (int)EstadoCuota.Pendiente &&
                                            x.cntc_sfecha_cuota <= DateTime.Today.AddDays(-(parametros.rgpmc_idias_mora)) &&
                                            x.cntc_nmonto_mora == 0 &&
                                            x.cntc_icod_tipo_cuota == 337 &&
                                            x.cntc_bautomatico
                                            )
                                    .ToList();

                if (pendientes.Any())
                {
                    pendientes.ForEach(x =>
                    {
                        x.cntc_nmonto_mora = parametros.rgpmc_nmonto_mora;
                        objVentasData.modificarContratoCuotas(x);
                    });
                    lista = objVentasData.listarContratoCuotas(cntc_icod_contrato);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarCCuotas(List<EContratoCuotas> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstDetalle.ForEach(x =>
                    {
                        new BVentas().insertarContratoCuotas(x);
                        new BVentas().ActualizarContrato(x.cntc_icod_contrato);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarContratoCuotas(EContratoCuotas oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarContratoCuotas(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCCuotas(List<EContratoCuotas> lstDetalle, List<EContratoCuotas> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstDelete.ForEach(x =>
                    {
                        new BVentas().eliminarContratoCuotas(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            new BVentas().insertarContratoCuotas(x);
                        }
                        else
                        {
                            new BVentas().modificarContratoCuotas(x);
                        }
                        new BVentas().ActualizarContrato(x.cntc_icod_contrato);

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContratoCuotas(EContratoCuotas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoCuotas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarContratoCuotas(EContratoCuotas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarContratoCuotas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EContratoCuotas> listarCuotas()
        {
            List<EContratoCuotas> lista = null;
            try
            {
                lista = objVentasData.listarCuotas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarContratoCuotasSituacion(EContratoCuotas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoCuotasSituacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContratoVerificacion(EContrato oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoVerificacion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContratoCuotasDocumentos(EContratoCuotas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoCuotasDocumentos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Tablas Sunat Cab
        public List<ETablaSunatCab> TablasSunatListar()
        {
            List<ETablaSunatCab> lista = null;
            try
            {
                lista = (objVentasData).TablasSunatListar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void TablasSunatInsertar(ETablaSunatCab obj)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatInsertar(obj);
                    tx.Complete();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatModificar(ETablaSunatCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatModificar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatEliminar(ETablaSunatCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatEliminar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tablas Sunat Det
        public List<ETablaSunatDet> TablasSunatDetListar(int icod)
        {
            List<ETablaSunatDet> lista = null;
            try
            {
                lista = (objVentasData).TablasSunatDetListar(icod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void TablasSunatDetInsertar(ETablaSunatDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatDetInsertar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatDetModificar(ETablaSunatDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatDetModificar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatDetEliminar(ETablaSunatDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatDetEliminar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        public void ActualizarMotivoBajaCabeceraFactura(EPlanillaCobranzaDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarMotivoBajaCabeceraFactura(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMotivoBajaCabeceraBoleta(EPlanillaCobranzaDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarMotivoBajaCabeceraBoleta(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarDescripcionAnulacion(EContrato obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarDescripcionAnulacion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EContratoTitular1> listarContratoTitular1(int cntc_icod_contrato)
        {
            List<EContratoTitular1> lista = null;
            try
            {
                lista = objVentasData.listarContratoTitular1(cntc_icod_contrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarContratoTitular1(EContratoTitular1 oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarContratoTitular1(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContratoTitular1(EContratoTitular1 oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoTitular1(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarContratoTitular1(EContratoTitular1 oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarContratoTitular1(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EContratoTitular2> listarContratoTitular2(int cntc_icod_contrato)
        {
            List<EContratoTitular2> lista = null;
            try
            {
                lista = objVentasData.listarContratoTitular2(cntc_icod_contrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarContratoTitular2(EContratoTitular2 oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarContratoTitular2(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContratoTitular2(EContratoTitular2 oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarContratoTitular2(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarContratoTitular2(EContratoTitular2 oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarContratoTitular2(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
