using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGE.Entity;
using System.Transactions;
using SGE.DataAccess;
using System.Data;
using SGE.Entity.Sire;

namespace SGE.BusinessLogic
{
    public class BContabilidad
    {
        VentasData objVentasData = new VentasData();
        ContabilidadData objContabilidadData = new ContabilidadData();
        AdministracionSistemaData objAdministracionSistemaData = new AdministracionSistemaData();
        #region Año Ejercicio
        public List<EAnioEjercicio> listarAnioEjercicio()
        {
            List<EAnioEjercicio> lista = null;
            try
            {
                lista = new ContabilidadData().listarAnioEjercicio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAnioEjercicio(EAnioEjercicio oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().insertarAnioEjercicio(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal getTipoCambioPorFecha(DateTime dtFecha)
        {
            decimal dcmlTipoCambio = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    dcmlTipoCambio = new ContabilidadData().getTipoCambioPorFecha(dtFecha);
                    tx.Complete();
                }
                return dcmlTipoCambio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarAnioEjercicio(EAnioEjercicio oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarAnioEjercicio(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESire> SireListar()
        {
            return new ContabilidadData().SireListar();
        }

        public void eliminarAnioEjercicio(EAnioEjercicio oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarAnioEjercicio(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Centro de Costo
        public List<ECentroCosto> listarCentroCosto()
        {
            List<ECentroCosto> lista = null;
            try
            {
                lista = new ContabilidadData().listarCentroCosto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECentroCosto> listarCentroCostoProyectos()
        {
            List<ECentroCosto> lista = null;
            try
            {
                lista = new ContabilidadData().listarCentroCostoProyectos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarCentroCosto(ECentroCosto oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().insertarCentroCosto(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCentroCosto(ECentroCosto oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarCentroCosto(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarCentroCosto(ECentroCosto oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarCentroCosto(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SireGuardar(ESire select)
        {
            new ContabilidadData().SireGuardar(select);
        }
        #endregion
        #region Analíticas
        public List<EAnaliticaDetalle> listarAnaliticaDetalle(int intTipoAnalitica)
        {
            List<EAnaliticaDetalle> lista = null;
            try
            {
                lista = new ContabilidadData().listarAnaliticaDetalle(intTipoAnalitica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EAnaliticaDetalle> listarAnaliticaDetalleTodo()
        {
            List<EAnaliticaDetalle> lista = null;
            try
            {
                lista = new ContabilidadData().listarAnaliticaDetalleTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAnaliticaDetalle(EAnaliticaDetalle oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().insertarAnaliticaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAnaliticaDetalle(EAnaliticaDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarAnaliticaDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAnaliticaDetalle(EAnaliticaDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarAnaliticaDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Parámetro Contable
        public List<EParametroContable> listarParametroContable()
        {
            List<EParametroContable> lista = null;
            try
            {
                lista = new ContabilidadData().listarParametroContable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarParametroContable(EParametroContable oBe)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().insertarParametroContable(oBe);
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarParamentroContable(EParametroContable oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarParamentroContable(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Cuentas Contables
        public List<ECuentaContable> listarCuentaContable()
        {
            List<ECuentaContable> lista = null;
            try
            {
                lista = new ContabilidadData().listarCuentaContable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECuentaContable> listarCuentaContableImpresion(string strCuentaInicio, string strCuentaFin)
        {
            List<ECuentaContable> lista = null;
            try
            {
                lista = new ContabilidadData().listarCuentaContableImpresion(strCuentaInicio, strCuentaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarCuentaContable(ECuentaContable oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().insertarCuentaContable(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCuentaContable(ECuentaContable oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarCuentaContable(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarCuentaContable(ECuentaContable oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarCuentaContable(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Sub Diarios
        public List<EComprobanteDetalle> ListarComprobanteDetallexSubdiario(int intEjercicio, int intMes, int subInicial, int subFinal)
        {
            List<EComprobanteDetalle> lista = null;
            try
            {
                lista = (new ContabilidadData()).ListarComprobanteDetallexSubdiario(intEjercicio, intMes, subInicial, subFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EComprobanteDetalle> ListarComprobanteDetalle(int code)
        {
            List<EComprobanteDetalle> lista = null;
            try
            {
                lista = (new ContabilidadData()).ListarComprobanteDetalle(code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EComprobante> ListarComprobantesxSubdiario(int intEjercicio, int intMes, int subInicial, int subFinal)
        {
            List<EComprobante> lista = null;
            try
            {
                lista = (new ContabilidadData()).ListarComprobantesxSubdiario(intEjercicio, intMes, subInicial, subFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EComprobante> ListarComprobantesxSubdiario_TXT(int intEjercicio, int intMes)
        {
            List<EComprobante> lista = null;
            try
            {
                lista = (new ContabilidadData()).ListarComprobantesxSubdiario_TXT(intEjercicio, intMes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ESubDiario> listarSubDiario()
        {
            List<ESubDiario> lista = null;
            try
            {
                lista = new ContabilidadData().listarSubDiario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarSubDiario(ESubDiario oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().insertarSubDiario(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarSubDiario(ESubDiario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarSubDiario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarSubDiario(ESubDiario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarSubDiario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Comprobantes
        public int getVoucherContableCabCorrelativo(int intEjercicio, int intPeriodo, int intSubdiario)
        {
            int intCorrelativo = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intCorrelativo = new ContabilidadData().getVoucherContableCabCorrelativo(intEjercicio,
                        intPeriodo,
                        intSubdiario);
                    tx.Complete();
                }
                return intCorrelativo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVoucherContableCab> getVoucherContableCab(string strOrigen, int intIcod)
        {
            List<EVoucherContableCab> lista = null;
            try
            {
                lista = new ContabilidadData().getVoucherContableCab(strOrigen, intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableCab> listarVoucherContableCab(int intEjercicio, int intPeriodo)
        {
            List<EVoucherContableCab> lista = null;
            try
            {
                lista = new ContabilidadData().listarVoucherContableCab(intEjercicio, intPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarVoucherContableListasNoOptimizadas(List<EVoucherContableCab> lstVCOCab, List<EVoucherContableDet> lstVCOdet, int intPeriodo, string strOpcion)
        {
            int intIcod = 0;
            ContabilidadData objContabiliadadData = new ContabilidadData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Se debe eliminar los registros anteriores
                    objContabilidadData.eliminarVoucherContableCabPorOrigen(Parametros.intEjercicio, intPeriodo, strOpcion);
                    //Luego procedemos a ingresar los registros 
                    lstVCOCab.ForEach(x =>
                    {
                        intIcod = objContabiliadadData.insertarVoucherContableCab(x);
                        lstVCOdet.Where(a => a.vcocc_icod_vcontable == x.vcocc_icod_vcontable).ToList().ForEach(e =>
                        {
                            e.vcocc_icod_vcontable = intIcod;
                            e.intUsuario = x.intUsuario;
                            e.strPc = x.strPc;
                            objContabiliadadData.insertarVoucherContableDet(e);
                        });
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarVoucherContableListas(DataTable lstVCOCab, DataTable lstVCOdet, int intPeriodo, string strOpcion)
        {
            int intIcod = 0;
            ContabilidadData objContabiliadadData = new ContabilidadData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Se debe eliminar los registros anteriores
                    objContabilidadData.eliminarVoucherContableCabPorOrigen(Parametros.intEjercicio, intPeriodo, strOpcion);

                    new ContabilidadData().InsetarMasivoComprobanteCab(lstVCOCab);
                    new ContabilidadData().InsetarMasivoComprobanteDet(lstVCOdet);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarVoucherContableCab(EVoucherContableCab oBe, List<EVoucherContableDet> lstVContableDet)
        {
            int intIcod = 0;
            ContabilidadData objContabiliadadData = new ContabilidadData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objContabiliadadData.insertarVoucherContableCab(oBe);
                    lstVContableDet.ForEach(x =>
                    {
                        if (x.intTipoOperacion == Parametros.intOperacionNuevo)
                        {
                            x.tablc_iid_tipo_analitica = x.tablc_iid_tipo_analitica == 0 ? null : x.tablc_iid_tipo_analitica;
                            x.anad_icod_analitica = x.anad_icod_analitica == 0 ? null : x.anad_icod_analitica;
                            x.vcocc_icod_vcontable = intIcod;
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objContabiliadadData.insertarVoucherContableDet(x);
                        }
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
        public void modificarVoucherContableCab(EVoucherContableCab oBe, List<EVoucherContableDet> lstDetalle, List<EVoucherContableDet> lstDelete)
        {
            ContabilidadData objContabiliadadData = new ContabilidadData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarVoucherContableCab(oBe);
                    lstDelete.ForEach(x =>
                    {
                        objContabiliadadData.eliminarVoucherContableDet(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == Parametros.intOperacionNuevo)
                        {
                            x.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objContabiliadadData.insertarVoucherContableDet(x);
                        }
                        else if (x.intTipoOperacion == Parametros.intOperacionModificar)
                        {
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objContabiliadadData.modificarVoucherContableDet(x);
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
        public void eliminarVoucherContableCab(EVoucherContableCab oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarVoucherContableCab(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVoucherContableDet> listarVoucherContableDet(int intIcod)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarVoucherContableDet(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void actualizarSituacionVoucher(int intIcod)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().actualizarSituacionVoucher(intIcod);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void redondearVoucher(int intIcod, int intCaso, int intUsuario, string strPC)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().redondearVoucher(intIcod, intCaso, intUsuario, strPC);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRedondeoVoucher(int intEjercicio, int intMes)
        {
            /*ELIMINA LOS REDONDEOS DEL MES SELECCIONADO Y ACTUALIZA LA SITUACION DE LOS COMPROBANTES*/
            List<EVoucherContableCab> lista = null;
            ContabilidadData objContabilidadData = new ContabilidadData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lista = objContabilidadData.eliminarRedondeoVoucher(intEjercicio, intMes);
                    if (lista.Count > 0)
                        lista.ForEach(x =>
                        {
                            objContabilidadData.actualizarSituacionVoucher(x.vcocc_icod_vcontable);
                        });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EComprobante> ListarComprobantesNoCuadrados(int año_ejercicio)
        {
            List<EComprobante> lista = null;
            try
            {
                lista = (new ContabilidadData()).ListarComprobantesNoCuadrados(año_ejercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Diferencia de Cambio
        public List<EVoucherContableDet> listarDiferenciaCambio(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarDiferenciaCambio(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EVoucherContableDet> listarDiferenciaCambio_3(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarDiferenciaCambio_3(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarDiferenciaCambio_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarDiferenciaCambio_2(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarDiferenciaCambio_4(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarDiferenciaCambio_4(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Mayores
        public List<EVoucherContableDet> listarMayorAuxiliarMensual(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorAuxiliarMensual(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorAuxiliarMensual_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorAuxiliarMensual_2(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorAuxiliarDetallado(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorAuxiliarDetallado(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarBalanceComprobacion(int intEjercicio, int intMes, int intMoneda, int intDigitos, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarBalanceComprobacion(intEjercicio, intMes, intMoneda, intDigitos, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarBalanceComprobacion_2(int intEjercicio, int intMes, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarBalanceComprobacion_2(intEjercicio, intMes, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorCCostoMensual(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin,
        string intCCostoInicio, string intCCostoFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorCCostoMensual(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intCCostoInicio, intCCostoFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorCCostoMensual_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin,
           string intCCostoInicio, string intCCostoFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorCCostoMensual_2(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intCCostoInicio, intCCostoFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorCCostoMensual_Balance(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin,
         string intCCostoInicio, string intCCostoFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorCCostoMensual_Balance(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intCCostoInicio, intCCostoFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorAnaliticaMensual(int intEjercicio, int intMes,
           int? intTipoAnalitica, string strAnaliticaInicio, string strAnaliticaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorAnaliticaMensual(intEjercicio, intMes, intTipoAnalitica, strAnaliticaInicio, strAnaliticaFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorAnaliticaMensual_2(int intEjercicio, int intMes,
         int? intTipoAnalitica, string strAnaliticaInicio, string strAnaliticaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorAnaliticaMensual_2(intEjercicio, intMes, intTipoAnalitica, strAnaliticaInicio, strAnaliticaFin, intOpcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorAuxiliarResumen(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorAuxiliarResumen(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EVoucherContableDet> listarMayorAuxiliarResumen_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                lista = new ContabilidadData().listarMayorAuxiliarResumen_2(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Registro de Compras
        public List<ERegistroCompras> ListarRegistroDeComprasGeneral(int anio, int mes)
        {
            List<ERegistroCompras> lista = null;
            try
            {
                lista = new ContabilidadData().ListarRegistroDeComprasGeneral(anio, mes);

                //dando formato para el reporte
                int nc;
                decimal tc;
                foreach (ERegistroCompras item in lista)
                {
                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor || item.tdocc_icod_tipo_doc == 119)
                        nc = -1;
                    else
                        nc = 1;

                    if (item.tablc_iid_tipo_moneda == Parametros.intTipoMonedaSoles)
                        tc = 1;
                    else
                        tc = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio);

                    item.doxpc_nmonto_destino_gravado = Math.Round((decimal)(item.doxpc_nmonto_destino_gravado * tc * nc), 2);
                    item.doxpc_nmonto_destino_mixto = Math.Round((decimal)(item.doxpc_nmonto_destino_mixto * tc * nc), 2);
                    item.doxpc_nmonto_destino_nogravado = Math.Round((decimal)(item.doxpc_nmonto_destino_nogravado * tc * nc), 2);
                    item.doxpc_nmonto_nogravado = Math.Round((decimal)(item.doxpc_nmonto_nogravado * tc * nc), 2);
                    item.doxpc_nmonto_referencial_cif = Math.Round((decimal)(item.doxpc_nmonto_referencial_cif * tc * nc), 2);
                    item.doxpc_nmonto_servicio_no_domic = Math.Round((decimal)(item.doxpc_nmonto_servicio_no_domic * tc * nc), 2);
                    item.valor_compra = Math.Round((decimal)(item.valor_compra * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_gravado = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_gravado * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_mixto = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_mixto * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_nogravado = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_nogravado * tc * nc), 2);
                    //item.doxpc_nmonto_isc = Math.Round((decimal)(item.doxpc_nmonto_isc * tc * nc), 2);
                    item.doxpc_nmonto_total_documento = Math.Round((decimal)(item.doxpc_nmonto_total_documento * tc * nc), 2);
                    item.doxpc_nmonto_total_pagado = Math.Round((decimal)(item.doxpc_nmonto_total_pagado * tc * nc), 2);
                    item.doxpc_nmonto_total_saldo = Math.Round((decimal)(item.doxpc_nmonto_total_saldo * tc * nc), 2);
                    item.MontoDUA = Math.Round((decimal)(item.MontoDUA * tc * nc), 2);
                    //montos reporte
                    item.str_doxpc_nmonto_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_destino_gravado).ToString("N2");
                    item.str_doxpc_nmonto_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_destino_mixto).ToString("N2");
                    item.str_doxpc_nmonto_destino_nogravado = Convert.ToDecimal(item.doxpc_nmonto_destino_nogravado).ToString("N2");
                    item.str_doxpc_nmonto_nogravado = Convert.ToDecimal(item.doxpc_nmonto_nogravado).ToString("N2");
                    item.str_doxpc_nmonto_referencial_cif = Convert.ToDecimal(item.doxpc_nmonto_referencial_cif).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_gravado).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_mixto).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_nogravado).ToString("N2");
                    //item.str_doxpc_nmonto_isc = Convert.ToDecimal(item.doxpc_nmonto_isc).ToString("N2");
                    item.str_doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento).ToString("N2");
                    item.str_doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio).ToString("N4");

                    item.str_doxpc_sfecha_doc = (item.doxpc_sfecha_doc != null) ? Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yy") : "";
                    item.str_doxpc_sfecha_vencimiento_doc = (item.doxpc_sfecha_vencimiento_doc != null) ? Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).ToString("dd/MM/yy") : "";
                    item.str_doxpc_sfec_deposito_detraccion = (!string.IsNullOrWhiteSpace(item.doxpc_vnro_deposito_detraccion)) ? Convert.ToDateTime(item.doxpc_sfec_deposito_detraccion).ToString("dd/MM/yy") : null;

                    item.strClasificacion = item.strClasificacion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ERegistroCompras> ListarRegistroDeCompras(int anio, int mes)
        {
            List<ERegistroCompras> lista = null;
            try
            {
                lista = new ContabilidadData().ListarRegistroDeCompras(anio, mes);

                //dando formato para el reporte
                int nc;
                decimal tc;
                foreach (ERegistroCompras item in lista)
                {
                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor || item.tdocc_icod_tipo_doc == 119)
                        nc = -1;
                    else
                        nc = 1;

                    if (item.tablc_iid_tipo_moneda == Parametros.intTipoMonedaSoles)
                        tc = 1;
                    else
                        tc = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio);

                    item.doxpc_nmonto_destino_gravado = Math.Round((decimal)(item.doxpc_nmonto_destino_gravado * tc * nc), 2);
                    item.doxpc_nmonto_destino_mixto = Math.Round((decimal)(item.doxpc_nmonto_destino_mixto * tc * nc), 2);
                    item.doxpc_nmonto_destino_nogravado = Math.Round((decimal)(item.doxpc_nmonto_destino_nogravado * tc * nc), 2);
                    item.doxpc_nmonto_nogravado = Math.Round((decimal)(item.doxpc_nmonto_nogravado * tc * nc), 2);
                    item.doxpc_nmonto_referencial_cif = Math.Round((decimal)(item.doxpc_nmonto_referencial_cif * tc * nc), 2);
                    item.doxpc_nmonto_servicio_no_domic = Math.Round((decimal)(item.doxpc_nmonto_servicio_no_domic * tc * nc), 2);
                    item.valor_compra = Math.Round((decimal)(item.valor_compra * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_gravado = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_gravado * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_mixto = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_mixto * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_nogravado = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_nogravado * tc * nc), 2);
                    //item.doxpc_nmonto_isc = Math.Round((decimal)(item.doxpc_nmonto_isc * tc * nc), 2);
                    item.doxpc_nmonto_total_documento = Math.Round((decimal)(item.doxpc_nmonto_total_documento * tc * nc), 2);
                    item.doxpc_nmonto_total_pagado = Math.Round((decimal)(item.doxpc_nmonto_total_pagado * tc * nc), 2);
                    item.doxpc_nmonto_total_saldo = Math.Round((decimal)(item.doxpc_nmonto_total_saldo * tc * nc), 2);
                    item.MontoDUA = Math.Round((decimal)(item.MontoDUA * tc * nc), 2);
                    item.MontoTotalDUA = Math.Round((decimal)(item.MontoDUA + item.doxpc_nmonto_imp_destino_gravado), 2);
                    //montos reporte
                    item.str_doxpc_nmonto_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_destino_gravado).ToString("N2");
                    item.str_doxpc_nmonto_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_destino_mixto).ToString("N2");
                    item.str_doxpc_nmonto_destino_nogravado = Convert.ToDecimal(item.doxpc_nmonto_destino_nogravado).ToString("N2");
                    item.str_doxpc_nmonto_nogravado = Convert.ToDecimal(item.doxpc_nmonto_nogravado).ToString("N2");
                    item.str_doxpc_nmonto_referencial_cif = Convert.ToDecimal(item.doxpc_nmonto_referencial_cif).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_gravado).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_mixto).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_nogravado).ToString("N2");
                    //item.str_doxpc_nmonto_isc = Convert.ToDecimal(item.doxpc_nmonto_isc).ToString("N2");
                    item.str_doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento).ToString("N2");
                    item.str_doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio).ToString("N4");

                    item.str_doxpc_sfecha_doc = (item.doxpc_sfecha_doc != null) ? Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yy") : "";
                    item.str_doxpc_sfecha_vencimiento_doc = (item.doxpc_sfecha_vencimiento_doc != null) ? Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).ToString("dd/MM/yy") : "";
                    item.str_doxpc_sfec_deposito_detraccion = (!string.IsNullOrWhiteSpace(item.doxpc_vnro_deposito_detraccion)) ? Convert.ToDateTime(item.doxpc_sfec_deposito_detraccion).ToString("dd/MM/yy") : null;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ERegistroCompras> ListarRegistroDeComprasNoDomic(int anio, int mes)
        {
            List<ERegistroCompras> lista = null;
            try
            {
                lista = new ContabilidadData().ListarRegistroDeComprasNoDomic(anio, mes);

                //dando formato para el reporte
                int nc;
                decimal tc;
                foreach (ERegistroCompras item in lista)
                {
                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor || item.tdocc_icod_tipo_doc == 119)
                        nc = -1;
                    else
                        nc = 1;

                    if (item.tablc_iid_tipo_moneda == Parametros.intTipoMonedaSoles)
                        tc = 1;
                    else
                        tc = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio);

                    item.doxpc_nmonto_destino_gravado = Math.Round((decimal)(item.doxpc_nmonto_destino_gravado * tc * nc), 2);
                    item.doxpc_nmonto_destino_mixto = Math.Round((decimal)(item.doxpc_nmonto_destino_mixto * tc * nc), 2);
                    item.doxpc_nmonto_destino_nogravado = Math.Round((decimal)(item.doxpc_nmonto_destino_nogravado * tc * nc), 2);
                    item.doxpc_nmonto_nogravado = Math.Round((decimal)(item.doxpc_nmonto_nogravado * tc * nc), 2);
                    item.doxpc_nmonto_referencial_cif = Math.Round((decimal)(item.doxpc_nmonto_referencial_cif * tc * nc), 2);
                    item.doxpc_nmonto_servicio_no_domic = Math.Round((decimal)(item.doxpc_nmonto_servicio_no_domic * tc * nc), 2);
                    item.valor_compra = Math.Round((decimal)(item.valor_compra * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_gravado = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_gravado * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_mixto = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_mixto * tc * nc), 2);
                    item.doxpc_nmonto_imp_destino_nogravado = Math.Round((decimal)(item.doxpc_nmonto_imp_destino_nogravado * tc * nc), 2);
                    //item.doxpc_nmonto_isc = Math.Round((decimal)(item.doxpc_nmonto_isc * tc * nc), 2);
                    item.doxpc_nmonto_total_documento = Math.Round((decimal)(item.doxpc_nmonto_total_documento * tc * nc), 2);
                    item.doxpc_nmonto_total_pagado = Math.Round((decimal)(item.doxpc_nmonto_total_pagado * tc * nc), 2);
                    item.doxpc_nmonto_total_saldo = Math.Round((decimal)(item.doxpc_nmonto_total_saldo * tc * nc), 2);

                    //montos reporte
                    item.str_doxpc_nmonto_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_destino_gravado).ToString("N2");
                    item.str_doxpc_nmonto_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_destino_mixto).ToString("N2");
                    item.str_doxpc_nmonto_destino_nogravado = Convert.ToDecimal(item.doxpc_nmonto_destino_nogravado).ToString("N2");
                    item.str_doxpc_nmonto_nogravado = Convert.ToDecimal(item.doxpc_nmonto_nogravado).ToString("N2");
                    item.str_doxpc_nmonto_referencial_cif = Convert.ToDecimal(item.doxpc_nmonto_referencial_cif).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_gravado).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_mixto).ToString("N2");
                    item.str_doxpc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.doxpc_nmonto_imp_destino_nogravado).ToString("N2");
                    //item.str_doxpc_nmonto_isc = Convert.ToDecimal(item.doxpc_nmonto_isc).ToString("N2");
                    item.str_doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento).ToString("N2");
                    item.str_doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio).ToString("N4");

                    item.str_doxpc_sfecha_doc = (item.doxpc_sfecha_doc != null) ? Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yy") : "";
                    item.str_doxpc_sfecha_vencimiento_doc = (item.doxpc_sfecha_vencimiento_doc != null) ? Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).ToString("dd/MM/yy") : "";
                    item.str_doxpc_sfec_deposito_detraccion = (!string.IsNullOrWhiteSpace(item.doxpc_vnro_deposito_detraccion)) ? Convert.ToDateTime(item.doxpc_sfec_deposito_detraccion).ToString("dd/MM/yy") : null;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Almacenes Contable
        public List<EAlmacenContable> listarAlmacenContable()
        {
            List<EAlmacenContable> lista = new List<EAlmacenContable>();
            try
            {
                lista = new ContabilidadData().listarAlmacenes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAlmacenContable(EAlmacenContable oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().insertarAlmacen(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAlmacenContable(EAlmacenContable oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarAlmacen(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAlmacenContable(EAlmacenContable oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarAlmacen(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Clasificacion de Tipo de Productos
        public List<EClasificacionProducto> listarClasificacionProducto()
        {
            List<EClasificacionProducto> lista = new List<EClasificacionProducto>();
            try
            {
                lista = new ContabilidadData().listarClasificacionProducto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarClasificacionProducto(EClasificacionProducto oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().insertarClasificacionProducto(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarClasificacionProducto(EClasificacionProducto oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarClasificacionProducto(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarClasificacionProducto(EClasificacionProducto oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarClasificacionProducto(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registro de Ventas
        public List<ERegistroVentas> ListarRegistroDeVentas(int anio, int mes)
        {
            List<ERegistroVentas> lista = null;
            try
            {
                lista = new ContabilidadData().ListarRegistroDeVentas(anio, mes);

                //dando formato para el reporte
                int nc;
                decimal tc;
                foreach (ERegistroVentas item in lista)
                {
                    item.str_doxcc_icod_correlativo = String.Format("{0:000000}", item.doxcc_icod_correlativo);
                    item.str_doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yy");
                    item.str_doxcc_sfecha_vencimiento_doc = Convert.ToDateTime(item.doxcc_sfecha_vencimiento_doc).ToString("dd/MM/yy");


                    item.cliec_vnombre_cliente_2 = item.cliec_vnombre_cliente;
                    item.tip_doc_cliente_2 = item.tip_doc_cliente;
                    item.num_doc_cliente_2 = item.num_doc_cliente;


                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                        nc = -1;
                    else
                        nc = 1;

                    if (item.tablc_iid_tipo_moneda == Parametros.intTipoMonedaSoles)
                        tc = 1;
                    else
                        tc = Convert.ToDecimal(item.doxcc_nmonto_tipo_cambio);

                    if (item.tablc_iid_situacion_documento == Parametros.intSitDocCobrarAnulado)
                    {

                        //item.doxcc_nmonto_tipo_cambio = 0;
                        item.doxcc_nporcentaje_igv = 0;
                        item.doxcc_nmonto_afecto = 0;
                        item.doxcc_nmonto_inafecto = 0;
                        item.valor_venta = 0;
                        item.doxcc_nmonto_impuesto = 0;
                        item.doxcc_nmonto_total = 0;
                        item.base_imp_ivap = 0;
                        item.valor_ex = 0;
                        item.doxcc_nmonto_ivap = 0;
                        item.cliec_vnombre_cliente = "***ANULADO***";
                        item.tip_doc_cliente = string.Empty;
                        item.num_doc_cliente = string.Empty;

                        item.rpt_tipo_cambio = string.Empty;
                        item.rpt_biog = string.Empty;
                        item.rpt_valor_ex = string.Empty;
                        item.rpt_valor_fact = string.Empty;
                        item.rpt_ivap = string.Empty;
                        item.rpt_valor_venta = string.Empty;
                        item.rpt_monto_igv = string.Empty;
                        item.rpt_precio_venta = string.Empty;



                    }
                    else
                    {
                        item.doxcc_nmonto_afecto = Math.Round((decimal)(item.doxcc_nmonto_afecto * tc * nc), 2);
                        item.doxcc_nmonto_inafecto = Math.Round((decimal)(item.doxcc_nmonto_inafecto * tc * nc), 2);
                        item.valor_venta = Math.Round((decimal)(item.valor_venta * tc * nc), 2);
                        item.doxcc_nmonto_impuesto = Math.Round((decimal)(item.doxcc_nmonto_impuesto * tc * nc), 2);
                        item.doxcc_nmonto_ivap = Math.Round((decimal)(item.doxcc_nmonto_ivap * tc * nc), 2);
                        item.doxcc_nmonto_total = Math.Round((decimal)(item.doxcc_nmonto_total * tc * nc), 2);

                        //if (item.doxcc_nmonto_ivap == 0)
                        //{
                        item.valor_ex = item.doxcc_nmonto_inafecto;
                        //item.base_imp_ivap = 0;
                        //}
                        //else
                        //{
                        item.base_imp_ivap = item.doxcc_base_imponible_ivap;
                        //item.valor_ex = 0;
                        //}
                        //item.valor_ex = 0;
                        //item.base_imp_ivap = 0;
                        //item.base_imp_ivap = 0;
                        //item.valor_ex = 0;

                        //montos reporte
                        item.rpt_tipo_cambio = string.Empty;
                        item.rpt_biog = Convert.ToDecimal(item.doxcc_nmonto_afecto).ToString("N2");

                        //item.rpt_valor_fact = 

                        item.rpt_ivap = Convert.ToDecimal(item.base_imp_ivap).ToString("N2");
                        item.rpt_valor_venta = Convert.ToDecimal(item.valor_venta).ToString("N2");
                        item.rpt_monto_igv = Convert.ToDecimal(item.doxcc_nmonto_impuesto).ToString("N2");
                        item.rpt_precio_venta = Convert.ToDecimal(item.doxcc_nmonto_total).ToString("N2");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Generación de Vouchers Contables
        #region Generación de las cabeceras de los Vouchers Contables
        public Tuple<List<EVoucherContableDet>, List<EVoucherContableDet>> addCtaAutomatica(EVoucherContableDet oBe, List<EVoucherContableDet> lstDet, List<EVoucherContableDet> lstDetalleGeneral, List<ECuentaContable> lstCuentas)
        {
            try
            {
                ECuentaContable CtaAux = new ECuentaContable();
                for (int x = 0; x < 2; x++)
                {
                    EVoucherContableDet obj = new EVoucherContableDet();

                    obj.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj.tdocc_icod_tipo_doc = oBe.tdocc_icod_tipo_doc;
                    obj.strTipNroDocumento = oBe.strTipNroDocumento;
                    obj.vcocd_numero_doc = oBe.vcocd_numero_doc;

                    if (x == 0)
                    {
                        obj.vcocd_nro_item_det = lstDet.Count() + 1;
                        obj.ctacc_icod_cuenta_contable = Convert.ToInt32(oBe.ctacc_icod_cuenta_debe_auto);
                        /*--------------------------------------------------*/
                        obj.vcocd_nmto_tot_debe_sol = oBe.vcocd_nmto_tot_debe_sol;
                        obj.vcocd_nmto_tot_haber_sol = oBe.vcocd_nmto_tot_haber_sol;
                        /*--------------------------------------------------*/
                        obj.vcocd_nmto_tot_debe_dol = oBe.vcocd_nmto_tot_debe_dol;
                        obj.vcocd_nmto_tot_haber_dol = oBe.vcocd_nmto_tot_haber_dol;
                        /*--------------------------------------------------*/
                        CtaAux = lstCuentas.Where(y => y.ctacc_icod_cuenta_contable == obj.ctacc_icod_cuenta_contable).ToList()[0];
                        /*-------------------------------------------------------------------*/
                        if (Convert.ToInt32(CtaAux.tablc_iid_tipo_analitica) > 0)
                        {
                            obj.tablc_iid_tipo_analitica = oBe.tablc_iid_tipo_analitica;
                            obj.anad_icod_analitica = oBe.anad_icod_analitica;
                        }
                        if (CtaAux.ctacc_iccosto)
                        {
                            obj.cecoc_icod_centro_costo = oBe.cecoc_icod_centro_costo;
                            obj.strCodCCosto = oBe.strCodCCosto;
                        }
                        /*-------------------------------------------------------------------*/
                    }
                    if (x == 1)
                    {
                        obj.vcocd_nro_item_det = lstDet.Count() + 1;
                        obj.ctacc_icod_cuenta_contable = Convert.ToInt32(oBe.ctacc_icod_cuenta_haber_auto);
                        /*--------------------------------------------------*/
                        obj.vcocd_nmto_tot_debe_sol = oBe.vcocd_nmto_tot_haber_sol;
                        obj.vcocd_nmto_tot_haber_sol = oBe.vcocd_nmto_tot_debe_sol;
                        /*--------------------------------------------------*/
                        obj.vcocd_nmto_tot_debe_dol = oBe.vcocd_nmto_tot_haber_dol;
                        obj.vcocd_nmto_tot_haber_dol = oBe.vcocd_nmto_tot_debe_dol;
                        /*--------------------------------------------------*/
                        CtaAux = lstCuentas.Where(y => y.ctacc_icod_cuenta_contable == obj.ctacc_icod_cuenta_contable).ToList()[0];
                        /*-------------------------------------------------------------------*/
                        if (Convert.ToInt32(CtaAux.tablc_iid_tipo_analitica) > 0)
                        {
                            obj.tablc_iid_tipo_analitica = oBe.tablc_iid_tipo_analitica;
                            obj.anad_icod_analitica = oBe.anad_icod_analitica;
                        }
                        if (CtaAux.ctacc_iccosto)
                        {
                            obj.cecoc_icod_centro_costo = oBe.cecoc_icod_centro_costo;
                            obj.strCodCCosto = oBe.strCodCCosto;
                        }
                        /*-------------------------------------------------------------------*/
                    }
                    obj.vcocd_vglosa_linea = oBe.vcocd_vglosa_linea.ToString();
                    obj.intTipoOperacion = 1;
                    obj.ctacc_iid_cuenta_contable_ref = oBe.ctacc_icod_cuenta_contable;
                    lstDet.Add(obj);
                    lstDetalleGeneral.Add(obj);
                }
                return new Tuple<List<EVoucherContableDet>, List<EVoucherContableDet>>(lstDet, lstDetalleGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersVentas(int intPeriodo, int intUsuario, string strPc)
        {
            List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
            List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
            TesoreriaData objTesoreriaData = new TesoreriaData();
            CuentasPorCobrarData objCuentasPorCobrarData = new CuentasPorCobrarData();
            VentasData objVentasData = new VentasData();

            var lstParamentros = new BContabilidad().listarParametroContable();
            var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            List<ECuentaContable> lstCtaAux = new List<ECuentaContable>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lstDXC = objCuentasPorCobrarData.listarDxcOrigenDifPlanilla(Parametros.intEjercicio, intPeriodo);
                    var lstDxcConPagos = objCuentasPorCobrarData.listarRelacionPagosVco(Parametros.intEjercicio, intPeriodo);
                    int MaxCorrelativo = new ContabilidadData().UltimoCorrelativoVoucherContableCab();

                    int strNroVco = 1;
                    #region Dxc...
                    lstDXC.ForEach(x =>
                    {
                        List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                        //ECliente objCliente = objVentasData.ListarCliente().Where(d => d.cliec_icod_cliente == Convert.ToInt32(x.cliec_icod_cliente)).ToList()[0];
                        #region Cabeceras de los Vouchers Contables
                        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                        obj_CompCab.anioc_iid_anio = Convert.ToInt32(Parametros.intEjercicio);
                        obj_CompCab.mesec_iid_mes = Convert.ToInt32(x.mesec_iid_mes);
                        obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);
                        obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(x.doxcc_sfecha_doc);
                        obj_CompCab.vcocc_glosa = x.doxcc_vobservaciones;
                        obj_CompCab.vNumero_documento = String.Format("{0} {1}", x.Abreviatura, x.doxcc_vnumero_doc);
                        obj_CompCab.vcocc_observacion = obj_CompCab.vcocc_glosa;
                        obj_CompCab.strNroVco = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                        obj_CompCab.tablc_iid_moneda = x.tablc_iid_tipo_moneda;
                        obj_CompCab.strTipoMoneda = (x.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                        obj_CompCab.intUsuario = intUsuario;
                        obj_CompCab.strPc = strPc;
                        obj_CompCab.vcocc_tipo_cambio = Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio);
                        obj_CompCab.tbl_origen = "DXC";
                        obj_CompCab.tbl_origen_icod = Convert.ToInt32(x.doxcc_icod_correlativo);

                        if (Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio) <= 0)
                            throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));

                        #endregion
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocFacturaVenta || x.tdocc_icod_tipo_doc == Parametros.intTipoDocBoletaVenta || x.tdocc_icod_tipo_doc == 110)
                        {
                            int Cont_detalle = 1;
                            #region Detalle 1 - Monto Total
                            EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                            obj_CompDet_01.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                            obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                            obj_CompDet_01.vcocd_numero_doc = x.doxcc_vnumero_doc;
                            obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                            obj_CompDet_01.ctacc_icod_cuenta_contable = x.intCtaTotal;

                            if (obj_CompDet_01.ctacc_icod_cuenta_contable == 0 && x.tablc_iid_tipo_moneda == 3)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), x.ClaseDocumento));

                            if (obj_CompDet_01.ctacc_icod_cuenta_contable == 0 && x.tablc_iid_tipo_moneda == 4)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), x.ClaseDocumento));

                            lstCtaAux = lstPlanCuentas.Where(y => y.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                            lstCtaAux.ForEach(Obe =>
                            {
                                obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                obj_CompDet_01.vcocd_vglosa_linea = Obe.ctacc_nombre_descripcion;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_01.anad_icod_analitica = x.anac_icod_analitica;
                                    obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_01.tablc_iid_tipo_analitica, x.anac_iid_analitica);
                                }
                            });


                            obj_CompDet_01.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? Convert.ToDecimal(x.doxcc_nmonto_total) : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_total) * Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                            obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_01.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? Convert.ToDecimal(x.doxcc_nmonto_total) : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_total) / Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                            obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_01.intTipoOperacion = 1;
                            obj_CompDet_01.vcocd_tipo_cambio = x.doxcc_nmonto_tipo_cambio;
                            obj_CompDet_01.doxcc_icod_correlativo = Convert.ToInt32(x.doxcc_icod_correlativo);
                            lstCompDetalle.Add(obj_CompDet_01);//-----------------------------------------------------------
                            lstDetGeneral.Add(obj_CompDet_01);
                            if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region Detalle 2 - Monto IGV
                            if (Convert.ToDecimal(x.doxcc_nmonto_impuesto) > 0)
                            {
                                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                                obj_CompDet_02.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                                obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                                obj_CompDet_02.vcocd_numero_doc = x.doxcc_vnumero_doc;
                                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);
                                obj_CompDet_02.ctacc_icod_cuenta_contable = x.intCtaIGV;

                                if (obj_CompDet_02.ctacc_icod_cuenta_contable == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable IGV de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), x.ClaseDocumento));

                                lstCtaAux = lstPlanCuentas.Where(y => y.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                                lstCtaAux.ForEach(Obe =>
                                {
                                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    obj_CompDet_02.vcocd_vglosa_linea = Obe.ctacc_nombre_descripcion;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_02.anad_icod_analitica = x.anac_icod_analitica;
                                        obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_02.tablc_iid_tipo_analitica, x.anac_iid_analitica);
                                    }
                                });

                                obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_02.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? x.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_impuesto) * Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_02.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? x.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_impuesto) / Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                obj_CompDet_02.intTipoOperacion = 1;
                                obj_CompDet_02.vcocd_tipo_cambio = x.doxcc_nmonto_tipo_cambio;
                                obj_CompDet_02.doxcc_icod_correlativo = Convert.ToInt32(x.doxcc_icod_correlativo);
                                lstCompDetalle.Add(obj_CompDet_02);//-----------------------------------------------------------
                                lstDetGeneral.Add(obj_CompDet_02);
                                if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                            }
                            #endregion
                            #region Detalle 3 - Monto IVAP
                            if (Convert.ToDecimal(x.doxcc_nmonto_ivap) > 0)
                            {
                                EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                                obj_CompDet_03.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                                obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_03.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                                obj_CompDet_03.vcocd_numero_doc = x.doxcc_vnumero_doc;
                                obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);
                                obj_CompDet_03.ctacc_icod_cuenta_contable = x.intCtaIVAP;

                                if (obj_CompDet_03.ctacc_icod_cuenta_contable == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable IGV de la Clase Doc. {0} {1:00}", obj_CompDet_03.strTipNroDocumento.Substring(0, 3), x.ClaseDocumento));

                                lstCtaAux = lstPlanCuentas.Where(y => y.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                                lstCtaAux.ForEach(Obe =>
                                {
                                    obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    obj_CompDet_03.vcocd_vglosa_linea = Obe.ctacc_nombre_descripcion;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_03.anad_icod_analitica = x.anac_icod_analitica;
                                        obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, x.anac_iid_analitica);
                                    }
                                });

                                obj_CompDet_03.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_03.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? x.doxcc_nmonto_ivap : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_ivap) * Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                obj_CompDet_03.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_03.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? x.doxcc_nmonto_ivap : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_ivap) / Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                obj_CompDet_03.intTipoOperacion = 1;
                                obj_CompDet_03.vcocd_tipo_cambio = x.doxcc_nmonto_tipo_cambio;
                                obj_CompDet_03.doxcc_icod_correlativo = Convert.ToInt32(x.doxcc_icod_correlativo);
                                lstCompDetalle.Add(obj_CompDet_03);//-----------------------------------------------------------
                                lstDetGeneral.Add(obj_CompDet_03);
                                if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                            }
                            #endregion
                            if (Convert.ToChar(x.doxcc_origen) != '2')//nota debito
                            {
                                #region Detalle - Detalle de la factura / Mercaderia

                                EVoucherContableDet obj_CompDet_m = new EVoucherContableDet();
                                obj_CompDet_m.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                                obj_CompDet_m.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_m.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                                obj_CompDet_m.vcocd_numero_doc = x.doxcc_vnumero_doc;
                                obj_CompDet_m.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_m.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_m.vcocd_numero_doc);
                                obj_CompDet_m.ctacc_icod_cuenta_contable = x.intCtaMercaderia;

                                if (obj_CompDet_m.ctacc_icod_cuenta_contable == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable GASTOS de la Clase Doc. {0} {1:00}", obj_CompDet_m.strTipNroDocumento.Substring(0, 3), x.ClaseDocumento));

                                lstCtaAux = lstPlanCuentas.Where(y => y.ctacc_icod_cuenta_contable == obj_CompDet_m.ctacc_icod_cuenta_contable).ToList();
                                lstCtaAux.ForEach(Obe =>
                                {
                                    obj_CompDet_m.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_m.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_m.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_m.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_m.anad_icod_analitica = x.anac_icod_analitica;
                                        obj_CompDet_m.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_m.tablc_iid_tipo_analitica, x.anac_iid_analitica);
                                    }
                                });
                                obj_CompDet_m.vcocd_vglosa_linea = x.doxcc_vobservaciones;
                                obj_CompDet_m.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_m.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? x.doxcc_nmonto_afecto + x.doxcc_base_imponible_ivap + x.doxcc_nmonto_inafecto : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_afecto + x.doxcc_base_imponible_ivap + x.doxcc_nmonto_inafecto) * Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                obj_CompDet_m.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_m.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? x.doxcc_nmonto_afecto + x.doxcc_base_imponible_ivap + x.doxcc_nmonto_inafecto : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_afecto + x.doxcc_base_imponible_ivap + x.doxcc_nmonto_inafecto) / Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                obj_CompDet_m.intTipoOperacion = 1;
                                obj_CompDet_m.vcocd_tipo_cambio = x.doxcc_nmonto_tipo_cambio;

                                lstCompDetalle.Add(obj_CompDet_m);//-----------------------------------------------------------
                                lstDetGeneral.Add(obj_CompDet_m);
                                if (obj_CompDet_m.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_m, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }


                                #endregion
                            }
                            else
                            {
                                #region Detalle
                                var lstDetalle = new BCuentasPorCobrar().BuscarDocumentoXCobrarCuentaContable(x.doxcc_icod_correlativo);
                                lstDetalle.ForEach(xd =>
                                {
                                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                                    obj_item_CompDet.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                                    obj_item_CompDet.vcocd_nro_item_det = Cont_detalle;
                                    Cont_detalle++;
                                    obj_item_CompDet.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                                    obj_item_CompDet.vcocd_numero_doc = x.doxcc_vnumero_doc;
                                    obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet.vcocd_numero_doc);
                                    obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(xd.ctacc_iid_cuenta_contable);
                                    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                                    Lista.ForEach(Obe =>
                                    {
                                        obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                        obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                        obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                        if (Obe.ctacc_iccosto)
                                            obj_item_CompDet.cecoc_icod_centro_costo = xd.cecoc_icod_centro_costo;
                                        List<ECentroCosto> ListaCC = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == xd.cecoc_icod_centro_costo).ToList();
                                        ListaCC.ForEach(Obe2 =>
                                        {
                                            obj_item_CompDet.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
                                        });
                                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                        {
                                            obj_item_CompDet.tablc_iid_tipo_analitica = Convert.ToInt32(xd.TipoAnalitica);
                                            obj_item_CompDet.anad_icod_analitica = x.anac_icod_analitica;
                                        }
                                    });

                                    obj_item_CompDet.vcocd_vglosa_linea = (xd.ccdcc_vglosa != null) ? xd.ccdcc_vglosa.ToUpper() : "";
                                    obj_item_CompDet.intTipoOperacion = 1;
                                    obj_item_CompDet.vcocd_tipo_cambio = x.doxcc_nmonto_tipo_cambio;
                                    if (xd.ccdcc_nmonto > 0)
                                    {
                                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? xd.ccdcc_nmonto : Math.Round(xd.ccdcc_nmonto * Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? xd.ccdcc_nmonto : Math.Round(xd.ccdcc_nmonto / Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                    }
                                    if (xd.ccdcc_nmonto < 0)
                                    {
                                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? (xd.ccdcc_nmonto * -1) : Math.Round((xd.ccdcc_nmonto * -1) * Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? (xd.ccdcc_nmonto * -1) : Math.Round((xd.ccdcc_nmonto * -1) / Convert.ToDecimal(x.doxcc_nmonto_tipo_cambio), 2);
                                    }
                                    obj_item_CompDet.doxcc_icod_correlativo = Convert.ToInt32(x.doxcc_icod_correlativo);

                                    lstCompDetalle.Add(obj_item_CompDet);
                                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/
                                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                                    {
                                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                        lstCompDetalle = tuple.Item1;
                                        lstDetGeneral = tuple.Item2;
                                    }
                                });
                                #endregion
                            }

                            #endregion
                            #region Totales del VCO
                            obj_CompCab.intMovimientos = lstCompDetalle.Count;
                            obj_CompCab.vcocc_nmto_tot_debe_sol = lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(w => Convert.ToDecimal(w.vcocd_nmto_tot_debe_sol));
                            obj_CompCab.vcocc_nmto_tot_haber_sol = lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(w => Convert.ToDecimal(w.vcocd_nmto_tot_haber_sol));
                            obj_CompCab.vcocc_nmto_tot_debe_dol = lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(w => Convert.ToDecimal(w.vcocd_nmto_tot_debe_dol));
                            obj_CompCab.vcocc_nmto_tot_haber_dol = lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(w => Convert.ToDecimal(w.vcocd_nmto_tot_haber_dol));
                            #endregion
                            #region Sit. del VCO
                            if (lstCompDetalle.Count > 0)
                            {
                                if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
                                    obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
                                {
                                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                                    obj_CompCab.strVcoSituacion = "Cuadrado";
                                }
                                else
                                {
                                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                                    obj_CompCab.strVcoSituacion = "No Cuadrado";
                                }
                            }
                            else
                            {
                                obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                                obj_CompCab.strVcoSituacion = "Sin Detalle";
                            }

                            #endregion
                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            strNroVco++;
                        }
                        else if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                        {
                            #region Nota de Crédito
                            lstDetGeneral = getDetVoucherDxcNotaCredito(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            strNroVco++;
                            #endregion
                        }
                        else if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocLetraCliente)
                        {
                            #region Letra Por Cobrar
                            var oBeLxc = objCuentasPorCobrarData.getLetraPorCobrarCab(x.docxc_icod_documento);//Convert.ToInt32(x.docxc_icod_documento)
                            if (oBeLxc.Count == 0)
                                throw new ArgumentException(String.Format("<<Error...>> No se han obtenido los datos de la LXP <<{0}>> correctamente!", x.doxcc_vnumero_doc));

                            lstDetGeneral = getDetVoucherDxpLxc(obj_CompCab, x, oBeLxc[0], lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            strNroVco++;
                            #endregion
                        }
                        else if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocGarantiaClientes)
                        {
                            #region Garantia Clientes
                            var oBeLxc = objCuentasPorCobrarData.getGarantiaClientesCab(x.docxc_icod_documento);//Convert.ToInt32(x.docxc_icod_documento)
                            if (oBeLxc.Count == 0)
                                throw new ArgumentException(String.Format("<<Error...>> No se han obtenido los datos de la GC <<{0}>> correctamente!", x.doxcc_vnumero_doc));


                            lstDetGeneral = getDetVoucherDxpGC(obj_CompCab, x, oBeLxc[0], lstPlanCuentas, lstDetGeneral, lstParamentros);

                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            strNroVco++;
                            #endregion
                        }
                        MaxCorrelativo++;//correlativo 
                    });
                    #endregion
                    #region Pagos...
                    lstDxcConPagos.ForEach(x =>
                    {
                        var lstPagosDirectos = objCuentasPorCobrarData.ListarPagoDirectoDocumentoXCobrar(x.doxcc_icod_correlativo).Where(ob => ob.pdxcc_vorigen == "D").ToList();
                        var lstPagocNc = objCuentasPorCobrarData.ListarPagoNotaCredito(0, x.doxcc_icod_correlativo, 0, Parametros.intEjercicio).Where(ob => ob.ncpac_iorigen != "B").ToList();

                        lstPagosDirectos.ForEach(y =>
                        {
                            #region Cabecera del Voucher (Pgo. Directo)...
                            EVoucherContableCab obj_CompCab1 = new EVoucherContableCab();
                            obj_CompCab1.anioc_iid_anio = Convert.ToDateTime(y.pdxcc_sfecha_cobro).Year;
                            obj_CompCab1.mesec_iid_mes = Convert.ToDateTime(y.pdxcc_sfecha_cobro).Month;
                            obj_CompCab1.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCab1.vcocc_fecha_vcontable = Convert.ToDateTime(y.pdxcc_sfecha_cobro);
                            obj_CompCab1.vcocc_glosa = (y.pdxcc_vobservacion == "" || y.pdxcc_vobservacion == null) ? "PAGO DIRECTO DE DOCUMENTO" : y.pdxcc_vobservacion.ToUpper();
                            obj_CompCab1.vcocc_observacion = obj_CompCab1.vcocc_glosa;
                            obj_CompCab1.vNumero_documento = String.Format("{0} {1}", y.Abreviatura, y.pdxcc_vnumero_doc);
                            obj_CompCab1.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCab1.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCab1.tablc_iid_moneda = y.tablc_iid_tipo_moneda;
                            obj_CompCab1.strTipoMoneda = (y.id_tipo_moneda_dxc == 3) ? "S/." : "US$";
                            obj_CompCab1.intUsuario = intUsuario;
                            obj_CompCab1.strPc = strPc;
                            obj_CompCab1.vcocc_tipo_cambio = Convert.ToDecimal(y.pdxcc_nmonto_tipo_cambio);
                            obj_CompCab1.tbl_origen = "DXC-PGO-DIR";
                            obj_CompCab1.tbl_origen_icod = Convert.ToInt32(y.pdxcc_icod_correlativo);
                            obj_CompCab1.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);

                            if (Convert.ToDecimal(y.pdxcc_nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab1.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            #region Detalle
                            lstDetGeneral = getDetVoucherDxcPagoDirecto(obj_CompCab1, y, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCab1.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab1.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab1);
                            strNroVco++;
                            MaxCorrelativo++;//correlativo 
                            #endregion
                        });
                        lstPagocNc.ForEach(y =>
                        {
                            #region Cabecera del Voucher (Cje. con Nc)...
                            EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                            obj_CompCab.anioc_iid_anio = Convert.ToDateTime(y.ncpac_sfecha_pago).Year;
                            obj_CompCab.mesec_iid_mes = Convert.ToDateTime(y.ncpac_sfecha_pago).Month;
                            obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(y.ncpac_sfecha_pago);
                            obj_CompCab.vcocc_glosa = (y.ncpac_vdescripcion == "" && y.ncpac_vdescripcion == null) ? "APLICACIÓN CON NOTA DE CRÉDITO" : y.ncpac_vdescripcion.ToUpper();
                            obj_CompCab.vcocc_observacion = obj_CompCab.vcocc_glosa;
                            obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCab.tablc_iid_moneda = y.tablc_iid_tipo_moneda;
                            obj_CompCab.strTipoMoneda = (y.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                            obj_CompCab.intUsuario = intUsuario;
                            obj_CompCab.strPc = strPc;
                            obj_CompCab.vcocc_tipo_cambio = Convert.ToDecimal(y.ncpac_nmonto_tipo_cambio);
                            obj_CompCab.tbl_origen = "DXC-CJ-NC";
                            obj_CompCab.tbl_origen_icod = Convert.ToInt32(y.ncpac_icod_correlativo);
                            obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);
                            if (Convert.ToDecimal(y.ncpac_nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            #region Detalle
                            lstDetGeneral = getDetVoucherDxcCanjeNotaCredito(obj_CompCab, y, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            MaxCorrelativo++;
                            strNroVco++;
                            #endregion
                        });

                    });
                    #region Pago Adelanto
                    var lstPagosAdelantos = objCuentasPorCobrarData.ListarAdelantoPago2(intPeriodo).Where(OB => OB.adpac_iorigen != "B").ToList();
                    lstPagosAdelantos.ForEach(y =>
                    {
                        #region Cabecera del Voucher (Cje. con Nc)...
                        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                        obj_CompCab.anioc_iid_anio = Convert.ToDateTime(y.adpac_sfecha_pago).Year;
                        obj_CompCab.mesec_iid_mes = Convert.ToDateTime(y.adpac_sfecha_pago).Month;
                        obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                        obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(y.adpac_sfecha_pago);
                        obj_CompCab.vcocc_glosa = (y.adpac_vdescripcion == "" && y.adpac_vdescripcion == null) ? "APLICACIÓN CON NOTA DE CRÉDITO" : y.adpac_vdescripcion.ToUpper();
                        obj_CompCab.vcocc_observacion = obj_CompCab.vcocc_glosa;
                        obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                        obj_CompCab.tablc_iid_moneda = y.tablc_iid_tipo_moneda;
                        obj_CompCab.strTipoMoneda = (y.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                        obj_CompCab.intUsuario = intUsuario;
                        obj_CompCab.strPc = strPc;
                        obj_CompCab.vcocc_tipo_cambio = Convert.ToDecimal(y.adpac_nmonto_tipo_cambio);
                        obj_CompCab.tbl_origen = "DXC-CJ-AD";
                        obj_CompCab.tbl_origen_icod = Convert.ToInt32(y.adpac_icod_correlativo);
                        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);
                        if (Convert.ToDecimal(y.adpac_nmonto_tipo_cambio) <= 0)
                            throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));
                        #endregion
                        #region Detalle
                        lstDetGeneral = getDetVoucherDxcCanjeAdelanto(obj_CompCab, y, lstPlanCuentas, lstDetGeneral, lstParamentros);
                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                        lstCabeceras.Add(obj_CompCab);
                        strNroVco++;
                        MaxCorrelativo++;
                        #endregion
                    });
                    #endregion
                    #endregion
                    #region Planilla Venta Diaria...
                    var lstPlanillas = objVentasData.listarPlanillaCobranzaContabilizacionCab(Parametros.intEjercicio, intPeriodo).ToList();
                    lstPlanillas.ForEach(x =>
                    {
                        #region Se crea el voucher en soles si hay moviemiento es SOLES
                        if (Convert.ToInt32(x.plnc_nmonto_importe) > 0 && Convert.ToInt32(x.tablc_iid_tipo_moneda) == 3 && Convert.ToInt32(x.plnc_nmonto_pagado) > 0)
                        {
                            #region Cabecera
                            EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                            obj_CompCab.anioc_iid_anio = x.plnc_sfecha_planilla.Year;
                            obj_CompCab.mesec_iid_mes = x.plnc_sfecha_planilla.Month;
                            obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);
                            obj_CompCab.vcocc_fecha_vcontable = x.plnc_sfecha_planilla;
                            obj_CompCab.vcocc_glosa = "PAGOS EN PLANILLA N° " + x.plnc_vnumero_planilla;
                            obj_CompCab.vcocc_observacion = x.plnc_vobservaciones;
                            obj_CompCab.strNroVco = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCab.tablc_iid_moneda = Parametros.intTipoMonedaSoles;
                            obj_CompCab.strTipoMoneda = "S/.";
                            obj_CompCab.intUsuario = intUsuario;
                            obj_CompCab.strPc = strPc;
                            obj_CompCab.vcocc_tipo_cambio = Convert.ToDecimal(x.dcmlTipoCambioSol);
                            obj_CompCab.tbl_origen = "PVD";
                            obj_CompCab.tbl_origen_icod = x.plnc_icod_planilla;
                            if (Convert.ToDecimal(x.dcmlTipoCambioSol) == 0)
                            {
                                x.dcmlTipoCambioSol = objContabilidadData.getTipoCambioPorFecha(x.plnc_sfecha_planilla);
                            }
                            if (Convert.ToDecimal(x.dcmlTipoCambioSol) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));

                            #endregion
                            lstDetGeneral = getDetVoucherPlanillaVentaSoles(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            MaxCorrelativo++;
                            strNroVco++;
                        }
                        #endregion
                        #region Se crea el voucher en dolares si hay moviemiento es DOLARES
                        if (Convert.ToInt32(x.plnc_nmonto_importe) > 0 && Convert.ToInt32(x.tablc_iid_tipo_moneda) == 4 && Convert.ToInt32(x.plnc_nmonto_pagado) > 0)
                        {
                            #region Cabecera
                            EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                            obj_CompCab.anioc_iid_anio = x.plnc_sfecha_planilla.Year;
                            obj_CompCab.mesec_iid_mes = x.plnc_sfecha_planilla.Month;
                            obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);
                            obj_CompCab.vcocc_fecha_vcontable = x.plnc_sfecha_planilla;
                            obj_CompCab.vcocc_glosa = "PAGOS EN PLANILLA N° " + x.plnc_vnumero_planilla;
                            obj_CompCab.vcocc_observacion = x.plnc_vobservaciones;
                            obj_CompCab.strNroVco = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCab.tablc_iid_moneda = Parametros.intTipoMonedaDolares;
                            obj_CompCab.strTipoMoneda = "US$";
                            obj_CompCab.intUsuario = intUsuario;
                            obj_CompCab.strPc = strPc;
                            obj_CompCab.vcocc_tipo_cambio = Convert.ToDecimal(x.dcmlTipoCambioDol);
                            obj_CompCab.tbl_origen = "PVD";
                            obj_CompCab.tbl_origen_icod = x.plnc_icod_planilla;
                            if (Convert.ToDecimal(x.dcmlTipoCambioSol) == 0)
                            {
                                obj_CompCab.vcocc_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.plnc_sfecha_planilla);
                            }

                            if (Convert.ToDecimal(x.dcmlTipoCambioSol) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));

                            #endregion
                            lstDetGeneral = getDetVoucherPlanillaVentaDolares(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            MaxCorrelativo++;
                            strNroVco++;
                        }
                        #endregion
                    });
                    #endregion
                    #region Retenciones...
                    var lstRetencion = objVentasData.listarRetencionCab(Parametros.intEjercicio, intPeriodo);
                    lstRetencion.ForEach(x =>
                    {
                        #region Cabeceras de los Vouchers Contables
                        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                        obj_CompCab.anioc_iid_anio = Convert.ToInt32(x.anioc_iid_anio);
                        obj_CompCab.mesec_iid_mes = Convert.ToInt32(x.mesec_iid_mes);
                        obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);
                        obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(x.retc_sfec_comprob_reten);
                        obj_CompCab.vcocc_glosa = String.Format("RETENCION {0}", x.retc_vnumero_comprob_reten);
                        obj_CompCab.vNumero_documento = String.Format("{0} {1}", "RET", x.retc_vnumero_comprob_reten);
                        obj_CompCab.vcocc_observacion = String.Format("RETENCION {0}", x.retc_vnumero_comprob_reten);
                        obj_CompCab.strNroVco = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                        obj_CompCab.tablc_iid_moneda = x.tablc_iid_moneda;
                        obj_CompCab.strTipoMoneda = (x.tablc_iid_moneda == 3) ? "S/." : "US$";
                        obj_CompCab.intUsuario = intUsuario;
                        obj_CompCab.strPc = strPc;
                        obj_CompCab.vcocc_tipo_cambio = Convert.ToDecimal(x.retc_nmto_tipo_cambio);
                        obj_CompCab.tbl_origen = "RET";
                        obj_CompCab.tbl_origen_icod = Convert.ToInt32(x.retc_icod_comprobante_retencion);

                        if (Convert.ToDecimal(x.retc_nmto_tipo_cambio) <= 0)
                            throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));

                        #endregion
                        lstDetGeneral = getDetVoucherRetencion(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                        lstCabeceras.Add(obj_CompCab);
                        MaxCorrelativo++;
                        strNroVco++;
                    });
                    #endregion


                    tx.Complete();
                }
                return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        int strNroVcoIngresos = 1;
        int strNroVcoEgresos = 1;

        public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersBancos(int intPeriodo, int intUsuario, string strPc)
        {
            strNroVcoIngresos = 1;
            strNroVcoEgresos = 1;
            List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
            List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
            TesoreriaData objTesoreriaData = new TesoreriaData();

            var lstParamentros = new BContabilidad().listarParametroContable();
            var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

            int MaxCorrelativo = new ContabilidadData().UltimoCorrelativoVoucherContableCab();

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lstBancoCuentas = objTesoreriaData.listarBancoCuentas(null);
                    lstBancoCuentas.ForEach(x =>
                    {
                        var lstMovimientos = objTesoreriaData.ListarLibroBancosVCO(Parametros.intEjercicio, intPeriodo, x.bcod_icod_banco_cuenta).Where(y => y.iid_situacion_movimiento_banco != 2).ToList();
                        lstMovimientos = lstMovimientos.ToList(); ;//provisional
                        lstMovimientos.ForEach(y =>
                        {
                            //if (y.ii_tipo_doc == 69)
                            //{
                            //    int i = 0;
                            // }
                            #region Cabecera de los vouchers contables
                            EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                            obj_CompCab.anioc_iid_anio = y.iid_anio;
                            obj_CompCab.mesec_iid_mes = y.iid_mes;
                            obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(y.dfecha_movimiento);
                            obj_CompCab.vcocc_glosa = y.vglosa;
                            obj_CompCab.vcocc_observacion = y.vglosa;
                            obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO) 

                            obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCab.tablc_iid_moneda = y.iid_tipo_moneda;
                            obj_CompCab.strTipoMoneda = (y.iid_tipo_moneda == 3) ? "S/." : "US$";
                            obj_CompCab.intUsuario = y.iusuario_crea;
                            obj_CompCab.strPc = y.vpc_crea;
                            obj_CompCab.vcocc_tipo_cambio = y.nmonto_tipo_cambio;
                            obj_CompCab.tbl_origen = "BANCOS";
                            obj_CompCab.tbl_origen_icod = y.icod_correlativo;
                            MaxCorrelativo++;

                            if (Convert.ToDecimal(y.nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            #region Detalle
                            switch (y.iid_motivo_mov_banco)
                            {
                                case 106: // Parametros.intMotivoCuentasPorPagar
                                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);
                                    lstDetGeneral = getDetVoucherDxpDxc(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                    if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                        strNroVcoEgresos++;
                                    }
                                    if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                        strNroVcoIngresos++;
                                    }

                                    lstCabeceras.Add(obj_CompCab);
                                    break;
                                case 107: // Parametros.intMotivoCuentasPorCobrar
                                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_ingbco);
                                    lstDetGeneral = getDetVoucherDxpDxc(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                    if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                        strNroVcoEgresos++;
                                    }
                                    if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                        strNroVcoIngresos++;
                                    }
                                    lstCabeceras.Add(obj_CompCab);
                                    break;
                                case 108: // Parametros.intMotivoAdelantosProveedores
                                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);
                                    lstDetGeneral = getDetVoucherADP(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                    if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                        strNroVcoEgresos++;
                                    }
                                    if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                        strNroVcoIngresos++;
                                    }
                                    lstCabeceras.Add(obj_CompCab);
                                    break;
                                case 109: // Parametros.intMotivoAdelantosClientes
                                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_ingbco);
                                    lstDetGeneral = getDetVoucherADC(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                    if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                        strNroVcoEgresos++;
                                    }
                                    if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                        strNroVcoIngresos++;
                                    }
                                    lstCabeceras.Add(obj_CompCab);
                                    break;
                                case 110: // Parametros.intMotivoVarios
                                    switch (y.cflag_tipo_movimiento)
                                    {
                                        case "1":
                                            obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_ingbco);
                                            lstDetGeneral = getDetVoucherVarios(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                            if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                            {
                                                obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                                strNroVcoEgresos++;
                                            }
                                            if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                            {
                                                obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                                strNroVcoIngresos++;
                                            }
                                            lstCabeceras.Add(obj_CompCab);
                                            break;
                                        case "0":
                                            obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);
                                            lstDetGeneral = getDetVoucherVarios(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                            if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                            {
                                                obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                                strNroVcoEgresos++;
                                            }
                                            if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                            {
                                                obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                                strNroVcoIngresos++;
                                            }
                                            lstCabeceras.Add(obj_CompCab);
                                            break;
                                    }
                                    break;
                                case 111: // Parametros.intMotivoPagoAdelantadoProveedores
                                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_ingbco);
                                    var lstADP = new BTesoreria().ListarEntidadFinancieraDetalle(y.icod_correlativo);
                                    lstDetGeneral = getDetVoucherADPP(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros, lstADP);
                                    if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                        strNroVcoEgresos++;
                                    }
                                    if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                        strNroVcoIngresos++;
                                    }
                                    lstCabeceras.Add(obj_CompCab);
                                    break;
                                case 112: // Parametros.intMotivoPagoAdelantadoClientes
                                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);
                                    var lstADC = new BTesoreria().ListarEntidadFinancieraDetalle(y.icod_correlativo);
                                    lstDetGeneral = getDetVoucherADCC(obj_CompCab, y, x, lstPlanCuentas, lstDetGeneral, lstParamentros, lstADC);
                                    if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                        strNroVcoEgresos++;
                                    }
                                    if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                    {
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                        strNroVcoIngresos++;
                                    }
                                    lstCabeceras.Add(obj_CompCab);
                                    break;
                                case 182: // Parametros.intMotivoTransferenciaCuentas
                                    if (y.cflag_tipo_movimiento == "0")
                                    {
                                        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);
                                        if (Convert.ToInt32(y.id_transferencia) == 0)
                                            throw new ArgumentException(String.Format("<<Error...>> No se econtró id de la Tranf. entre cuentas del mov. {0} {1}", y.TipoDocAbreviado, y.vnro_documento));
                                        int intTranfCtaBancaria = objTesoreriaData.getBancoCuenta(Convert.ToInt32(y.id_transferencia));
                                        var oBeTranfCta = lstBancoCuentas.Where(z => z.bcod_icod_banco_cuenta == intTranfCtaBancaria).ToList()[0];
                                        lstDetGeneral = getDetVoucherTransferencia(obj_CompCab, y, x, oBeTranfCta, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                        if (obj_CompCab.subdi_icod_subdiario == 8)//EGRESO BANCOS
                                        {
                                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoEgresos);//ESTO ES PROVISIONAL
                                            strNroVcoEgresos++;
                                        }
                                        if (obj_CompCab.subdi_icod_subdiario == 9)//INGRESO BANCO
                                        {
                                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVcoIngresos);//ESTO ES PROVISIONAL
                                            strNroVcoIngresos++;
                                        }
                                        lstCabeceras.Add(obj_CompCab);
                                    }
                                    break;
                            }

                            #endregion
                        });
                    });
                    tx.Complete();
                }
                return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersCompras(int intPeriodo, int intUsuario, string strPc)
        {
            try
            {
                List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
                List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
                TesoreriaData objTesoreriaData = new TesoreriaData();
                CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();

                var lstParamentros = new BContabilidad().listarParametroContable();
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

                int MaxCorrelativo = new ContabilidadData().UltimoCorrelativoVoucherContableCab();

                int strNroVco = 1;

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    EDocPorPagar objE_DocPorPagar = new EDocPorPagar() { anio = Parametros.intEjercicio, mesec_iid_mes = intPeriodo };
                    var lstDocPorPagar = objCuentasPorPagarData.listarDocPorPagar(objE_DocPorPagar);
                    lstDocPorPagar.ForEach(x =>
                    {
                        #region Cabeceras de los Vouchers Contables
                        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                        obj_CompCab.anioc_iid_anio = x.anio;
                        obj_CompCab.mesec_iid_mes = x.mesec_iid_mes;
                        obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                        obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(x.doxpc_sfecha_doc);
                        obj_CompCab.vcocc_glosa = (x.doxpc_vdescrip_transaccion != null) ? x.doxpc_vdescrip_transaccion.ToUpper() : null;
                        obj_CompCab.vcocc_observacion = (x.doxpc_vdescrip_transaccion != null) ? x.doxpc_vdescrip_transaccion.ToUpper() : null;
                        obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                        obj_CompCab.tablc_iid_moneda = x.tablc_iid_tipo_moneda;
                        obj_CompCab.strTipoMoneda = (x.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                        obj_CompCab.intUsuario = intUsuario;
                        obj_CompCab.strPc = strPc;
                        obj_CompCab.vcocc_tipo_cambio = x.doxpc_nmonto_tipo_cambio;
                        obj_CompCab.tbl_origen = "DXP";
                        obj_CompCab.tbl_origen_icod = Convert.ToInt32(x.doxpc_icod_correlativo);
                        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxpag);
                        obj_CompCab.doxpc_viid_correlativo = x.doxpc_viid_correlativo;

                        MaxCorrelativo++;
                        if (Convert.ToDecimal(x.doxpc_nmonto_tipo_cambio) <= 0)
                            throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));

                        #endregion
                        #region Detalle de los Vouchers Contables
                        switch (x.tdocc_icod_tipo_doc)
                        {
                            case 34: //Parametros.intTipoDocLetraProveedor
                                break;
                            case 113: //Parametros.intTipoDocGarantiaProveedor
                                break;
                            case 121: //Parametros.intTipoDocGarantiaProveedor
                                break;
                            case 86://nota de credito proveedor
                                {
                                    #region Nota de Crédito
                                    lstDetGeneral = getDetVoucherDxpNcp(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                    obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                                    lstCabeceras.Add(obj_CompCab);
                                    strNroVco++;
                                    #endregion
                                }
                                break;
                            case 119://nota de credito importacion
                                {
                                    #region Nota de Crédito Importacion
                                    lstDetGeneral = getDetVoucherDxpNcp(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                    obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                                    lstCabeceras.Add(obj_CompCab);
                                    strNroVco++;
                                    #endregion
                                }
                                break;
                            //case 98://percepcion compra
                            //    {
                            //        lstDetGeneral = getDetVoucherPercepcion(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            //        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            //        lstCabeceras.Add(obj_CompCab);
                            //        strNroVco++;
                            //        MaxCorrelativo++;
                            //    }
                            //    break;
                            default:
                                switch (x.doxpc_origen)
                                {
                                    case "2": //Parametros.origenManual
                                        lstDetGeneral = getDetVoucherDxpManual(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                                        lstCabeceras.Add(obj_CompCab);
                                        strNroVco++;
                                        break;
                                    case "9": //Parametros.origenAlmacenCompra
                                        lstDetGeneral = getDetVoucherDxpManual(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                                        lstCabeceras.Add(obj_CompCab);
                                        strNroVco++;
                                        break;
                                    case "6"://"8": //Parametros.origenComprasFac
                                        lstDetGeneral = getDetVoucherDxpManual(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                                        lstCabeceras.Add(obj_CompCab);
                                        strNroVco++;
                                        break;
                                }

                                break;
                        }
                        #endregion

                        #region Letra Por Pagar
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocLetraProveedor)
                        {
                            var oBeLxp = objCuentasPorPagarData.getLetraPorPagarCab(x.doxpc_icod_documento);
                            if (oBeLxp.Count == 0)
                                throw new ArgumentException(String.Format("<<Error...>> No se han obtenido los datos de la LXP <<{0}>> correctamente!", x.doxpc_vnumero_doc));
                            #region Cabecera del Voucher(Lxp...)
                            EVoucherContableCab obj_CompCabLxp = new EVoucherContableCab();
                            obj_CompCabLxp.anioc_iid_anio = Parametros.intEjercicio;
                            obj_CompCabLxp.mesec_iid_mes = oBeLxp[0].mesec_iid_mes;
                            obj_CompCabLxp.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCabLxp.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxpag);
                            obj_CompCabLxp.vcocc_fecha_vcontable = oBeLxp[0].lexpc_sfecha_letra;
                            obj_CompCabLxp.vcocc_glosa = (oBeLxp[0].lexpc_vobservaciones == null || oBeLxp[0].lexpc_vobservaciones == "") ? "APLICACIÓN DE LETRA" : oBeLxp[0].lexpc_vobservaciones.ToUpper();
                            obj_CompCabLxp.vcocc_observacion = obj_CompCabLxp.vcocc_glosa;
                            obj_CompCabLxp.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCabLxp.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCabLxp.tablc_iid_moneda = oBeLxp[0].tablc_iid_tipo_moneda;
                            obj_CompCabLxp.strTipoMoneda = (oBeLxp[0].tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                            obj_CompCabLxp.intUsuario = intUsuario;
                            obj_CompCabLxp.strPc = strPc;
                            obj_CompCabLxp.vcocc_tipo_cambio = oBeLxp[0].lexpc_nmonto_tipo_cambio;
                            obj_CompCabLxp.tbl_origen = "DXP-LXP";
                            obj_CompCabLxp.tbl_origen_icod = Convert.ToInt32(x.doxpc_icod_correlativo);
                            if (Convert.ToDecimal(oBeLxp[0].lexpc_nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCabLxp.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            lstDetGeneral = getDetVoucherDxpLxp(obj_CompCabLxp, x, oBeLxp[0], lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCabLxp.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCabLxp.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCabLxp);
                            MaxCorrelativo++;
                            strNroVco++;

                        }
                        #endregion
                        #region Garantia Proveedores
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocGarantiaProveedor)
                        {
                            var oBeLxp = objCuentasPorPagarData.getGarantiaPorPagarCab(x.doxpc_icod_documento);
                            if (oBeLxp.Count == 0)
                                throw new ArgumentException(String.Format("<<Error...>> No se han obtenido los datos de la GAP <<{0}>> correctamente!", x.doxpc_vnumero_doc));
                            #region Cabecera del Voucher(Lxp...)
                            EVoucherContableCab obj_CompCabLxp = new EVoucherContableCab();
                            obj_CompCabLxp.anioc_iid_anio = Parametros.intEjercicio;
                            obj_CompCabLxp.mesec_iid_mes = oBeLxp[0].garp_sfecha_garantia.Month;
                            obj_CompCabLxp.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCabLxp.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxpag);
                            obj_CompCabLxp.vcocc_fecha_vcontable = oBeLxp[0].garp_sfecha_garantia;
                            obj_CompCabLxp.vcocc_glosa = "GP N°" + oBeLxp[0].garap_vnumero_garantia;
                            obj_CompCabLxp.vcocc_observacion = obj_CompCabLxp.vcocc_glosa;
                            obj_CompCabLxp.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCabLxp.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCabLxp.tablc_iid_moneda = oBeLxp[0].tablc_iid_tipo_moneda;
                            obj_CompCabLxp.strTipoMoneda = (oBeLxp[0].tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                            obj_CompCabLxp.intUsuario = intUsuario;
                            obj_CompCabLxp.strPc = strPc;
                            obj_CompCabLxp.vcocc_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBeLxp[0].garp_sfecha_garantia);
                            obj_CompCabLxp.tbl_origen = "DXP-GXP";
                            obj_CompCabLxp.tbl_origen_icod = Convert.ToInt32(x.doxpc_icod_correlativo);
                            if (Convert.ToDecimal(obj_CompCabLxp.vcocc_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCabLxp.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            lstDetGeneral = getDetVoucherDxpGxp(obj_CompCabLxp, x, oBeLxp[0], lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCabLxp.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCabLxp.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCabLxp);
                            MaxCorrelativo++;
                            strNroVco++;
                        }

                        if (x.tdocc_icod_tipo_doc == 121)
                        {
                        }

                        #endregion

                    });
                    #region Canjes con Notas de Crédito (En caso existan...)
                    //var lstPagosConNc = objCuentasPorPagarData.listarDxpPagosNc(x.doxpc_icod_correlativo, 0, Parametros.intEjercicio);
                    var lstPagosConNc = objCuentasPorPagarData.listarDxpPagosNc2(intPeriodo, Parametros.intEjercicio).Where(e => e.ncpap_iorigen == "N").ToList();

                    if (lstPagosConNc.Count > 0)
                    {
                        lstPagosConNc.ForEach(y =>
                        {
                            #region Cabecera del Voucher (Canje...)
                            EVoucherContableCab obj_CompCabNc = new EVoucherContableCab();
                            obj_CompCabNc.anioc_iid_anio = Convert.ToDateTime(y.ncpap_sfecha_pago).Year;
                            obj_CompCabNc.mesec_iid_mes = Convert.ToDateTime(y.ncpap_sfecha_pago).Month;
                            obj_CompCabNc.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCabNc.vcocc_fecha_vcontable = Convert.ToDateTime(y.ncpap_sfecha_pago);
                            obj_CompCabNc.vcocc_glosa = (y.ncpap_vdescripcion == "" || y.ncpap_vdescripcion == null) ? "CANJE CON N/C" : y.ncpap_vdescripcion.ToUpper();
                            obj_CompCabNc.vcocc_observacion = obj_CompCabNc.vcocc_glosa;
                            obj_CompCabNc.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCabNc.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCabNc.tablc_iid_moneda = y.iid_moneda_nota_credito;
                            obj_CompCabNc.strTipoMoneda = (y.iid_moneda_nota_credito == 3) ? "S/." : "US$";
                            obj_CompCabNc.intUsuario = y.intUsuario;
                            obj_CompCabNc.strPc = y.strPc;
                            obj_CompCabNc.vcocc_tipo_cambio = Convert.ToDecimal(y.ncpap_nmonto_tipo_cambio);
                            obj_CompCabNc.tbl_origen = "DXP-CJ-NC";
                            obj_CompCabNc.tbl_origen_icod = Convert.ToInt32(y.ncpap_icod_correlativo);
                            obj_CompCabNc.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxpag);
                            if (Convert.ToDecimal(y.ncpap_nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>> <<Cje. Nota Crédito>>", obj_CompCabNc.vcocc_fecha_vcontable.ToShortDateString()));

                            #endregion
                            lstDetGeneral = getDetVoucherDxpCanjeNotaCredito(obj_CompCabNc, y, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCabNc.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCabNc.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCabNc);
                            MaxCorrelativo++;
                            strNroVco++;
                        });
                    }
                    #endregion 
                    #region Pagos directos (En caso existan...)
                    //var lstPagosDirectos = new CuentasPorPagarData().listarDxpPagos(x.doxpc_icod_correlativo, Parametros.intEjercicio).Where(ob => ob.pdxpc_vorigen == "D").ToList();
                    var lstPagosDirectos = new CuentasPorPagarData().listarDxpPagos2(intPeriodo, Parametros.intEjercicio).Where(ob => ob.pdxpc_vorigen == "D").ToList();
                    if (lstPagosDirectos.Count > 0)
                    {
                        lstPagosDirectos.ForEach(y =>
                        {
                            #region Cabecera del Voucher (Pago directo...)
                            EVoucherContableCab obj_CompCabPagoDirecto = new EVoucherContableCab();
                            obj_CompCabPagoDirecto.anioc_iid_anio = Convert.ToDateTime(y.pdxpc_sfecha_pago).Year;
                            obj_CompCabPagoDirecto.mesec_iid_mes = Convert.ToDateTime(y.pdxpc_sfecha_pago).Month;

                            obj_CompCabPagoDirecto.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            MaxCorrelativo++;

                            obj_CompCabPagoDirecto.vcocc_fecha_vcontable = Convert.ToDateTime(y.pdxpc_sfecha_pago);
                            obj_CompCabPagoDirecto.vcocc_glosa = (y.pdxpc_vobservacion == "" || y.pdxpc_vobservacion == null) ? "PAGO DIRECTO DE DOCUMENTOS" : y.pdxpc_vobservacion.ToUpper();
                            obj_CompCabPagoDirecto.vcocc_observacion = obj_CompCabPagoDirecto.vcocc_glosa;
                            obj_CompCabPagoDirecto.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCabPagoDirecto.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCabPagoDirecto.tablc_iid_moneda = y.tablc_iid_tipo_moneda;
                            obj_CompCabPagoDirecto.strTipoMoneda = (y.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                            obj_CompCabPagoDirecto.intUsuario = y.intUsuario;
                            obj_CompCabPagoDirecto.strPc = y.strPc;
                            obj_CompCabPagoDirecto.vcocc_tipo_cambio = Convert.ToDecimal(y.pdxpc_nmonto_tipo_cambio);
                            obj_CompCabPagoDirecto.tbl_origen = "DXP-PGO-DIR";
                            obj_CompCabPagoDirecto.tbl_origen_icod = Convert.ToInt32(y.pdxpc_icod_correlativo);
                            obj_CompCabPagoDirecto.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxpag);

                            if (Convert.ToDecimal(y.pdxpc_nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>> <<Cje. Pgo. Directo>>", obj_CompCabPagoDirecto.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            lstDetGeneral = getDetVoucherDxpPagoDirecto(obj_CompCabPagoDirecto, y, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCabPagoDirecto.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCabPagoDirecto.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCabPagoDirecto);
                            strNroVco++;
                        });
                    }
                    #endregion
                    #region CANJE X DXP (En caso existan...)
                    //var lstPagosDirectos = new CuentasPorPagarData().listarDxpPagos(x.doxpc_icod_correlativo, Parametros.intEjercicio).Where(ob => ob.pdxpc_vorigen == "D").ToList();
                    var lstPagosDirectosDXP = new CuentasPorPagarData().listarDxpPagos2(intPeriodo, Parametros.intEjercicio).Where(ob => ob.pdxpc_vorigen == "X").ToList();
                    if (lstPagosDirectosDXP.Count > 0)
                    {
                        lstPagosDirectosDXP.ForEach(y =>
                        {
                            #region Cabecera del Voucher (Pago directo...)
                            EVoucherContableCab obj_CompCabPagoDirecto = new EVoucherContableCab();
                            obj_CompCabPagoDirecto.anioc_iid_anio = Convert.ToDateTime(y.pdxpc_sfecha_pago).Year;
                            obj_CompCabPagoDirecto.mesec_iid_mes = Convert.ToDateTime(y.pdxpc_sfecha_pago).Month;

                            obj_CompCabPagoDirecto.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            MaxCorrelativo++;

                            obj_CompCabPagoDirecto.vcocc_fecha_vcontable = Convert.ToDateTime(y.pdxpc_sfecha_pago);
                            obj_CompCabPagoDirecto.vcocc_glosa = (y.pdxpc_vobservacion == "" || y.pdxpc_vobservacion == null) ? "PAGO DIRECTO DE DOCUMENTOS" : y.pdxpc_vobservacion.ToUpper();
                            obj_CompCabPagoDirecto.vcocc_observacion = obj_CompCabPagoDirecto.vcocc_glosa;
                            obj_CompCabPagoDirecto.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCabPagoDirecto.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCabPagoDirecto.tablc_iid_moneda = y.tablc_iid_tipo_moneda;
                            obj_CompCabPagoDirecto.strTipoMoneda = (y.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                            obj_CompCabPagoDirecto.intUsuario = y.intUsuario;
                            obj_CompCabPagoDirecto.strPc = y.strPc;
                            obj_CompCabPagoDirecto.vcocc_tipo_cambio = Convert.ToDecimal(y.pdxpc_nmonto_tipo_cambio);
                            obj_CompCabPagoDirecto.tbl_origen = "DXP-PGO-DIR";
                            obj_CompCabPagoDirecto.tbl_origen_icod = Convert.ToInt32(y.pdxpc_icod_correlativo);
                            obj_CompCabPagoDirecto.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxpag);

                            if (Convert.ToDecimal(y.pdxpc_nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>> <<Cje. Pgo. Directo>>", obj_CompCabPagoDirecto.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            lstDetGeneral = getDetVoucherDxpPagoDirectoCanje(obj_CompCabPagoDirecto, y, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCabPagoDirecto.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCabPagoDirecto.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCabPagoDirecto);
                            strNroVco++;
                        });
                    }
                    #endregion
                    #region Canjes con Adelantos (En caso existan...)
                    //var lstPagosConAdelantos = objCuentasPorPagarData.ListarAdelantoPago(x.doxpc_icod_correlativo, 0, Parametros.intEjercicio);
                    var lstPagosConAdelantos = objCuentasPorPagarData.ListarAdelantoPago2(intPeriodo, Parametros.intEjercicio);
                    if (lstPagosConAdelantos.Count > 0)
                    {
                        lstPagosConAdelantos.ForEach(y =>
                        {
                            #region Cabecera de Voucher (Canje...)
                            EVoucherContableCab obj_CompCabAd = new EVoucherContableCab();
                            obj_CompCabAd.anioc_iid_anio = Convert.ToDateTime(y.adpap_sfecha_pago).Year;
                            obj_CompCabAd.mesec_iid_mes = Convert.ToDateTime(y.adpap_sfecha_pago).Month;
                            obj_CompCabAd.vcocc_icod_vcontable = MaxCorrelativo + 1;
                            obj_CompCabAd.vcocc_fecha_vcontable = Convert.ToDateTime(y.adpap_sfecha_pago);
                            obj_CompCabAd.vcocc_glosa = (y.adpap_vdescripcion == "" || y.adpap_vdescripcion == null) ? "CANJE CON ADELANTO " + y.vnumero_adelanto : "CANJE CON ADELANTO";
                            obj_CompCabAd.vcocc_observacion = obj_CompCabAd.vcocc_glosa;
                            obj_CompCabAd.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCabAd.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCabAd.tablc_iid_moneda = Convert.ToInt32(y.id_tipo_moneda_adelanto);
                            obj_CompCabAd.strTipoMoneda = (y.id_tipo_moneda_adelanto == 3) ? "S/." : "US$";
                            obj_CompCabAd.intUsuario = intUsuario;
                            obj_CompCabAd.strPc = strPc;
                            obj_CompCabAd.vcocc_tipo_cambio = Convert.ToDecimal(y.adpap_nmonto_tipo_cambio);
                            obj_CompCabAd.tbl_origen = "DXP-CJ-AD";
                            obj_CompCabAd.tbl_origen_icod = Convert.ToInt32(y.adpap_icod_correlativo);
                            obj_CompCabAd.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_docxpag);

                            if (Convert.ToDecimal(y.adpap_nmonto_tipo_cambio) <= 0)
                                throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>> <<Cje. Adelanto>>", obj_CompCabAd.vcocc_fecha_vcontable.ToShortDateString()));
                            #endregion
                            lstDetGeneral = getDetVoucherDxpCanjeAdelanto(obj_CompCabAd, y, lstPlanCuentas, lstDetGeneral, lstParamentros);
                            obj_CompCabAd.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCabAd.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCabAd);
                            MaxCorrelativo++;
                            strNroVco++;
                        });
                    }
                    #endregion
                    tx.Complete();
                }
                return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersCajaChica(int intPeriodo, int intUsuario, string strPc)
        {
            try
            {
                List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
                List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
                TesoreriaData objTesoreriaData = new TesoreriaData();
                var lstParamentros = new BContabilidad().listarParametroContable();
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                int MaxCorrelativo = new ContabilidadData().UltimoCorrelativoVoucherContableCab();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    int strNroVco = 1;
                    var lstLiqCaja = objTesoreriaData.listarLiquidacionCaja(Parametros.intEjercicio, intPeriodo);
                    lstLiqCaja.ForEach(x =>
                    {
                        #region Cabecera del Voucher
                        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                        obj_CompCab.anioc_iid_anio = x.lqcc_iid_anio;
                        obj_CompCab.mesec_iid_mes = x.lqcc_iid_mes;
                        obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_cjachic);
                        obj_CompCab.vcocc_fecha_vcontable = x.lqcc_sfecha_liquid;
                        obj_CompCab.vcocc_glosa = x.lqcc_vconcepto;
                        obj_CompCab.vcocc_observacion = x.lqcc_vconcepto;
                        obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                        obj_CompCab.tablc_iid_moneda = x.lqcc_iid_tipo_moneda;
                        obj_CompCab.strTipoMoneda = (x.lqcc_iid_tipo_moneda == 3) ? "S/." : "US$"; ;
                        obj_CompCab.intUsuario = intUsuario;
                        obj_CompCab.strPc = strPc;
                        obj_CompCab.vcocc_tipo_cambio = x.lqcc_ntipo_cambio;
                        obj_CompCab.tbl_origen = "LQ-CJA";
                        obj_CompCab.tbl_origen_icod = x.lqcc_icod_liquid_cja;
                        if (Convert.ToDecimal(x.lqcc_ntipo_cambio) <= 0)
                            throw new ArgumentException(String.Format("No se encontró TIPO DE CAMBIO para la fecha <<{0}>>", obj_CompCab.vcocc_fecha_vcontable.ToShortDateString()));
                        #endregion
                        #region Detalle
                        lstDetGeneral = getDetVoucherLiqCaja(obj_CompCab, x, lstPlanCuentas, lstDetGeneral, lstParamentros);
                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                        lstCabeceras.Add(obj_CompCab);
                        MaxCorrelativo++;
                        strNroVco++;
                        #endregion
                    });
                    tx.Complete();
                }
                return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersReporteProduccion(int intPeriodo, int intUsuario, string strPc)
        {
            try
            {
                List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
                List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
                ContabilidadData objContabilidadData = new ContabilidadData();
                ComprasData objComprasData = new ComprasData();
                var lstParamentros = new BContabilidad().listarParametroContable();
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                int MaxCorrelativo = new ContabilidadData().UltimoCorrelativoVoucherContableCab();
                List<ECuentaContable> listCuentaLocal;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    int strNroVco = 1;
                    int nroVoucher = 0;

                    List<EReporteProduccion> listRepProdCab = objComprasData.ListarReporteProduccionXMes(intPeriodo, Parametros.intEjercicio);
                    List<EReporteProduccionDetalle> listRepProdDet = objComprasData.ListarReporteProduccionDetalleXMes(intPeriodo, Parametros.intEjercicio);

                    listRepProdCab.ForEach(x =>
                    {
                        /***CREANDO VOUCHER CABECERA***/
                        nroVoucher += 1;
                        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                        #region Cabecera de Comprobante
                        obj_CompCab.anioc_iid_anio = Parametros.intEjercicio;
                        obj_CompCab.mesec_iid_mes = intPeriodo;
                        obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                        /*Falta Completar*/
                        obj_CompCab.subdi_icod_subdiario = 22;
                        obj_CompCab.vcocc_fecha_vcontable = x.rp_sfecha_produccion;
                        obj_CompCab.vcocc_glosa = x.rp_voservaciones_produccion;
                        obj_CompCab.vcocc_observacion = x.rp_voservaciones_produccion;
                        /*Falta completar*/
                        //obj_CompCab.vnumero_voucher_contable = String.Format("{0:00}", lstParamentros[0].subdi_iid_costo_venta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                        obj_CompCab.tablc_iid_moneda = 3;// NUEVOS SOLES
                        obj_CompCab.strTipoMoneda = "S/.";
                        obj_CompCab.intUsuario = intUsuario;
                        obj_CompCab.strPc = strPc;
                        //obj_CompCab.cestado = 'A';
                        obj_CompCab.vcocc_tipo_cambio = x.rp_ntipo_cambio;
                        obj_CompCab.tbl_origen = "REPOR-PRODUC";
                        obj_CompCab.tbl_origen_icod = x.rp_icod_produccion;
                        if (Convert.ToDecimal(x.rp_ntipo_cambio) <= 0)
                        {
                            throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");
                        }
                        #endregion

                        decimal mto;
                        decimal mto_soles;
                        decimal mto_dolares;

                        /***CREANDO VOUCHER DETALLE***/

                        /*** DETALLE HABER ***/
                        #region Haber

                        int cont = 0;
                        List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                        listRepProdDet.Where(obe => obe.rp_icod_produccion == x.rp_icod_produccion).ToList().ForEach(y =>
                        {
                            EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                            obj_CompDet_01.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                            obj_CompDet_01.vcocd_nro_item_det = ++cont;
                            obj_CompDet_01.tdocc_icod_tipo_doc = 47; // Reporte Producción;
                            obj_CompDet_01.vcocd_numero_doc = x.rp_num_produccion;
                            obj_CompDet_01.intUsuario = intUsuario;
                            obj_CompDet_01.strPc = strPc;
                            //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                            //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                            //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                            obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(y.clasc_vcuenta_contable_producto);
                            listCuentaLocal = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                            obj_CompDet_01.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                            listCuentaLocal.ForEach(Obe =>
                            {
                                obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            });
                            obj_CompDet_01.vcocd_vglosa_linea = y.prdc_vdescripcion_larga;
                            //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                            //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                            mto = Math.Round(Convert.ToDecimal(y.pcp * y.rpd_ncant_pro), 2);

                            obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_01.vcocd_nmto_tot_haber_sol = mto;
                            obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_01.vcocd_nmto_tot_haber_dol = Math.Round(mto / Convert.ToDecimal(x.rp_ntipo_cambio), 2);
                            mto_soles = Convert.ToDecimal(obj_CompDet_01.vcocd_nmto_tot_haber_sol);
                            mto_dolares = Convert.ToDecimal(obj_CompDet_01.vcocd_nmto_tot_haber_dol);

                            obj_CompDet_01.intTipoOperacion = 1;
                            //obj_CompDet_01.cestado = 'A';
                            obj_CompDet_01.vcocd_tipo_cambio = x.rp_ntipo_cambio;

                            lstCompDetalle.Add(obj_CompDet_01);
                            lstDetGeneral.Add(obj_CompDet_01);
                            //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                            //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                            //    mto_soles,
                            //    mto_dolares, mlistCuenta);

                        });

                        #endregion

                        /*** DETALLE DEBE ***/
                        #region Debe

                        // SUBPRODUCTO
                        #region Sub Producto
                        if (x.prdc_icod_sub_producto != 0)
                        {
                            EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                            obj_CompDet_03.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                            obj_CompDet_03.vcocd_nro_item_det = ++cont;
                            obj_CompDet_03.tdocc_icod_tipo_doc = 47; // Reporte Producción;
                            obj_CompDet_03.vcocd_numero_doc = x.rp_num_produccion;
                            obj_CompDet_03.intUsuario = intUsuario;
                            obj_CompDet_03.strPc = strPc;
                            //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                            //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                            //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                            obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(x.clasc_vcuenta_contable_producto_sub_prod);
                            listCuentaLocal = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                            obj_CompDet_03.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                            listCuentaLocal.ForEach(Obe =>
                            {
                                obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            });
                            obj_CompDet_03.vcocd_vglosa_linea = x.SubProductoEspecifico;
                            //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                            //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                            mto = x.rp_nmonto_costo_subprod;

                            obj_CompDet_03.vcocd_nmto_tot_debe_sol = mto;
                            obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_03.vcocd_nmto_tot_debe_dol = Math.Round(mto / Convert.ToDecimal(x.rp_ntipo_cambio), 2);
                            obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                            mto_soles = Convert.ToDecimal(obj_CompDet_03.vcocd_nmto_tot_debe_sol);
                            mto_dolares = Convert.ToDecimal(obj_CompDet_03.vcocd_nmto_tot_debe_dol);

                            obj_CompDet_03.intTipoOperacion = 1;
                            //obj_CompDet_03.cestado = 'A';
                            obj_CompDet_03.vcocd_tipo_cambio = x.rp_ntipo_cambio;
                            lstCompDetalle.Add(obj_CompDet_03);
                            lstDetGeneral.Add(obj_CompDet_03);
                            //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                            //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                            //    mto_soles,
                            //    mto_dolares, mlistCuenta);
                        }

                        #endregion

                        // PRODUCTO
                        #region Producto
                        EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                        obj_CompDet_02.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                        obj_CompDet_02.vcocd_nro_item_det = ++cont;
                        obj_CompDet_02.tdocc_icod_tipo_doc = 47; // Reporte Producción;
                        obj_CompDet_02.vcocd_numero_doc = x.rp_num_produccion;
                        obj_CompDet_02.intUsuario = intUsuario;
                        obj_CompDet_02.strPc = strPc;
                        //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                        //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                        //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                        obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(x.clasc_vcuenta_contable_producto);
                        listCuentaLocal = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                        obj_CompDet_02.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                        listCuentaLocal.ForEach(Obe =>
                        {
                            obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        obj_CompDet_02.vcocd_vglosa_linea = x.prdc_vdescripcion_larga;
                        //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                        //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                        mto = Convert.ToDecimal(lstCompDetalle.Sum(a => a.vcocd_nmto_tot_haber_sol)) - x.rp_nmonto_costo_subprod;

                        obj_CompDet_02.vcocd_nmto_tot_debe_sol = mto;
                        obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                        obj_CompDet_02.vcocd_nmto_tot_debe_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_sol) / x.rp_ntipo_cambio, 2)) - x.rp_nmonto_costo_subprod;
                        obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                        mto_soles = Convert.ToDecimal(obj_CompDet_02.vcocd_nmto_tot_haber_sol);
                        mto_dolares = Convert.ToDecimal(obj_CompDet_02.vcocd_nmto_tot_haber_dol);

                        obj_CompDet_02.intTipoOperacion = 1;
                        //obj_CompDet_02.cestado = 'A';
                        obj_CompDet_02.vcocd_tipo_cambio = x.rp_ntipo_cambio;
                        lstCompDetalle.Add(obj_CompDet_02);
                        lstDetGeneral.Add(obj_CompDet_02);
                        //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                        //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                        //    mto_soles,
                        //    mto_dolares, mlistCuenta);
                        #endregion

                        #endregion

                        /***TOTALES DEL COMPROBANTE***/

                        if (cont > 0)
                        {
                            obj_CompCab.vcocc_nmto_tot_debe_sol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_debe_sol), 2));
                            obj_CompCab.vcocc_nmto_tot_haber_sol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_sol), 2));
                            obj_CompCab.vcocc_nmto_tot_debe_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_debe_dol), 2));
                            obj_CompCab.vcocc_nmto_tot_haber_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_dol), 2));

                            if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_debe_sol &&
                                obj_CompCab.vcocc_nmto_tot_haber_sol == obj_CompCab.vcocc_nmto_tot_haber_sol)
                            {
                                obj_CompCab.tarec_icorrelativo_situacion_vcontable = 1;
                                obj_CompCab.strVcoSituacion = "Cuadrado";
                            }
                            else
                            {
                                obj_CompCab.tarec_icorrelativo_situacion_vcontable = 2;
                                obj_CompCab.strVcoSituacion = "No Cuadrado";
                            }
                            lstCompDetalle.AddRange(lstCompDetalle);
                        }
                        else
                        {
                            obj_CompCab.tarec_icorrelativo_situacion_vcontable = 4;
                            obj_CompCab.strVcoSituacion = "Sin Detalle";
                        }

                        //obj_CompCab.Movimiento = cont;
                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL   
                        lstCabeceras.Add(obj_CompCab);
                        strNroVco++;
                        MaxCorrelativo++;
                    });

                    tx.Complete();
                }
                return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersReporteConversion(int intPeriodo, int intUsuario, string strPc)
        {
            try
            {
                List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
                List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
                ContabilidadData objContabilidadData = new ContabilidadData();
                AlmacenData objAlmacenData = new AlmacenData();
                var lstParamentros = new BContabilidad().listarParametroContable();
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                int MaxCorrelativo = new ContabilidadData().UltimoCorrelativoVoucherContableCab();
                List<ECuentaContable> listCuentaLocal;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    int strNroVco = 1;
                    int nroVoucher = 0;

                    List<EReporteConversionCab> listRepProdCab = objAlmacenData.ReporteConversionListarxMes(Parametros.intEjercicio, intPeriodo);
                    List<EReporteConversionDet> listRepProdDet = objAlmacenData.ReporteConversionDetListarxMes(Parametros.intEjercicio, intPeriodo);

                    listRepProdCab.ForEach(x =>
                    {
                        nroVoucher += 1;
                        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                        #region Cabecera de Comprobante
                        obj_CompCab.anioc_iid_anio = Parametros.intEjercicio;
                        obj_CompCab.mesec_iid_mes = intPeriodo;
                        obj_CompCab.vcocc_icod_vcontable = MaxCorrelativo + 1;
                        /*Falta Completar*/
                        obj_CompCab.subdi_icod_subdiario = 23;
                        obj_CompCab.vcocc_fecha_vcontable = x.rcc_sfecha;
                        obj_CompCab.vcocc_glosa = x.rcc_vobservaciones;
                        obj_CompCab.vcocc_observacion = x.rcc_vobservaciones;
                        /*Falta completar*/
                        //obj_CompCab.vnumero_voucher_contable = String.Format("{0:00}", lstParamentros[0].subdi_iid_costo_venta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                        obj_CompCab.tablc_iid_moneda = 3;// NUEVOS SOLES
                        obj_CompCab.strTipoMoneda = "S/.";
                        obj_CompCab.intUsuario = intUsuario;
                        obj_CompCab.strPc = strPc;
                        //obj_CompCab.cestado = 'A';
                        obj_CompCab.vcocc_tipo_cambio = new BContabilidad().getTipoCambioPorFecha(x.rcc_sfecha);
                        obj_CompCab.tbl_origen = "REPOR-PRODUC";
                        obj_CompCab.tbl_origen_icod = x.rcc_icod_reporte_conversion;
                        if (Convert.ToDecimal(obj_CompCab.vcocc_tipo_cambio) <= 0)
                        {
                            throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");
                        }
                        #endregion
                        decimal mto;
                        decimal mto_soles;
                        decimal mto_dolares;
                        #region Haber

                        int cont = 0;
                        List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                        listRepProdDet.Where(obe => obe.rcc_icod_reporte_conversion == x.rcc_icod_reporte_conversion).ToList().ForEach(y =>
                        {
                            EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                            obj_CompDet_01.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                            obj_CompDet_01.vcocd_nro_item_det = ++cont;
                            obj_CompDet_01.tdocc_icod_tipo_doc = 120; // Reporte Conversion;
                            obj_CompDet_01.vcocd_numero_doc = x.rcc_vnuemro_reporte_conversion;
                            obj_CompDet_01.intUsuario = intUsuario;
                            obj_CompDet_01.strPc = strPc;
                            //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                            //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                            //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                            obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(y.ctacc_icod_cuenta_contable_producto);
                            listCuentaLocal = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                            obj_CompDet_01.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                            listCuentaLocal.ForEach(Obe =>
                            {
                                obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            });
                            obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(t => t.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                            obj_CompDet_01.vcocd_vglosa_linea = y.prdc_vdescripcion_larga;
                            //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                            //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                            //mto = Math.Round(Convert.ToDecimal(y.pcp * y.rpd_ncant_pro), 2);
                            //mto = Math.Round(Convert.ToDecimal(y.monto_total), 2);
                            mto = x.monto_total * x.rcc_dcantidad_conversion;
                            obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_01.vcocd_nmto_tot_haber_sol = mto;
                            obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_01.vcocd_nmto_tot_haber_dol = Math.Round(mto / Convert.ToDecimal(obj_CompCab.vcocc_tipo_cambio), 2);
                            mto_soles = Convert.ToDecimal(obj_CompDet_01.vcocd_nmto_tot_haber_sol);
                            mto_dolares = Convert.ToDecimal(obj_CompDet_01.vcocd_nmto_tot_haber_dol);

                            obj_CompDet_01.intTipoOperacion = 1;
                            //obj_CompDet_01.cestado = 'A';
                            obj_CompDet_01.vcocd_tipo_cambio = obj_CompCab.vcocc_tipo_cambio;

                            lstCompDetalle.Add(obj_CompDet_01);
                            lstDetGeneral.Add(obj_CompDet_01);
                            //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                            //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                            //    mto_soles,
                            //    mto_dolares, mlistCuenta);

                        });

                        #endregion
                        #region Debe
                        #region Sub Producto
                        //if (x.prdc_icod_sub_producto != 0)
                        //{
                        EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                        obj_CompDet_03.vcocc_icod_vcontable = obj_CompCab.vcocc_icod_vcontable;
                        obj_CompDet_03.vcocd_nro_item_det = ++cont;
                        obj_CompDet_03.tdocc_icod_tipo_doc = 120; // Reporte Conversion;
                        obj_CompDet_03.vcocd_numero_doc = x.rcc_vnuemro_reporte_conversion;
                        obj_CompDet_03.intUsuario = intUsuario;
                        obj_CompDet_03.strPc = strPc;
                        //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                        //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                        //obj_CompDet_01.iid_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                        obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(x.IcodCuentaContable);
                        listCuentaLocal = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                        obj_CompDet_03.strNroCuenta = listCuentaLocal[0].ctacc_numero_cuenta_contable;
                        listCuentaLocal.ForEach(Obe =>
                        {
                            obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);
                        obj_CompDet_03.vcocd_vglosa_linea = x.prdc_vdescripcion_larga;
                        //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                        //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                        mto = x.monto_total * x.rcc_dcantidad_conversion;

                        obj_CompDet_03.vcocd_nmto_tot_debe_sol = mto;
                        obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                        obj_CompDet_03.vcocd_nmto_tot_debe_dol = Math.Round(mto / Convert.ToDecimal(obj_CompCab.vcocc_tipo_cambio), 2);
                        obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                        mto_soles = Convert.ToDecimal(obj_CompDet_03.vcocd_nmto_tot_debe_sol);
                        mto_dolares = Convert.ToDecimal(obj_CompDet_03.vcocd_nmto_tot_debe_dol);

                        obj_CompDet_03.intTipoOperacion = 1;
                        //obj_CompDet_03.cestado = 'A';
                        obj_CompDet_03.vcocd_tipo_cambio = obj_CompCab.vcocc_tipo_cambio;
                        lstCompDetalle.Add(obj_CompDet_03);
                        lstDetGeneral.Add(obj_CompDet_03);
                        //    if (obj_CompDet_01.iid_cautomatica_debe != null)
                        //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, 
                        //    mto_soles,
                        //    mto_dolares, mlistCuenta);
                        //}

                        #endregion
                        #endregion
                        #region Totales
                        if (cont > 0)
                        {
                            obj_CompCab.vcocc_nmto_tot_debe_sol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_debe_sol), 2));
                            obj_CompCab.vcocc_nmto_tot_haber_sol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_sol), 2));
                            obj_CompCab.vcocc_nmto_tot_debe_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_debe_dol), 2));
                            obj_CompCab.vcocc_nmto_tot_haber_dol = lstCompDetalle.Sum(a => Math.Round(Convert.ToDecimal(a.vcocd_nmto_tot_haber_dol), 2));

                            if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_debe_sol &&
                                obj_CompCab.vcocc_nmto_tot_haber_sol == obj_CompCab.vcocc_nmto_tot_haber_sol)
                            {
                                obj_CompCab.tarec_icorrelativo_situacion_vcontable = 1;
                                obj_CompCab.strVcoSituacion = "Cuadrado";
                            }
                            else
                            {
                                obj_CompCab.tarec_icorrelativo_situacion_vcontable = 2;
                                obj_CompCab.strVcoSituacion = "No Cuadrado";
                            }
                            lstCompDetalle.AddRange(lstCompDetalle);
                        }
                        else
                        {
                            obj_CompCab.tarec_icorrelativo_situacion_vcontable = 4;
                            obj_CompCab.strVcoSituacion = "Sin Detalle";
                        }
                        #endregion
                        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL   
                        lstCabeceras.Add(obj_CompCab);
                        strNroVco++;
                        MaxCorrelativo++;
                    });

                    tx.Complete();
                }
                return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<EGuiaRemision> listarDespachoTransferenciaCab(int intEjercicio, int intMes)
        //{
        //    List<EGuiaRemision> lista = null;
        //    try
        //    {
        //        lista = new ContabilidadData().listarDespachoTransferenciaCab(intEjercicio, intMes);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersCostos(int intPeriodo, int intUsuario, string strPc)
        {
            try
            {
                List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
                List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
                ContabilidadData objContabilidadData = new ContabilidadData();
                var lstParamentros = new BContabilidad().listarParametroContable();
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    int strNroVco = 1;
                    var lstCostoVentaCab = objContabilidadData.listarCostoVentaCab(Parametros.intEjercicio, intPeriodo);
                    int nroVoucher = 0;
                    List<ECostoVenta> lstCostoVentaDet = new List<ECostoVenta>();
                    lstCostoVentaCab.ForEach(x =>
                    {
                        nroVoucher += 1;
                        if (x.strTipoVenta == "FAV")
                            lstCostoVentaDet = objContabilidadData.listarCostoVentaFavDet(x);
                        else if (x.strTipoVenta == "BOV")
                            lstCostoVentaDet = objContabilidadData.listarCostoVentaBovDet(x);
                        else if (x.strTipoVenta == "NCV")
                            lstCostoVentaDet = objContabilidadData.listarCostoVentaNcvDet(x);

                        //LOS QUE NO TIENEN DETALLE DE COSTO DE VENTA ES PORQUE SON SERVICIOS, LO CUAL FALTA ANALIZAR COMO SE VA A 
                        //CONSIDERAR
                        if (lstCostoVentaDet.Count > 0)
                        {
                            EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                            #region Cabecera de Comprobante
                            obj_CompCab.anioc_iid_anio = x.intEjercicio;
                            obj_CompCab.mesec_iid_mes = x.favc_sfecha_factura.Month;
                            obj_CompCab.vcocc_icod_vcontable = x.intCorrelativo;
                            if (Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta) == 0)
                                throw new ArgumentException("No se encontró el SubDiario de Costo de Venta, favor de revisar los parámetros contables");
                            obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta);
                            obj_CompCab.vcocc_fecha_vcontable = x.favc_sfecha_factura;
                            obj_CompCab.vcocc_glosa = x.favc_vobservacion;
                            obj_CompCab.vcocc_observacion = x.favc_vobservacion;
                            obj_CompCab.vcocc_numero_vcontable = String.Format("{0:00}", lstParamentros[0].parac_id_sd_cosvta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                            obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                            obj_CompCab.tablc_iid_moneda = x.tablc_iid_tipo_moneda;
                            obj_CompCab.strTipoMoneda = (x.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                            obj_CompCab.intUsuario = intUsuario;
                            obj_CompCab.strPc = strPc;
                            obj_CompCab.vcocc_tipo_cambio = x.dcmlTipoCambio;
                            obj_CompCab.tbl_origen = "COS-VTA";
                            obj_CompCab.tbl_origen_icod = x.favc_icod_factura;
                            if (Convert.ToDecimal(x.dcmlTipoCambio) <= 0)
                                throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");

                            #endregion

                            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                            lstCostoVentaDet.ForEach(y =>
                            {
                                decimal mto;
                                #region Debe/Haber
                                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                                obj_CompDet_01.vcocc_icod_vcontable = x.intCorrelativo;
                                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                                obj_CompDet_01.tdocc_icod_tipo_doc = x.intTipoDoc;
                                obj_CompDet_01.vcocd_numero_doc = x.favc_vnumero_factura;
                                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                                if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
                                    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");
                                obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
                                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                });
                                obj_CompDet_01.vcocd_vglosa_linea = y.descripcion;
                                //obj_CompDet_01.icod_centro_costo = y.iid_centro_costos;
                                //obj_CompDet_01.vcode_centro_costo = y.codigo_centro_costos;
                                mto = Math.Round(y.pcp * y.dfacc_ncantidad_producto, 2);
                                if (mto >= 0)
                                {
                                    if (x.strTipoVenta == "NCV")
                                    {
                                        obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                                        obj_CompDet_01.vcocd_nmto_tot_haber_sol = mto;
                                        obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                                        obj_CompDet_01.vcocd_nmto_tot_haber_dol = Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
                                    }
                                    else
                                    {
                                        //obj_CompDet_01.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
                                        obj_CompDet_01.vcocd_nmto_tot_debe_sol = mto;
                                        obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                                        //obj_CompDet_01.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
                                        obj_CompDet_01.vcocd_nmto_tot_debe_dol = Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
                                        obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                                    }
                                }
                                else
                                {
                                    if (x.strTipoVenta == "NCV")
                                    {
                                        obj_CompDet_01.vcocd_nmto_tot_debe_sol = mto * -1;
                                        obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                                        obj_CompDet_01.vcocd_nmto_tot_debe_dol = Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
                                        obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                                    }
                                    else
                                    {
                                        obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                                        obj_CompDet_01.vcocd_nmto_tot_haber_sol = mto * -1;
                                        obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                                        obj_CompDet_01.vcocd_nmto_tot_haber_dol = Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
                                    }
                                }

                                obj_CompDet_01.intTipoOperacion = 1;
                                obj_CompDet_01.vcocd_tipo_cambio = x.dcmlTipoCambio;

                                lstCompDetalle.Add(obj_CompDet_01);
                                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region Haber/Debe
                                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                                obj_CompDet_02.vcocc_icod_vcontable = x.intCorrelativo;
                                obj_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                                obj_CompDet_02.tdocc_icod_tipo_doc = x.intTipoDoc;
                                obj_CompDet_02.vcocd_numero_doc = x.favc_vnumero_factura;
                                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);
                                if (Convert.ToInt32(y.iid_cuenta_producto) == 0)
                                    throw new ArgumentException("No se encontró CUENTA CONTABLE DEL PRODUCTO para la generación del voucher contable");
                                obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_producto);
                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                });
                                obj_CompDet_02.vcocd_vglosa_linea = y.descripcion;
                                //obj_CompDet_02.cecoc_icod_centro_costo = null;

                                if (mto >= 0)
                                {
                                    if (x.strTipoVenta == "NCV")
                                    {
                                        obj_CompDet_02.vcocd_nmto_tot_debe_sol = mto;
                                        obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                                        obj_CompDet_02.vcocd_nmto_tot_debe_dol = Math.Round(mto / x.dcmlTipoCambio, 2);
                                        obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                                    }
                                    else
                                    {
                                        obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                                        obj_CompDet_02.vcocd_nmto_tot_haber_sol = mto;
                                        obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                                        obj_CompDet_02.vcocd_nmto_tot_haber_dol = Math.Round(mto / x.dcmlTipoCambio, 2);
                                    }
                                }
                                else
                                {
                                    if (x.strTipoVenta == "NCV")
                                    {
                                        obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                                        obj_CompDet_02.vcocd_nmto_tot_haber_sol = mto * -1;
                                        obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                                        obj_CompDet_02.vcocd_nmto_tot_haber_dol = Math.Round((mto * -1) / x.dcmlTipoCambio, 2);

                                    }
                                    else
                                    {
                                        obj_CompDet_02.vcocd_nmto_tot_debe_sol = mto * -1;
                                        obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                                        obj_CompDet_02.vcocd_nmto_tot_debe_dol = Math.Round((mto * -1) / x.dcmlTipoCambio, 2);
                                        obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                                    }
                                }

                                obj_CompDet_02.intTipoOperacion = 1;
                                obj_CompDet_02.vcocd_tipo_cambio = x.dcmlTipoCambio;

                                lstCompDetalle.Add(obj_CompDet_02);
                                lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                                if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region Total y Estado del Voucher
                                obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_sol));
                                obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_sol));
                                obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_dol));
                                obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_dol));
                                obj_CompCab.intMovimientos = lstCompDetalle.Count;

                                if (lstCompDetalle.Count > 0)
                                {
                                    if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
                                        obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
                                    {
                                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                                        obj_CompCab.strVcoSituacion = "Cuadrado";
                                    }
                                    else
                                    {
                                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                                        obj_CompCab.strVcoSituacion = "No Cuadrado";
                                    }
                                }
                                else
                                {
                                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                                    obj_CompCab.strVcoSituacion = "Sin Detalle";
                                }
                                #endregion
                            });
                            obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
                            lstCabeceras.Add(obj_CompCab);
                            strNroVco++;
                        }
                    });
                    tx.Complete();
                }
                return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region
        //public Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>> generarVouchersCostos(int intPeriodo, int intUsuario, string strPc)
        //{
        //    try
        //    {
        //        List<EVoucherContableCab> lstCabeceras = new List<EVoucherContableCab>();
        //        List<EVoucherContableDet> lstDetGeneral = new List<EVoucherContableDet>();
        //        ContabilidadData objContabilidadData = new ContabilidadData();
        //        var lstParamentros = new BContabilidad().listarParametroContable();
        //        var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
        //        var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
        //        List<EGuiaRemision> lstDespachoTranferenciaCab = new List<EGuiaRemision>();
        //        using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
        //        {

        //            int strNroVco = 1;
        //            //var lstDespachoVentaCab = objContabilidadData.listarDespachoVentaCab(Parametros.intEjercicio, intPeriodo).Where(x => x.tablc_iid_motivo == 226 && x.tablc_iid_motivo == 221);
        //            //var lstDespachoTranferenciaCab = objContabilidadData.listarDespachoTransferenciaCab(Parametros.intEjercicio, intPeriodo).Where(x => x.tablc_iid_motivo == 226 && x.tablc_iid_motivo == 221);
        //            lstDespachoTranferenciaCab = new BContabilidad().listarDespachoTransferenciaCab(Parametros.intEjercicio, intPeriodo).Where(x => x.tablc_iid_motivo == 226 || x.tablc_iid_motivo == 221).ToList();
        //            int nroVoucher = 0;
        //            #region Aterior
        //            List<ECostoVenta> lstDespachoTransferenciaDet = new List<ECostoVenta>();
        //            List<ECostoVenta> lstDespachoVentaDet = new List<ECostoVenta>();

        //            lstDespachoTranferenciaCab.ForEach(x =>
        //            {

        //                nroVoucher += 1;
        //                if (x.tablc_iid_motivo == 226)
        //                    lstDespachoTransferenciaDet = objContabilidadData.listarDespachoTransferenciaDet(x);
        //                else if (x.tablc_iid_motivo == 221)
        //                    lstDespachoTransferenciaDet = objContabilidadData.listarDespachoVentaDet(x);

        //                if (lstDespachoTransferenciaDet.Count > 0)
        //                {
        //                    EVoucherContableCab obj_CompCab = new EVoucherContableCab();
        //                    #region Cabecera de Comprobante
        //                    obj_CompCab.anioc_iid_anio = x.intEjercicio;
        //                    obj_CompCab.mesec_iid_mes = x.remic_sfecha_remision.Month;
        //                    obj_CompCab.vcocc_icod_vcontable = x.intCorrelativo;
        //                    if (Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta) == 0)
        //                        throw new ArgumentException("No se encontró el SubDiario de Costo de Venta, favor de revisar los parámetros contables");
        //                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta);
        //                    obj_CompCab.vcocc_fecha_vcontable = x.remic_sfecha_remision;
        //                    if (x.tablc_iid_motivo == 226)
        //                    {
        //                        obj_CompCab.vcocc_glosa = x.favc_vobservacion;
        //                    }
        //                    else if (x.tablc_iid_motivo == 221)
        //                    {
        //                        obj_CompCab.vcocc_glosa = "VENTAS" + x.remic_vnumero_remision;
        //                    }
        //                    if (x.tablc_iid_motivo == 226)
        //                    {
        //                        obj_CompCab.vcocc_observacion = x.favc_vobservacion;
        //                    }
        //                    else if (x.tablc_iid_motivo == 221)
        //                    {
        //                        obj_CompCab.vcocc_observacion = "VENTAS" + x.remic_vnumero_remision;
        //                    }
        //                    //obj_CompCab.vcocc_observacion = x.favc_vobservacion;
        //                    obj_CompCab.vcocc_numero_vcontable = String.Format("{0:00}", lstParamentros[0].parac_id_sd_cosvta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
        //                    obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
        //                    obj_CompCab.tablc_iid_moneda = x.tablc_iid_tipo_moneda;
        //                    obj_CompCab.strTipoMoneda = (x.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
        //                    obj_CompCab.intUsuario = intUsuario;
        //                    obj_CompCab.strPc = strPc;
        //                    obj_CompCab.vcocc_tipo_cambio = x.dcmlTipoCambio;
        //                    obj_CompCab.tbl_origen = "COS-VTA-" + x.strTipoVenta;
        //                    obj_CompCab.tbl_origen_icod = x.remic_icod_remision;
        //                    if (Convert.ToDecimal(x.dcmlTipoCambio) <= 0)
        //                        throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");

        //                    #endregion
        //                    List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
        //                    lstDespachoTransferenciaDet.ForEach(y =>
        //                    {
        //                        decimal mto;
        //                        #region Debe/Haber
        //                        EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
        //                        obj_CompDet_03.vcocc_icod_vcontable = x.intCorrelativo;
        //                        obj_CompDet_03.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //                        obj_CompDet_03.tdocc_icod_tipo_doc = x.intTipoDoc;
        //                        obj_CompDet_03.vcocd_numero_doc = x.remic_vnumero_remision;
        //                        obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);
        //                        //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
        //                        //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");

        //                        obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
        //                        var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
        //                        Lista.ForEach(Obe =>
        //                        {
        //                            obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //                            obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //                            obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //                        });
        //                        obj_CompDet_03.vcocd_vglosa_linea = y.descripcion;
        //                        obj_CompDet_03.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //                        List<ECentroCosto> ListaCC = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //                        ListaCC.ForEach(Obe2 =>
        //                        {
        //                            obj_CompDet_03.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
        //                        });

        //                        mto = Math.Round(y.pcp * y.dfacc_ncantidad_producto, 2);
        //                        if (mto >= 0)
        //                        {
        //                            if (x.almac_icod_almacen_ingreso == 53)
        //                            {
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_sol = 0;
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_dol = 0;
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                            }
        //                            else
        //                            {
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (x.almac_icod_almacen_ingreso == 53)
        //                            {
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
        //                            }
        //                            else
        //                            {
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_sol = 0;
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                                obj_CompDet_03.vcocd_nmto_tot_debe_dol = 0;
        //                                obj_CompDet_03.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //                            }
        //                        }

        //                        obj_CompDet_03.intTipoOperacion = 1;
        //                        obj_CompDet_03.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //                        lstCompDetalle.Add(obj_CompDet_03);
        //                        lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
        //                        if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
        //                        {
        //                            var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //                            lstCompDetalle = tuple.Item1;
        //                            lstDetGeneral = tuple.Item2;
        //                        }
        //                        #endregion
        //                        #region Haber/Debe
        //                        EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
        //                        obj_CompDet_04.vcocc_icod_vcontable = x.intCorrelativo;
        //                        obj_CompDet_04.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //                        obj_CompDet_04.tdocc_icod_tipo_doc = x.intTipoDoc;
        //                        obj_CompDet_04.vcocd_numero_doc = x.remic_vnumero_remision;
        //                        obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);
        //                        //if (Convert.ToInt32(y.iid_cuenta_producto) == 0)
        //                        //    throw new ArgumentException("No se encontró CUENTA CONTABLE DEL PRODUCTO para la generación del voucher contable");
        //                        obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_producto);
        //                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
        //                        Lista.ForEach(Obe =>
        //                        {
        //                            obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //                            obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //                            obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //                        });
        //                        obj_CompDet_04.vcocd_vglosa_linea = y.descripcion;
        //                        obj_CompDet_04.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //                        //List<ECentroCosto> ListaCC2 = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //                        //ListaCC2.ForEach(Obe3 =>
        //                        //{
        //                        //    obj_CompDet_04.strCodCCosto = Obe3.cecoc_vcodigo_centro_costo;
        //                        //});
        //                        if (mto >= 0)
        //                        {
        //                            if (x.almac_icod_almacen == 53)
        //                            {
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
        //                            }
        //                            else
        //                            {
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_sol = 0;
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_dol = 0;
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (x.almac_icod_almacen == 53)
        //                            {
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);

        //                            }
        //                            else
        //                            {
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_sol = 0;
        //                                obj_CompDet_04.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);
        //                                obj_CompDet_04.vcocd_nmto_tot_haber_dol = 0;
        //                            }
        //                        }

        //                        obj_CompDet_04.intTipoOperacion = 1;
        //                        obj_CompDet_04.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //                        lstCompDetalle.Add(obj_CompDet_04);
        //                        lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
        //                        if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
        //                        {
        //                            var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //                            lstCompDetalle = tuple.Item1;
        //                            lstDetGeneral = tuple.Item2;
        //                        }
        //                        #endregion
        //                        #region Total y Estado del Voucher
        //                        obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_sol));
        //                        obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_sol));
        //                        obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_dol));
        //                        obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_dol));
        //                        obj_CompCab.intMovimientos = lstCompDetalle.Count;

        //                        if (lstCompDetalle.Count > 0)
        //                        {
        //                            if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
        //                                obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
        //                            {
        //                                obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
        //                                obj_CompCab.strVcoSituacion = "Cuadrado";
        //                            }
        //                            else
        //                            {
        //                                obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
        //                                obj_CompCab.strVcoSituacion = "No Cuadrado";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
        //                            obj_CompCab.strVcoSituacion = "Sin Detalle";
        //                        }
        //                        #endregion
        //                    });
        //                    obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
        //                    lstCabeceras.Add(obj_CompCab);
        //                    strNroVco++;
        //                }

        //            });


        //            //lstDespachoVentaCab.ForEach(x =>
        //            //{
        //            //    nroVoucher += 1;
        //            //    if (x.tablc_iid_motivo == 221)
        //            //        lstDespachoVentaDet = objContabilidadData.listarDespachoVentaDet(x);
        //            //    if (lstDespachoVentaDet.Count > 0)
        //            //    {
        //            //        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
        //            //        #region Cabecera de Comprobante
        //            //        obj_CompCab.anioc_iid_anio = x.intEjercicio;
        //            //        obj_CompCab.mesec_iid_mes = x.remic_sfecha_remision.Month;
        //            //        obj_CompCab.vcocc_icod_vcontable = x.intCorrelativo;
        //            //        if (Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta) == 0)
        //            //            throw new ArgumentException("No se encontró el SubDiario de Costo de Venta, favor de revisar los parámetros contables");
        //            //        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta);
        //            //        obj_CompCab.vcocc_fecha_vcontable = x.remic_sfecha_remision;
        //            //        obj_CompCab.vcocc_glosa = x.favc_vobservacion;
        //            //        obj_CompCab.vcocc_observacion = x.favc_vobservacion;
        //            //        obj_CompCab.vcocc_numero_vcontable = String.Format("{0:00}", lstParamentros[0].parac_id_sd_cosvta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
        //            //        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
        //            //        obj_CompCab.tablc_iid_moneda = x.tablc_iid_tipo_moneda;
        //            //        obj_CompCab.strTipoMoneda = (x.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
        //            //        obj_CompCab.intUsuario = intUsuario;
        //            //        obj_CompCab.strPc = strPc;
        //            //        obj_CompCab.vcocc_tipo_cambio = x.dcmlTipoCambio;
        //            //        obj_CompCab.tbl_origen = "COS-VTA-" + x.strTipoVenta;
        //            //        obj_CompCab.tbl_origen_icod = x.remic_icod_remision;
        //            //        if (Convert.ToDecimal(x.dcmlTipoCambio) <= 0)
        //            //            throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");

        //            //        #endregion
        //            //        List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();

        //            //        lstDespachoVentaDet.ForEach(y =>
        //            //        {
        //            //            decimal mto;
        //            //            #region Debe/Haber
        //            //            EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
        //            //            obj_CompDet_01.vcocc_icod_vcontable = x.intCorrelativo;
        //            //            obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //            //            obj_CompDet_01.tdocc_icod_tipo_doc = x.intTipoDoc;
        //            //            obj_CompDet_01.vcocd_numero_doc = x.remic_vnumero_remision;
        //            //            obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
        //            //            //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
        //            //            //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");

        //            //            obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
        //            //            var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
        //            //            Lista.ForEach(Obe =>
        //            //            {
        //            //                obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //            //                obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //            //                obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //            //            });
        //            //            obj_CompDet_01.vcocd_vglosa_linea = y.descripcion;
        //            //            obj_CompDet_01.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //            //            List<ECentroCosto> ListaCC = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //            //            ListaCC.ForEach(Obe2 =>
        //            //            {
        //            //                obj_CompDet_01.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
        //            //            });

        //            //            mto = Math.Round(y.pcp * y.dfacc_ncantidad_producto, 2);
        //            //            if (mto >= 0)
        //            //            {
        //            //                if (x.almac_icod_almacen_ingreso == 53)
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                if (x.almac_icod_almacen_ingreso == 53)
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                }
        //            //            }

        //            //            obj_CompDet_01.intTipoOperacion = 1;
        //            //            obj_CompDet_01.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //            //            lstCompDetalle.Add(obj_CompDet_01);
        //            //            lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
        //            //            if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
        //            //            {
        //            //                var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //            //                lstCompDetalle = tuple.Item1;
        //            //                lstDetGeneral = tuple.Item2;
        //            //            }
        //            //            #endregion
        //            //            #region Haber/Debe
        //            //            EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
        //            //            obj_CompDet_02.vcocc_icod_vcontable = x.intCorrelativo;
        //            //            obj_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //            //            obj_CompDet_02.tdocc_icod_tipo_doc = x.intTipoDoc;
        //            //            obj_CompDet_02.vcocd_numero_doc = x.remic_vnumero_remision;
        //            //            obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);
        //            //            //if (Convert.ToInt32(y.iid_cuenta_producto) == 0)
        //            //            //    throw new ArgumentException("No se encontró CUENTA CONTABLE DEL PRODUCTO para la generación del voucher contable");
        //            //            obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_producto);
        //            //            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
        //            //            Lista.ForEach(Obe =>
        //            //            {
        //            //                obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //            //                obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //            //                obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //            //            });
        //            //            obj_CompDet_02.vcocd_vglosa_linea = y.descripcion;
        //            //            obj_CompDet_02.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //            //            List<ECentroCosto> ListaCC2 = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //            //            ListaCC2.ForEach(Obe3 =>
        //            //            {
        //            //                obj_CompDet_02.strCodCCosto = Obe3.cecoc_vcodigo_centro_costo;
        //            //            });
        //            //            if (mto >= 0)
        //            //            {
        //            //                if (x.almac_icod_almacen == 53)
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                if (x.almac_icod_almacen == 53)
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);

        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //            }

        //            //            obj_CompDet_02.intTipoOperacion = 1;
        //            //            obj_CompDet_02.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //            //            lstCompDetalle.Add(obj_CompDet_02);
        //            //            lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
        //            //            if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
        //            //            {
        //            //                var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //            //                lstCompDetalle = tuple.Item1;
        //            //                lstDetGeneral = tuple.Item2;
        //            //            }
        //            //            #endregion
        //            //            #region Total y Estado del Voucher
        //            //            obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_sol));
        //            //            obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_sol));
        //            //            obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_dol));
        //            //            obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_dol));
        //            //            obj_CompCab.intMovimientos = lstCompDetalle.Count;

        //            //            if (lstCompDetalle.Count > 0)
        //            //            {
        //            //                if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
        //            //                    obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
        //            //                {
        //            //                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
        //            //                    obj_CompCab.strVcoSituacion = "Cuadrado";
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
        //            //                    obj_CompCab.strVcoSituacion = "No Cuadrado";
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
        //            //                obj_CompCab.strVcoSituacion = "Sin Detalle";
        //            //            }
        //            //            #endregion
        //            //        });
        //            //        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
        //            //        lstCabeceras.Add(obj_CompCab);
        //            //        strNroVco++;
        //            //    }
        //            //});
        //            #endregion



        //            #region Aterior
        //            //List<ECostoVenta> lstDespachoTransferenciaDet = new List<ECostoVenta>();
        //            //lstDespachoTranferenciaCab.ForEach(x =>
        //            //{
        //            //    nroVoucher += 1;
        //            //    if (x.tablc_iid_motivo == 226)
        //            //        lstDespachoTransferenciaDet = objContabilidadData.listarDespachoTransferenciaDet(x);
        //            //    if (lstDespachoTransferenciaDet.Count > 0)
        //            //    {
        //            //        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
        //            //        #region Cabecera de Comprobante
        //            //        obj_CompCab.anioc_iid_anio = x.intEjercicio;
        //            //        obj_CompCab.mesec_iid_mes = x.remic_sfecha_remision.Month;
        //            //        obj_CompCab.vcocc_icod_vcontable = x.intCorrelativo;
        //            //        if (Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta) == 0)
        //            //            throw new ArgumentException("No se encontró el SubDiario de Costo de Venta, favor de revisar los parámetros contables");
        //            //        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta);
        //            //        obj_CompCab.vcocc_fecha_vcontable = x.remic_sfecha_remision;
        //            //        obj_CompCab.vcocc_glosa = x.favc_vobservacion;
        //            //        obj_CompCab.vcocc_observacion = x.favc_vobservacion;
        //            //        obj_CompCab.vcocc_numero_vcontable = String.Format("{0:00}", lstParamentros[0].parac_id_sd_cosvta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
        //            //        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
        //            //        obj_CompCab.tablc_iid_moneda = x.tablc_iid_tipo_moneda;
        //            //        obj_CompCab.strTipoMoneda = (x.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
        //            //        obj_CompCab.intUsuario = intUsuario;
        //            //        obj_CompCab.strPc = strPc;
        //            //        obj_CompCab.vcocc_tipo_cambio = x.dcmlTipoCambio;
        //            //        obj_CompCab.tbl_origen = "COS-VTA-" + x.strTipoVenta;
        //            //        obj_CompCab.tbl_origen_icod = x.remic_icod_remision;
        //            //        if (Convert.ToDecimal(x.dcmlTipoCambio) <= 0)
        //            //            throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");

        //            //        #endregion
        //            //        List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
        //            //        lstDespachoTransferenciaDet.ForEach(y =>
        //            //        {
        //            //            decimal mto;
        //            //            #region Debe/Haber
        //            //            EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
        //            //            obj_CompDet_03.vcocc_icod_vcontable = x.intCorrelativo;
        //            //            obj_CompDet_03.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //            //            obj_CompDet_03.tdocc_icod_tipo_doc = x.intTipoDoc;
        //            //            obj_CompDet_03.vcocd_numero_doc = x.remic_vnumero_remision;
        //            //            obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);
        //            //            //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
        //            //            //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");

        //            //            obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
        //            //            var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
        //            //            Lista.ForEach(Obe =>
        //            //            {
        //            //                obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //            //                obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //            //                obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //            //            });
        //            //            obj_CompDet_03.vcocd_vglosa_linea = y.descripcion;
        //            //            obj_CompDet_03.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //            //            List<ECentroCosto> ListaCC = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //            //            ListaCC.ForEach(Obe2 =>
        //            //            {
        //            //                obj_CompDet_03.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
        //            //            });

        //            //            mto = Math.Round(y.pcp * y.dfacc_ncantidad_producto, 2);
        //            //            if (mto >= 0)
        //            //            {
        //            //                if (x.almac_icod_almacen_ingreso == 53)
        //            //                {
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                if (x.almac_icod_almacen_ingreso == 53)
        //            //                {
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_03.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_03.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                }
        //            //            }

        //            //            obj_CompDet_03.intTipoOperacion = 1;
        //            //            obj_CompDet_03.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //            //            lstCompDetalle.Add(obj_CompDet_03);
        //            //            lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
        //            //            if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
        //            //            {
        //            //                var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //            //                lstCompDetalle = tuple.Item1;
        //            //                lstDetGeneral = tuple.Item2;
        //            //            }
        //            //            #endregion
        //            //            #region Haber/Debe
        //            //            EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
        //            //            obj_CompDet_04.vcocc_icod_vcontable = x.intCorrelativo;
        //            //            obj_CompDet_04.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //            //            obj_CompDet_04.tdocc_icod_tipo_doc = x.intTipoDoc;
        //            //            obj_CompDet_04.vcocd_numero_doc = x.remic_vnumero_remision;
        //            //            obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);
        //            //            //if (Convert.ToInt32(y.iid_cuenta_producto) == 0)
        //            //            //    throw new ArgumentException("No se encontró CUENTA CONTABLE DEL PRODUCTO para la generación del voucher contable");
        //            //            obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_producto);
        //            //            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
        //            //            Lista.ForEach(Obe =>
        //            //            {
        //            //                obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //            //                obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //            //                obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //            //            });
        //            //            obj_CompDet_04.vcocd_vglosa_linea = y.descripcion;
        //            //            obj_CompDet_04.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //            //            List<ECentroCosto> ListaCC2 = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //            //            ListaCC2.ForEach(Obe3 =>
        //            //            {
        //            //                obj_CompDet_04.strCodCCosto = Obe3.cecoc_vcodigo_centro_costo;
        //            //            });
        //            //            if (mto >= 0)
        //            //            {
        //            //                if (x.almac_icod_almacen == 53)
        //            //                {
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_dol = 0;
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                if (x.almac_icod_almacen == 53)
        //            //                {
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);

        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_04.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_04.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //            }

        //            //            obj_CompDet_04.intTipoOperacion = 1;
        //            //            obj_CompDet_04.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //            //            lstCompDetalle.Add(obj_CompDet_04);
        //            //            lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
        //            //            if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
        //            //            {
        //            //                var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //            //                lstCompDetalle = tuple.Item1;
        //            //                lstDetGeneral = tuple.Item2;
        //            //            }
        //            //            #endregion
        //            //            #region Total y Estado del Voucher
        //            //            obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_sol));
        //            //            obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_sol));
        //            //            obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_dol));
        //            //            obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_dol));
        //            //            obj_CompCab.intMovimientos = lstCompDetalle.Count;

        //            //            if (lstCompDetalle.Count > 0)
        //            //            {
        //            //                if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
        //            //                    obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
        //            //                {
        //            //                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
        //            //                    obj_CompCab.strVcoSituacion = "Cuadrado";
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
        //            //                    obj_CompCab.strVcoSituacion = "No Cuadrado";
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
        //            //                obj_CompCab.strVcoSituacion = "Sin Detalle";
        //            //            }
        //            //            #endregion
        //            //        });
        //            //        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
        //            //        lstCabeceras.Add(obj_CompCab);
        //            //        strNroVco++;
        //            //    }
        //            //});

        //            //List<ECostoVenta> lstDespachoVentaDet = new List<ECostoVenta>();
        //            //lstDespachoVentaCab.ForEach(x =>
        //            //{
        //            //    nroVoucher += 1;
        //            //    if (x.tablc_iid_motivo == 221)
        //            //        lstDespachoVentaDet = objContabilidadData.listarDespachoVentaDet(x);
        //            //    if (lstDespachoVentaDet.Count > 0)
        //            //    {
        //            //        EVoucherContableCab obj_CompCab = new EVoucherContableCab();
        //            //        #region Cabecera de Comprobante
        //            //        obj_CompCab.anioc_iid_anio = x.intEjercicio;
        //            //        obj_CompCab.mesec_iid_mes = x.remic_sfecha_remision.Month;
        //            //        obj_CompCab.vcocc_icod_vcontable = x.intCorrelativo;
        //            //        if (Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta) == 0)
        //            //            throw new ArgumentException("No se encontró el SubDiario de Costo de Venta, favor de revisar los parámetros contables");
        //            //        obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_cosvta);
        //            //        obj_CompCab.vcocc_fecha_vcontable = x.remic_sfecha_remision;
        //            //        obj_CompCab.vcocc_glosa = x.favc_vobservacion;
        //            //        obj_CompCab.vcocc_observacion = x.favc_vobservacion;
        //            //        obj_CompCab.vcocc_numero_vcontable = String.Format("{0:00}", lstParamentros[0].parac_id_sd_cosvta) + "." + String.Format("{0:000000}", nroVoucher);//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
        //            //        obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
        //            //        obj_CompCab.tablc_iid_moneda = x.tablc_iid_tipo_moneda;
        //            //        obj_CompCab.strTipoMoneda = (x.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
        //            //        obj_CompCab.intUsuario = intUsuario;
        //            //        obj_CompCab.strPc = strPc;
        //            //        obj_CompCab.vcocc_tipo_cambio = x.dcmlTipoCambio;
        //            //        obj_CompCab.tbl_origen = "COS-VTA-" + x.strTipoVenta;
        //            //        obj_CompCab.tbl_origen_icod = x.remic_icod_remision;
        //            //        if (Convert.ToDecimal(x.dcmlTipoCambio) <= 0)
        //            //            throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");

        //            //        #endregion
        //            //        List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();

        //            //        lstDespachoVentaDet.ForEach(y =>
        //            //        {
        //            //            decimal mto;
        //            //            #region Debe/Haber
        //            //            EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
        //            //            obj_CompDet_01.vcocc_icod_vcontable = x.intCorrelativo;
        //            //            obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //            //            obj_CompDet_01.tdocc_icod_tipo_doc = x.intTipoDoc;
        //            //            obj_CompDet_01.vcocd_numero_doc = x.remic_vnumero_remision;
        //            //            obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
        //            //            //if (Convert.ToInt32(y.iid_cuenta_costos) == 0)
        //            //            //    throw new ArgumentException("No se encontró CUENTA DE COSTOS para la generación del voucher contable");

        //            //            obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_costos);
        //            //            var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
        //            //            Lista.ForEach(Obe =>
        //            //            {
        //            //                obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //            //                obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //            //                obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //            //            });
        //            //            obj_CompDet_01.vcocd_vglosa_linea = y.descripcion;
        //            //            obj_CompDet_01.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //            //            List<ECentroCosto> ListaCC = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //            //            ListaCC.ForEach(Obe2 =>
        //            //            {
        //            //                obj_CompDet_01.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
        //            //            });

        //            //            mto = Math.Round(y.pcp * y.dfacc_ncantidad_producto, 2);
        //            //            if (mto >= 0)
        //            //            {
        //            //                if (x.almac_icod_almacen_ingreso == 53)
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                if (x.almac_icod_almacen_ingreso == 53)
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / Convert.ToDecimal(x.dcmlTipoCambio), 2);
        //            //                }
        //            //            }

        //            //            obj_CompDet_01.intTipoOperacion = 1;
        //            //            obj_CompDet_01.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //            //            lstCompDetalle.Add(obj_CompDet_01);
        //            //            lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
        //            //            if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
        //            //            {
        //            //                var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //            //                lstCompDetalle = tuple.Item1;
        //            //                lstDetGeneral = tuple.Item2;
        //            //            }
        //            //            #endregion
        //            //            #region Haber/Debe
        //            //            EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
        //            //            obj_CompDet_02.vcocc_icod_vcontable = x.intCorrelativo;
        //            //            obj_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
        //            //            obj_CompDet_02.tdocc_icod_tipo_doc = x.intTipoDoc;
        //            //            obj_CompDet_02.vcocd_numero_doc = x.remic_vnumero_remision;
        //            //            obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(c => c.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);
        //            //            //if (Convert.ToInt32(y.iid_cuenta_producto) == 0)
        //            //            //    throw new ArgumentException("No se encontró CUENTA CONTABLE DEL PRODUCTO para la generación del voucher contable");
        //            //            obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(y.iid_cuenta_producto);
        //            //            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
        //            //            Lista.ForEach(Obe =>
        //            //            {
        //            //                obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
        //            //                obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
        //            //                obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
        //            //            });
        //            //            obj_CompDet_02.vcocd_vglosa_linea = y.descripcion;
        //            //            obj_CompDet_02.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
        //            //            List<ECentroCosto> ListaCC2 = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList();
        //            //            ListaCC2.ForEach(Obe3 =>
        //            //            {
        //            //                obj_CompDet_02.strCodCCosto = Obe3.cecoc_vcodigo_centro_costo;
        //            //            });
        //            //            if (mto >= 0)
        //            //            {
        //            //                if (x.almac_icod_almacen == 53)
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto : Math.Round(mto * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto : Math.Round(mto / x.dcmlTipoCambio, 2);
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                if (x.almac_icod_almacen == 53)
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);

        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_sol = (x.tablc_iid_tipo_moneda == 3) ? mto * -1 : Math.Round((mto * -1) * x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
        //            //                    obj_CompDet_02.vcocd_nmto_tot_debe_dol = (x.tablc_iid_tipo_moneda == 4) ? mto * -1 : Math.Round((mto * -1) / x.dcmlTipoCambio, 2);
        //            //                    obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
        //            //                }
        //            //            }

        //            //            obj_CompDet_02.intTipoOperacion = 1;
        //            //            obj_CompDet_02.vcocd_tipo_cambio = x.dcmlTipoCambio;

        //            //            lstCompDetalle.Add(obj_CompDet_02);
        //            //            lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
        //            //            if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
        //            //            {
        //            //                var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
        //            //                lstCompDetalle = tuple.Item1;
        //            //                lstDetGeneral = tuple.Item2;
        //            //            }
        //            //            #endregion
        //            //            #region Total y Estado del Voucher
        //            //            obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_sol));
        //            //            obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_sol));
        //            //            obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_debe_dol));
        //            //            obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(z => z.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(d => d.vcocd_nmto_tot_haber_dol));
        //            //            obj_CompCab.intMovimientos = lstCompDetalle.Count;

        //            //            if (lstCompDetalle.Count > 0)
        //            //            {
        //            //                if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
        //            //                    obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
        //            //                {
        //            //                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
        //            //                    obj_CompCab.strVcoSituacion = "Cuadrado";
        //            //                }
        //            //                else
        //            //                {
        //            //                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
        //            //                    obj_CompCab.strVcoSituacion = "No Cuadrado";
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
        //            //                obj_CompCab.strVcoSituacion = "Sin Detalle";
        //            //            }
        //            //            #endregion
        //            //        });
        //            //        obj_CompCab.strNroVco = String.Format("{0:00}.{1:00000}", obj_CompCab.subdi_icod_subdiario, strNroVco);//ESTO ES PROVISIONAL                           
        //            //        lstCabeceras.Add(obj_CompCab);
        //            //        strNroVco++;
        //            //    }
        //            //});
        //            #endregion
        //            tx.Complete();
        //        }
        //        return new Tuple<List<EVoucherContableCab>, List<EVoucherContableDet>>(lstCabeceras, lstDetGeneral);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
        #region Generación de los detalle de los Vouchers Contables
        #region Desde Ventas...
        private List<EVoucherContableDet> getDetVoucherDxcPagoDirecto(EVoucherContableCab oBe, EDocXCobrarPago oBeDxcPago, List<ECuentaContable> lstPlanCuentas,
            List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDxcPago.tdocc_icod_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeDxcPago.pdxcc_vnumero_doc;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeDxcPago.ctacc_iid_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Obe.ctacc_iccosto)
                    {
                        obj_item_CompDet_01.cecoc_icod_centro_costo = oBeDxcPago.cecoc_icod_centro_costo;
                        obj_item_CompDet_01.strCodCCosto = oBeDxcPago.CentroCosto;
                    }
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = oBeDxcPago.anac_icod_analitica;
                        obj_item_CompDet_01.anad_icod_analitica = oBeDxcPago.anac_icod_analitica_det;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_01.tablc_iid_tipo_analitica, oBeDxcPago.Analitica);
                    }
                });

                obj_item_CompDet_01.vcocd_vglosa_linea = String.Format("PAGO MANUAL {0} {1}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), oBeDxcPago.pdxcc_vnumero_doc);
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeDxcPago.pdxcc_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeDxcPago.tablc_iid_tipo_moneda == 3) ? oBeDxcPago.pdxcc_nmonto_cobro : Math.Round(Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_cobro) * Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeDxcPago.tablc_iid_tipo_moneda == 4) ? oBeDxcPago.pdxcc_nmonto_cobro : Math.Round(Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_cobro) / Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;

                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDxcPago.intTipoDoc);
                obj_item_CompDet_02.vcocd_numero_doc = oBeDxcPago.strNroDoc;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);

                var lstDocumentoClase_02 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBeDxcPago.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_02.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBeDxcPago.tdodc_iid_correlativo).ToList()[0];

                /**/
                var lstCliente = (new VentasData()).ListarCliente();
                ECliente objCliente = lstCliente.Where(d => d.cliec_icod_cliente == oBeDxcPago.cliec_icod_cliente).ToList()[0];

                if (oBeDxcPago.id_tipo_moneda_dxc == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeDxcPago.id_tipo_moneda_dxc == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeDxcPago.id_tipo_moneda_dxc == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_02.anad_icod_analitica = objCliente.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_02.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = "PAGO MANUAL " + obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3) + " " + oBeDxcPago.strNroDoc;
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeDxcPago.pdxcc_nmonto_tipo_cambio;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = (oBeDxcPago.tablc_iid_tipo_moneda == 3) ? oBeDxcPago.pdxcc_nmonto_cobro : Math.Round(Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_cobro) * Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = (oBeDxcPago.tablc_iid_tipo_moneda == 4) ? oBeDxcPago.pdxcc_nmonto_cobro : Math.Round(Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_cobro) / Convert.ToDecimal(oBeDxcPago.pdxcc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.doxcc_icod_correlativo = Convert.ToInt32(oBeDxcPago.doxcc_icod_correlativo);

                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxcCanjeAdelanto(EVoucherContableCab oBe, EAdelantoPago oBeNcPago, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                obj_item_CompDet_01.vcocd_numero_doc = oBeNcPago.vnumero_doc_adelanto;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_01.tdocc_icod_tipo_doc);

                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeNcPago.tdocc_iid_correlativo_adelanto)).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_01.vcocd_numero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeNcPago.tdocc_iid_correlativo_adelanto)).ToList()[0];

                if (oBeNcPago.cliec_icod_cliente == 0)
                    throw new ArgumentException(String.Format("<<Error>> Cod. de Cliente no válido para el Cje. con NC", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_01.vcocd_numero_doc));
                ECliente objCliente = new VentasData().ListarCliente().Where(d => d.cliec_icod_cliente == oBeNcPago.cliec_icod_cliente).ToList()[0];

                if (oBeNcPago.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeNcPago.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = (oBeNcPago.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_01.anad_icod_analitica = objCliente.anac_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_01.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_01.cecoc_icod_centro_costo = null;
                obj_item_CompDet_01.vcocd_vglosa_linea = (oBeNcPago.adpac_vdescripcion == "" && oBeNcPago.adpac_vdescripcion == null) ? "APLICACIÓN CON N/C" : oBeNcPago.adpac_vdescripcion.ToUpper();
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeNcPago.adpac_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeNcPago.tablc_iid_tipo_moneda == 3) ? oBeNcPago.adpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.adpac_nmonto_pago) * Convert.ToDecimal(oBeNcPago.adpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeNcPago.tablc_iid_tipo_moneda == 4) ? oBeNcPago.adpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.adpac_nmonto_pago) / Convert.ToDecimal(oBeNcPago.adpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_01.doxcc_icod_correlativo = Convert.ToInt32(oBeNcPago.doxcc_icod_correlativo_adelanto);

                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeNcPago.tdocc_icod_tipo_doc);
                obj_item_CompDet_02.vcocd_numero_doc = oBeNcPago.vnumero_doc_pago;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);

                var lstDocumentoClase_02 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBeNcPago.tdocc_iid_correlativo_pago).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_02.vcocd_numero_doc));
                obj_DocumentoClase = lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBeNcPago.tdocc_iid_correlativo_pago).ToList()[0];

                if (oBeNcPago.iid_tipo_moneda_pago == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeNcPago.iid_tipo_moneda_pago == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeNcPago.iid_tipo_moneda_pago == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_02.anad_icod_analitica = objCliente.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_02.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = (oBeNcPago.adpac_vdescripcion == "" && oBeNcPago.adpac_vdescripcion == null) ? "CANJE CON N/C " + oBeNcPago.doxcc_vnumero_doc : oBeNcPago.adpac_vdescripcion.ToUpper();
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeNcPago.adpac_nmonto_tipo_cambio;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = (oBeNcPago.tablc_iid_tipo_moneda == 3) ? oBeNcPago.adpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.adpac_nmonto_pago) * Convert.ToDecimal(oBeNcPago.adpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = (oBeNcPago.tablc_iid_tipo_moneda == 4) ? oBeNcPago.adpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.adpac_nmonto_pago) / Convert.ToDecimal(oBeNcPago.adpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.doxcc_icod_correlativo = Convert.ToInt32(oBeNcPago.doxcc_icod_correlativo_pago);

                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxcCanjeNotaCredito(EVoucherContableCab oBe, ENotaCreditoPago oBeNcPago, List<ECuentaContable> lstPlanCuentas,
            List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                obj_item_CompDet_01.vcocd_numero_doc = oBeNcPago.vnumero_documento_NC;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_01.tdocc_icod_tipo_doc);

                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeNcPago.iid_correlativo_nota_credito)).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_01.vcocd_numero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeNcPago.iid_correlativo_nota_credito)).ToList()[0];

                if (oBeNcPago.intCliente == 0)
                    throw new ArgumentException(String.Format("<<Error>> Cod. de Cliente no válido para el Cje. con NC", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_01.vcocd_numero_doc));
                ECliente objCliente = new VentasData().ListarCliente().Where(d => d.cliec_icod_cliente == oBeNcPago.intCliente).ToList()[0];

                if (oBeNcPago.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeNcPago.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = (oBeNcPago.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_01.anad_icod_analitica = objCliente.anac_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_01.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_01.cecoc_icod_centro_costo = null;
                obj_item_CompDet_01.vcocd_vglosa_linea = (oBeNcPago.ncpac_vdescripcion == "" && oBeNcPago.ncpac_vdescripcion == null) ? "APLICACIÓN CON N/C" : oBeNcPago.ncpac_vdescripcion.ToUpper();
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeNcPago.ncpac_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeNcPago.tablc_iid_tipo_moneda == 3) ? oBeNcPago.ncpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.ncpac_nmonto_pago) * Convert.ToDecimal(oBeNcPago.ncpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeNcPago.tablc_iid_tipo_moneda == 4) ? oBeNcPago.ncpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.ncpac_nmonto_pago) / Convert.ToDecimal(oBeNcPago.ncpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_01.doxcc_icod_correlativo = Convert.ToInt32(oBeNcPago.doxcc_icod_correlativo_nota_credito);

                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeNcPago.tdocc_icod_tipo_doc);
                obj_item_CompDet_02.vcocd_numero_doc = oBeNcPago.vnumero_documento_pago;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);

                var lstDocumentoClase_02 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBeNcPago.iid_correlativo_pago).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_02.vcocd_numero_doc));
                obj_DocumentoClase = lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBeNcPago.iid_correlativo_pago).ToList()[0];

                if (oBeNcPago.iid_tipo_moneda_pago == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeNcPago.iid_tipo_moneda_pago == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeNcPago.iid_tipo_moneda_pago == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_02.anad_icod_analitica = objCliente.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_02.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = (oBeNcPago.ncpac_vdescripcion == "" && oBeNcPago.ncpac_vdescripcion == null) ? "CANJE CON N/C " + oBeNcPago.vnumero_documento_pago : oBeNcPago.ncpac_vdescripcion.ToUpper();
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeNcPago.ncpac_nmonto_tipo_cambio;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = (oBeNcPago.tablc_iid_tipo_moneda == 3) ? oBeNcPago.ncpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.ncpac_nmonto_pago) * Convert.ToDecimal(oBeNcPago.ncpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = (oBeNcPago.tablc_iid_tipo_moneda == 4) ? oBeNcPago.ncpac_nmonto_pago : Math.Round(Convert.ToDecimal(oBeNcPago.ncpac_nmonto_pago) / Convert.ToDecimal(oBeNcPago.ncpac_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.doxcc_icod_correlativo = Convert.ToInt32(oBeNcPago.doxcc_icod_correlativo_pago);

                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpLxc(EVoucherContableCab oBe, EDocXCobrar oBeDXC, ELetraPorCobrar oBeLXC, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                obj_CompDet_01.vcocd_numero_doc = oBeLXC.lexcc_vnumero_letra;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                if (oBeLXC.cliec_icod_cliente == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del cliente de la LXC {0}", oBeLXC.lexcc_vnumero_letra));
                var lstCliente = new VentasData().ListarCliente();
                ECliente obj_Cliente = lstCliente.Where(x => x.cliec_icod_cliente == oBeLXC.cliec_icod_cliente).ToList()[0];

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocLetraCliente);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXC.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_CompDet_01.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXC.tdodc_iid_correlativo).ToList()[0];

                if (oBeLXC.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeLXC.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_CompDet_01.ctacc_icod_cuenta_contable = (oBeLXC.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_CompDet_01.anad_icod_analitica = obj_Cliente.anac_icod_analitica;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaClientes, obj_Cliente.anac_iid_analitica);
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = oBe.vcocc_glosa;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeLXC.tablc_iid_tipo_moneda == 3) ? oBeLXC.lexcc_nmonto_total : Math.Round(oBeLXC.lexcc_nmonto_total * Convert.ToDecimal(oBeLXC.lexcc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeLXC.tablc_iid_tipo_moneda == 4) ? oBeLXC.lexcc_nmonto_total : Math.Round(oBeLXC.lexcc_nmonto_total / Convert.ToDecimal(oBeLXC.lexcc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeLXC.lexcc_nmonto_tipo_cambio;
                obj_CompDet_01.doxcc_icod_correlativo = Convert.ToInt32(oBeLXC.doxcc_icod_correlativo);

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                var lstDet = new CuentasPorCobrarData().listarLetraPorCobrarDet(oBeLXC.lexcc_icod_correlativo);
                lstDet.ForEach(x =>
                {
                    EVoucherContableDet obj_CompDet_D = new EVoucherContableDet();
                    obj_CompDet_D.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_CompDet_D.vcocd_nro_item_det = Cont_detalle;
                    Cont_detalle++;
                    if (Convert.ToInt32(x.ctacc_iid_cuenta_contable) > 0)
                    {
                        obj_CompDet_D.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                        obj_CompDet_D.vcocd_numero_doc = oBeLXC.lexcc_vnumero_letra;
                        obj_CompDet_D.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_D.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_D.vcocd_numero_doc);
                        obj_CompDet_D.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_iid_cuenta_contable);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_D.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_CompDet_D.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_CompDet_D.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_D.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Obe.ctacc_iccosto)
                                obj_CompDet_D.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo; ;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_CompDet_D.tablc_iid_tipo_analitica = Convert.ToInt32(x.strCodAnalitica);
                                obj_CompDet_D.anad_icod_analitica = x.anac_icod_analitica;
                                obj_CompDet_D.strAnalisis = String.Format("{0:00}.{1}", x.strCodAnalitica, x.strCodSubAnalitica);
                            }
                        });
                    }
                    else
                    {
                        obj_CompDet_D.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                        obj_CompDet_D.vcocd_numero_doc = x.pdxcc_vnumero_doc;
                        obj_CompDet_D.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_D.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_D.vcocd_numero_doc);

                        var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                        /*****************************************/
                        if (lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_CompDet_D.vcocd_numero_doc));
                        obj_DocumentoClase = lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList()[0];
                        /*****************************************/

                        obj_Cliente = lstCliente.Where(y => y.cliec_icod_cliente == oBeLXC.cliec_icod_cliente).ToList()[0];

                        if (oBeLXC.tablc_iid_tipo_moneda == 3)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        if (oBeLXC.tablc_iid_tipo_moneda == 4)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        obj_CompDet_D.ctacc_icod_cuenta_contable = (oBeLXC.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_D.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_CompDet_D.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_CompDet_D.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_D.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_CompDet_D.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                obj_CompDet_D.anad_icod_analitica = obj_Cliente.anac_icod_analitica;
                                obj_CompDet_D.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaClientes, obj_Cliente.anac_iid_analitica);
                            }
                        });
                    }

                    obj_CompDet_D.vcocd_vglosa_linea = x.lxcpc_vconcepto;
                    obj_CompDet_D.vcocd_nmto_tot_debe_sol = 0;
                    obj_CompDet_D.vcocd_nmto_tot_haber_sol = (oBeLXC.tablc_iid_tipo_moneda == 3) ? x.lxcpc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxcpc_nmonto_pago) * Convert.ToDecimal(oBeLXC.lexcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.vcocd_nmto_tot_debe_dol = 0;
                    obj_CompDet_D.vcocd_nmto_tot_haber_dol = (oBeLXC.tablc_iid_tipo_moneda == 4) ? x.lxcpc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxcpc_nmonto_pago) / Convert.ToDecimal(oBeLXC.lexcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.intTipoOperacion = 1;
                    obj_CompDet_D.vcocd_tipo_cambio = oBeLXC.lexcc_nmonto_tipo_cambio;
                    obj_CompDet_D.doxcc_icod_correlativo = Convert.ToInt32(x.doxcc_icod_correlativo);
                    lstCompDetalle.Add(obj_CompDet_D);
                    lstDetGeneral.Add(obj_CompDet_D);/***********************************************************/
                    if (obj_CompDet_D.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_CompDet_D, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<EVoucherContableDet> getDetVoucherDxpGC(EVoucherContableCab oBe, EDocXCobrar oBeDXC, EGarantiaClientes oBeGC, List<ECuentaContable> lstPlanCuentas,
         List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                CuentasPorCobrarData objCuentasPorCobrarData = new CuentasPorCobrarData();
                int Cont_detalle = 1;
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocGarantiaClientes;
                obj_CompDet_01.vcocd_numero_doc = oBeGC.garc_vnumero_garantia;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                if (oBeGC.cliec_icod_cliente == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del cliente de la GC {0}", oBeGC.garc_vnumero_garantia));
                var lstCliente = new VentasData().ListarCliente();
                ECliente obj_Cliente = lstCliente.Where(x => x.cliec_icod_cliente == oBeGC.cliec_icod_cliente).ToList()[0];

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocGarantiaClientes);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXC.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_CompDet_01.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXC.tdodc_iid_correlativo).ToList()[0];

                if (oBeGC.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeGC.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_CompDet_01.ctacc_icod_cuenta_contable = (oBeGC.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                //if (Obe.ctacc_iccosto)
                obj_CompDet_01.cecoc_icod_centro_costo = oBeGC.cecoc_icod_centro_costo;
                List<ECentroCosto> ListaCC = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == oBeGC.cecoc_icod_centro_costo).ToList();
                ListaCC.ForEach(Obe2 =>
                {
                    obj_CompDet_01.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
                });


                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_CompDet_01.anad_icod_analitica = obj_Cliente.anac_icod_analitica;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaClientes, obj_Cliente.anac_iid_analitica);
                    }
                });
                //obj_CompDet_01.vcocd_vglosa_linea = oBe.vcocc_glosa;
                obj_CompDet_01.vcocd_vglosa_linea = oBeGC.NumDXC + obj_CompDet_01.strCodCCosto;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeGC.tablc_iid_tipo_moneda == 3) ? oBeGC.garc_nmonto : Math.Round(oBeGC.garc_nmonto * Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeGC.tablc_iid_tipo_moneda == 4) ? oBeGC.garc_nmonto : Math.Round(oBeGC.garc_nmonto / Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBe.vcocc_tipo_cambio;
                obj_CompDet_01.doxcc_icod_correlativo = Convert.ToInt32(oBeGC.intDXP);
                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_CompDet_02.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                obj_CompDet_02.vcocd_numero_doc = oBeGC.NumDXC;
                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                if (oBeGC.cliec_icod_cliente == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del cliente de la GC {0}", oBeGC.garc_vnumero_garantia));
                var lstCliente2 = new VentasData().ListarCliente();
                ECliente obj_Cliente2 = lstCliente.Where(x => x.cliec_icod_cliente == oBeGC.cliec_icod_cliente).ToList()[0];



                var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocFacturaVenta);
                if (lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == oBeGC.ClaseFac).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                var obj_DocumentoClase2 = lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == oBeGC.ClaseFac).ToList()[0];


                if (oBeGC.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                if (oBeGC.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                obj_CompDet_02.ctacc_icod_cuenta_contable = (oBeGC.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra);

                obj_CompDet_02.cecoc_icod_centro_costo = oBeGC.cecoc_icod_centro_costo;
                List<ECentroCosto> ListaCC2 = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == oBeGC.cecoc_icod_centro_costo).ToList();
                ListaCC2.ForEach(Obe2 =>
                {
                    obj_CompDet_02.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
                });


                var Lista2 = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista2.ForEach(Obe =>
                {
                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_CompDet_02.anad_icod_analitica = obj_Cliente.anac_icod_analitica;
                        obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaClientes, obj_Cliente.anac_iid_analitica);
                    }
                });
                //obj_CompDet_02.vcocd_vglosa_linea = oBe.vcocc_glosa;
                obj_CompDet_02.vcocd_vglosa_linea = oBeGC.NumDXC + obj_CompDet_02.strCodCCosto;
                obj_CompDet_02.vcocd_nmto_tot_haber_sol = (oBeGC.tablc_iid_tipo_moneda == 3) ? oBeGC.garc_nmonto : Math.Round(oBeGC.garc_nmonto * Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_02.vcocd_nmto_tot_haber_dol = (oBeGC.tablc_iid_tipo_moneda == 4) ? oBeGC.garc_nmonto : Math.Round(oBeGC.garc_nmonto / Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_02.intTipoOperacion = 1;
                obj_CompDet_02.vcocd_tipo_cambio = oBe.vcocc_tipo_cambio;
                obj_CompDet_02.doxcc_icod_correlativo = Convert.ToInt32(oBeGC.favc_icod_factura);
                lstCompDetalle.Add(obj_CompDet_02);
                lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxcNotaCredito(EVoucherContableCab oBe, EDocXCobrar oBeDXC, List<ECuentaContable> lstPlanCuentas,
            List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;

                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc));
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXC.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", oBeDXC.tdocc_vabreviatura_tipo_doc, oBeDXC.doxcc_vnumero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXC.tdodc_iid_correlativo).ToList()[0];
                //bool Flag = false;
                #region anterior
                //#region detalle 01

                //if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_gastos_nac) != 0)
                //{
                //    EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                //    obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                //    obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                //    Cont_detalle++;
                //    obj_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc);
                //    obj_CompDet_01.vcocd_numero_doc = oBeDXC.doxcc_vnumero_doc;
                //    obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                //    obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_gastos_nac);
                //    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                //    Lista.ForEach(Obe =>
                //    {
                //        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                //        obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                //        obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                //    });
                //    obj_CompDet_01.vcocd_vglosa_linea = oBeDXC.doxcc_vobservaciones;
                //    obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                //    obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto) : Math.Round((Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto)) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //    obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                //    obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto) : Math.Round((Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto)) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //    obj_CompDet_01.intTipoOperacion = 1;
                //    obj_CompDet_01.vcocd_tipo_cambio = oBeDXC.doxcc_nmonto_tipo_cambio;

                //    lstCompDetalle.Add(obj_CompDet_01);
                //    lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                //    if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                //    {
                //        var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                //        lstCompDetalle = tuple.Item1;
                //        lstDetGeneral = tuple.Item2;
                //    }



                //}
                //#endregion
                //var lstDetalle = new BCuentasPorCobrar().BuscarDocumentoXCobrarCuentaContable(oBeDXC.doxcc_icod_correlativo);
                //lstDetalle.ForEach(xd =>
                //{
                //    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                //    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                //    obj_item_CompDet.vcocd_nro_item_det = Cont_detalle;
                //    Cont_detalle++;
                //    obj_item_CompDet.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc);
                //    obj_item_CompDet.vcocd_numero_doc = oBeDXC.doxcc_vnumero_doc;
                //    obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet.vcocd_numero_doc);
                //    obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(xd.ctacc_iid_cuenta_contable);
                //    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                //    Lista.ForEach(Obe =>
                //    {
                //        obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                //        obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                //        obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                //        if (Obe.ctacc_iccosto)
                //            obj_item_CompDet.cecoc_icod_centro_costo = xd.cecoc_icod_centro_costo;
                //        List<ECentroCosto> ListaCC = new BContabilidad().listarCentroCosto().Where(xx => xx.cecoc_icod_centro_costo == xd.cecoc_icod_centro_costo).ToList();
                //        ListaCC.ForEach(Obe2 =>
                //        {
                //            obj_item_CompDet.strCodCCosto = Obe2.cecoc_vcodigo_centro_costo;
                //        });
                //        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                //        {
                //            obj_item_CompDet.tablc_iid_tipo_analitica = Convert.ToInt32(xd.TipoAnalitica);
                //            obj_item_CompDet.anad_icod_analitica = oBeDXC.anac_icod_analitica;
                //        }
                //    });

                //    obj_item_CompDet.vcocd_vglosa_linea = (xd.ccdcc_vglosa != null) ? xd.ccdcc_vglosa.ToUpper() : "";
                //    obj_item_CompDet.intTipoOperacion = 1;
                //    obj_item_CompDet.vcocd_tipo_cambio = oBeDXC.doxcc_nmonto_tipo_cambio;
                //    if (xd.ccdcc_nmonto > 0)
                //    {
                //        obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                //        obj_item_CompDet.vcocd_nmto_tot_debe_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? xd.ccdcc_nmonto : Math.Round(xd.ccdcc_nmonto * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //        obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                //        obj_item_CompDet.vcocd_nmto_tot_debe_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? xd.ccdcc_nmonto : Math.Round(xd.ccdcc_nmonto / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //    }
                //    if (xd.ccdcc_nmonto < 0)
                //    {
                //        obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                //        obj_item_CompDet.vcocd_nmto_tot_haber_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? (xd.ccdcc_nmonto * -1) : Math.Round((xd.ccdcc_nmonto * -1) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //        obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                //        obj_item_CompDet.vcocd_nmto_tot_haber_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? (xd.ccdcc_nmonto * -1) : Math.Round((xd.ccdcc_nmonto * -1) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //    }
                //    obj_item_CompDet.doxcc_icod_correlativo = Convert.ToInt32(xd.doxcc_icod_correlativo);

                //    lstCompDetalle.Add(obj_item_CompDet);
                //    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/
                //    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                //    {
                //        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                //        lstCompDetalle = tuple.Item1;
                //        lstDetGeneral = tuple.Item2;
                //    }
                //    Flag = true;
                //});
                //#region detalle 02
                //EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                //obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                //obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                //Cont_detalle++;
                //obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc);
                //obj_CompDet_02.vcocd_numero_doc = oBeDXC.doxcc_vnumero_doc;
                //obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                //if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac) == 0)
                //    throw new ArgumentException("No se encontró cuenta contable de IGV para la generación del voucher contable");
                //obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac);
                //var Lista2 = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                //Lista2.ForEach(Obe =>
                //{
                //    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                //    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                //    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                //});
                //obj_CompDet_02.vcocd_vglosa_linea = oBeDXC.doxcc_vobservaciones;
                //obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                //obj_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                //obj_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //obj_CompDet_02.intTipoOperacion = 1;
                //obj_CompDet_02.vcocd_tipo_cambio = oBeDXC.doxcc_nmonto_tipo_cambio;
                //obj_CompDet_02.doxcc_icod_correlativo = Convert.ToInt32(oBeDXC.doxcc_icod_correlativo);
                //lstCompDetalle.Add(obj_CompDet_02);
                //lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                //if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                //{
                //    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                //    lstCompDetalle = tuple.Item1;
                //    lstDetGeneral = tuple.Item2;
                //}
                //#region detalle 03
                //var lstCliente = (new VentasData()).ListarCliente();
                //ECliente objCliente = lstCliente.Where(x => x.cliec_icod_cliente == oBeDXC.cliec_icod_cliente).ToList()[0];

                //EVoucherContableDet obj_CompDet_05 = new EVoucherContableDet();
                //obj_CompDet_05.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                //obj_CompDet_05.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                //obj_CompDet_05.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc);
                //obj_CompDet_05.vcocd_numero_doc = oBeDXC.doxcc_vnumero_doc;
                //obj_CompDet_05.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_05.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_05.vcocd_numero_doc);

                //if (oBeDXC.tablc_iid_tipo_moneda == 3)
                //    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                //        throw new ArgumentException("No se encontró cuenta contable NACIONAL para la generación del voucher contable");

                //if (oBeDXC.tablc_iid_tipo_moneda == 4)
                //    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                //        throw new ArgumentException("No se encontró cuenta contable EXTRANJERA para la generación del voucher contable");
                //obj_CompDet_05.ctacc_icod_cuenta_contable = (oBeDXC.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                //Lista2 = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_05.ctacc_icod_cuenta_contable).ToList();
                //Lista2.ForEach(Obe =>
                //{
                //    obj_CompDet_05.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                //    obj_CompDet_05.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                //    obj_CompDet_05.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                //    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                //    {
                //        obj_CompDet_05.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                //        obj_CompDet_05.anad_icod_analitica = objCliente.anac_icod_analitica;
                //        obj_CompDet_05.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_05.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                //    }
                //});

                //obj_CompDet_05.vcocd_vglosa_linea = oBeDXC.doxcc_vobservaciones;

                //if (Flag == false)
                //{
                //    obj_CompDet_05.vcocd_nmto_tot_haber_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //    obj_CompDet_05.vcocd_nmto_tot_haber_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //}
                //else
                //{
                //    obj_CompDet_05.vcocd_nmto_tot_haber_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? oBeDXC.doxcc_nmonto_total : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_total) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //    obj_CompDet_05.vcocd_nmto_tot_haber_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? oBeDXC.doxcc_nmonto_total : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_total) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                //}
                //obj_CompDet_05.vcocd_nmto_tot_debe_sol = 0;
                //obj_CompDet_05.vcocd_nmto_tot_debe_dol = 0;
                //obj_CompDet_05.intTipoOperacion = 1;
                //obj_CompDet_05.vcocd_tipo_cambio = oBeDXC.doxcc_nmonto_tipo_cambio;
                //obj_CompDet_05.doxcc_icod_correlativo = Convert.ToInt32(oBeDXC.doxcc_icod_correlativo);
                //lstCompDetalle.Add(obj_CompDet_05);
                //lstDetGeneral.Add(obj_CompDet_05);/***********************************************************/
                //if (obj_CompDet_05.ctacc_icod_cuenta_debe_auto != null)
                //{
                //    var tuple = addCtaAutomatica(obj_CompDet_05, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                //    lstCompDetalle = tuple.Item1;
                //    lstDetGeneral = tuple.Item2;
                //}
                //#endregion
                //#endregion
                #endregion
                #region detalle 01
                bool Flag = false;
                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_gastos_nac) != 0)
                {
                    EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                    obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                    Cont_detalle++;
                    obj_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc);
                    obj_CompDet_01.vcocd_numero_doc = oBeDXC.doxcc_vnumero_doc;
                    obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                    obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_gastos_nac);
                    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    obj_CompDet_01.vcocd_vglosa_linea = oBeDXC.doxcc_vobservaciones;
                    obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                    obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto) : Math.Round((Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto)) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                    obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto) : Math.Round((Convert.ToDecimal(oBeDXC.doxcc_nmonto_afecto) + Convert.ToDecimal(oBeDXC.doxcc_nmonto_inafecto)) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_01.intTipoOperacion = 1;
                    obj_CompDet_01.vcocd_tipo_cambio = oBeDXC.doxcc_nmonto_tipo_cambio;

                    lstCompDetalle.Add(obj_CompDet_01);
                    lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                    if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }

                    Flag = true;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc);
                obj_CompDet_02.vcocd_numero_doc = oBeDXC.doxcc_vnumero_doc;
                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac) == 0)
                    throw new ArgumentException("No se encontró cuenta contable de IGV para la generación del voucher contable");
                obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac);
                var Lista2 = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista2.ForEach(Obe =>
                {
                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                });
                obj_CompDet_02.vcocd_vglosa_linea = oBeDXC.doxcc_vobservaciones;
                obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                obj_CompDet_02.intTipoOperacion = 1;
                obj_CompDet_02.vcocd_tipo_cambio = oBeDXC.doxcc_nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_02);
                lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #region detalle 03
                var lstCliente = (new VentasData()).ListarCliente();
                ECliente objCliente = lstCliente.Where(x => x.cliec_icod_cliente == oBeDXC.cliec_icod_cliente).ToList()[0];

                EVoucherContableDet obj_CompDet_05 = new EVoucherContableDet();
                obj_CompDet_05.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_05.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                obj_CompDet_05.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDXC.tdocc_icod_tipo_doc);
                obj_CompDet_05.vcocd_numero_doc = oBeDXC.doxcc_vnumero_doc;
                obj_CompDet_05.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_05.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_05.vcocd_numero_doc);

                if (oBeDXC.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException("No se encontró cuenta contable NACIONAL para la generación del voucher contable");

                if (oBeDXC.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException("No se encontró cuenta contable EXTRANJERA para la generación del voucher contable");
                obj_CompDet_05.ctacc_icod_cuenta_contable = (oBeDXC.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista2 = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_05.ctacc_icod_cuenta_contable).ToList();
                Lista2.ForEach(Obe =>
                {
                    obj_CompDet_05.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_05.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_05.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_05.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_CompDet_05.anad_icod_analitica = objCliente.anac_icod_analitica;
                        obj_CompDet_05.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_05.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                    }
                });

                obj_CompDet_05.vcocd_vglosa_linea = oBeDXC.doxcc_vobservaciones;

                if (Flag == false)
                {
                    obj_CompDet_05.vcocd_nmto_tot_haber_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_05.vcocd_nmto_tot_haber_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? oBeDXC.doxcc_nmonto_impuesto : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_impuesto) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                }
                else
                {
                    obj_CompDet_05.vcocd_nmto_tot_haber_sol = (oBeDXC.tablc_iid_tipo_moneda == 3) ? oBeDXC.doxcc_nmonto_total : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_total) * Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_05.vcocd_nmto_tot_haber_dol = (oBeDXC.tablc_iid_tipo_moneda == 4) ? oBeDXC.doxcc_nmonto_total : Math.Round(Convert.ToDecimal(oBeDXC.doxcc_nmonto_total) / Convert.ToDecimal(oBeDXC.doxcc_nmonto_tipo_cambio), 2);
                }
                obj_CompDet_05.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_05.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_05.intTipoOperacion = 1;
                obj_CompDet_05.vcocd_tipo_cambio = oBeDXC.doxcc_nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_05);
                lstDetGeneral.Add(obj_CompDet_05);/***********************************************************/
                if (obj_CompDet_05.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_05, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherRetencion(EVoucherContableCab oBe, ERetencion oBeRet, List<ECuentaContable> lstPlanCuentas,
            List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocRetencion;
                obj_CompDet_01.vcocd_numero_doc = oBeRet.retc_vnumero_comprob_reten;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocRetencion);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocRetencion).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), oBeRet.retc_vnumero_comprob_reten));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocRetencion).ToList()[0];

                if (oBeRet.tablc_iid_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeRet.tablc_iid_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_CompDet_01.ctacc_icod_cuenta_contable = (oBeRet.tablc_iid_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                });
                obj_CompDet_01.vcocd_vglosa_linea = "";
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeRet.tablc_iid_moneda == 3) ? oBeRet.retc_nmto_total_retencion : Math.Round(oBeRet.retc_nmto_total_retencion * oBeRet.retc_nmto_tipo_cambio, 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeRet.tablc_iid_moneda == 4) ? oBeRet.retc_nmto_total_retencion : Math.Round(oBeRet.retc_nmto_total_retencion / oBeRet.retc_nmto_tipo_cambio, 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeRet.retc_nmto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                var lstDet = new BVentas().listarRetencionDet(oBeRet.retc_icod_comprobante_retencion);
                lstDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet_n = new EVoucherContableDet();
                    obj_item_CompDet_n.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_n.vcocd_nro_item_det = Cont_detalle;
                    Cont_detalle++;
                    obj_item_CompDet_n.tdocc_icod_tipo_doc = x.tdoc_icod_tipo_documento;
                    obj_item_CompDet_n.vcocd_numero_doc = x.retd_vnumero_documento;
                    obj_item_CompDet_n.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_n.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_n.vcocd_numero_doc);

                    lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(x.tdoc_icod_tipo_documento);
                    if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList().Count == 0)
                        throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_n.strTipNroDocumento.Substring(0, 3), oBeRet.retc_vnumero_comprob_reten));
                    obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];

                    if (oBeRet.tablc_iid_moneda == 3)
                        if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_n.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                    if (oBeRet.tablc_iid_moneda == 4)
                        if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_n.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                    obj_item_CompDet_n.ctacc_icod_cuenta_contable = (x.Moneda_DXC == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                    Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_n.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet_n.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_n.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_n.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        {
                            obj_item_CompDet_n.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                            obj_item_CompDet_n.anad_icod_analitica = x.intAnalitica;
                            obj_item_CompDet_n.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_n.tablc_iid_tipo_analitica, x.strCodAnalitica);
                        }
                    });

                    obj_item_CompDet_n.vcocd_vglosa_linea = String.Format("PAGO CON RETENCION {0}", oBeRet.retc_vnumero_comprob_reten);
                    obj_item_CompDet_n.intTipoOperacion = 1;
                    obj_item_CompDet_n.vcocd_tipo_cambio = oBeRet.retc_nmto_tipo_cambio;

                    obj_item_CompDet_n.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_n.vcocd_nmto_tot_haber_sol = (oBeRet.tablc_iid_moneda == 3) ? x.retd_nmto_retencion : Math.Round(x.retd_nmto_retencion * oBeRet.retc_nmto_tipo_cambio, 2);
                    obj_item_CompDet_n.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_n.vcocd_nmto_tot_haber_dol = (oBeRet.tablc_iid_moneda == 4) ? x.retd_nmto_retencion : Math.Round(x.retd_nmto_retencion / oBeRet.retc_nmto_tipo_cambio, 2);

                    lstCompDetalle.Add(obj_item_CompDet_n);
                    lstDetGeneral.Add(obj_item_CompDet_n);/***********************************************************/
                    if (obj_item_CompDet_n.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_n, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherPlanillaVentaSoles(EVoucherContableCab oBe, EPlanillaCobranzaCab oBePln, List<ECuentaContable> lstPlanCuentas,
            List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;
                VentasData objVentasData = new VentasData();
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                obj_CompDet_01.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", "PVD", obj_CompDet_01.vcocd_numero_doc);

                obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBePln.intCtaContableCtaBancariaSol);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_CompDet_01.anad_icod_analitica = oBePln.intAnaliticaCtaBancariaSol;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_01.tablc_iid_tipo_analitica, oBePln.strCodAnaliticaCtaBancariaSol);
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = String.Format("VENTAS DEL DÍA {0}", oBePln.plnc_sfecha_planilla.ToShortDateString());
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = oBePln.dcmlTotalSol;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = Math.Round(Convert.ToDecimal(oBePln.dcmlTotalSol) / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02

                var lstPlnDet = objVentasData.listarPlanillaCobranzaDetalleVCO(oBePln.plnc_icod_planilla).Where(ob => ob.tablc_iid_tipo_moneda == Parametros.intTipoMonedaSoles).ToList();
                lstPlnDet.ForEach(x =>
                {
                    if (x.tablc_iid_tipo_mov == Parametros.intPlnFacturacion)
                    {
                        #region Pagos de los docs. facturados

                        var lstPagos = objVentasData.listarPago(Convert.ToInt32(x.plnd_icod_tipo_doc), Convert.ToInt32(x.plnd_icod_documento)).Where(y => y.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaSoles).ToList();
                        lstPagos.ForEach(y =>
                        {
                            #region Pagos en efectivo...
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                            {
                                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                                obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                                obj_CompDet_02.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", x.tdocc_vabreviatura_tipo_docPago, obj_CompDet_02.vcocd_numero_doc);

                                /////
                                //var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                                if (x.tdocd_iid_correlativo == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                                //ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), x.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_02.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_02.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_02.vcocd_vglosa_linea = String.Format("NÚMERO DE OT {0}", x.strNroOt);
                                obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_02.vcocd_nmto_tot_haber_sol = y.pgoc_nmonto;
                                obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_02.vcocd_nmto_tot_haber_dol = Math.Round(y.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                                obj_CompDet_02.intTipoOperacion = 1;
                                obj_CompDet_02.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                                lstCompDetalle.Add(obj_CompDet_02);
                                lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                                if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                            }
                            #endregion
                            #region Pago con tarjeta
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                            {
                                #region debe...
                                EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                                obj_CompDet_03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_03.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                                obj_CompDet_03.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                                obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", "PVD", obj_CompDet_03.vcocd_numero_doc);

                                obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(y.intCtaContableBcoTarjeta);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                                        obj_CompDet_03.anad_icod_analitica = y.intAnaliticaBcoTarjeta;
                                        obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, y.strCodAnaliticaBcoTarjeta);
                                    }
                                });
                                obj_CompDet_03.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                                obj_CompDet_03.vcocd_nmto_tot_debe_sol = y.pgoc_nmonto;
                                obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                                obj_CompDet_03.vcocd_nmto_tot_debe_dol = Math.Round(y.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                                obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                                obj_CompDet_03.intTipoOperacion = 1;
                                obj_CompDet_03.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                                lstCompDetalle.Add(obj_CompDet_03);
                                lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
                                if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region haber...
                                EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
                                obj_CompDet_04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_04.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_04.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                                obj_CompDet_04.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", x.tdocc_vabreviatura_tipo_docPago, obj_CompDet_04.vcocd_numero_doc);

                                /////
                                //var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                                if (x.tdocd_iid_correlativo == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_CompDet_04.vcocd_numero_doc));
                                //ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), x.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_04.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_04.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_04.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_04.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_04.vcocd_vglosa_linea = "PAGO CON TARJETA " + y.pgoc_descripcion;
                                obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_04.vcocd_nmto_tot_haber_sol = y.pgoc_nmonto;
                                obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_04.vcocd_nmto_tot_haber_dol = Math.Round(y.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                                obj_CompDet_04.intTipoOperacion = 1;
                                obj_CompDet_04.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                                lstCompDetalle.Add(obj_CompDet_04);
                                lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
                                if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion

                            }
                            #endregion
                            #region Pago con nota de credito
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                            {
                                #region debe...
                                EVoucherContableDet obj_CompDet_05 = new EVoucherContableDet();
                                obj_CompDet_05.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_05.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_05.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                                obj_CompDet_05.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                                obj_CompDet_05.strTipNroDocumento = String.Format("{0} {1}", "PVD", obj_CompDet_05.vcocd_numero_doc);

                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(y.tdocc_icoc_tipo_documento);
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_nota_credito).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para NCV {0}", y.strNroNotaCredito));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_nota_credito).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_05.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_05.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_05.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_05.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_05.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_05.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_05.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_05.anad_icod_analitica = y.intAnaliticaClienteNC;
                                        obj_CompDet_05.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_05.tablc_iid_tipo_analitica, y.strCodAnaliticaClienteNC);
                                    }
                                });
                                obj_CompDet_05.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                                obj_CompDet_05.vcocd_nmto_tot_debe_sol = y.pgoc_nmonto;
                                obj_CompDet_05.vcocd_nmto_tot_haber_sol = 0;
                                obj_CompDet_05.vcocd_nmto_tot_debe_dol = Math.Round(y.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                                obj_CompDet_05.vcocd_nmto_tot_haber_dol = 0;
                                obj_CompDet_05.intTipoOperacion = 1;
                                obj_CompDet_05.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                                lstCompDetalle.Add(obj_CompDet_05);
                                lstDetGeneral.Add(obj_CompDet_05);/***********************************************************/
                                if (obj_CompDet_05.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_05, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region haber...
                                EVoucherContableDet obj_CompDet_06 = new EVoucherContableDet();
                                obj_CompDet_06.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_06.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_06.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                                obj_CompDet_06.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_06.strTipNroDocumento = String.Format("{0} {1}", x.tdocc_vabreviatura_tipo_docPago, obj_CompDet_06.vcocd_numero_doc);

                                /////
                                //lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                                if (x.tdocd_iid_correlativo == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_CompDet_06.vcocd_numero_doc));
                                //obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_06.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_06.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_06.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_06.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_06.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_06.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_06.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_06.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_06.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_06.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                                obj_CompDet_06.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_06.vcocd_nmto_tot_haber_sol = y.pgoc_nmonto;
                                obj_CompDet_06.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_06.vcocd_nmto_tot_haber_dol = Math.Round(y.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                                obj_CompDet_06.intTipoOperacion = 1;
                                obj_CompDet_06.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                                lstCompDetalle.Add(obj_CompDet_06);
                                lstDetGeneral.Add(obj_CompDet_06);/***********************************************************/
                                if (obj_CompDet_06.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_06, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                            }
                            #endregion
                            #region Pago con anticipo
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                            {
                                #region debe...
                                EVoucherContableDet obj_CompDet_07 = new EVoucherContableDet();
                                obj_CompDet_07.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_07.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_07.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                                obj_CompDet_07.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                                obj_CompDet_07.strTipNroDocumento = String.Format("{0} {1}", "PVD", obj_CompDet_07.vcocd_numero_doc);

                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(y.tdocc_icoc_tipo_documento);
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_anticipo).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para ADC {0}", y.strNroAnticipo));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_anticipo).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_07.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_07.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_07.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_07.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_07.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_07.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_07.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_07.anad_icod_analitica = y.intAnaliticaClienteAnticipo;
                                        obj_CompDet_07.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_07.tablc_iid_tipo_analitica, y.strCodAnaliticaClienteAnticipo);
                                    }
                                });
                                obj_CompDet_07.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                                obj_CompDet_07.vcocd_nmto_tot_debe_sol = y.pgoc_nmonto;
                                obj_CompDet_07.vcocd_nmto_tot_haber_sol = 0;
                                obj_CompDet_07.vcocd_nmto_tot_debe_dol = Math.Round(y.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                                obj_CompDet_07.vcocd_nmto_tot_haber_dol = 0;
                                obj_CompDet_07.intTipoOperacion = 1;
                                obj_CompDet_07.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                                lstCompDetalle.Add(obj_CompDet_07);
                                lstDetGeneral.Add(obj_CompDet_07);/***********************************************************/
                                if (obj_CompDet_07.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_07, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region haber...
                                EVoucherContableDet obj_CompDet_08 = new EVoucherContableDet();
                                obj_CompDet_08.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_08.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_08.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                                obj_CompDet_08.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_08.strTipNroDocumento = String.Format("{0} {1}", x.tdocc_vabreviatura_tipo_docPago, obj_CompDet_08.vcocd_numero_doc);

                                /////
                                //lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                                if (x.tdocd_iid_correlativo == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_CompDet_08.vcocd_numero_doc));
                                //obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_08.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_08.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_08.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_08.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_08.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_08.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_08.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_08.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_08.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_08.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                                obj_CompDet_08.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_08.vcocd_nmto_tot_haber_sol = y.pgoc_nmonto;
                                obj_CompDet_08.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_08.vcocd_nmto_tot_haber_dol = Math.Round(y.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                                obj_CompDet_08.intTipoOperacion = 1;
                                obj_CompDet_08.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                                lstCompDetalle.Add(obj_CompDet_08);
                                lstDetGeneral.Add(obj_CompDet_08);/***********************************************************/
                                if (obj_CompDet_08.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_08, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                            }
                            #endregion
                        });
                        #endregion
                    }
                    else if (x.tablc_iid_tipo_mov == Parametros.intPlnPago)
                    {
                        #region pagos...
                        var oBePgo = objVentasData.getDatosPago(Convert.ToInt32(x.pgoc_icod_pago))[0];
                        #region Pagos en efectivo...
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                            obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_02.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", x.tdocc_vabreviatura_tipo_docPago, obj_CompDet_02.vcocd_numero_doc);

                            /////
                            //var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (x.tdocd_iid_correlativo == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                            //ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), x.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_02.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_02.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_02.vcocd_vglosa_linea = String.Format("NÚMERO DE OT {0}", x.strNroOt);
                            obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_sol = oBePgo.pgoc_nmonto;
                            obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_dol = Math.Round(oBePgo.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_02.intTipoOperacion = 1;
                            obj_CompDet_02.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_02);
                            lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                            if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                        }
                        #endregion
                        #region Pago con tarjeta
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                            obj_CompDet_03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_03.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_03.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", "PVD", obj_CompDet_03.vcocd_numero_doc);

                            obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(oBePgo.intCtaContableBcoTarjeta);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                                    obj_CompDet_03.anad_icod_analitica = oBePgo.intAnaliticaBcoTarjeta;
                                    obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, oBePgo.strCodAnaliticaBcoTarjeta);
                                }
                            });
                            obj_CompDet_03.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                            obj_CompDet_03.vcocd_nmto_tot_debe_sol = oBePgo.pgoc_nmonto;
                            obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_03.vcocd_nmto_tot_debe_dol = Math.Round(oBePgo.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_03.intTipoOperacion = 1;
                            obj_CompDet_03.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_03);
                            lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
                            if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
                            obj_CompDet_04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_04.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_04.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_04.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", x.tdocc_vabreviatura_tipo_docPago, obj_CompDet_04.vcocd_numero_doc);

                            /////
                            //var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (x.tdocd_iid_correlativo == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_CompDet_04.vcocd_numero_doc));
                            //ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), x.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_04.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_04.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_04.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_04.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_04.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                            obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_sol = oBePgo.pgoc_nmonto;
                            obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_dol = Math.Round(oBePgo.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_04.intTipoOperacion = 1;
                            obj_CompDet_04.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_04);
                            lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
                            if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        #endregion
                        #region Pago con nota de credito
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_05 = new EVoucherContableDet();
                            obj_CompDet_05.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_05.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_05.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_05.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_05.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_05.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_05.vcocd_numero_doc);

                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(oBePgo.tdocc_icoc_tipo_documento);
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_nota_credito).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para NCV {0}", oBePgo.strNroNotaCredito));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_nota_credito).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_05.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_05.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_05.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_05.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_05.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_05.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_05.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_05.anad_icod_analitica = oBePgo.intAnaliticaClienteNC;
                                    obj_CompDet_05.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_05.tablc_iid_tipo_analitica, oBePgo.strCodAnaliticaClienteNC);
                                }
                            });
                            obj_CompDet_05.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                            obj_CompDet_05.vcocd_nmto_tot_debe_sol = oBePgo.pgoc_nmonto;
                            obj_CompDet_05.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_05.vcocd_nmto_tot_debe_dol = Math.Round(oBePgo.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_05.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_05.intTipoOperacion = 1;
                            obj_CompDet_05.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_05);
                            lstDetGeneral.Add(obj_CompDet_05);/***********************************************************/
                            if (obj_CompDet_05.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_05, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_06 = new EVoucherContableDet();
                            obj_CompDet_06.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_06.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_06.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_06.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_06.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_06.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_06.vcocd_numero_doc);

                            /////
                            lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_CompDet_06.vcocd_numero_doc));
                            obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_06.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_06.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_06.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_06.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_06.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_06.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_06.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_06.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_06.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_06.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                            obj_CompDet_06.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_06.vcocd_nmto_tot_haber_sol = oBePgo.pgoc_nmonto;
                            obj_CompDet_06.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_06.vcocd_nmto_tot_haber_dol = Math.Round(oBePgo.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_06.intTipoOperacion = 1;
                            obj_CompDet_06.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_06);
                            lstDetGeneral.Add(obj_CompDet_06);/***********************************************************/
                            if (obj_CompDet_06.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_06, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        #endregion
                        #region Pago con anticipo
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_07 = new EVoucherContableDet();
                            obj_CompDet_07.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_07.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_07.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_07.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_07.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_07.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_07.vcocd_numero_doc);

                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocAdelantoCliente);
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_anticipo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para ADC {0}", oBePgo.strNroAnticipo));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_anticipo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_07.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_07.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_07.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_07.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_07.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_07.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_07.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_07.anad_icod_analitica = oBePgo.intAnaliticaClienteAnticipo;
                                    obj_CompDet_07.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_07.tablc_iid_tipo_analitica, oBePgo.strCodAnaliticaClienteAnticipo);
                                }
                            });
                            obj_CompDet_07.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                            obj_CompDet_07.vcocd_nmto_tot_debe_sol = oBePgo.pgoc_nmonto;
                            obj_CompDet_07.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_07.vcocd_nmto_tot_debe_dol = Math.Round(oBePgo.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_07.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_07.intTipoOperacion = 1;
                            obj_CompDet_07.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_07);
                            lstDetGeneral.Add(obj_CompDet_07);/***********************************************************/
                            if (obj_CompDet_07.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_07, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_08 = new EVoucherContableDet();
                            obj_CompDet_08.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_08.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_08.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_08.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_08.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_08.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_08.vcocd_numero_doc);

                            /////
                            lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_CompDet_08.vcocd_numero_doc));
                            obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_08.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_08.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_08.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_08.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_08.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_08.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_08.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_08.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_08.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_08.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                            obj_CompDet_08.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_08.vcocd_nmto_tot_haber_sol = oBePgo.pgoc_nmonto;
                            obj_CompDet_08.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_08.vcocd_nmto_tot_haber_dol = Math.Round(oBePgo.pgoc_nmonto / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_08.intTipoOperacion = 1;
                            obj_CompDet_08.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_08);
                            lstDetGeneral.Add(obj_CompDet_08);/***********************************************************/
                            if (obj_CompDet_08.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_08, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        #endregion
                        #endregion
                    }

                    else if (x.tablc_iid_tipo_mov == Parametros.intPlnAnticipo)
                    {

                        if (x.tablc_iid_tipo_pago == 1)
                        {
                            #region Anticipo en efectivo...
                            EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                            obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.plnd_icod_tipo_doc);
                            obj_CompDet_02.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                            /////
                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.plnd_icod_tipo_doc));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_02.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_02.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_02.vcocd_vglosa_linea = String.Format("NÚMERO DE OT {0}", x.strNroOt);
                            obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_sol = x.plnd_nmonto_pagado;
                            obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_dol = Math.Round(x.plnd_nmonto_pagado / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_02.intTipoOperacion = 1;
                            obj_CompDet_02.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_02);
                            lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                            if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        if (x.tablc_iid_tipo_pago == 2)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                            obj_CompDet_03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_03.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_03.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);

                            if (x.intTipoTarjeta == Parametros.Tabla_Tabla_registro_Visa)
                                obj_CompDet_03.ctacc_icod_cuenta_contable = Parametros.Cuenta_tarjeta_visa;
                            else if (x.intTipoTarjeta == Parametros.Tabla_Tabla_registro_Mastercard)
                                obj_CompDet_03.ctacc_icod_cuenta_contable = Parametros.Cuenta_tarjeta_mastercard;
                            else if (x.intTipoTarjeta == Parametros.Tabla_Tabla_registro_Diners)
                                obj_CompDet_03.ctacc_icod_cuenta_contable = Parametros.Cuenta_tarjeta_dinners;
                            else
                                obj_CompDet_03.ctacc_icod_cuenta_contable = Parametros.Cuenta_tarjeta_visa;

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                                    obj_CompDet_03.anad_icod_analitica = x.intAnaliticaBancoTarjetaBanco;
                                    obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, x.strCodAnaliticaBancoTarjetaBanco);
                                }
                            });
                            obj_CompDet_03.vcocd_vglosa_linea = String.Format("PAGO DEL ADC {0}", x.plnd_vnumero_doc);
                            obj_CompDet_03.vcocd_nmto_tot_debe_sol = x.plnd_nmonto_pagado;
                            obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_03.vcocd_nmto_tot_debe_dol = Math.Round(x.plnd_nmonto_pagado / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_03.intTipoOperacion = 1;
                            obj_CompDet_03.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_03);
                            lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
                            if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
                            obj_CompDet_04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_04.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_04.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_04.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_04.vcocd_numero_doc);

                            /////
                            //var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (x.tdocd_iid_correlativo == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_CompDet_04.vcocd_numero_doc));
                            //ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), x.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_icod_cuenta_contable_nac);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_04.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_04.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_04.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_04.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_04.vcocd_vglosa_linea = x.strCliente;
                            obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_sol = x.plnd_nmonto_pagado;
                            obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_dol = Math.Round(x.plnd_nmonto_pagado / Convert.ToDecimal(oBePln.dcmlTipoCambioSol), 2);
                            obj_CompDet_04.intTipoOperacion = 1;
                            obj_CompDet_04.vcocd_tipo_cambio = oBePln.dcmlTipoCambioSol;

                            lstCompDetalle.Add(obj_CompDet_04);
                            lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
                            if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                    }

                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherPlanillaVentaDolares(EVoucherContableCab oBe, EPlanillaCobranzaCab oBePln, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                int Cont_detalle = 1;
                VentasData objVentasData = new VentasData();
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = Cont_detalle;
                Cont_detalle++;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                obj_CompDet_01.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBePln.intCtaContableCtaBancariaDol);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_CompDet_01.anad_icod_analitica = oBePln.intAnaliticaCtaBancariaDol;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_01.tablc_iid_tipo_analitica, oBePln.strCodAnaliticaCtaBancariaDol);
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = String.Format("VENTAS DEL DÍA {0}", oBePln.plnc_sfecha_planilla.ToShortDateString());
                //obj_CompDet_01.vcocd_nmto_tot_debe_sol = oBePln.dcmlTotalDol;
                //obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                //obj_CompDet_01.vcocd_nmto_tot_debe_dol = Math.Round(Convert.ToDecimal(oBePln.dcmlTotalDol) / Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = Math.Round(Convert.ToDecimal(oBePln.dcmlTotalDol) * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = oBePln.dcmlTotalDol;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                var lstPlnDet = objVentasData.listarPlanillaCobranzaDetalleVCO(oBePln.plnc_icod_planilla).Where(ob => ob.tablc_iid_tipo_moneda == Parametros.intTipoMonedaDolares).ToList();
                lstPlnDet.ForEach(x =>
                {
                    if (x.tablc_iid_tipo_mov == Parametros.intPlnFacturacion)
                    {
                        #region Pagos de los docs. facturados
                        var lstPagos = objVentasData.listarPago(Convert.ToInt32(x.plnd_icod_tipo_doc), Convert.ToInt32(x.plnd_icod_documento)).Where(y => y.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares).ToList();
                        lstPagos.ForEach(y =>
                        {
                            #region Pagos en efectivo...
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                            {
                                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                                obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.plnd_icod_tipo_doc);
                                obj_CompDet_02.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                                /////
                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.plnd_icod_tipo_doc));
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_02.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_02.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_02.vcocd_vglosa_linea = String.Format("NÚMERO");
                                obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_02.vcocd_nmto_tot_haber_sol = Math.Round(y.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                                obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_02.vcocd_nmto_tot_haber_dol = y.pgoc_nmonto;
                                obj_CompDet_02.intTipoOperacion = 1;
                                obj_CompDet_02.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                                lstCompDetalle.Add(obj_CompDet_02);
                                lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                                if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                            }
                            #endregion
                            #region Pago con tarjeta
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                            {
                                #region debe...
                                EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                                obj_CompDet_03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_03.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                                obj_CompDet_03.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                                obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);

                                obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(y.intCtaContableBcoTarjeta);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                                        obj_CompDet_03.anad_icod_analitica = y.intAnaliticaBcoTarjeta;
                                        obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, y.strCodAnaliticaBcoTarjeta);
                                    }
                                });
                                obj_CompDet_03.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                                obj_CompDet_03.vcocd_nmto_tot_debe_sol = Math.Round(y.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                                obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                                obj_CompDet_03.vcocd_nmto_tot_debe_dol = y.pgoc_nmonto;
                                obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                                obj_CompDet_03.intTipoOperacion = 1;
                                obj_CompDet_03.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                                lstCompDetalle.Add(obj_CompDet_03);
                                lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
                                if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region haber...
                                EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
                                obj_CompDet_04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_04.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_04.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                                obj_CompDet_04.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_04.vcocd_numero_doc);

                                /////
                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_CompDet_04.vcocd_numero_doc));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_04.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_04.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_04.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_04.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_04.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                                obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_04.vcocd_nmto_tot_haber_sol = Math.Round(y.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                                obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_04.vcocd_nmto_tot_haber_dol = y.pgoc_nmonto;
                                obj_CompDet_04.intTipoOperacion = 1;
                                obj_CompDet_04.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                                lstCompDetalle.Add(obj_CompDet_04);
                                lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
                                if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                            }
                            #endregion
                            #region Pago con nota de credito
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                            {
                                #region debe...
                                EVoucherContableDet obj_CompDet_05 = new EVoucherContableDet();
                                obj_CompDet_05.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_05.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_05.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                                obj_CompDet_05.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                                obj_CompDet_05.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_05.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_05.vcocd_numero_doc);

                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(y.tdocc_icoc_tipo_documento);
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_nota_credito).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para NCV {0}", y.strNroNotaCredito));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_nota_credito).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_05.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_05.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_05.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_05.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_05.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_05.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_05.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_05.anad_icod_analitica = y.intAnaliticaClienteNC;
                                        obj_CompDet_05.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_05.tablc_iid_tipo_analitica, y.strCodAnaliticaClienteNC);
                                    }
                                });
                                obj_CompDet_05.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                                obj_CompDet_05.vcocd_nmto_tot_debe_sol = Math.Round(y.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                                obj_CompDet_05.vcocd_nmto_tot_haber_sol = 0;
                                obj_CompDet_05.vcocd_nmto_tot_debe_dol = y.pgoc_nmonto;
                                obj_CompDet_05.vcocd_nmto_tot_haber_dol = 0;
                                obj_CompDet_05.intTipoOperacion = 1;
                                obj_CompDet_05.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                                lstCompDetalle.Add(obj_CompDet_05);
                                lstDetGeneral.Add(obj_CompDet_05);/***********************************************************/
                                if (obj_CompDet_05.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_05, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region haber...
                                EVoucherContableDet obj_CompDet_06 = new EVoucherContableDet();
                                obj_CompDet_06.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_06.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                                obj_CompDet_06.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                                obj_CompDet_06.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_06.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_06.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_06.vcocd_numero_doc);

                                /////
                                lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.plnd_icod_tipo_doc));
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_CompDet_06.vcocd_numero_doc));
                                obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_06.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_06.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_06.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_06.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_06.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_06.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_06.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_06.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_06.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_06.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                                obj_CompDet_06.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_06.vcocd_nmto_tot_haber_sol = Math.Round(y.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                                obj_CompDet_06.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_06.vcocd_nmto_tot_haber_dol = y.pgoc_nmonto;
                                obj_CompDet_06.intTipoOperacion = 1;
                                obj_CompDet_06.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                                lstCompDetalle.Add(obj_CompDet_06);
                                lstDetGeneral.Add(obj_CompDet_06);/***********************************************************/
                                if (obj_CompDet_06.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_06, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                            }
                            #endregion
                            #region Pago con anticipo
                            if (y.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                            {
                                #region debe...
                                EVoucherContableDet obj_CompDet_07 = new EVoucherContableDet();
                                obj_CompDet_07.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_07.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_07.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                                obj_CompDet_07.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                                obj_CompDet_07.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_07.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_07.vcocd_numero_doc);

                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(y.tdocc_icoc_tipo_documento);
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_anticipo).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para ADC {0}", y.strNroAnticipo));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == y.tdodc_iid_correlativo_anticipo).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_07.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_07.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_07.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_07.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_07.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_07.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_07.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_07.anad_icod_analitica = y.intAnaliticaClienteAnticipo;
                                        obj_CompDet_07.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_07.tablc_iid_tipo_analitica, y.strCodAnaliticaClienteAnticipo);
                                    }
                                });
                                obj_CompDet_07.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                                obj_CompDet_07.vcocd_nmto_tot_debe_sol = Math.Round(y.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                                obj_CompDet_07.vcocd_nmto_tot_haber_sol = 0;
                                obj_CompDet_07.vcocd_nmto_tot_debe_dol = y.pgoc_nmonto;
                                obj_CompDet_07.vcocd_nmto_tot_haber_dol = 0;
                                obj_CompDet_07.intTipoOperacion = 1;
                                obj_CompDet_07.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                                lstCompDetalle.Add(obj_CompDet_07);
                                lstDetGeneral.Add(obj_CompDet_07);/***********************************************************/
                                if (obj_CompDet_07.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_07, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                                #region haber...
                                EVoucherContableDet obj_CompDet_08 = new EVoucherContableDet();
                                obj_CompDet_08.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_CompDet_08.vcocd_nro_item_det = Cont_detalle;
                                Cont_detalle++;
                                obj_CompDet_08.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                                obj_CompDet_08.vcocd_numero_doc = x.plnd_vnumero_doc;
                                obj_CompDet_08.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_08.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_08.vcocd_numero_doc);

                                /////
                                lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_CompDet_08.vcocd_numero_doc));
                                obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                                /////
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                                ////
                                obj_CompDet_08.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_08.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_CompDet_08.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_CompDet_08.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_CompDet_08.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                    {
                                        obj_CompDet_08.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                        obj_CompDet_08.anad_icod_analitica = x.intAnaliticaCliente;
                                        obj_CompDet_08.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_08.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                    }
                                });
                                obj_CompDet_08.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                                obj_CompDet_08.vcocd_nmto_tot_debe_sol = 0;
                                obj_CompDet_08.vcocd_nmto_tot_haber_sol = Math.Round(y.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                                obj_CompDet_08.vcocd_nmto_tot_debe_dol = 0;
                                obj_CompDet_08.vcocd_nmto_tot_haber_dol = y.pgoc_nmonto;
                                obj_CompDet_08.intTipoOperacion = 1;
                                obj_CompDet_08.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                                lstCompDetalle.Add(obj_CompDet_08);
                                lstDetGeneral.Add(obj_CompDet_08);/***********************************************************/
                                if (obj_CompDet_08.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_CompDet_08, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                                #endregion
                            }
                            #endregion
                        });
                        #endregion
                    }
                    else if (x.tablc_iid_tipo_mov == Parametros.intPlnPago)
                    {
                        var oBePgo = objVentasData.getDatosPago(Convert.ToInt32(x.pgoc_icod_pago))[0];
                        #region Pagos en efectivo...
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                            obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.plnd_icod_tipo_doc);
                            obj_CompDet_02.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                            /////
                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.plnd_icod_tipo_doc));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_02.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_02.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_02.vcocd_vglosa_linea = String.Format("NÚMERO DE OT");
                            obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_sol = Math.Round(oBePgo.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_dol = oBePgo.pgoc_nmonto;
                            obj_CompDet_02.intTipoOperacion = 1;
                            obj_CompDet_02.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_02);
                            lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                            if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                        }
                        #endregion
                        #region Pago con tarjeta
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                            obj_CompDet_03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_03.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_03.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);

                            obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(oBePgo.intCtaContableBcoTarjeta);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                                    obj_CompDet_03.anad_icod_analitica = oBePgo.intAnaliticaBcoTarjeta;
                                    obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, oBePgo.strCodAnaliticaBcoTarjeta);
                                }
                            });
                            obj_CompDet_03.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                            obj_CompDet_03.vcocd_nmto_tot_debe_sol = Math.Round(oBePgo.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_03.vcocd_nmto_tot_debe_dol = oBePgo.pgoc_nmonto;
                            obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_03.intTipoOperacion = 1;
                            obj_CompDet_03.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_03);
                            lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
                            if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
                            obj_CompDet_04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_04.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_04.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_04.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_04.vcocd_numero_doc);

                            /////
                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_CompDet_04.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_04.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_04.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_04.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_04.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_04.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                            obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_sol = Math.Round(oBePgo.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_dol = oBePgo.pgoc_nmonto;
                            obj_CompDet_04.intTipoOperacion = 1;
                            obj_CompDet_04.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_04);
                            lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
                            if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        #endregion
                        #region Pago con nota de credito
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_05 = new EVoucherContableDet();
                            obj_CompDet_05.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_05.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_05.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_05.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_05.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_05.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_05.vcocd_numero_doc);

                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(oBePgo.tdocc_icoc_tipo_documento);
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_nota_credito).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para NCV {0}", oBePgo.strNroNotaCredito));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_nota_credito).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_05.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_05.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_05.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_05.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_05.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_05.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_05.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_05.anad_icod_analitica = oBePgo.intAnaliticaClienteNC;
                                    obj_CompDet_05.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_05.tablc_iid_tipo_analitica, oBePgo.strCodAnaliticaClienteNC);
                                }
                            });
                            obj_CompDet_05.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                            obj_CompDet_05.vcocd_nmto_tot_debe_sol = Math.Round(oBePgo.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_05.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_05.vcocd_nmto_tot_debe_dol = oBePgo.pgoc_nmonto;
                            obj_CompDet_05.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_05.intTipoOperacion = 1;
                            obj_CompDet_05.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_05);
                            lstDetGeneral.Add(obj_CompDet_05);/***********************************************************/
                            if (obj_CompDet_05.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_05, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_06 = new EVoucherContableDet();
                            obj_CompDet_06.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_06.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_06.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_06.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_06.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_06.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_06.vcocd_numero_doc);

                            /////
                            lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_CompDet_06.vcocd_numero_doc));
                            obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_06.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_06.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_06.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_06.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_06.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_06.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_06.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_06.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_06.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_06.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_06.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                            obj_CompDet_06.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_06.vcocd_nmto_tot_haber_sol = Math.Round(oBePgo.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_06.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_06.vcocd_nmto_tot_haber_dol = oBePgo.pgoc_nmonto;
                            obj_CompDet_06.intTipoOperacion = 1;
                            obj_CompDet_06.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_06);
                            lstDetGeneral.Add(obj_CompDet_06);/***********************************************************/
                            if (obj_CompDet_06.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_06, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        #endregion
                        #region Pago con anticipo
                        if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_07 = new EVoucherContableDet();
                            obj_CompDet_07.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_07.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_07.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_07.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_07.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_07.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_07.vcocd_numero_doc);

                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocAdelantoCliente);
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_anticipo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para ADC {0}", oBePgo.strNroAnticipo));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePgo.tdodc_iid_correlativo_anticipo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_07.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_07.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_07.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_07.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_07.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_07.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_07.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_07.anad_icod_analitica = oBePgo.intAnaliticaClienteAnticipo;
                                    obj_CompDet_07.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_07.tablc_iid_tipo_analitica, oBePgo.strCodAnaliticaClienteAnticipo);
                                }
                            });
                            obj_CompDet_07.vcocd_vglosa_linea = String.Format("PAGO DEL DOCUMENTO {0} {1}", x.strTipoDoc, x.plnd_vnumero_doc);
                            obj_CompDet_07.vcocd_nmto_tot_debe_sol = Math.Round(oBePgo.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_07.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_07.vcocd_nmto_tot_debe_dol = oBePgo.pgoc_nmonto;
                            obj_CompDet_07.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_07.intTipoOperacion = 1;
                            obj_CompDet_07.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_07);
                            lstDetGeneral.Add(obj_CompDet_07);/***********************************************************/
                            if (obj_CompDet_07.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_07, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_08 = new EVoucherContableDet();
                            obj_CompDet_08.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_08.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_08.tdocc_icod_tipo_doc = Convert.ToInt32(x.intTipoDocDelPago);
                            obj_CompDet_08.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_08.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_08.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_08.vcocd_numero_doc);

                            /////
                            lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.intTipoDocDelPago));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_CompDet_08.vcocd_numero_doc));
                            obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_08.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_08.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_08.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_08.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_08.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_08.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_08.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_08.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_08.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_08.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_08.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                            obj_CompDet_08.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_08.vcocd_nmto_tot_haber_sol = Math.Round(oBePgo.pgoc_nmonto * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_08.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_08.vcocd_nmto_tot_haber_dol = oBePgo.pgoc_nmonto;
                            obj_CompDet_08.intTipoOperacion = 1;
                            obj_CompDet_08.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_08);
                            lstDetGeneral.Add(obj_CompDet_08);/***********************************************************/
                            if (obj_CompDet_08.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_08, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else if (x.tablc_iid_tipo_mov == Parametros.intPlnAnticipo)
                    {
                        if (x.tablc_iid_tipo_pago == 1)
                        {
                            #region Anticipo en efectivo...
                            EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                            obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_02.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.plnd_icod_tipo_doc);
                            obj_CompDet_02.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                            /////
                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.plnd_icod_tipo_doc));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_02.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_02.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_02.vcocd_vglosa_linea = String.Format("NÚMERO DE OT");
                            obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_sol = Math.Round(x.plnd_nmonto_pagado * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_02.vcocd_nmto_tot_haber_dol = x.plnd_nmonto_pagado;
                            obj_CompDet_02.intTipoOperacion = 1;
                            obj_CompDet_02.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_02);
                            lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                            if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        if (x.tablc_iid_tipo_pago == 2)
                        {
                            #region debe...
                            EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                            obj_CompDet_03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_03.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            obj_CompDet_03.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                            obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);

                            obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(oBePln.intCtaContableCtaBancariaDol);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                                    obj_CompDet_03.anad_icod_analitica = x.intAnaliticaBancoTarjetaBanco;
                                    obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, x.strCodAnaliticaBancoTarjetaBanco);
                                }
                            });
                            obj_CompDet_03.vcocd_vglosa_linea = String.Format("PAGO DEL ADC {0}", x.plnd_vnumero_doc);
                            obj_CompDet_03.vcocd_nmto_tot_debe_sol = Math.Round(x.plnd_nmonto_pagado * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                            obj_CompDet_03.vcocd_nmto_tot_debe_dol = x.plnd_nmonto_pagado;
                            obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                            obj_CompDet_03.intTipoOperacion = 1;
                            obj_CompDet_03.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_03);
                            lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
                            if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region haber...
                            EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
                            obj_CompDet_04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_CompDet_04.vcocd_nro_item_det = Cont_detalle;
                            Cont_detalle++;
                            obj_CompDet_04.tdocc_icod_tipo_doc = Convert.ToInt32(x.plnd_icod_tipo_doc);
                            obj_CompDet_04.vcocd_numero_doc = x.plnd_vnumero_doc;
                            obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_04.vcocd_numero_doc);

                            /////
                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.plnd_icod_tipo_doc));
                            if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_CompDet_04.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                            /////
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            ////
                            obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_CompDet_04.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                    obj_CompDet_04.anad_icod_analitica = x.intAnaliticaCliente;
                                    obj_CompDet_04.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_04.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                                }
                            });
                            obj_CompDet_04.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                            obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_sol = Math.Round(x.plnd_nmonto_pagado * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                            obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
                            obj_CompDet_04.vcocd_nmto_tot_haber_dol = x.plnd_nmonto_pagado;
                            obj_CompDet_04.intTipoOperacion = 1;
                            obj_CompDet_04.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                            lstCompDetalle.Add(obj_CompDet_04);
                            lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
                            if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }

                        //#region debe...
                        //EVoucherContableDet obj_CompDet_03 = new EVoucherContableDet();
                        //obj_CompDet_03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                        //obj_CompDet_03.vcocd_nro_item_det = Cont_detalle;
                        //Cont_detalle++;
                        //obj_CompDet_03.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        //obj_CompDet_03.vcocd_numero_doc = oBePln.plnc_vnumero_planilla;
                        //obj_CompDet_03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(r => r.tdocc_icod_tipo_doc == obj_CompDet_03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_03.vcocd_numero_doc);

                        //obj_CompDet_03.ctacc_icod_cuenta_contable = Convert.ToInt32(x.intCtaCbleTarjetaBanco);

                        //Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_03.ctacc_icod_cuenta_contable).ToList();
                        //Lista.ForEach(Obe =>
                        //{
                        //    obj_CompDet_03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        //    obj_CompDet_03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        //    obj_CompDet_03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        //    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        //    {
                        //        obj_CompDet_03.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        //        obj_CompDet_03.anad_icod_analitica = x.intAnaliticaBancoTarjetaBanco;
                        //        obj_CompDet_03.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_03.tablc_iid_tipo_analitica, x.strCodAnaliticaBancoTarjetaBanco);
                        //    }
                        //});
                        //obj_CompDet_03.vcocd_vglosa_linea = String.Format("PAGO DEL ADC {0}", x.plnd_vnumero_doc);
                        //obj_CompDet_03.vcocd_nmto_tot_debe_sol = Math.Round(x.plnd_nmonto_pagado * Convert.ToDecimal(oBePln.dcmlTipoCambioDol), 2);
                        //obj_CompDet_03.vcocd_nmto_tot_haber_sol = 0;
                        //obj_CompDet_03.vcocd_nmto_tot_debe_dol = x.plnd_nmonto_pagado;
                        //obj_CompDet_03.vcocd_nmto_tot_haber_dol = 0;
                        //obj_CompDet_03.intTipoOperacion = 1;
                        //obj_CompDet_03.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                        //lstCompDetalle.Add(obj_CompDet_03);
                        //lstDetGeneral.Add(obj_CompDet_03);/***********************************************************/
                        //if (obj_CompDet_03.ctacc_icod_cuenta_debe_auto != null)
                        //{
                        //    var tuple = addCtaAutomatica(obj_CompDet_03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        //    lstCompDetalle = tuple.Item1;
                        //    lstDetGeneral = tuple.Item2;
                        //}
                        //#endregion
                        //#region haber...
                        //EVoucherContableDet obj_CompDet_04 = new EVoucherContableDet();
                        //obj_CompDet_04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                        //obj_CompDet_04.vcocd_nro_item_det = Cont_detalle;
                        //Cont_detalle++;
                        //obj_CompDet_04.tdocc_icod_tipo_doc = Convert.ToInt32(x.plnd_icod_tipo_doc);
                        //obj_CompDet_04.vcocd_numero_doc = x.plnd_vnumero_doc;
                        //obj_CompDet_04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_04.vcocd_numero_doc);

                        ///////
                        //lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.plnd_icod_tipo_doc));
                        //if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList().Count == 0)
                        //    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_CompDet_04.vcocd_numero_doc));
                        //obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdocd_iid_correlativo).ToList()[0];
                        ///////
                        //if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        //    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_04.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                        //////
                        //obj_CompDet_04.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        //Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_04.ctacc_icod_cuenta_contable).ToList();
                        //Lista.ForEach(Obe =>
                        //{
                        //    obj_CompDet_04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        //    obj_CompDet_04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        //    obj_CompDet_04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        //    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        //    {
                        //        obj_CompDet_04.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        //        obj_CompDet_04.anad_icod_analitica = x.intAnaliticaCliente;
                        //        obj_CompDet_04.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_04.tablc_iid_tipo_analitica, x.strCodAnaliticaCliente);
                        //    }
                        //});
                        //obj_CompDet_04.vcocd_vglosa_linea = String.Format("PLANILLA NRO: {0}", oBePln.plnc_vnumero_planilla);
                        //obj_CompDet_04.vcocd_nmto_tot_debe_sol = 0;
                        //obj_CompDet_04.vcocd_nmto_tot_haber_sol = Math.Round(x.plnd_nmonto_pagado * Convert.ToDecimal(oBePln.dcmlTipoCambioDol),2);
                        //obj_CompDet_04.vcocd_nmto_tot_debe_dol = 0;
                        //obj_CompDet_04.vcocd_nmto_tot_haber_dol = x.plnd_nmonto_pagado;
                        //obj_CompDet_04.intTipoOperacion = 1;
                        //obj_CompDet_04.vcocd_tipo_cambio = oBePln.dcmlTipoCambioDol;

                        //lstCompDetalle.Add(obj_CompDet_04);
                        //lstDetGeneral.Add(obj_CompDet_04);/***********************************************************/
                        //if (obj_CompDet_04.ctacc_icod_cuenta_debe_auto != null)
                        //{
                        //    var tuple = addCtaAutomatica(obj_CompDet_04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        //    lstCompDetalle = tuple.Item1;
                        //    lstDetGeneral = tuple.Item2;
                        //}
                        //#endregion
                        //#endregion
                    }

                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Desde Bancos...
        private List<EVoucherContableDet> getDetVoucherDxpDxc(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
            List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
            List<EParametroContable> lstParametros)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
            try
            {
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);
                if (Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException(String.Format("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria:{0} <<{1}>>", oBeBC.strBanco, oBeBC.bcod_vnumero_cuenta));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);
                obj_item_CompDet_01.strNroCuenta = oBeBC.strCodCtaContable;
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = 1;
                        obj_item_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", 1, oBeBC.strCodAnalitica);
                    }
                });

                obj_item_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;

                if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                }
                if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                }
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }

                #endregion
                #region detalle n
                lstMovBancoDet = new BTesoreria().ListarEntidadFinancieraDetalle(oBeLB.icod_correlativo);
                lstMovBancoDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;

                    if (x.iid_cuenta_contable > 0)
                    {
                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = 56;//ID DOC VCO                       
                        obj_item_CompDet.vcocd_numero_doc = oBeLB.vnro_documento;
                        obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLB.vnro_documento);
                        obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.iid_cuenta_contable);
                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Obe.ctacc_iccosto)
                                obj_item_CompDet.cecoc_icod_centro_costo = x.icod_centro_costo;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                                obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                            }
                        });

                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        obj_item_CompDet.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                    }
                    else
                    {
                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                        obj_item_CompDet.vcocd_numero_doc = x.vnumero_doc;
                        obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet.vcocd_numero_doc);
                        var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                        if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", x.tdocc_vabreviatura_tipo_doc, x.vnumero_doc));
                        ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];

                        if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                        {
                            if (oBeLB.iid_tipo_moneda == 3)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                            if (oBeLB.iid_tipo_moneda == 4)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            //if (oBeLB.iid_motivo_mov_banco == 106)
                            //{
                            //    obj_item_CompDet.ctacc_icod_cuenta_contable = (x.MonedaDXP == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra); 
                            //}
                            if (oBeLB.iid_motivo_mov_banco == 107)
                            {
                                obj_item_CompDet.ctacc_icod_cuenta_contable = (x.MonedaDXC == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);
                            }

                        }
                        if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                        {
                            if (oBeLB.iid_tipo_moneda == 3)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                            if (oBeLB.iid_tipo_moneda == 4)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                            if (oBeLB.iid_motivo_mov_banco == 106)
                            {
                                obj_item_CompDet.ctacc_icod_cuenta_contable = (x.MonedaDXP == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);
                            }
                        }

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                                obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                                obj_item_CompDet.strAnalisis = x.vdes_analisis;
                            }
                        });

                        obj_item_CompDet.cecoc_icod_centro_costo = null;
                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        obj_item_CompDet.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                    }

                    if (x.mto_mov_soles < 0 || x.mto_mov_dolar < 0)
                    {
                        if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles * -1;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = x.mto_mov_dolar * -1;
                            if (x.mto_retenido_soles < 0)
                                obj_item_CompDet.vcocd_nmto_tot_haber_sol = obj_item_CompDet.vcocd_nmto_tot_haber_sol + (x.mto_retenido_soles * -1);
                            else
                                obj_item_CompDet.vcocd_nmto_tot_haber_sol = obj_item_CompDet.vcocd_nmto_tot_haber_sol + x.mto_retenido_soles;

                            if (x.mto_retenido_soles < 0)
                                obj_item_CompDet.vcocd_nmto_tot_haber_dol = obj_item_CompDet.vcocd_nmto_tot_haber_dol + (x.mto_retenido_dolar * -1);
                            else
                                obj_item_CompDet.subdi_banc_nmto_tot_haber_sol = obj_item_CompDet.vcocd_nmto_tot_haber_dol + x.mto_retenido_dolar;
                        }
                        if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles * -1;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = x.mto_mov_dolar * -1;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                        }
                    }
                    else
                    {
                        if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles + x.mto_retenido_soles;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = x.mto_mov_dolar + x.mto_retenido_dolar;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                        }
                        if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = x.mto_mov_dolar;
                        }
                    }
                    lstCompDetalle.Add(obj_item_CompDet);
                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/
                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });

                if (oBeLB.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                {
                    lstMovBancoDet.ForEach(x =>
                    {

                        if (Convert.ToInt32(x.iid_cuenta_contable) == 0 && x.mto_retenido_soles > 0)
                        {
                            EVoucherContableDet obj_item_CompDet_R = new EVoucherContableDet();
                            obj_item_CompDet_R.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_item_CompDet_R.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                            obj_item_CompDet_R.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);

                            obj_item_CompDet_R.vcocd_numero_doc = x.vnumero_doc;
                            obj_item_CompDet_R.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_R.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_R.vcocd_numero_doc);
                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];
                            if (Convert.ToInt32(lstParametros[0].parac_id_cta_retencion) == 0)
                                throw new ArgumentException("No se encontró la CTA. CONTABLE para RETENCIONES, Favor de registrar la Cta. Contable en <<Registro de Parámetros Contables>>");
                            obj_item_CompDet_R.ctacc_icod_cuenta_contable = Convert.ToInt32(lstParametros[0].parac_id_cta_retencion);
                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_R.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_item_CompDet_R.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_item_CompDet_R.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_item_CompDet_R.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_item_CompDet_R.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                                    obj_item_CompDet_R.anad_icod_analitica = x.icod_analitica;
                                }
                            });
                            obj_item_CompDet_R.cecoc_icod_centro_costo = null;

                            obj_item_CompDet_R.vcocd_vglosa_linea = "RETENCIÓN DEL IGV";
                            obj_item_CompDet_R.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet_R.vcocd_nmto_tot_haber_sol = x.mto_retenido_soles;
                            obj_item_CompDet_R.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet_R.vcocd_nmto_tot_haber_dol = x.mto_retenido_dolar;
                            obj_item_CompDet_R.intTipoOperacion = 1;
                            obj_item_CompDet_R.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                            lstCompDetalle.Add(obj_item_CompDet_R);
                            lstDetGeneral.Add(obj_item_CompDet_R);/**********************************************************/
                            if (obj_item_CompDet_R.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_item_CompDet_R, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                        }
                    });
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherVarios(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
            List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
            List<EParametroContable> lstParametros)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
            try
            {
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLB.vnro_documento);

                if (Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException(String.Format("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria:{0} <<{1}>>", oBeBC.strBanco, oBeBC.bcod_vnumero_cuenta));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_item_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", 1, oBeBC.strCodAnalitica);
                    }

                });

                obj_item_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;                //
                if (oBeLB.cflag_tipo_movimiento == Parametros.intTipoMovimientoAbono)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;

                }
                if (oBeLB.cflag_tipo_movimiento == Parametros.intTipoMovimientoCargo)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                }
                //
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/

                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle n
                lstMovBancoDet = new BTesoreria().ListarEntidadFinancieraDetalle(oBeLB.icod_correlativo);
                lstMovBancoDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet.tdocc_icod_tipo_doc = 56;//ID DOC VCO
                    obj_item_CompDet.vcocd_numero_doc = oBeLB.vnro_documento;
                    obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLB.vnro_documento);
                    obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.iid_cuenta_contable);
                    Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Obe.ctacc_iccosto)
                        {
                            obj_item_CompDet.cecoc_icod_centro_costo = x.icod_centro_costo;
                        }
                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        {
                            obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                            obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                            obj_item_CompDet.strAnalisis = x.vdes_analisis;
                        }
                    });

                    obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                    obj_item_CompDet.intTipoOperacion = 1;
                    obj_item_CompDet.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                    if (oBeLB.cflag_tipo_movimiento == Parametros.intTipoMovimientoAbono)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = Math.Round((x.mto_mov_soles / oBeLB.nmonto_tipo_cambio), 2);

                    }
                    if (oBeLB.cflag_tipo_movimiento == Parametros.intTipoMovimientoCargo)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = Math.Round((x.mto_mov_soles / oBeLB.nmonto_tipo_cambio), 2);
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;

                    }

                    lstCompDetalle.Add(obj_item_CompDet);
                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/
                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherADP(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
           List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
           List<EParametroContable> lstParametros)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
            try
            {
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);
                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_item_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                    }
                });
                obj_item_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/

                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }

                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoProveedor;
                obj_item_CompDet_02.vcocd_numero_doc = oBeLB.inumero_orden;
                var objTipoDoc = lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0];
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", objTipoDoc.tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);
                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocAdelantoProveedor);
                ETipoDocumentoDetalleCta objClaseDoc = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocAdelantoProveedor).ToList()[0];

                var lstProveedor = (new ComprasData()).ListarProveedor();
                EProveedor obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == oBeLB.proc_icod_proveedor).ToList()[0];

                if (oBeLB.iid_tipo_moneda == 3)
                    if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));

                if (oBeLB.iid_tipo_moneda == 4)
                    if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));
                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeLB.iid_tipo_moneda == 3) ? Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra);
                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_02.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = oBeLB.vglosa;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;

                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/

                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }

                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherADC(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
            List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
            List<EParametroContable> lstParametros)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

            try
            {
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;

                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                obj_item_CompDet_02.vcocd_numero_doc = oBeLB.inumero_orden;
                var objTipoDoc = lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0];
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", objTipoDoc.tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);
                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocAdelantoCliente);
                ETipoDocumentoDetalleCta objClaseDoc = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocAdelantoCliente).ToList()[0];

                var lstCliente = (new VentasData()).ListarCliente();
                ECliente objCliente = lstCliente.Where(d => d.cliec_icod_cliente == oBeLB.cliec_icod_cliente).ToList()[0];

                if (oBeLB.iid_tipo_moneda == 3)
                    if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));

                if (oBeLB.iid_tipo_moneda == 4)
                    if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));
                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeLB.iid_tipo_moneda == 3) ? Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_02.anad_icod_analitica = objCliente.anac_icod_analitica;
                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = oBeLB.vglosa;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/

                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherPagoADPNCP(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
           List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
           List<EParametroContable> lstParametros)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
            try
            {
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                obj_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                    }
                });

                obj_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;

                obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 1) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 2) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;

                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/

                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle n
                lstMovBancoDet = new BTesoreria().ListarEntidadFinancieraDetalleADPNCP(oBeLB.iid_correlativo);
                lstMovBancoDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;

                    if (x.iid_cuenta_contable > 0)
                    {
                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = 56;//ID DOC VCO                        
                        obj_item_CompDet.vcocd_numero_doc = oBeLB.vnro_documento;
                        obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                        obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.iid_cuenta_contable);
                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Obe.ctacc_iccosto)
                                obj_item_CompDet.cecoc_icod_centro_costo = x.icod_centro_costo;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                                obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                            }
                        });

                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        obj_item_CompDet.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                    }
                    else
                    {

                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                        obj_item_CompDet.vcocd_numero_doc = x.vnumero_doc;
                        obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                        var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                        if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", x.tdocc_vabreviatura_tipo_doc, x.vnumero_doc));

                        ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];
                        var lstProveedor = (new ComprasData()).ListarProveedor();
                        EProveedor obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == x.mobdc_icod_proveedor).ToList()[0];

                        if (oBeLB.iid_tipo_moneda == 1)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        if (oBeLB.iid_tipo_moneda == 2)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                        obj_item_CompDet.ctacc_icod_cuenta_contable = (oBeLB.iid_tipo_moneda == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                                obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                            }
                        });
                        obj_item_CompDet.cecoc_icod_centro_costo = null;
                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        obj_item_CompDet.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                    }

                    if (x.mto_mov_soles < 0 || x.mto_mov_dolar < 0)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles * -1;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = x.mto_mov_dolar * -1;
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                    }
                    else
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = x.mto_mov_dolar;
                    }

                    lstCompDetalle.Add(obj_item_CompDet);
                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/

                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });

                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private List<EVoucherContableDet> getDetVoucherPagoADCNCC(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
           List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
           List<EParametroContable> lstParametros)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
            try
            {
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                    }
                });

                obj_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 1) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 2) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }

                #endregion
                #region detalle n
                lstMovBancoDet = new BTesoreria().ListarEntidadFinancieraDetalleADPNCP_Cliente(oBeLB.iid_correlativo);
                lstMovBancoDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;

                    if (x.iid_cuenta_contable > 0)
                    {
                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = 56;//ID DOC VCO
                        obj_item_CompDet.vcocd_numero_doc = oBeLB.vnro_documento;
                        obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                        obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.iid_cuenta_contable);
                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Obe.ctacc_iccosto)
                                obj_item_CompDet.cecoc_icod_centro_costo = x.icod_centro_costo;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                                obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                            }
                        });


                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        obj_item_CompDet.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                    }
                    else
                    {

                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                        obj_item_CompDet.vcocd_numero_doc = x.vnumero_doc;
                        obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet.vcocd_numero_doc);

                        var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                        if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", x.tdocc_vabreviatura_tipo_doc, x.vnumero_doc));
                        ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];

                        var lstCliente = (new VentasData()).ListarCliente();
                        ECliente objCliente = lstCliente.Where(d => d.cliec_icod_cliente == x.mobdc_icod_cliente).ToList()[0];

                        if (oBeLB.iid_tipo_moneda == 1)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        if (oBeLB.iid_tipo_moneda == 2)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", x.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                        obj_item_CompDet.ctacc_icod_cuenta_contable = (oBeLB.iid_tipo_moneda == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                                obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                            }
                        });

                        obj_item_CompDet.cecoc_icod_centro_costo = null;
                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        obj_item_CompDet.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                    }

                    if (x.mto_mov_soles < 0 || x.mto_mov_dolar < 0)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles * -1;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = x.mto_mov_dolar * -1;
                    }
                    else
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = x.mto_mov_dolar;
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                    }

                    lstCompDetalle.Add(obj_item_CompDet);
                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/

                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });

                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private List<EVoucherContableDet> getDetVoucherTransferencia(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC, EBancoCuenta oBeBCTransf,
            List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
            List<EParametroContable> lstParametros)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
            try
            {
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLB.vnro_documento);

                if (Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException(String.Format("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria:{0} <<{1}>>", oBeBC.strBanco, oBeBC.bcod_vnumero_cuenta));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_item_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", 1, oBeBC.strCodAnalitica);
                    }
                });

                obj_item_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/

                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_02.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLB.vnro_documento);

                if (Convert.ToInt32(oBeBCTransf.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException(String.Format("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria:{0} <<{1}>>", oBeBCTransf.strBanco, oBeBCTransf.bcod_vnumero_cuenta));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBCTransf.ctacc_icod_cuenta_contable);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_item_CompDet_02.anad_icod_analitica = oBeBCTransf.anad_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", 1, oBeBCTransf.strCodAnalitica);
                    }

                });

                obj_item_CompDet_02.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/

                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Desde Compras...
        private List<EVoucherContableDet> getDetVoucherDxpManual(EVoucherContableCab oBe, EDocPorPagar oBeDXP, List<ECuentaContable> lstPlanCuentas,
            List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            CuentasPorPagarData objCuentasPorPagar = new CuentasPorPagarData();
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                var lstParametroCont = objContabilidadData.listarParametroContable();
                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(oBeDXP.tdocc_icod_tipo_doc);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", oBeDXP.tdocc_vabreviatura_tipo_doc, oBeDXP.doxpc_vnumero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList()[0];

                #region detalle 01
                var lstDetalle = objCuentasPorPagar.listarDXPDetCtaContable(oBeDXP.doxpc_icod_correlativo);
                lstDetalle.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                    obj_item_CompDet.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet.vcocd_numero_doc);
                    obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_iid_cuenta_contable);
                    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Obe.ctacc_iccosto)
                            obj_item_CompDet.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        {
                            obj_item_CompDet.tablc_iid_tipo_analitica = Convert.ToInt32(x.IdTipoAnalitica);
                            obj_item_CompDet.anad_icod_analitica = x.anac_icod_analitica;
                        }
                    });

                    obj_item_CompDet.vcocd_vglosa_linea = (x.cdxpc_vglosa != null) ? x.cdxpc_vglosa.ToUpper() : "";
                    obj_item_CompDet.intTipoOperacion = 1;
                    obj_item_CompDet.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;
                    if (x.cdxpc_nmonto_cuenta > 0)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? x.cdxpc_nmonto_cuenta : Math.Round(x.cdxpc_nmonto_cuenta * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? x.cdxpc_nmonto_cuenta : Math.Round(x.cdxpc_nmonto_cuenta / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                    }
                    if (x.cdxpc_nmonto_cuenta < 0)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? (x.cdxpc_nmonto_cuenta * -1) : Math.Round((x.cdxpc_nmonto_cuenta * -1) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? (x.cdxpc_nmonto_cuenta * -1) : Math.Round((x.cdxpc_nmonto_cuenta * -1) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                    }
                    obj_item_CompDet.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_item_CompDet);
                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/
                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region detalle 02
                decimal dcmlSumaIGV = 0;
                dcmlSumaIGV = Convert.ToDecimal(oBeDXP.doxpc_nmonto_imp_destino_gravado) +
                    Convert.ToDecimal(oBeDXP.doxpc_nmonto_imp_destino_mixto);
                if (dcmlSumaIGV > 0)
                {
                    EVoucherContableDet obj_item_CompDet_IGV = new EVoucherContableDet();
                    obj_item_CompDet_IGV.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_IGV.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_IGV.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                    obj_item_CompDet_IGV.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet_IGV.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_IGV.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_IGV.vcocd_numero_doc);

                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable IGV de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                    obj_item_CompDet_IGV.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac);
                    var Lista_N = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_IGV.ctacc_icod_cuenta_contable).ToList();
                    Lista_N.ForEach(Obe =>
                    {
                        obj_item_CompDet_IGV.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_IGV.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_IGV.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    //Inicio// Los siguientes campos tiene el valor de null, por especificación de desarrollo
                    obj_item_CompDet_IGV.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_IGV.tablc_iid_tipo_analitica = null;
                    obj_item_CompDet_IGV.anad_icod_analitica = null;
                    //Fin//
                    obj_item_CompDet_IGV.vcocd_vglosa_linea = (oBeDXP.doxpc_vdescrip_transaccion != null) ? oBeDXP.doxpc_vdescrip_transaccion.ToUpper() : "";
                    obj_item_CompDet_IGV.intTipoOperacion = 1;
                    obj_item_CompDet_IGV.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet_IGV.vcocd_nmto_tot_debe_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? dcmlSumaIGV : Math.Round(dcmlSumaIGV * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_IGV.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_IGV.vcocd_nmto_tot_debe_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? dcmlSumaIGV : Math.Round(dcmlSumaIGV / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_IGV.vcocd_nmto_tot_haber_dol = 0;
                    obj_item_CompDet_IGV.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_item_CompDet_IGV);
                    lstDetGeneral.Add(obj_item_CompDet_IGV);/***********************************************************/
                    if (obj_item_CompDet_IGV.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_IGV, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                }
                #endregion
                #region detalle 03
                if (Convert.ToDecimal(oBeDXP.doxpc_nmonto_isc) > 0)
                {
                    EVoucherContableDet obj_item_CompDet_N = new EVoucherContableDet();
                    obj_item_CompDet_N.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_N.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_N.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                    obj_item_CompDet_N.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet_N.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_N.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_N.vcocd_numero_doc);

                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_isc) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable ISC de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                    obj_item_CompDet_N.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_isc);
                    var Lista_N = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_N.ctacc_icod_cuenta_contable).ToList();
                    Lista_N.ForEach(Obe =>
                    {
                        obj_item_CompDet_N.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_N.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_N.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    //Inicio// Los siguientes campos tiene el valor de null, por especificación de desarrollo
                    obj_item_CompDet_N.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_N.tablc_iid_tipo_analitica = null;
                    obj_item_CompDet_N.anad_icod_analitica = null;
                    //Fin
                    obj_item_CompDet_N.vcocd_vglosa_linea = oBeDXP.doxpc_vdescrip_transaccion.ToUpper();
                    obj_item_CompDet_N.intTipoOperacion = 1;
                    obj_item_CompDet_N.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet_N.vcocd_nmto_tot_debe_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? oBeDXP.doxpc_nmonto_isc : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_isc) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_N.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_N.vcocd_nmto_tot_debe_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? oBeDXP.doxpc_nmonto_isc : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_isc) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_N.vcocd_nmto_tot_haber_dol = 0;
                    obj_item_CompDet_N.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_item_CompDet_N);
                    lstDetGeneral.Add(obj_item_CompDet_N);/***********************************************************/
                    if (obj_item_CompDet_N.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_N, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                }
                #endregion
                #region detalle 04
                EVoucherContableDet obj_item_CompDet_U = new EVoucherContableDet();
                obj_item_CompDet_U.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_U.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_U.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                obj_item_CompDet_U.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                obj_item_CompDet_U.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_U.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_U.vcocd_numero_doc);

                var lstProveedor = (new ComprasData()).ListarProveedor();
                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBeDXP.proc_icod_proveedor).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_item_CompDet_U.strTipNroDocumento));
                EProveedor obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == oBeDXP.proc_icod_proveedor).ToList()[0];

                if (oBeDXP.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeDXP.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_U.ctacc_icod_cuenta_contable = (oBeDXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista_U = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_U.ctacc_icod_cuenta_contable).ToList();
                Lista_U.ForEach(Obe =>
                {
                    obj_item_CompDet_U.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_U.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_U.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_U.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_U.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                    }
                });
                obj_item_CompDet_U.cecoc_icod_centro_costo = null;
                obj_item_CompDet_U.vcocd_vglosa_linea = (oBeDXP.doxpc_vdescrip_transaccion != null) ? oBeDXP.doxpc_vdescrip_transaccion.ToUpper() : "";
                obj_item_CompDet_U.intTipoOperacion = 1;
                obj_item_CompDet_U.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                obj_item_CompDet_U.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_U.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_U.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_U.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_U.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                lstCompDetalle.Add(obj_item_CompDet_U);
                lstDetGeneral.Add(obj_item_CompDet_U);/***********************************************************/
                if (obj_item_CompDet_U.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_U, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 05
                if (Convert.ToDecimal(oBeDXP.doxpc_nmonto_retencion_rh) > 0)
                {
                    EVoucherContableDet obj_item_CompDet_N = new EVoucherContableDet();
                    obj_item_CompDet_N.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_N.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_N.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                    obj_item_CompDet_N.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet_N.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_N.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_N.vcocd_numero_doc);

                    if (Convert.ToInt32(lstParametros[0].parac_id_cta_4ta_cat) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable 4ta Categoria en Parametros contables"));

                    obj_item_CompDet_N.ctacc_icod_cuenta_contable = Convert.ToInt32(lstParametros[0].parac_id_cta_4ta_cat);
                    var Lista_N = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_N.ctacc_icod_cuenta_contable).ToList();
                    Lista_N.ForEach(Obe =>
                    {
                        obj_item_CompDet_N.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_N.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_N.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    //Inicio// Los siguientes campos tiene el valor de null, por especificación de desarrollo
                    obj_item_CompDet_N.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_N.tablc_iid_tipo_analitica = null;
                    obj_item_CompDet_N.anad_icod_analitica = null;
                    //Fin
                    obj_item_CompDet_N.vcocd_vglosa_linea = oBeDXP.doxpc_vdescrip_transaccion.ToUpper();
                    obj_item_CompDet_N.intTipoOperacion = 1;
                    obj_item_CompDet_N.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet_N.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_N.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? oBeDXP.doxpc_nmonto_retencion_rh : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_retencion_rh) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_N.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_N.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? oBeDXP.doxpc_nmonto_retencion_rh : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_retencion_rh) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_N.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_item_CompDet_N);
                    lstDetGeneral.Add(obj_item_CompDet_N);/***********************************************************/
                    if (obj_item_CompDet_N.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_N, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion

                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpRho(EVoucherContableCab oBe, EDocPorPagar oBeDXP, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            CuentasPorPagarData objCuentasPorPagar = new CuentasPorPagarData();
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(oBeDXP.tdocc_icod_tipo_doc);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", oBeDXP.tdocc_vabreviatura_tipo_doc, oBeDXP.doxpc_vnumero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList()[0];
                #region detalle 01
                var lstDetalle = objCuentasPorPagar.listarDXPDetCtaContable(oBeDXP.doxpc_icod_correlativo);
                lstDetalle.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                    obj_item_CompDet.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet.vcocd_numero_doc);
                    obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_iid_cuenta_contable);
                    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Obe.ctacc_iccosto)
                            obj_item_CompDet.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        {
                            obj_item_CompDet.tablc_iid_tipo_analitica = Convert.ToInt32(x.IdTipoAnalitica);
                            obj_item_CompDet.anad_icod_analitica = x.anac_icod_analitica;
                        }
                    });

                    obj_item_CompDet.vcocd_vglosa_linea = (x.cdxpc_vglosa != null) ? x.cdxpc_vglosa.ToUpper() : "";
                    obj_item_CompDet.intTipoOperacion = 1;
                    obj_item_CompDet.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet.vcocd_nmto_tot_debe_sol = (oBeDXP.tablc_iid_tipo_moneda == 1) ? x.cdxpc_nmonto_cuenta : Math.Round(x.cdxpc_nmonto_cuenta * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet.vcocd_nmto_tot_debe_dol = (oBeDXP.tablc_iid_tipo_moneda == 2) ? x.cdxpc_nmonto_cuenta : Math.Round(x.cdxpc_nmonto_cuenta / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;

                    lstCompDetalle.Add(obj_item_CompDet);
                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/
                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region detalle 02
                if (Convert.ToDecimal(oBeDXP.doxpc_nmonto_retencion_rh) > 0)
                {
                    EVoucherContableDet obj_item_CompDet_N = new EVoucherContableDet();
                    obj_item_CompDet_N.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_N.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_N.tdocc_icod_tipo_doc = Parametros.intTipoDocReciboPorHonorarios;
                    obj_item_CompDet_N.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet_N.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_N.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_N.vcocd_numero_doc);
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable IGV de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));
                    obj_item_CompDet_N.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac);
                    var Lista_N = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_N.ctacc_icod_cuenta_contable).ToList();
                    Lista_N.ForEach(Obe =>
                    {
                        obj_item_CompDet_N.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_N.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_N.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    obj_item_CompDet_N.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_N.tablc_iid_tipo_analitica = null;
                    obj_item_CompDet_N.anad_icod_analitica = null;
                    obj_item_CompDet_N.vcocd_vglosa_linea = oBeDXP.doxpc_vdescrip_transaccion;
                    obj_item_CompDet_N.intTipoOperacion = 1;
                    obj_item_CompDet_N.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet_N.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_N.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 1) ? oBeDXP.doxpc_nmonto_retencion_rh : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_retencion_rh) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_N.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_N.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 2) ? oBeDXP.doxpc_nmonto_retencion_rh : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_retencion_rh) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);

                    lstCompDetalle.Add(obj_item_CompDet_N);
                    lstDetGeneral.Add(obj_item_CompDet_N);/***********************************************************/
                    if (obj_item_CompDet_N.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_N, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                }
                #endregion
                #region detalle 03
                EVoucherContableDet obj_item_CompDet_U = new EVoucherContableDet();
                obj_item_CompDet_U.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_U.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_U.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                obj_item_CompDet_U.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                obj_item_CompDet_U.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_U.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_U.vcocd_numero_doc);

                var lstProveedor = (new ComprasData()).ListarProveedor();

                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBeDXP.proc_icod_proveedor).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_item_CompDet_U.strTipNroDocumento));

                EProveedor obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == oBeDXP.proc_icod_proveedor).ToList()[0];

                if (oBeDXP.tablc_iid_tipo_moneda == 1)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));


                if (oBeDXP.tablc_iid_tipo_moneda == 2)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));


                obj_item_CompDet_U.ctacc_icod_cuenta_contable = (oBeDXP.tablc_iid_tipo_moneda == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);
                var Lista_U = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_U.ctacc_icod_cuenta_contable).ToList();
                Lista_U.ForEach(Obe =>
                {
                    obj_item_CompDet_U.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_U.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_U.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_U.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_U.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                    }
                });
                obj_item_CompDet_U.cecoc_icod_centro_costo = null;
                obj_item_CompDet_U.vcocd_vglosa_linea = (oBeDXP.doxpc_vdescrip_transaccion != null) ? oBeDXP.doxpc_vdescrip_transaccion.ToUpper() : "";
                obj_item_CompDet_U.intTipoOperacion = 1;
                obj_item_CompDet_U.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                obj_item_CompDet_U.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_U.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 1) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_U.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_U.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 2) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);

                lstCompDetalle.Add(obj_item_CompDet_U);
                lstDetGeneral.Add(obj_item_CompDet_U);/***********************************************************/
                if (obj_item_CompDet_U.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_U, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpNcp(EVoucherContableCab oBe, EDocPorPagar oBeDXP, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            CuentasPorPagarData objCuentasPorPagar = new CuentasPorPagarData();
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(oBeDXP.tdocc_icod_tipo_doc);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", oBeDXP.tdocc_vabreviatura_tipo_doc, oBeDXP.doxpc_vnumero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList()[0];
                #region detalle 01
                var lstDetalle = objCuentasPorPagar.listarDXPDetCtaContable(oBeDXP.doxpc_icod_correlativo);
                lstDetalle.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                    obj_item_CompDet.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet.vcocd_numero_doc);
                    obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_iid_cuenta_contable);
                    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Obe.ctacc_iccosto)
                            obj_item_CompDet.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo;
                        //if (Convert.ToInt32(oBeDXP.anac_icod_analitica) > 0)
                        //{
                        //    obj_item_CompDet.tablc_iid_tipo_analitica = Convert.ToInt32(oBeDXP.TipoAnalitica);
                        //    obj_item_CompDet.anad_icod_analitica = oBeDXP.anac_icod_analitica;
                        //    obj_item_CompDet.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet.tablc_iid_tipo_analitica, oBeDXP.NumAnalitica);
                        //}
                    });

                    obj_item_CompDet.vcocd_vglosa_linea = (x.cdxpc_vglosa != null) ? x.cdxpc_vglosa.ToUpper() : "";
                    obj_item_CompDet.intTipoOperacion = 1;
                    obj_item_CompDet.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? x.cdxpc_nmonto_cuenta : Math.Round(x.cdxpc_nmonto_cuenta * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? x.cdxpc_nmonto_cuenta : Math.Round(x.cdxpc_nmonto_cuenta / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    /*txt*/
                    obj_item_CompDet.doxpc_icod_correlativo = Convert.ToInt32(x.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_item_CompDet);
                    lstDetGeneral.Add(obj_item_CompDet);/***********************************************************/
                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region detalle 02
                decimal sumIGV = 0;
                sumIGV = Convert.ToDecimal(oBeDXP.doxpc_nmonto_imp_destino_gravado) +
                    Convert.ToDecimal(oBeDXP.doxpc_nmonto_imp_destino_mixto) +
                    Convert.ToDecimal(oBeDXP.doxpc_nmonto_imp_destino_nogravado);
                if (sumIGV > 0)
                {
                    EVoucherContableDet obj_item_CompDet_IGV = new EVoucherContableDet();
                    obj_item_CompDet_IGV.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_IGV.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_IGV.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                    obj_item_CompDet_IGV.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    obj_item_CompDet_IGV.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_IGV.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_IGV.vcocd_numero_doc);

                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable IGV de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                    obj_item_CompDet_IGV.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac);
                    var Lista_N = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_IGV.ctacc_icod_cuenta_contable).ToList();
                    Lista_N.ForEach(Obe =>
                    {
                        obj_item_CompDet_IGV.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_IGV.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    obj_item_CompDet_IGV.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_IGV.tablc_iid_tipo_analitica = null;
                    obj_item_CompDet_IGV.anad_icod_analitica = null;
                    obj_item_CompDet_IGV.vcocd_vglosa_linea = (oBeDXP.doxpc_vdescrip_transaccion != null) ? oBeDXP.doxpc_vdescrip_transaccion.ToUpper() : "";
                    obj_item_CompDet_IGV.intTipoOperacion = 1;
                    obj_item_CompDet_IGV.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet_IGV.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_IGV.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? sumIGV : Math.Round(sumIGV * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_IGV.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_IGV.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? sumIGV : Math.Round(sumIGV / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    /*txt*/
                    obj_item_CompDet_IGV.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_item_CompDet_IGV);
                    lstDetGeneral.Add(obj_item_CompDet_IGV);/***********************************************************/
                    if (obj_item_CompDet_IGV.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_IGV, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                }
                #endregion
                #region detalle 03
                if (Convert.ToDecimal(oBeDXP.doxpc_nmonto_isc) > 0)
                {
                    EVoucherContableDet obj_item_CompDet_ISC = new EVoucherContableDet();
                    obj_item_CompDet_ISC.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_ISC.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_ISC.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;

                    obj_item_CompDet_ISC.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_isc) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable ISC de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                    obj_item_CompDet_ISC.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_isc);
                    var Lista_N = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_ISC.ctacc_icod_cuenta_contable).ToList();
                    Lista_N.ForEach(Obe =>
                    {
                        obj_item_CompDet_ISC.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_ISC.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_ISC.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    obj_item_CompDet_ISC.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_ISC.tablc_iid_tipo_analitica = null;
                    obj_item_CompDet_ISC.anad_icod_analitica = null;
                    obj_item_CompDet_ISC.vcocd_vglosa_linea = oBeDXP.doxpc_vdescrip_transaccion.ToUpper();
                    obj_item_CompDet_ISC.intTipoOperacion = 1;
                    obj_item_CompDet_ISC.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet_ISC.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_ISC.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? oBeDXP.doxpc_nmonto_isc : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_isc) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_ISC.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_ISC.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? oBeDXP.doxpc_nmonto_isc : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_isc) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                    obj_item_CompDet_ISC.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_item_CompDet_ISC);
                    lstDetGeneral.Add(obj_item_CompDet_ISC);/***********************************************************/
                    if (obj_item_CompDet_ISC.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_ISC, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                }
                #endregion
                #region detalle 04
                EVoucherContableDet obj_item_CompDet_U = new EVoucherContableDet();
                obj_item_CompDet_U.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_U.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_U.tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc;
                obj_item_CompDet_U.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                obj_item_CompDet_U.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_U.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_U.vcocd_numero_doc);

                /**/
                var lstProveedor = (new ComprasData()).ListarProveedor();

                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBeDXP.proc_icod_proveedor).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_item_CompDet_U.strTipNroDocumento));

                EProveedor obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == oBeDXP.proc_icod_proveedor).ToList()[0];

                if (oBeDXP.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeDXP.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", oBeDXP.tdocc_vabreviatura_tipo_doc, obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_U.ctacc_icod_cuenta_contable = (oBeDXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista_U = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_U.ctacc_icod_cuenta_contable).ToList();
                Lista_U.ForEach(Obe =>
                {
                    obj_item_CompDet_U.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_U.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_U.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(oBeDXP.anac_icod_analitica) > 0)
                    {
                        obj_item_CompDet_U.tablc_iid_tipo_analitica = Convert.ToInt32(oBeDXP.TipoAnalitica);
                        obj_item_CompDet_U.anad_icod_analitica = oBeDXP.anac_icod_analitica;
                        obj_item_CompDet_U.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_U.tablc_iid_tipo_analitica, oBeDXP.NumAnalitica);
                    }
                });

                obj_item_CompDet_U.cecoc_icod_centro_costo = null;
                obj_item_CompDet_U.vcocd_vglosa_linea = (oBeDXP.doxpc_vdescrip_transaccion != null) ? oBeDXP.doxpc_vdescrip_transaccion.ToUpper() : "";
                obj_item_CompDet_U.intTipoOperacion = 1;
                obj_item_CompDet_U.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                obj_item_CompDet_U.vcocd_nmto_tot_debe_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) * Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_U.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_U.vcocd_nmto_tot_debe_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) / Convert.ToDecimal(oBeDXP.doxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_U.vcocd_nmto_tot_haber_dol = 0;
                /*TXT*/
                obj_item_CompDet_U.doxpc_icod_correlativo = Convert.ToInt32(oBeDXP.doxpc_icod_correlativo);
                lstCompDetalle.Add(obj_item_CompDet_U);
                lstDetGeneral.Add(obj_item_CompDet_U);/***********************************************************/
                if (obj_item_CompDet_U.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_U, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpCanjeAdelanto(EVoucherContableCab oBe, EDocxPagarPagoAdelanto oBePagoAd, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            CuentasPorPagarData objCuentasPorPagar = new CuentasPorPagarData();
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoProveedor;
                obj_item_CompDet_01.vcocd_numero_doc = oBePagoAd.vnumero_adelanto;//nro. de doc. del adelanto
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);
                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_01.tdocc_icod_tipo_doc);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePagoAd.Vcorrelativo_adelanto).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", Parametros.intTipoDocAdelantoProveedor, oBePagoAd.vnumero_adelanto));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBePagoAd.Vcorrelativo_adelanto).ToList()[0];

                var lstProveedor = (new ComprasData()).ListarProveedor();

                if (lstProveedor.Where(a => a.iid_icod_proveedor == Convert.ToInt32(oBePagoAd.intProveedor)).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_item_CompDet_01.strTipNroDocumento));

                EProveedor obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == Convert.ToInt32(oBePagoAd.intProveedor)).ToList()[0];

                if (oBePagoAd.id_tipo_moneda_adelanto == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBePagoAd.id_tipo_moneda_adelanto == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = (oBePagoAd.id_tipo_moneda_adelanto == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_01.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_01.cecoc_icod_centro_costo = null;
                obj_item_CompDet_01.vcocd_vglosa_linea = (oBePagoAd.adpap_vdescripcion == "" || oBePagoAd.adpap_vdescripcion == null) ? "CANJE CON ADELANTO" : oBePagoAd.adpap_vdescripcion.ToUpper();
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBePagoAd.adpap_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBePagoAd.id_tipo_moneda_adelanto == 3) ? oBePagoAd.adpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.adpap_nmonto_pago) * Convert.ToDecimal(oBePagoAd.adpap_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBePagoAd.id_tipo_moneda_adelanto == 4) ? oBePagoAd.adpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.adpap_nmonto_pago) / Convert.ToDecimal(oBePagoAd.adpap_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.doxpc_icod_correlativo = Convert.ToInt32(oBePagoAd.doxpc_icod_correlativo_adelanto);
                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = oBePagoAd.tdocc_icod_tipo_doc;
                obj_item_CompDet_02.vcocd_numero_doc = oBePagoAd.vnumero_pago;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);
                var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == oBePagoAd.Vcorrelativo_Pago).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", oBePagoAd.AbreviaturaAdelanto, oBePagoAd.vnumero_pago));
                ETipoDocumentoDetalleCta obj_DocumentoClase2 = lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == oBePagoAd.Vcorrelativo_Pago).ToList()[0];


                var lstProveedorDXP = (new ComprasData()).ListarProveedor();

                if (lstProveedorDXP.Where(a => a.iid_icod_proveedor == Convert.ToInt32(oBePagoAd.intProveedorDXP)).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_item_CompDet_02.strTipNroDocumento));

                EProveedor obj_ProveedorDXP = lstProveedorDXP.Where(a => a.iid_icod_proveedor == Convert.ToInt32(oBePagoAd.intProveedorDXP)).ToList()[0];





                if (oBePagoAd.id_tipo_moneda_pago == 3)
                    if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                if (oBePagoAd.id_tipo_moneda_pago == 4)
                    if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBePagoAd.id_tipo_moneda_pago == 3) ? Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_02.anad_icod_analitica = obj_ProveedorDXP.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_ProveedorDXP.anac_iid_analitica);

                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = (oBePagoAd.adpap_vdescripcion != null) ? oBePagoAd.adpap_vdescripcion.ToUpper() : "CANJE CON ADELANTO";
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBePagoAd.adpap_nmonto_tipo_cambio;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBePagoAd.id_tipo_moneda_adelanto == 3) ? oBePagoAd.adpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.adpap_nmonto_pago) * Convert.ToDecimal(oBePagoAd.adpap_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBePagoAd.id_tipo_moneda_adelanto == 4) ? oBePagoAd.adpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.adpap_nmonto_pago) / Convert.ToDecimal(oBePagoAd.adpap_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_02.doxpc_icod_correlativo = Convert.ToInt32(oBePagoAd.doxpc_icod_correlativo_pago);
                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private List<EVoucherContableDet> getDetVoucherDxpCanjeNotaCredito(EVoucherContableCab oBe, EDocPorPagarNotaCredito oBePagoAd, List<ECuentaContable> lstPlanCuentas,
            List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor;
                obj_item_CompDet_01.vcocd_numero_doc = oBePagoAd.doc_vnumero_nota_credito;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_01.tdocc_icod_tipo_doc);

                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBePagoAd.idd_correlativo_nota_credito)).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), oBePagoAd.doc_vnumero_nota_credito));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBePagoAd.idd_correlativo_nota_credito)).ToList()[0];

                EProveedor obj_Proveedor = (new ComprasData()).ListarProveedor().Where(a => a.iid_icod_proveedor == oBePagoAd.intProveedor).ToList()[0];

                if (oBePagoAd.iid_moneda_nota_credito == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBePagoAd.iid_moneda_nota_credito == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = (oBePagoAd.iid_moneda_nota_credito == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_01.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_01.tablc_iid_tipo_analitica, obj_Proveedor.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_01.cecoc_icod_centro_costo = null;
                obj_item_CompDet_01.vcocd_vglosa_linea = (oBePagoAd.ncpap_vdescripcion == "" || oBePagoAd.ncpap_vdescripcion == null) ? String.Format("CANJE CON N/C {0}", oBePagoAd.doc_vnumero_nota_credito) : oBePagoAd.ncpap_vdescripcion.ToUpper();
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBePagoAd.ncpap_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBePagoAd.iid_moneda_nota_credito == 3) ? oBePagoAd.ncpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.ncpap_nmonto_pago) * Convert.ToDecimal(oBePagoAd.ncpap_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBePagoAd.iid_moneda_nota_credito == 4) ? oBePagoAd.ncpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.ncpap_nmonto_pago) / Convert.ToDecimal(oBePagoAd.ncpap_nmonto_tipo_cambio), 2);

                obj_item_CompDet_01.doxpc_icod_correlativo = Convert.ToInt32(oBePagoAd.doxpc_icod_correlativo_nota_credito);
                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = oBePagoAd.tipo_documento_pago;
                obj_item_CompDet_02.vcocd_numero_doc = oBePagoAd.doc_vnumero_pago;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);

                var lstDocumentoClase_02 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBePagoAd.idd_correlativo_pago).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), oBePagoAd.doc_vnumero_pago));
                obj_DocumentoClase = lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBePagoAd.idd_correlativo_pago).ToList()[0];

                if (oBePagoAd.iid_moneda_pago == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBePagoAd.iid_moneda_pago == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBePagoAd.iid_moneda_pago == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_02.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_02.tablc_iid_tipo_analitica, obj_Proveedor.anac_iid_analitica);
                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = (oBePagoAd.ncpap_vdescripcion == "" || oBePagoAd.ncpap_vdescripcion == null) ? String.Format("CANJE CON N/C {0}", oBePagoAd.doc_vnumero_nota_credito) : oBePagoAd.ncpap_vdescripcion.ToUpper();
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBePagoAd.ncpap_nmonto_tipo_cambio;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBePagoAd.iid_moneda_nota_credito == 3) ? oBePagoAd.ncpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.ncpap_nmonto_pago) * Convert.ToDecimal(oBePagoAd.ncpap_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBePagoAd.iid_moneda_nota_credito == 4) ? oBePagoAd.ncpap_nmonto_pago : Math.Round(Convert.ToDecimal(oBePagoAd.ncpap_nmonto_pago) / Convert.ToDecimal(oBePagoAd.ncpap_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_02.doxpc_icod_correlativo = Convert.ToInt32(oBePagoAd.doxpc_icod_correlativo_pago);
                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpCanjeDxc(EVoucherContableCab oBe, EDocPorPagarPago oBePago, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                EDocXCobrarDocxPagarCanje oBeCanje = new BCuentasPorPagar().ListaDatosCanjexIcodDxpPago(oBePago.pdxpc_icod_correlativo);
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeCanje.tipo_doc_dxc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeCanje.doxcc_vnumero_doc;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_01.tdocc_icod_tipo_doc);

                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeCanje.clase_documento_dxc)).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", oBeCanje.tipo_doc_abr_dxc, oBeCanje.doxcc_vnumero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeCanje.clase_documento_dxc)).ToList()[0];

                if (Convert.ToInt32(oBeCanje.cliec_icod_cliente) == 0)
                    throw new ArgumentException(String.Format("<<Error...>> Código de cliente no válido <<Cje>> <<{0}{1}>>", oBeCanje.tipo_doc_abr_dxc, oBeCanje.doxcc_vnumero_doc));

                ECliente objCliente = objVentasData.ListarCliente().Where(d => d.cliec_icod_cliente == Convert.ToInt32(oBeCanje.cliec_icod_cliente)).ToList()[0];

                if (oBeCanje.tipo_moneda_canje == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));


                if (oBeCanje.tipo_moneda_canje == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = (oBeCanje.tipo_moneda_canje == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_01.anad_icod_analitica = objCliente.anac_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_01.tablc_iid_tipo_analitica, objCliente.anac_iid_analitica);
                    }
                });
                /**/
                obj_item_CompDet_01.cecoc_icod_centro_costo = null;
                obj_item_CompDet_01.vcocd_vglosa_linea = (oBeCanje.canjec_vobservacion == "") ? "CANJE CON " + obj_item_CompDet_01.vcocd_numero_doc : oBeCanje.canjec_vobservacion.ToUpper();
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeCanje.canjec_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeCanje.tipo_moneda_canje == 3) ? oBeCanje.canjec_nmonto_pago : Math.Round(Convert.ToDecimal(oBeCanje.canjec_nmonto_pago) * Convert.ToDecimal(oBeCanje.canjec_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeCanje.tipo_moneda_canje == 4) ? oBeCanje.canjec_nmonto_pago : Math.Round(Convert.ToDecimal(oBeCanje.canjec_nmonto_pago) / Convert.ToDecimal(oBeCanje.canjec_nmonto_tipo_cambio), 2);

                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeCanje.tipo_doc_dxp);
                obj_item_CompDet_02.vcocd_numero_doc = oBeCanje.doxpc_vnumero_doc;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);

                lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeCanje.clase_documento_dxp)).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", oBeCanje.tipo_doc_abr_dxc, oBeCanje.doxcc_vnumero_doc));
                obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Convert.ToInt32(oBeCanje.clase_documento_dxp)).ToList()[0];

                if (oBeCanje.proc_icod_proveedor == 0)
                    throw new ArgumentException(String.Format("<<Error...>> Código de proveedor no válido <<Cje>> <<{0}{1}>>", oBeCanje.tipo_doc_abr_dxp, oBeCanje.doxpc_vnumero_doc));
                EProveedor obj_Proveedor = (new ComprasData()).ListarProveedor().Where(a => a.iid_icod_proveedor == oBeCanje.proc_icod_proveedor).ToList()[0];

                if (oBeCanje.tipo_moneda_canje == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeCanje.tipo_moneda_canje == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeCanje.tipo_moneda_canje == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_02.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_02.tablc_iid_tipo_analitica, obj_Proveedor.anac_iid_analitica);
                    }
                });
                /**/
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = oBeCanje.doxpc_vdescrip_transaccion.ToUpper();
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeCanje.canjec_nmonto_tipo_cambio;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeCanje.tipo_moneda_canje == 3) ? oBeCanje.canjec_nmonto_pago : Math.Round(Convert.ToDecimal(oBeCanje.canjec_nmonto_pago) * Convert.ToDecimal(oBeCanje.canjec_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeCanje.tipo_moneda_canje == 4) ? oBeCanje.canjec_nmonto_pago : Math.Round(Convert.ToDecimal(oBeCanje.canjec_nmonto_pago) / Convert.ToDecimal(oBeCanje.canjec_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;

                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpPagoDirecto(EVoucherContableCab oBe, EDocPorPagarPago oBePago, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBePago.tdocc_icod_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBePago.pdxpc_vnumero_doc;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBePago.ctacc_iid_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Obe.ctacc_iccosto)
                    {
                        obj_item_CompDet_01.cecoc_icod_centro_costo = oBePago.cecoc_icod_centro_costo;
                        obj_item_CompDet_01.strCodCCosto = oBePago.cecoc_ccodigo_centro_costo;
                    }
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = oBePago.IdTipoAnalitica;
                        obj_item_CompDet_01.anad_icod_analitica = oBePago.anac_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}{1}", obj_item_CompDet_01.tablc_iid_tipo_analitica, obj_item_CompDet_01.anad_icod_analitica);
                    }
                });

                obj_item_CompDet_01.vcocd_vglosa_linea = "PAGO MANUAL " + obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3) + " " + oBePago.pdxpc_vnumero_doc;
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBePago.pdxpc_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBePago.tablc_iid_tipo_moneda == 3) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) * Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBePago.tablc_iid_tipo_moneda == 4) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) / Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);

                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBePago.intTipoDoc);
                obj_item_CompDet_02.vcocd_numero_doc = oBePago.numero_doc_dxp;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);

                var lstDocumentoClase_02 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBePago.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_02.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBePago.tdodc_iid_correlativo).ToList()[0];


                var lstProveedor = new ComprasData().ListarProveedor();
                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBePago.IcodProveedorDXP).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_item_CompDet_02.strTipNroDocumento));

                var obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == oBePago.IcodProveedorDXP).ToList()[0];

                if (oBePago.moneda_dxp == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBePago.moneda_dxp == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBePago.moneda_dxp == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();

                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_02.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);

                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = "PAGO MANUAL " + obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3) + " " + oBePago.strNroDoc;
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBePago.pdxpc_nmonto_tipo_cambio;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBePago.tablc_iid_tipo_moneda == 3) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) * Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBePago.tablc_iid_tipo_moneda == 4) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) / Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_02.doxpc_icod_correlativo = Convert.ToInt32(oBePago.doxpc_icod_correlativo);
                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpPagoDirectoCanje(EVoucherContableCab oBe, EDocPorPagarPago oBePago, List<ECuentaContable> lstPlanCuentas,
          List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBePago.IcodTD);
                obj_item_CompDet_01.vcocd_numero_doc = oBePago.pdxpc_vnumero_doc;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);

                var lstDocumentoClase_01 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_01.tdocc_icod_tipo_doc);
                if (lstDocumentoClase_01.Where(z => z.tdocd_iid_correlativo == oBePago.IddTD).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_01.vcocd_numero_doc));
                var obj_DocumentoClase1 = lstDocumentoClase_01.Where(z => z.tdocd_iid_correlativo == oBePago.IddTD).ToList()[0];


                //obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBePago.ctacc_iid_cuenta_contable);
                obj_item_CompDet_01.ctacc_icod_cuenta_contable = (oBePago.MonDXC == 3) ? Convert.ToInt32(obj_DocumentoClase1.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase1.ctacc_icod_cuenta_contable_extra);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();

                if (oBePago.cliec_icod_cliente == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del cliente {0}", obj_item_CompDet_01.strTipNroDocumento));
                var lstCliente = new VentasData().ListarCliente();
                ECliente obj_Cliente = lstCliente.Where(x => x.cliec_icod_cliente == oBePago.cliec_icod_cliente).ToList()[0];

                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Obe.ctacc_iccosto)
                    {
                        obj_item_CompDet_01.cecoc_icod_centro_costo = oBePago.cecoc_icod_centro_costo;
                        obj_item_CompDet_01.strCodCCosto = oBePago.cecoc_ccodigo_centro_costo;
                    }
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_item_CompDet_01.anad_icod_analitica = obj_Cliente.anac_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_01.tablc_iid_tipo_analitica, obj_item_CompDet_01.anad_icod_analitica);
                    }
                });

                obj_item_CompDet_01.vcocd_vglosa_linea = oBePago.pdxpc_vobservacion;
                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBePago.pdxpc_nmonto_tipo_cambio;

                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBePago.tablc_iid_tipo_moneda == 3) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) * Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBePago.tablc_iid_tipo_moneda == 4) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) / Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);

                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBePago.intTipoDoc);
                obj_item_CompDet_02.vcocd_numero_doc = oBePago.numero_doc_dxp;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);

                var lstDocumentoClase_02 = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                if (lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBePago.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_02.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase_02.Where(z => z.tdocd_iid_correlativo == oBePago.tdodc_iid_correlativo).ToList()[0];


                var lstProveedor = new ComprasData().ListarProveedor();
                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBePago.IcodProveedorDXP).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_item_CompDet_02.strTipNroDocumento));

                var obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == oBePago.IcodProveedorDXP).ToList()[0];

                if (oBePago.moneda_dxp == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBePago.moneda_dxp == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBePago.moneda_dxp == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();

                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_item_CompDet_02.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);

                    }
                });
                obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                obj_item_CompDet_02.vcocd_vglosa_linea = oBePago.pdxpc_vobservacion;
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBePago.pdxpc_nmonto_tipo_cambio;

                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBePago.tablc_iid_tipo_moneda == 3) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) * Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBePago.tablc_iid_tipo_moneda == 4) ? oBePago.pdxpc_nmonto_pago : Math.Round(Convert.ToDecimal(oBePago.pdxpc_nmonto_pago) / Convert.ToDecimal(oBePago.pdxpc_nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_02.doxpc_icod_correlativo = Convert.ToInt32(oBePago.doxpc_icod_correlativo);
                lstCompDetalle.Add(obj_item_CompDet_02);
                lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/
                if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<EVoucherContableDet> getDetVoucherDxpLxp(EVoucherContableCab oBe, EDocPorPagar oBeDXP, ELetraPorPagar oBeLXP, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                obj_CompDet_01.vcocd_numero_doc = oBeLXP.lexpc_vnumero_letra;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                if (oBeLXP.proc_icod_proveedor == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del proveedor de la LXP {0}", oBeLXP.lexpc_vnumero_letra));

                var lstProveedor = new ComprasData().ListarProveedor();

                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_CompDet_01.strTipNroDocumento));

                EProveedor obj_Proveedor = lstProveedor.Where(x => x.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList()[0];

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocLetraProveedor);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_CompDet_01.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList()[0];

                if (oBeLXP.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeLXP.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_CompDet_01.ctacc_icod_cuenta_contable = (oBeLXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_CompDet_01.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = oBe.vcocc_glosa;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLXP.tablc_iid_tipo_moneda == 3) ? oBeLXP.lexpc_nmonto_total : Math.Round(oBeLXP.lexpc_nmonto_total * Convert.ToDecimal(oBeLXP.lexpc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLXP.tablc_iid_tipo_moneda == 4) ? oBeLXP.lexpc_nmonto_total : Math.Round(oBeLXP.lexpc_nmonto_total / Convert.ToDecimal(oBeLXP.lexpc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeLXP.lexpc_nmonto_tipo_cambio;
                obj_CompDet_01.doxpc_icod_correlativo = oBeLXP.lexpc_icod_correlativo;
                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                var lstDet = new CuentasPorPagarData().listarLetraPorPagarDet(oBeLXP.lexpc_icod_correlativo);
                lstDet.ForEach(x =>
                {
                    EVoucherContableDet obj_CompDet_D = new EVoucherContableDet();
                    obj_CompDet_D.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_CompDet_D.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    if (Convert.ToInt32(x.ctacc_iid_cuenta_contable) > 0)
                    {
                        obj_CompDet_D.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                        obj_CompDet_D.vcocd_numero_doc = oBeLXP.lexpc_vnumero_letra;
                        obj_CompDet_D.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_D.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_D.vcocd_numero_doc);
                        obj_CompDet_D.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_iid_cuenta_contable);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_D.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_CompDet_D.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_CompDet_D.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_D.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Obe.ctacc_iccosto)
                                obj_CompDet_D.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo; ;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_CompDet_D.tablc_iid_tipo_analitica = Convert.ToInt32(x.strCodAnalitica);
                                obj_CompDet_D.anad_icod_analitica = x.anac_icod_analitica;
                                obj_CompDet_D.strAnalisis = String.Format("{0:00}.{1}", x.strCodAnalitica, x.strCodSubAnalitica);
                            }
                        });
                    }
                    else
                    {
                        obj_CompDet_D.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                        obj_CompDet_D.vcocd_numero_doc = x.pdxpc_vnumero_doc;
                        obj_CompDet_D.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_D.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_D.vcocd_numero_doc);

                        var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                        /*****************************************/
                        if (lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_CompDet_D.vcocd_numero_doc));
                        obj_DocumentoClase = lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList()[0];
                        /*****************************************/

                        if (lstProveedor.Where(a => a.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList().Count() == 0)
                            throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_CompDet_D.strTipNroDocumento));

                        if (oBeLXP.proc_icod_proveedor == 0)
                            throw new ArgumentException(String.Format("No se encontró los datos de proveedor de la LXP {0}", oBeLXP.lexpc_vnumero_letra));
                        obj_Proveedor = lstProveedor.Where(y => y.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList()[0];

                        if (oBeLXP.tablc_iid_tipo_moneda == 3)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        if (oBeLXP.tablc_iid_tipo_moneda == 4)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        obj_CompDet_D.ctacc_icod_cuenta_contable = (oBeLXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_D.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_CompDet_D.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_CompDet_D.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_D.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_CompDet_D.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                                obj_CompDet_D.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                                obj_CompDet_D.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);
                            }
                        });
                    }

                    obj_CompDet_D.vcocd_vglosa_linea = x.lxppc_vconcepto;
                    obj_CompDet_D.vcocd_nmto_tot_debe_sol = (oBeLXP.tablc_iid_tipo_moneda == 3) ? x.lxppc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxppc_nmonto_pago) * Convert.ToDecimal(oBeLXP.lexpc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.vcocd_nmto_tot_haber_sol = 0;
                    obj_CompDet_D.vcocd_nmto_tot_debe_dol = (oBeLXP.tablc_iid_tipo_moneda == 4) ? x.lxppc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxppc_nmonto_pago) / Convert.ToDecimal(oBeLXP.lexpc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.vcocd_nmto_tot_haber_dol = 0;
                    obj_CompDet_D.intTipoOperacion = 1;
                    obj_CompDet_D.vcocd_tipo_cambio = oBeLXP.lexpc_nmonto_tipo_cambio;
                    obj_CompDet_D.doxpc_icod_correlativo = Convert.ToInt32(x.doxpc_icod_correlativo);
                    lstCompDetalle.Add(obj_CompDet_D);
                    lstDetGeneral.Add(obj_CompDet_D);/***********************************************************/
                    if (obj_CompDet_D.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_CompDet_D, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherDxpGxp(EVoucherContableCab oBe, EDocPorPagar oBeDXP, EGarantiaProveedores oBeLXP, List<ECuentaContable> lstPlanCuentas,
         List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocGarantiaProveedor;
                obj_CompDet_01.vcocd_numero_doc = oBeLXP.garap_vnumero_garantia;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                if (oBeLXP.proc_icod_proveedor == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del proveedor de la LXP {0}", oBeLXP.garap_vnumero_garantia));

                var lstProveedor = new ComprasData().ListarProveedor();

                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_CompDet_01.strTipNroDocumento));

                EProveedor obj_Proveedor = lstProveedor.Where(x => x.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList()[0];

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocGarantiaProveedor);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_CompDet_01.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == oBeDXP.tdodc_iid_correlativo).ToList()[0];

                if (oBeLXP.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBeLXP.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_CompDet_01.ctacc_icod_cuenta_contable = (oBeLXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_CompDet_01.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);
                    }
                });
                //obj_CompDet_01.vcocd_vglosa_linea = oBe.vcocc_glosa;NumDXP
                obj_CompDet_01.vcocd_vglosa_linea = "FAC " + oBeLXP.NumDXP + " " + oBeLXP.CentroCosto + " " + oBeLXP.DesCC;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLXP.tablc_iid_tipo_moneda == 3) ? oBeLXP.garp_nmonto : Math.Round(oBeLXP.garp_nmonto * Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLXP.tablc_iid_tipo_moneda == 4) ? oBeLXP.garp_nmonto : Math.Round(oBeLXP.garp_nmonto / Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBe.vcocc_tipo_cambio;
                obj_CompDet_01.doxpc_icod_correlativo = Convert.ToInt32(oBeLXP.doxpc_icod_correlativo);
                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_02.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaCompra;
                obj_CompDet_02.vcocd_numero_doc = oBeLXP.NumDXP;
                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLXP.NumDXP);

                if (oBeLXP.proc_icod_proveedor == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del proveedor de la LXP {0}", oBeLXP.garap_vnumero_garantia));

                //var lstProveedor = new ComprasData().ListarProveedor();

                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_CompDet_02.strTipNroDocumento));

                EProveedor obj_Proveedor2 = lstProveedor.Where(x => x.iid_icod_proveedor == oBeLXP.proc_icod_proveedor).ToList()[0];

                var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocFacturaCompra);
                if (lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == oBeLXP.ClaseDXP).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_CompDet_02.vcocd_numero_doc));
                var obj_DocumentoClase2 = lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == oBeLXP.ClaseDXP).ToList()[0];

                if (oBeLXP.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                if (oBeLXP.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                obj_CompDet_02.ctacc_icod_cuenta_contable = (oBeLXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra);

                var Lista2 = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista2.ForEach(Obe =>
                {
                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_CompDet_02.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);
                    }
                });
                obj_CompDet_02.vcocd_vglosa_linea = oBe.vcocc_glosa;
                obj_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeLXP.tablc_iid_tipo_moneda == 3) ? oBeLXP.garp_nmonto : Math.Round(oBeLXP.garp_nmonto * Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeLXP.tablc_iid_tipo_moneda == 4) ? oBeLXP.garp_nmonto : Math.Round(oBeLXP.garp_nmonto / Convert.ToDecimal(oBe.vcocc_tipo_cambio), 2);
                obj_CompDet_02.intTipoOperacion = 1;
                obj_CompDet_02.vcocd_tipo_cambio = oBe.vcocc_tipo_cambio;
                obj_CompDet_02.doxpc_icod_correlativo = Convert.ToInt32(oBeLXP.fcoc_icod_doc);
                lstCompDetalle.Add(obj_CompDet_02);
                lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherPercepcion(EVoucherContableCab oBe, EDocPorPagar oBeDXP, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                var lstDet = new ComprasData().listarPercepcionDet(Convert.ToInt32(oBeDXP.percc_icod_percepcion));
                lstDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet_n = new EVoucherContableDet();
                    obj_item_CompDet_n.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_n.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_n.tdocc_icod_tipo_doc = x.tdoc_icod_tipo_documento;
                    obj_item_CompDet_n.vcocd_numero_doc = x.percd_vnro_doc;
                    obj_item_CompDet_n.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_n.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_n.vcocd_numero_doc);

                    var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(x.tdoc_icod_tipo_documento);
                    if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList().Count == 0)
                        throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_n.strTipNroDocumento.Substring(0, 3), x.percd_vnro_doc));
                    var obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];

                    if (oBeDXP.tablc_iid_tipo_moneda == 3)
                        if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_n.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                    if (oBeDXP.tablc_iid_tipo_moneda == 4)
                        if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_n.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                    obj_item_CompDet_n.ctacc_icod_cuenta_contable = (oBeDXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                    var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_n.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet_n.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_n.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_n.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        {
                            obj_item_CompDet_n.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                            obj_item_CompDet_n.anad_icod_analitica = x.intAnalitica;
                            obj_item_CompDet_n.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_n.tablc_iid_tipo_analitica, x.strCodAnalitica);
                        }
                    });

                    obj_item_CompDet_n.vcocd_vglosa_linea = String.Format("PAGO CON PERCEPCION {0}", oBeDXP.doxpc_vnumero_doc);
                    obj_item_CompDet_n.intTipoOperacion = 1;
                    obj_item_CompDet_n.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                    obj_item_CompDet_n.vcocd_nmto_tot_debe_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? x.percd_nmonto_percibido_doc : Math.Round(x.percd_nmonto_percibido_doc * oBeDXP.doxpc_nmonto_tipo_cambio, 2);
                    obj_item_CompDet_n.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_n.vcocd_nmto_tot_debe_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? x.percd_nmonto_percibido_doc : Math.Round(x.percd_nmonto_percibido_doc / oBeDXP.doxpc_nmonto_tipo_cambio, 2);
                    obj_item_CompDet_n.vcocd_nmto_tot_haber_dol = 0;

                    lstCompDetalle.Add(obj_item_CompDet_n);
                    lstDetGeneral.Add(obj_item_CompDet_n);/***********************************************************/
                    if (obj_item_CompDet_n.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_n, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region detalle 02
                EVoucherContableDet obj_CompDet_02 = new EVoucherContableDet();
                obj_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_02.tdocc_icod_tipo_doc = Parametros.intTipoDocPercepcionCompra;
                obj_CompDet_02.vcocd_numero_doc = oBeDXP.doxpc_vnumero_doc;
                obj_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_02.vcocd_numero_doc);

                var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocRetencion);
                if (lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocRetencion).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), oBeDXP.doxpc_vnumero_doc));
                ETipoDocumentoDetalleCta obj_DocumentoClase2 = lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocRetencion).ToList()[0];

                if (oBeDXP.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                if (oBeDXP.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_02.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                obj_CompDet_02.ctacc_icod_cuenta_contable = (oBeDXP.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra);

                var Lista2 = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista2.ForEach(Obe =>
                {
                    obj_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                });
                obj_CompDet_02.vcocd_vglosa_linea = "";
                obj_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_02.vcocd_nmto_tot_haber_sol = (oBeDXP.tablc_iid_tipo_moneda == 3) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) * oBeDXP.doxpc_nmonto_tipo_cambio, 2);
                obj_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_02.vcocd_nmto_tot_haber_dol = (oBeDXP.tablc_iid_tipo_moneda == 4) ? oBeDXP.doxpc_nmonto_total_documento : Math.Round(Convert.ToDecimal(oBeDXP.doxpc_nmonto_total_documento) / oBeDXP.doxpc_nmonto_tipo_cambio, 2);
                obj_CompDet_02.intTipoOperacion = 1;
                obj_CompDet_02.vcocd_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_02);
                lstDetGeneral.Add(obj_CompDet_02);/***********************************************************/
                if (obj_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Desde Caja Chica...
        private List<EVoucherContableDet> getDetVoucherLiqCaja(EVoucherContableCab oBe, ELiquidacionCaja oBeLiqCaja, List<ECuentaContable> lstPlanCuentas,
           List<EVoucherContableDet> lstDetGeneral, List<EParametroContable> lstParametros)
        {
            try
            {
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                var lstDet = new BTesoreria().ListarLiquidacionCajaDetalle(oBeLiqCaja.lqcc_icod_liquid_cja);
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                obj_CompDet_01.vcocd_numero_doc = oBeLiqCaja.caja_nro + String.Format("{0:000}", oBeLiqCaja.lqcc_inro_liquid_caja);
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                obj_CompDet_01.ctacc_icod_cuenta_contable = oBeLiqCaja.caja_iid_cuenta_contable;
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.tablc_iid_tipo_analitica = oBeLiqCaja.caja_tipo_analitica;
                        obj_CompDet_01.anad_icod_analitica = oBeLiqCaja.caja_icod_analitica;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", obj_CompDet_01.tablc_iid_tipo_analitica, oBeLiqCaja.strCodAnalitica);
                    }
                });

                obj_CompDet_01.vcocd_vglosa_linea = oBeLiqCaja.lqcc_vconcepto;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? oBeLiqCaja.lqcc_nmonto_total : Math.Round(oBeLiqCaja.lqcc_nmonto_total * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? oBeLiqCaja.lqcc_nmonto_total : Math.Round(oBeLiqCaja.lqcc_nmonto_total / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }

                #endregion
                #region detalle 02
                lstDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet_Det = new EVoucherContableDet();
                    obj_item_CompDet_Det.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    if (x.lqcd_vtipo_movimiento == "CCONTABLE")
                    {
                        #region det...
                        obj_item_CompDet_Det.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                        obj_item_CompDet_Det.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                        obj_item_CompDet_Det.vcocd_numero_doc = oBeLiqCaja.caja_nro + String.Format("{0:000}", oBeLiqCaja.lqcc_inro_liquid_caja);
                        obj_item_CompDet_Det.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det.vcocd_numero_doc);
                        obj_item_CompDet_Det.ctacc_icod_cuenta_contable = Convert.ToInt32(x.lqcd_iid_cuenta_contable);
                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet_Det.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Obe.ctacc_iccosto)
                            {
                                obj_item_CompDet_Det.cecoc_icod_centro_costo = x.lqcd_iid_centro_costo;
                                obj_item_CompDet_Det.strCodCCosto = x.codigo_ccosto;
                            }
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet_Det.tablc_iid_tipo_analitica = x.lqcd_iid_tipo_analitica;
                                obj_item_CompDet_Det.anad_icod_analitica = x.lqcd_iid_analitica;
                                obj_item_CompDet_Det.strAnalisis = x.analisis;
                            }
                        });

                        obj_item_CompDet_Det.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_dol = 0;
                        obj_item_CompDet_Det.intTipoOperacion = 1;
                        obj_item_CompDet_Det.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                        /**/
                        lstCompDetalle.Add(obj_item_CompDet_Det);
                        lstDetGeneral.Add(obj_item_CompDet_Det);/***********************************************************/
                        if (obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto != null)
                        {
                            var tuple = addCtaAutomatica(obj_item_CompDet_Det, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                            lstCompDetalle = tuple.Item1;
                            lstDetGeneral = tuple.Item2;
                        }
                        #endregion
                    }
                    else if (x.lqcd_vtipo_movimiento == "PGOPROVIS")
                    {
                        #region det...
                        obj_item_CompDet_Det.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                        obj_item_CompDet_Det.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                        obj_item_CompDet_Det.vcocd_numero_doc = x.lqcd_vnumero_doc;
                        obj_item_CompDet_Det.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det.vcocd_numero_doc);
                        var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                        if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_Det.vcocd_numero_doc));
                        ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                        if (oBeLiqCaja.lqcc_iid_tipo_moneda == 3)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        if (oBeLiqCaja.lqcc_iid_tipo_moneda == 4)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        obj_item_CompDet_Det.ctacc_icod_cuenta_contable = (x.MonedaDXP == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet_Det.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;

                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_item_CompDet_Det.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                                obj_item_CompDet_Det.anad_icod_analitica = x.lqcd_iid_analitica;
                                obj_item_CompDet_Det.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_Det.tablc_iid_tipo_analitica, x.iid_analitica);
                            }
                        });

                        obj_item_CompDet_Det.cecoc_icod_centro_costo = null;
                        obj_item_CompDet_Det.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_dol = 0;
                        obj_item_CompDet_Det.intTipoOperacion = 1;
                        obj_item_CompDet_Det.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;

                        /**/
                        lstCompDetalle.Add(obj_item_CompDet_Det);
                        lstDetGeneral.Add(obj_item_CompDet_Det);/***********************************************************/
                        if (obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto != null)
                        {
                            var tuple = addCtaAutomatica(obj_item_CompDet_Det, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                            lstCompDetalle = tuple.Item1;
                            lstDetGeneral = tuple.Item2;
                        }
                        #endregion
                    }
                    else if (x.lqcd_vtipo_movimiento == "GENPROVIS")
                    {
                        if (x.lqcd_iid_tipo_doc != Parametros.intTipoDocReciboPorHonorarios)
                        {
                            #region Gen. Prov. 01
                            obj_item_CompDet_Det.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                            obj_item_CompDet_Det.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                            obj_item_CompDet_Det.vcocd_numero_doc = x.lqcd_vnumero_doc;
                            obj_item_CompDet_Det.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det.vcocd_numero_doc);
                            obj_item_CompDet_Det.ctacc_icod_cuenta_contable = Convert.ToInt32(x.lqcd_iid_cuenta_contable);
                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_item_CompDet_Det.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_item_CompDet_Det.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Obe.ctacc_iccosto)
                                {
                                    obj_item_CompDet_Det.cecoc_icod_centro_costo = x.lqcd_iid_centro_costo;
                                    obj_item_CompDet_Det.strCodCCosto = x.codigo_ccosto;
                                }
                            });

                            obj_item_CompDet_Det.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                            /**/
                            decimal dcmlMonto = x.lqcd_nmonto_afecto + x.lqcd_nmonto_dest_mixto + x.lqcd_nmonto_inafecto;
                            /**/
                            obj_item_CompDet_Det.vcocd_nmto_tot_debe_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? dcmlMonto : Math.Round(dcmlMonto * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det.vcocd_nmto_tot_haber_sol = 0;
                            obj_item_CompDet_Det.vcocd_nmto_tot_debe_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? dcmlMonto : Math.Round(dcmlMonto / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det.vcocd_nmto_tot_haber_dol = 0;
                            obj_item_CompDet_Det.intTipoOperacion = 1;
                            obj_item_CompDet_Det.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                            /**/
                            lstCompDetalle.Add(obj_item_CompDet_Det);
                            lstDetGeneral.Add(obj_item_CompDet_Det);/***********************************************************/
                            if (obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_item_CompDet_Det, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region Gen. Prov. 02
                            if (x.lqcd_nmonto_igv > 0)
                            {
                                EVoucherContableDet obj_item_CompDet_Det02 = new EVoucherContableDet();
                                obj_item_CompDet_Det02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_item_CompDet_Det02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                                obj_item_CompDet_Det02.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                                obj_item_CompDet_Det02.vcocd_numero_doc = x.lqcd_vnumero_doc;
                                obj_item_CompDet_Det02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det02.vcocd_numero_doc);

                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_Det.vcocd_numero_doc));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable IGV de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                                obj_item_CompDet_Det02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac);
                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det02.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_item_CompDet_Det02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_item_CompDet_Det02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_item_CompDet_Det02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                });

                                obj_item_CompDet_Det02.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                                obj_item_CompDet_Det02.vcocd_nmto_tot_debe_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? x.lqcd_nmonto_igv : Math.Round(x.lqcd_nmonto_igv * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                                obj_item_CompDet_Det02.vcocd_nmto_tot_haber_sol = 0;
                                obj_item_CompDet_Det02.vcocd_nmto_tot_debe_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? x.lqcd_nmonto_igv : Math.Round(x.lqcd_nmonto_igv / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                                obj_item_CompDet_Det02.vcocd_nmto_tot_haber_dol = 0;
                                obj_item_CompDet_Det02.intTipoOperacion = 1;
                                obj_item_CompDet_Det02.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                                /**/
                                lstCompDetalle.Add(obj_item_CompDet_Det02);
                                lstDetGeneral.Add(obj_item_CompDet_Det02);/***********************************************************/
                                if (obj_item_CompDet_Det02.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_item_CompDet_Det02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                            }
                            #endregion
                            #region Gen. Prov. 03
                            EVoucherContableDet obj_item_CompDet_Det03 = new EVoucherContableDet();
                            obj_item_CompDet_Det03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_item_CompDet_Det03.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                            obj_item_CompDet_Det03.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                            obj_item_CompDet_Det03.vcocd_numero_doc = x.lqcd_vnumero_doc;
                            obj_item_CompDet_Det03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det03.vcocd_numero_doc);

                            var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                            if (lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_Det03.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_Det03.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase2 = lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                            if (oBeLiqCaja.lqcc_iid_tipo_moneda == 3)
                                if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det03.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                            if (oBeLiqCaja.lqcc_iid_tipo_moneda == 4)
                                if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det03.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                            obj_item_CompDet_Det03.ctacc_icod_cuenta_contable = (x.MonedaDXP == 3) ? Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra);
                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det03.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_item_CompDet_Det03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_item_CompDet_Det03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_item_CompDet_Det03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Obe.ctacc_iccosto)
                                {
                                    obj_item_CompDet_Det03.cecoc_icod_centro_costo = x.lqcd_iid_centro_costo;
                                    obj_item_CompDet_Det03.strCodCCosto = x.codigo_ccosto;
                                }
                            });

                            obj_item_CompDet_Det03.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                            obj_item_CompDet_Det03.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet_Det03.vcocd_nmto_tot_haber_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? Convert.ToDecimal(x.docxp_mto_total) : Math.Round(Convert.ToDecimal(x.docxp_mto_total) * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det03.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet_Det03.vcocd_nmto_tot_haber_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? Convert.ToDecimal(x.docxp_mto_total) : Math.Round(Convert.ToDecimal(x.docxp_mto_total) / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det03.intTipoOperacion = 1;
                            obj_item_CompDet_Det03.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                            /**/
                            lstCompDetalle.Add(obj_item_CompDet_Det03);
                            lstDetGeneral.Add(obj_item_CompDet_Det03);/***********************************************************/
                            if (obj_item_CompDet_Det03.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_item_CompDet_Det03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region Gen. Prov. 04
                            EVoucherContableDet obj_item_CompDet_Det04 = new EVoucherContableDet();
                            obj_item_CompDet_Det04.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_item_CompDet_Det04.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                            obj_item_CompDet_Det04.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                            obj_item_CompDet_Det04.vcocd_numero_doc = x.lqcd_vnumero_doc;
                            obj_item_CompDet_Det04.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det04.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det04.vcocd_numero_doc);

                            var lstDocumentoClase4 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                            if (lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_Det04.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_Det04.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase4 = lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                            if (oBeLiqCaja.lqcc_iid_tipo_moneda == 3)
                                if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det04.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                            if (oBeLiqCaja.lqcc_iid_tipo_moneda == 4)
                                if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det04.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                            obj_item_CompDet_Det04.ctacc_icod_cuenta_contable = (x.MonedaDXP == 3) ? Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_contable_extra);
                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det04.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_item_CompDet_Det04.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_item_CompDet_Det04.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_item_CompDet_Det04.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Obe.ctacc_iccosto)
                                {
                                    obj_item_CompDet_Det04.cecoc_icod_centro_costo = x.lqcd_iid_centro_costo;
                                    obj_item_CompDet_Det04.strCodCCosto = x.codigo_ccosto;
                                }
                            });

                            obj_item_CompDet_Det04.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                            obj_item_CompDet_Det04.vcocd_nmto_tot_haber_sol = 0;
                            obj_item_CompDet_Det04.vcocd_nmto_tot_debe_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? Convert.ToDecimal(x.docxp_mto_total) : Math.Round(Convert.ToDecimal(x.docxp_mto_total) * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det04.vcocd_nmto_tot_haber_dol = 0;
                            obj_item_CompDet_Det04.vcocd_nmto_tot_debe_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? Convert.ToDecimal(x.docxp_mto_total) : Math.Round(Convert.ToDecimal(x.docxp_mto_total) / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det04.intTipoOperacion = 1;
                            obj_item_CompDet_Det04.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                            /**/
                            lstCompDetalle.Add(obj_item_CompDet_Det04);
                            lstDetGeneral.Add(obj_item_CompDet_Det04);/***********************************************************/
                            if (obj_item_CompDet_Det04.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_item_CompDet_Det04, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                        else
                        {
                            #region Gen. Prov. 01
                            obj_item_CompDet_Det.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                            obj_item_CompDet_Det.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                            obj_item_CompDet_Det.vcocd_numero_doc = x.lqcd_vnumero_doc;
                            obj_item_CompDet_Det.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det.vcocd_numero_doc);

                            var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                            if (lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_Det.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase2 = lstDocumentoClase2.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                            if (Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_gastos_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable GASTOS de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase2.tdocd_iid_codigo_doc_det));

                            obj_item_CompDet_Det.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase2.ctacc_icod_cuenta_gastos_nac);
                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_item_CompDet_Det.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_item_CompDet_Det.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Obe.ctacc_iccosto)
                                {
                                    obj_item_CompDet_Det.cecoc_icod_centro_costo = x.lqcd_iid_centro_costo;
                                    obj_item_CompDet_Det.strCodCCosto = x.codigo_ccosto;
                                }
                                if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                                {
                                    obj_item_CompDet_Det.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                                    obj_item_CompDet_Det.anad_icod_analitica = x.lqcd_iid_analitica;
                                    obj_item_CompDet_Det.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_Det.tablc_iid_tipo_analitica, x.iid_analitica);
                                }
                            });

                            obj_item_CompDet_Det.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                            /**/

                            /**/
                            obj_item_CompDet_Det.vcocd_nmto_tot_debe_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? Convert.ToDecimal(x.docxp_mto_total) : Math.Round(Convert.ToDecimal(x.docxp_mto_total) * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det.vcocd_nmto_tot_haber_sol = 0;
                            obj_item_CompDet_Det.vcocd_nmto_tot_debe_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? Convert.ToDecimal(x.docxp_mto_total) : Math.Round(Convert.ToDecimal(x.docxp_mto_total) / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det.vcocd_nmto_tot_haber_dol = 0;
                            obj_item_CompDet_Det.intTipoOperacion = 1;
                            obj_item_CompDet_Det.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                            /**/
                            lstCompDetalle.Add(obj_item_CompDet_Det);
                            lstDetGeneral.Add(obj_item_CompDet_Det);/***********************************************************/
                            if (obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_item_CompDet_Det, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                            #region Gen. Prov. 02
                            if (x.lqcd_nmonto_rta_cuarta > 0)
                            {
                                EVoucherContableDet obj_item_CompDet_Det02 = new EVoucherContableDet();
                                obj_item_CompDet_Det02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                                obj_item_CompDet_Det02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                                obj_item_CompDet_Det02.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                                obj_item_CompDet_Det02.vcocd_numero_doc = x.lqcd_vnumero_doc;
                                obj_item_CompDet_Det02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det02.vcocd_numero_doc);

                                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_Det.vcocd_numero_doc));
                                ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable IGV de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                                obj_item_CompDet_Det02.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_igv_nac);
                                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det02.ctacc_icod_cuenta_contable).ToList();
                                Lista.ForEach(Obe =>
                                {
                                    obj_item_CompDet_Det02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                    obj_item_CompDet_Det02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                    obj_item_CompDet_Det02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                });

                                obj_item_CompDet_Det02.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                                obj_item_CompDet_Det02.vcocd_nmto_tot_debe_sol = 0;
                                obj_item_CompDet_Det02.vcocd_nmto_tot_haber_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? x.lqcd_nmonto_rta_cuarta : Math.Round(x.lqcd_nmonto_rta_cuarta * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                                obj_item_CompDet_Det02.vcocd_nmto_tot_debe_dol = 0;
                                obj_item_CompDet_Det02.vcocd_nmto_tot_haber_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? x.lqcd_nmonto_rta_cuarta : Math.Round(x.lqcd_nmonto_rta_cuarta / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                                obj_item_CompDet_Det02.intTipoOperacion = 1;
                                obj_item_CompDet_Det02.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                                /**/
                                lstCompDetalle.Add(obj_item_CompDet_Det02);
                                lstDetGeneral.Add(obj_item_CompDet_Det02);/***********************************************************/
                                if (obj_item_CompDet_Det02.ctacc_icod_cuenta_debe_auto != null)
                                {
                                    var tuple = addCtaAutomatica(obj_item_CompDet_Det02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                    lstCompDetalle = tuple.Item1;
                                    lstDetGeneral = tuple.Item2;
                                }
                            }
                            #endregion
                            #region Gen. Prov. 03
                            EVoucherContableDet obj_item_CompDet_Det03 = new EVoucherContableDet();
                            obj_item_CompDet_Det03.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                            obj_item_CompDet_Det03.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                            obj_item_CompDet_Det03.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                            obj_item_CompDet_Det03.vcocd_numero_doc = x.lqcd_vnumero_doc;
                            obj_item_CompDet_Det03.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_item_CompDet_Det03.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det03.vcocd_numero_doc);

                            var lstDocumentoClase3 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                            if (lstDocumentoClase3.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                                throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_item_CompDet_Det03.strTipNroDocumento.Substring(0, 3), obj_item_CompDet_Det03.vcocd_numero_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase3 = lstDocumentoClase3.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                            if (oBeLiqCaja.lqcc_iid_tipo_moneda == 3)
                                if (Convert.ToInt32(obj_DocumentoClase3.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det03.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase3.tdocd_iid_codigo_doc_det));

                            if (oBeLiqCaja.lqcc_iid_tipo_moneda == 4)
                                if (Convert.ToInt32(obj_DocumentoClase3.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_item_CompDet_Det03.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase3.tdocd_iid_codigo_doc_det));

                            obj_item_CompDet_Det03.ctacc_icod_cuenta_contable = (x.MonedaDXP == 3) ? Convert.ToInt32(obj_DocumentoClase3.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase3.ctacc_icod_cuenta_contable_extra);
                            Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det03.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_item_CompDet_Det03.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                                obj_item_CompDet_Det03.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_item_CompDet_Det03.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                                if (Obe.ctacc_iccosto)
                                {
                                    obj_item_CompDet_Det03.cecoc_icod_centro_costo = x.lqcd_iid_centro_costo;
                                    obj_item_CompDet_Det03.strCodCCosto = x.codigo_ccosto;
                                }
                            });

                            obj_item_CompDet_Det03.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                            /**/
                            decimal dcmlMonto = x.lqcd_nmonto_afecto + x.lqcd_nmonto_dest_mixto + x.lqcd_nmonto_inafecto;
                            /**/
                            obj_item_CompDet_Det03.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet_Det03.vcocd_nmto_tot_haber_sol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 3) ? dcmlMonto : Math.Round(dcmlMonto * oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det03.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet_Det03.vcocd_nmto_tot_haber_dol = (oBeLiqCaja.lqcc_iid_tipo_moneda == 4) ? dcmlMonto : Math.Round(dcmlMonto / oBeLiqCaja.lqcc_ntipo_cambio, 2);
                            obj_item_CompDet_Det03.intTipoOperacion = 1;
                            obj_item_CompDet_Det03.vcocd_tipo_cambio = oBeLiqCaja.lqcc_ntipo_cambio;
                            /**/
                            lstCompDetalle.Add(obj_item_CompDet_Det03);
                            lstDetGeneral.Add(obj_item_CompDet_Det03);/***********************************************************/
                            if (obj_item_CompDet_Det03.ctacc_icod_cuenta_debe_auto != null)
                            {
                                var tuple = addCtaAutomatica(obj_item_CompDet_Det03, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                                lstCompDetalle = tuple.Item1;
                                lstDetGeneral = tuple.Item2;
                            }
                            #endregion
                        }
                    }

                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region ActivosFijos

        #region Localidades
        public List<ELocalidades> listarLocalidades()
        {
            List<ELocalidades> lista = null;
            try
            {
                lista = new ContabilidadData().listarLocalidades();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int InsertarLocalidades(ELocalidades oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().InsertarLocalidades(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarLocalidades(ELocalidades oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarLocalidades(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarLocalidades(ELocalidades oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarLocalidades(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Registro ActivoFijo

        public List<EActivoFijo> listarActivoFijo()
        {
            List<EActivoFijo> lista = null;
            try
            {
                lista = new ContabilidadData().listarActivoFijo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int InsertarActivoFijo(EActivoFijo oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new ContabilidadData().InsertarActivoFijo(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarActivoFijo(EActivoFijo oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().modificarActivoFijo(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarActivoFijo(EActivoFijo oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new ContabilidadData().eliminarActivoFijo(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Estado Gancias y Perdidas

        public List<EEstadoGanPer> ListarEstadoGanPer()
        {
            List<EEstadoGanPer> Lista = new List<EEstadoGanPer>();
            try
            {
                Lista = objContabilidadData.ListarEstadoGanPer();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public int InsertarEstadoGanPer(EEstadoGanPer obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarEstadoGanPer(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }

        public void ModificarEstadoGanPer(EEstadoGanPer obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.ModificarEstadoGanPer(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarEstadoGanPer(EEstadoGanPer obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //List<EPosFinanCta> ListaPosFinanCta = new List<EPosFinanCta>();
                    //ListaPosFinanCta = objContabilidadData.ListarPosicionFinancieraCtasxIcodPosFin(Convert.ToInt32(obj.posc_icod_forma_posi_finan));
                    //foreach (var item in ListaPosFinanCta)
                    //{
                    //item.PcModifica = obj.PcModifica;
                    //item.IdUsuarioModifica = obj.IdUsuarioModifica;
                    //objContabilidadData.EliminarPosicionFinancieraCtas(item);
                    //}
                    objContabilidadData.EliminarEstadoGanPer(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Estado Gancias y Perdidas Cuentas

        public List<EEstadoGanPerCtas> ListarEstadoGanPerCtasxIcodPosFin(int icod_pos_finan)
        {
            List<EEstadoGanPerCtas> Lista = new List<EEstadoGanPerCtas>();
            try
            {
                Lista = objContabilidadData.ListarEstadoGanPerCtasxIcodPosFin(icod_pos_finan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public List<EEstadoGanPerCtas> ListarEstadoGanPerCtasxIcodPosFinMontos(int icod_pos_finan, int cecoc_icod_centro_costo, int vcocc_fecha_vcontable, int indicador)
        {
            List<EEstadoGanPerCtas> Lista = new List<EEstadoGanPerCtas>();
            try
            {
                Lista = objContabilidadData.ListarEstadoGanPerCtasxIcodPosFinMontos(icod_pos_finan, cecoc_icod_centro_costo, vcocc_fecha_vcontable, indicador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarPEstadoGanPerCtas(EEstadoGanPerCtas obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarPEstadoGanPerCtas(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }

        public void EliminarEstadoGanPerCtas(EEstadoGanPerCtas obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.EliminarEstadoGanPerCtas(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Balance
        public List<EBalance> ListarBalance()
        {
            List<EBalance> Lista = new List<EBalance>();
            try
            {
                Lista = objContabilidadData.ListarBalance();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public int InsertarBalance(EBalance obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarBalance(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }

        public void ModificarBalance(EBalance obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.ModificarBalance(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarBalance(EBalance obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //List<EPosFinanCta> ListaPosFinanCta = new List<EPosFinanCta>();
                    //ListaPosFinanCta = objContabilidadData.ListarPosicionFinancieraCtasxIcodPosFin(Convert.ToInt32(obj.posc_icod_forma_posi_finan));
                    //foreach (var item in ListaPosFinanCta)
                    //{
                    //item.PcModifica = obj.PcModifica;
                    //item.IdUsuarioModifica = obj.IdUsuarioModifica;
                    //objContabilidadData.EliminarPosicionFinancieraCtas(item);
                    //}
                    objContabilidadData.EliminarBalance(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Balance Cuentas
        public List<EBalanceCtas> ListarBalanceCtasxIcodPosFin(int icod_pos_finan)
        {
            List<EBalanceCtas> Lista = new List<EBalanceCtas>();
            try
            {
                Lista = objContabilidadData.ListarBalanceCtasxIcodPosFin(icod_pos_finan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public List<EBalanceCtas> ListarBalanceCtasxIcodPosFinMontos(int icod_pos_finan, int vcocc_fecha_vcontable)
        {
            List<EBalanceCtas> Lista = new List<EBalanceCtas>();
            try
            {
                Lista = objContabilidadData.ListarBalanceCtasxIcodPosFinMontos(icod_pos_finan, vcocc_fecha_vcontable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public List<EBalanceCtas> ListarBalanceCtasxFechaContable(int vcocc_fecha_vcontable)
        {
            List<EBalanceCtas> Lista = new List<EBalanceCtas>();
            try
            {
                Lista = objContabilidadData.ListarBalanceCtasxFechaContable(vcocc_fecha_vcontable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarBalanceCtas(EBalanceCtas obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarBalanceCtas(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }

        public void EliminarBalanceCtas(EBalanceCtas obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.EliminarBalanceCtas(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region lista de centro de conto

        public DataTable listaCentroCosto_D(int id_ger_per, int anio, int id_mes)
        {
            try
            {
                DataTable lista = new DataTable();
                lista = objContabilidadData.listarCentroCostoDinamico(id_ger_per, anio, id_mes);

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable listaCentroCosto_Plantilla(int id_costo, int anio, int id_mes)
        {
            try
            {
                DataTable lista = new DataTable();
                lista = objContabilidadData.listarCentroCostoDinamico_plantilla(id_costo, anio, id_mes);

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable listaCentroCosto_Detalle_excel(int anio, int id_mes)
        {
            try
            {
                DataTable lista = new DataTable();
                lista = objContabilidadData.listarCentroCostoDinamico_Detalle_excel(anio, id_mes);

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable listaCentroCosto_Plantilla_sinMovimiento()
        {
            try
            {
                DataTable lista = new DataTable();
                lista = objContabilidadData.listarCentroCostoDinamico_plantilla_SinMovimiento();

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Estado Gancias y Perdidas Funcion
        public List<EEstadoGanPerFuncion> ListarEstadoGanPerFuncion()
        {
            List<EEstadoGanPerFuncion> Lista = new List<EEstadoGanPerFuncion>();
            try
            {
                Lista = objContabilidadData.ListarEstadoGanPerFuncion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarEstadoGanPerFuncion(EEstadoGanPerFuncion obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarEstadoGanPerFuncion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }
        public void ModificarEstadoGanPerFuncion(EEstadoGanPerFuncion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.ModificarEstadoGanPerFuncion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarEstadoGanPerFuncion(EEstadoGanPerFuncion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //List<EPosFinanCta> ListaPosFinanCta = new List<EPosFinanCta>();
                    //ListaPosFinanCta = objContabilidadData.ListarPosicionFinancieraCtasxIcodPosFin(Convert.ToInt32(obj.posc_icod_forma_posi_finan));
                    //foreach (var item in ListaPosFinanCta)
                    //{
                    //item.PcModifica = obj.PcModifica;
                    //item.IdUsuarioModifica = obj.IdUsuarioModifica;
                    //objContabilidadData.EliminarPosicionFinancieraCtas(item);
                    //}
                    objContabilidadData.EliminarEstadoGanPerFuncion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Estado Gancias y Perdidas Cuentas Funcion
        public List<EEstadoGanPerCtasFuncion> ListarEstadoGanPerCtasxIcodPosFinFuncion(int icod_pos_finan)
        {
            List<EEstadoGanPerCtasFuncion> Lista = new List<EEstadoGanPerCtasFuncion>();
            try
            {
                Lista = objContabilidadData.ListarEstadoGanPerCtasxIcodPosFinFuncion(icod_pos_finan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public List<EEstadoGanPerCtasFuncion> ListarEstadoGanPerCtasxFechaContable(int cecoc_icod_centro_costo, int vcocc_fecha_vcontable, int indicador)
        {
            List<EEstadoGanPerCtasFuncion> Lista = new List<EEstadoGanPerCtasFuncion>();
            try
            {
                Lista = objContabilidadData.ListarEstadoGanPerCtasxFechaContable(cecoc_icod_centro_costo, vcocc_fecha_vcontable, indicador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarPEstadoGanPerCtasFuncion(EEstadoGanPerCtasFuncion obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarPEstadoGanPerCtasFuncion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }
        public void EliminarEstadoGanPerCtasFuncion(EEstadoGanPerCtasFuncion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.EliminarEstadoGanPerCtasFuncion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Inventario Resultado
        public List<EInventarioResultado> ListarInventarioResultado()
        {
            List<EInventarioResultado> Lista = new List<EInventarioResultado>();
            try
            {
                Lista = objContabilidadData.ListarInventarioResultado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public int InsertarInventarioResultado(EInventarioResultado obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarInventarioResultado(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }

        public void ModificarInventarioResultado(EInventarioResultado obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.ModificarInventarioResultado(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarInventarioResultado(EInventarioResultado obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //List<EPosFinanCta> ListaPosFinanCta = new List<EPosFinanCta>();
                    //ListaPosFinanCta = objContabilidadData.ListarPosicionFinancieraCtasxIcodPosFin(Convert.ToInt32(obj.posc_icod_forma_posi_finan));
                    //foreach (var item in ListaPosFinanCta)
                    //{
                    //item.PcModifica = obj.PcModifica;
                    //item.IdUsuarioModifica = obj.IdUsuarioModifica;
                    //objContabilidadData.EliminarPosicionFinancieraCtas(item);
                    //}
                    objContabilidadData.EliminarInventarioResultado(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Inventario Resultado Cuentas
        public List<EInventarioResultadoCtas> ListarInventarioResultadoCtasxIcodPosFin(int icod_pos_finan)
        {
            List<EInventarioResultadoCtas> Lista = new List<EInventarioResultadoCtas>();
            try
            {
                Lista = objContabilidadData.ListarInventarioResultadoCtasxIcodPosFin(icod_pos_finan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }



        public int InsertarInventarioResultadoCtas(EInventarioResultadoCtas obj)
        {
            int Cab_icod_correlativo;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cab_icod_correlativo = objContabilidadData.InsertarInventarioResultadoCtas(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cab_icod_correlativo;
        }

        public void EliminarInventarioResultadoCtas(EInventarioResultadoCtas obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objContabilidadData.EliminarInventarioResultadoCtas(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private List<EVoucherContableDet> getDetVoucherADPP(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
 List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
 List<EParametroContable> lstParametros, List<ELibroBancosDetalle> lstMovBancoDet)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            //List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
            try
            {
                decimal mto_sol = 0;
                decimal mto_dol = 0;
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_item_CompDet_01.vcocd_numero_doc);
                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_item_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                    }
                });
                obj_item_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;

                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);
                lstDetGeneral.Add(obj_item_CompDet_01);/***********************************************************/

                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }

                #endregion
                #region detalle 02
                lstMovBancoDet.ForEach(x =>
                {

                    EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                    obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                    obj_item_CompDet_02.vcocd_numero_doc = x.vnumero_doc;
                    var objTipoDoc = lstTipoDoc.Where(xx => xx.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0];
                    obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", objTipoDoc.tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);
                    obj_item_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(x.iid_cuenta_contable);
                    var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                    ETipoDocumentoDetalleCta objClaseDoc = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];

                    var lstProveedor = (new ComprasData()).ListarProveedor();
                    EProveedor obj_Proveedor = lstProveedor.Where(a => a.iid_icod_proveedor == oBeLB.proc_icod_proveedor).ToList()[0];

                    if (oBeLB.iid_tipo_moneda == 3)
                        if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));

                    if (oBeLB.iid_tipo_moneda == 4)
                        if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));
                    obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeLB.iid_tipo_moneda == 3) ? Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra);
                    Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        {
                            obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                            obj_item_CompDet_02.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                            obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", obj_item_CompDet_02.tablc_iid_tipo_analitica, obj_Proveedor.anac_iid_analitica);

                        }
                    });
                    obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_02.vcocd_vglosa_linea = oBeLB.vglosa;

                    obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 3) ? x.mto_mov_soles : Math.Round(x.mto_mov_soles * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 4) ? x.mto_mov_soles : Math.Round(x.mto_mov_soles / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                    obj_item_CompDet_02.intTipoOperacion = 1;
                    obj_item_CompDet_02.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                    lstCompDetalle.Add(obj_item_CompDet_02);
                    lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/

                    if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }

                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> getDetVoucherADCC(EVoucherContableCab oBe, ELibroBancos oBeLB, EBancoCuenta oBeBC,
 List<ECuentaContable> lstPlanCuentas, List<EVoucherContableDet> lstDetGeneral,
 List<EParametroContable> lstParametros, List<ELibroBancosDetalle> lstMovBancoDet)
        {
            List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
            //List<ELibroBancosDetalle> lstMovBancoDet = new List<ELibroBancosDetalle>();
            var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

            try
            {
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                obj_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);
                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 3) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 4) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_01);
                lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                {
                    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    lstCompDetalle = tuple.Item1;
                    lstDetGeneral = tuple.Item2;
                }
                #endregion
                #region detalle 02
                lstMovBancoDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                    obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                    obj_item_CompDet_02.vcocd_numero_doc = x.vnumero_doc;
                    var objTipoDoc = lstTipoDoc.Where(xx => xx.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0];
                    obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", objTipoDoc.tdocc_vabreviatura_tipo_doc, obj_item_CompDet_02.vcocd_numero_doc);
                    var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(obj_item_CompDet_02.tdocc_icod_tipo_doc);
                    ETipoDocumentoDetalleCta objClaseDoc = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];

                    var lstCliente = (new VentasData()).ListarCliente();
                    ECliente objCliente = lstCliente.Where(d => d.cliec_icod_cliente == oBeLB.cliec_icod_cliente).ToList()[0];

                    if (oBeLB.iid_tipo_moneda == 3)
                        if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));

                    if (oBeLB.iid_tipo_moneda == 4)
                        if (Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra) == 0)
                            throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA del Tipo y Clase de Doc. <<{0}:{1:00}>>, para la generación del voucher contable", objTipoDoc.tdocc_vabreviatura_tipo_doc, objClaseDoc.tdocd_iid_codigo_doc_det));
                    obj_item_CompDet_02.ctacc_icod_cuenta_contable = (oBeLB.iid_tipo_moneda == 3) ? Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(objClaseDoc.ctacc_icod_cuenta_contable_extra);

                    Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                        {
                            obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                            obj_item_CompDet_02.anad_icod_analitica = objCliente.anac_icod_analitica;
                        }
                    });
                    obj_item_CompDet_02.cecoc_icod_centro_costo = null;
                    obj_item_CompDet_02.vcocd_vglosa_linea = oBeLB.vglosa;

                    obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 3) ? x.mto_mov_soles : Math.Round(x.mto_mov_soles * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 4) ? x.mto_mov_soles : Math.Round(x.mto_mov_soles / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;

                    obj_item_CompDet_02.intTipoOperacion = 1;
                    obj_item_CompDet_02.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                    lstCompDetalle.Add(obj_item_CompDet_02);
                    lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/

                    if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                    {
                        var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                        lstCompDetalle = tuple.Item1;
                        lstDetGeneral = tuple.Item2;
                    }
                });
                #endregion
                #region totales y situación del voucher
                oBe.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                oBe.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                oBe.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                oBe.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                oBe.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (oBe.vcocc_nmto_tot_debe_sol == oBe.vcocc_nmto_tot_haber_sol &&
                        oBe.vcocc_nmto_tot_debe_dol == oBe.vcocc_nmto_tot_haber_dol)
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        oBe.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        oBe.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    oBe.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    oBe.strVcoSituacion = "Sin Detalle";
                }
                #endregion
                return lstDetGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
