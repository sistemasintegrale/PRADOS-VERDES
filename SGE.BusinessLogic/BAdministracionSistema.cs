using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGE.Entity;
using SGE.DataAccess;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BAdministracionSistema
    {
        AdministracionSistemaData objAdministracioData = new AdministracionSistemaData();
        #region Usuario
        public List<EUsuario> listarUsuarios()
        {
            List<EUsuario> lista = null;
            try
            {
                lista = objAdministracioData.listarUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarUsuario(EUsuario oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarUsuario(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarUsuario(EUsuario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarUsuario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarUsuario(EUsuario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarUsuario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Accesos de Usuario
        public List<EFormulario> listarAccesosNoPermitidos(int intUsuario)
        {
            List<EFormulario> lista = null;
            try
            {
                lista = objAdministracioData.listarAccesosNoPermitidos(intUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EFormulario> listarAccesosPermitidos(int intUsuario)
        {
            List<EFormulario> lista = null;
            try
            {
                lista = objAdministracioData.listarAccesosPermitidos(intUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAccesoUsuario(EFormulario oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarAccesoUsuario(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAccesoUsuario(EFormulario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarAccesoUsuario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControlEquipos Equipo_Obtner_Datos(string text, string idCpu)
        {
            return objAdministracioData.Equipo_Obtner_Datos(text, idCpu);
        }
        public List<ControlVersiones> Listar_Versiones()
        {
            return objAdministracioData.Listar_Versiones();
        }
        #endregion
        #region Tipo Documento
        public List<ETipoDocumento> listarTipoDocumento()
        {
            List<ETipoDocumento> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ETipoDocumento> listarTipoDocumentoPorModulo(int IdModulo)
        {
            List<ETipoDocumento> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumentoPorModulo(IdModulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoDocumento(ETipoDocumento oBe, List<EModulo> lstModulo)
        {
            int intIcod = 0;            
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoDocumento(oBe);
                    /*----------------------------------------------------------------*/
                    oBe.tdocc_icod_tipo_doc = intIcod;
                    /*----------------------------------------------------------------*/
                    objAdministracioData.insertarTipoDocumentoDetalle(oBe, lstModulo);
                    /*----------------------------------------------------------------*/
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Equipo_Modificar(ControlEquipos objEquipo)
        {
            objAdministracioData.Equipo_Modificar(objEquipo);
        }

        public void modificarTipoDocumento(ETipoDocumento oBe, List<EModulo> lstModulo)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoDocumento(oBe);
                    /*----------------------------------------------------------------*/
                    objAdministracioData.eliminarTipoDocumentoDetalle(oBe);
                    /*----------------------------------------------------------------*/
                    objAdministracioData.insertarTipoDocumentoDetalle(oBe, lstModulo);
                    /*----------------------------------------------------------------*/
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoDocumento(ETipoDocumento oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoDocumento(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo Documento Detalle
        public List<ETipoDocumentoDetalle> listarTipoDocumentoDetalle(ETipoDocumento oBe)
        {
            List<ETipoDocumentoDetalle> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumentoDetalle(oBe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Tipo Documento Detalle Cuenta
        public List<ETipoDocumentoDetalleCta> listarTipoDocumentoDetCta(int intTipoDoc)
        {
            List<ETipoDocumentoDetalleCta> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumentoDetCta(intTipoDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoDocumentoDetCta(ETipoDocumentoDetalleCta oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoDocumentoDetCta(oBe);                   
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoDocumentoDetCta(ETipoDocumentoDetalleCta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoDocumentoDetCta(oBe);                   
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoDocumentoDetCta(ETipoDocumentoDetalleCta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoDocumentoDetCta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Módulo
        public List<EModulo> listarModulo()
        {
            List<EModulo> lista = null;
            try
            {
                lista = objAdministracioData.listarModulo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarModulo(EModulo oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarModulo(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarModulo(EModulo oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarModulo(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarModulo(EModulo oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarModulo(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Parametro
        public void modificarCorrelativoOR(EParametro obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    (new AdministracionSistemaData()).modificarCorrelativoOR(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EParametro> listarParametro()
        {
            List<EParametro> lista = null;
            try
            {
                lista = objAdministracioData.listarParametro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarParametro(EParametro obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarParametro(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tabla
        public List<ETabla> listarTabla()
        {
            List<ETabla> lista = null;
            try
            {
                lista = objAdministracioData.listarTabla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTabla(ETabla oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTabla(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTabla(ETabla oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTabla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTabla(ETabla oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTabla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tabla Registro
        public int insertarTablaRegistro(ETablaRegistro oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTablaRegistro(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaRegistro(ETablaRegistro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTablaRegistro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaRegistro(ETablaRegistro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTablaRegistro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tabla Ventas Cab
        public List<ETablaVentaCab> listarTablaVentaCab()
        {
            List<ETablaVentaCab> lista = null;
            try
            {
                lista = objAdministracioData.listarTablaVentaCab();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTablaVentaCab(ETablaVentaCab oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTablaVentaCab(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaVentaCab(ETablaVentaCab oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTablaVentaCab(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaVentaCab(ETablaVentaCab oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTablaVentaCab(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tabla Venta Det
        public int insertarTablaVentaDet(ETablaVentaDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTablaVentaDet(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaVentaDet(ETablaVentaDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTablaVentaDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaVentaDet(ETablaVentaDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTablaVentaDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo Cambio
        public List<ETipoCambio> listarTipoCambio()
        {
            List<ETipoCambio> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoCambio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoCambio(ETipoCambio oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoCambio(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoCambio(ETipoCambio oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoCambio(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoCambio(ETipoCambio oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoCambio(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo Cambio Euro
        public List<ETipoCambioEuro> listarTipoCambioEuro(int intEjercicio)
        {
            List<ETipoCambioEuro> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoCambioEuro(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoCambioEuro(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoCambioEuro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoCambioEuro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public string GetCorrelativoDocumentoBancos(int anio, int mes, int cuenta, int tipo_doc)
        {
            string nro = "";
            try
            {
              //  nro = (new TesoreriaData()).GetCorrelativoDocumentoBancos(anio, mes, cuenta, tipo_doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nro;
        }
        public void ActualizarCorrelativoDocumentoBancos(int anio, int mes, int cuenta, int tipo_doc, string nro_doc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //objTesoreriaData.ActualizarCorrelativoDocumentoBancos(anio, mes, cuenta, tipo_doc, nro_doc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ETipoDocumento> getCorrelativoTipoDoc(int intTipoDoc)
        {
            try
            {
                var lst = new AdministracionSistemaData().getCorrelativoTipoDocumento(intTipoDoc);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateCorrelativoTipoDocumento(int intTipoDocumento, int intCorrelativo, int intOpcion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AdministracionSistemaData().updateCorrelativoTipoDocumento(intTipoDocumento, intCorrelativo, intOpcion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ObtenerCorrelativoDocumento(string serie, int Tipo_docuemnto)
        {
            string NumDocumento = null;
            try
            {
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoDocumento(serie, Tipo_docuemnto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        public string ObtenerCorrelativoOCL(int serie)
        {
            string NumDocumento = null;
            try
            {
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoOCL(serie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }

        public string ObtenerCorrelativoOCS(int serie)
        {
            string NumDocumento = null;
            try
            {
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoOCS(serie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }

        public string ObtenerCorrelativoOCI(int serie)
        {
            string NumDocumento = null;
            try
            {
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoOCI(serie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }

        public string ObtenerCorrelativoProyecto(int serie)
        {
            string NumDocumento = null;
            try
            {
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoProyecto(serie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }

    }
}
