using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGE.Entity;

namespace SGE.DataAccess
{
    public class AdministracionSistemaData
    {
        #region Usuario 
        public List<EUsuario> listarUsuarios()
        {
            List<EUsuario> lista = new List<EUsuario>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGES_USUARIO_LISTAR("ACCESSKEY");
                    foreach (var item in query)
                    {
                        lista.Add(new EUsuario()
                        {
                            usua_icod_usuario = item.usua_icod_usuario,
                            usua_codigo_usuario = item.usua_codigo_usuario.Trim(),
                            usua_nombre_usuario = item.usua_nombre_usuario.Trim(),
                            usua_password_usuario = item.usua_password_usuario,
                            usua_iactivo = Convert.ToBoolean(item.usua_iactivo),
                            strEstado = (Convert.ToBoolean(item.usua_iactivo)) ? "Activo" : "Inactivo",
                            usua_indicador_asesor = Convert.ToBoolean(item.usua_indicador_asesor),
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            usua_bweb = Convert.ToBoolean(item.usua_bweb)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarUsuario(EUsuario obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGES_USUARIO_INSERTAR(
                        ref intIcod,
                        obj.usua_codigo_usuario,
                        obj.usua_nombre_usuario,
                        obj.usua_password_usuario,
                        obj.usua_iactivo,
                        "ACCESSKEY",
                        obj.intUsuario,
                        obj.usua_indicador_asesor,
                        obj.vendc_icod_vendedor,
                        obj.usua_bweb
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarUsuario(EUsuario obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGES_USUARIO_MODIFICAR(
                        obj.usua_icod_usuario,
                        obj.usua_nombre_usuario,
                        obj.usua_password_usuario,
                        obj.usua_iactivo,
                        "ACCESSKEY",
                        obj.intUsuario,
                        obj.usua_indicador_asesor,
                        obj.vendc_icod_vendedor,
                        obj.usua_bweb
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarUsuario(EUsuario obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGES_USUARIO_ELIMINAR(
                        obj.usua_icod_usuario,
                        obj.intUsuario);
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
            List<EFormulario> lista = new List<EFormulario>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEAS_FORMULARIO_NO_PERMITIDO_LISTAR(intUsuario);
                    foreach (var item in query)
                    {

                        lista.Add(new EFormulario()
                        {
                            formc_icod_forms = item.formc_icod_forms,
                            moduc_icod_modulo = item.moduc_icod_modulo,
                            strModulo = item.moduc_vdescripcion,
                            formc_vnombre_forms = item.formc_vnombre_forms,
                            formc_vdescripcion = item.formc_vdescripcion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ControlVersiones> Listar_Versiones()
        {
            List<ControlVersiones> lista = new List<ControlVersiones>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.ACT_ACTUALIZACIONES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ControlVersiones()
                        {
                            cvr_icod_version = item.cvr_icod_version,
                            cvr_vversion = item.cvr_vversion,
                            cvr_sfecha_version = item.cvr_sfecha_version,
                            cvr_vurl = item.cvr_vurl,
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }
        public ControlEquipos Equipo_Obtner_Datos(string text, string idCpu)
        {
            ControlEquipos obj = new ControlEquipos();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.ACT_EQUIPO_DATOS_OBTENER(text, idCpu);
                    foreach (var item in query)
                    {
                        obj.ceq_icod_equipo = item.ceq_icod_equipo;
                        obj.ceq_vnombre_equipo = item.ceq_vnombre_equipo;
                        obj.cvr_icod_version = item.cvr_icod_version;
                        obj.ceq_sfecha_actualizacion = item.ceq_sfecha_actualizacion;
                        obj.cvr_vversion = item.cvr_vversion;
                        obj.cvr_sfecha_version = item.cvr_sfecha_version;
                        obj.cep_vubicacion_actualizador = item.cep_vubicacion_actualizador;
                        obj.cep_vid_cpu = item.cep_vid_cpu;
                        obj.cep_bflag_acceso = item.cep_bflag_acceso;
                        obj.cep_vubicacion_actualizador = item.cep_vubicacion_actualizador;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public void Equipo_Modificar(ControlEquipos objEquipo)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.ACT_EQUIPO_MODIFICAR(
                           objEquipo.ceq_icod_equipo,
                           objEquipo.ceq_vnombre_equipo,
                           objEquipo.cvr_icod_version,
                           objEquipo.ceq_sfecha_actualizacion,
                           objEquipo.cep_vubicacion_actualizador,
                           0
                           );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EFormulario> listarAccesosPermitidos(int intUsuario)
        {
            List<EFormulario> lista = new List<EFormulario>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEAS_FORMULARIO_PERMITIDO_LISTAR(intUsuario);
                    foreach (var item in query)
                    {

                        lista.Add(new EFormulario()
                        {
                            formc_icod_forms = item.formc_icod_forms,
                            moduc_icod_modulo = item.moduc_icod_modulo,
                            strModulo = item.moduc_vdescripcion,
                            formc_vnombre_forms = item.formc_vnombre_forms,
                            formc_vdescripcion = item.formc_vdescripcion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAccesoUsuario(EFormulario obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_ACCESO_USUARIO_INSERTAR(
                        ref intIcod,
                        obj.intUsuarioAcceso,
                        obj.formc_icod_forms,
                        obj.intUsuario
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAccesoUsuario(EFormulario obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_ACCESO_USUARIO_ELIMINAR(
                        obj.intUsuarioAcceso,
                        obj.formc_icod_forms);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tipo Documento
        public List<ETipoDocumento> listarTipoDocumentoPorModulo(int IdModulo)
        {
            List<ETipoDocumento> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoDocumento>();
                    var query = dc.SGEAS_TIPO_DOCUMENTO_MODULO_LISTAR(IdModulo);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumento()
                        {
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdocc_iid_tipo_doc = Convert.ToInt32(item.tdocc_iid_tipo_doc),
                            tdocc_vdescripcion = item.tdocc_vdescripcion,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ETipoDocumento> listarTipoDocumento()
        {
            List<ETipoDocumento> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoDocumento>();
                    var query = dc.SGEAS_TIPO_DOCUMENTO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumento()
                        {
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdocc_iid_tipo_doc = Convert.ToInt32(item.tdocc_iid_tipo_doc),
                            tdocc_vdescripcion = item.tdocc_vdescripcion,
                            tdocc_coa = item.tdocc_coa,
                            tdocc_nro_correlativo = item.tdocc_nro_correlativo,
                            tdocc_nro_correlativo_2 = item.tdocc_nro_correlativo_2,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            tdocc_nro_serie = item.tdocc_nro_serie,
                            tdocc_nro_serie_2 = item.tdocc_nro_serie_2,
                            strNroCorrelativo = (Convert.ToInt32(item.tdocc_nro_correlativo) != 0) ? String.Format("{0:00000000000}", Convert.ToInt32(item.tdocc_nro_correlativo)) : "",
                            strNroCorrelativo_2 = (Convert.ToInt32(item.tdocc_nro_correlativo_2) != 0) ? String.Format("{0:00000000000}", Convert.ToInt32(item.tdocc_nro_correlativo_2)) : ""
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoDocumento(ETipoDocumento oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_TIPO_DOCUMENTO_INSERTAR(
                    ref intIcod,
                    oBe.tdocc_iid_tipo_doc,
                    oBe.tdocc_vabreviatura_tipo_doc,
                    oBe.tdocc_vdescripcion,
                    oBe.tdocc_coa,
                    oBe.tdocc_nro_correlativo,
                    oBe.intUsuario,
                    oBe.tdocc_nro_serie,
                    oBe.tdocc_nro_serie_2,
                    oBe.tdocc_nro_correlativo_2);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoDocumento(ETipoDocumento oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_MODIFICAR(
                    oBe.tdocc_icod_tipo_doc,
                    oBe.tdocc_vdescripcion,
                    oBe.tdocc_coa,
                    oBe.tdocc_nro_correlativo,
                    oBe.intUsuario,
                    oBe.tdocc_nro_serie,
                    oBe.tdocc_nro_serie_2,
                    oBe.tdocc_nro_correlativo_2);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_ELIMINAR(
                        oBe.tdocc_icod_tipo_doc,
                        oBe.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ETipoDocumento> getCorrelativoTipoDocumento(int intTipoDocumento)
        {
            int? intCorrelativo = 0;
            string strNroSerie = "";
            int? intCorrelativo2 = 0;
            string strNroSerie2 = "";
            List<ETipoDocumento> lst = new List<ETipoDocumento>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_GET_CORRELATIVO_TIPO_DOC(
                    ref intCorrelativo,
                    ref strNroSerie,
                    ref intCorrelativo2,
                    ref strNroSerie2,
                    intTipoDocumento);

                    lst.Add(new ETipoDocumento()
                    {
                        tdocc_nro_serie = strNroSerie,
                        tdocc_nro_correlativo = intCorrelativo,
                        tdocc_nro_serie_2 = strNroSerie2,
                        tdocc_nro_correlativo_2 = intCorrelativo2
                    });

                }
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_UPDATE_CORRELATIVO_TIPO_DOC(
                    intTipoDocumento,
                    intCorrelativo,
                    intOpcion);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_DOCUMENTO(serie, Tipo_docuemnto);
                    foreach (var item in query)
                    {
                        NumDocumento = item.NumDocumento;
                    }
                }
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_OCL(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_OCS(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_OCI(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_PROYECTO(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:000000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        #endregion  
        #region Tipo Documento Detalle
        public List<ETipoDocumentoDetalle> listarTipoDocumentoDetalle(ETipoDocumento oBe)
        {
            List<ETipoDocumentoDetalle> lista = new List<ETipoDocumentoDetalle>();

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEAS_TIPO_DOCUMENTO_DET_LISTAR(oBe.tdocc_icod_tipo_doc);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumentoDetalle()
                        {
                            moduc_icod_modulo = item.moduc_icod_modulo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarTipoDocumentoDetalle(ETipoDocumento oBe, List<EModulo> lstModulo)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    foreach (EModulo x in lstModulo)
                    {
                        if (x.moduc_flag_estado == true)
                        {
                            dc.SGEAS_TIPO_DOCUMENTO_DET_INSERTAR(
                            oBe.tdocc_icod_tipo_doc,
                            x.moduc_icod_modulo,
                            oBe.intUsuario
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarTipoDocumentoDetalle(ETipoDocumento oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_ELIMINAR(
                        oBe.tdocc_icod_tipo_doc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo Documento Clase
        public List<ETipoDocumentoDetalleCta> listarTipoDocumentoDetCta(int intTipoDoc)
        {
            List<ETipoDocumentoDetalleCta> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoDocumentoDetalleCta>();
                    var query = dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_LISTAR(intTipoDoc);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumentoDetalleCta()
                        {
                            tdocd_iid_correlativo = item.tdocd_iid_correlativo,
                            tdocd_iid_codigo_doc_det = item.tdocd_iid_codigo_doc_det,
                            tdocd_descripcion = item.tdocd_descripcion,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            ctacc_icod_cuenta_contable_nac = item.ctacc_icod_cuenta_contable_nac,
                            ctacc_icod_cuenta_contable_extra = item.ctacc_icod_cuenta_contable_extra,
                            ctacc_icod_cuenta_matris_nac = item.ctacc_icod_cuenta_matris_nac,
                            ctacc_icod_cuenta_matris_extra = item.ctacc_icod_cuenta_matris_extra,
                            ctacc_icod_subcuenta_nac = item.ctacc_icod_subcuenta_nac,
                            ctacc_icod_subcuenta_extra = item.ctacc_icod_subcuenta_extra,
                            ctacc_icod_cuenta_asociada_nac = item.ctacc_icod_cuenta_asociada_nac,
                            ctacc_icod_cuenta_asociada_extra = item.ctacc_icod_cuenta_asociada_extra,
                            ctacc_icod_cuenta_igv_nac = item.ctacc_icod_cuenta_igv_nac,
                            ctacc_icod_cuenta_isc = item.ctacc_icod_cuenta_isc,
                            ctacc_icod_cuenta_gastos_nac = item.ctacc_icod_cuenta_gastos_nac,
                            ctacc_icod_cuenta_ivap = item.ctacc_icod_cuenta_ivap,
                            tdocd_iestado_registro = item.tdocd_iestado_registro,
                            tdocd_estado_coa = item.tdocd_estado_coa,
                            reg_compra = (item.tdocd_iestado_registro == 0) ? "*" : "",
                            reg_venta = (item.tdocd_iestado_registro == 1) ? "*" : "",
                            ctacc_icod_cuenta_servicios = item.ctacc_icod_cuenta_servicios
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoDocumentoDetCta(ETipoDocumentoDetalleCta obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_INSERTAR(
                        ref intIcod,
                        obj.tdocd_iid_codigo_doc_det,
                        obj.tdocd_descripcion,
                        obj.tdocc_icod_tipo_doc,
                        obj.ctacc_icod_cuenta_contable_nac,
                        obj.ctacc_icod_cuenta_contable_extra,
                        obj.ctacc_icod_cuenta_matris_nac,
                        obj.ctacc_icod_cuenta_matris_extra,
                        obj.ctacc_icod_subcuenta_nac,
                        obj.ctacc_icod_subcuenta_extra,
                        obj.ctacc_icod_cuenta_asociada_nac,
                        obj.ctacc_icod_cuenta_asociada_extra,
                        obj.ctacc_icod_cuenta_igv_nac,
                        obj.ctacc_icod_cuenta_isc,
                        obj.ctacc_icod_cuenta_gastos_nac,
                        obj.ctacc_icod_cuenta_ivap,
                        obj.tdocd_iestado_registro,
                        obj.tdocd_estado_coa,
                        obj.intUsuario,
                        obj.ctacc_icod_cuenta_servicios
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoDocumentoDetCta(ETipoDocumentoDetalleCta obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_MODIFICAR(
                        obj.tdocd_iid_correlativo,
                        obj.tdocd_descripcion,
                        obj.tdocc_icod_tipo_doc,
                        obj.ctacc_icod_cuenta_contable_nac,
                        obj.ctacc_icod_cuenta_contable_extra,
                        obj.ctacc_icod_cuenta_matris_nac,
                        obj.ctacc_icod_cuenta_matris_extra,
                        obj.ctacc_icod_subcuenta_nac,
                        obj.ctacc_icod_subcuenta_extra,
                        obj.ctacc_icod_cuenta_asociada_nac,
                        obj.ctacc_icod_cuenta_asociada_extra,
                        obj.ctacc_icod_cuenta_igv_nac,
                        obj.ctacc_icod_cuenta_isc,
                        obj.ctacc_icod_cuenta_gastos_nac,
                        obj.ctacc_icod_cuenta_ivap,
                        obj.tdocd_iestado_registro,
                        obj.tdocd_estado_coa,
                        obj.intUsuario,
                        obj.ctacc_icod_cuenta_servicios);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoDocumentoDetCta(ETipoDocumentoDetalleCta obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_ELIMINAR(
                        obj.tdocd_iid_correlativo,
                        obj.tdocc_icod_tipo_doc);
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
            int cont = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<EModulo>();
                    var query = dc.SGEAS_MODULO_LISTAR();
                    foreach (var item in query)
                    {
                        cont += 1;
                        lista.Add(new EModulo()
                        {
                            intCorrelativo = cont,
                            moduc_icod_modulo = item.moduc_icod_modulo,
                            moduc_vdescripcion = item.moduc_vdescripcion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarModulo(EModulo oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_MODULO_INSERTAR(
                    ref intIcod,
                    oBe.moduc_vdescripcion,
                    oBe.intUsuario);
                }
                return Convert.ToInt32(intIcod);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_MODULO_MODIFICAR(
                    oBe.moduc_icod_modulo,
                    oBe.moduc_vdescripcion,
                    oBe.intUsuario);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_MODULO_ELIMINAR(
                        oBe.moduc_icod_modulo,
                        oBe.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Parametro
        public void modificarCorrelativoOR(EParametro oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_PARAMETRO_MODIFICAR_CORRELATIVO_OR(
                        oBe.pm_icod_parametro,
                        oBe.pm_correlativo_OR);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EParametro> listarParametro()
        {
            List<EParametro> lista = new List<EParametro>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGAS_PARAMETRO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EParametro()
                        {
                            pm_icod_parametro = item.pm_icod_parametro,
                            pm_nigv_parametro = item.pm_nigv_parametro,
                            pm_ntope_parametro = item.pm_ntope_parametro,
                            pm_nuit_parametro = item.pm_nuit_parametro,
                            pm_ncategoria_parametro = item.pm_ncategoria_parametro,
                            pm_nivap_parametro = item.pm_nivap_parametro,
                            pm_nisc_parametro = item.pm_nisc_parametro,
                            pm_nombre_empresa = item.pm_nombre_empresa,
                            pm_direccion_empresa = item.pm_direccion_empresa,
                            pm_vruc = item.pm_vruc,
                            pm_correlativo_OR = Convert.ToInt64(item.pm_correlativo_OR),
                            pm_ntipo_cambio = Convert.ToDecimal(item.pm_ntipo_cambio),
                            urlServicioFacturaElectronica = item.urlServicioFacturaElectronica,
                            urlServicioNotaCredito = item.urlServicioNotaCredito,
                            urlServicioNotaDebito = item.urlServicioNotaDebito,
                            Ruc = item.Ruc,
                            UsuarioSol = item.UsuarioSol,
                            ClaveSol = item.ClaveSol,
                            EndPointUrlPrueba = item.EndPointUrlPrueba,
                            EndPointUrlDesarrollo = item.EndPointUrlDesarrollo,
                            PasswordCertificado = item.PasswordCertificado,
                            CertificadoDigital = item.CertificadoDigital,
                            urlServicioEnviarDocumento = item.urlServicioEnviarDocumento,
                            urlServicioFirma = item.urlServicioFirma,
                            IdServiceValidacion = item.IdServiceValidacion,
                            pm_sfecha_inicio = Convert.ToDateTime(item.pm_sfecha_inicio),
                            DirecciónXML = item.DirecciónXML,
                            urlServicioEnvioResumen = item.urlServicioEnvioResumen,
                            urlServicoGenerarResumen = item.urlServicoGenerarResumen,
                            IdServiceValidacionResumen = item.IdServiceValidacionResumen,
                            ServiceConsultaTiket = item.ServiceConsultaTiket,
                            pm_vruta_resumen = item.pm_vruta_resumen
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarParametro(EParametro oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_PARAMETRO_MODIFICAR(
                        oBe.pm_icod_parametro,
                        oBe.pm_nigv_parametro,
                        oBe.pm_nuit_parametro,
                        oBe.pm_ncategoria_parametro,
                        oBe.pm_nombre_empresa,
                        oBe.pm_direccion_empresa,
                        oBe.pm_vruc,
                        oBe.intUsuario,
                        oBe.pm_correlativo_OR,
                        oBe.pm_ntipo_cambio,
                        oBe.urlServicioFacturaElectronica,
                        oBe.urlServicioNotaCredito,
                        oBe.urlServicioNotaDebito,
                        oBe.Ruc,
                        oBe.UsuarioSol,
                        oBe.ClaveSol,
                        oBe.EndPointUrlPrueba,
                        oBe.EndPointUrlDesarrollo,
                        oBe.PasswordCertificado,
                        oBe.CertificadoDigital,
                        oBe.urlServicioEnviarDocumento,
                        oBe.urlServicioFirma,
                        oBe.IdServiceValidacion,
                        oBe.pm_sfecha_inicio,
                        oBe.DirecciónXML,
                        oBe.urlServicioEnvioResumen,
                        oBe.urlServicoGenerarResumen,
                        oBe.IdServiceValidacionResumen,
                        oBe.ServiceConsultaTiket,
                        oBe.pm_vruta_resumen
                        );

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
            List<ETabla> lista = new List<ETabla>(); ;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_TABLA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETabla()
                        {
                            tablc_iid_tipo_tabla = item.tablc_iid_tipo_tabla,
                            tablc_vdescripcion = item.tablc_vdescripcion.Trim(),
                            tablc_cestado = Convert.ToChar(item.tablc_cestado),
                            strEstado = (item.tablc_cestado == 'A') ? "Activo" : "Inactivo"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTabla(ETabla obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_INSERTAR(
                        ref intIcod,
                        obj.tablc_vdescripcion,
                        obj.tablc_cestado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTabla(ETabla obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_MODIFICAR(
                        obj.tablc_iid_tipo_tabla,
                        obj.tablc_vdescripcion,
                        obj.tablc_cestado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTabla(ETabla obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_ELIMINAR(
                        obj.tablc_iid_tipo_tabla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tabla Registro

        public int insertarTablaRegistro(ETablaRegistro obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_REGISTRO_INSERTAR(
                        ref intIcod,
                        obj.tablc_iid_tipo_tabla,
                        obj.tarec_icorrelativo_registro,
                        obj.tarec_vdescripcion,
                        obj.tarec_cestado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaRegistro(ETablaRegistro obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_REGISTRO_MODIFICAR(
                        obj.tarec_iid_tabla_registro,
                        obj.tarec_vdescripcion,
                        obj.tarec_cestado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaRegistro(ETablaRegistro obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_REGISTRO_ELIMINAR(
                        obj.tarec_iid_tabla_registro);
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
            List<ETablaVentaCab> lista = new List<ETablaVentaCab>(); ;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_TABLA_VENTA_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaVentaCab()
                        {
                            tabvc_iid_tipo_tabla = item.tabvc_iid_tipo_tabla,
                            tabvc_vdescripcion = item.tabvc_vdescripcion.Trim(),
                            tabvc_cestado = Convert.ToChar(item.tabvc_cestado),
                            strEstado = (item.tabvc_cestado == 'A') ? "Activo" : "Inactivo"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTablaVentaCab(ETablaVentaCab obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_VENTA_CAB_INSERTAR(
                        ref intIcod,
                        obj.tabvc_vdescripcion,
                        obj.tabvc_cestado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaVentaCab(ETablaVentaCab obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_VENTA_CAB_MODIFICAR(
                        obj.tabvc_iid_tipo_tabla,
                        obj.tabvc_vdescripcion,
                        obj.tabvc_cestado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaVentaCab(ETablaVentaCab obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_VENTA_CAB_ELIMINAR(
                        obj.tabvc_iid_tipo_tabla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tabla Ventas Det

        public int insertarTablaVentaDet(ETablaVentaDet obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_VENTA_DET_INSERTAR(
                        ref intIcod,
                        obj.tabvc_iid_tipo_tabla,
                        obj.tabvd_icorrelativo_venta_det,
                        obj.tabvd_vdesc_abreviado,
                        obj.tabvd_vdescripcion,
                        obj.tabvd_cestado,
                        obj.tabvd_icod_ref
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaVentaDet(ETablaVentaDet obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_VENTA_DET_MODIFICAR(
                        obj.tabvd_iid_tabla_venta_det,
                        obj.tabvd_vdesc_abreviado,
                        obj.tabvd_vdescripcion,
                        obj.tabvd_cestado,
                        obj.tabvd_icod_ref
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaVentaDet(ETablaVentaDet obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_VENTA_DET_ELIMINAR(
                        obj.tabvd_iid_tabla_venta_det
                        );
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoCambio>();
                    var query = dc.SGEAS_TIPO_CAMBIO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoCambio()
                        {
                            ticac_icod_tipo_cambio = item.ticac_icod_tipo_cambio,
                            ticac_fecha_tipo_cambio = Convert.ToDateTime(item.ticac_fecha_tipo_cambio),
                            ticac_tipo_cambio_compra = Convert.ToDecimal(item.ticac_tipo_cambio_compra),
                            ticac_tipo_cambio_venta = Convert.ToDecimal(item.ticac_tipo_cambio_venta)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoCambio(ETipoCambio oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_TIPO_CAMBIO_INSERTAR(
                    ref intIcod,
                    oBe.ticac_fecha_tipo_cambio,
                    oBe.ticac_tipo_cambio_compra,
                    oBe.ticac_tipo_cambio_venta,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ticac_flag_estado);
                }
                return Convert.ToInt32(intIcod);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_MODIFICAR(
                    oBe.ticac_icod_tipo_cambio,
                    oBe.ticac_fecha_tipo_cambio,
                    oBe.ticac_tipo_cambio_compra,
                    oBe.ticac_tipo_cambio_venta,
                    oBe.intUsuario,
                    oBe.strPc);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_ELIMINAR(
                        oBe.ticac_icod_tipo_cambio,
                        oBe.intUsuario,
                        oBe.strPc);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoCambioEuro>();
                    var query = dc.SGEAS_TIPO_CAMBIO_EURO_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoCambioEuro()
                        {
                            tceu_icod_tipo_cambio_euro = item.tceu_icod_tipo_cambio_euro,
                            tceu_sfecha_tipo_cambio_euro = Convert.ToDateTime(item.tceu_sfecha_tipo_cambio_euro),
                            tceu_tipo_cambio_euro_compra = Convert.ToDecimal(item.tceu_tipo_cambio_euro_compra),
                            tceu_tipo_cambio_euro_venta = Convert.ToDecimal(item.tceu_tipo_cambio_euro_venta)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_TIPO_CAMBIO_EURO_INSERTAR(
                    ref intIcod,
                    oBe.tceu_sfecha_tipo_cambio_euro,
                    oBe.tceu_tipo_cambio_euro_compra,
                    oBe.tceu_tipo_cambio_euro_venta,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.tceu_flag_estado);
                }
                return Convert.ToInt32(intIcod);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_EURO_MODIFICAR(
                    oBe.tceu_icod_tipo_cambio_euro,
                    oBe.tceu_tipo_cambio_euro_compra,
                    oBe.tceu_tipo_cambio_euro_venta,
                    oBe.intUsuario,
                    oBe.strPc);
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
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_EURO_ELIMINAR(
                        oBe.tceu_icod_tipo_cambio_euro,
                        oBe.intUsuario,
                        oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public void updateCorrelativoTipoDocumentoRP(int intTipoDocumento, int intCorrelativo, int intOpcion)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_UPDATE_CORRELATIVO_RP(
                    intTipoDocumento,
                    intCorrelativo,
                    intOpcion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
