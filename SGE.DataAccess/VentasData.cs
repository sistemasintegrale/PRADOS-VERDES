using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGE.Entity;
using System.Globalization;
using SGE.Entity.FacturaElectronica;
using System.Data;
using System.Data.SqlClient;

namespace SGE.DataAccess
{
    public class VentasData
    {
        #region Cliente
        public List<ECliente> ListarCliente()
        {
            List<ECliente> lista = new List<ECliente>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGES_CLIENTE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECliente()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            giroc_icod_giro = Convert.ToInt32(item.giroc_icod_giro),
                            giroc_vnombre_giro = item.giroc_vnombre_giro,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            cliec_vnombre_comercial = item.cliec_vnombre_comercial,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            ubicc_icod_ubicacion = item.ubicc_icod_ubicacion,
                            ubicc_vnombre_ubicacion = item.ubicc_vnombre_ubicacion,
                            cliec_vnro_telefono = item.cliec_vnro_telefono,
                            cliec_vnro_fax = item.cliec_vnro_fax,
                            cliec_vnro_celular = item.cliec_vnro_celular,
                            tabl_iid_tipo_documento = item.tabl_iid_tipo_documento,
                            TipoDoc = item.TipoDoc,
                            cliec_vnumero_doc_cli = (item.cliec_vnumero_doc_cli == null) ? "" : item.cliec_vnumero_doc_cli,
                            cliec_vnombre_contacto = (item.cliec_vnombre_contacto == null) ? "" : item.cliec_vnombre_contacto,
                            tablc_iid_tipo_relacion_cli = item.tablc_iid_tipo_relacion_cli,
                            EmpresaRelacion = item.EmpresaRelacion,
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            vendc_vnombre_vendedor = (item.vendc_vnombre_vendedor == null) ? "" : item.vendc_vnombre_vendedor,
                            vendc_iid_vendedor = item.vendc_iid_vendedor,
                            cliec_sfecha_registro_cliente = item.cliec_sfecha_registro_cliente,
                            cliec_iid_situacion_cliente = Convert.ToInt32(item.cliec_iid_situacion_cliente),
                            Situacion = item.Situacion,
                            cliec_vcorreo_electronico = item.cliec_vcorreo_electronico,
                            cliec_vapellido_paterno = item.cliec_vapellido_paterno,
                            cliec_vapellido_materno = item.cliec_vapellido_materno,
                            cliec_vnombres = item.cliec_vnombres,
                            tablc_iid_tipo_cliente = item.tablc_iid_tipo_cliente,
                            cliec_cruc = string.IsNullOrEmpty(item.cliec_cruc) ? "" : item.cliec_cruc,
                            cambiar = true,
                            cliec_icod_autoventa = item.cliec_icod_autoventa,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            anac_icod_analitica = item.anac_icod_analitica,
                            anac_iid_analitica = item.anad_iid_analitica,
                            pcomc_icod_pcompra = item.pcomc_icod_pcompra,
                            cliec_bcredito = item.cliec_bcredito,
                            cliec_nnumero_dias = item.cliec_nnumero_dias,


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

        public DataTable PlanNecisidadSepulturaReporte()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(Helper.conexion()))
            {
                SqlCommand command = new SqlCommand("SGEV_REPORTE_PRECIO_LISTA", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataTable);


            }
            return dataTable;
        }

        public bool PlanNecisidadSepulturaValidar(EPlanNecesidadSepultura obe)
        {
            bool? result = false;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLAN_NECESIDAD_SEPULTURA_VALIDAR_EXISTENCIA(
                        obe.id,
                        obe.icod_tipo_sepultura,
                        obe.icod_tipo_plan,
                        obe.icod_nombre_plan,
                        ref result
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result.Value;
        }

        public void PlanNecisidadSepulturaDetalleSave(EPlanNecesidadSepulturaDetalle obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLAN_NECESIDAD_SEPULTURA_DETALLE_SAVE(
                        obe.id,
                        obe.id_cab,
                        obe.icantidad_cuotas,
                        obe.nmonto,
                        obe.intUsuario,
                        obe.strPc,
                        DateTime.Now
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void PlanNecisidadSepulturaDetalleEliminar(EPlanNecesidadSepulturaDetalle obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext())
                {
                    dc.SGE_PLAN_NECESIDAD_SEPULTURA_DETALLE_ELIMINAR(
                        obe.id,
                        obe.intUsuario,
                        obe.strPc,
                        DateTime.Now
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EPlanNecesidadSepulturaDetalle> PlanNecisidadSepulturaDetalleListar(int id)
        {
            var lista = new List<EPlanNecesidadSepulturaDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PLAN_NECESIDAD_SEPULTURA_DETALLE_LISTAR(id);

                    foreach (var item in collection)
                    {
                        lista.Add(new EPlanNecesidadSepulturaDetalle()
                        {
                            id = item.id,
                            id_cab = item.id_cab,
                            nmonto = item.nmonto,
                            icantidad_cuotas = item.icantidad_cuotas,

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

        public void PlanNecisidadSepulturaGuaradar(EPlanNecesidadSepultura obe)
        {

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLAN_NECESIDAD_SEPULTURA_SAVE(
                        obe.id,
                        obe.icod_tipo_sepultura,
                        obe.icod_tipo_plan,
                        obe.icod_nombre_plan,
                        obe.nprecio_lista,
                        obe.ncuota_inicial,
                        obe.intUsuario,
                        obe.strPc,
                        DateTime.Now,
                        obe.nmonto_descuento
                        );


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<EPlanNecesidadSepultura> PlanNecisidadSepulturaListar(int tabvd_iid_tabla_venta_det, int icod_tipo_plan, int icod_nombre_plan)
        {
            var lista = new List<EPlanNecesidadSepultura>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PLAN_NECESIDAD_SEPULTURA_LISTAR(tabvd_iid_tabla_venta_det, icod_tipo_plan, icod_nombre_plan);
                    foreach (var item in collection)
                    {
                        lista.Add(new EPlanNecesidadSepultura
                        {
                            id = item.id,
                            icod_nombre_plan = item.icod_nombre_plan,
                            icod_tipo_plan = item.icod_tipo_plan,
                            icod_tipo_sepultura = item.icod_tipo_sepultura,
                            nprecio_lista = item.nprecio_lista,
                            ncuota_inicial = item.ncuota_inicial,
                            nmonto_descuento = Convert.ToDecimal(item.nmonto_descuento)
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

        public void PlanNecisidadSepulturaEliminar(EPlanNecesidadSepultura select)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLAN_NECESIDAD_SEPULTURA_ELIMINAR(
                        select.id,
                        select.intUsuario,
                        select.strPc,
                        DateTime.Now
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EContrato> listarContratoPorFechas(DateTime fechaIncio, DateTime fechaFinal, int tipoContrato)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 99999999;
                    var query = dc.SGEV_CONTRATOS_LISTAR_POR_FECHAS_2(fechaIncio, fechaFinal, tipoContrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EContrato()
                        {

                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_sfecha_contrato = Convert.ToDateTime(item.cntc_sfecha_contrato),
                            cntc_icod_vendedor = Convert.ToInt32(item.cntc_icod_vendedor),
                            cntc_iorigen_venta = Convert.ToInt32(item.cntc_iorigen_venta),
                            cntc_icod_funeraria = Convert.ToInt32(item.cntc_icod_funeraria),
                            cntc_vnombre_comercial = item.cntc_vnombre_comercial,
                            cntc_vunidad_venta = item.cntc_vunidad_venta,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante == null ? "" : item.cntc_vdni_contratante,
                            cntc_vruc_contratante = item.cntc_vruc_contratante,
                            cntc_sfecha_nacimineto_contratante = Convert.ToDateTime(item.cntc_sfecha_nacimineto_contratante),
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante,
                            cntc_vdireccion_contratante = item.cntc_vdireccion_contratante,
                            cntc_inacionalidad_contratante = Convert.ToInt32(item.cntc_inacionalidad_contratante),
                            cntc_itipo_documento_contratante = Convert.ToInt32(item.cntc_itipo_documento_contratante),
                            cntc_vnombre_representante = item.cntc_vnombre_representante,
                            cntc_vapellido_paterno_representante = item.cntc_vapellido_paterno_representante,
                            cntc_vapellido_materno_representante = item.cntc_vapellido_materno_representante,
                            cntc_vdni_representante = item.cntc_vdni_representante,
                            cntc_ruc_representante = item.cntc_ruc_representante,
                            cntc_sfecha_nacimiento_representante = Convert.ToDateTime(item.cntc_sfecha_nacimiento_representante),
                            cntc_iestado_civil_representante = Convert.ToInt32(item.cntc_iestado_civil_representante),
                            cntc_inacionalidad_respresentante = Convert.ToInt32(item.cntc_inacionalidad_respresentante),
                            cntc_vtelefono_representante = item.cntc_vtelefono_representante,
                            cntc_vdireccion_representante = item.cntc_vdireccion_representante,
                            cntc_vnumero_direccion_representante = item.cntc_vnumero_direccion_representante,
                            cntc_vdepartamento_representante = item.cntc_vdepartamento_representante,
                            cntc_idistrito_representante = Convert.ToInt32(item.cntc_idistrito_representante),
                            cntc_vcodigo_postal_representante = item.cntc_vcodigo_postal_representante,
                            cntc_itipo_documento_representante = Convert.ToInt32(item.cntc_itipo_documento_representante),
                            cntc_vdocumento_respresentante = item.cntc_vdocumento_respresentante,
                            cntc_vnombre_beneficiario = item.cntc_vnombre_beneficiario,
                            cntc_vapellido_paterno_beneficiario = item.cntc_vapellido_paterno_beneficiario,
                            cntc_vapellido_materno_beneficiario = item.cntc_vapellido_materno_beneficiario,
                            cntc_vdni_beneficiario = item.cntc_vdni_beneficiario,
                            cntc_sfecha_nacimiento_beneficiario = Convert.ToDateTime(item.cntc_sfecha_nacimiento_beneficiario),
                            cntc_vdireccion_beneficiario = item.cntc_vdireccion_beneficiario,
                            cntc_inacionalidad = Convert.ToInt32(item.cntc_inacionalidad),
                            cntc_icodigo_plan = Convert.ToInt32(item.cntc_icodigo_plan),
                            cntc_icod_nombre_plan = Convert.ToInt32(item.cntc_icod_nombre_plan),
                            cntc_vnombre_plan = item.cntc_vnombre_plan,
                            cntc_nprecio_lista = Convert.ToDecimal(item.cntc_nprecio_lista),
                            cntc_ndescuento = Convert.ToDecimal(item.cntc_ndescuento),
                            cntc_ninhumacion = Convert.ToDecimal(item.cntc_ninhumacion),
                            cntc_naporte_fondo = Convert.ToDecimal(item.cntc_naporte_fondo),
                            cntc_nIGV = Convert.ToDecimal(item.cntc_nIGV),
                            cntc_nprecio_total = Convert.ToDecimal(item.cntc_nprecio_total),
                            cntc_itipo_sepultura = Convert.ToInt32(item.cntc_itipo_sepultura),
                            cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato,
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total,
                            cntc_vurnas = item.cntc_vurnas,
                            cntc_vservico_inhumacion = item.cntc_vservico_inhumacion,
                            cntc_icod_plataforma = Convert.ToInt32(item.cntc_icod_plataforma),
                            cntc_icod_manzana = Convert.ToInt32(item.cntc_icod_manzana),
                            cntc_icod_isepultura = Convert.ToInt32(item.cntc_icod_isepultura),
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espac_icod_vespacios = item.espac_icod_vespacios,
                            cntc_bnivel1 = item.cntc_bnivel1,
                            cntc_bnivel2 = item.cntc_bnivel2,
                            cntc_bnivel3 = item.cntc_bnivel3,
                            cntc_bnivel4 = item.cntc_bnivel4,
                            cntc_bnivel5 = item.cntc_bnivel5,
                            cntc_bnivel6 = item.cntc_bnivel6,
                            espad_iid_iespacios1 = Convert.ToInt32(item.espad_iid_iespacios1),
                            espad_iid_iespacios2 = Convert.ToInt32(item.espad_iid_iespacios2),
                            espad_iid_iespacios3 = Convert.ToInt32(item.espad_iid_iespacios3),
                            espad_iid_iespacios4 = Convert.ToInt32(item.espad_iid_iespacios4),
                            espad_iid_iespacios5 = Convert.ToInt32(item.espad_iid_iespacios5),
                            espad_iid_iespacios6 = Convert.ToInt32(item.espad_iid_iespacios6),
                            cntc_vcodigo_sepultura = item.cntc_vcodigo_sepultura,
                            cntc_vnumero_reserva = item.cntc_vnumero_reserva,
                            strcodigoplan = item.strcodigoplan,
                            strNombreplan = item.strNombreplan,
                            strorigenventa = item.strorigenventa,
                            strtiposepultura = item.strtiposepultura,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strNombreIEC = item.strNombreIEC,
                            cntc_flag_estado = Convert.ToBoolean(item.cntc_flag_estado),
                            strsepultura = item.strsepultura,
                            strnacionalidad = item.strnacionalidad,
                            strnacionalidadfallec = item.strnacionalidadfallec,
                            strestadocivil = item.strestadocivil,
                            strdistrito = item.strdistrito,
                            cntc_icod_situacion = Convert.ToInt32(item.cntc_icod_situacion),
                            strSituacion = item.strSituacion,
                            cntc_ncuota_inicial = Convert.ToDecimal(item.cntc_ncuota_inicial),
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            cntc_nmonto_cuota = Convert.ToDecimal(item.cntc_nmonto_cuota),
                            cntc_sfecha_cuota = item.cntc_sfecha_cuota,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_vdescripcion_anulacion = item.cntc_vdescripcion_anulacion,
                            cntc_icod_indicador_espacios = Convert.ToInt32(item.cntc_icod_indicador_espacios),
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_flag_verificacion = Convert.ToBoolean(item.cntc_flag_verificacion),
                            cntc_indicador_pre_contrato = Convert.ToInt32(item.cntc_indicador_pre_contrato),
                            strIndicador = item.cntc_indicador_pre_contrato.ToString(),
                            cntc_itipo_pago = Convert.ToInt32(item.cntc_itipo_pago),
                            strTipoPago = item.strTipoPago,
                            cntc_vdireccion_fallecido = item.cntc_vdireccion_fallecido,
                            cntc_itipo_doc_prestamo = Convert.ToInt32(item.cntc_itipo_doc_prestamo),
                            func_icod_funeraria_prestamo = Convert.ToInt32(item.func_icod_funeraria_prestamo),
                            strFunerariaPrestamo = item.strFunerariaPrestamo,
                            strNombreContratante = item.strNombreContratante,
                            cntc_sfecha_crea = item.cntc_sfecha_crea,
                            cntc_nmonto_foma = Convert.ToDecimal(item.cntc_nmonto_foma),
                            flag_indicador = Convert.ToBoolean(item.flag_indicador),
                            strDetalle = item.strDetalle,
                            flag_tit = Convert.ToBoolean(item.flag_tit),
                            cntc_npago_covid19 = Convert.ToDecimal(item.cntc_npago_covid19),
                            cntc_icod_deceso = Convert.ToInt32(item.cntc_icod_deceso),
                            cntc_icod_foma_mante = Convert.ToInt32(item.cntc_icod_foma_mante),
                            monto_contado = (Math.Round(Convert.ToDecimal(item.monto_contado), 2)).ToString(),
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_paterno_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_materno_contratante.TrimEnd().TrimStart()),
                            cntc_nfinanciamientro = Convert.ToDecimal(item.cntc_nfinanciamientro),
                            cntc_nmonto_total_foma = item.cntc_nmonto_total_foma,
                            cntc_nmonto_total_foma_pagado = item.cntc_nmonto_total_foma_pagado,
                            cntc_vnumero_contrato_corr = item.cntc_vnumero_contrato_corr,
                            cntc_iindicador_contr_sol = item.cntc_iindicador_contr_sol,
                            cntc_vnumero_solicitud = item.cntc_vnumero_solicitud,
                            //cntc_iestado_sol = item.cntc_iestado_sol,
                            cntc_vobservaciones_sol = item.cntc_vobservaciones_sol,
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante
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

        public EContratoWeb ContratoWebById(int cntc_icod_contrato)
        {
            EContratoWeb obe = new EContratoWeb();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_CONTRATO_GET_BY_ID_WEB(cntc_icod_contrato);
                    foreach (var item in collection)
                    {
                        obe.cntc_icod_contrato = item.cntc_icod_contrato;
                        obe.cntc_vnumero_contrato = item.cntc_vnumero_contrato;
                        obe.cntc_sfecha_contrato = item.cntc_sfecha_contrato;
                        obe.cntc_icod_vendedor = item.cntc_icod_vendedor;
                        obe.cntc_iorigen_venta = item.cntc_iorigen_venta;
                        obe.cntc_icod_funeraria = item.cntc_icod_funeraria;
                        obe.cntc_vnombre_comercial = item.cntc_vnombre_comercial;
                        obe.cntc_vunidad_venta = item.cntc_vunidad_venta;
                        obe.cntc_vnombre_contratante = item.cntc_vnombre_contratante;
                        obe.cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante;
                        obe.cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante;
                        obe.cntc_vruc_contratante = item.cntc_vruc_contratante;
                        obe.cntc_sfecha_nacimineto_contratante = item.cntc_sfecha_nacimineto_contratante;
                        obe.cntc_vtelefono_contratante = item.cntc_vtelefono_contratante;
                        obe.cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante;
                        obe.cntc_vdireccion_contratante = item.cntc_vdireccion_contratante;
                        obe.cntc_inacionalidad_contratante = item.cntc_inacionalidad_contratante;
                        obe.cntc_itipo_documento_contratante = item.cntc_itipo_documento_contratante;
                        obe.cntc_vdocumento_contratante = item.cntc_vdocumento_contratante;
                        obe.cntc_iparentesco_contratante = item.cntc_iparentesco_contratante;
                        obe.cntc_iestado_civil_contratante = item.cntc_iestado_civil_contratante;
                        obe.cntc_vparentesco_contratante = item.cntc_vparentesco_contratante;
                        obe.cntc_vnombre_contratante2 = item.cntc_vnombre_contratante2;
                        obe.cntc_vapellido_paterno_contratante2 = item.cntc_vapellido_paterno_contratante2;
                        obe.cntc_vapellido_materno_contratante2 = item.cntc_vapellido_materno_contratante2;
                        obe.cntc_vruc_contratante2 = item.cntc_vruc_contratante2;
                        obe.cntc_sfecha_nacimineto_contratante2 = item.cntc_sfecha_nacimineto_contratante2;
                        obe.cntc_vtelefono_contratante2 = item.cntc_vtelefono_contratante2;
                        obe.cntc_vdireccion_correo_contratante2 = item.cntc_vdireccion_correo_contratante2;
                        obe.cntc_vdireccion_contratante2 = item.cntc_vdireccion_contratante2;
                        obe.cntc_inacionalidad_contratante2 = item.cntc_inacionalidad_contratante2;
                        obe.cntc_itipo_documento_contratante2 = item.cntc_itipo_documento_contratante2;
                        obe.cntc_vdocumento_contratante2 = item.cntc_vdocumento_contratante2;
                        obe.cntc_iparentesco_contratante2 = item.cntc_iparentesco_contratante2;
                        obe.cntc_iestado_civil_contratante2 = item.cntc_iestado_civil_contratante2;
                        obe.cntc_vparentesco_contratante2 = item.cntc_vparentesco_contratante2;
                        obe.cntc_vnombre_representante = item.cntc_vnombre_representante;
                        obe.cntc_vapellido_paterno_representante = item.cntc_vapellido_paterno_representante;
                        obe.cntc_vapellido_materno_representante = item.cntc_vapellido_materno_representante;
                        obe.cntc_vdni_representante = item.cntc_vdni_representante;
                        obe.cntc_ruc_representante = item.cntc_ruc_representante;
                        obe.cntc_sfecha_nacimiento_representante = item.cntc_sfecha_nacimiento_representante;
                        obe.cntc_iestado_civil_representante = item.cntc_iestado_civil_representante;
                        obe.cntc_vestado_civil_representante = item.cntc_vestado_civil_representante;
                        obe.cntc_inacionalidad_respresentante = item.cntc_inacionalidad_respresentante;
                        obe.cntc_vnacionalidad_respresentante = item.cntc_vnacionalidad_respresentante;
                        obe.cntc_vtelefono_representante = item.cntc_vtelefono_representante;
                        obe.cntc_vdireccion_representante = item.cntc_vdireccion_representante;
                        obe.cntc_vnumero_direccion_representante = item.cntc_vnumero_direccion_representante;
                        obe.cntc_vdepartamento_representante = item.cntc_vdepartamento_representante;
                        obe.cntc_idistrito_representante = item.cntc_idistrito_representante;
                        obe.cntc_vdistrito_representante = item.cntc_vdistrito_representante;
                        obe.cntc_vcodigo_postal_representante = item.cntc_vcodigo_postal_representante;
                        obe.cntc_itipo_documento_representante = item.cntc_itipo_documento_representante;
                        obe.cntc_vdocumento_respresentante = item.cntc_vdocumento_respresentante;
                        obe.cntc_vnombre_beneficiario = item.cntc_vnombre_beneficiario;
                        obe.cntc_vapellido_paterno_beneficiario = item.cntc_vapellido_paterno_beneficiario;
                        obe.cntc_vapellido_materno_beneficiario = item.cntc_vapellido_materno_beneficiario;
                        obe.cntc_vdni_beneficiario = item.cntc_vdni_beneficiario;
                        obe.cntc_sfecha_nacimiento_beneficiario = item.cntc_sfecha_nacimiento_beneficiario;
                        obe.cntc_vdireccion_beneficiario = item.cntc_vdireccion_beneficiario;
                        obe.cntc_vnacionalidad = item.cntc_vnacionalidad;
                        obe.cntc_icodigo_plan = item.cntc_icodigo_plan;
                        obe.cntc_icod_nombre_plan = item.cntc_icod_nombre_plan;
                        obe.cntc_vnombre_plan = item.cntc_vnombre_plan;
                        obe.cntc_nprecio_lista = item.cntc_nprecio_lista;
                        obe.cntc_ndescuento = item.cntc_ndescuento;
                        obe.cntc_ninhumacion = item.cntc_ninhumacion;
                        obe.cntc_naporte_fondo = item.cntc_naporte_fondo;
                        obe.cntc_nIGV = item.cntc_nIGV;
                        obe.cntc_nprecio_total = item.cntc_nprecio_total;
                        obe.cntc_itipo_sepultura = item.cntc_itipo_sepultura;
                        obe.cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato;
                        obe.cntc_vcapacidad_total = item.cntc_vcapacidad_total;
                        obe.cntc_vurnas = item.cntc_vurnas;
                        obe.cntc_vservico_inhumacion = item.cntc_vservico_inhumacion;
                        obe.cntc_icod_plataforma = item.cntc_icod_plataforma;
                        obe.cntc_icod_manzana = item.cntc_icod_manzana;
                        obe.cntc_icod_isepultura = item.cntc_icod_isepultura;
                        obe.cntc_vnumero_sepultura = item.cntc_vnumero_sepultura;
                        obe.espac_iid_iespacios = item.espac_iid_iespacios;
                        obe.cntc_bnivel1 = item.cntc_bnivel1;
                        obe.cntc_bnivel2 = item.cntc_bnivel2;
                        obe.cntc_bnivel3 = item.cntc_bnivel3;
                        obe.cntc_bnivel4 = item.cntc_bnivel4;
                        obe.cntc_bnivel5 = item.cntc_bnivel5;
                        obe.cntc_bnivel6 = item.cntc_bnivel6;
                        obe.cntc_icod_nivel = item.cntc_icod_nivel;
                        obe.espad_iid_iespacios1 = item.espad_iid_iespacios1;
                        obe.espad_iid_iespacios2 = item.espad_iid_iespacios2;
                        obe.espad_iid_iespacios3 = item.espad_iid_iespacios3;
                        obe.espad_iid_iespacios4 = item.espad_iid_iespacios4;
                        obe.espad_iid_iespacios5 = item.espad_iid_iespacios5;
                        obe.espad_iid_iespacios6 = item.espad_iid_iespacios6;
                        obe.cntc_vcodigo_sepultura = item.cntc_vcodigo_sepultura;
                        obe.cntc_vnumero_reserva = item.cntc_vnumero_reserva;
                        obe.cntc_iusuario_crea = item.cntc_iusuario_crea;
                        obe.cntc_sfecha_crea = item.cntc_sfecha_crea;
                        obe.cntc_vpc_crea = item.cntc_vpc_crea;
                        obe.cntc_iusuario_modifica = item.cntc_iusuario_modifica;
                        obe.cntc_sfecha_modifica = item.cntc_sfecha_modifica;
                        obe.cntc_vpc_modifica = item.cntc_vpc_modifica;
                        obe.cntc_iusuario_elimina = item.cntc_iusuario_elimina;
                        obe.cntc_sfecha_elimina = item.cntc_sfecha_elimina;
                        obe.cntc_vpc_elimina = item.cntc_vpc_elimina;
                        obe.cntc_flag_estado = item.cntc_flag_estado;
                        obe.cntc_icod_situacion = item.cntc_icod_situacion;
                        obe.cntc_ncuota_inicial = item.cntc_ncuota_inicial;
                        obe.cntc_inro_cuotas = item.cntc_inro_cuotas;
                        obe.cntc_nmonto_cuota = item.cntc_nmonto_cuota;
                        obe.cntc_sfecha_cuota = item.cntc_sfecha_cuota;
                        obe.cntc_vdescripcion_anulacion = item.cntc_vdescripcion_anulacion;
                        obe.cntc_icod_indicador_espacios = item.cntc_icod_indicador_espacios;
                        obe.cntc_vobservaciones = item.cntc_vobservaciones;
                        obe.cntc_flag_verificacion = item.cntc_flag_verificacion;
                        obe.cntc_indicador_pre_contrato = item.cntc_indicador_pre_contrato;
                        obe.cntc_itipo_pago = item.cntc_itipo_pago;
                        obe.cntc_itipo_doc_prestamo = item.cntc_itipo_doc_prestamo;
                        obe.func_icod_funeraria_prestamo = item.func_icod_funeraria_prestamo;
                        obe.cntc_nmonto_foma = item.cntc_nmonto_foma;
                        obe.flag_indicador = item.flag_indicador;
                        obe.cntc_npago_covid19 = item.cntc_npago_covid19;
                        obe.cntc_icod_deceso = item.cntc_icod_deceso;
                        obe.cntc_icod_foma_mante = item.cntc_icod_foma_mante;
                        obe.cntc_nfinanciamientro = item.cntc_nfinanciamientro;
                        obe.cntc_nmonto_total_foma = item.cntc_nmonto_total_foma;
                        obe.cntc_nmonto_total_foma_pagado = item.cntc_nmonto_total_foma_pagado;
                        obe.cntc_vnumero_contrato_corr = item.cntc_vnumero_contrato_corr;
                        obe.cntc_iindicador_contr_sol = item.cntc_iindicador_contr_sol;
                        obe.cntc_vnumero_solicitud = item.cntc_vnumero_solicitud;
                        obe.cntc_iestado_sol = item.cntc_iestado_sol;
                        obe.cntc_vobservaciones_sol = item.cntc_vobservaciones_sol;
                        obe.cntc_sfecha_inicio_pago = item.cntc_sfecha_inicio_pago;
                        obe.cntc_sfecha_fin_pago = item.cntc_sfecha_fin_pago;
                        obe.cntc_vnombre_fallecido = item.cntc_vnombre_fallecido;
                        obe.cntc_vapellido_paterno_fallecido = item.cntc_vapellido_paterno_fallecido;
                        obe.cntc_vapellido_materno_fallecido = item.cntc_vapellido_materno_fallecido;
                        obe.cntc_sfecha_nac_fallecido = item.cntc_sfecha_nac_fallecido;
                        obe.cntc_sfecha_fallecimiento = item.cntc_sfecha_fallecimiento;
                        obe.cntc_sfecha_entierro = item.cntc_sfecha_entierro;
                        obe.cntc_itipo_documento_fallecido = item.cntc_itipo_documento_fallecido;
                        obe.cntc_vdocumento_fallecido = item.cntc_vdocumento_fallecido;
                        obe.cntc_inacionalidad = item.cntc_inacionalidad;
                        obe.cntc_vdireccion_fallecido = item.cntc_vdireccion_fallecido;
                        obe.cntc_icod_religiones = item.cntc_icod_religiones;
                        obe.cntc_icod_tipo_deceso = item.cntc_icod_tipo_deceso;
                        obe.cntc_vobservacion = item.cntc_vobservacion;
                        obe.cntc_icod_contrato_fallecido = item.cntc_icod_contrato_fallecido;
                        obe.cntc_vorigen_registro = item.cntc_vorigen_registro;
                        obe.Funeraria = item.Funeraria;
                        obe.cntc_nsaldo = item.cntc_nsaldo;
                        obe.cntc_idocumento_financiado = item.cntc_idocumento_financiado;
                        obe.cntc_icod_tipo_contrato = item.cntc_icod_tipo_contrato;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obe;
        }

        public void Contratante1WebGuardar(EContratoWeb obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.USP_CONTRATANTES_INSERTAR(
                        obe.cntc_icod_contrato,
                        1,
                        obe.cntc_vnombre_contratante,
                        obe.cntc_vapellido_paterno_contratante,
                        obe.cntc_vapellido_materno_contratante,
                        obe.cntc_vdocumento_contratante,
                        obe.cntc_vruc_contratante,
                        obe.cntc_sfecha_nacimineto_contratante,
                        obe.cntc_vtelefono_contratante,
                        obe.cntc_vdireccion_correo_contratante,
                        obe.cntc_vdireccion_contratante,
                        obe.cntc_inacionalidad_contratante,
                        obe.cntc_itipo_documento_contratante,
                        obe.cntc_iusuario_crea,
                        obe.cntc_vpc_crea,
                        obe.cntc_iparentesco_contratante,
                        obe.cntc_vparentesco_contratante,
                        obe.cntc_iestado_civil_contratante
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Contratante2WebGuardar(EContratoWeb obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.USP_CONTRATANTES_INSERTAR(
                        obe.cntc_icod_contrato,
                        2,
                        obe.cntc_vnombre_contratante2,
                        obe.cntc_vapellido_paterno_contratante2,
                        obe.cntc_vapellido_materno_contratante2,
                        obe.cntc_vdocumento_contratante2,
                        obe.cntc_vruc_contratante2,
                        obe.cntc_sfecha_nacimineto_contratante2,
                        obe.cntc_vtelefono_contratante2,
                        obe.cntc_vdireccion_correo_contratante2,
                        obe.cntc_vdireccion_contratante2,
                        obe.cntc_inacionalidad_contratante2,
                        obe.cntc_itipo_documento_contratante2,
                        obe.cntc_iusuario_crea,
                        obe.cntc_vpc_crea,
                        obe.cntc_iparentesco_contratante2,
                        obe.cntc_vparentesco_contratante2,
                        obe.cntc_iestado_civil_contratante2
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void FallecidoWebGuardar(EContratoWeb obe)
        {
            int? icod = obe.cntc_icod_contrato_fallecido;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.USP_CONTRATOS_FALLECIDO_GUARDAR(
                        ref icod,
                        obe.cntc_icod_contrato,
                        obe.cntc_vnombre_fallecido,
                        obe.cntc_vapellido_paterno_fallecido,
                        obe.cntc_vapellido_materno_fallecido,
                        obe.cntc_sfecha_nac_fallecido,
                        obe.cntc_sfecha_fallecimiento,
                        obe.cntc_sfecha_entierro,
                        obe.cntc_itipo_documento_fallecido,
                        obe.cntc_vdocumento_fallecido,
                        obe.cntc_inacionalidad,
                        obe.cntc_vdireccion_fallecido,
                        obe.cntc_icod_religiones,
                        obe.cntc_icod_tipo_deceso,
                        obe.cntc_vobservacion,
                        obe.cntc_sfecha_crea,
                        obe.cntc_iusuario_crea,
                        obe.cntc_vpc_crea
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int ContratoWebGuardar(EContratoWeb obe)
        {
            int? icod = obe.cntc_icod_contrato;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATO_WEB_GUARDAR(
                         ref icod,
                         obe.cntc_vnumero_contrato,
                         obe.cntc_sfecha_contrato,
                         obe.cntc_icod_situacion,
                         obe.cntc_icod_vendedor,
                         obe.cntc_iorigen_venta,
                         obe.cntc_icod_funeraria,
                         obe.cntc_itipo_doc_prestamo,
                         obe.cntc_itipo_documento_contratante,
                         obe.cntc_vdocumento_contratante,
                         obe.cntc_vapellido_paterno_contratante,
                         obe.cntc_vruc_contratante,
                         obe.cntc_vapellido_materno_contratante,
                         obe.cntc_sfecha_nacimineto_contratante,
                         obe.cntc_vtelefono_contratante,
                         obe.cntc_vdireccion_correo_contratante,
                         obe.cntc_vdireccion_contratante,
                         obe.cntc_vnombre_contratante,
                         obe.cntc_inacionalidad_contratante,
                         obe.cntc_icodigo_plan,
                         obe.cntc_icod_nombre_plan,
                         obe.cntc_itipo_sepultura,
                         obe.cntc_nprecio_lista,
                         obe.cntc_ndescuento,
                         obe.cntc_ninhumacion,
                         obe.cntc_icod_deceso,
                         obe.cntc_npago_covid19,
                         obe.cntc_naporte_fondo,
                         obe.cntc_nIGV,
                         obe.cntc_nfinanciamientro,
                         obe.cntc_itipo_pago,
                         obe.cntc_ncuota_inicial,
                         obe.cntc_inro_cuotas,
                         obe.cntc_nmonto_cuota,
                         obe.cntc_sfecha_cuota,
                         obe.cntc_sfecha_inicio_pago,
                         obe.cntc_sfecha_fin_pago,
                         obe.cntc_nprecio_total,
                         obe.cntc_iusuario_crea,
                         obe.cntc_sfecha_crea,
                         obe.cntc_vpc_crea,
                         obe.cntc_iusuario_modifica,
                         obe.cntc_sfecha_modifica,
                         obe.cntc_vpc_modifica,
                         obe.cntc_iparentesco_contratante,
                         obe.cntc_iestado_civil_contratante,
                         obe.cntc_vparentesco_contratante,
                         obe.cntc_vorigen_registro,
                         obe.cntc_nsaldo,
                         obe.cntc_vcapacidad_contrato,
                         obe.cntc_vcapacidad_total,
                         obe.cntc_vurnas,
                         obe.cntc_vservico_inhumacion,
                         obe.cntc_icod_plataforma,
                         obe.cntc_idocumento_financiado,
                         obe.cntc_icod_tipo_contrato

                      );

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return icod.Value;
        }

        public ContratoImpresion ContratoImpresion(int cntc_icod_contrato)
        {
            ContratoImpresion obj = new ContratoImpresion();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.USP_CONTRATO_IMPRESION_POR_ID(cntc_icod_contrato);
                    foreach (var item in collection)
                    {
                        obj = new ContratoImpresion()
                        {
                            NumeroContrato = item.NumeroContrato,
                            strNombreIEC = item.strNombreIEC,
                            OrigenVenta = item.OrigenVenta,
                            NombreContratante = item.NombreContratante,
                            ApellidoPaternoContratante = item.ApellidoPaternoContratante,
                            ApellidoMaternoContratante = item.ApellidoMaternoContratante,
                            NumeroDocumentoContratante = item.NumeroDocumentoContratante,
                            RucContratante = item.RucContratante,
                            FechaNacimientoContratante = item.FechaNacimientoContratante,
                            EstadoCivilContratante = item.EstadoCivilContratante,
                            ParentescoContratatante = item.ParentescoContratatante,
                            NacionalidadContratante = item.NacionalidadContratante,
                            TelefonoContratante = item.TelefonoContratante,
                            DireccionContratante = item.DireccionContratante,
                            NombreContratante2 = item.NombreContratante2,
                            ApellidoPaternoContratante2 = item.ApellidoPaternoContratante2,
                            ApellidoMaternoContratante2 = item.ApellidoMaternoContratante2,
                            NumeroDocumentoContratante2 = item.NumeroDocumentoContratante2,
                            RucContratante2 = item.RucContratante2,
                            FechaNacimientoContratante2 = item.FechaNacimientoContratante2,
                            EstadoCivilContratante2 = item.EstadoCivilContratante2,
                            ParentescoContratatante2 = item.ParentescoContratatante2,
                            NacionalidadContratante2 = item.NacionalidadContratante2,
                            TelefonoContratante2 = item.TelefonoContratante2,
                            DireccionContratante2 = item.DireccionContratante2,
                            NombreBeneficiario = item.NombreBeneficiario,
                            ApellidoPaternoBeneficiario = item.ApellidoPaternoBeneficiario,
                            ApellidoMaternoBeneficiario = item.ApellidoMaternoBeneficiario,
                            DocumentoBeneficiario = item.DocumentoBeneficiario,
                            FechaNacimientoBeneficiario = item.FechaNacimientoBeneficiario,
                            DireccionBeneficiario = item.DireccionBeneficiario,
                            NombreFallecido = item.NombreFallecido,
                            ApellidoPaternoFallecido = item.ApellidoPaternoFallecido,
                            ApellidoMaternoFallecido = item.ApellidoMaternoFallecido,
                            DocumentoFallecido = item.DocumentoFallecido,
                            FechaNacimientoFallecido = item.FechaNacimientoFallecido,
                            FechaFallecimientoFallecido = item.FechaFallecimientoFallecido,
                            FechaEntierroFallecido = item.FechaEntierroFallecido,
                            NacionalidadFallecido = item.NacionalidadFallecido,
                            CodigoPlan = item.CodigoPlan,
                            NombrePlan = item.NombrePlan,
                            TipoSepultura = item.TipoSepultura,
                            CapacidadContratada = item.CapacidadContratada,
                            CapacidadTotal = item.CapacidadTotal,
                            Urnas = item.Urnas,
                            ServicioInhumacion = item.ServicioInhumacion,
                            Plataforma = item.Plataforma,
                            Manzana = item.Manzana,
                            NumeroSepultura = item.NumeroSepultura,
                            Nivel = item.Nivel,
                            CodigoSepultura = item.CodigoSepultura,
                            NumeroReserva = item.NumeroReserva,
                            PrecioLista = item.PrecioLista,
                            Descuento = item.Descuento,
                            Inhumacion = item.Inhumacion,
                            Foma = item.Foma,
                            PrecioTotal = item.PrecioTotal,
                            PagoInicial = item.PagoInicial,
                            NumeroCuotas = item.NumeroCuotas,
                            MontoCuota = item.MontoCuota,
                            FechaCuota = item.FechaCuota,
                            UltimoVencimiento = item.UltimoVencimiento,
                            FechaContrato = item.FechaContrato,
                            Comprobante = item.Comprobante,
                            SistemaPago = item.SistemaPago,
                            DocumentoFinanciado = item.DocumentoFinanciado,
                            Saldo = item.Saldo,
                            CorreoContratante = item.CorreoContrantante,
                            CorreoContratante2 = item.CorreoContrantante2,
                            TipoContrato = item.TipoContrato,
                            TipoDeceso = item.TipoDeceso
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<EContrato> ContratosListarPorNumeroContratanteDni(string numero, string contratante, string dni)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 99999999;
                    var query = dc.SGEV_CONTRATOS_LISTAR_NUMERO_CONTRATANTE_DNI(numero, contratante, dni);
                    foreach (var item in query)
                    {
                        lista.Add(new EContrato()
                        {

                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_sfecha_contrato = Convert.ToDateTime(item.cntc_sfecha_contrato),
                            cntc_icod_vendedor = Convert.ToInt32(item.cntc_icod_vendedor),
                            cntc_iorigen_venta = Convert.ToInt32(item.cntc_iorigen_venta),
                            cntc_icod_funeraria = Convert.ToInt32(item.cntc_icod_funeraria),
                            cntc_vnombre_comercial = item.cntc_vnombre_comercial,
                            cntc_vunidad_venta = item.cntc_vunidad_venta,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante == null ? "" : item.cntc_vdni_contratante,
                            cntc_vruc_contratante = item.cntc_vruc_contratante,
                            cntc_sfecha_nacimineto_contratante = Convert.ToDateTime(item.cntc_sfecha_nacimineto_contratante),
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante,
                            cntc_vdireccion_contratante = item.cntc_vdireccion_contratante,
                            cntc_inacionalidad_contratante = Convert.ToInt32(item.cntc_inacionalidad_contratante),
                            cntc_itipo_documento_contratante = Convert.ToInt32(item.cntc_itipo_documento_contratante),
                            cntc_vnombre_representante = item.cntc_vnombre_representante,
                            cntc_vapellido_paterno_representante = item.cntc_vapellido_paterno_representante,
                            cntc_vapellido_materno_representante = item.cntc_vapellido_materno_representante,
                            cntc_vdni_representante = item.cntc_vdni_representante,
                            cntc_ruc_representante = item.cntc_ruc_representante,
                            cntc_sfecha_nacimiento_representante = Convert.ToDateTime(item.cntc_sfecha_nacimiento_representante),
                            cntc_iestado_civil_representante = Convert.ToInt32(item.cntc_iestado_civil_representante),
                            cntc_inacionalidad_respresentante = Convert.ToInt32(item.cntc_inacionalidad_respresentante),
                            cntc_vtelefono_representante = item.cntc_vtelefono_representante,
                            cntc_vdireccion_representante = item.cntc_vdireccion_representante,
                            cntc_vnumero_direccion_representante = item.cntc_vnumero_direccion_representante,
                            cntc_vdepartamento_representante = item.cntc_vdepartamento_representante,
                            cntc_idistrito_representante = Convert.ToInt32(item.cntc_idistrito_representante),
                            cntc_vcodigo_postal_representante = item.cntc_vcodigo_postal_representante,
                            cntc_itipo_documento_representante = Convert.ToInt32(item.cntc_itipo_documento_representante),
                            cntc_vdocumento_respresentante = item.cntc_vdocumento_respresentante,
                            cntc_vnombre_beneficiario = item.cntc_vnombre_beneficiario,
                            cntc_vapellido_paterno_beneficiario = item.cntc_vapellido_paterno_beneficiario,
                            cntc_vapellido_materno_beneficiario = item.cntc_vapellido_materno_beneficiario,
                            cntc_vdni_beneficiario = item.cntc_vdni_beneficiario,
                            cntc_sfecha_nacimiento_beneficiario = Convert.ToDateTime(item.cntc_sfecha_nacimiento_beneficiario),
                            cntc_vdireccion_beneficiario = item.cntc_vdireccion_beneficiario,
                            cntc_inacionalidad = Convert.ToInt32(item.cntc_inacionalidad),
                            cntc_icodigo_plan = Convert.ToInt32(item.cntc_icodigo_plan),
                            cntc_icod_nombre_plan = Convert.ToInt32(item.cntc_icod_nombre_plan),
                            cntc_vnombre_plan = item.cntc_vnombre_plan,
                            cntc_nprecio_lista = Convert.ToDecimal(item.cntc_nprecio_lista),
                            cntc_ndescuento = Convert.ToDecimal(item.cntc_ndescuento),
                            cntc_ninhumacion = Convert.ToDecimal(item.cntc_ninhumacion),
                            cntc_naporte_fondo = Convert.ToDecimal(item.cntc_naporte_fondo),
                            cntc_nIGV = Convert.ToDecimal(item.cntc_nIGV),
                            cntc_nprecio_total = Convert.ToDecimal(item.cntc_nprecio_total),
                            cntc_itipo_sepultura = Convert.ToInt32(item.cntc_itipo_sepultura),
                            cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato,
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total,
                            cntc_vurnas = item.cntc_vurnas,
                            cntc_vservico_inhumacion = item.cntc_vservico_inhumacion,
                            cntc_icod_plataforma = Convert.ToInt32(item.cntc_icod_plataforma),
                            cntc_icod_manzana = Convert.ToInt32(item.cntc_icod_manzana),
                            cntc_icod_isepultura = Convert.ToInt32(item.cntc_icod_isepultura),
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espac_icod_vespacios = item.espac_icod_vespacios,
                            cntc_bnivel1 = item.cntc_bnivel1,
                            cntc_bnivel2 = item.cntc_bnivel2,
                            cntc_bnivel3 = item.cntc_bnivel3,
                            cntc_bnivel4 = item.cntc_bnivel4,
                            cntc_bnivel5 = item.cntc_bnivel5,
                            cntc_bnivel6 = item.cntc_bnivel6,
                            espad_iid_iespacios1 = Convert.ToInt32(item.espad_iid_iespacios1),
                            espad_iid_iespacios2 = Convert.ToInt32(item.espad_iid_iespacios2),
                            espad_iid_iespacios3 = Convert.ToInt32(item.espad_iid_iespacios3),
                            espad_iid_iespacios4 = Convert.ToInt32(item.espad_iid_iespacios4),
                            espad_iid_iespacios5 = Convert.ToInt32(item.espad_iid_iespacios5),
                            espad_iid_iespacios6 = Convert.ToInt32(item.espad_iid_iespacios6),
                            cntc_vcodigo_sepultura = item.cntc_vcodigo_sepultura,
                            cntc_vnumero_reserva = item.cntc_vnumero_reserva,
                            strcodigoplan = item.strcodigoplan,
                            strNombreplan = item.strNombreplan,
                            strorigenventa = item.strorigenventa,
                            strtiposepultura = item.strtiposepultura,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strNombreIEC = item.strNombreIEC,
                            cntc_flag_estado = Convert.ToBoolean(item.cntc_flag_estado),
                            strsepultura = item.strsepultura,
                            strnacionalidad = item.strnacionalidad,
                            strnacionalidadfallec = item.strnacionalidadfallec,
                            strestadocivil = item.strestadocivil,
                            strdistrito = item.strdistrito,
                            cntc_icod_situacion = Convert.ToInt32(item.cntc_icod_situacion),
                            strSituacion = item.strSituacion,
                            cntc_ncuota_inicial = Convert.ToDecimal(item.cntc_ncuota_inicial),
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            cntc_nmonto_cuota = Convert.ToDecimal(item.cntc_nmonto_cuota),
                            cntc_sfecha_cuota = item.cntc_sfecha_cuota,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_vdescripcion_anulacion = item.cntc_vdescripcion_anulacion,
                            cntc_icod_indicador_espacios = Convert.ToInt32(item.cntc_icod_indicador_espacios),
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_flag_verificacion = Convert.ToBoolean(item.cntc_flag_verificacion),
                            cntc_indicador_pre_contrato = Convert.ToInt32(item.cntc_indicador_pre_contrato),
                            strIndicador = item.cntc_indicador_pre_contrato.ToString(),
                            cntc_itipo_pago = Convert.ToInt32(item.cntc_itipo_pago),
                            strTipoPago = item.strTipoPago,
                            cntc_vdireccion_fallecido = item.cntc_vdireccion_fallecido,
                            cntc_itipo_doc_prestamo = Convert.ToInt32(item.cntc_itipo_doc_prestamo),
                            func_icod_funeraria_prestamo = Convert.ToInt32(item.func_icod_funeraria_prestamo),
                            strFunerariaPrestamo = item.strFunerariaPrestamo,
                            strNombreContratante = item.strNombreContratante,
                            cntc_sfecha_crea = item.cntc_sfecha_crea,
                            cntc_nmonto_foma = Convert.ToDecimal(item.cntc_nmonto_foma),
                            flag_indicador = Convert.ToBoolean(item.flag_indicador),
                            strDetalle = item.strDetalle,
                            flag_tit = Convert.ToBoolean(item.flag_tit),
                            cntc_npago_covid19 = Convert.ToDecimal(item.cntc_npago_covid19),
                            cntc_icod_deceso = Convert.ToInt32(item.cntc_icod_deceso),
                            cntc_icod_foma_mante = Convert.ToInt32(item.cntc_icod_foma_mante),
                            monto_contado = (Math.Round(Convert.ToDecimal(item.monto_contado), 2)).ToString(),
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_paterno_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_materno_contratante.TrimEnd().TrimStart()),
                            cntc_nfinanciamientro = Convert.ToDecimal(item.cntc_nfinanciamientro),
                            cntc_nmonto_total_foma = item.cntc_nmonto_total_foma,
                            cntc_nmonto_total_foma_pagado = item.cntc_nmonto_total_foma_pagado,
                            cntc_vnumero_contrato_corr = item.cntc_vnumero_contrato_corr,
                            cntc_iindicador_contr_sol = item.cntc_iindicador_contr_sol,
                            cntc_vnumero_solicitud = item.cntc_vnumero_solicitud,
                            cntc_iestado_sol = item.cntc_iestado_sol,
                            cntc_vobservaciones_sol = item.cntc_vobservaciones_sol,
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante
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

        public List<EContrato> ContratoListarPorFechas(DateTime fechaincio, DateTime fechafinal, int intContrato)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_CONTRATOS_LISTAR_POR_FECHAS(intContrato, fechaincio, fechafinal);
                    foreach (var item in collection)
                    {
                        lista.Add(new EContrato()
                        {
                            flag_tit = Convert.ToBoolean(item.flag_tit),
                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_icod_vendedor = item.cntc_icod_vendedor,
                            cntc_iorigen_venta = item.cntc_iorigen_venta,
                            cntc_itipo_sepultura = item.cntc_itipo_sepultura,
                            cntc_icod_plataforma = item.cntc_icod_plataforma,
                            cntc_icod_manzana = item.cntc_icod_manzana,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_icod_isepultura = item.cntc_icod_isepultura,
                            cntc_icod_situacion = item.cntc_icod_situacion,
                            cntc_vdocumento_contratante = string.IsNullOrEmpty(item.cntc_vdocumento_contratante) ? "" : item.cntc_vdocumento_contratante,
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_sfecha_contrato = item.cntc_sfecha_contrato,
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total,
                            cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato,
                            cntc_icod_nombre_plan = item.cntc_icod_nombre_plan,
                            cntc_icodigo_plan = item.cntc_icodigo_plan,
                            cntc_nprecio_lista = item.cntc_nprecio_lista,
                            cntc_naporte_fondo = item.cntc_naporte_fondo,
                            cntc_nprecio_total = item.cntc_nprecio_total,
                            cntc_ncuota_inicial = item.cntc_ncuota_inicial,
                            cntc_inro_cuotas = item.cntc_inro_cuotas,
                            cntc_nmonto_cuota = item.cntc_nmonto_cuota,
                            cntc_sfecha_cuota = item.cntc_sfecha_cuota,
                            cntc_itipo_pago = item.cntc_itipo_pago,
                            cntc_sfecha_crea = item.cntc_sfecha_crea,
                            cntc_vnombre_comercial = item.cntc_vnombre_comercial,
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_paterno_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_materno_contratante.TrimEnd().TrimStart()),
                            cntc_vorigen_registro = item.cntc_vorigen_registro,
                            cntc_vnumero_reserva = item.cntc_vnumero_reserva
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        public List<EContrato> ContratoFallecidoListarPorFechas(DateTime fechaincio, DateTime fechafinal, int intContrato)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_CONSULTA_CONTRATOS_FALLECIOS_LISTAR_POR_FECHAS(intContrato, fechaincio, fechafinal);
                    foreach (var item in collection)
                    {
                        lista.Add(new EContrato()
                        {
                            
                             
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_icod_vendedor = item.cntc_icod_vendedor,
                            cntc_iorigen_venta = item.cntc_iorigen_venta,
                            cntc_itipo_sepultura = item.cntc_itipo_sepultura,
                            cntc_icod_plataforma = item.cntc_icod_plataforma,
                            cntc_icod_manzana = item.cntc_icod_manzana,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_icod_isepultura = item.cntc_icod_isepultura,
                            cntc_icod_situacion = item.cntc_icod_situacion,
                            cntc_vdocumento_contratante = string.IsNullOrEmpty(item.cntc_vdocumento_contratante) ? "" : item.cntc_vdocumento_contratante,
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_sfecha_contrato = item.cntc_sfecha_contrato,
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total,
                            cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato,
                            cntc_icod_nombre_plan = item.cntc_icod_nombre_plan,
                            cntc_icodigo_plan = item.cntc_icodigo_plan,
                            cntc_nprecio_lista = item.cntc_nprecio_lista,
                            cntc_naporte_fondo = item.cntc_naporte_fondo,
                            cntc_nprecio_total = item.cntc_nprecio_total,
                            cntc_ncuota_inicial = item.cntc_ncuota_inicial,
                            cntc_inro_cuotas = item.cntc_inro_cuotas,
                            cntc_nmonto_cuota = item.cntc_nmonto_cuota,
                            cntc_sfecha_cuota = item.cntc_sfecha_cuota,
                            cntc_itipo_pago = item.cntc_itipo_pago,
                            cntc_sfecha_crea = item.cntc_sfecha_crea,
                            cntc_vnombre_comercial = item.cntc_vnombre_comercial,
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante, item.cntc_vapellido_paterno_contratante, item.cntc_vapellido_materno_contratante),
                            cntc_vorigen_registro = item.cntc_vorigen_registro,
                            cntc_vnombre_fallecido = item.cntc_vnombre_fallecido,
                            cntc_vapellido_paterno_fallecido = item.cntc_vapellido_paterno_fallecido,
                            cntc_vapellido_materno_fallecido = item.cntc_vapellido_materno_fallecido,
                            cntc_sfecha_fallecimiento = item.cntc_sfecha_fallecimiento,
                            cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante,
                            
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_vnumero_reserva = item.cntc_vnumero_reserva
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
        public List<EEspaciosDet> listarEspaciosConsultasIcodContrato(int icodcontrato)
        {
            var lista = new List<EEspaciosDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ESPACIOS_DET_LISTAR_CONSULTA_ICOD_CONTRATO(icodcontrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EEspaciosDet()
                        {
                            espad_iid_iespacios = item.espad_iid_iespacios,
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espad_vnivel = item.espad_vnivel,
                            espad_icod_isituacion = Convert.ToInt32(item.espad_icod_isituacion),
                            espad_icod_iestado = Convert.ToInt32(item.espad_icod_iestado),
                            strsituacion = item.strSituacion,
                            strestado = item.strestado,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            NumContrato = item.NumContrato,
                            NomContratante = item.NomContratante,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strsepultura = item.strsepultura,
                            espad_vnom_fallecido = item.espad_vnom_fallecido,
                            espad_vapellido_paterno_fallecido = item.espad_vapellido_paterno_fallecido,
                            espad_vapellido_materno_fallecido = item.espad_vapellido_materno_fallecido,
                            espad_vdni_fallecido = item.espad_vdni_fallecido,
                            espad_sfecha_nac_fallecido = item.espad_sfecha_nac_fallecido,
                            espad_sfecha_fallecido = item.espad_sfecha_fallecido,
                            espad_sfecha_entierro = item.espad_sfecha_entierro,
                            espad_inacionalidad = Convert.ToInt32(item.espad_inacionalidad),
                            espad_thora = item.espad_thora,
                            espad_vsolicitante = item.espad_vsolicitante,
                            espad_vnro_doc = item.espad_vnro_doc,
                            espad_vnom_ape_fallecido = item.espad_vnom_fallecido + " " + item.espad_vapellido_paterno_fallecido + " " + item.espad_vapellido_materno_fallecido,
                            CodigoSepultura = item.CodigoSepultura,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            strdistrito = item.strdistrito,
                            strorigenventa = item.strorigenventa,
                            strtiposepultura = item.strtiposepultura,
                            espac_icod_imanzana = item.espac_icod_imanzana,
                            espac_isepultura = item.espac_isepultura,
                            espac_icod_iplataforma = item.espac_icod_iplataforma,
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

        public EEspacios EspaciosGetById(int espac_iid_iespacios)
        {
            var obj = new EEspacios();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ESPACIOS_GET_BY_ID(espac_iid_iespacios);
                    foreach (var item in query)
                    {


                        obj.espac_iid_iespacios = item.espac_iid_iespacios;
                        obj.espac_icod_vespacios = item.espac_icod_vespacios;
                        obj.espac_icod_iplataforma = item.espac_icod_iplataforma;
                        obj.espac_isepultura = item.espac_isepultura;
                        obj.espac_icod_imanzana = item.espac_icod_imanzana;
                        obj.espac_icod_inivel = item.espac_icod_inivel;
                        obj.espac_icod_isituacion = item.espac_icod_isituacion;
                        obj.espac_icod_iestado = item.espac_icod_iestado;
                        obj.strplataforma = item.strplataforma;
                        obj.strmanzana = item.strmanzana;
                        obj.strnivel = item.strnivel;
                        obj.strsituacion = item.strSituacion;
                        obj.strestado = item.strestado;
                        obj.strsepultura = item.strsepultura;
                        obj.codigo = string.Format("{0}-{1}-{2}-{3}", Convert.ToInt32(item.espac_icod_iplataforma), Convert.ToInt32(item.espac_icod_imanzana), Convert.ToInt32(item.espac_isepultura), Convert.ToInt32(item.espac_icod_inivel));

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public List<EEspacios> Espacios()
        {
            var lista = new List<EEspacios>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_ESPACIOS();
                    foreach (var item in collection)
                    {
                        lista.Add(new EEspacios
                        {
                            espac_iid_iespacios = item.espac_iid_iespacios,
                            espac_icod_vespacios = item.espac_icod_vespacios,
                            espac_icod_iplataforma = item.espac_icod_iplataforma,
                            espac_icod_imanzana = item.espac_icod_imanzana,
                            espac_isepultura = item.espac_isepultura,
                            espac_icod_inivel = item.espac_icod_inivel,
                            espac_icod_isituacion = item.espac_icod_isituacion

                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<ECertificadoUsoPerpetuo> CetificadoUsoPerpetuolistarConContrato()
        {
            List<ECertificadoUsoPerpetuo> lista = new List<ECertificadoUsoPerpetuo>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_CERTIFICADO_PERPETUO_LISTAR_CON_CONTRATO();
                    foreach (var item in collection)
                    {
                        lista.Add(new ECertificadoUsoPerpetuo()
                        {
                            cup_icod = item.cup_icod,
                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cup_vnumero = item.cup_vnumero,
                            cup_sfecha_emision = item.cup_sfecha_emision,
                            cup_sfecha_entrega = item.cup_sfecha_entrega,
                            cup_isituacion = item.cup_isituacion,
                            cup_bautorizado = Convert.ToBoolean(item.cup_bautorizado),
                            cntc_itipo_sepultura = item.cntc_itipo_sepultura,
                            cntc_icod_plataforma = item.cntc_icod_plataforma,
                            cntc_icod_manzana = item.cntc_icod_manzana,
                            cntc_icod_isepultura = item.cntc_icod_isepultura,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante,
                            strNombreContratante = item.strNombreContratante,
                            cntc_sfecha_contrato = item.cntc_sfecha_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato
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

        public void CetificadoUsoPerpetuoEliminar(int cup_icod)
        {
            throw new NotImplementedException();
        }

        public List<ECertificadoUsoPerpetuo> CetificadoUsoPerpetuolistar(int cntc_icod_contrato)
        {
            var lista = new List<ECertificadoUsoPerpetuo>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_CERTIFICADO_PERPETUO_LISTAR(cntc_icod_contrato);
                    foreach (var item in collection)
                    {
                        lista.Add(new ECertificadoUsoPerpetuo()
                        {
                            cup_icod = item.cup_icod,
                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cup_vnumero = item.cup_vnumero,
                            cup_sfecha_emision = item.cup_sfecha_emision,
                            cup_sfecha_entrega = item.cup_sfecha_entrega,
                            cup_isituacion = item.cup_isituacion,
                            cup_bautorizado = Convert.ToBoolean(item.cup_bautorizado)
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

        public List<EContrato> listarContratoSimple(int intContrato, int situacion)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 999999999;
                    var collection = dc.SGEV_CONTRATOS_LISTAR_SIMPLE(intContrato, situacion);
                    foreach (var item in collection)
                    {
                        lista.Add(new EContrato()
                        {
                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_paterno_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_materno_contratante.TrimEnd().TrimStart())

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

        public ECuotaFoma CuotaFomaGetById(int? ccf_icod_cuota)
        {
            ECuotaFoma obj = new ECuotaFoma();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_CUOTA_FOMA_GET_BY_ID(ccf_icod_cuota);
                    foreach (var item in collection)
                    {
                        obj = new ECuotaFoma()
                        {
                            ccf_icod_cuota = item.ccf_icod_cuota,
                            cntc_icod_contrato = item.cntc_icod_contrato,
                            ccf_icod_nivel = item.ccf_icod_nivel,
                            rc_icod_recibo = item.rc_icod_recibo,
                            ccf_nmonto_pagar = item.ccf_nmonto_pagar,
                            ccf_nmonto_pagado = item.ccf_nmonto_pagado,
                            cff_sfecha_pago = item.cff_sfecha_pago,
                            strNivel = item.strNivel
                        };
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public List<EContrato> listarContratoNuevo(int intContrato)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_CONTRATOS_LISTAR_NUEVO(intContrato);
                    foreach (var item in collection)
                    {
                        lista.Add(new EContrato()
                        {
                            flag_tit = Convert.ToBoolean(item.flag_tit),
                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_icod_vendedor = item.cntc_icod_vendedor,
                            cntc_iorigen_venta = item.cntc_iorigen_venta,
                            cntc_itipo_sepultura = item.cntc_itipo_sepultura,
                            cntc_icod_plataforma = item.cntc_icod_plataforma,
                            cntc_icod_manzana = item.cntc_icod_manzana,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_icod_isepultura = item.cntc_icod_isepultura,
                            cntc_icod_situacion = item.cntc_icod_situacion,
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante,
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_sfecha_contrato = item.cntc_sfecha_contrato,
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total,
                            cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato,
                            cntc_icod_nombre_plan = item.cntc_icod_nombre_plan,
                            cntc_icodigo_plan = item.cntc_icodigo_plan,
                            cntc_nprecio_lista = item.cntc_nprecio_lista,
                            cntc_naporte_fondo = item.cntc_naporte_fondo,
                            cntc_nprecio_total = item.cntc_nprecio_total,
                            cntc_ncuota_inicial = item.cntc_ncuota_inicial,
                            cntc_inro_cuotas = item.cntc_inro_cuotas,
                            cntc_nmonto_cuota = item.cntc_nmonto_cuota,
                            cntc_sfecha_cuota = item.cntc_sfecha_cuota,
                            cntc_itipo_pago = item.cntc_itipo_pago,
                            cntc_sfecha_crea = item.cntc_sfecha_crea,
                            cntc_vnombre_comercial = item.cntc_vnombre_comercial,
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_paterno_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_materno_contratante.TrimEnd().TrimStart()),
                            cntc_vorigen_registro = item.cntc_vorigen_registro,
                            cntc_vnumero_reserva = item.cntc_vnumero_reserva
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public void CuotaFomaEliminar(ECuotaFoma obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CUOTA_FOMA_ELIMINAR(
                        obj.ccf_icod_cuota,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ECuotaFoma> CuotaFomaListar(int cntc_icod_contrato)
        {
            List<ECuotaFoma> lista = new List<ECuotaFoma>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_CUOTA_FOMA_LISTAR(cntc_icod_contrato);
                    foreach (var item in collection)
                    {
                        lista.Add(new ECuotaFoma()
                        {
                            ccf_icod_cuota = item.ccf_icod_cuota,
                            cntc_icod_contrato = item.cntc_icod_contrato,
                            ccf_icod_nivel = item.ccf_icod_nivel,
                            rc_icod_recibo = item.rc_icod_recibo,
                            ccf_nmonto_pagar = item.ccf_nmonto_pagar,
                            ccf_nmonto_pagado = item.ccf_nmonto_pagado,
                            cff_sfecha_pago = item.cff_sfecha_pago,
                            strNumRecibo = item.strNumRecibo,
                            dtFechaRecibo = item.dtFechaRecibo,
                            strNivel = item.strNivel
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

        public int CetificadoUsoPerpetuoInsertar(ECertificadoUsoPerpetuo objCertificado)
        {
            int? icod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CERTIFICADO_PERPETUO_INSERTAR
                        (
                        ref icod,
                        objCertificado.cntc_icod_contrato,
                        objCertificado.cup_vnumero,
                        objCertificado.cup_sfecha_emision,
                        objCertificado.cup_sfecha_entrega,
                        objCertificado.cup_isituacion,
                        objCertificado.intUsuario,
                        objCertificado.strPc,
                        objCertificado.cup_bautorizado
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Convert.ToInt32(icod);
        }

        public void CetificadoUsoPerpetuoModificar(ECertificadoUsoPerpetuo objCertificado)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CERTIFICADO_PERPETUO_MODIFICAR(
                        objCertificado.cup_icod,
                        objCertificado.cntc_icod_contrato,
                        objCertificado.cup_vnumero,
                        objCertificado.cup_sfecha_emision,
                        objCertificado.cup_sfecha_entrega,
                        objCertificado.cup_isituacion,
                        objCertificado.intUsuario,
                        objCertificado.strPc,
                        objCertificado.flag,
                        objCertificado.cup_bautorizado
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CuotaFomaModificar(ECuotaFoma objCuota)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CUOTA_FOMA_MODIFICAR(
                        objCuota.ccf_icod_cuota,
                        objCuota.cntc_icod_contrato,
                        objCuota.ccf_icod_nivel,
                        objCuota.rc_icod_recibo,
                        objCuota.ccf_nmonto_pagar,
                        objCuota.ccf_nmonto_pagado,
                        objCuota.cff_sfecha_pago,
                        objCuota.intUsuario,
                        objCuota.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int CuotaFomaInsertar(ECuotaFoma objCuota)
        {
            int? icod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CUOTA_FOMA_INSERTAR(
                        ref icod,
                        objCuota.cntc_icod_contrato,
                        objCuota.ccf_icod_nivel,
                        objCuota.rc_icod_recibo,
                        objCuota.ccf_nmonto_pagar,
                        objCuota.ccf_nmonto_pagado,
                        objCuota.cff_sfecha_pago,
                        objCuota.intUsuario,
                        objCuota.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Convert.ToInt32(icod);
        }

        public List<EContrato> ConsultaDatosFallecido(DateTime fechaInicio, DateTime fechaFin)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONSULTA_DATOS_FALLECIDOS(fechaInicio, fechaFin);

                    foreach (var item in query)
                    {
                        lista.Add(new EContrato()
                        {
                            cntc_vnombre_fallecido = item.cntc_vnombre_fallecido,
                            cntc_vapellido_paterno_fallecido = item.cntc_vapellido_paterno_fallecido,
                            cntc_vapellido_materno_fallecido = item.cntc_vapellido_materno_fallecido,
                            cntc_sfecha_fallecimiento = item.cntc_sfecha_fallecimiento,
                            cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_sfecha_contrato = Convert.ToDateTime(item.cntc_sfecha_contrato),
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_icod_vendedor = item.cntc_icod_vendedor,
                            cntc_itipo_sepultura = item.cntc_itipo_sepultura
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

        public List<EReciboCajaDetalle> listar_recibo_caja_detalle(int rc_icod_recibo)
        {
            List<EReciboCajaDetalle> lista = new List<EReciboCajaDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_RECIBO_CAJA_DETALLE_LISTAR(rc_icod_recibo);
                    foreach (var item in query)
                    {
                        lista.Add(new EReciboCajaDetalle()
                        {
                            rcd_icod_recibo = item.rcd_icod_recibo,
                            rc_icod_recibo = item.rc_icod_recibo,
                            rc_itipo_pago = item.rc_itipo_pago,
                            rcd_icantidad = item.rcd_icantidad,
                            rcd_dprecio_unit = item.rcd_dprecio_unit,
                            rcd_dprecio_total = item.rcd_dprecio_total,
                            rcd_inro_item = item.rcd_inro_item
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

        public void ResumenDocumentosCabMensajeRespuestaModificar(ESunatResumenDocumentosCab item, string mensajeRespuesta, DateTime? fechaEnvio)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_CAB_MENSAJE_RESPUESTA_ACTUALIZAR(item.IdCabecera, mensajeRespuesta, fechaEnvio);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void modificarResumenDiarioResponse(int id, EResumenResponse response)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RESUEMEN_RESPONSE_MODIFCAR(id, response.NroTicket, response.NombreArchivo, response.Exito, response.MensajeError, response.CodigoRespuesta);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void eliminar_recibo_caja_cabecera(EReciboCajaCabecera objCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RECIBO_CAJA_CABECERA_ELIMINAR(
                        objCabecera.rc_icod_recibo,
                        objCabecera.intUsuario,
                        objCabecera.strPc
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool verificarSerieReciboCaja(string numero)
        {
            bool? respuesta = false;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_VERIFICAR_EXISTENCIA_SERIE_RECIBO_CAJA(numero, ref respuesta);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Convert.ToBoolean(respuesta);
        }

        public void anular_recibo_caja_cabecera(EReciboCajaCabecera objCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RECIBO_CAJA_CABECERA_MODIFICAR(
                        objCabecera.rc_icod_recibo,
                        objCabecera.rc_vnumero,
                        objCabecera.rc_icod_cliente,
                        objCabecera.rc_sfecha_recibo,
                        objCabecera.rc_itipo_moneda,
                        objCabecera.rc_isituacion,
                        objCabecera.rc_icod_contrato,
                        objCabecera.rc_dmonto_total,
                        objCabecera.rc_vnombre_cliente,
                        objCabecera.rc_vdireccion_cliente,
                        objCabecera.rc_vnro_doc_cliente,
                        objCabecera.intUsuario,
                        objCabecera.strPc,
                        objCabecera.rc_icod_foma_anulado
                        );
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int insertar_recibo_caja_detalle(EReciboCajaDetalle obj)
        {
            int? icod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RECIBO_CAJA_DETALLE_INSERTAR(
                            ref icod,
                            obj.rcd_inro_item,
                            obj.rc_icod_recibo,
                            obj.rc_itipo_pago,
                            obj.rcd_icantidad,
                            obj.rcd_dprecio_unit,
                            obj.rcd_dprecio_total,
                            obj.intUsuario,
                            obj.strPc
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Convert.ToInt32(icod);
        }

        public void MondificarMontosContrato(decimal monto, int rc_icod_contrato, bool indicador)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_MODIFICAR_MONTOS(monto, rc_icod_contrato, indicador);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public EContratoFallecido ObnterFallecido(int cntc_icod_contrato_fallecido)
        {
            EContratoFallecido obj = new EContratoFallecido();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_FALLECIDO_X_ICOD(cntc_icod_contrato_fallecido);
                    foreach (var item in query)
                    {

                        obj.cntc_icod_contrato_fallecido = item.cntc_icod_contrato_fallecido;
                        obj.cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato);
                        obj.cntc_vnombre_fallecido = item.cntc_vnombre_fallecido;
                        obj.cntc_vapellido_paterno_fallecido = item.cntc_vapellido_paterno_fallecido;
                        obj.cntc_vapellido_materno_fallecido = item.cntc_vapellido_materno_fallecido;
                        obj.cntc_vdni_fallecido = item.cntc_vdni_fallecido;
                        obj.cntc_sfecha_nac_fallecido = item.cntc_sfecha_nac_fallecido;
                        obj.cntc_sfecha_fallecimiento = item.cntc_sfecha_fallecimiento;
                        obj.cntc_sfecha_entierro = item.cntc_sfecha_entierro;
                        obj.cntc_itipo_documento_fallecido = Convert.ToInt32(item.cntc_itipo_documento_fallecido);
                        obj.cntc_vdocumento_fallecido = item.cntc_vdocumento_fallecido;
                        obj.cntc_inacionalidad = Convert.ToInt32(item.cntc_inacionalidad);
                        obj.cntc_icod_indicador_espacios = Convert.ToInt32(item.cntc_icod_indicador_espacios);
                        obj.cntc_vdireccion_fallecido = item.cntc_vdireccion_fallecido;
                        obj.cntc_icod_religiones = Convert.ToInt32(item.cntc_icod_religiones);
                        obj.cntc_icod_tipo_deceso = Convert.ToInt32(item.cntc_icod_tipo_deceso);
                        obj.cntc_vobservacion = item.cntc_vobservacion;
                        obj.strReligiones = item.strReligiones;
                        obj.strTipoDeceso = item.strTipoDeceso;
                        obj.cntc_icod_tamanio_lapida = Convert.ToInt32(item.cntc_icod_tamanio_lapida);
                        obj.cntc_sfecha_accion = item.cntc_sfecha_accion;
                        obj.espad_vnivel = item.espad_vnivel;
                        obj.cntc_vfrase = item.cntc_vfrase;
                        obj.cntc_vpensamiento = item.cntc_vpensamiento;
                        obj.cntc_itipo_sepultura = item.cntc_itipo_sepultura;
                        obj.cntc_icod_manzana = item.cntc_icod_manzana;
                        obj.cntc_icod_isepultura = item.cntc_icod_isepultura;
                        obj.espac_iid_iespacios = item.espac_iid_iespacios;
                        obj.cntc_icod_plataforma = item.cntc_icod_plataforma;
                        obj.strsepultura = item.strsepultura;
                        obj.espac_icod_vespacios = item.espac_icod_vespacios;
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public void modificar_recibo_caja_cabecera(EReciboCajaCabecera obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RECIBO_CAJA_CABECERA_MODIFICAR(
                            obj.rc_icod_recibo,
                            obj.rc_vnumero,
                            obj.rc_icod_cliente,
                            obj.rc_sfecha_recibo,
                            obj.rc_itipo_moneda,
                            obj.rc_isituacion,
                            obj.rc_icod_contrato,
                            obj.rc_dmonto_total,
                            obj.rc_vnombre_cliente,
                            obj.rc_vdireccion_cliente,
                            obj.rc_vnro_doc_cliente,
                            obj.intUsuario,
                            obj.strPc,
                            obj.rc_icod_foma_anulado
                        );
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
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GET_FOMA_FINANCIAMIENTO_COTRATO_X_ICOD(rc_icod_contrato);
                    foreach (var item in query)
                    {
                        obj.cntc_nmonto_foma = Convert.ToDecimal(item.cntc_nmonto_foma);
                        obj.cntc_nfinanciamientro = Convert.ToDecimal(item.cntc_nfinanciamientro);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return obj;
        }

        public void eliminar_recibo_caja_detalle(EReciboCajaDetalle obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RECIBO_CAJA_DETALLE_ELIMINAR(
                            obj.rcd_icod_recibo,
                            obj.intUsuario,
                            obj.strPc
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void modificar_recibo_caja_detalle(EReciboCajaDetalle obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RECIBO_CAJA_DETALLE_MODIFICAR(
                            obj.rcd_icod_recibo,
                            obj.rcd_inro_item,
                            obj.rc_itipo_pago,
                            obj.rcd_icantidad,
                            obj.rcd_dprecio_unit,
                            obj.rcd_dprecio_total,
                            obj.intUsuario,
                            obj.strPc
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int insertar_recibo_caja_cabecera(EReciboCajaCabecera obj)
        {
            int? icod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RECIBO_CAJA_CABECERA_INSERTAR(
                            ref icod,
                            obj.rc_vnumero,
                            obj.rc_icod_cliente,
                            obj.rc_sfecha_recibo,
                            obj.rc_itipo_moneda,
                            obj.rc_isituacion,
                            obj.rc_icod_contrato,
                            obj.rc_dmonto_total,
                            obj.rc_vnombre_cliente,
                            obj.rc_vdireccion_cliente,
                            obj.rc_vnro_doc_cliente,
                            obj.intUsuario,
                            obj.strPc
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Convert.ToInt32(icod);
        }

        public List<EReciboCajaCabecera> listar_recibos_caja_cabecera()
        {
            List<EReciboCajaCabecera> lista = new List<EReciboCajaCabecera>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_RECIBO_CAJA_CABECERA_LISTAR();

                    foreach (var item in query)
                    {
                        lista.Add(new EReciboCajaCabecera()
                        {
                            rc_icod_recibo = item.rc_icod_recibo,
                            rc_vnumero = item.rc_vnumero,
                            rc_icod_cliente = item.rc_icod_cliente,
                            rc_sfecha_recibo = item.rc_sfecha_recibo,
                            rc_itipo_moneda = item.rc_itipo_moneda,
                            rc_isituacion = item.rc_isituacion,
                            rc_icod_contrato = item.rc_icod_contrato,
                            rc_dmonto_total = item.rc_dmonto_total,
                            strNroDocCliente = item.strNroDocCliente,
                            strCliente = item.strCliente,
                            cliec_vnro_telefono = item.cliec_vnro_telefono,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            strContrato = item.strContrato,
                            rc_vnombre_cliente = item.rc_vnombre_cliente,
                            rc_vdireccion_cliente = item.rc_vdireccion_cliente,
                            rc_vnro_doc_cliente = item.rc_vnro_doc_cliente,
                            strNumContrato = item.strContrato,
                            strRucCliente = item.cliec_cruc,
                            cntc_sfecha_contrato = item.cntc_sfecha_contrato,
                            rc_icod_foma_anulado = item.rc_icod_foma_anulado
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

        public List<EProyeccionVendedor> listar_informe_ventas_por_mes(int anio, int mes, int asesor)
        {
            List<EProyeccionVendedor> lista = new List<EProyeccionVendedor>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_INFORME_VENTAS_X_MES(anio, mes, asesor);
                    foreach (var item in query)
                    {
                        lista.Add(new EProyeccionVendedor()
                        {
                            vendc_vnombre_vendedor = item.vendc_vnombre_vendedor,
                            vendc_vcod_vendedor = item.vendc_vcod_vendedor,
                            cant_necesidad_futura = Convert.ToInt32(item.cant_necesidad_futura),
                            cant_necesidad_inmediata = Convert.ToInt32(item.cant_necesidad_inmediata),
                            cant_credito = Convert.ToInt32(item.cant_credito),
                            cant_contado = Convert.ToInt32(item.cant_contado),
                            proyc_icantidad_estimada = Convert.ToInt32(item.proyc_icantidad_estimada)
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

        public void ProyeccionVentasEliminar(EProyeccionVendedor obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEVO_PREYECCION_VETAS_ASESOR_ELIMINAR(
                       obe.proyc_icod_proyeccion,
                       obe.proyc_iusuario,
                       obe.proyc_vpc
                       );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ProyeccionVentasModificar(EProyeccionVendedor obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEVO_PREYECCION_VETAS_ASESOR_MODIFICAR(
                       obj.proyc_icod_proyeccion,
                       obj.vendc_icod_vendedor,
                       obj.anioc_iid_anio_ejercicio,
                       obj.proyc_imes,
                       obj.proyc_icantidad_estimada,
                       obj.proyc_iusuario,
                       obj.proyc_vpc
                       );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ProyeccionVentasInsertar(EProyeccionVendedor obj)
        {
            int? icod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEVO_PREYECCION_VETAS_ASESOR_INSERTAR(
                        ref icod,
                        obj.vendc_icod_vendedor,
                        obj.anioc_iid_anio_ejercicio,
                        obj.proyc_imes,
                        obj.proyc_icantidad_estimada,
                        obj.proyc_iusuario,
                        obj.proyc_vpc
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Convert.ToInt32(icod);
        }

        public List<EProyeccionVendedor> ProyeccionVentasListar(int anio, int mes)
        {
            List<EProyeccionVendedor> lista = new List<EProyeccionVendedor>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEVO_PREYECCION_VETAS_ASESOR_LISTAR(anio, mes);
                    foreach (var item in query)
                    {
                        lista.Add(new EProyeccionVendedor()
                        {
                            proyc_icod_proyeccion = item.proyc_icod_proyeccion,
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            anioc_iid_anio_ejercicio = Convert.ToInt32(item.anioc_iid_anio_ejercicio),
                            proyc_imes = Convert.ToInt32(item.proyc_imes),
                            proyc_icantidad_estimada = Convert.ToInt32(item.proyc_icantidad_estimada)
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

        public void SGEV_INGRESAR_PLAN_TIPO_SEPULTURA(EContrato obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_INGRESAR_PLAN_TIPO_SEPULTURA(
                        obe.cntc_icod_contrato,
                        obe.cntc_icod_nombre_plan,
                        obe.cntc_itipo_sepultura
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EPlanillaCobranzaDet ObtenerDocumentoXid(int plnd_icod_documento)
        {
            EPlanillaCobranzaDet obe = new EPlanillaCobranzaDet();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_DET_OBTENER_X_ID(plnd_icod_documento);
                    foreach (var item in query)
                    {
                        obe.plnd_icod_detalle = item.plnd_icod_detalle;
                        obe.plnc_icod_planilla = item.plnc_icod_planilla;
                        obe.tablc_iid_tipo_mov = Convert.ToInt32(item.tablc_iid_tipo_mov);
                        obe.plnd_sfecha_doc = Convert.ToDateTime(item.plnd_sfecha_doc);
                        obe.plnd_icod_tipo_doc = item.plnd_icod_tipo_doc;
                        obe.plnd_icod_documento = item.plnd_icod_documento;
                        obe.plnd_vnumero_doc = item.plnd_vnumero_doc;
                        obe.plnd_nmonto = Convert.ToDecimal(item.plnd_nmonto);
                        obe.plnd_nmonto_pagado = Convert.ToDecimal(item.plnd_nmonto_pagado);
                        obe.strTipoDoc = item.tdocc_vabreviatura_tipo_doc;
                        obe.strCliente = item.strCliente;
                        obe.strTipoMov = item.strTipoMov;
                        obe.pgoc_icod_pago = item.pgoc_icod_pago;
                        obe.pgoc_dxc_icod_pago = item.pgoc_dxc_icod_pago;
                        obe.intIcodDxc = item.intIcodDxc;
                        obe.intCliente = Convert.ToInt32(item.intCliente);
                        obe.tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                        obe.strTipoMoneda = item.strTipoMoneda;
                        obe.intTipoPago = item.pgoc_tipo_pago;
                        obe.intNotaCredito = item.pgoc_icod_nota_credito;
                        obe.intTipoTarjeta = item.pgoc_icod_tipo_tarjeta;
                        obe.strPagoDescripcion = item.strPagoDescripcion;
                        obe.StrNotaCredito = item.StrNotaCredito;
                        obe.antc_icod_anticipo = item.antc_icod_anticipo;
                        obe.strAnticipo = item.strAnticipo;
                        obe.strAdelantoCliente = item.strAdelantoCliente;
                        obe.intSituacionFavBov = Convert.ToInt32(item.intSituacion);
                        obe.strSituacionFavBov = item.strSituacion;
                        obe.plnd_tipo_cambio = Convert.ToDecimal(item.plnd_tipo_cambio);
                        obe.intIcodEntidadFinanMov = item.antc_icod_entidad_finan_mov;
                        obe.intAnaliticaCliente = Convert.ToInt32(item.intAnaliticaCliente);
                        obe.strCodAnaliticaCliente = item.strAnaliticaCliente;
                        obe.tdocd_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo);
                        /*Se utiliza los sgts. campos en contabilización para anticipos*/
                        obe.intAnaliticaBancoTarjetaBanco = item.intAnaliticaBancoTarjetaBanco;
                        obe.strCodAnaliticaBancoTarjetaBanco = item.strAnaliticaBancoTarjetaBanco;
                        obe.intCtaCbleTarjetaBanco = item.intCtaCbleTarjetaBanco;
                        obe.intTipoDocDelPago = item.intTipoDocDelPago;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obe;
        }

        public void FomaFinanciamientoModificar(EPagoFomaFinanciamiento obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FOMA_FINANCIAMIENTO_MODIFICAR(
                            obj.pgs_icod_pagos,
                            obj.pgs_icod_contrato,
                            obj.pgs_nmonto_pagado,
                            obj.pgs_itipo,
                            obj.rc_icod_recibo,
                            obj.intusuario,
                            obj.pgs_vpc,
                            obj.pgs_sfecha_pago

                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EPagoFomaFinanciamiento> listarFomaFinanciamiento(int cntc_icod_contrato)
        {
            List<EPagoFomaFinanciamiento> lista = new List<EPagoFomaFinanciamiento>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FOMA_FINANCIAMIENTO_LISTAR(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EPagoFomaFinanciamiento()
                        {
                            pgs_icod_pagos = item.pgs_icod_pagos,
                            pgs_icod_contrato = item.pgs_icod_contrato,
                            pgs_nmonto_pagado = Convert.ToDecimal(item.pgs_nmonto_pagado),
                            pgs_sfecha_pago = item.pgs_sfecha_pago,
                            pgs_itipo = item.pgs_itipo,
                            rc_vnumero = item.rc_vnumero
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public int FomaFinanciamientoInsertar(EPagoFomaFinanciamiento obj)
        {
            int? icod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FOMA_FINANCIAMIENTO_INSERTAR(
                            ref icod,
                            obj.pgs_icod_contrato,
                            obj.pgs_nmonto_pagado,
                            obj.pgs_itipo,
                            obj.rc_icod_recibo,
                            obj.intusuario,
                            obj.pgs_vpc,
                            obj.pgs_sfecha_pago
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Convert.ToInt32(icod);
        }

        public void ExistenciaSerieEnEntregas(string serie, out bool existe, out string mensaje)
        {

            string valMensaje = string.Empty;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_OBTENER_EXISTENCIA_SERIE_ENTREGAS(serie);
                    foreach (var item in collection)
                    {
                        valMensaje = item.Column1;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            existe = string.IsNullOrEmpty(valMensaje) ? false : true;
            mensaje = valMensaje;
        }

        public List<EEntregaFormulario> listarEntregaFormularios(DateTime dtIninio, DateTime dtfinal, int asesor)
        {
            List<EEntregaFormulario> lista = new List<EEntregaFormulario>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ENTREGA_FORMULARIOS_LISTAR(dtIninio, dtfinal, asesor);
                    foreach (var item in query)
                    {
                        lista.Add(new EEntregaFormulario()
                        {
                            entf_icod_entrega = item.entf_icod_entrega,
                            entf_iid_entrega = Convert.ToInt32(item.entf_iid_entrega),
                            entf_sfecha_entrega = item.entf_sfecha_entrega,
                            entf_icod_vendedor = item.entf_icod_vendedor,
                            entf_vobservaciones = item.entf_vobservaciones,
                            entf_icod_estado = Convert.ToInt32(item.entf_icod_estado),
                            entf_vnumero_formulario = item.entf_vnumero_formulario,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante == null ? "" : item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante == null ? "" : item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante == null ? "" : item.cntc_vapellido_materno_contratante,
                            strContratante = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante,
                                                                        item.cntc_vapellido_paterno_contratante,
                                                                        item.cntc_vapellido_materno_contratante)
                        });
                    }
                }
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ENTREGA_FORMULARIOS_DETALLE_LISTAR(entf_icod_entrega);

                    foreach (var item in query)
                    {
                        lista.Add(new EEntregaFormularioDetalle()
                        {
                            entfd_icod_entrega = item.entfd_icod_entrega,
                            entf_icod_entrega = item.entf_icod_entrega,
                            entfd_iid_entrega = Convert.ToInt32(item.entfd_iid_entrega),
                            entfd_vdocumento = item.entfd_vdocumento,
                            entfd_vestado = item.entfd_vestado != "A" ? "Ocupado" : "Desocupado",
                        });
                    }
                }
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ENTREGA_FORMUALARIOS_ELIMINAR(obe.entf_icod_entrega, obe.entf_iusuario_modifica, obe.entf_sfecha_modifica, obe.entf_vpc_modifica);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void modificarEntregaFormularioDetalle(EEntregaFormularioDetalle x)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ENTREGA_FORMULARIOS_DETALLE_MODIFICAR(
                        x.entfd_icod_entrega,
                        x.entf_icod_entrega,
                        x.entfd_iid_entrega,
                        x.entfd_vdocumento,
                        x.entfd_iusuario_modifa,
                        x.entfd_vpc_modifica
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void insertarEntregaFormularioDetalle(EEntregaFormularioDetalle x)
        {
            int? icod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ENTREGA_FORMULARIOS_DETALLE_INSERTAR(
                        ref icod,
                        x.entf_icod_entrega,
                        x.entfd_iid_entrega,
                        x.entfd_vdocumento,
                        x.entfd_iusuario_crea,
                        x.entfd_vpc_crea
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ENTREGA_FORMULARIOS_MODIFICAR(
                        obj.entf_icod_entrega,
                        obj.entf_iid_entrega,
                        obj.entf_sfecha_entrega,
                        obj.entf_icod_vendedor,
                        obj.entf_vobservaciones,
                        obj.entf_iusuario_modifica,
                        obj.entf_vpc_modifica,
                        obj.entf_icod_estado,
                        obj.entf_vnumero_formulario
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int insertarEntregaFormulario(EEntregaFormulario obj)
        {
            int? codigo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ENTREGA_FORMULARIOS_INSERTAR(
                        ref codigo,
                        obj.entf_iid_entrega,
                        obj.entf_sfecha_entrega,
                        obj.entf_icod_vendedor,
                        obj.entf_vobservaciones,
                        obj.entf_iusuario_crea,
                        obj.entf_vpc_crea,
                        obj.entf_icod_estado,
                        obj.entf_vnumero_formulario
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Convert.ToInt32(codigo);
        }

        public void eliminarEntregaFormularioDetalle(EEntregaFormularioDetalle x)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ENTREGA_FORMULARIOS_DETALLE_ELIMINAR(
                        x.entfd_icod_entrega,
                        x.entfd_iusuario_modifa,
                        x.entfd_vpc_modifica
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EEspaciosDet> Control_Sepultura_Listar()
        {
            List<EEspaciosDet> lista = new List<EEspaciosDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTROL_SEPULTURA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEspaciosDet()
                        {
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strsepultura = item.strsepultura,
                            espad_vnivel = item.espad_vnivel,
                            strestado = item.strestado,
                            strSituacion = item.strSituacion,
                            cntc_vcodigo_sepultura = item.cntc_vcodigo_sepultura,
                            strtiposepultura = item.strtiposepultura,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            cntc_vnombre_fallecido = item.cntc_vnombre_fallecido,
                            cntc_vapellido_paterno_fallecido = item.cntc_vapellido_paterno_fallecido,
                            cntc_vapellido_materno_fallecido = item.cntc_vapellido_materno_fallecido,
                            cntc_sfecha_contrato = item.cntc_sfecha_contrato == null ? (DateTime?)null : (DateTime)item.cntc_sfecha_contrato,
                            cntc_sfecha_nac_fallecido = item.cntc_sfecha_nac_fallecido == null ? (DateTime?)null : (DateTime)item.cntc_sfecha_nac_fallecido,
                            cntc_sfecha_fallecimiento = item.cntc_sfecha_fallecimiento == null ? (DateTime?)null : (DateTime)item.cntc_sfecha_fallecimiento,
                            cntc_sfecha_entierro = item.cntc_sfecha_entierro == null ? (DateTime?)null : (DateTime)item.cntc_sfecha_entierro,
                            disc_vdescripcion = item.disc_vdescripcion,
                            strorigenventa = item.strorigenventa,

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

        public void modificarContratatante(EContratante obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATANTES_MODIFICAR(
                        obj.cntcc_icod_contratante,
                        obj.cntc_icod_contrato,
                        obj.cntcc_vnombre_contratante,
                        obj.cntcc_vapellido_paterno_contratante,
                        obj.cntcc_vapellido_materno_contratante,
                        obj.cntcc_vdni_contratante,
                        obj.cntcc_vruc_contratante,
                        obj.cntcc_sfecha_nacimineto_contratante,
                        obj.cntcc_vtelefono_contratante,
                        obj.cntcc_vdireccion_correo_contratante,
                        obj.cntcc_vdireccion_contratante,
                        obj.cntcc_inacionalidad_contratante,
                        obj.cntcc_itipo_documento_contratante,
                        obj.cntcc_iusuario_modifica,
                        obj.cntcc_vpc_modifica,
                        obj.cntcc_bflag_seleccion,
                        obj.cntcc_bflag_estado
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int insertarContratante(EContratante obj)
        {
            int? codigo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 999999999;
                    dc.SGEV_CONTRATANTES_INSERTAR(
                        ref codigo,
                        obj.cntc_icod_contrato,
                        obj.cntcc_iid_contratante,
                        obj.cntcc_vnombre_contratante,
                        obj.cntcc_vapellido_paterno_contratante,
                        obj.cntcc_vapellido_materno_contratante,
                        obj.cntcc_vdni_contratante,
                        obj.cntcc_vruc_contratante,
                        obj.cntcc_sfecha_nacimineto_contratante,
                        obj.cntcc_vtelefono_contratante,
                        obj.cntcc_vdireccion_correo_contratante,
                        obj.cntcc_vdireccion_contratante,
                        obj.cntcc_inacionalidad_contratante,
                        obj.cntcc_itipo_documento_contratante,
                        obj.cntcc_iusuario_crea,
                        obj.cntcc_vpc_crea,
                        obj.cntcc_bflag_seleccion
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Convert.ToInt32(codigo);
        }

        public List<EContratante> listarContratantes(int cntc_icod_contrato)
        {
            List<EContratante> lista = new List<EContratante>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATANTES_LISTAR(cntc_icod_contrato);

                    foreach (var item in query)
                    {
                        lista.Add(new EContratante()
                        {
                            cntcc_icod_contratante = item.cntcc_icod_contratante,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntcc_iid_contratante = Convert.ToInt32(item.cntcc_iid_contratante),
                            cntcc_vnombre_contratante = item.cntcc_vnombre_contratante,
                            cntcc_vapellido_paterno_contratante = item.cntcc_vapellido_paterno_contratante,
                            cntcc_vapellido_materno_contratante = item.cntcc_vapellido_materno_contratante,
                            cntcc_vdni_contratante = item.cntcc_vdni_contratante,
                            cntcc_vruc_contratante = item.cntcc_vruc_contratante,
                            cntcc_sfecha_nacimineto_contratante = Convert.ToDateTime(item.cntcc_sfecha_nacimineto_contratante),
                            cntcc_vtelefono_contratante = item.cntcc_vtelefono_contratante,
                            cntcc_vdireccion_correo_contratante = item.cntcc_vdireccion_correo_contratante,
                            cntcc_vdireccion_contratante = item.cntcc_vdireccion_contratante,
                            cntcc_inacionalidad_contratante = Convert.ToInt32(item.cntcc_inacionalidad_contratante),
                            cntcc_itipo_documento_contratante = Convert.ToInt32(item.cntcc_itipo_documento_contratante),
                            cntcc_bflag_seleccion = Convert.ToBoolean(item.cntcc_bflag_seleccion),
                            strNombreCompleto = $"{item.cntcc_vnombre_contratante} {item.cntcc_vapellido_paterno_contratante} {item.cntcc_vapellido_materno_contratante}"
                        });
                    }
                }
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEP_LISTAR_PAGOS_DOCUMENTOS(cod);

                    foreach (var item in query)
                    {
                        lista.Add(new EPagosCuotas
                        {
                            cntc_icod_contrato_cuotas = Convert.ToInt32(item.cntc_icod_contrato_cuotas),
                            pgc_nmonto_pago = Convert.ToDecimal(item.pgc_nmonto_pago),
                            cntc_icod_documento = Convert.ToInt32(item.cntc_icod_documento),
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            strTipoDoc = item.tdocc_icod_tipo_doc == null ? "" : item.tdocc_icod_tipo_doc == 26 ? "FAV" : "BOV",
                            plnd_sfecha_doc = Convert.ToDateTime(item.plnd_sfecha_doc),
                            pgc_nmonto_pago_mora = Convert.ToDecimal(item.pgc_nmonto_pago_mora)
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

        public List<EReprogramaciones> ListarReprogramaciones(int cntc_icod_contrato)
        {
            List<EReprogramaciones> list = new List<EReprogramaciones>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEC_REPROGRAMACIONES_LISTAR(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        list.Add(new EReprogramaciones
                        {
                            cntcr_icod_reprogracion = item.cntcr_icod_reprogramacion,
                            cntcr_iid_reprogramacion = item.cntcr_iid_reprogramacion,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntcr_inro_cuotas = Convert.ToInt32(item.cntcr_inro_cuotas),
                            cntcr_sfecha_cuota = Convert.ToDateTime(item.cntcr_sfecha_cuota),
                            cntcr_nmonto_cuota = Convert.ToDecimal(item.cntcr_nmonto_cuota),
                            cntcr_icod_situacion = Convert.ToInt32(item.cntcr_icod_situacion),
                            cntcr_nmonto_saldo_anterior = Convert.ToDecimal(item.cntcr_nmonto_saldo_anterior),
                            cntcr_nmonto_cuota_total = Convert.ToDecimal(item.cntcr_nmonto_cuota_total),
                            cntcr_nmonto_total = Convert.ToDecimal(item.cntcr_nmonto_total),
                            cntcr_nvariacion_interes = Convert.ToDecimal(item.cntcr_nvariacion_interes),
                            strSituacion = item.strSituacion,
                            cntcr_nmonto_financiamiento = Convert.ToDecimal(item.cntcr_nmonto_financiamiento),
                            cntcr_iplan = Convert.ToInt32(item.cntcr_iplan),
                            cntcr_vobservaciones = item.cntcr_vobservaciones
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }

        public void Actualizar_Cuotas_Reprogramacion_Eliminada(int reprogramacionAnterior, int cntc_icod_contrato)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ACTUALIZAR_CUOTAS_REPROGRAMACION_ELIMINADA(reprogramacionAnterior, cntc_icod_contrato);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EContratoCuotas ListarContratoCronogramas(int cntc_icod_contrato, int i)
        {
            EContratoCuotas obe = new EContratoCuotas();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_LISTAR_CRONOGRAMAS(cntc_icod_contrato, i);
                    foreach (var item in query)
                    {
                        obe.cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato);
                        obe.cntc_itipo_cuota = Convert.ToInt32(item.cntc_itipo_cuota);
                        obe.numero_cuotas = Convert.ToInt32(item.numero_cuotas);
                        obe.monto_total = Convert.ToDecimal(item.monto_total);
                        obe.monto_cuota = Convert.ToDecimal(item.monto_cuota);
                        obe.cntc_sfecha_cuota = Convert.ToDateTime(item.cntc_sfecha_cuota);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obe;
        }

        public void EliminarDetReprogramacion(int cntcr_iid_reprogramacion, int cntc_icod_contrato)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ELIMINAR_CUOTAS_POR_REPROGRAMACION(cntcr_iid_reprogramacion, cntc_icod_contrato);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEP_LISTAR_PAGOS(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EContratoCuotas
                        {
                            cntc_icod_contrato_cuotas = item.cntc_icod_contrato_cuotas,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            flag_multiple = Convert.ToBoolean(item.cntc_flag_multiple),
                            cntc_icod_tipo_cuota = Convert.ToInt32(item.cntc_icod_tipo_cuota),
                            strTipo = item.strTipo,
                            cntc_itipo_cuota = Convert.ToInt32(item.cntc_itipo_cuota),
                            cntc_nmonto_cuota = Convert.ToDecimal(item.cntc_nmonto_cuota),
                            cntc_npagado = Convert.ToDecimal(item.pgc_nmonto_pago),
                            cntc_icod_situacion = Convert.ToInt32(item.cntc_icod_situacion),
                            strSituacion = item.strSituacion,
                            pgc_icod_pago = Convert.ToInt32(item.pgc_icod_pago),
                            cntc_nsaldo = Convert.ToDecimal(item.cntc_nsaldo),
                            monto_pagar = Convert.ToDecimal(item.pgc_nmonto_pago),
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_sfecha_cuota = Convert.ToDateTime(item.cntc_sfecha_cuota),
                            strTipoCredito = Convert.ToInt32(item.cntc_itipo_cuota) == 0 ? "PRINCIPAL" : "REPROGRAMACION " + item.cntc_itipo_cuota.ToString(),
                            cntc_icod_documento = Convert.ToInt32(item.cntc_icod_documento),
                            cntc_nmonto_mora = Convert.ToDecimal(item.cntc_nmonto_mora),
                            cntc_nmonto_mora_pago = Convert.ToDecimal(item.cntc_nmonto_mora_pago)
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

        public int InsertarCliente(ECliente ob)
        {
            int? id_cliente = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_CLIENTE_INSERTAR(
                            ref id_cliente,
                            ob.giroc_icod_giro,
                            ob.cliec_vnombre_cliente,
                            ob.cliec_vnombre_comercial,
                            ob.cliec_vdireccion_cliente,
                            ob.ubicc_icod_ubicacion,
                            ob.cliec_vnro_telefono,
                            ob.cliec_vnro_fax,
                            ob.cliec_vnro_celular,
                            ob.tabl_iid_tipo_documento,
                            ob.cliec_vnumero_doc_cli,
                            ob.cliec_vnombre_contacto,
                            ob.tablc_iid_tipo_relacion_cli,
                            ob.vendc_icod_vendedor,
                            ob.cliec_sfecha_registro_cliente,
                            ob.cliec_iid_situacion_cliente,
                            ob.usuario,
                            ob.pc,
                            ob.cliec_vcorreo_electronico,
                            ob.pcomc_icod_pcompra,
                            ob.cliec_vapellido_paterno,
                            ob.cliec_vapellido_materno,
                            ob.cliec_vnombres,
                            ob.tablc_iid_tipo_cliente,
                            ob.cliec_cruc,
                            ob.cliec_vcod_cliente,
                            ob.cliec_bcredito,
                            ob.cliec_nnumero_dias

                            );
                }
                return Convert.ToInt32(id_cliente);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_ELILMINAR_REPROGRAMACIONES(obj.cntcr_icod_reprogracion);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ModificarReprogramacion(EReprogramaciones obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_REPROGRAMACIONES_MODIFICAR(
                        obj.cntcr_icod_reprogracion,
                        obj.cntcr_iid_reprogramacion,
                        obj.cntc_icod_contrato,
                        obj.cntcr_inro_cuotas,
                        obj.cntcr_sfecha_cuota,
                        obj.cntcr_nmonto_cuota,
                        obj.cntcr_icod_situacion,
                        obj.cntcr_iusuario_modifica,
                        obj.cntcr_vpc_modifica,
                        obj.cntcr_nmonto_saldo_anterior,
                        obj.cntcr_nmonto_cuota_total,
                        obj.cntcr_nmonto_total,
                        obj.cntcr_nvariacion_interes,
                        obj.cntcr_nmonto_financiamiento,
                        obj.cntcr_iplan,
                        obj.cntcr_vobservaciones
                       );

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ActualizarReprogramacion(int cntcr_icod_reprogracion, int cntc_icod_contrato, int cntcr_iid_reprogramacion)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_ACTUALIZAR_REPROGRAMACIONES(
                        cntcr_iid_reprogramacion,
                        cntc_icod_contrato,
                        cntcr_icod_reprogracion
                        );
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertarReprogramacion(EReprogramaciones obj)
        {
            int? codigo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_REPROGRAMACIONES_INSERTAR(
                        ref codigo,
                        obj.cntcr_iid_reprogramacion,
                        obj.cntc_icod_contrato,
                        obj.cntcr_inro_cuotas,
                        obj.cntcr_sfecha_cuota,
                        obj.cntcr_nmonto_cuota,
                        obj.cntcr_icod_situacion,
                        obj.cntcr_iusuario_crea,
                        obj.cntcr_vpc_crea,
                        obj.cntcr_nmonto_saldo_anterior,
                        obj.cntcr_nmonto_cuota_total,
                        obj.cntcr_nmonto_total,
                        obj.cntcr_nvariacion_interes,
                        obj.cntcr_nmonto_financiamiento,
                        obj.cntcr_iplan,
                        obj.cntcr_vobservaciones
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Convert.ToInt32(codigo);
        }

        public void ActualizarCliente(ECliente ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_CLIENTE_ACTUALIZAR(ob.cliec_icod_cliente,
                            ob.giroc_icod_giro,
                            ob.cliec_vnombre_cliente,
                            ob.cliec_vnombre_comercial,
                            ob.cliec_vdireccion_cliente,
                            ob.ubicc_icod_ubicacion,
                            ob.cliec_vnro_telefono,
                            ob.cliec_vnro_fax,
                            ob.cliec_vnro_celular,
                            ob.tabl_iid_tipo_documento,
                            ob.cliec_vnumero_doc_cli,
                            ob.cliec_vnombre_contacto,
                            ob.tablc_iid_tipo_relacion_cli,
                            ob.vendc_icod_vendedor,
                            ob.cliec_sfecha_registro_cliente,
                            ob.cliec_iid_situacion_cliente,
                            ob.usuario,
                            ob.pc,
                            ob.cliec_vcorreo_electronico,
                            ob.pcomc_icod_pcompra,
                            ob.cliec_vapellido_paterno,
                            ob.cliec_vapellido_materno,
                            ob.cliec_vnombres,
                            ob.tablc_iid_tipo_cliente,
                            ob.cliec_cruc,
                            ob.anac_icod_analitica,
                            ob.cliec_bcredito,
                            ob.cliec_nnumero_dias

                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarClienteAnalitica(ECliente ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_CLIENTE_ACTUALIZAR_ANALITICA(
                            ob.cliec_icod_cliente,
                            ob.anac_icod_analitica
                            );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGES_CLIENTE_ELIMINAR(cliec_icod_cliente, usuario, pc, anac_icod_analitica);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void actualizarClienteOT(int intOT, int intClienteReal, int intVehiculo)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_CLIENTE_OT(
                        intOT,
                        intClienteReal,
                        intVehiculo
                           );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Ubicacion
        public List<EUbicacion> ListarUbicacion()
        {
            List<EUbicacion> lista = new List<EUbicacion>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGES_UBICACION_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EUbicacion()
                        {
                            ubicc_icod_ubicacion = item.ubicc_icod_ubicacion,
                            tablc_iid_tipo_ubicacion = item.tablc_iid_tipo_ubicacion,
                            Ubicacion = item.Ubicacion,
                            ubicc_ccod_ubicacion = item.ubicc_ccod_ubicacion,
                            ubicc_vnombre_ubicacion = item.ubicc_vnombre_ubicacion,
                            ubicc_icod_ubicacion_padre = item.ubicc_icod_ubicacion_padre,
                            ubicc_iid_situacion_ubicacion = item.ubicc_iid_situacion_ubicacion
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
        public void InsertarUbicacion(EUbicacion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_UBICACION_INSERTAR(ob.tablc_iid_tipo_ubicacion,
                        ob.ubicc_ccod_ubicacion,
                        ob.ubicc_vnombre_ubicacion,
                        ob.ubicc_icod_ubicacion_padre,
                        ob.ubicc_iid_situacion_ubicacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarUbicacion(EUbicacion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_UBICACION_ACTUALIZAR(ob.ubicc_icod_ubicacion,
                        ob.tablc_iid_tipo_ubicacion,
                        ob.ubicc_ccod_ubicacion,
                        ob.ubicc_vnombre_ubicacion,
                        ob.ubicc_icod_ubicacion_padre,
                        ob.ubicc_iid_situacion_ubicacion);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_UBICACION_ELIMINAR(ubicc_icod_ubicacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Giro
        public List<EGiro> ListarGiro()
        {
            List<EGiro> lista = new List<EGiro>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGES_GIRO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EGiro()
                        {
                            giroc_icod_giro = item.giroc_icod_giro,
                            giroc_iid_giro = item.giroc_iid_giro,
                            giroc_vnombre_giro = item.giroc_vnombre_giro,
                            tablc_iid_situacion_giro = item.tablc_iid_situacion_giro,
                            giroc_bindicador_expo_nextel = item.giroc_bindicador_expo_nextel,
                            DescripSituacion = item.DescripSituacion
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
        public void InsertarGiro(EGiro ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_GIRO_INSERTAR(ob.giroc_iid_giro,
                            ob.giroc_vnombre_giro,
                            ob.tablc_iid_situacion_giro,
                            ob.giroc_bindicador_expo_nextel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarGiro(EGiro ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_GIRO_ACTUALIZAR(ob.giroc_icod_giro,
                            ob.giroc_iid_giro,
                            ob.giroc_vnombre_giro,
                            ob.tablc_iid_situacion_giro,
                            ob.giroc_bindicador_expo_nextel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EliminarGiro(int giroc_icod_giro)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_GIRO_ELIMINAR(giroc_icod_giro);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        public List<EAdelantoProveedor> ListarAdelantoProveedoresCorrelativo(int intEjercicio)
        {
            List<EAdelantoProveedor> lista = new List<EAdelantoProveedor>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTT_ADELANTO_PROVEEDOR_CORRELATIVOS_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoProveedor()
                        {
                            vnumero_adelanto = item.adpr_vnumero_adelanto
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

        public void Elimina_Pago(int pgc_icod_pago)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SEGP_PAGOS_ELIMINAR(pgc_icod_pago);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EAdelantoCliente> ListarAdelantoClientexAñoTodos(int intEjercicio)
        {
            List<EAdelantoCliente> lista = new List<EAdelantoCliente>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTT_ADELANTO_CLIENTE_LISTAR_X_AÑO(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoCliente()
                        {
                            vnumero_adelanto = item.adci_vnumero_adelanto
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

        #region Mantenimiento Factura Venta Electronica
        public List<EFacturaVentaElectronica> listarfacturaVentaElectronica(DateTime fechaInicio)
        {
            DateTime dateOut;
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_LISTAR(fechaInicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            IdCabecera = item.IdCabecera,
                            idDocumento = item.idDocumento.Trim(),
                            fechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            //horaEmision = DateTime.ParseExact(item.horaEmision.Trim(), "HH:mm:ss", CultureInfo.InvariantCulture).ToString("HH:mm:ss"),
                            //horaEmision = Convert.ToDateTime(item.horaEmision),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            fechaVencimiento = DateTime.TryParseExact(item.fehaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut) == true ? DateTime.ParseExact(item.fehaVencimiento.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : "",
                            tipoDocumento = item.tipoDocumento.Trim(),
                            StrTipoDoc = item.StrTipoDoc,
                            moneda = item.moneda.Trim(),
                            CodMotivoNota = item.CodMotivoNota,
                            DescripMotivoNota = item.DescripMotivoNota,
                            NroDocqModifica = item.NroDocqModifica,
                            TipoDocqModifica = item.TipoDocqModifica,
                            cantidadItems = Convert.ToInt32(item.cantidadItems),
                            nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            CodLocalEmisor = Convert.ToInt32(item.CodLocalEmisor),
                            nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            direccionReceptor = item.direccionReceptor,
                            CodMotivoDescuento = Convert.ToInt32(item.CodMotivoDescuento),
                            PorcDescuento = Convert.ToDecimal(item.PorcDescuento),
                            MontoDescuentoGlobal = Convert.ToDecimal(item.MontoDescuentoGlobal),
                            BaseMontoDescuento = Convert.ToDecimal(item.BaseMontoDescuento),
                            MontoTotalImpuesto = Convert.ToDecimal(item.MontoTotalImpuesto),
                            MontoGravadasIGV = Convert.ToDecimal(item.MontoGravadasIGV),
                            CodigoTributo = Convert.ToInt32(item.CodigoTributo),
                            MontoExonerado = Convert.ToDecimal(item.MontoExonerado),
                            MontoInafecto = Convert.ToDecimal(item.MontoInafecto),
                            MontoGratuitoImpuesto = Convert.ToDecimal(item.MontoGratuitoImpuesto),
                            MontoBaseGratuito = Convert.ToDecimal(item.MontoBaseGratuito),
                            totalIgv = Convert.ToDecimal(item.totalIgv),
                            MontoGravadosISC = Convert.ToDecimal(item.MontoGravadosISC),
                            totalIsc = Convert.ToDecimal(item.totalIsc),
                            MontoGravadosOtros = Convert.ToDecimal(item.MontoGravadosOtros),
                            totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            TotalValorVenta = Convert.ToDecimal(item.TotalValorVenta),
                            TotalPrecioVenta = Convert.ToDecimal(item.TotalPrecioVenta),
                            MontoDescuento = Convert.ToDecimal(item.MontoDescuento),
                            MontoTotalCargo = Convert.ToDecimal(item.MontoTotalCargo),
                            MontoTotalAnticipo = Convert.ToDecimal(item.MontoTotalAnticipo),
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            EstadoAnulacion = Convert.ToInt32(item.EstadoAnulacion),
                            EAnulado = item.EAnulado,
                            EstadoSunat = item.EstadoSunat,
                            Mensaje = item.Mensaje,
                            FormaPagoS = item.FormaPago,
                            MontoTotalPago = Convert.ToDecimal(item.MontoTotalPago),
                            NroCuota = item.NroCuota,
                            MontoCuota = Convert.ToDecimal(item.MontoTotalPago),
                            FechaPago = item.FechaPago,
                            Hora = item.Hora
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
        public List<EFacturaVentaElectronica> FacturaVentaElectronicaObtenerDoc(int doc_icod_documento , string tipoDocumento)
        {
            DateTime dateOut;
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_GET_DOC(doc_icod_documento, tipoDocumento);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            IdCabecera = item.IdCabecera,
                            idDocumento = item.idDocumento.Trim(),
                            fechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            //horaEmision = DateTime.ParseExact(item.horaEmision.Trim(), "HH:mm:ss", CultureInfo.InvariantCulture).ToString("HH:mm:ss"),
                            //horaEmision = Convert.ToDateTime(item.horaEmision),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            fechaVencimiento = DateTime.TryParseExact(item.fehaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut) == true ? DateTime.ParseExact(item.fehaVencimiento.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : "",
                            tipoDocumento = item.tipoDocumento.Trim(),
                            StrTipoDoc = item.StrTipoDoc,
                            moneda = item.moneda.Trim(),
                            CodMotivoNota = item.CodMotivoNota,
                            DescripMotivoNota = item.DescripMotivoNota,
                            NroDocqModifica = item.NroDocqModifica,
                            TipoDocqModifica = item.TipoDocqModifica,
                            cantidadItems = Convert.ToInt32(item.cantidadItems),
                            nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            CodLocalEmisor = Convert.ToInt32(item.CodLocalEmisor),
                            nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            direccionReceptor = item.direccionReceptor,
                            CodMotivoDescuento = Convert.ToInt32(item.CodMotivoDescuento),
                            PorcDescuento = Convert.ToDecimal(item.PorcDescuento),
                            MontoDescuentoGlobal = Convert.ToDecimal(item.MontoDescuentoGlobal),
                            BaseMontoDescuento = Convert.ToDecimal(item.BaseMontoDescuento),
                            MontoTotalImpuesto = Convert.ToDecimal(item.MontoTotalImpuesto),
                            MontoGravadasIGV = Convert.ToDecimal(item.MontoGravadasIGV),
                            CodigoTributo = Convert.ToInt32(item.CodigoTributo),
                            MontoExonerado = Convert.ToDecimal(item.MontoExonerado),
                            MontoInafecto = Convert.ToDecimal(item.MontoInafecto),
                            MontoGratuitoImpuesto = Convert.ToDecimal(item.MontoGratuitoImpuesto),
                            MontoBaseGratuito = Convert.ToDecimal(item.MontoBaseGratuito),
                            totalIgv = Convert.ToDecimal(item.totalIgv),
                            MontoGravadosISC = Convert.ToDecimal(item.MontoGravadosISC),
                            totalIsc = Convert.ToDecimal(item.totalIsc),
                            MontoGravadosOtros = Convert.ToDecimal(item.MontoGravadosOtros),
                            totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            TotalValorVenta = Convert.ToDecimal(item.TotalValorVenta),
                            TotalPrecioVenta = Convert.ToDecimal(item.TotalPrecioVenta),
                            MontoDescuento = Convert.ToDecimal(item.MontoDescuento),
                            MontoTotalCargo = Convert.ToDecimal(item.MontoTotalCargo),
                            MontoTotalAnticipo = Convert.ToDecimal(item.MontoTotalAnticipo),
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            EstadoAnulacion = Convert.ToInt32(item.EstadoAnulacion),
                            EAnulado = item.EAnulado,
                            EstadoSunat = item.EstadoSunat,
                            Mensaje = item.Mensaje,
                            FormaPagoS = item.FormaPago,
                            MontoTotalPago = Convert.ToDecimal(item.MontoTotalPago),
                            NroCuota = item.NroCuota,
                            MontoCuota = Convert.ToDecimal(item.MontoTotalPago),
                            FechaPago = item.FechaPago,
                            Hora = item.Hora
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
        public EVendedor ObtenerIECDesdeFormatos(string formato)
        {
            EVendedor obj = new EVendedor();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_OBTENER_VENDEDOR_X_FORMATO(formato);
                    foreach (var item in query)
                    {
                        obj.vendc_icod_vendedor = item.entf_icod_vendedor;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public void Actualizar_Pagos(int cntc_icod_contrato_cuotas, int pgc_icod_pago)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEP_ACTUALIZAR_SALDO_CUOTA(
                        cntc_icod_contrato_cuotas,
                        pgc_icod_pago
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Modificar_Pagos(EPagosCuotas objPg)
        {

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SEGP_PAGOS_MODIFICAR(
                        objPg.pgc_icod_pago,
                        objPg.cntc_icod_contrato_cuotas,
                        objPg.pgc_nmonto_pago,
                        objPg.pgc_sfecha_pago,
                        objPg.tdocc_icod_tipo_doc,
                        objPg.cntc_icod_documento,
                        objPg.pgc_iusuario_modifica,
                        objPg.pgc_vpc_modifica,
                        objPg.pgc_nmonto_pago_mora
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public int Insertar_Pagos(EPagosCuotas objPg)
        {
            int? codigo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SEGP_PAGOS_INSERTAR(
                        ref codigo,
                        objPg.cntc_icod_contrato_cuotas,
                        objPg.pgc_nmonto_pago,
                        objPg.pgc_sfecha_pago,
                        objPg.tdocc_icod_tipo_doc,
                        objPg.cntc_icod_documento,
                        objPg.pgc_iusuario_crea,
                        objPg.pgc_vpc_crea,
                        objPg.pgc_nmonto_pago_mora

                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Convert.ToInt32(codigo);
        }

        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaAnular(DateTime fechaInicio)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_LISTAR_ANULAR(fechaInicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            //IdCabezera = item.IdCabezera,
                            //idDocumento = item.idDocumento.Trim(),
                            //tipoDocumento = item.tipoDocumento.Trim(),
                            //StrTipoDoc = item.StrTipoDoc,
                            //nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            //tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            //nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            //nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            //ubigeovarEmisor = item.ubigeovarEmisor,
                            //direccionEmisor = item.direccionEmisor,
                            //urbanizacionEmisor = item.urbanizacionEmisor.Trim(),
                            //departamentoEmisor = item.departamentoEmisor.Trim(),
                            //provinciaEmisor = item.provinciaEmisor.Trim(),
                            //distritoEmisor = item.distritoEmisor.Trim(),
                            //nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            //tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            //nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            //nombreComercialReceptor = item.nombreComercialReceptor,
                            //ubigeoReceptor = item.ubigeoReceptor,
                            //direccionReceptor = item.direccionReceptor,
                            //urbanizacionReceptor = item.urbanizacionReceptor,
                            //departamentoReceptor = item.departamentoReceptor,
                            //provinciaReceptor = item.provinciaReceptor,
                            //distritoReceptor = item.distritoReceptor,
                            //fechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"),
                            //FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            //// fechaEmision = item.fechaEmision,
                            //moneda = item.moneda.Trim(),
                            //tipoOperacion = item.tipoOperacion.Trim(),
                            //gravadas = item.gravadas,
                            //gratuitas = Convert.ToDecimal(item.gratuitas),
                            //inafectas = Convert.ToDecimal(item.inafectas),
                            //exoneradas = Convert.ToDecimal(item.exoneradas),
                            //descuentoGlobal = Convert.ToDecimal(item.descuentoGlobal),
                            //totalVenta = Convert.ToDecimal(item.totalVenta),
                            //totalIgv = Convert.ToDecimal(item.totalIgv),
                            //totalIsc = Convert.ToDecimal(item.totalIsc),
                            //totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            //calculoIgv = Convert.ToDecimal(item.calculoIgv),
                            //calculoIsc = Convert.ToDecimal(item.calculoIsc),
                            //calculoDetraccion = Convert.ToDecimal(item.calculoDetraccion),
                            //EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            //EstadoAnulacion = Convert.ToInt32(item.EstadoAnulacion),
                            //EAnulado = item.EAnulado,
                            //EstadoSunat = item.EstadoSunat,
                            ////EstadoSunatInt=item.EstadoFacturacion,
                            //tipoDocRef = item.tipoDocRef,
                            //numDocRef = item.numDocRef,
                            //codigoMotivoRef = item.codigoMotivoRef,
                            //desMotivoRef = item.desMotivoRef,
                            //Mensaje = item.Mensaje,
                            //doc_icod_documento = Convert.ToInt32(item.doc_icod_documento)
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
        public int insertarfacturaVentaElectronica(EFacturaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                             ref intIcod,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                             oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.EstadoAnulacion,
                            oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago

                            );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEP_ACTUALIZAR_CONTRATO(contrato);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int modificarfacturaVentaElectronica(EFacturaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                            oBe.IdCabecera,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.EstadoAnulacion,
                                                        oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarBoletaVentaElectronica(EBoletaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                             ref intIcod,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.EstadoAnulacion,
                            oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int modificarBoletaVentaElectronica(EBoletaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                            oBe.IdCabecera,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.EstadoAnulacion,
                                                        oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaCreditoVentaElectronica(ENotaCredito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                            ref intIcod,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                             oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.EstadoAnulacion,
                                                        oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago
                       );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ObtenerExistenciaSerie(string serie)
        {
            bool? existencia = false;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_OBTENER_EXISTENCIA_SERIE_CONTRATO(
                        serie,
                        ref existencia
                        );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Convert.ToBoolean(existencia);
        }

        public int modificarNotaCreditoVentaElectronica(ENotaCredito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                            oBe.IdCabecera,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.EstadoAnulacion,
                                                        oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago
                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaCreditoVentaNoComercialElectronica(ENotaCredito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                       ref intIcod,
                           oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                             oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.EstadoAnulacion,
                                                        oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago
                       );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaDebitoVentaElectronica(ENotaDebito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    //dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                    //   ref intIcod,
                    //   //oBe.facv_vnumero_fac_venta,
                    //   oBe.ndebc_vnumero_debito.Remove(4, 8) + '-' + oBe.ndebc_vnumero_debito.Remove(0, 4),
                    //   "08",
                    //   oBe.nroDocumentoEmisior,
                    //   "6",
                    //   oBe.nombreLegalEmisor,
                    //   oBe.nombreComercialEmisor,
                    //   "140124",
                    //   oBe.direccionEmisor,
                    //   "-",
                    //   "LIMA",
                    //   "LIMA",
                    //   "SURQUILLO",
                    //   oBe.nroDocumentoReceptor,
                    //   "6",
                    //   oBe.nombreLegalReceptor,
                    //   oBe.nombreComercialReceptor,
                    //   " ",
                    //   oBe.direccionReceptor,
                    //   " ",
                    //   " ",
                    //   " ",
                    //   " ",
                    //   oBe.ndebc_sfecha_debito.ToString(),
                    //   "PEN",
                    //   "01",
                    //   Convert.ToDecimal(oBe.ndebc_nmonto_neto),
                    //   Convert.ToDecimal(0.00),
                    //   Convert.ToDecimal(0.00),
                    //   Convert.ToDecimal(0.00),
                    //   Convert.ToDecimal(0.00),
                    //   Convert.ToDecimal(oBe.ndebc_nmonto_total),
                    //   Convert.ToDecimal(oBe.ndebc_nmonto_neto) - Convert.ToDecimal(oBe.ndebc_nmonto_total),
                    //   Convert.ToDecimal(0.00),
                    //   0,
                    //   Convert.ToDecimal(oBe.ndebc_npor_imp_igv),
                    //   Convert.ToDecimal(0.00),
                    //   Convert.ToDecimal(0.00),
                    //   4,
                    //   0,
                    //   "03",
                    //   oBe.ndebc_vnumero_documento,
                    //   "01",
                    //   "DES",
                    //   oBe.doc_icod_documento,
                    //   ""
                    //   );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int modificarNotaDebitoVentaElectronica(ENotaDebito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    //dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                    //        oBe.IdCabezera,
                    //        oBe.ndebc_vnumero_debito.Remove(4, 8) + '-' + oBe.ndebc_vnumero_debito.Remove(0, 4),
                    //        "08",
                    //        oBe.nroDocumentoEmisior,
                    //        "6",
                    //        oBe.nombreLegalEmisor,
                    //        oBe.nombreComercialEmisor,
                    //        "140124",
                    //        oBe.direccionEmisor,
                    //        "-",
                    //        "LIMA",
                    //        "LIMA",
                    //        "SURQUILLO",
                    //        oBe.nroDocumentoReceptor,
                    //        "6",
                    //        oBe.nombreLegalReceptor,
                    //        oBe.nombreComercialReceptor,
                    //        " ",
                    //        oBe.direccionReceptor,
                    //        " ",
                    //        " ",
                    //        " ",
                    //        " ",
                    //        oBe.ndebc_sfecha_debito.ToString(),
                    //        "",
                    //        "01",
                    //        Convert.ToDecimal(oBe.ndebc_nmonto_neto),
                    //        Convert.ToDecimal(0.00),
                    //        Convert.ToDecimal(0.00),
                    //        Convert.ToDecimal(0.00),
                    //        Convert.ToDecimal(0.00),
                    //        Convert.ToDecimal(oBe.ndebc_nmonto_total),
                    //        Convert.ToDecimal(oBe.ndebc_nmonto_neto) - Convert.ToDecimal(oBe.ndebc_nmonto_total),
                    //        Convert.ToDecimal(0.00),
                    //        0,
                    //        Convert.ToDecimal(oBe.ndebc_npor_imp_igv),
                    //        Convert.ToDecimal(0.00),
                    //        Convert.ToDecimal(0.00),
                    //        4,
                    //        0,
                    //        "",
                    //        "",
                    //        null,
                    //        "",
                    //        ""
                    //        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarFacturaVentaElectronica(int icodCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ELIMINAR(icodCabecera);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarFacturaElectronicaResponse(EFacturaElectronicaResponse obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_RESPONSE_INSERTAR(
                         ref intIcod,
                         obj.IdCabezera,
                         obj.ProcesoGenerar,
                         obj.ProcesoFirmar,
                         obj.ProcesoEnviar,
                         obj.NombreArchivo,
                         obj.CodigoRespuesta,
                         obj.MensajeRespuesta,
                         obj.MensajeError,
                         obj.NroTicketCdr,
                         obj.TramaZipCdr,
                         obj.Exito
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void modificarFacturaElectronicaResponse(int idCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_RESPONSE_MODIFICAR(
                        idCabecera
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarFacturaElectronicaResponseAnular(EFacturaElectronicaResponse obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_RESPONSE_INSERTAR_ANULAR(
                         ref intIcod,
                         obj.IdCabezera,
                         obj.ProcesoGenerar,
                         obj.ProcesoFirmar,
                         obj.ProcesoEnviar,
                         obj.NombreArchivo,
                         obj.CodigoRespuesta,
                         obj.MensajeRespuesta,
                         obj.MensajeError,
                         obj.NroTicketCdr,
                         obj.TramaZipCdr,
                         obj.Exito
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void modificarFacturaElectronicaResponseAnular(int idCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_RESPONSE_MODIFICAR_ANULAR(
                        idCabecera
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarFacturaElectronicaEstado(int id, int estadoSunat)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ESTADO(
                        id,
                        estadoSunat
                      );
                }
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ESTADO_ANULADO(
                        id,
                        estadoSunat
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarfacturaVentaElectronicaAnulado(EFacturaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                           ref intIcod,
                            oBe.idDocumento.Remove(4, 8) + '-' + oBe.idDocumento.Remove(0, 4),
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.EstadoAnulacion,
                                                        oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.NroCuota,
                            oBe.MontoCuota,
                            oBe.FechaPago

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<ESunatResumenDet> listarSunatResumenDet()
        {
            List<ESunatResumenDet> lista = new List<ESunatResumenDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_RESUMEN_DET_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatResumenDet()
                        {
                            IdItems = item.IdItems,
                            IdCabecera = Convert.ToInt32(item.IdCabecera),
                            IdDocumento = item.IdDocumento,
                            TipoDocumentoReceptor = item.TipoDocumentoReceptor,
                            NroDocumentoReceptor = item.NroDocumentoReceptor,
                            CodigoEstadoItem = Convert.ToInt32(item.CodigoEstadoItem),
                            DocumentoRelacionado = item.DocumentoRelacionado,
                            TipoDocumentoRelacionado = item.TipoDocumentoRelacionado,
                            CorrelativoInicio = Convert.ToInt32(item.CorrelativoInicio),
                            CorrelativoFin = Convert.ToInt32(item.CorrelativoFin),
                            Moneda = item.Moneda,
                            TotalVenta = Convert.ToDecimal(item.TotalVenta),
                            TotalDescuentos = Convert.ToDecimal(item.TotalDescuentos),
                            TotalIgv = Convert.ToDecimal(item.TotalIgv),
                            TotalIsc = Convert.ToDecimal(item.TotalIsc),
                            TotalOtrosImpuestos = Convert.ToDecimal(item.TotalOtrosImpuestos),
                            Gravadas = Convert.ToDecimal(item.Gravadas),
                            Exoneradas = Convert.ToDecimal(item.Exoneradas),
                            Inafectas = Convert.ToDecimal(item.Inafectas),
                            Exportacion = Convert.ToDecimal(item.Exportacion),
                            Gratuitas = Convert.ToDecimal(item.Gratuitas),
                            Id = Convert.ToInt32(item.Id),
                            TipoDocumento = item.TipoDocumento,
                            Serie = item.Serie
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
        public int insertarSunatResumenDet(ESunatResumenDet obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DET_INSERTAR(
                        ref intIcod,
                        obj.IdCabecera,
                        obj.IdDocumento,
                        obj.TipoDocumentoReceptor,
                        obj.NroDocumentoReceptor,
                        obj.CodigoEstadoItem,
                        obj.DocumentoRelacionado,
                        obj.TipoDocumentoRelacionado,
                        obj.CorrelativoInicio,
                        obj.CorrelativoFin,
                        obj.Moneda,
                        obj.TotalVenta,
                        obj.TotalDescuentos,
                        obj.TotalIgv,
                        obj.TotalIsc,
                        obj.TotalOtrosImpuestos,
                        obj.Gravadas,
                        obj.Exoneradas,
                        obj.Inafectas,
                        obj.Exportacion,
                        obj.Gratuitas,
                        obj.Id,
                        obj.TipoDocumento,
                        obj.Serie
                      );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarResumenDiariaResponse(EResumenResponse obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RESUEMEN_RESPONSE_INSERTAR(
                         ref intIcod,
                         obj.IdItems,
                         obj.NroTicket,
                         obj.NombreArchivo,
                         obj.Exito,
                         obj.MensajeError,
                         obj.Pila,
                         obj.CodigoRespuesta,
                         obj.ProcesoFirmar,
                         obj.ProcesoEnviar,
                         obj.ProcesoGenerar
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ESunatDocumentosBaja> listarSunatDocumentosBajaCab(DateTime fechaInicio)
        {
            List<ESunatDocumentosBaja> lista = new List<ESunatDocumentosBaja>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_DOCUMENTOS_BAJA_LISTAR(fechaInicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatDocumentosBaja()
                        {
                            IdCabecera = item.IdCabecera,
                            Id = Convert.ToInt32(item.Id),
                            FechaEmision = DateTime.ParseExact(item.FechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            FEmisionPresentacion = DateTime.ParseExact(item.FechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            TipoDocumento = item.TipoDocumento,
                            StrTipoDoc = item.StrTipoDoc,
                            Serie = item.Serie,
                            Correlativo = item.Correlativo,
                            MotivoBaja = item.MotivoBaja,
                            contador = Convert.ToInt32(item.contador),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            EstadoSunat = item.EstadoSunat,
                            Mensaje = item.Mensaje
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
        public int insertarSunatDocumentosBajaCab(ESunatDocumentosBaja obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_DOCUMENTOS_BAJA_INSERTAR(
                         ref intIcod,
                         obj.Id,
                         obj.FechaEmision,
                         obj.TipoDocumento,
                         obj.Serie,
                         obj.Correlativo,
                         obj.MotivoBaja,
                         obj.contador,
                         obj.EstadoFacturacion
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int insertarDocumentosBajaResponse(ESunatDocumentosBajaResponse obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_DOCUMENTOS_BAJA_RESPONSE_INSERTAR(
                         ref intIcod,
                         obj.IdCabecera,
                         obj.IdItems,
                         obj.NroTicket,
                         obj.NombreArchivo,
                         obj.Exito,
                         obj.MensajeError,
                         obj.Pila,
                         obj.CodigoRespuesta,
                         obj.ProcesoFirmar,
                         obj.ProcesoEnviar,
                         obj.ProcesoGenerar
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void actualizarDocumentosBajaResponse(int id, int estadoSunat)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_DOCUMENTOS_BAJA_ESTADO(
                        id,
                        estadoSunat
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Factura Venta Detalle Electronico
        public List<EFacturaVentaDetalleElectronico> listarfacturaVentaElectronicaDetalle(int facvd_icod_fac_venta)
        {
            List<EFacturaVentaDetalleElectronico> lista = new List<EFacturaVentaDetalleElectronico>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_LISTAR(facvd_icod_fac_venta);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaDetalleElectronico()
                        {
                            IdItems = item.IdItems,
                            IdCabezera = Convert.ToInt32(item.IdCabezera),
                            NumeroOrdenItem = Convert.ToInt32(item.NumeroOrdenItem),
                            cantidad = Convert.ToInt32(item.cantidad),
                            unidadMedida = item.unidadMedida,
                            ValorVentaItem = Convert.ToDecimal(item.ValorVentaItem),
                            CodMotivoDescuentoItem = Convert.ToInt32(item.CodMotivoDescuentoItem),
                            FactorDescuentoItem = Convert.ToDecimal(item.FactorDescuentoItem),
                            DescuentoItem = Convert.ToDecimal(item.DescuentoItem),
                            BaseDescuentotem = Convert.ToDecimal(item.BaseDescuentotem),
                            CodMotivoCargoItem = Convert.ToInt32(item.CodMotivoCargoItem),
                            FactorCargoItem = Convert.ToDecimal(item.FactorCargoItem),
                            MontoCargoItem = Convert.ToDecimal(item.MontoCargoItem),
                            BaseCargoItem = Convert.ToDecimal(item.BaseCargoItem),
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.MontoTotalImpuestosItem),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.MontoImpuestoIgvItem),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.MontoAfectoImpuestoIgv),
                            PorcentajeIGVItem = Convert.ToDecimal(item.PorcentajeIGVItem),
                            MontoInafectoItem = Convert.ToDecimal(item.MontoInafectoItem),
                            MontoImpuestoISCItem = Convert.ToDecimal(item.MontoImpuestoISCItem),
                            MontoAfectoImpuestoIsc = Convert.ToDecimal(item.MontoAfectoImpuestoIsc),
                            PorcentajeISCtem = Convert.ToDecimal(item.PorcentajeISCtem),
                            descripcion = item.descripcion,
                            codigoItem = item.codigoItem,
                            ObservacionesItem = item.ObservacionesItem,
                            ValorUnitarioItem = Convert.ToDecimal(item.ValorUnitarioItem),
                            PrecioVentaUnitarioItem = Convert.ToDecimal(item.PrecioVentaUnitarioItem),
                            tipoImpuesto = item.tipoImpuesto
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
        public int insertarfacturaVentaElectronicaDetalle(EFacturaDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                            ref intIcod,
                                oBe.IdCabezera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto
                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarBoletaVentaElectronicaDetalle(EBoletaDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                                ref intIcod,
                                oBe.IdCabezera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto
                               );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaCreditoVentaElectronicaDetalle(ENotaCreditoDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                                ref intIcod,
                                oBe.IdCabezera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto
                                              );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaCreditoVentaNoComercialElectronicaDetalle(ENotaCreditoDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                                              ref intIcod,
                                                oBe.IdCabezera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto
                                              );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaDebitoVentaElectronicaDetalle(ENotaDebitoDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    //dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                    //                          ref intIcod,
                    //                          oBe.ndebc_icod_debito,
                    //                          oBe.ddebc_inro_item,
                    //                          //if (Convert.ToInt32(oBe.facvd_ncantidad) == 1)
                    //                          //{
                    //                          Convert.ToDecimal("0.0" + Convert.ToInt32(oBe.ddebc_ncantidad_producto)),
                    //                          //}
                    //                          //else
                    //                          //{
                    //                          //Convert.ToDecimal("0." + Convert.ToInt32(oBe.facvd_ncantidad)),
                    //                          //}                         
                    //                          "ZZ",
                    //                          "SERV" + (String.Format("{0:000}", Convert.ToInt32(oBe.ddebc_inro_item))),
                    //                          oBe.ddebc_vdescripcion,
                    //                          oBe.ddebc_nmonto_unitario,
                    //                          oBe.ddebc_nmonto_total,
                    //                          "01",
                    //                          "10",
                    //                          Convert.ToDecimal(0.00),
                    //                          Convert.ToDecimal(0.00),
                    //                          Convert.ToDecimal(0.00),
                    //                          Convert.ToDecimal(0.00),
                    //                          "",
                    //                          oBe.ddebc_nmonto_total,
                    //                          oBe.ddebc_nmonto_total,
                    //                          ""
                    //                          );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_ELIMINAR(icodCabecera);
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
            List<EFacturaCab> lista = new List<EFacturaCab>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_CAB_GET(id_factura);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCab()
                        {
                            favc_icod_factura = item.favc_icod_factura,
                            favc_vnumero_factura = item.favc_vnumero_factura,
                            favc_sfecha_factura = item.favc_sfecha_factura,
                            favc_sfecha_vencim_factura = item.favc_sfecha_vencim_factura,
                            cliec_icod_cliente = item.favc_icod_cliente,
                            clic_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            favc_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            favc_vruc = item.cliec_cruc,
                            cliec_vnumero_doc_cli = item.cliec_vnumero_doc_cli,
                            pdvc_icod_pedido = Convert.ToInt32(item.pdvc_icod_pedido),
                            pdvc_numero_pedido = item.pdvc_numero_pedido,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = item.tablc_iid_situacion,
                            favc_npor_imp_igv = item.favc_npor_imp_igv,
                            favc_nmonto_neto = item.favc_nmonto_neto,
                            favc_nmonto_imp = item.favc_nmonto_imp,
                            favc_nmonto_total = item.favc_nmonto_total,
                            doxcc_icod_correlativo = Convert.ToInt32(item.doxcc_icod_correlativo),
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            favc_vobservacion = item.favc_vobservacion,
                            favc_bincluye_igv = Convert.ToBoolean(item.favc_bincluye_igv),
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            desc_nFactura_porc = Convert.ToDecimal(item.desc_nFactura_porc),
                            desc_nMonto = Convert.ToDecimal(item.desc_nMonto),
                            nmonto_nDescuento = Convert.ToDecimal(item.nmonto_nDescuento),
                            nmonto_nSubTotal = Convert.ToDecimal(item.nmonto_nSubTotal),
                            doc_icod_documento = item.favc_icod_factura,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            facv_nmonto_credito = Convert.ToDecimal(item.facv_nmonto_credito),
                            facv_nmonto_1era_cuota = Convert.ToDecimal(item.facv_nmonto_1era_cuota),
                            facv_sfecha_pago_1era_cuota = Convert.ToDateTime(item.facv_sfecha_pago_1era_cuota)

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
        public List<EFacturaCab> listarFacturaCab(int intEjericio)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCab()
                        {
                            favc_icod_factura = item.favc_icod_factura,
                            favc_vnumero_factura = item.favc_vnumero_factura,
                            favc_sfecha_factura = item.favc_sfecha_factura,
                            favc_sfecha_vencim_factura = item.favc_sfecha_vencim_factura,
                            favc_icod_cliente = item.favc_icod_cliente,
                            clic_vcod_cliente = item.cliec_vcod_cliente,
                            favc_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            favc_vruc = item.cliec_cruc,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = item.tablc_iid_situacion,
                            favc_npor_imp_igv = item.favc_npor_imp_igv,
                            favc_nmonto_neto = item.favc_nmonto_neto,
                            favc_nmonto_imp = item.favc_nmonto_imp,
                            favc_nmonto_total = item.favc_nmonto_total,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            favc_vobservacion = item.favc_vobservacion,
                            favc_bincluye_igv = Convert.ToBoolean(item.favc_bincluye_igv),
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            cliec_nnumero_dias = Convert.ToInt32(item.cliec_nnumero_dias),
                            remic_icod_remision = item.remic_icod_remision,
                            //remic_vnumero_remision = item.remic_vnumero_remision,
                            favc_bafecto_igv = Convert.ToBoolean(item.favc_bafecto_igv),
                            favc_bind_arroz = Convert.ToBoolean(item.favc_bind_arroz),
                            favc_npor_imp_ivap = Convert.ToDecimal(item.favc_npor_imp_ivap),
                            favc_nmonto_ivap = Convert.ToDecimal(item.favc_nmonto_ivap),
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            NomVendedor = item.NomVendedor,
                            favc_nmonto_neto_ivap = Convert.ToDecimal(item.favc_nmonto_neto_ivap),
                            favc_nmonto_neto_exo = Convert.ToDecimal(item.favc_nmonto_neto_exo),
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_icod_contrato_cuotas = Convert.ToInt32(item.cntc_icod_contrato_cuotas),
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            facv_nmonto_credito = Convert.ToDecimal(item.facv_nmonto_credito),
                            facv_nmonto_1era_cuota = Convert.ToDecimal(item.facv_nmonto_1era_cuota),
                            facv_sfecha_pago_1era_cuota = Convert.ToDateTime(item.facv_sfecha_pago_1era_cuota)
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
        public int insertarFactura(EFacturaCab ob)
        {
            int? id_factura = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_INSERTAR(
                            ref id_factura,
                            ob.favc_vnumero_factura,
                            ob.favc_sfecha_factura,
                            ob.favc_sfecha_vencim_factura,
                            ob.favc_icod_cliente,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.tablc_iid_situacion,
                            ob.favc_npor_imp_igv,
                            ob.favc_nmonto_neto,
                            ob.favc_nmonto_imp,
                            ob.favc_nmonto_total,
                            ob.doxcc_icod_correlativo,
                            ob.favc_vobservacion,
                            ob.favc_bincluye_igv,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_icod_remision,
                            ob.favc_bafecto_igv,
                            ob.favc_bind_arroz,
                            ob.favc_npor_imp_ivap,
                            ob.favc_nmonto_ivap,
                            ob.vendc_icod_vendedor,
                            ob.favc_nmonto_neto_ivap,
                            ob.favc_nmonto_neto_exo,
                            ob.cntc_icod_contrato,
                            ob.cntc_icod_contrato_cuotas,
                            ob.facv_nmonto_credito,
                            ob.facv_nmonto_1era_cuota,
                            ob.facv_sfecha_pago_1era_cuota
                            );
                }
                return Convert.ToInt32(id_factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFactura(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_MODIFICAR(
                            ob.favc_icod_factura,
                            ob.favc_vnumero_factura,
                            ob.favc_sfecha_factura,
                            ob.favc_sfecha_vencim_factura,
                            ob.favc_icod_cliente,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.tablc_iid_situacion,
                            ob.favc_npor_imp_igv,
                            ob.favc_nmonto_neto,
                            ob.favc_nmonto_imp,
                            ob.favc_nmonto_total,
                            ob.favc_vobservacion,
                            ob.favc_bincluye_igv,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_icod_remision,
                            ob.favc_bafecto_igv,
                            ob.favc_bind_arroz,
                            ob.favc_npor_imp_ivap,
                            ob.favc_nmonto_ivap,
                            ob.vendc_icod_vendedor,
                            ob.favc_nmonto_neto_ivap,
                            ob.favc_nmonto_neto_exo,
                            ob.cntc_icod_contrato,
                            ob.cntc_icod_contrato_cuotas,
                            ob.facv_nmonto_credito,
                            ob.facv_nmonto_1era_cuota,
                            ob.facv_sfecha_pago_1era_cuota
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFactura(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_ELIMINAR(
                            ob.favc_icod_factura,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void anularFactura(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_ANULAR(
                            ob.favc_icod_factura,
                            ob.intUsuario,
                            ob.strPc
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EFacturaDet> listarFacturaDetalle(int intFactura, int aNIO)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_DET_LISTAR(intFactura, aNIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaDet()
                        {
                            favd_icod_item_factura = item.favd_icod_item_factura,
                            favc_icod_factura = item.favc_icod_factura,
                            favd_iitem_factura = Convert.ToInt32(item.favd_iitem_factura),
                            favd_ncantidad = item.favd_ncantidad,
                            favd_vncantidad = item.favd_ncantidad.ToString(),
                            favd_vdescripcion = item.favd_vdescripcion,
                            favd_nprecio_unitario_item = Convert.ToDecimal(item.favd_nprecio_unitario_item),
                            favd_vnprecio_unitario_item = item.favd_nprecio_unitario_item.ToString(),
                            favd_nmonto_impuesto_item = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            favd_nporcentaje_descuento_item = Convert.ToDecimal(item.favd_nporcentaje_descuento_item),
                            favd_nprecio_total_item = Convert.ToDecimal(item.favd_nprecio_total_item),
                            favd_vnprecio_total_item = item.favd_nprecio_total_item.ToString(),
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            //strCategoria = item.strLinea,
                            //strSubCategoriaUno = item.strSubLinea,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda,
                            dblStockDisponible = item.dblStockDisponible,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            //prdc_vpart_number=item.prdc_vpart_number,
                            favd_nobservaciones = item.favd_nobservaciones,
                            OrdenItemImprimir = 1,
                            tablc_iid_tipo_venta = Convert.ToInt32(item.tablc_iid_tipo_venta),
                            strTipoVenta = item.StrTipoVenta,
                            favd_npor_imp_arroz = Convert.ToDecimal(item.favd_npor_imp_arroz),
                            favd_nmonto_imp_arroz = Convert.ToDecimal(item.favd_nmonto_imp_arroz),
                            favd_nneto_ivap = Convert.ToDecimal(item.favd_nneto_ivap),
                            favd_nneto_igv = Convert.ToDecimal(item.favd_nneto_igv),
                            favd_nneto_exo = Convert.ToDecimal(item.favd_nneto_exo),
                            prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv)


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

        public List<EFacturaDet> listarFacturaDetalle_NTC(int intFactura, int aNIO)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_DET_LISTAR_NTC(intFactura, aNIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaDet()
                        {
                            favd_icod_item_factura = item.favd_icod_item_factura,
                            favc_icod_factura = item.favc_icod_factura,
                            favd_iitem_factura = Convert.ToInt32(item.favd_iitem_factura),
                            favd_ncantidad = item.favd_ncantidad,
                            favd_vncantidad = item.favd_ncantidad.ToString(),
                            favd_vdescripcion = item.favd_vdescripcion,
                            favd_nprecio_unitario_item = Convert.ToDecimal(item.favd_nprecio_unitario_item),
                            favd_vnprecio_unitario_item = item.favd_nprecio_unitario_item.ToString(),
                            favd_nmonto_impuesto_item = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            favd_nporcentaje_descuento_item = Convert.ToDecimal(item.favd_nporcentaje_descuento_item),
                            favd_nprecio_total_item = Convert.ToDecimal(item.favd_nprecio_total_item),
                            favd_vnprecio_total_item = item.favd_nprecio_total_item.ToString(),
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            //strCategoria = item.strLinea,
                            //strSubCategoriaUno = item.strSubLinea,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda,
                            dblStockDisponible = item.dblStockDisponible,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            //prdc_vpart_number=item.prdc_vpart_number,
                            favd_nobservaciones = item.favd_nobservaciones,
                            OrdenItemImprimir = 1,
                            tablc_iid_tipo_venta = Convert.ToInt32(item.tablc_iid_tipo_venta),
                            strTipoVenta = item.StrTipoVenta,
                            favd_npor_imp_arroz = Convert.ToDecimal(item.favd_npor_imp_arroz),
                            favd_nmonto_imp_arroz = Convert.ToDecimal(item.favd_nmonto_imp_arroz),
                            favd_nneto_ivap = Convert.ToDecimal(item.favd_nneto_ivap),
                            favd_nneto_igv = Convert.ToDecimal(item.favd_nneto_igv),
                            favd_nneto_exo = Convert.ToDecimal(item.favd_nneto_exo),
                            prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv)


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


        public List<EFacturaDet> listarFacturaServicioDetalle(int intFactura, int aNIO)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_DET_LISTAR_SERVICIO(intFactura);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaDet()
                        {
                            favd_icod_item_factura = item.favd_icod_item_factura,
                            favc_icod_factura = item.favc_icod_factura,
                            favd_iitem_factura = Convert.ToInt32(item.favd_iitem_factura),
                            favd_ncantidad = item.favd_ncantidad,
                            favd_vncantidad = item.favd_ncantidad.ToString(),
                            favd_vdescripcion = item.favd_vdescripcion,
                            favd_nprecio_unitario_item = Convert.ToDecimal(item.favd_nprecio_unitario_item),
                            favd_vnprecio_unitario_item = item.favd_nprecio_unitario_item.ToString(),
                            favd_nprecio_total_item = Convert.ToDecimal(item.favd_nprecio_total_item),
                            favd_vnprecio_total_item = item.favd_nprecio_total_item.ToString(),
                            //strCategoria = item.strLinea,
                            //strSubCategoriaUno = item.strSubLinea,
                            //strDesUM = item.strDesUM,
                            //prdc_vpart_number=item.prdc_vpart_number,
                            favd_nobservaciones = item.favd_nobservaciones,
                            OrdenItemImprimir = 1,
                            // AfectoIVAP = Convert.ToBoolean(item.AfectoIVAP),
                            // favd_nneto_ivap = Convert.ToDecimal(item.favd_nneto_ivap),
                            // favd_nneto_igv = Convert.ToDecimal(item.favd_nneto_igv),
                            // favd_nneto_exo = Convert.ToDecimal(item.favd_nneto_exo),
                            //prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            //prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv)


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

        public void insertarFacturaDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_INSERTAR(
                            ob.favc_icod_factura,
                            ob.favd_iitem_factura,
                            ob.almac_icod_almacen,
                            ob.prdc_icod_producto,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nporcentaje_descuento_item,
                            ob.favd_nprecio_total_item,
                            ob.favd_npor_imp_arroz,
                            ob.favd_nmonto_imp_arroz,
                            ob.favd_nobservaciones,
                            ob.intUsuario,
                            ob.strPc,
                            ob.kardc_icod_correlativo,
                            ob.tablc_iid_tipo_venta,
                            ob.favd_nneto_ivap,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarFacturaServicioDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_INSERTAR_SERVICIOS(
                            ob.favc_icod_factura,
                            ob.favd_iitem_factura,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nprecio_total_item,
                            ob.favd_nobservaciones,
                            ob.intUsuario,
                            ob.strPc,
                            ob.favd_nneto_ivap,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_MODIFICAR(
                            ob.favd_icod_item_factura,
                            ob.favd_iitem_factura,
                            ob.almac_icod_almacen,
                            ob.prdc_icod_producto,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nporcentaje_descuento_item,
                            ob.favd_nprecio_total_item,
                            ob.favd_npor_imp_arroz,
                            ob.favd_nmonto_imp_arroz,
                            ob.favd_nobservaciones,
                            ob.intUsuario,
                            ob.strPc,
                            ob.kardc_icod_correlativo,
                            ob.tablc_iid_tipo_venta,
                            ob.favd_nneto_ivap,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo

                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaServicioDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_MODIFICAR_SERVICIOS(
                            ob.favd_icod_item_factura,
                            ob.favd_iitem_factura,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nprecio_total_item,
                            ob.favd_nobservaciones,
                            ob.intUsuario,
                            ob.strPc,
                            ob.favd_nneto_ivap,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo

                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_ELIMINAR(
                            ob.favd_icod_item_factura,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaServicioDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_ELIMINAR_SERVICIOS(
                            ob.favd_icod_item_factura,
                            ob.intUsuario,
                            ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_FACTURA_VENTA(favc_icod_factura, favc_bafecto_igv);
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
            List<EBoletaCab> lista = new List<EBoletaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_BOLETA_CAB_GET(id_boleta);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaCab()
                        {
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovc_vnumero_boleta = item.bovc_vnumero_boleta,
                            bovc_sfecha_boleta = item.bovc_sfecha_boleta,
                            bovc_sfecha_vencim_boleta = item.bovc_sfecha_vencim_boleta,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            cliec_cruc = item.cliec_cruc,
                            cliec_vnumero_doc_cli = item.cliec_vnumero_doc_cli,
                            pdvc_icod_pedido = Convert.ToInt32(item.pdvc_icod_pedido),
                            pdvc_numero_pedido = item.pdvc_numero_pedido,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            bovc_npor_imp_igv = item.bovc_npor_imp_igv,
                            bovc_nmonto_neto = item.bovc_nmonto_neto,
                            bovc_nmonto_imp = item.bovc_nmonto_imp,
                            bovc_nmonto_total = item.bovc_nmonto_total,
                            cliec_vnombre_cliente = item.strDesCliente,
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            bovc_vobservacion = item.bovc_vobservacion,
                            bovc_vnombre_cliente = item.bovc_vnombre_cliente,
                            bovc_vdireccion_cliente = item.bovc_vdireccion_cliente,
                            bovc_vDNI_cliente = item.bovc_vDNI_cliente,
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            desc_nFactura_porc = Convert.ToDecimal(item.desc_nFactura_porc),
                            desc_nMonto = Convert.ToDecimal(item.desc_nMonto),
                            nmonto_nDescuento = Convert.ToDecimal(item.nmonto_nDescuento),
                            nmonto_nSubTotal = Convert.ToDecimal(item.nmonto_nSubTotal),
                            doc_icod_documento = item.bovc_icod_boleta,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato
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
        public List<EBoletaCab> listarBoletaCab(int intEjericio)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_BOLETA_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaCab()
                        {
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovc_vnumero_boleta = item.bovc_vnumero_boleta,
                            bovc_sfecha_boleta = item.bovc_sfecha_boleta,
                            bovc_sfecha_vencim_boleta = item.bovc_sfecha_vencim_boleta,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            cliec_cruc = item.cliec_cruc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            bovc_npor_imp_igv = item.bovc_npor_imp_igv,
                            bovc_nmonto_neto = item.bovc_nmonto_neto,
                            bovc_nmonto_imp = item.bovc_nmonto_imp,
                            bovc_nmonto_total = item.bovc_nmonto_total,
                            cliec_vnombre_cliente = item.strDesCliente,
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            bovc_vobservacion = item.bovc_vobservacion,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            cliec_nnumero_dias = Convert.ToInt32(item.cliec_nnumero_dias),
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            bovc_vnombre_cliente = item.bovc_vnombre_cliente,
                            bfavc_bafecto_igv = Convert.ToBoolean(item.bfavc_bafecto_igv),
                            bovc_bind_arroz = Convert.ToBoolean(item.bovc_bind_arroz),
                            bovc_npor_imp_ivap = Convert.ToDecimal(item.bovc_npor_imp_ivap),
                            bovc_nmonto_ivap = Convert.ToDecimal(item.bovc_nmonto_ivap),
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            NomVendedor = item.NomVendedor,
                            bovc_nmonto_neto_ivap = Convert.ToDecimal(item.bovc_nmonto_neto_ivap),
                            bovc_nmonto_neto_exo = Convert.ToDecimal(item.bovc_nmonto_neto_exo),
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_icod_contrato_cuotas = Convert.ToInt32(item.cntc_icod_contrato_cuotas),
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas)
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
        public int insertarBoleta(EBoletaCab ob)
        {
            int? bovc_icod_boleta = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_INSERTAR(
                            ref bovc_icod_boleta,
                            ob.bovc_vnumero_boleta,
                            ob.bovc_sfecha_boleta,
                            ob.bovc_sfecha_vencim_boleta,
                            ob.doxcc_icod_correlativo,
                            ob.cliec_icod_cliente,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.bovc_npor_imp_igv,
                            ob.bovc_nmonto_neto,
                            ob.bovc_nmonto_imp,
                            ob.bovc_nmonto_total,
                            ob.anio,
                            ob.intUsuario,
                            ob.strPc,
                            ob.bovc_vobservacion,
                            ob.remic_icod_remision,
                            ob.bovc_vnombre_cliente,
                            ob.bfavc_bafecto_igv,
                            ob.bovc_bind_arroz,
                            ob.bovc_npor_imp_ivap,
                            ob.bovc_nmonto_ivap,
                            ob.vendc_icod_vendedor,
                            ob.bovc_nmonto_neto_ivap,
                            ob.bovc_nmonto_neto_exo,
                            ob.cntc_icod_contrato,
                            ob.cntc_icod_contrato_cuotas
                            );
                }
                return Convert.ToInt32(bovc_icod_boleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoleta(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_MODIFICAR(
                    ob.bovc_icod_boleta,
                    ob.bovc_vnumero_boleta,
                    ob.bovc_sfecha_boleta,
                    ob.bovc_sfecha_vencim_boleta,
                    ob.cliec_icod_cliente,
                    ob.tablc_iid_tipo_moneda,
                    ob.tablc_iid_forma_pago,
                    ob.bovc_npor_imp_igv,
                    ob.bovc_nmonto_neto,
                    ob.bovc_nmonto_imp,
                    ob.bovc_nmonto_total,
                    ob.intUsuario,
                    ob.strPc,
                    ob.bovc_vobservacion,
                    ob.bovc_vnombre_cliente,
                    ob.bfavc_bafecto_igv,
                    ob.bovc_bind_arroz,
                    ob.bovc_npor_imp_ivap,
                    ob.bovc_nmonto_ivap,
                    ob.vendc_icod_vendedor,
                    ob.bovc_nmonto_neto_ivap,
                    ob.bovc_nmonto_neto_exo,
                    ob.cntc_icod_contrato,
                    ob.cntc_icod_contrato_cuotas
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoleta(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_ELIMINAR(
                            ob.bovc_icod_boleta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularBoleta(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_ANULAR(
                            ob.bovc_icod_boleta,
                            ob.intUsuario,
                            ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_BOLETA_VENTA(bovc_icod_boleta, bfavc_bafecto_igv);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Boleta Detalle
        public List<EBoletaDet> listarBoletaDetalle(int intFactura)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_BOLETA_DET_LISTAR(intFactura);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaDet()
                        {
                            bovd_icod_item_boleta = item.bovd_icod_item_boleta,
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovd_iitem_boleta = Convert.ToInt32(item.bovd_iitem_boleta),
                            bovd_ncantidad = item.bovd_ncantidad,
                            bovd_vdescripcion = item.bovd_vdescripcion,
                            bovd_nprecio_unitario_item = item.bovd_nprecio_unitario_item,
                            bovd_nprecio_total_item = item.bovd_nprecio_total_item,
                            bovd_npor_imp_arroz = Convert.ToDecimal(item.bovd_npor_imp_arroz),
                            bovd_nmonto_imp_arroz = Convert.ToDecimal(item.bovd_nmonto_imp_arroz),
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda,
                            dblStockDisponible = item.dblStockDisponible,
                            kardc_icod_correlativo = Convert.ToInt32(item.kardc_icod_correlativo),
                            //prdc_vpart_number=item.prdc_vpart_number,
                            bolvd_vobservaciones = item.bolvd_vobservaciones,
                            tablc_iid_tipo_venta = Convert.ToInt32(item.tablc_iid_tipo_venta),
                            strTipoVenta = item.StrTipoVenta,
                            AfectoIVAP = Convert.ToBoolean(item.AfectoIVAP),
                            bovd_nneto_ivap = Convert.ToDecimal(item.bovd_nneto_ivap),
                            bovd_nneto_igv = Convert.ToDecimal(item.bovd_nneto_igv),
                            bovd_nneto_exo = Convert.ToDecimal(item.bovd_nneto_exo),
                            prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv),
                            favd_iicod_tipo_pago = Convert.ToInt32(item.favd_iicod_tipo_pago),
                            strTipoServicio = item.strTipoServicio
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


        public List<EBoletaDet> listarBoletaDetalleNTC(int intFactura)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_BOLETA_DET_LISTAR_NTC(intFactura);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaDet()
                        {
                            bovd_icod_item_boleta = item.bovd_icod_item_boleta,
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovd_iitem_boleta = Convert.ToInt32(item.bovd_iitem_boleta),
                            bovd_ncantidad = item.bovd_ncantidad,
                            bovd_vdescripcion = item.bovd_vdescripcion,
                            bovd_nprecio_unitario_item = item.bovd_nprecio_unitario_item,
                            bovd_nprecio_total_item = item.bovd_nprecio_total_item,
                            bovd_npor_imp_arroz = Convert.ToDecimal(item.bovd_npor_imp_arroz),
                            bovd_nmonto_imp_arroz = Convert.ToDecimal(item.bovd_nmonto_imp_arroz),
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda,
                            dblStockDisponible = item.dblStockDisponible,
                            kardc_icod_correlativo = Convert.ToInt32(item.kardc_icod_correlativo),
                            //prdc_vpart_number=item.prdc_vpart_number,
                            bolvd_vobservaciones = item.bolvd_vobservaciones,
                            tablc_iid_tipo_venta = Convert.ToInt32(item.tablc_iid_tipo_venta),
                            strTipoVenta = item.StrTipoVenta,
                            AfectoIVAP = Convert.ToBoolean(item.AfectoIVAP),
                            bovd_nneto_ivap = Convert.ToDecimal(item.bovd_nneto_ivap),
                            bovd_nneto_igv = Convert.ToDecimal(item.bovd_nneto_igv),
                            bovd_nneto_exo = Convert.ToDecimal(item.bovd_nneto_exo),
                            prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv),
                            favd_iicod_tipo_pago = Convert.ToInt32(item.favd_iicod_tipo_pago),
                            strTipoServicio = item.strTipoServicio
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

        public void insertarBoletaDetalle(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_DET_INSERTAR(
                     ob.bovc_icod_boleta
                    , ob.bovd_iitem_boleta
                    , ob.almac_icod_almacen
                    , ob.prdc_icod_producto
                    , ob.bovd_ncantidad
                    , ob.bovd_vdescripcion
                    , ob.bovd_nprecio_unitario_item
                    , ob.bovd_nmonto_impuesto_item
                    , ob.bovd_nporcentaje_descuento_item
                    , ob.bovd_nprecio_total_item
                    , ob.bovd_npor_imp_arroz
                    , ob.bovd_nmonto_imp_arroz
                    , ob.kardc_icod_correlativo
                    , ob.bolvd_vobservaciones
                    , ob.intUsuario
                    , ob.strPc
                    , ob.bovd_flag_estado
                    , ob.tablc_iid_tipo_venta
                    , ob.bovd_nneto_ivap
                    , ob.bovd_nneto_igv
                    , ob.bovd_nneto_exo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaDetalle(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_DET_MODIFICAR(
                     ob.bovd_icod_item_boleta
                    , ob.bovd_iitem_boleta
                    , ob.bovd_ncantidad
                    , ob.bovd_vdescripcion
                    , ob.bovd_nprecio_unitario_item
                    , ob.bovd_nmonto_impuesto_item
                    , ob.bovd_nporcentaje_descuento_item
                    , ob.bovd_nprecio_total_item
                    , ob.bovd_npor_imp_arroz
                    , ob.bovd_nmonto_imp_arroz
                    , ob.bolvd_vobservaciones
                    , ob.intUsuario
                    , ob.strPc
                    , ob.tablc_iid_tipo_venta
                    , ob.bovd_nneto_ivap
                    , ob.bovd_nneto_igv
                    , ob.bovd_nneto_exo
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaDetalle(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_DET_ELIMINAR(
                            ob.bovd_icod_item_boleta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public int verificarDocVentaPlanilla(int intTipoDoc, int intIcodDoc)
        {
            int? intFlag = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_VERIFICAR_DOC_VENTA_PLANILLA(
                            intTipoDoc,
                            intIcodDoc,
                            ref intFlag
                            );
                }
                return Convert.ToInt32(intFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EOrdenCompra> getOCLCabVerificarNumero(string vnumero, int ANIO)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_OCL_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompra()
                        {
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra

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
        public List<EOrdenCompraServicio> getOCSCabVerificarNumero(string vnumero, int ANIO)
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_OCS_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraServicio()
                        {
                            ocsc_icod_ocs = item.ocsc_icod_ocs

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
        public List<EOrdenCompraImportacion> getOCICabVerificarNumero(string vnumero, int ANIO)
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_OCI_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraImportacion()
                        {
                            ocic_icod_oci = item.ocic_icod_oci

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
        #region Venta Directa
        public void actualizarSituacionVentaDirecta(int intVD, int intSituacion)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_SITUACION(
                        intVD,
                        intSituacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EVentaDirecta> listarVentaDirecta(int intEjericio)
        {
            List<EVentaDirecta> lista = new List<EVentaDirecta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_DOC_VENTA_DIRECTA_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentaDirecta()
                        {
                            dvdc_icod_doc_venta_directa = item.dvdc_icod_doc_venta_directa,
                            dvdc_vnumero_doc_venta_directa = item.dvdc_vnumero_doc_venta_directa,
                            dvdc_sfecha_doc_venta_directa = item.dvdc_sfecha_doc_venta_directa,
                            dvdc_icod_cliente = item.dvdc_icod_cliente,
                            clic_vcod_cliente = item.clic_vcod_cliente,
                            dvdc_vdireccion_cliente = item.dvdc_vdireccion_cliente,
                            dvdc_vruc = item.dvdc_vruc,
                            dvdc_vdni = item.dvdc_vdni,
                            dvdc_iid_vehiculo = item.dvdc_iid_vehiculo,
                            dvdc_vcolor = item.dvdc_vcolor,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            dvdc_npor_imp_igv = item.dvdc_npor_imp_igv,
                            dvdc_nmonto_neto = item.dvdc_nmonto_neto,
                            dvdc_nmonto_imp = item.dvdc_nmonto_imp,
                            dvdc_nmonto_total = item.dvdc_nmonto_total,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            strDesCliente = item.strDesCliente,
                            strPlaca = item.strPlaca,
                            strMarca = item.strMarca,
                            strModelo = item.strModelo,
                            strSituacion = item.strSituacion,
                            intAnioVehiculo = Convert.ToInt32(item.strAnioVehiculo),
                            strMoneda = item.strMoneda
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
        public int insertarVentaDirecta(EVentaDirecta ob)
        {
            int? id_factura = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_INSERTAR(
                            ref id_factura,
                            ob.dvdc_vnumero_doc_venta_directa,
                            ob.dvdc_sfecha_doc_venta_directa,
                            ob.dvdc_icod_cliente,
                            ob.clic_vcod_cliente,
                            ob.dvdc_vdireccion_cliente,
                            ob.dvdc_vruc,
                            ob.dvdc_vdni,
                            ob.dvdc_iid_vehiculo,
                            ob.dvdc_vcolor,
                            ob.tablc_iid_tipo_moneda,
                            ob.dvdc_npor_imp_igv,
                            ob.dvdc_nmonto_neto,
                            ob.dvdc_nmonto_imp,
                            ob.dvdc_nmonto_total,
                            ob.tablc_iid_situacion,
                            ob.intUsuario,
                            ob.strPc);
                }
                return Convert.ToInt32(id_factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirecta(EVentaDirecta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_MODIFICAR(
                            ob.dvdc_icod_doc_venta_directa,
                            ob.dvdc_vnumero_doc_venta_directa,
                            ob.dvdc_sfecha_doc_venta_directa,
                            ob.dvdc_icod_cliente,
                            ob.clic_vcod_cliente,
                            ob.dvdc_vdireccion_cliente,
                            ob.dvdc_vruc,
                            ob.dvdc_vdni,
                            ob.dvdc_iid_vehiculo,
                            ob.dvdc_vcolor,
                            ob.tablc_iid_tipo_moneda,
                            ob.dvdc_npor_imp_igv,
                            ob.dvdc_nmonto_neto,
                            ob.dvdc_nmonto_imp,
                            ob.dvdc_nmonto_total,
                            ob.tablc_iid_situacion,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirecta(EVentaDirecta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_ELIMINAR(
                            ob.dvdc_icod_doc_venta_directa,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVentaDirectaDet> listarVentaDirectaDetalle(int intVentaDirecta)
        {
            List<EVentaDirectaDet> lista = new List<EVentaDirectaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_LISTAR(intVentaDirecta);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentaDirectaDet()
                        {
                            dvdd_iid_icod_doc_venta_directa = item.dvdd_iid_icod_doc_venta_directa,
                            dvdd_iitem_doc_venta_directa = Convert.ToInt32(item.dvdd_iitem_doc_venta_directa),
                            dvdd_iid_almacen = Convert.ToInt32(item.dvdd_iid_almacen),
                            dvdd_iid_producto = Convert.ToInt32(item.dvdd_iid_producto),
                            dvdd_ncantidad = item.dvdd_ncantidad,
                            dvdd_vdescripcion = item.dvdd_vdescripcion,
                            dvdd_bbonificacion = Convert.ToBoolean(item.dvdd_bbonificacion),
                            dvdd_nprecio_unitario_item = item.dvdd_nprecio_unitario_item,
                            dvdd_nmonto_impuesto_item = item.dvdd_nmonto_impuesto_item,
                            dvdd_nprecio_total_item = item.dvdd_nprecio_total_item,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto
                            //strLinea = item.strLinea,
                            //strSubLinea = item.strSubLinea,
                            //strDesUM = item.strDesUM,
                            //strAlmacen = item.strAlmacen,
                            //strMoneda = item.strMoneda,
                            //dblStockDisponible = item.dblStockDisponible,
                            //dvdd_icod_personal = item.dvdd_icod_personal,
                            //dvdd_area_personal = item.dvdd_area_personal,
                            //strPersonal = item.perc_vapellidos_nombres,
                            //dvdd_nporc_productividad = Convert.ToDecimal(item.dvdd_nporc_productividad),
                            //dvdd_nmonto_productividad = Convert.ToDecimal(item.dvdd_nmonto_productividad),
                            //dvdd_clasificacion = Convert.ToInt32(item.dvdd_clasificacion),
                            //dvdd_fecha_servicio = item.dvdd_fecha_servicio
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
        public void insertarVentaDirectaDetalle(EVentaDirectaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_INSERTAR(
                            ob.dvdc_icod_doc_venta_directa,
                            ob.dvdd_iitem_doc_venta_directa,
                            ob.dvdd_iid_almacen,
                            ob.dvdd_iid_producto,
                            ob.dvdd_ncantidad,
                            ob.dvdd_vdescripcion,
                            ob.dvdd_bbonificacion,
                            ob.dvdd_nprecio_unitario_item,
                            ob.dvdd_nmonto_impuesto_item,
                            ob.dvdd_nprecio_total_item,
                            ob.dvdd_icod_personal,
                            ob.dvdd_area_personal,
                            ob.dvdd_clasificacion,
                            ob.dvdd_nporc_productividad,
                            ob.dvdd_nmonto_productividad,
                            ob.dvdd_fecha_servicio,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirectaDetalle(EVentaDirectaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_MODIFICAR(
                            ob.dvdd_iid_icod_doc_venta_directa,
                            ob.dvdc_icod_doc_venta_directa,
                            ob.dvdd_iitem_doc_venta_directa,
                            ob.dvdd_iid_almacen,
                            ob.dvdd_iid_producto,
                            ob.dvdd_ncantidad,
                            ob.dvdd_vdescripcion,
                            ob.dvdd_bbonificacion,
                            ob.dvdd_nprecio_unitario_item,
                            ob.dvdd_nmonto_impuesto_item,
                            ob.dvdd_nprecio_total_item,
                            ob.dvdd_icod_personal,
                            ob.dvdd_area_personal,
                            ob.dvdd_clasificacion,
                            ob.dvdd_nporc_productividad,
                            ob.dvdd_nmonto_productividad,
                            ob.dvdd_fecha_servicio,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirectaDetalle(EVentaDirectaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_ELIMINAR(
                            ob.dvdd_iid_icod_doc_venta_directa,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Pago de Documentos de Venta
        public List<EPagoDocVenta> listarPago(int intTipoDoc, int intIcodDoc)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PAGO_LISTAR(intTipoDoc, intIcodDoc);
                    foreach (var item in query)
                    {
                        lista.Add(new EPagoDocVenta()
                        {
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            tdocc_icoc_tipo_documento = Convert.ToInt32(item.tdocc_icoc_tipo_documento),
                            pgoc_icod_documento = Convert.ToInt32(item.pgoc_icod_documento),
                            pgoc_sfecha_pago = Convert.ToDateTime(item.pgoc_sfecha_pago),
                            pgoc_tipo_pago = Convert.ToInt32(item.pgoc_tipo_pago),
                            pgoc_icod_nota_credito = item.pgoc_icod_nota_credito,
                            pgoc_icod_tipo_tarjeta = item.pgoc_icod_tipo_tarjeta,
                            pgoc_icod_tipo_moneda = Convert.ToInt32(item.pgoc_icod_tipo_moneda),
                            pgoc_descripcion = item.pgoc_descripcion,
                            pgoc_nmonto = Convert.ToDecimal(item.pgoc_nmonto),
                            pgoc_dxc_icod_pago = Convert.ToInt64(item.pgoc_dxc_icod_pago),
                            strTipoPago = item.strTipoPago,
                            pgoc_fecha_venc_credito = item.pgoc_fecha_venc_credito,
                            tblc_iid_banco_cheque = item.tblc_iid_banco_cheque,
                            pgoc_nro_cheque = item.pgoc_nro_cheque,
                            pgoc_fecha_cob_cheque = item.pgoc_fecha_cob_cheque,
                            bcoc_icod_banco = item.bcoc_icod_banco,
                            bcod_icod_banco_cuenta = item.bcod_icod_banco_cuenta,
                            pgoc_icod_anticipo = item.pgoc_icod_anticipo,
                            strNroNotaCredito = item.strNroNotaCredito,
                            strNroAnticipo = item.strNroAnticipo,
                            pgoc_dxc_icod_canje_doc = item.pgoc_dxc_icod_canje_doc,
                            pgoc_iid_tipo_doc_docventa = Convert.ToInt32(item.pgoc_iid_tipo_doc_docventa),
                            pgoc_dxc_icod_doc = Convert.ToInt64(item.pgoc_dxc_icod_doc),
                            pgoc_icod_entidad_finan_mov = item.pgoc_icod_entidad_finan_mov,
                            strTipoMoneda = (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 3) ? "S/." : (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 4) ? "US$" : "",

                            intCtaContableBcoTarjeta = item.intCtaContableBcoTarjeta,
                            intAnaliticaBcoTarjeta = item.intAnaliticaBcoTarjeta,
                            strCodAnaliticaBcoTarjeta = item.strCodAnaliticaBcoTarjeta,
                            tdodc_iid_correlativo_nota_credito = item.tdodc_iid_correlativo_nota_credito,
                            tdodc_iid_correlativo_anticipo = item.tdodc_iid_correlativo_anticipo,

                            intAnaliticaClienteNC = item.intAnaliticaClienteNC,
                            strCodAnaliticaClienteNC = item.strCodAnaliticaClienteNC,
                            intAnaliticaClienteAnticipo = item.intAnaliticaClienteAnticipo,
                            strCodAnaliticaClienteAnticipo = item.strCodAnaliticaClienteAnticipo,
                            pgoc_vreferecia = item.pgoc_vreferecia,
                            pgoc_vnum_operacion = item.pgoc_vnum_operacion
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

        public List<EPagoDocVenta> getDatosPago(int intPago)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PAGO_GET_DATOS(intPago);
                    foreach (var item in query)
                    {
                        lista.Add(new EPagoDocVenta()
                        {
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            tdocc_icoc_tipo_documento = Convert.ToInt32(item.tdocc_icoc_tipo_documento),
                            pgoc_icod_documento = item.pgoc_icod_documento,
                            pgoc_sfecha_pago = Convert.ToDateTime(item.pgoc_sfecha_pago),
                            pgoc_tipo_pago = Convert.ToInt32(item.pgoc_tipo_pago),
                            pgoc_icod_nota_credito = item.pgoc_icod_nota_credito,
                            pgoc_icod_tipo_tarjeta = item.pgoc_icod_tipo_tarjeta,
                            pgoc_icod_tipo_moneda = Convert.ToInt32(item.pgoc_icod_tipo_moneda),
                            pgoc_descripcion = item.pgoc_descripcion,
                            pgoc_nmonto = Convert.ToDecimal(item.pgoc_nmonto),
                            pgoc_dxc_icod_pago = Convert.ToInt64(item.pgoc_dxc_icod_pago),
                            strTipoPago = item.strTipoPago,
                            pgoc_fecha_venc_credito = item.pgoc_fecha_venc_credito,
                            tblc_iid_banco_cheque = item.tblc_iid_banco_cheque,
                            pgoc_nro_cheque = item.pgoc_nro_cheque,
                            pgoc_fecha_cob_cheque = item.pgoc_fecha_cob_cheque,
                            bcoc_icod_banco = item.bcoc_icod_banco,
                            bcod_icod_banco_cuenta = item.bcod_icod_banco_cuenta,
                            pgoc_icod_anticipo = item.pgoc_icod_anticipo,
                            strNroNotaCredito = item.strNroNotaCredito,
                            strNroAnticipo = item.strNroAnticipo,
                            pgoc_dxc_icod_canje_doc = item.pgoc_dxc_icod_canje_doc,
                            pgoc_iid_tipo_doc_docventa = Convert.ToInt32(item.pgoc_iid_tipo_doc_docventa),
                            pgoc_dxc_icod_doc = Convert.ToInt64(item.pgoc_dxc_icod_doc),
                            pgoc_icod_entidad_finan_mov = item.pgoc_icod_entidad_finan_mov,
                            pgoc_icod_cliente = Convert.ToInt32(item.pgoc_icod_cliente),
                            strTipoMoneda = (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 1) ? "S/." : (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 2) ? "US$" : "",

                            intCtaContableBcoTarjeta = item.intCtaContableBcoTarjeta,
                            intAnaliticaBcoTarjeta = item.intAnaliticaBcoTarjeta,
                            strCodAnaliticaBcoTarjeta = item.strCodAnaliticaBcoTarjeta,
                            tdodc_iid_correlativo_nota_credito = item.tdodc_iid_correlativo_nota_credito,
                            tdodc_iid_correlativo_anticipo = item.tdodc_iid_correlativo_anticipo,

                            intAnaliticaClienteNC = item.intAnaliticaClienteNC,
                            strCodAnaliticaClienteNC = item.strCodAnaliticaClienteNC,
                            intAnaliticaClienteAnticipo = item.intAnaliticaClienteAnticipo,
                            strCodAnaliticaClienteAnticipo = item.strCodAnaliticaClienteAnticipo,
                            pgoc_vnum_operacion = item.pgoc_vnum_operacion,
                            pgoc_vreferecia = item.pgoc_vreferecia
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

        public int insertarPago(EPagoDocVenta oBe)
        {
            int? idPago = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PAGO_INSERTAR(
                        ref idPago,
                        oBe.tdocc_icoc_tipo_documento,
                        oBe.pgoc_icod_documento,
                        oBe.pgoc_icod_cliente,
                        oBe.pgoc_sfecha_pago,
                        oBe.pgoc_vnumero_planilla,
                        oBe.pgoc_tipo_pago,
                        oBe.pgoc_icod_nota_credito,
                        oBe.pgoc_icod_tipo_tarjeta,
                        oBe.pgoc_icod_tipo_moneda,
                        oBe.pgoc_descripcion,
                        oBe.pgoc_nmonto,
                        oBe.pgoc_dxc_icod_doc,
                        oBe.pgoc_dxc_icod_pago,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pgoc_fecha_venc_credito,
                        oBe.tblc_iid_banco_cheque,
                        oBe.pgoc_nro_cheque,
                        oBe.pgoc_fecha_cob_cheque,
                        oBe.bcoc_icod_banco,
                        oBe.bcod_icod_banco_cuenta,
                        oBe.pgoc_icod_anticipo,
                        oBe.pgoc_dxc_icod_canje_doc,
                        oBe.pgoc_icod_entidad_finan_mov,
                        oBe.pgoc_iid_tipo_doc_docventa,
                        oBe.pgoc_vreferecia,
                        oBe.pgoc_vnum_operacion

                        );
                }
                return Convert.ToInt32(idPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPago(EPagoDocVenta oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PAGO_MODIFICAR(
                        oBe.pgoc_icod_pago,
                        oBe.tdocc_icoc_tipo_documento,
                        oBe.pgoc_icod_documento,
                        oBe.pgoc_icod_cliente,
                        oBe.pgoc_sfecha_pago,
                        oBe.pgoc_vnumero_planilla,
                        oBe.pgoc_tipo_pago,
                        oBe.pgoc_icod_nota_credito,
                        oBe.pgoc_icod_tipo_tarjeta,
                        oBe.pgoc_icod_tipo_moneda,
                        oBe.pgoc_descripcion,
                        oBe.pgoc_nmonto,
                        oBe.pgoc_dxc_icod_doc,
                        oBe.pgoc_dxc_icod_pago,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pgoc_fecha_venc_credito,
                        oBe.tblc_iid_banco_cheque,
                        oBe.pgoc_nro_cheque,
                        oBe.pgoc_fecha_cob_cheque,
                        oBe.bcoc_icod_banco,
                        oBe.bcod_icod_banco_cuenta,
                        oBe.pgoc_icod_anticipo,
                        oBe.pgoc_dxc_icod_canje_doc,
                        oBe.pgoc_icod_entidad_finan_mov,
                        oBe.pgoc_iid_tipo_doc_docventa,
                        oBe.pgoc_vreferecia,
                        oBe.pgoc_vnum_operacion
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PAGO_ELIMINAR(
                        oBe.pgoc_icod_pago,
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
        #region Planilla Cobranza Cabecera
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaCab(int intEjercicio)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaCab()
                        {
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            plnc_vnumero_planilla = item.plnc_vnumero_planilla,
                            plnc_sfecha_planilla = Convert.ToDateTime(item.plnc_sfecha_planilla),
                            tblc_iid_tipo_moneda = Convert.ToInt32(item.tblc_iid_tipo_moneda),
                            tblc_iid_situacion = Convert.ToInt32(item.tblc_iid_situacion),
                            plnc_nmonto_importe = Convert.ToDecimal(item.plnc_nmonto_importe),
                            plnc_nmonto_pagado = Convert.ToDecimal(item.plnc_nmonto_pagado),
                            plnc_vobservaciones = item.plnc_vobservaciones,
                            strSituacion = item.strSituacion,
                            plnc_icod_ent_finan_mov_sol = item.plnc_icod_ent_finan_mov_sol,
                            plnc_icod_ent_finan_mov_dol = item.plnc_icod_ent_finan_mov_dol,
                            plnc_monto_soles = item.plnc_monto_soles,
                            plnc_monto_dolares = item.plnc_monto_dolares,
                            plnc_fecha_modi = item.plnc_fecha_modi,
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            plnc_icod_pvt = Convert.ToInt32(item.plnc_icod_pvt)


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
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaContabilizacionCabVCO(int intEjercicio, int intPeriodo)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PLANILLA_COBRANZA_CAB_CONTABILIZACION_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaCab()
                        {
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            plnc_vnumero_planilla = item.plnc_vnumero_planilla,
                            plnc_sfecha_planilla = Convert.ToDateTime(item.plnc_sfecha_planilla),
                            tblc_iid_tipo_moneda = Convert.ToInt32(item.tblc_iid_tipo_moneda),
                            tblc_iid_situacion = Convert.ToInt32(item.tblc_iid_situacion),
                            plnc_nmonto_importe = Convert.ToDecimal(item.plnc_nmonto_importe),
                            plnc_nmonto_pagado = Convert.ToDecimal(item.plnc_nmonto_pagado),
                            plnc_vobservaciones = item.plnc_vobservaciones,
                            strSituacion = item.strSituacion,
                            plnc_icod_ent_finan_mov_sol = item.plnc_icod_ent_finan_mov_sol,
                            plnc_icod_ent_finan_mov_dol = item.plnc_icod_ent_finan_mov_dol,
                            /**/
                            intAnaliticaCtaBancariaSol = item.intAnaliticaCtaBancariaSol,
                            strCodAnaliticaCtaBancariaSol = item.strCodAnaliticaCtaBancariaSol,
                            intCtaContableCtaBancariaSol = item.intCtaContableCtaBancariaSol,
                            dcmlTipoCambioSol = item.dcmlTipoCambioSol,
                            dcmlTotalSol = item.dcmlTotalSol,
                            intAnaliticaCtaBancariaDol = item.intAnaliticaCtaBancariaDol,
                            strCodAnaliticaCtaBancariaDol = item.strCodAnaliticaCtaBancariaDol,
                            intCtaContableCtaBancariaDol = item.intCtaContableCtaBancariaDol,
                            dcmlTipoCambioDol = item.dcmlTipoCambioDol,
                            dcmlTotalDol = item.dcmlTotalDol,
                            plnc_icod_pvt = Convert.ToInt32(item.plnc_icod_pvt),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda)

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
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaContabilizacionCab(int intEjercicio, int intPeriodo)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PLANILLA_COBRANZA_CAB_CONTABILIZACION_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaCab()
                        {
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            plnc_vnumero_planilla = item.plnc_vnumero_planilla,
                            plnc_sfecha_planilla = Convert.ToDateTime(item.plnc_sfecha_planilla),
                            tblc_iid_tipo_moneda = Convert.ToInt32(item.tblc_iid_tipo_moneda),
                            tblc_iid_situacion = Convert.ToInt32(item.tblc_iid_situacion),
                            plnc_nmonto_importe = Convert.ToDecimal(item.plnc_nmonto_importe),
                            plnc_nmonto_pagado = Convert.ToDecimal(item.plnc_nmonto_pagado),
                            plnc_vobservaciones = item.plnc_vobservaciones,
                            strSituacion = item.strSituacion,
                            plnc_icod_ent_finan_mov_sol = item.plnc_icod_ent_finan_mov_sol,
                            plnc_icod_ent_finan_mov_dol = item.plnc_icod_ent_finan_mov_dol,
                            /**/
                            intAnaliticaCtaBancariaSol = item.intAnaliticaCtaBancariaSol,
                            strCodAnaliticaCtaBancariaSol = item.strCodAnaliticaCtaBancariaSol,
                            intCtaContableCtaBancariaSol = item.intCtaContableCtaBancariaSol,
                            dcmlTipoCambioSol = item.dcmlTipoCambioSol,
                            dcmlTotalSol = item.dcmlTotalSol,
                            intAnaliticaCtaBancariaDol = item.intAnaliticaCtaBancariaDol,
                            strCodAnaliticaCtaBancariaDol = item.strCodAnaliticaCtaBancariaDol,
                            intCtaContableCtaBancariaDol = item.intCtaContableCtaBancariaDol,
                            dcmlTipoCambioDol = item.dcmlTipoCambioDol,
                            dcmlTotalDol = item.dcmlTotalDol,
                            plnc_icod_pvt = Convert.ToInt32(item.plnc_icod_pvt),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda)

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
        public Tuple<decimal, decimal> getTotalEfectivoPlanilla(int intPlanilla)
        {
            decimal? dcmlEfectivoSol = 0;
            decimal? dcmlEfectivoDol = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GET_TOTAL_EFECTIVO_PLANILLA(intPlanilla, ref dcmlEfectivoSol, ref dcmlEfectivoDol);
                }
                return new Tuple<decimal, decimal>(Convert.ToDecimal(dcmlEfectivoSol), Convert.ToDecimal(dcmlEfectivoDol));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarIcodMovimientoPlanilla(int intPlanilla, int? intIcodSol, int? intIcodDol, decimal? intSoles, decimal? intDolares,
            DateTime? fecha)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_ICOD_MOV_ACTUALIZAR(
                        intPlanilla,
                        intIcodSol,
                        intIcodDol,
                        intSoles,
                        intDolares,
                        fecha);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void revertirCierre(int intPlanilla)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_REVERTIR_CIERRE(intPlanilla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarPlanillaCobranzaCab(EPlanillaCobranzaCab ob)
        {
            int? id_planilla = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_INSERTAR(
                            ref id_planilla,
                            ob.plnc_vnumero_planilla,
                            ob.plnc_sfecha_planilla,
                            ob.tblc_iid_tipo_moneda,
                            ob.tblc_iid_situacion,
                            ob.plnc_nmonto_importe,
                            ob.plnc_nmonto_pagado,
                            ob.plnc_vobservaciones,
                            ob.vendc_icod_vendedor,
                            ob.intUsuario,
                            ob.strPc,
                            ob.plnc_icod_pvt
                            );
                }
                return Convert.ToInt32(id_planilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlanillaCobranzaCab(EPlanillaCobranzaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_MODIFICAR(
                            ob.plnc_icod_planilla,
                            ob.plnc_vnumero_planilla,
                            ob.plnc_sfecha_planilla,
                            ob.tblc_iid_tipo_moneda,
                            ob.tblc_iid_situacion,
                            ob.plnc_nmonto_importe,
                            ob.plnc_nmonto_pagado,
                            ob.plnc_vobservaciones,
                            ob.vendc_icod_vendedor,
                            ob.intUsuario,
                            ob.strPc,
                            ob.plnc_icod_pvt
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlanillaCobranzaCab(EPlanillaCobranzaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_ELIMINAR(
                            ob.plnc_icod_planilla,
                            ob.intUsuario,
                            ob.strPc
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Planilla Cobranza Detalle
        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaDetalleVCO(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_DET_LISTAR_VCO(intPlanilla);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaDet()
                        {
                            tablc_iid_tipo_mov = Convert.ToInt32(item.tablc_iid_tipo_mov),
                            plnd_icod_tipo_doc = item.plnd_icod_tipo_doc,
                            plnd_icod_documento = item.plnd_icod_documento,
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            plnd_nmonto_pagado = Convert.ToDecimal(item.plnd_nmonto_pagado),
                            strTipoDoc = item.tdocc_vabreviatura_tipo_doc,
                            strCliente = item.strCliente,
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            intIcodDxc = item.intIcodDxc,
                            intCliente = Convert.ToInt32(item.intCliente),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            intSituacionFavBov = Convert.ToInt32(item.intSituacion),
                            strSituacionFavBov = item.strSituacion,
                            intIcodEntidadFinanMov = item.antc_icod_entidad_finan_mov,
                            intAnaliticaCliente = item.intAnaliticaCliente,
                            strCodAnaliticaCliente = item.strAnaliticaCliente,
                            tdocd_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            /*Se utiliza los sgts. campos en contabilización para anticipos*/
                            intAnaliticaBancoTarjetaBanco = item.intAnaliticaBancoTarjetaBanco,
                            strCodAnaliticaBancoTarjetaBanco = item.strAnaliticaBancoTarjetaBanco,
                            intCtaCbleTarjetaBanco = item.intCtaCbleTarjetaBanco,
                            intTipoDocDelPago = item.intTipoDocDelPago,
                            strNroOt = item.strNroOt,
                            tdocc_vabreviatura_tipo_docPago = item.tdocc_vabreviatura_tipo_docPago,
                            ctacc_icod_cuenta_contable_nac = item.ctacc_icod_cuenta_contable_nac,
                            tdocd_iid_codigo_doc_det = Convert.ToInt32(item.tdocd_iid_codigo_doc_det),
                            tablc_iid_tipo_pago = Convert.ToInt32(item.tablc_iid_tipo_pago),
                            intTipoTarjeta = Convert.ToInt32(item.tablc_iid_tipo_tarjeta)
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

        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_DET_LISTAR(intPlanilla);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaDet()
                        {
                            plnd_icod_detalle = item.plnd_icod_detalle,
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            tablc_iid_tipo_mov = Convert.ToInt32(item.tablc_iid_tipo_mov),
                            plnd_sfecha_doc = Convert.ToDateTime(item.plnd_sfecha_doc),
                            plnd_icod_tipo_doc = item.plnd_icod_tipo_doc,
                            plnd_icod_documento = item.plnd_icod_documento,
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            plnd_nmonto = Convert.ToDecimal(item.plnd_nmonto),
                            plnd_nmonto_pagado = Convert.ToDecimal(item.plnd_nmonto_pagado),
                            strTipoDoc = item.tdocc_vabreviatura_tipo_doc,
                            strCliente = item.strCliente,
                            strTipoMov = item.strTipoMov,
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            pgoc_dxc_icod_pago = item.pgoc_dxc_icod_pago,
                            intIcodDxc = item.intIcodDxc,
                            intCliente = Convert.ToInt32(item.intCliente),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            strTipoMoneda = item.strTipoMoneda,
                            intTipoPago = item.pgoc_tipo_pago,
                            intNotaCredito = item.pgoc_icod_nota_credito,
                            intTipoTarjeta = item.pgoc_icod_tipo_tarjeta,
                            strPagoDescripcion = item.strPagoDescripcion,
                            StrNotaCredito = item.StrNotaCredito,
                            antc_icod_anticipo = item.antc_icod_anticipo,
                            strAnticipo = item.strAnticipo,
                            strAdelantoCliente = item.strAdelantoCliente,
                            intSituacionFavBov = Convert.ToInt32(item.intSituacion),
                            strSituacionFavBov = item.strSituacion,
                            plnd_tipo_cambio = Convert.ToDecimal(item.plnd_tipo_cambio),
                            intIcodEntidadFinanMov = item.antc_icod_entidad_finan_mov,
                            intAnaliticaCliente = Convert.ToInt32(item.intAnaliticaCliente),
                            strCodAnaliticaCliente = item.strAnaliticaCliente,
                            tdocd_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            /*Se utiliza los sgts. campos en contabilización para anticipos*/
                            intAnaliticaBancoTarjetaBanco = item.intAnaliticaBancoTarjetaBanco,
                            strCodAnaliticaBancoTarjetaBanco = item.strAnaliticaBancoTarjetaBanco,
                            intCtaCbleTarjetaBanco = item.intCtaCbleTarjetaBanco,
                            intTipoDocDelPago = item.intTipoDocDelPago

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

        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaImpresionDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_DET_IMPRESION_LISTAR(intPlanilla);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaDet()
                        {
                            plnd_icod_detalle = item.plnd_icod_detalle,
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            tablc_iid_tipo_mov = Convert.ToInt32(item.tablc_iid_tipo_mov),
                            plnd_sfecha_doc = Convert.ToDateTime(item.plnd_sfecha_doc),
                            plnd_icod_tipo_doc = item.plnd_icod_tipo_doc,
                            plnd_icod_documento = item.plnd_icod_documento,
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            plnd_nmonto = Convert.ToDecimal(item.plnd_nmonto),
                            plnd_nmonto_pagado = Convert.ToDecimal(item.plnd_nmonto_pagado),
                            strTipoDoc = String.Format("{0}-{1}", item.tdocc_vabreviatura_tipo_doc, item.plnd_vnumero_doc),
                            strCliente = item.strCliente,
                            strTipoMov = item.strTipoMov,
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            pgoc_dxc_icod_pago = item.pgoc_dxc_icod_pago,
                            intIcodDxc = item.pgoc_dxc_icod_doc,
                            intCliente = Convert.ToInt32(item.intCliente),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            strMonedaGroup = (Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3) ? "MOVIMIENTOS EN NUEVOS SOLES" : "MOVIMIENTOS EN DOLARES",
                            strTipoMoneda = item.strTipoMoneda,
                            intTipoPago = item.pgoc_tipo_pago,
                            intNotaCredito = item.pgoc_icod_nota_credito,
                            intTipoTarjeta = item.pgoc_icod_tipo_tarjeta,
                            strPagoDescripcion = item.strPagoDescripcion,
                            StrNotaCredito = item.StrNotaCredito,
                            antc_icod_anticipo = item.antc_icod_anticipo,
                            strAnticipo = item.strAnticipo,
                            strAdelantoCliente = item.strAdelantoCliente,
                            intSituacionFavBov = Convert.ToInt32(item.intSituacion),
                            strSituacionFavBov = item.strSituacion,
                            plnd_tipo_cambio = Convert.ToDecimal(item.plnd_tipo_cambio),
                            dblPagoEfectivo = Convert.ToDecimal(item.dblPagoEfectivo),
                            dblPagoTarjetaCredito = Convert.ToDecimal(item.dblPagoTarjetaCredito),
                            dblPagoNotaCredito = Convert.ToDecimal(item.dblPagoNotaCredito),
                            dblPagoCheque = Convert.ToDecimal(item.dblPagoCheque),
                            dblPagoTransferencia = Convert.ToDecimal(item.dblPagoTransferencia),
                            dblPagoCredito = Convert.ToDecimal(item.dblPagoCredito),
                            dblPagoAnticipo = Convert.ToDecimal(item.dblPagoAnticipo)
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

        public int insertarPlanillaCobranzaDetalle(EPlanillaCobranzaDet ob)
        {
            int? id_planilla_det = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_INSERTAR(
                            ref id_planilla_det,
                            ob.plnc_icod_planilla,
                            ob.tablc_iid_tipo_mov,
                            ob.plnd_sfecha_doc,
                            ob.plnd_icod_tipo_doc,
                            ob.plnd_icod_documento,
                            ob.plnd_vnumero_doc,
                            ob.plnd_nmonto,
                            ob.plnd_nmonto_pagado,
                            ob.intUsuario,
                            ob.strPc,
                            ob.pgoc_icod_pago,
                            ob.tablc_iid_tipo_moneda,
                            ob.antc_icod_anticipo,
                            ob.plnd_tipo_cambio);
                }
                return Convert.ToInt32(id_planilla_det);

                if (ob.tablc_iid_tipo_mov == 2)
                {
                    ob.plnd_icod_detalle = Convert.ToInt32(id_planilla_det);
                    modificarPlanillaCobranzaDetalleIcodTipoDocumento(ob);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlanillaCobranzaDetalleIcodTipoDocumento(EPlanillaCobranzaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_MODIFICAR_TIPO_DOC(
                            ob.plnd_icod_detalle,
                            ob.plnd_icod_tipo_doc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarPlanillaCobranzaDetalle(EPlanillaCobranzaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_MODIFICAR(
                            ob.plnd_icod_detalle,
                            ob.plnd_sfecha_doc,
                            ob.plnd_icod_tipo_doc,
                            ob.plnd_icod_documento,
                            ob.plnd_vnumero_doc,
                            ob.plnd_nmonto,
                            ob.plnd_nmonto_pagado,
                            ob.intUsuario,
                            ob.strPc,
                            ob.plnd_tipo_cambio,
                            ob.pgoc_icod_pago);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlanillaCobranzaDetalle(EPlanillaCobranzaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_ELIMINAR(
                            ob.plnd_icod_detalle,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Anticipo
        public int insertarAnticipo(EAnticipo ob)
        {
            int? id_anticipo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_ANTICIPO_INSERTAR(
                            ref id_anticipo,
                            ob.antc_vnumero_anticipo,
                            ob.antc_sfecha_anticipo,
                            ob.antc_icod_cliente,
                            ob.antc_observaciones,
                            ob.antc_nmonto_anticipo,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_situacion_anticipo,
                            ob.antc_icod_adelanto_cliente,
                            ob.antc_icod_dxc_adelanto,
                            ob.tablc_iid_tipo_pago,
                            ob.tablc_iid_tipo_tarjeta,
                            ob.antc_icod_entidad_finan_mov,
                            ob.intUsuario,
                            ob.strPc
                           );
                }
                return Convert.ToInt32(id_anticipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAnticipo(EAnticipo ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_ANTICIPO_MODIFICAR(
                            ob.antc_icod_anticipo,
                            ob.antc_vnumero_anticipo,
                            ob.antc_sfecha_anticipo,
                            ob.antc_icod_cliente,
                            ob.antc_observaciones,
                            ob.antc_nmonto_anticipo,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_situacion_anticipo,
                            ob.antc_icod_adelanto_cliente,
                            ob.antc_icod_dxc_adelanto,
                            ob.tablc_iid_tipo_pago,
                            ob.tablc_iid_tipo_tarjeta,
                            ob.antc_icod_entidad_finan_mov,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAnticipo(EAnticipo ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_ANTICIPO_ELIMINAR(
                            ob.antc_icod_anticipo,
                            ob.intUsuario,
                            ob.strPc);
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
            List<ERetencion> lista = new List<ERetencion>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RETENCION_CAB_LISTAR(intEjericio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new ERetencion()
                        {
                            retc_icod_comprobante_retencion = item.retc_icod_comprobante_retencion,
                            anioc_iid_anio = Convert.ToInt32(item.anioc_iid_anio),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            retc_vnumero_comprob_reten = item.retc_vnumero_comprob_reten,
                            retc_sfec_comprob_reten = Convert.ToDateTime(item.retc_sfec_comprob_reten),
                            proc_icod_cliente = Convert.ToInt32(item.proc_icod_cliente),
                            tablc_iid_moneda = Convert.ToInt32(item.tablc_iid_moneda),
                            retc_nmto_tipo_cambio = Convert.ToDecimal(item.retc_nmto_tipo_cambio),
                            retc_nmto_total_pago = Convert.ToDecimal(item.retc_nmto_total_pago),
                            retc_nmto_total_retencion = Convert.ToDecimal(item.retc_nmto_total_retencion),
                            strCliente = item.strCliente,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            intAnalitica = Convert.ToInt32(item.intAnalitica),
                            strCodAnalitica = item.strCodAnalitica
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
        public int insertarRetencionCab(ERetencion ob)
        {
            int? id_retencion = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_CAB_INSERTAR(
                        ref id_retencion,
                        ob.anioc_iid_anio,
                        ob.mesec_iid_mes,
                        ob.retc_vnumero_comprob_reten,
                        ob.retc_sfec_comprob_reten,
                        ob.proc_icod_cliente,
                        ob.tablc_iid_moneda,
                        ob.retc_nmto_tipo_cambio,
                        ob.retc_nmto_total_pago,
                        ob.retc_nmto_total_retencion,
                        ob.tablc_iid_situacion,
                        ob.intUsuario,
                        ob.strPc);
                }
                return Convert.ToInt32(id_retencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRetencionCab(ERetencion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_CAB_MODIFICAR(
                        ob.retc_icod_comprobante_retencion,
                        ob.anioc_iid_anio,
                        ob.mesec_iid_mes,
                        ob.retc_vnumero_comprob_reten,
                        ob.retc_sfec_comprob_reten,
                        ob.proc_icod_cliente,
                        ob.tablc_iid_moneda,
                        ob.retc_nmto_tipo_cambio,
                        ob.retc_nmto_total_pago,
                        ob.retc_nmto_total_retencion,
                        ob.tablc_iid_situacion,
                        ob.intUsuario,
                        ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRetencionCab(ERetencion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_CAB_ELIMINAR(
                            ob.retc_icod_comprobante_retencion,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ERetencionDet> listarRetencionDet(int intRetencion)
        {
            List<ERetencionDet> lista = new List<ERetencionDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RETENCION_DET_LISTAR(intRetencion);
                    foreach (var item in query)
                    {
                        lista.Add(new ERetencionDet()
                        {
                            retd_icod_detalle = item.retd_icod_detalle,
                            retc_icod_comprobante_retencion = Convert.ToInt32(item.retc_icod_comprobante_retencion),
                            tdoc_icod_tipo_documento = Convert.ToInt32(item.tdoc_icod_tipo_documento),
                            retd_vnumero_documento = item.retd_vnumero_documento,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            retd_sfec_documento = Convert.ToDateTime(item.retd_sfec_documento),
                            retd_nmto_tipo_cambio_doc = Convert.ToDecimal(item.retd_nmto_tipo_cambio_doc),
                            retd_nmto_pago_doc = Convert.ToDecimal(item.retd_nmto_pago_doc),
                            retd_nmto_retencion = Convert.ToDecimal(item.retd_nmto_retencion),
                            retd_nmto_total_doc = Convert.ToDecimal(item.retd_nmto_total_doc),
                            pdxpc_icod_correlativo = Convert.ToInt64(item.pdxpc_icod_correlativo),
                            strTipoDoc = item.strTipoDoc,
                            strMoneda = item.strMoneda,
                            intIcodDXC = Convert.ToInt64(item.intIcodDXC),
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            intAnalitica = Convert.ToInt32(item.intAnalitica),
                            strCodAnalitica = item.strCodAnalitica,
                            Moneda_DXC = item.Moneda_DXC
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
        public void insertarRetencionDet(ERetencionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_DET_INSERTAR(
                        ob.retc_icod_comprobante_retencion,
                        ob.tdoc_icod_tipo_documento,
                        ob.retd_vnumero_documento,
                        ob.tablc_iid_tipo_moneda,
                        ob.retd_sfec_documento,
                        ob.retd_nmto_tipo_cambio_doc,
                        ob.retd_nmto_pago_doc,
                        ob.retd_nmto_retencion,
                        ob.retd_nmto_total_doc,
                        ob.pdxpc_icod_correlativo,
                        ob.intUsuario,
                        ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRetencionDet(ERetencionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_DET_MODIFICAR(
                            ob.retd_icod_detalle,
                            ob.tdoc_icod_tipo_documento,
                            ob.retd_vnumero_documento,
                            ob.tablc_iid_tipo_moneda,
                            ob.retd_sfec_documento,
                            ob.retd_nmto_tipo_cambio_doc,
                            ob.retd_nmto_pago_doc,
                            ob.retd_nmto_retencion,
                            ob.retd_nmto_total_doc,
                            ob.pdxpc_icod_correlativo,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRetencionDet(ERetencionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_DET_ELIMINAR(
                            ob.retd_icod_detalle,
                            ob.pdxpc_icod_correlativo,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Nota Crédito Ventas
        public List<ENotaCredito> listarNotaCreditoClienteCab(int intEjericio)
        {
            List<ENotaCredito> lista = new List<ENotaCredito>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_VENTA_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCredito()
                        {
                            ncrec_icod_credito = item.ncrec_icod_credito,
                            ncrec_vnumero_credito = item.ncrec_vnumero_credito,
                            ncrec_sfecha_credito = item.ncrec_sfecha_credito,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            ncrec_ianio = Convert.ToInt32(item.ncrec_ianio),
                            ncrec_vreferencia = item.ncrec_vreferencia,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tablc_iid_tipo_nota_credito_venta = item.tablc_iid_tipo_nota_credito_venta,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ncrec_vnumero_documento = item.ncrec_vnumero_documento,
                            ncrec_sfecha_documento = item.ncrec_sfecha_documento,
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            NomVendedor = item.NomVendedor,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            ncrec_nmonto_neto = item.ncrec_nmonto_neto,
                            ncrec_npor_imp_igv = item.ncrec_npor_imp_igv,
                            ncrec_nmonto_imp = item.ncrec_nmonto_imp,
                            ncrec_nmonto_total = item.ncrec_nmonto_total,
                            ncrec_iid_situacion_credito = Convert.ToInt32(item.ncrec_iid_situacion_credito),
                            almac_icod_almacen = item.almac_icod_almacen,
                            ncrec_tipo_cambio_fecha_doc_venta = item.ncrec_tipo_cambio_fecha_doc_venta,
                            ncrec_nmonto_pagado = item.ncrec_nmonto_pagado,
                            strSituacion = item.strSituacion,
                            strDesCliente = item.strDesCliente,
                            strRuc = item.strRuc,
                            DireccionCliente = item.DireccionCliente,
                            strTipoDoc = item.strTipoDoc,
                            strMoneda = item.strMoneda,
                            ncrec_icod_dxc = Convert.ToInt64(item.ncrec_icod_dxc),
                            ncrec_bincluye_igv = item.ncrec_bincluye_igv,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            ncrec_iclase_doc = Convert.ToInt32(item.ncrec_iclase_doc),
                            StrClaseDocumento = item.StrClaseDocumento,
                            ncrec_tipo_nota_credito = Convert.ToInt32(item.ncrec_tipo_nota_credito),
                            ncrec_bind_arroz = Convert.ToBoolean(item.ncrec_bind_arroz),
                            ncrec_npor_imp_ivap = Convert.ToDecimal(item.ncrec_npor_imp_ivap),
                            ncrec_nmonto_ivap = Convert.ToDecimal(item.ncrec_nmonto_ivap),
                            ncrec_nmonto_neto_ivap = Convert.ToDecimal(item.ncrec_nmonto_neto_ivap),
                            ncrec_nmonto_neto_exo = Convert.ToDecimal(item.ncrec_nmonto_neto_exo),
                            ncrec_vmotivo_sunat = item.ncrec_vmotivo_sunat,
                            doc_icod_documento = Convert.ToInt32(item.ncrec_icod_credito)
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
        public int insertarNotaCreditoClienteCab(ENotaCredito ob)
        {
            int? id_retencion = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_INSERTAR(
                        ref id_retencion,
                        ob.ncrec_vnumero_credito,
                        ob.ncrec_sfecha_credito,
                        ob.cliec_icod_cliente,
                        ob.ncrec_ianio,
                        ob.ncrec_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ncrec_vnumero_documento,
                        ob.ncrec_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ncrec_nmonto_neto,
                        ob.ncrec_npor_imp_igv,
                        ob.ncrec_nmonto_imp,
                        ob.ncrec_nmonto_total,
                        ob.ncrec_iid_situacion_credito,
                        ob.almac_icod_almacen,
                        ob.ncrec_tipo_cambio_fecha_doc_venta,
                        ob.ncrec_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ncrec_nmonto_pagado,
                        ob.ncrec_icod_dxc,
                        ob.ncrec_bincluye_igv,
                        ob.ncrec_iclase_doc,
                        ob.ncrec_tipo_nota_credito,
                        ob.ncrec_bind_arroz,
                        ob.ncrec_npor_imp_ivap,
                        ob.ncrec_nmonto_ivap,
                        ob.ncrec_nmonto_neto_ivap,
                        ob.ncrec_nmonto_neto_exo,
                        ob.ncrec_vmotivo_sunat);
                }
                return Convert.ToInt32(id_retencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoClienteCab(ENotaCredito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_MODIFICAR(
                        ob.ncrec_icod_credito,
                        ob.ncrec_vnumero_credito,
                        ob.ncrec_sfecha_credito,
                        ob.cliec_icod_cliente,
                        ob.ncrec_ianio,
                        ob.ncrec_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ncrec_vnumero_documento,
                        ob.ncrec_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ncrec_nmonto_neto,
                        ob.ncrec_npor_imp_igv,
                        ob.ncrec_nmonto_imp,
                        ob.ncrec_nmonto_total,
                        ob.ncrec_iid_situacion_credito,
                        ob.almac_icod_almacen,
                        ob.ncrec_tipo_cambio_fecha_doc_venta,
                        ob.ncrec_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ncrec_nmonto_pagado,
                        ob.ncrec_icod_dxc,
                        ob.ncrec_bincluye_igv,
                        ob.ncrec_iclase_doc,
                        ob.ncrec_bind_arroz,
                        ob.ncrec_npor_imp_ivap,
                        ob.ncrec_nmonto_ivap,
                        ob.ncrec_nmonto_neto_ivap,
                        ob.ncrec_nmonto_neto_exo,
                        ob.ncrec_vmotivo_sunat);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoClienteCab(ENotaCredito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_ELIMINAR(
                        ob.ncrec_icod_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularNotaCreditoClienteCab(ENotaCredito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_ANULAR(
                        ob.ncrec_icod_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaCreditoDet> listarNotaCreditoClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_DET_LISTAR(intNotaCredito);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoDet()
                        {
                            dcrec_icod_detalle_credito = item.dcrec_icod_detalle_credito,
                            dcrec_inro_item = item.dcrec_inro_item,
                            dcrec_ncantidad_producto = item.dcrec_ncantidad_producto,
                            dcrec_nmonto_unitario = decimal.Round(item.dcrec_nmonto_unitario, 2),
                            dcrec_vdescripcion = item.dcrec_vdescripcion,
                            dcrec_nmonto_item = item.dcrec_nmonto_item,
                            dcrec_nmonto_impuesto = item.dcrec_nmonto_impuesto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_sit_item_nota_credito = item.tablc_iid_sit_item_nota_credito,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            //intClasificacion = item.intClasificacion,
                            //strLinea = item.strLinea,
                            //strSubLinea = item.strSubLinea,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            dcrec_npor_imp_arroz = Convert.ToDecimal(item.dcrec_npor_imp_arroz),
                            dcrec_nmonto_imp_arroz = Convert.ToDecimal(item.dcrec_nmonto_imp_arroz),
                            dcrec_nneto_ivap = Convert.ToDecimal(item.dcrec_nneto_ivap),
                            dcrec_nneto_igv = Convert.ToDecimal(item.dcrec_nneto_igv),
                            dcrec_nneto_exo = Convert.ToDecimal(item.dcrec_nneto_exo),
                            prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv),
                            dcrec_nmonto_total = Convert.ToDecimal(item.dcrec_nmonto_total),

                            #region Factura Electronica Detalle
                            NumeroOrdenItem = item.dcrec_inro_item,
                            cantidad = item.dcrec_ncantidad_producto,
                            unidadMedida = "ZZ",
                            ValorVentaItem = Convert.ToDecimal(item.dcrec_nmonto_total),
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Math.Round(Convert.ToDecimal((item.dcrec_nmonto_total * Convert.ToDecimal(18)) / 100), 2, MidpointRounding.ToEven),
                            MontoImpuestoIgvItem = Math.Round(Convert.ToDecimal((item.dcrec_nmonto_total * Convert.ToDecimal(18)) / 100), 2, MidpointRounding.ToEven),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.dcrec_nmonto_total),
                            PorcentajeIGVItem = Convert.ToDecimal(18),
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.dcrec_vdescripcion,
                            codigoItem = "SERV0" + item.dcrec_inro_item.ToString(),
                            ObservacionesItem = "",
                            ValorUnitarioItem = item.dcrec_nmonto_unitario
                            #endregion

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
        public List<ENotaCreditoDet> listarNotaCreditoNoComercialClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_DET_LISTAR(intNotaCredito);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoDet()
                        {
                            dcrec_icod_detalle_credito = item.dcrec_icod_detalle_credito,
                            dcrec_inro_item = item.dcrec_inro_item,
                            dcrec_ncantidad_producto = item.dcrec_ncantidad_producto,
                            dcrec_nmonto_unitario = decimal.Round(item.dcrec_nmonto_unitario, 2),
                            dcrec_vdescripcion = item.dcrec_vdescripcion,
                            dcrec_nmonto_item = item.dcrec_nmonto_item,
                            dcrec_nmonto_impuesto = item.dcrec_nmonto_impuesto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_sit_item_nota_credito = item.tablc_iid_sit_item_nota_credito,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            //intClasificacion = item.intClasificacion,
                            //strLinea = item.strLinea,
                            //strSubLinea = item.strSubLinea,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            dcrec_npor_imp_arroz = Convert.ToDecimal(item.dcrec_npor_imp_arroz),
                            dcrec_nmonto_imp_arroz = Convert.ToDecimal(item.dcrec_nmonto_imp_arroz),
                            dcrec_nneto_ivap = Convert.ToDecimal(item.dcrec_nneto_ivap),
                            dcrec_nneto_igv = Convert.ToDecimal(item.dcrec_nneto_igv),
                            dcrec_nneto_exo = Convert.ToDecimal(item.dcrec_nneto_exo),
                            prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv),
                            dcrec_nmonto_total = Convert.ToDecimal(item.dcrec_nmonto_total)
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
        public void insertarNotaCreditoClienteDet(ENotaCreditoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_DET_INSERTAR(
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.dcrec_nporcentaje_impuesto,
                        ob.dcrec_npor_imp_arroz,
                        ob.dcrec_nmonto_imp_arroz,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.dcrec_nneto_ivap,
                        ob.dcrec_nneto_igv,
                        ob.dcrec_nneto_exo,
                        ob.dcrec_nmonto_total
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoClienteDet(ENotaCreditoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_DET_MODIFICAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.dcrec_nporcentaje_impuesto,
                        ob.dcrec_npor_imp_arroz,
                        ob.dcrec_nmonto_imp_arroz,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.dcrec_nneto_ivap,
                        ob.dcrec_nneto_igv,
                        ob.dcrec_nneto_exo,
                        ob.dcrec_nmonto_total
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoClienteDet(ENotaCreditoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_DET_ELIMINAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region NOTA DEBITO VENTA
        public List<ENotaDebito> listarNotaDebitoClienteCab(int intEjericio)
        {
            List<ENotaDebito> lista = new List<ENotaDebito>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_DEBITO_VENTA_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaDebito()
                        {
                            ndebc_icod_debito = item.ndebc_icod_debito,
                            ndebc_vnumero_debito = item.ndebc_vnumero_debito,
                            ndebc_sfecha_debito = item.ndebc_sfecha_debito,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            ndebc_ianio = Convert.ToInt32(item.ndebc_ianio),
                            ndebc_vreferencia = item.ndebc_vreferencia,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tablc_iid_tipo_nota_credito_venta = item.tablc_iid_tipo_nota_credito_venta,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ndebc_vnumero_documento = item.ndebc_vnumero_documento,
                            ndebc_sfecha_documento = item.ndebc_sfecha_documento,
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            NomVendedor = item.NomVendedor,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            ndebc_nmonto_neto = item.ndebc_nmonto_neto,
                            ndebc_npor_imp_igv = item.ndebc_npor_imp_igv,
                            ndebc_nmonto_imp = item.ndebc_nmonto_imp,
                            ndebc_nmonto_total = item.ndebc_nmonto_total,
                            ndebc_iid_situacion_debito = Convert.ToInt32(item.ndebc_iid_situacion_debito),
                            almac_icod_almacen = item.almac_icod_almacen,
                            ndebc_tipo_cambio_fecha_doc_venta = item.ndebc_tipo_cambio_fecha_doc_venta,
                            ndebc_nmonto_pagado = item.ndebc_nmonto_pagado,
                            strSituacion = item.strSituacion,
                            strDesCliente = item.strDesCliente,
                            strRuc = item.strRuc,
                            strTipoDoc = item.strTipoDoc,
                            strMoneda = item.strMoneda,
                            ndebc_icod_dxc = Convert.ToInt64(item.ndebc_icod_dxc),
                            ndebc_bincluye_igv = item.ndebc_bincluye_igv,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            ndebc_iclase_doc = Convert.ToInt32(item.ndebc_iclase_doc),
                            StrClaseDocumento = item.StrClaseDocumento,
                            ndebc_tipo_nota_debito = Convert.ToInt32(item.ndebc_tipo_nota_debito),
                            ndebc_bind_arroz = Convert.ToBoolean(item.ndebc_bind_arroz),
                            ndebc_npor_imp_ivap = Convert.ToDecimal(item.ndebc_npor_imp_ivap),
                            ndebc_nmonto_ivap = Convert.ToDecimal(item.ndebc_nmonto_ivap),
                            ndebc_nmonto_neto_ivap = Convert.ToDecimal(item.ndebc_nmonto_neto_ivap),
                            ndebc_nmonto_neto_exo = Convert.ToDecimal(item.ndebc_nmonto_neto_exo)
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
        public int insertarNotaDebitoClienteCab(ENotaDebito ob)
        {
            int? id_retencion = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_INSERTAR(
                        ref id_retencion,
                        ob.ndebc_vnumero_debito,
                        ob.ndebc_sfecha_debito,
                        ob.cliec_icod_cliente,
                        ob.ndebc_ianio,
                        ob.ndebc_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ndebc_vnumero_documento,
                        ob.ndebc_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ndebc_nmonto_neto,
                        ob.ndebc_npor_imp_igv,
                        ob.ndebc_nmonto_imp,
                        ob.ndebc_nmonto_total,
                        ob.ndebc_iid_situacion_debito,
                        ob.almac_icod_almacen,
                        ob.ndebc_tipo_cambio_fecha_doc_venta,
                        ob.ndebc_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ndebc_nmonto_pagado,
                        ob.ndebc_icod_dxc,
                        ob.ndebc_bincluye_igv,
                        ob.ndebc_iclase_doc,
                        ob.ndebc_tipo_nota_debito,
                        ob.ndebc_bind_arroz,
                        ob.ndebc_npor_imp_ivap,
                        ob.ndebc_nmonto_ivap,
                        ob.ndebc_nmonto_neto_ivap,
                        ob.ndebc_nmonto_neto_exo);
                }
                return Convert.ToInt32(id_retencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaDebitoClienteCab(ENotaDebito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_MODIFICAR(
                        ob.ndebc_icod_debito,
                        ob.ndebc_vnumero_debito,
                        ob.ndebc_sfecha_debito,
                        ob.cliec_icod_cliente,
                        ob.ndebc_ianio,
                        ob.ndebc_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ndebc_vnumero_documento,
                        ob.ndebc_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ndebc_nmonto_neto,
                        ob.ndebc_npor_imp_igv,
                        ob.ndebc_nmonto_imp,
                        ob.ndebc_nmonto_total,
                        ob.ndebc_iid_situacion_debito,
                        ob.almac_icod_almacen,
                        ob.ndebc_tipo_cambio_fecha_doc_venta,
                        ob.ndebc_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ndebc_nmonto_pagado,
                        ob.ndebc_icod_dxc,
                        ob.ndebc_bincluye_igv,
                        ob.ndebc_iclase_doc,
                        ob.ndebc_bind_arroz,
                        ob.ndebc_npor_imp_ivap,
                        ob.ndebc_nmonto_ivap,
                        ob.ndebc_nmonto_neto_ivap,
                        ob.ndebc_nmonto_neto_exo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitoClienteCab(ENotaDebito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_ELIMINAR(
                        ob.ndebc_icod_debito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularNotaDebitoClienteCab(ENotaDebito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_ANULAR(
                        ob.ndebc_icod_debito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaDebitoDet> listarNotaDebitoClienteDet(int intNotaDebito)
        {
            List<ENotaDebitoDet> lista = new List<ENotaDebitoDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_DEBITO_DET_LISTAR(intNotaDebito);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaDebitoDet()
                        {
                            ddebc_icod_detalle_debito = item.ddebc_icod_detalle_debito,
                            ddebc_inro_item = item.ddebc_inro_item,
                            ddebc_ncantidad_producto = item.ddebc_ncantidad_producto,
                            ddebc_nmonto_unitario = decimal.Round(item.ddebc_nmonto_unitario, 2),
                            ddebc_vdescripcion = item.ddebc_vdescripcion,
                            ddebc_nmonto_item = item.ddebc_nmonto_item,
                            ddebc_nmonto_impuesto = item.ddebc_nmonto_impuesto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_sit_item_nota_credito = item.tablc_iid_sit_item_nota_credito,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            //intClasificacion = item.intClasificacion,
                            //strLinea = item.strLinea,
                            //strSubLinea = item.strSubLinea,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            ddebc_npor_imp_arroz = Convert.ToDecimal(item.ddebc_npor_imp_arroz),
                            ddebc_nmonto_imp_arroz = Convert.ToDecimal(item.ddebc_nmonto_imp_arroz),
                            ddebc_nneto_ivap = Convert.ToDecimal(item.ddebc_nneto_ivap),
                            ddebc_nneto_igv = Convert.ToDecimal(item.ddebc_nneto_igv),
                            ddebc_nneto_exo = Convert.ToDecimal(item.ddebc_nneto_exo),
                            prdc_afecto_ivap = Convert.ToBoolean(item.prdc_afecto_ivap),
                            prdc_afecto_igv = Convert.ToBoolean(item.prdc_afecto_igv),
                            ddebc_nmonto_total = Convert.ToDecimal(item.ddebc_nmonto_total),
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
        public void insertarNotaDebitoClienteDet(ENotaDebitoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_DET_INSERTAR(
                        ob.ndebc_icod_debito,
                        ob.ddebc_inro_item,
                        ob.ddebc_ncantidad_producto,
                        ob.ddebc_nmonto_unitario,
                        ob.ddebc_vdescripcion,
                        ob.ddebc_nmonto_item,
                        ob.ddebc_nmonto_impuesto,
                        ob.ddebc_nporcentaje_impuesto,
                        ob.ddebc_npor_imp_arroz,
                        ob.ddebc_nmonto_imp_arroz,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ddebc_nneto_ivap,
                        ob.ddebc_nneto_igv,
                        ob.ddebc_nneto_exo,
                        ob.ddebc_nmonto_total
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaDebitoClienteDet(ENotaDebitoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_DET_MODIFICAR(
                        ob.ddebc_icod_detalle_debito,
                        ob.ndebc_icod_debito,
                        ob.ddebc_inro_item,
                        ob.ddebc_ncantidad_producto,
                        ob.ddebc_nmonto_unitario,
                        ob.ddebc_vdescripcion,
                        ob.ddebc_nmonto_item,
                        ob.ddebc_nmonto_impuesto,
                        ob.ddebc_nporcentaje_impuesto,
                        ob.ddebc_npor_imp_arroz,
                        ob.ddebc_nmonto_imp_arroz,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ddebc_nneto_ivap,
                        ob.ddebc_nneto_igv,
                        ob.ddebc_nneto_exo,
                        ob.ddebc_nmonto_total
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitoClienteDet(ENotaDebitoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_DET_ELIMINAR(
                        ob.ddebc_icod_detalle_debito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Tipo Tarjeta
        public List<ETipoTarjeta> listarTipoTarjeta()
        {
            List<ETipoTarjeta> lista = new List<ETipoTarjeta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_TIPO_TARJETA_CREDITO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoTarjeta()
                        {
                            tcrc_icod_tipo_tarjeta_cred = item.tcrc_icod_tipo_tarjeta_cred,
                            tcrc_iid_tipo_tarjeta_cred = Convert.ToInt32(item.tcrc_iid_tipo_tarjeta_cred),
                            tcrc_vdescripcion_tipo_tarjeta_cred = item.tcrc_vdescripcion_tipo_tarjeta_cred,
                            tcrc_nporcentaje_comision = Convert.ToDecimal(item.tcrc_nporcentaje_comision),
                            bcoc_icod_banco = Convert.ToInt32(item.bcoc_icod_banco),
                            bcod_icod_banco_cuenta = Convert.ToInt32(item.bcod_icod_banco_cuenta),
                            strDesBanco = item.strDesBanco,
                            strNroCuenta = item.strNroCuenta
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
        public int insertarTipoTarjeta(ETipoTarjeta ob)
        {
            int? id_anticipo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TIPO_TARJETA_CREDITO_INSERTAR(
                            ref id_anticipo,
                            ob.tcrc_iid_tipo_tarjeta_cred,
                            ob.tcrc_vdescripcion_tipo_tarjeta_cred,
                            ob.tcrc_nporcentaje_comision,
                            ob.bcoc_icod_banco,
                            ob.bcod_icod_banco_cuenta,
                            ob.intUsuario,
                            ob.strPc
                           );
                }
                return Convert.ToInt32(id_anticipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoTarjeta(ETipoTarjeta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TIPO_TARJETA_CREDITO_MODIFICAR(
                            ob.tcrc_icod_tipo_tarjeta_cred,
                            ob.tcrc_iid_tipo_tarjeta_cred,
                            ob.tcrc_vdescripcion_tipo_tarjeta_cred,
                            ob.tcrc_nporcentaje_comision,
                            ob.bcoc_icod_banco,
                            ob.bcod_icod_banco_cuenta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoTarjeta(ETipoTarjeta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TIPO_TARJETA_CREDITO_ELIMINAR(
                            ob.tcrc_icod_tipo_tarjeta_cred,
                            ob.intUsuario,
                            ob.strPc);
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
            List<ETransportista> lista = new List<ETransportista>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_TRANSPORTISTA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETransportista()
                        {
                            tranc_icod_transportista = item.tranc_icod_transportista,
                            tranc_iid_transportista = item.tranc_iid_transportista,
                            tranc_vnombre_transportista = item.tranc_vnombre_transportista,
                            tranc_vruc = item.tranc_vruc,
                            tranc_vdireccion = item.tranc_vdireccion,
                            tranc_vnumero_telefono = item.tranc_vnumero_telefono,
                            tranc_vmicti = item.tranc_vmicti,
                            tranc_cnumero_dni = item.tranc_cnumero_dni,
                            tranc_vnombre_conductor = item.tranc_vnombre_conductor,
                            tranc_vnum_licencia_conducir = item.tranc_vnum_licencia_conducir,
                            tranc_vnum_placa = item.tranc_vnum_placa,
                            tranc_vnum_matricula = item.tranc_vnum_matricula,
                            tranc_iid_situacion_transporte = Convert.ToInt32(item.tranc_iid_situacion_transporte),
                            strSituacion = item.strSituacion,
                            tranc_icod_pvt = Convert.ToInt32(item.tranc_icod_pvt),
                            DesPVT = item.DesPVT
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
        public int insertarTransportista(ETransportista ob)
        {
            int? id_transportista = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TRANSPORTISTA_INSERTAR(
                            ref id_transportista,
                            ob.tranc_iid_transportista,
                            ob.tranc_vnombre_transportista,
                            ob.tranc_vruc,
                            ob.tranc_vdireccion,
                            ob.tranc_vnumero_telefono,
                            ob.tranc_vmicti,
                            ob.tranc_cnumero_dni,
                            ob.tranc_vnombre_conductor,
                            ob.tranc_vnum_licencia_conducir,
                            ob.tranc_vnum_placa,
                            ob.tranc_vnum_matricula,
                            ob.tranc_iid_situacion_transporte,
                            ob.intUsuario,
                            ob.strPc,
                            ob.tranc_icod_pvt
                           );
                }
                return Convert.ToInt32(id_transportista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTransportista(ETransportista ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TRANSPORTISTA_MODIFICAR(
                            ob.tranc_icod_transportista,
                            ob.tranc_iid_transportista,
                            ob.tranc_vnombre_transportista,
                            ob.tranc_vruc,
                            ob.tranc_vdireccion,
                            ob.tranc_vnumero_telefono,
                            ob.tranc_vmicti,
                            ob.tranc_cnumero_dni,
                            ob.tranc_vnombre_conductor,
                            ob.tranc_vnum_licencia_conducir,
                            ob.tranc_vnum_placa,
                            ob.tranc_vnum_matricula,
                            ob.tranc_iid_situacion_transporte,
                            ob.intUsuario,
                            ob.strPc,
                            ob.tranc_icod_pvt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTransportista(ETransportista ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TRANSPORTISTA_ELIMINAR(
                            ob.tranc_icod_transportista,
                            ob.intUsuario,
                            ob.strPc);
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
            EGuiaRemision _be = new EGuiaRemision();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_LISTAR_X_ID(remic_icod_remision);
                    foreach (var item in query)
                    {
                        _be.remic_icod_remision = item.remic_icod_remision;
                        _be.remic_vnumero_remision = item.remic_vnumero_remision;
                        _be.remic_sfecha_remision = item.remic_sfecha_remision;
                        _be.cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente);
                        _be.remic_vnombre_destinatario = item.remic_vnombre_destinatario;
                        _be.remic_vdireccion_destinatario = item.remic_vdireccion_destinatario;
                        _be.remic_vruc_destinatario = item.remic_vruc_destinatario;
                        _be.remic_vdireccion_procedencia = item.remic_vdireccion_procedencia;
                        _be.almac_icod_almacen = item.almac_icod_almacen;
                        _be.tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo);
                        _be.tranc_icod_transportista = Convert.ToInt32(item.tranc_icod_transportista);
                        _be.remic_vlicencia = item.remic_vlicencia;
                        _be.remic_vruc_transportista = item.remic_vruc_transportista;
                        _be.tablc_iid_situacion_remision = Convert.ToInt32(item.tablc_iid_situacion_remision);
                        _be.remic_vreferencia = item.remic_vreferencia;
                        _be.strDesAlmacen = item.strDesAlmacen;
                        _be.strTransportista = item.strTransportista;
                        _be.StrSitucion = item.StrSitucion;
                        _be.remic_sfecha_inicio = Convert.ToDateTime(item.remic_sfecha_inicio);
                        _be.almac_icod_almacen_ingreso = item.almac_icod_almacen_ingreso;
                        _be.strDesAlmaceningreso = item.strDesAlmaceningreso;
                        _be.remic_vmarca_placa = item.remic_vmarca_placa;
                        _be.remic_vcertif_inscripcion = item.remic_vcertif_inscripcion;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _be;
        }
        public List<EGuiaRemision> listarGuiaRemision(int intEjericio)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            remic_sfecha_remision = item.remic_sfecha_remision,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            NomClie = item.NomClie,
                            remic_vnombre_destinatario = item.remic_vnombre_destinatario,
                            remic_vdireccion_destinatario = item.remic_vdireccion_destinatario,
                            remic_vruc_destinatario = item.remic_vruc_destinatario,
                            remic_vdireccion_procedencia = item.remic_vdireccion_procedencia,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            tranc_icod_transportista = Convert.ToInt32(item.tranc_icod_transportista),
                            remic_vlicencia = item.remic_vlicencia,
                            remic_vruc_transportista = item.remic_vruc_transportista,
                            tablc_iid_situacion_remision = Convert.ToInt32(item.tablc_iid_situacion_remision),
                            remic_vreferencia = item.remic_vreferencia,
                            strDesAlmacen = item.strDesAlmacen,
                            strTransportista = item.strTransportista,
                            StrSitucion = item.StrSitucion,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            remic_sfecha_inicio = Convert.ToDateTime(item.remic_sfecha_inicio),
                            almac_icod_almacen_ingreso = item.almac_icod_almacen_ingreso,
                            strDesAlmaceningreso = item.strDesAlmaceningreso,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            CentroCosto = item.CentroCosto,
                            CodProyecto = item.CodProyecto
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
        public List<EGuiaRemision> getGRCabVerificarNumero(string vnumero)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GR_CAB_VERIFICAR_NUEMRO(vnumero);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            remic_icod_remision = item.remic_icod_remision

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
        public int insertarGuiaRemision(EGuiaRemision ob)
        {
            int? id_guia_remision = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_INSERTAR(
                        ref id_guia_remision,
                        ob.remic_vnumero_remision,
                        ob.remic_sfecha_remision,
                        ob.cliec_icod_cliente,
                        ob.remic_vnombre_destinatario,
                        ob.remic_vdireccion_destinatario,
                        ob.remic_vruc_destinatario,
                        ob.remic_vdireccion_procedencia,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_motivo,
                        ob.tranc_icod_transportista,
                        ob.remic_vlicencia,
                        ob.remic_vruc_transportista,
                        ob.tablc_iid_situacion_remision,
                        ob.remic_vreferencia,
                        ob.intUsuario,
                        ob.strPc,
                        ob.remic_sfecha_inicio,
                        ob.almac_icod_almacen_ingreso,
                        ob.pryc_icod_proyecto,
                        ob.cecoc_icod_centro_costo,
                        ob.remic_vmarca_placa,
                        ob.remic_vcertif_inscripcion
                        );
                }
                return Convert.ToInt32(id_guia_remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemision(EGuiaRemision ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_MODIFICAR(
                            ob.remic_icod_remision,
                            ob.remic_vnumero_remision,
                            ob.remic_sfecha_remision,
                            ob.cliec_icod_cliente,
                            ob.remic_vnombre_destinatario,
                            ob.remic_vdireccion_destinatario,
                            ob.remic_vruc_destinatario,
                            ob.remic_vdireccion_procedencia,
                            ob.almac_icod_almacen,
                            ob.tablc_iid_motivo,
                            ob.tranc_icod_transportista,
                            ob.remic_vlicencia,
                            ob.remic_vruc_transportista,
                            ob.tablc_iid_situacion_remision,
                            ob.remic_vreferencia,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_sfecha_inicio,
                            ob.almac_icod_almacen_ingreso,
                            ob.pryc_icod_proyecto,
                            ob.cecoc_icod_centro_costo,
                           ob.remic_vmarca_placa,
                           ob.remic_vcertif_inscripcion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemision_Situ_Tipo_Doc(int remic_icod_remision, int icod_tipo_documento, int remic_icod_doc_venta, int tablc_iid_situacion_remision, int intUsuario, string strPc)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_MODIFICAR_SITUACION_AND_TIPO_DOC(
                            remic_icod_remision,
                            icod_tipo_documento,
                            remic_icod_doc_venta,
                            tablc_iid_situacion_remision,
                            intUsuario,
                            strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemision(EGuiaRemision ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_ELIMINAR(
                            ob.remic_icod_remision,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularGuiaRemision(EGuiaRemision ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_ANULAR(
                            ob.remic_icod_remision
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EGuiaRemisionDet> listarGuiaRemisionDet(int remic_icod_remision, int intEjericio)
        {
            List<EGuiaRemisionDet> lista = new List<EGuiaRemisionDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_DET_LISTAR(remic_icod_remision, intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionDet()
                        {
                            dremc_icod_detalle_remision = item.dremc_icod_detalle_remision,
                            remic_icod_remision = Convert.ToInt32(item.remic_icod_remision),
                            dremc_inro_item = item.dremc_inro_item,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            dremc_ncantidad_producto = item.dremc_ncantidad_producto,
                            dremc_vcantidad_producto = item.dremc_ncantidad_producto.ToString(),
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            dremc_vobservaciones = item.dremc_vobservaciones,
                            dblStockDisponible = Convert.ToDecimal(item.dblStockDisponible),
                            kardc_icod_correlativo_ingreso = item.kardc_icod_correlativo_ingreso
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
        public int insertarGuiaRemisionDet(EGuiaRemisionDet ob)
        {
            int? id_guia_remision = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_DET_INSERTAR(
                        ref id_guia_remision,
                        ob.remic_icod_remision,
                        ob.dremc_inro_item,
                        ob.prdc_icod_producto,
                        ob.dremc_ncantidad_producto,
                        ob.kardc_icod_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.dremc_vobservaciones,
                        ob.kardc_icod_correlativo_ingreso);
                }
                return Convert.ToInt32(id_guia_remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionDet(EGuiaRemisionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_DET_MODIFICAR(
                            ob.dremc_icod_detalle_remision,
                            ob.remic_icod_remision,
                            ob.dremc_inro_item,
                            ob.prdc_icod_producto,
                            ob.dremc_ncantidad_producto,
                            ob.kardc_icod_correlativo,
                            ob.intUsuario,
                            ob.strPc,
                            ob.dremc_vobservaciones);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionDet(EGuiaRemisionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_DET_ELIMINAR(
                            ob.dremc_icod_detalle_remision,
                            ob.intUsuario,
                            ob.strPc);
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
            List<EEspacios> lista = new List<EEspacios>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ESPACIOS_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEspacios()
                        {
                            espac_iid_iespacios = item.espac_iid_iespacios,
                            espac_icod_vespacios = item.espac_icod_vespacios,
                            espac_icod_iplataforma = item.espac_icod_iplataforma,
                            espac_isepultura = item.espac_isepultura,
                            espac_icod_imanzana = item.espac_icod_imanzana,
                            espac_icod_inivel = item.espac_icod_inivel,
                            espac_icod_isituacion = item.espac_icod_isituacion,
                            espac_icod_iestado = item.espac_icod_iestado,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strnivel = item.strnivel,
                            strsituacion = item.strSituacion,
                            strestado = item.strestado,
                            strsepultura = item.strsepultura,
                            codigo = string.Format("{0}-{1}-{2}-{3}", Convert.ToInt32(item.espac_icod_iplataforma), Convert.ToInt32(item.espac_icod_imanzana), Convert.ToInt32(item.espac_isepultura), Convert.ToInt32(item.espac_icod_inivel))
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
        public int insertarEspacios(EEspacios oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_CAB_INSERTAR(
                        ref intIcod,
                        oBe.espac_icod_vespacios,
                        oBe.espac_icod_iplataforma,
                        oBe.espac_isepultura,
                        oBe.espac_icod_imanzana,
                        oBe.espac_icod_inivel,
                        oBe.espac_icod_isituacion,
                        oBe.espac_icod_iestado,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarEspacios(EEspacios oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_CAB_MODIFICAR(
                        oBe.espac_iid_iespacios,
                        oBe.espac_icod_vespacios,
                        oBe.espac_icod_iplataforma,
                        oBe.espac_isepultura,
                        oBe.espac_icod_imanzana,
                        oBe.espac_icod_inivel,
                        oBe.espac_icod_isituacion,
                        oBe.espac_icod_iestado,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_CAB_ELIMINAR(
                        oBe.espac_iid_iespacios,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<EEspaciosDet> lista = new List<EEspaciosDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ESPACIOS_DET_LISTAR(espac_iid_iespacios);
                    foreach (var item in query)
                    {
                        lista.Add(new EEspaciosDet()
                        {
                            espad_iid_iespacios = item.espad_iid_iespacios,
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espad_vnivel = item.espad_vnivel,
                            espad_icod_isituacion = Convert.ToInt32(item.espad_icod_isituacion),
                            espad_icod_iestado = Convert.ToInt32(item.espad_icod_iestado),
                            strsituacion = item.strSituacion,
                            strestado = item.strestado,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            NumContrato = item.NumContrato,
                            NomContratante = item.NomContratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante
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
        public int insertarEspaciosDet(EEspaciosDet oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_DET_INSERTAR(
                        ref intIcod,
                        oBe.espac_iid_iespacios,
                        oBe.espad_vnivel,
                        oBe.espad_icod_isituacion,
                        oBe.espad_icod_iestado,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_DET_MODIFICAR(
                        oBe.espad_iid_iespacios,
                        oBe.espac_iid_iespacios,
                        oBe.espad_vnivel,
                        oBe.espad_icod_isituacion,
                        oBe.espad_icod_iestado,
                        oBe.cntc_icod_contrato,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_DET_ELIMINAR(
                        oBe.espad_iid_iespacios,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_DET_ACTUALIZAR(
                        oBe.espad_iid_iespacios,
                        oBe.espad_icod_isituacion
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_DET_ACTUALIZAR_ESTADO(
                        oBe.espad_iid_iespacios,
                        oBe.espad_icod_iestado
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EEspaciosDet> listarEspaciosConsultas()
        {
            List<EEspaciosDet> lista = new List<EEspaciosDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ESPACIOS_DET_LISTAR_CONSULTA();
                    foreach (var item in query)
                    {
                        lista.Add(new EEspaciosDet()
                        {
                            espad_iid_iespacios = item.espad_iid_iespacios,
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espad_vnivel = item.espad_vnivel,
                            espad_icod_isituacion = Convert.ToInt32(item.espad_icod_isituacion),
                            espad_icod_iestado = Convert.ToInt32(item.espad_icod_iestado),
                            strsituacion = item.strSituacion,
                            strestado = item.strestado,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            NumContrato = item.NumContrato,
                            NomContratante = item.NomContratante,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strsepultura = item.strsepultura,
                            espad_vnom_fallecido = item.espad_vnom_fallecido,
                            espad_vapellido_paterno_fallecido = item.espad_vapellido_paterno_fallecido,
                            espad_vapellido_materno_fallecido = item.espad_vapellido_materno_fallecido,
                            espad_vdni_fallecido = item.espad_vdni_fallecido,
                            espad_sfecha_nac_fallecido = item.espad_sfecha_nac_fallecido,
                            espad_sfecha_fallecido = item.espad_sfecha_fallecido,
                            espad_sfecha_entierro = item.espad_sfecha_entierro,
                            espad_inacionalidad = Convert.ToInt32(item.espad_inacionalidad),
                            espad_thora = item.espad_thora,
                            espad_vsolicitante = item.espad_vsolicitante,
                            espad_vnro_doc = item.espad_vnro_doc,
                            espad_vnom_ape_fallecido = item.espad_vnom_fallecido + " " + item.espad_vapellido_paterno_fallecido + " " + item.espad_vapellido_materno_fallecido,
                            CodigoSepultura = item.CodigoSepultura,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            strdistrito = item.strdistrito,
                            strorigenventa = item.strorigenventa,
                            strtiposepultura = item.strtiposepultura,
                            espac_icod_imanzana = item.espac_icod_imanzana,
                            espac_isepultura = item.espac_isepultura,
                            espac_icod_iplataforma = item.espac_icod_iplataforma,
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
        public void modificarEspaciosDetConsultas(EEspaciosDet oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_DET_MODIFICAR_CONSULTA(
                        oBe.espad_iid_iespacios,
                        oBe.espac_iid_iespacios,
                        oBe.espad_vnivel,
                        oBe.espad_icod_isituacion,
                        oBe.espad_icod_iestado,
                        oBe.cntc_icod_contrato,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.espad_vnom_fallecido,
                        oBe.espad_vapellido_paterno_fallecido,
                        oBe.espad_vapellido_materno_fallecido,
                        oBe.espad_vdni_fallecido,
                        oBe.espad_sfecha_nac_fallecido,
                        oBe.espad_sfecha_fallecido,
                        oBe.espad_sfecha_entierro,
                        oBe.espad_inacionalidad,
                        oBe.espad_thora,
                        oBe.espad_vsolicitante,
                        oBe.espad_vnro_doc
                        );
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
            List<EEspaciosAutorizacionUso> lista = new List<EEspaciosAutorizacionUso>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ESPACIOS_AUTORIZACION_USO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEspaciosAutorizacionUso()
                        {
                            espau_icod_autorizacion_uso = item.espau_icod_autorizacion_uso,
                            espau_iid_autorizacion_uso = Convert.ToInt32(item.espau_iid_autorizacion_uso),
                            espau_sfecha = Convert.ToDateTime(item.espau_sfecha),
                            espau_vnom_fallecido = item.espau_vnom_fallecido,
                            espau_vapellido_paterno_fallecido = item.espau_vapellido_paterno_fallecido,
                            espau_vapellido_materno_fallecido = item.espau_vapellido_materno_fallecido,
                            espau_vdni_fallecido = item.espau_vdni_fallecido,
                            espau_sfecha_nac_fallecido = Convert.ToDateTime(item.espau_sfecha_nac_fallecido),
                            espau_sfecha_fallecido = Convert.ToDateTime(item.espau_sfecha_fallecido),
                            espau_sfecha_entierro = Convert.ToDateTime(item.espau_sfecha_entierro),
                            espau_inacionalidad = Convert.ToInt32(item.espau_inacionalidad),
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            NumContrato = item.NumContrato,
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espad_iid_iespacios = Convert.ToInt32(item.espad_iid_iespacios),
                            espad_vnivel = item.espad_vnivel
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
        public int insertarEspaciosAutorizacionUso(EEspaciosAutorizacionUso oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_AUTORIZACION_USO_INSERTAR(
                        ref intIcod,
                        oBe.espau_iid_autorizacion_uso,
                        oBe.espau_sfecha,
                        oBe.espau_vnom_fallecido,
                        oBe.espau_vapellido_paterno_fallecido,
                        oBe.espau_vapellido_materno_fallecido,
                        oBe.espau_vdni_fallecido,
                        oBe.espau_sfecha_nac_fallecido,
                        oBe.espau_sfecha_fallecido,
                        oBe.espau_sfecha_entierro,
                        oBe.espau_inacionalidad,
                        oBe.cntc_icod_contrato,
                        oBe.espad_iid_iespacios,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_AUTORIZACION_USO_MODIFICAR(
                        oBe.espau_icod_autorizacion_uso,
                        oBe.espau_iid_autorizacion_uso,
                        oBe.espau_sfecha,
                        oBe.espau_vnom_fallecido,
                        oBe.espau_vapellido_paterno_fallecido,
                        oBe.espau_vapellido_materno_fallecido,
                        oBe.espau_vdni_fallecido,
                        oBe.espau_sfecha_nac_fallecido,
                        oBe.espau_sfecha_fallecido,
                        oBe.espau_sfecha_entierro,
                        oBe.espau_inacionalidad,
                        oBe.cntc_icod_contrato,
                        oBe.espad_iid_iespacios,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ESPACIOS_AUTORIZACION_USO_ELIMINAR(
                        oBe.espau_icod_autorizacion_uso,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<EPlantillaProgramacion> lista = new List<EPlantillaProgramacion>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PLANTILLA_PROGRAMACION_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPlantillaProgramacion()
                        {
                            plap_icod_plantilla_programacion = item.plap_icod_plantilla_programacion,
                            plap_inumero_plantilla = Convert.ToInt32(item.plap_inumero_plantilla)
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
        public int insertarPlantillaProgramacion(EPlantillaProgramacion oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANTILLA_PROGRAMACION_CAB_INSERTAR(
                        ref intIcod,
                        oBe.plap_inumero_plantilla,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlantillaProgramacion(EPlantillaProgramacion oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANTILLA_PROGRAMACION_CAB_MODIFICAR(
                        oBe.plap_icod_plantilla_programacion,
                        oBe.plap_inumero_plantilla,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANTILLA_PROGRAMACION_CAB_ELIMINAR(
                        oBe.plap_icod_plantilla_programacion,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<EPlantillaProgramacionDet> lista = new List<EPlantillaProgramacionDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PLANTILLA_PROGRAMACION_DET_LISTAR(espac_iid_iespacios);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlantillaProgramacionDet()
                        {
                            plad_icod_plantilla_programacion_det = item.plad_icod_plantilla_programacion_det,
                            plap_icod_plantilla_programacion = Convert.ToInt32(item.plap_icod_plantilla_programacion),
                            plad_iorden = Convert.ToInt32(item.plad_iorden),
                            plad_vhora_inicial = item.plad_vhora_inicial,
                            plad_vhora_final = item.plad_vhora_final
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
        public int insertarPlantillaProgramacionDet(EPlantillaProgramacionDet oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANTILLA_PROGRAMACION_DET_INSERTAR(
                        ref intIcod,
                        oBe.plap_icod_plantilla_programacion,
                        oBe.plad_iorden,
                        oBe.plad_vhora_inicial,
                        oBe.plad_vhora_final,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANTILLA_PROGRAMACION_DET_MODIFICAR(
                        oBe.plad_icod_plantilla_programacion_det,
                        oBe.plap_icod_plantilla_programacion,
                        oBe.plad_iorden,
                        oBe.plad_vhora_inicial,
                        oBe.plad_vhora_final,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANTILLA_PROGRAMACION_DET_ELIMINAR(
                        oBe.plad_icod_plantilla_programacion_det,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<ERegistroProgramacion> lista = new List<ERegistroProgramacion>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_REGISTRO_PROGRAMACION_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERegistroProgramacion()
                        {
                            rp_icod_registro_programacion = item.rp_icod_registro_programacion,
                            rp_inumero_registro_programacion = Convert.ToInt32(item.rp_inumero_registro_programacion),
                            rp_fecha = Convert.ToDateTime(item.rp_fecha),
                            rp_vobservaciones = item.rp_vobservaciones,
                            plap_icod_plantilla_programacion = Convert.ToInt32(item.plap_icod_plantilla_programacion),
                            NumPlantilla = Convert.ToInt32(item.NumPlantilla)
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
        public int insertarRegistroProgramacion(ERegistroProgramacion oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REGISTRO_PROGRAMACION_CAB_INSERTAR(
                        ref intIcod,
                        oBe.rp_inumero_registro_programacion,
                        oBe.rp_fecha,
                        oBe.rp_vobservaciones,
                        oBe.plap_icod_plantilla_programacion,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRegistroProgramacion(ERegistroProgramacion oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REGISTRO_PROGRAMACION_CAB_MODIFICAR(
                        oBe.rp_icod_registro_programacion,
                        oBe.rp_inumero_registro_programacion,
                        oBe.rp_fecha,
                        oBe.rp_vobservaciones,
                        oBe.plap_icod_plantilla_programacion,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REGISTRO_PROGRAMACION_CAB_ELIMINAR(
                        oBe.rp_icod_registro_programacion,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<ERegistroProgramacionDet> lista = new List<ERegistroProgramacionDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_REGISTRO_PROGRAMACION_DET_LISTAR(espac_iid_iespacios);
                    foreach (var item in query)
                    {
                        lista.Add(new ERegistroProgramacionDet()
                        {
                            rpd_icod_registro_programacion_det = item.rpd_icod_registro_programacion_det,
                            rp_icod_registro_programacion = Convert.ToInt32(item.rp_icod_registro_programacion),
                            rpd_iorden = Convert.ToInt32(item.rpd_iorden),
                            rpd_vhora_inicio = item.rpd_vhora_inicio,
                            rpd_vhora_final = item.rpd_vhora_final,
                            rpd_vnombre_fallecido = item.rpd_vnombre_fallecido,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            espad_iid_iespacios = Convert.ToInt32(item.espad_iid_iespacios),
                            NumContrato = item.NumContrato,
                            strtiposepultura = item.strtiposepultura,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            Nivel = item.Nivel
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
        public int insertarRegistroProgramacionDet(ERegistroProgramacionDet oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REGISTRO_PROGRAMACION_DET_INSERTAR(
                        ref intIcod,
                        oBe.rp_icod_registro_programacion,
                        oBe.rpd_iorden,
                        oBe.rpd_vhora_inicio,
                        oBe.rpd_vhora_final,
                        oBe.rpd_vnombre_fallecido,
                        oBe.cntc_icod_contrato,
                        oBe.espad_iid_iespacios,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REGISTRO_PROGRAMACION_DET_MODIFICAR(
                        oBe.rpd_icod_registro_programacion_det,
                        oBe.rp_icod_registro_programacion,
                        oBe.rpd_iorden,
                        oBe.rpd_vhora_inicio,
                        oBe.rpd_vhora_final,
                        oBe.rpd_vnombre_fallecido,
                        oBe.cntc_icod_contrato,
                        oBe.espad_iid_iespacios,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REGISTRO_PROGRAMACION_DET_ELIMINAR(
                        oBe.rpd_icod_registro_programacion_det,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<EProgramacion> lista = new List<EProgramacion>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PROGRAMACION_LISTAR(fecha);
                    foreach (var item in query)
                    {
                        lista.Add(new EProgramacion()
                        {
                            rpd_icod_registro_programacion_det = Convert.ToInt32(item.rpd_icod_registro_programacion_det),
                            rp_icod_registro_programacion = Convert.ToInt32(item.rp_icod_registro_programacion),
                            rp_fecha = Convert.ToDateTime(item.rp_fecha),
                            rpd_iorden = Convert.ToInt32(item.rpd_iorden),
                            rpd_vhora_inicio = item.rpd_vhora_inicio,
                            rpd_vhora_final = item.rpd_vhora_final,
                            rpd_vnombre_fallecido = item.rpd_vnombre_fallecido,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            espad_iid_iespacios = Convert.ToInt32(item.espad_iid_iespacios),
                            rpd_itipo_sepultura = Convert.ToInt32(item.rpd_itipo_sepultura),
                            rpd_icod_deceso = Convert.ToInt32(item.rpd_icod_deceso),
                            rpd_icod_vendedor = Convert.ToInt32(item.rpd_icod_vendedor),
                            rpd_icod_funeraria = Convert.ToInt32(item.rpd_icod_funeraria),
                            NumContrato = item.NumContrato,
                            strtiposepultura = item.strtiposepultura,
                            strDeceso = item.strDeceso,
                            strNombreVendedor = item.strNombreVendedor,
                            strFuneraria = item.strFuneraria,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strsepultura = item.strsepultura,
                            Nivel = item.Nivel,
                            rpd_vcontrato = item.rpd_vcontrato,
                            rpd_vfuneraria = item.rpd_vfuneraria,
                            rpd_vcontratante = item.rpd_vcontratante,
                            rpd_observaciones = item.rpd_observaciones,
                            rpd_icod_situacion = Convert.ToInt32(item.rpd_icod_situacion)
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
        public void modificarProgramacion(EProgramacion oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PROGRAMACION_MODIFICAR(
                        oBe.rpd_icod_registro_programacion_det,
                        oBe.rpd_vnombre_fallecido,
                        oBe.cntc_icod_contrato,
                        oBe.espad_iid_iespacios,
                        oBe.rpd_itipo_sepultura,
                        oBe.rpd_icod_deceso,
                        oBe.rpd_icod_vendedor,
                        oBe.rpd_icod_funeraria,
                        oBe.rpd_vcontrato,
                        oBe.rpd_vfuneraria,
                        oBe.rpd_vcontratante,
                        oBe.rpd_observaciones,
                        oBe.rpd_icod_situacion
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Lista de Pedido Venta
        public List<EPedidoClienCab> listarPedidoVenta()
        {
            List<EPedidoClienCab> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EPedidoClienCab>();
                    var query = dc.SGEC_PEDIDO_CLIEN_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidoClienCab()
                        {
                            lpedi_icod_cliente = Convert.ToInt32(item.lpedi_icod_cliente),
                            perc_icod_personal = item.perc_icod_personal,
                            lpedi_Numerolista = item.lpedi_Numerolista,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            lpedi_sfecha_cliente = Convert.ToDateTime(item.lpedi_sfecha_cliente),
                            lpedi_Observaciones = item.lpedi_Observaciones,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            perc_vapellidos_nombres = item.perc_vapellidos_nombres,
                            lpedi_isituacion_prov = Convert.ToInt32(item.lpedi_isituacion_prov),
                            StrSituacion = item.StrSituacion,
                            tablc_iid_tipo_ped = Convert.ToInt32(item.tablc_iid_tipo_ped),
                            StrTipoPedido = item.StrTipoPedido
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

        public int insertarPedidoVenta(EPedidoClienCab obj)
        {

            try
            {
                int? lpedi_icod_cliente = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_CAB_INSERTAR(
                        ref lpedi_icod_cliente,
                        obj.perc_icod_personal,
                        obj.lpedi_Numerolista,
                        obj.cliec_icod_cliente,
                        obj.lpedi_sfecha_cliente,
                        obj.lpedi_Observaciones,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpedi_sflag_estado,
                        obj.lpedi_isituacion_prov,
                        obj.tablc_iid_tipo_ped
                    );

                }
                return Convert.ToInt32(lpedi_icod_cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPedidoVenta(EPedidoClienCab obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_CAB_MODIFICAR(
                        obj.lpedi_icod_cliente,
                        obj.lpedi_sfecha_cliente,
                        obj.lpedi_Observaciones,
                        obj.intUsuario,
                        obj.strPc,
                        obj.tablc_iid_tipo_ped
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_CAB_ELIMINAR(
                        obj.lpedi_icod_cliente,
                        obj.intUsuario,
                        obj.strPc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Lista de Pedido Venta Det
        public List<EPedidoClienDet> listarPedidoVentaDet(int lpedi_icod_proveedor, int ANIO)
        {
            List<EPedidoClienDet> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EPedidoClienDet>();
                    var query = dc.SGEC_PEDIDO_CLIEN_DET_LISTAR(lpedi_icod_proveedor, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidoClienDet()
                        {
                            lpedid_icod_cliente = item.lpedid_icod_cliente,
                            lpedi_icod_cliente = item.lpedi_icod_cliente,
                            lpedid_iitem = item.lpedid_iitem,
                            prdc_icod_producto = item.prdc_icod_producto,
                            lpedid_icod_moneda = Convert.ToInt32(item.lpedid_icod_moneda),
                            lpedid_nstock_producto = item.lpedid_nstock_producto,
                            lpedid_nCant_pedido = Convert.ToInt32(item.lpedid_nCant_pedido),
                            lpedid_nprecio_uni = Convert.ToDecimal(item.lpedid_nprecio_uni),
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            strEditorial = item.edit_vdescripcion,
                            prdc_vAutor = item.prdc_vAutor,
                            lpedid_vDesc_moneda = item.strMoneda,
                            intTipoOperacion = item.intOperacion,
                            lpedid_nTotal_precio = Convert.ToDecimal(item.lpedid_nprecio_uni * item.lpedid_nCant_pedido),
                            strCategoria = item.strCategoria,
                            strSubCategoriaUno = item.strSubCategoriaUno,
                            StrUnidadMedida = item.StrUnidadMedida
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

        public int insertarPedidoVentaDet(EPedidoClienDet obj)
        {

            try
            {
                int? lpedid_icod_cliente = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_DET_INSERTAR(
                        ref lpedid_icod_cliente,
                        obj.lpedi_icod_cliente,
                        obj.lpedid_iitem,
                        obj.prdc_icod_producto,
                        obj.lpedid_nstock_producto,
                        obj.lpedid_nCant_pedido,
                        obj.lpedid_icod_moneda,
                        obj.lpedid_nprecio_uni,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpedid_sflag_estado
                    );

                }
                return Convert.ToInt32(lpedid_icod_cliente);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_DET_MODIFICAR(
                        obj.lpedid_icod_cliente,
                        obj.lpedid_iitem,
                        obj.lpedid_nstock_producto,
                        obj.lpedid_nCant_pedido,
                        obj.lpedid_nprecio_uni,
                        obj.intUsuario,
                        obj.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_DET_ELIMINAR(
                        obj.lpedid_icod_cliente,
                        obj.intUsuario,
                        obj.strPc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region EventoVenta
        public List<EEventoVenta> ListarEventoVenta()
        {
            List<EEventoVenta> lista = new List<EEventoVenta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_EVENTO_VENTA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEventoVenta()
                        {
                            evev_icod_evento_venta = item.evev_icod_evento_venta,
                            evev_vnumero_evento_venta = item.evev_vnumero_evento_venta,
                            evev_isituacion_even_venta = item.evev_isituacion_even_venta,
                            evev_vlugar_evento_venta = item.evev_vlugar_evento_venta,
                            evev_vDirecc_evento_venta = item.evev_vDirecc_evento_venta,
                            evev_vcorreo_evento_venta = item.evev_vcorreo_evento_venta,
                            evev_vcontac_evento_venta = item.evev_vcontac_evento_venta,
                            evev_vTelefo_evento_venta = item.evev_vTelefo_evento_venta,
                            evev_sfecha_evento_inicio = item.evev_sfecha_evento_inicio,
                            evev_sfecha_evento_final = item.evev_sfecha_evento_final,
                            almac_icod_almacen = item.almac_icod_almacen,
                            almac_vresponsa_even_venta = item.almac_vresponsa_even_venta,
                            almac_vdescripcion = item.evev_vDirecc_evento_venta,
                            desSituacion = item.desSituacion,
                            evev_vnombre_evento_venta = item.evev_vnombre_evento_venta
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
        public int? InsertarEventoVenta(EEventoVenta ob)
        {
            int? evev_icod_evento_venta = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_EVENTO_VENTA_INSERTAR(
                        ref evev_icod_evento_venta,
                        ob.evev_vnumero_evento_venta,
                        ob.evev_isituacion_even_venta,
                        ob.evev_vlugar_evento_venta,
                        ob.evev_vDirecc_evento_venta,
                        ob.evev_vcorreo_evento_venta,
                        ob.evev_vcontac_evento_venta,
                        ob.evev_vTelefo_evento_venta,
                        ob.evev_sfecha_evento_inicio,
                        ob.evev_sfecha_evento_final,
                        ob.almac_icod_almacen,
                        ob.almac_vresponsa_even_venta,
                        ob.intUsuario,
                        DateTime.Now,
                        ob.strPc,
                        null,
                        null,
                        null,
                        ob.evev_flag_estado,
                        ob.evev_vnombre_evento_venta);
                }
                return evev_icod_evento_venta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarEventoVenta(EEventoVenta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_EVENTO_VENTA_MODIFICAR(
                        ob.evev_icod_evento_venta,
                        ob.evev_vnumero_evento_venta,
                        ob.evev_isituacion_even_venta,
                        ob.evev_vlugar_evento_venta,
                        ob.evev_vDirecc_evento_venta,
                        ob.evev_vcorreo_evento_venta,
                        ob.evev_vcontac_evento_venta,
                        ob.evev_vTelefo_evento_venta,
                        ob.evev_sfecha_evento_inicio,
                        ob.evev_sfecha_evento_final,
                        ob.almac_icod_almacen,
                        ob.almac_vresponsa_even_venta,
                        ob.intUsuario,
                        DateTime.Now,
                        ob.strPc,
                        null, null, null,
                        ob.evev_vnombre_evento_venta);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_EVENTO_VENTA_ELIMINAR(evev_icod_evento_venta, intUsuario, strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Proyectos
        public List<EProyectos> listarProyectos()
        {
            List<EProyectos> lista = new List<EProyectos>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EProyectos>();
                    var query = dc.SGE_PROYECTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProyectos()
                        {
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            pryc_ianio = Convert.ToInt32(item.pryc_ianio),
                            pryc_vcorrelativo = item.pryc_vcorrelativo,
                            pryc_vdescripcion = item.pryc_vdescripcion,
                            pryc_vdireccion_prov = item.pryc_vdireccion_prov,
                            pryc_icod_cliente = Convert.ToInt32(item.pryc_icod_cliente),
                            NomCliente = item.NomCliente,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            StrAlmacen = item.StrAlmacen,
                            StrAlmacenDir = item.StrAlmacenDir,
                            RUC = item.cliec_cruc,
                            pryc_icod_ccosto = Convert.ToInt32(item.pryc_icod_ccosto),
                            CentroCosto = item.CentroCosto,
                            pryc_sfecha_inicio = Convert.ToDateTime(item.pryc_sfecha_inicio),
                            pryc_sfecha_emtrega = Convert.ToDateTime(item.pryc_sfecha_emtrega),
                            pryc_nrentabilidad = Convert.ToDecimal(item.pryc_nrentabilidad),
                            pryc_icod_acta_entrega = Convert.ToInt32(item.pryc_icod_acta_entrega),
                            strActaEntrega = item.strActaEntrega,
                            pryc_iestado = Convert.ToInt32(item.pryc_iestado),
                            strEstado = item.strEstado,
                            NumHojaCosto = item.NumHojaCosto,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo)

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

        public void CuotasActualizarDocEliminado(int? tdocc_icod_tipo_doc, int doxcc_num_comprobante_referencia)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CUOTAS_ACTUALIZAR_DOC_ELIMINADO(doxcc_num_comprobante_referencia, tdocc_icod_tipo_doc);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int insertarProyectos(EProyectos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_PROYECTOS_INSERTAR(
                    ref intIcod,
                    oBe.pryc_ianio,
                    oBe.pryc_vcorrelativo,
                    oBe.pryc_vdescripcion,
                    oBe.pryc_icod_cliente,
                    oBe.almac_icod_almacen,
                    oBe.pryc_vdireccion_prov,
                    oBe.pryc_icod_ccosto,
                    oBe.pryc_sfecha_inicio,
                    oBe.pryc_sfecha_emtrega,
                    oBe.pryc_icod_acta_entrega,
                    oBe.pryc_nrentabilidad,
                    oBe.pryc_iestado,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.pryc_flag_estado,
                    oBe.hjcc_icod_hoja_costo);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProyectos(EProyectos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROYECTOS_MODIFICAR(
                    oBe.pryc_icod_proyecto,
                    oBe.pryc_vcorrelativo,
                    oBe.pryc_vdescripcion,
                    oBe.pryc_icod_cliente,
                    oBe.almac_icod_almacen,
                    oBe.pryc_vdireccion_prov,
                    oBe.pryc_icod_ccosto,
                    oBe.pryc_sfecha_inicio,
                    oBe.pryc_sfecha_emtrega,
                    oBe.pryc_icod_acta_entrega,
                    oBe.pryc_nrentabilidad,
                    oBe.pryc_iestado,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.hjcc_icod_hoja_costo);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProyectos(EProyectos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROYECTOS_ELIMINAR(
                        oBe.pryc_icod_proyecto,
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

        #region Archivos
        public List<EArchivos> listarArchivos(int codigo)
        {
            List<EArchivos> lista = new List<EArchivos>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EArchivos>();
                    var query = dc.SGE_ARCHIVOS_LISTAR(codigo);
                    foreach (var item in query)
                    {
                        lista.Add(new EArchivos()
                        {
                            arch_icod_archivos = Convert.ToInt32(item.arch_icod_archivos),
                            arch_iid_correlativo = Convert.ToInt32(item.arch_iid_correlativo),
                            arch_iid_orden_compra_local = Convert.ToInt32(item.arch_iid_orden_compra_local),
                            arch_vdescripcion = item.arch_vdescripcion,
                            arch_vruta = item.arch_vruta,
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
        public int insertarArchivos(EArchivos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_ARCHIVOS_INSERTAR(
                    ref intIcod,
                    oBe.arch_iid_correlativo,
                    oBe.arch_iid_orden_compra_local,
                    oBe.arch_vdescripcion,
                    oBe.arch_vruta,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.arch_flag_estado);
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ARCHIVOS_MODIFICAR(
                    oBe.arch_icod_archivos,
                    oBe.arch_iid_correlativo,
                    oBe.arch_iid_orden_compra_local,
                    oBe.arch_vdescripcion,
                    oBe.arch_vruta,
                    oBe.intUsuario,
                    oBe.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ARCHIVOS_ELIMINAR(
                        oBe.arch_icod_archivos,
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


        #region Hoja Costos
        public List<EHojaCostos> listarHojaCostos()
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostos()
                        {
                            hjcc_icod_hoja_costo = item.hjcc_icod_hoja_costo,
                            hjcc_vnumero_hoja_costo = item.hjcc_vnumero_hoja_costo,
                            hjcc_sfecha_hoja_costo = Convert.ToDateTime(item.hjcc_sfecha_hoja_costo),
                            hjcc_ntipo_cambio = Convert.ToInt32(item.hjcc_ntipo_cambio),
                            tablc_iid_situacion_hc = Convert.ToInt32(item.tablc_iid_situacion_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            pryc_vdescripcion = item.pryc_vdescripcion,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            hjcc_vdescripcion = item.hjcc_vdescripcion,
                            hjcc_nmonto_presup = Convert.ToInt32(item.hjcc_nmonto_presup),
                            hjcc_nmonto_real = Convert.ToInt32(item.hjcc_nmonto_real),
                            hjcc_ntotal_soles = Convert.ToDecimal(item.hjcc_ntotal_soles),
                            hjcc_ntotal_dolares = Convert.ToDecimal(item.hjcc_ntotal_dolares),

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
        public int insertarHojaCostos(EHojaCostos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_INSERTAR(
                        ref intIcod,
                        oBe.hjcc_vnumero_hoja_costo,
                        oBe.hjcc_sfecha_hoja_costo,
                        oBe.hjcc_ntipo_cambio,
                        oBe.tablc_iid_situacion_hc,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.hjcc_vdescripcion,
                        oBe.hjcc_nmonto_presup,
                        oBe.hjcc_nmonto_real,
                        oBe.hjcc_ntotal_soles,
                        oBe.hjcc_ntotal_dolares,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarPagosCuota(int? tdocc_icod_tipo_doc, string ncrec_vnumero_documento)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ELIMINAR_PAGOS_CUOTA(tdocc_icod_tipo_doc, ncrec_vnumero_documento);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void modificarHojaCostos(EHojaCostos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_MODIFICAR(
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcc_sfecha_hoja_costo,
                        oBe.hjcc_ntipo_cambio,
                        oBe.tablc_iid_situacion_hc,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.hjcc_vdescripcion,
                        oBe.hjcc_nmonto_presup,
                        oBe.hjcc_nmonto_real,
                        oBe.hjcc_ntotal_soles,
                        oBe.hjcc_ntotal_dolares,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_ELIMINAR(
                        oBe.hjcc_icod_hoja_costo,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EHojaCostos> getHCCabVerificarNumero(string vnumero, int ANIO)
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_HC_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostos()
                        {
                            hjcc_icod_hoja_costo = item.hjcc_icod_hoja_costo

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
        #endregion
        #region Hoja Costo Conceptos
        public List<EHojaCostosConceptos> listarHojaCostosConceptos(int Concepto)
        {
            List<EHojaCostosConceptos> lista = new List<EHojaCostosConceptos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_CONCEPTO_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostosConceptos()
                        {
                            hjcd1_icod_detalle_hc = item.hjcd1_icod_detalle_hc,
                            hjcd1_iitem_orden = item.hjcd1_iitem_orden,
                            hjcd1_vcodigo_concepto_hc = item.hjcd1_vcodigo_concepto_hc,
                            hjcd1_vdescripcion = item.hjcd1_vdescripcion,
                            hjcd1_nmonto_presup = Convert.ToDecimal(item.hjcd1_nmonto_presup),
                            hjcd1_nmonto_real = Convert.ToDecimal(item.hjcd1_nmonto_real)

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
        public int insertarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_CONCEPTO_INSERTAR(
                        ref intIcod,
                         oBe.hjcd1_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcd1_vcodigo_concepto_hc,
                        oBe.hjcd1_vdescripcion,
                        oBe.hjcd1_nmonto_presup,
                        oBe.hjcd1_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcd1_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_CONCEPTO_MODIFICAR(
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.hjcd1_iitem_orden,
                        oBe.hjcd1_vcodigo_concepto_hc,
                        oBe.hjcd1_vdescripcion,
                        oBe.hjcd1_nmonto_presup,
                        oBe.hjcd1_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_CONCEPTO_ELIMINAR(
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_LISTAR(SubConcepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EhojaCostosSubConceptos()
                        {
                            hjcd2_icod_subconcepto_hc = item.hjcd2_icod_subconcepto_hc,
                            hjcd2_iitem_orden = item.hjcd2_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            hjcc1_iiten = item.hjcc1_iiten,
                            hjcd2_vcodigo_concepto_hc = item.hjcd2_vcodigo_concepto_hc,
                            hjcd2_vdescripcion = item.hjcd2_vdescripcion,
                            hjcd2_nmonto_presup = Convert.ToDecimal(item.hjcd2_nmonto_presup),
                            hjcd2_nmonto_real = Convert.ToDecimal(item.hjcd2_nmonto_real)

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
        public int insertarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_INSERTAR(
                        ref intIcod,
                        oBe.hjcd2_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcd1_icod_concepto_hc,
                        oBe.hjcc1_iiten,
                        oBe.hjcd2_vcodigo_concepto_hc,
                        oBe.hjcd2_vdescripcion,
                        oBe.hjcd2_nmonto_presup,
                        oBe.hjcd2_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcd2_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_MODIFICAR(
                        oBe.hjcd2_icod_subconcepto_hc,
                        oBe.hjcd2_iitem_orden,
                        oBe.hjcd2_vcodigo_concepto_hc,
                        oBe.hjcd2_vdescripcion,
                        oBe.hjcd2_nmonto_presup,
                        oBe.hjcd2_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_ELIMINAR(
                        oBe.hjcd2_icod_subconcepto_hc,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_RUBROS_LISTAR(Rubros);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostosRubros()
                        {
                            hjcd3_icod_rubros_hc = item.hjcd3_icod_rubros_hc,
                            hjcd3_iitem_orden = item.hjcd3_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            hjcd2_iitem = item.hjcd2_iitem,
                            hjcd3_vcodigo_relacion = item.hjcd3_vcodigo_relacion,
                            hjcd3_vcodigo_concepto_hc = item.hjcd3_vcodigo_concepto_hc,
                            hjcd3_ncantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            hjcd3_unidc_icod_unidad_medida = Convert.ToInt32(item.hjcd3_unidc_icod_unidad_medida),
                            Unidad = item.Unidad,
                            hjcd3_vdescripcion = item.hjcd3_vdescripcion,
                            tablc_icod_tipo_moneda = Convert.ToInt32(item.tablc_icod_tipo_moneda),
                            Moneda = item.Moneda,
                            TipoModena = item.TipoModena,
                            hjcd3_nmonto_unitario = Convert.ToDecimal(item.hjcd3_nmonto_unitario),
                            hjcd3_nmonto_real = Convert.ToDecimal(item.hjcd3_nmonto_real),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            str_producto = item.str_producto,
                            hjcd3_ncantidad_requerida = Convert.ToDecimal(item.hjcd3_ncantidad_requerida),
                            hjcd3_ncantidad_autorizada = Convert.ToDecimal(item.hjcd3_ncantidad_autorizada),
                            hjcd3_ncantidad_atendida = Convert.ToDecimal(item.hjcd3_ncantidad_atendida),
                            hjcd3_ncantidad_saldo = Convert.ToDecimal(item.hjcd3_ncantidad_saldo),
                            tablc_icod_tipo_rubro = Convert.ToInt32(item.tablc_icod_tipo_rubro)

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
        public List<EHojaCostosRubros> listarHojaCostosRubrosRQM(int Rubros)
        {
            List<EHojaCostosRubros> lista = new List<EHojaCostosRubros>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_RUBROS_RQM_LISTAR(Rubros);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostosRubros()
                        {
                            hjcd3_icod_rubros_hc = item.hjcd3_icod_rubros_hc,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            hjcd2_iitem = item.hjcd2_iitem,
                            hjcd3_vcodigo_relacion = item.hjcd3_vcodigo_relacion,
                            hjcd3_vcodigo_concepto_hc = item.hjcd3_vcodigo_concepto_hc,
                            hjcd3_ncantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            hjcd3_unidc_icod_unidad_medida = Convert.ToInt32(item.hjcd3_unidc_icod_unidad_medida),
                            Unidad = item.Unidad,
                            hjcd3_vdescripcion = item.hjcd3_vdescripcion,
                            tablc_icod_tipo_moneda = Convert.ToInt32(item.tablc_icod_tipo_moneda),
                            Moneda = item.Moneda,
                            TipoModena = item.TipoModena,
                            hjcd3_nmonto_unitario = Convert.ToDecimal(item.hjcd3_nmonto_unitario),
                            hjcd3_nmonto_real = Convert.ToDecimal(item.hjcd3_nmonto_real),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            str_producto = item.str_producto,
                            hjcd3_ncantidad_atendida = Convert.ToDecimal(item.hjcd3_ncantidad_atendida),
                            hjcd3_ncantidad_saldo = Convert.ToDecimal(item.hjcd3_ncantidad_saldo),
                            tablc_icod_tipo_rubro = Convert.ToInt32(item.tablc_icod_tipo_rubro),
                            hjcd3_ncantidad_requerida = Convert.ToDecimal(item.hjcd3_ncantidad_requerida)

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
        public int insertarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_RUBROS_INSERTAR(
                        ref intIcod,
                        oBe.hjcd3_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcd2_icod_subconcepto_hc,
                        oBe.hjcd2_iitem,
                        oBe.hjcd3_vcodigo_relacion,
                        oBe.hjcd3_vcodigo_concepto_hc,
                        oBe.hjcd3_ncantidad,
                        oBe.hjcd3_unidc_icod_unidad_medida,
                        oBe.hjcd3_vdescripcion,
                        oBe.tablc_icod_tipo_moneda,
                        oBe.hjcd3_nmonto_unitario,
                        oBe.hjcd3_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcd3_flag_estado,
                        oBe.prdc_icod_producto,
                        oBe.hjcd3_ncantidad_requerida,
                        oBe.hjcd3_ncantidad_autorizada,
                        oBe.hjcd3_ncantidad_atendida,
                        oBe.hjcd3_ncantidad_saldo,
                        oBe.tablc_icod_tipo_rubro
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_RUBROS_MODIFICAR(
                        oBe.hjcd3_icod_rubros_hc,
                        oBe.hjcd3_iitem_orden,
                        oBe.hjcd3_vcodigo_relacion,
                        oBe.hjcd3_vcodigo_concepto_hc,
                        oBe.hjcd3_ncantidad,
                        oBe.hjcd3_unidc_icod_unidad_medida,
                        oBe.hjcd3_vdescripcion,
                        oBe.tablc_icod_tipo_moneda,
                        oBe.hjcd3_nmonto_unitario,
                        oBe.hjcd3_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.prdc_icod_producto,
                        oBe.hjcd3_ncantidad_requerida,
                        oBe.hjcd3_ncantidad_autorizada,
                        oBe.hjcd3_ncantidad_atendida,
                        oBe.hjcd3_ncantidad_saldo,
                        oBe.tablc_icod_tipo_rubro
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_RUBROS_ELIMINAR(
                        oBe.hjcd3_icod_rubros_hc,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_REQUERIMIENTOS_MATERIALES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMateriales()
                        {
                            rqmc_icod_requerimiento_materiales = item.rqmc_icod_requerimiento_materiales,
                            rqmc_numero_req_material = item.rqmc_numero_req_material,
                            rqmc_sfecha_req_material = Convert.ToDateTime(item.rqmc_sfecha_req_material),
                            tablc_iid_tipo_requerimiento = Convert.ToInt32(item.tablc_iid_tipo_requerimiento),
                            Tipo = item.Tipo,
                            tablc_iid_situación_hc = Convert.ToInt32(item.tablc_iid_situación_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            rqmc_vdescripcion = item.rqmc_vdescripcion,
                            rqmc_bautorizado = Convert.ToBoolean(item.rqmc_bautorizado),
                            NomCliente = item.NomCliente,
                            NumHojaCosto = item.NumHojaCosto,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo)


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
        public List<ERequerimientoMateriales> listarAutorizacionRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMateriales()
                        {
                            rqmc_icod_requerimiento_materiales = item.rqmc_icod_requerimiento_materiales,
                            rqmc_numero_req_material = item.rqmc_numero_req_material,
                            rqmc_sfecha_req_material = Convert.ToDateTime(item.rqmc_sfecha_req_material),
                            tablc_iid_tipo_requerimiento = Convert.ToInt32(item.tablc_iid_tipo_requerimiento),
                            Tipo = item.Tipo,
                            tablc_iid_situación_hc = Convert.ToInt32(item.tablc_iid_situación_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            rqmc_vdescripcion = item.rqmc_vdescripcion,
                            rqmc_bautorizado = Convert.ToBoolean(item.rqmc_bautorizado),
                            NomCliente = item.NomCliente,
                            NumHojaCosto = item.NumHojaCosto


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
        public List<ERequerimientoMateriales> listarVerificacionStockRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMateriales()
                        {
                            rqmc_icod_requerimiento_materiales = item.rqmc_icod_requerimiento_materiales,
                            rqmc_numero_req_material = item.rqmc_numero_req_material,
                            rqmc_sfecha_req_material = Convert.ToDateTime(item.rqmc_sfecha_req_material),
                            tablc_iid_tipo_requerimiento = Convert.ToInt32(item.tablc_iid_tipo_requerimiento),
                            Tipo = item.Tipo,
                            tablc_iid_situación_hc = Convert.ToInt32(item.tablc_iid_situación_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            rqmc_vdescripcion = item.rqmc_vdescripcion,
                            rqmc_bautorizado = Convert.ToBoolean(item.rqmc_bautorizado),
                            NomCliente = item.NomCliente,
                            NumHojaCosto = item.NumHojaCosto


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
        public int insertarRequerimientoMateriales(ERequerimientoMateriales oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_INSERTAR(
                        ref intIcod,
                        oBe.rqmc_numero_req_material,
                        oBe.rqmc_sfecha_req_material,
                        oBe.tablc_iid_situación_hc,
                        oBe.tablc_iid_tipo_requerimiento,
                        oBe.pryc_icod_proyecto,
                        oBe.rqmc_vdescripcion,
                        oBe.rqmc_bautorizado,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rqmc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRequerimientoMateriales(ERequerimientoMateriales oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_MODIFICAR(
                        oBe.rqmc_icod_requerimiento_materiales,
                        oBe.rqmc_sfecha_req_material,
                        oBe.tablc_iid_situación_hc,
                        oBe.tablc_iid_tipo_requerimiento,
                        oBe.pryc_icod_proyecto,
                        oBe.rqmc_vdescripcion,
                        oBe.rqmc_bautorizado,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_ELIMINAR(
                        oBe.rqmc_icod_requerimiento_materiales,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularRequerimientoMateriales(int rqmc_icod_requerimiento_materiales, int tablc_iid_situación_hc)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_ANULAR(rqmc_icod_requerimiento_materiales, tablc_iid_situación_hc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_ELIMINAR(
                        oBe.rqmc_icod_requerimiento_materiales,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMaterialesDetalle()
                        {
                            rqmd_icod_req_materiales_detalle = item.rqmd_icod_req_materiales_detalle,
                            rqmd_vcodigo_item_requerim = item.rqmd_vcodigo_item_requerim,
                            hjcd3_icod_rubros_hc = Convert.ToInt32(item.hjcd3_icod_rubros_hc),
                            rqmd_cantidad_pedida = Convert.ToDecimal(item.rqmd_cantidad_pedida),
                            rqmd_cantidad_aprobada = Convert.ToDecimal(item.rqmd_cantidad_aprobada),
                            DescripcionRubro = item.hjcd3_vdescripcion,
                            CodigoRubro = item.hjcd3_vcodigo_relacion,
                            Medida = item.Unidad,
                            Cantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto)


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
        public List<ERequerimientoMaterialesDetalle> listarAutorizacionRequerimientoMaterialesDetalle(int Concepto)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_DETALLE_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMaterialesDetalle()
                        {
                            rqmd_icod_req_materiales_detalle = item.rqmd_icod_req_materiales_detalle,
                            rqmd_vcodigo_item_requerim = item.rqmd_vcodigo_item_requerim,
                            hjcd3_icod_rubros_hc = Convert.ToInt32(item.hjcd3_icod_rubros_hc),
                            rqmd_cantidad_pedida = Convert.ToDecimal(item.rqmd_cantidad_pedida),
                            rqmd_cantidad_aprobada = Convert.ToDecimal(item.rqmd_cantidad_aprobada),
                            DescripcionRubro = item.hjcd3_vdescripcion,
                            CodigoRubro = item.hjcd3_vcodigo_relacion,
                            Medida = item.Unidad,
                            Cantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto)


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
        public int insertarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.rqmc_icod_requerimiento_materiales,
                        oBe.rqmd_vcodigo_item_requerim,
                        oBe.hjcd3_icod_rubros_hc,
                        oBe.rqmd_cantidad_pedida,
                        oBe.rqmd_cantidad_aprobada,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rqmd_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_MODIFICAR(
                        oBe.rqmd_icod_req_materiales_detalle,
                        oBe.rqmd_vcodigo_item_requerim,
                        oBe.hjcd3_icod_rubros_hc,
                        oBe.rqmd_cantidad_pedida,
                        oBe.rqmd_cantidad_aprobada,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_ELIMINAR(
                        oBe.rqmd_icod_req_materiales_detalle,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_DETALLE_ELIMINAR(
                        oBe.rqmd_icod_req_materiales_detalle
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Conceptos Costos
        public List<EConceptosCostos> listarConceptosCostos()
        {
            List<EConceptosCostos> lista = new List<EConceptosCostos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CONCEPTO_COSTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptosCostos()
                        {
                            chc_icod_detalle_hc = item.chc_icod_detalle_hc,
                            chc_iitem_orden = item.chc_iitem_orden,
                            chc_vcodigo_concepto_hc = item.chc_vcodigo_concepto_hc,
                            chc_vdescripcion = item.chc_vdescripcion,
                            chc_nmonto_presup = Convert.ToDecimal(item.chc_nmonto_presup),
                            chc_nmonto_real = Convert.ToDecimal(item.chc_nmonto_real)

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
        public int insertarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_INSERTAR(
                        ref intIcod,
                         oBe.chc_iitem_orden,
                        oBe.chc_icod_hoja_costo,
                        oBe.chc_vcodigo_concepto_hc,
                        oBe.chc_vdescripcion,
                        oBe.chc_nmonto_presup,
                        oBe.chc_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.chc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_MODIFICAR(
                        oBe.chc_icod_detalle_hc,
                        oBe.chc_iitem_orden,
                        oBe.chc_vcodigo_concepto_hc,
                        oBe.chc_vdescripcion,
                        oBe.chc_nmonto_presup,
                        oBe.chc_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_ELIMINAR(
                        oBe.chc_icod_detalle_hc,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CONCEPTO_COSTOS_DETALLE_LISTAR(SubConcepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptosCostosDetalle()
                        {
                            chcd_icod_subconcepto_hc = item.chcd_icod_subconcepto_hc,
                            chcd_iitem_orden = item.chcd_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            chc_iiten = item.chc_iiten,
                            chcd_vcodigo_concepto_hc = item.chcd_vcodigo_concepto_hc,
                            chcd_vdescripcion = item.chcd_vdescripcion,
                            chcd_nmonto_presup = Convert.ToDecimal(item.chcd_nmonto_presup),
                            chcd_nmonto_real = Convert.ToDecimal(item.chcd_nmonto_real)

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
        public List<EConceptosCostosDetalle> listarConceptosCostosDetalle2()
        {
            List<EConceptosCostosDetalle> lista = new List<EConceptosCostosDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CONCEPTO_COSTOS_DETALLE2_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptosCostosDetalle()
                        {
                            chcd_icod_subconcepto_hc = item.chcd_icod_subconcepto_hc,
                            chcd_iitem_orden = item.chcd_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            chc_iiten = item.chc_iiten,
                            chcd_vcodigo_concepto_hc = item.chcd_vcodigo_concepto_hc,
                            chcd_vdescripcion = item.chcd_vdescripcion,
                            chcd_nmonto_presup = Convert.ToDecimal(item.chcd_nmonto_presup),
                            chcd_nmonto_real = Convert.ToDecimal(item.chcd_nmonto_real)

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
        public int insertarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.chcd_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.chc_icod_concepto_hc,
                        oBe.chc_iiten,
                        oBe.chcd_vcodigo_concepto_hc,
                        oBe.chcd_vdescripcion,
                        oBe.chcd_nmonto_presup,
                        oBe.chcd_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.chcd_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_DETALLE_MODIFICAR(
                        oBe.chcd_icod_subconcepto_hc,
                        oBe.chcd_iitem_orden,
                        oBe.chcd_vcodigo_concepto_hc,
                        oBe.chcd_vdescripcion,
                        oBe.chcd_nmonto_presup,
                        oBe.chcd_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_DETALLE_ELIMINAR(
                        oBe.chcd_icod_subconcepto_hc,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Suma Cantidad Requeridad
        public void ActualizarCantidadRequerida(int hjcd3_icod_rubros_hc, int prdc_icod_producto)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_REQUERIDA_RQM(hjcd3_icod_rubros_hc, prdc_icod_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Listar Cantidad Requerida RQM
        public decimal listarCantidadRequeidaRQM(int hjcd3_icod_rubros_hc, int prdc_icod_producto)
        {
            decimal? Cantidad_Saldo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CANTIDAD_REQUERIDA_RQM(
                        ref Cantidad_Saldo,
                        hjcd3_icod_rubros_hc,
                        prdc_icod_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToDecimal(Cantidad_Saldo);
        }
        #endregion

        #region Garantia Clientes
        public List<EGarantiaClientes> listarGarantiaClientes()
        {
            List<EGarantiaClientes> lista = new List<EGarantiaClientes>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_GARANTIA_CLIENTES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EGarantiaClientes()
                        {
                            garc_icod_garantia = item.garc_icod_garantia,
                            garc_vnumero_garantia = item.garc_vnumero_garantia,
                            garc_sfecha_garantia = Convert.ToDateTime(item.garc_sfecha_garantia),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            Situacion = item.Situacion,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            NomClie = item.NomClie,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            garc_nmonto = Convert.ToDecimal(item.garc_nmonto),
                            favc_icod_factura = Convert.ToInt32(item.favc_icod_factura),
                            NumDoc = item.NumDoc,
                            intDXP = Convert.ToInt64(item.intDXP),
                            pdxpc_icod_correlativo = Convert.ToInt64(item.pdxpc_icod_correlativo)

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
        public int insertarGarantiaClientes(EGarantiaClientes oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_CLIENTES_INSERTAR(
                        ref intIcod,
                        oBe.garc_vnumero_garantia,
                        oBe.garc_sfecha_garantia,
                        oBe.tablc_iid_situacion,
                        oBe.cliec_icod_cliente,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.garc_nmonto,
                        oBe.favc_icod_factura,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.garc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGarantiaClientes(EGarantiaClientes oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_CLIENTES_MODIFICAR(
                        oBe.garc_icod_garantia,
                        oBe.garc_vnumero_garantia,
                        oBe.garc_sfecha_garantia,
                        oBe.tablc_iid_situacion,
                        oBe.cliec_icod_cliente,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.garc_nmonto,
                        oBe.favc_icod_factura,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGarantiaClientes(EGarantiaClientes oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_CLIENTES_ELIMINAR(
                        oBe.garc_icod_garantia,
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

        #region VentasDaot

        public List<EVentasDaot> ListarVentasDaot(decimal monto, int anio)
        {
            List<EVentasDaot> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EVentasDaot>();
                    var query = dc.SIGTS_VENTAS_DAOT_LISTAR(monto, anio);
                    foreach (var item in query)
                    {
                        string[] nombres = { string.Empty, string.Empty };
                        string app = string.Empty, apm = string.Empty;
                        if (item.cliec_vnombres != null && item.tip_doc_cliente != null && item.tip_doc_cliente == 1)
                        {
                            nombres[0] = item.cliec_vnombres.Split(' ')[0];
                            nombres[0] = (item.cliec_vnombres.Split(' ').Count() == 2) ? item.cliec_vnombres.Split(' ')[1] : string.Empty;
                            app = item.cliec_vapellido_paterno;
                            apm = item.cliec_vapellido_materno;
                        }
                        lista.Add(new EVentasDaot()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            valor_venta_dolares = item.valor_venta_dolares,
                            valor_venta_soles = item.valor_venta_soles,
                            tip_doc_cliente = item.tip_doc_cliente,
                            tipo_persona = item.tipo_persona,
                            num_doc_cliente = item.num_doc_cliente,
                            cliec_vnombre1 = nombres[0],
                            cliec_vnombre2 = nombres[1],
                            cliec_vapellido_paterno = app,
                            cliec_vapellido_materno = apm
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

        public List<EVentasDaotDetalle> ListarVentasDaotDetallexCliente(long proc_icod_proveedor, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EVentasDaotDetalle>();
                    var query = dc.SIGTS_VENTAS_DAOT_DETALLE_X_CLIENTE_LISTAR(proc_icod_proveedor, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentasDaotDetalle()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            simboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            valor_venta_soles = item.valor_venta_soles,
                            valor_venta_dolares = item.valor_venta_dolares,
                            valor_venta_dolares_str = (item.tablc_iid_tipo_moneda == 1) ? string.Empty : Convert.ToDecimal(item.valor_venta_dolares).ToString("n2"),
                            valor_venta_mond_orig = item.valor_venta_mond_orig,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion
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

        public List<EVentasDaotDetalle> ListarVentasDaotDetalle(decimal monto, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EVentasDaotDetalle>();
                    var query = dc.SIGTS_VENTAS_DAOT_DETALLE_LISTAR(monto, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentasDaotDetalle()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            simboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            valor_venta_soles = item.valor_venta_soles,
                            valor_venta_dolares = item.valor_venta_dolares,
                            valor_venta_dolares_str = (item.tablc_iid_tipo_moneda == 1) ? string.Empty : Convert.ToDecimal(item.valor_venta_dolares).ToString("n2"),
                            valor_venta_mond_orig = item.valor_venta_mond_orig,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
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

        #endregion


        #region Caja
        public List<ECaja> listarCaja()
        {
            List<ECaja> lista = new List<ECaja>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ECaja>();
                    var query = dc.SGE_CAJA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECaja()
                        {
                            cajac_icod_caja = item.cajac_icod_caja,
                            cajac_vcod_caja = item.cajac_vcod_caja,
                            puvec_icod_punto_venta = item.puvec_icod_punto_venta,
                            puvec_vcod_punto_venta = item.puvec_vcod_punto_venta,
                            puvec_vdescripcion = item.puvec_vdescripcion,
                            cajac_inro_serie_factura = item.cajac_inro_serie_factura,
                            cajac_inro_serie_boleta = item.cajac_inro_serie_boleta,
                            cajac_inro_serie_nota_credito = item.cajac_inro_serie_nota_credito,
                            cajac_icorrelativo_factura = item.cajac_icorrelativo_factura,
                            cajac_icorrelativo_boleta = item.cajac_icorrelativo_boleta,
                            cajac_icorrelativo_nota_credito = item.cajac_icorrelativo_nota_credito,
                            cajac_vdescripcion = item.cajac_vdescripcion,
                            cajac_vNombreLocal = item.cajac_vNombreLocal,
                            cajac_vDirecLocal = item.cajac_vDirecLocal,
                            cajac_vNumeroSerie = item.cajac_vNumeroSerie,
                            cajac_vSerieImpres = item.cajac_vSerieImpres,
                            cajac_iNumCorrelTck = item.cajac_iNumCorrelTck
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
        public int insertarCaja(ECaja ob)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_CAJA_INSERTAR(
                    ref intIcod,
                    ob.cajac_vcod_caja,
                    ob.puvec_icod_punto_venta,
                    ob.cajac_vdescripcion,
                    ob.cajac_inro_serie_factura,
                    ob.cajac_inro_serie_boleta,
                    ob.cajac_inro_serie_nota_credito,
                    ob.cajac_icorrelativo_factura,
                    ob.cajac_icorrelativo_boleta,
                    ob.cajac_icorrelativo_nota_credito,
                    ob.intUsuario,
                    ob.strPc);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCaja(ECaja ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJA_MODIFICAR(
                    ob.cajac_icod_caja,
                    ob.cajac_vcod_caja,
                    ob.puvec_icod_punto_venta,
                    ob.cajac_vdescripcion,
                    ob.cajac_inro_serie_factura,
                    ob.cajac_inro_serie_boleta,
                    ob.cajac_inro_serie_nota_credito,
                    ob.cajac_icorrelativo_factura,
                    ob.cajac_icorrelativo_boleta,
                    ob.cajac_icorrelativo_nota_credito,
                    ob.intUsuario,
                    ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJA_ELIMINAR(
                        oBe.cajac_icod_caja,
                        oBe.intUsuario,
                        oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ValidarCodigoCaja()
        {
            int codigocaja = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VALIDAR_NUMERO_CAJA();
                    foreach (var item in query)
                    {
                        codigocaja = Convert.ToInt32(item.CodigoCaja);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return codigocaja;

        }
        #endregion
        #region Vendedor
        public List<EVendedor> listarVendedor()
        {
            List<EVendedor> lista = new List<EVendedor>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VENDEDOR_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EVendedor()
                        {
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            vendc_iid_vendedor = item.vendc_iid_vendedor,
                            vendc_vnombre_vendedor = item.vendc_vnombre_vendedor,
                            vendc_vdireccion = item.vendc_vdireccion,
                            vendc_vnumero_telefono = item.vendc_vnumero_telefono,
                            vendc_cnumero_dni = item.vendc_cnumero_dni,
                            tablc_iid_situacion_vendedor = item.tablc_iid_situacion_vendedor,
                            vdescripcion_situacion_vendedor = item.Situacion,
                            vendc_tipo_vendedor = item.vendc_tipo_vendedor,
                            TipoVendedor = item.TipoVendedor,
                            vendc_vpassword_vendedor = item.vendc_vpassword_vendedor,
                            vendc_icod_pvt = item.vendc_icod_pvt,
                            vendc_vcorreo = item.vendc_vcorreo,
                            zonc_icod_zona = Convert.ToInt32(item.zonc_icod_zona),
                            zonc_vdescripcion = item.zonc_vdescripcion,
                            vendc_vcod_vendedor = item.vendc_vcod_vendedor
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
        public int insertarVendedor(EVendedor ob)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_VENDEDOR_INSERTAR(
                        ref intIcod,
                        ob.vendc_iid_vendedor,
                        ob.vendc_vnombre_vendedor,
                        ob.vendc_vdireccion,
                        ob.vendc_vnumero_telefono,
                        ob.vendc_cnumero_dni,
                        ob.tablc_iid_situacion_vendedor,
                        ob.vendc_vpassword_vendedor,
                        ob.intUsuario,
                        ob.strPc,
                        ob.vendc_tipo_vendedor,
                        ob.vendc_icod_pvt,
                        ob.vendc_vcorreo,
                        ob.zonc_icod_zona,
                        ob.vendc_vcod_vendedor
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarVendedor(EVendedor ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_VENDEDOR_MODIFICAR(
                        ob.vendc_icod_vendedor,
                        ob.vendc_iid_vendedor,
                        ob.vendc_vnombre_vendedor,
                        ob.vendc_vdireccion,
                        ob.vendc_vnumero_telefono,
                        ob.vendc_cnumero_dni,
                        ob.tablc_iid_situacion_vendedor,
                        ob.vendc_vpassword_vendedor,
                        ob.intUsuario,
                        ob.strPc,
                        ob.vendc_tipo_vendedor,
                        ob.vendc_icod_pvt,
                        ob.vendc_vcorreo,
                        ob.zonc_icod_zona,
                        ob.vendc_vcod_vendedor
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_VENDEDOR_ELIMINAR
                        (
                        objEVendedor.vendc_icod_vendedor,
                        objEVendedor.intUsuario,
                        objEVendedor.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PEDIDOS_PVT_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidosPVT()
                        {
                            pdvc_icod_pedido = item.pdvc_icod_pedido,
                            pdvc_numero_pedido = item.pdvc_numero_pedido,
                            pdvc_sfecha_pedido = Convert.ToDateTime(item.pdvc_sfecha_pedido),
                            tablc_iid_situación = Convert.ToInt32(item.tablc_iid_situación),
                            Situacion = item.Situacion,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            pdvc_vcliente = item.pdvc_vcliente,
                            pdvc_vobservaciones = item.pdvc_vobservaciones,
                            Ruc = item.Ruc
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
        public int insertarPedidosPVT(EPedidosPVT oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PEDIDOS_PVT_INSERTAR(
                        ref intIcod,
                        oBe.pdvc_numero_pedido,
                        oBe.pdvc_sfecha_pedido,
                        oBe.tablc_iid_situación,
                        oBe.cliec_icod_cliente,
                        oBe.pdvc_vcliente,
                        oBe.pdvc_vobservaciones,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPedidosPVT(EPedidosPVT oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PEDIDOS_PVT_MODIFICAR(
                        oBe.pdvc_icod_pedido,
                        oBe.pdvc_numero_pedido,
                        oBe.pdvc_sfecha_pedido,
                        oBe.tablc_iid_situación,
                        oBe.cliec_icod_cliente,
                        oBe.pdvc_vcliente,
                        oBe.pdvc_vobservaciones,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PEDIDOS_PVT_ELIMINAR(
                        oBe.pdvc_icod_pedido,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PEDIDOS_PVT_DETALLE_LISTAR(pdvc_icod_pedido);
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidosPVTDetalle()
                        {
                            pdvd_icod_pedido_detalle = item.pdvd_icod_pedido_detalle,
                            pdvd_iid_pedido_detalle = Convert.ToInt32(item.pdvd_iid_pedido_detalle),
                            pdvc_icod_pedido = Convert.ToInt32(item.pdvc_icod_pedido),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            CodPro = item.CodPro,
                            DesLarga = item.DesLarga,
                            DesCorta = item.DesCorta,
                            AbreUM = item.AbreUM,
                            pdvd_ncantidad = Convert.ToDecimal(item.pdvd_ncantidad),
                            pdvd_nprecio_unitario = Convert.ToDecimal(item.pdvd_nprecio_unitario),
                            pdvd_nprecio_total = Convert.ToDecimal(item.pdvd_nprecio_total),
                            Indicador = Convert.ToInt32(item.Indicador)
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
        public int insertarPedidosPVTDetalle(EPedidosPVTDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PEDIDOS_PVT_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.pdvd_iid_pedido_detalle,
                        oBe.pdvc_icod_pedido,
                        oBe.prdc_icod_producto,
                        oBe.pdvd_ncantidad,
                        oBe.pdvd_nprecio_unitario,
                        oBe.pdvd_nprecio_total,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PEDIDOS_PVT_DETALLE_MODIFICAR(
                        oBe.pdvd_icod_pedido_detalle,
                        oBe.pdvd_iid_pedido_detalle,
                        oBe.pdvc_icod_pedido,
                        oBe.prdc_icod_producto,
                        oBe.pdvd_ncantidad,
                        oBe.pdvd_nprecio_unitario,
                        oBe.pdvd_nprecio_total,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PEDIDOS_PVT_DETALLE_ELIMINAR(
                        oBe.pdvd_icod_pedido_detalle,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Planilla Venta Diario
        public List<EFacturaCab> listarFacturaCabPlanilla(int intEjericio)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PLANILLA_FACTURA_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCab()
                        {
                            favc_icod_factura = item.favc_icod_factura,
                            favc_vnumero_factura = item.favc_vnumero_factura,
                            favc_sfecha_factura = item.favc_sfecha_factura,
                            favc_sfecha_vencim_factura = item.favc_sfecha_vencim_factura,
                            favc_icod_cliente = item.favc_icod_cliente,
                            clic_vcod_cliente = item.cliec_vcod_cliente,
                            favc_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            favc_vruc = item.cliec_cruc,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            pdvc_icod_pedido = Convert.ToInt32(item.pdvc_icod_pedido),
                            pdvc_numero_pedido = item.pdvc_numero_pedido,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = item.tablc_iid_situacion,
                            favc_npor_imp_igv = item.favc_npor_imp_igv,
                            favc_nmonto_neto = item.favc_nmonto_neto,
                            favc_nmonto_imp = item.favc_nmonto_imp,
                            favc_nmonto_total = item.favc_nmonto_total,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            favc_vobservacion = item.favc_vobservacion,
                            favc_bincluye_igv = Convert.ToBoolean(item.favc_bincluye_igv),
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            cliec_nnumero_dias = Convert.ToInt32(item.cliec_nnumero_dias),
                            remic_icod_remision = item.remic_icod_remision,
                            //remic_vnumero_remision = item.remic_vnumero_remision,
                            favc_bafecto_igv = Convert.ToBoolean(item.favc_bafecto_igv),
                            desc_nFactura_porc = Convert.ToDecimal(item.desc_nFactura_porc),
                            desc_nMonto = Convert.ToDecimal(item.desc_nMonto),
                            nmonto_nDescuento = Convert.ToDecimal(item.nmonto_nDescuento),
                            nmonto_nSubTotal = Convert.ToDecimal(item.nmonto_nSubTotal),
                            favc_nmonto_neto_exo = Convert.ToDecimal(item.favc_nmonto_neto_exo),
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cliec_vnumero_doc_cli = item.cliec_vnumero_doc_cli
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
        public int insertarFacturaPlanilla(EFacturaCab ob)
        {
            int? id_factura = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_FACTURA_CAB_INSERTAR(
                             ref id_factura,
                            ob.favc_vnumero_factura,
                            ob.favc_sfecha_factura,
                            ob.favc_sfecha_vencim_factura,
                            ob.cliec_icod_cliente,
                            ob.pdvc_icod_pedido,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.tablc_iid_situacion,
                            ob.favc_npor_imp_igv,
                            ob.favc_nmonto_neto,
                            ob.favc_nmonto_imp,
                            ob.favc_nmonto_total,
                            ob.doxcc_icod_correlativo,
                            ob.favc_vobservacion,
                            ob.favc_bincluye_igv,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_icod_remision,
                            ob.favc_bafecto_igv,
                            ob.desc_nFactura_porc,
                            ob.desc_nMonto,
                            ob.nmonto_nDescuento,
                            ob.nmonto_nSubTotal,
                            ob.favc_nmonto_neto_exo,
                            ob.cntc_icod_contrato
                            );
                }
                return Convert.ToInt32(id_factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaPlanilla(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_FACTURA_CAB_MODIFICAR(
                             ob.favc_icod_factura,
                            ob.favc_vnumero_factura,
                            ob.favc_sfecha_factura,
                            ob.favc_sfecha_vencim_factura,
                            ob.cliec_icod_cliente,
                            ob.pdvc_icod_pedido,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.tablc_iid_situacion,
                            ob.favc_npor_imp_igv,
                            ob.favc_nmonto_neto,
                            ob.favc_nmonto_imp,
                            ob.favc_nmonto_total,
                            ob.favc_vobservacion,
                            ob.favc_bincluye_igv,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_icod_remision,
                            ob.favc_bafecto_igv,
                            ob.desc_nFactura_porc,
                            ob.desc_nMonto,
                            ob.nmonto_nDescuento,
                            ob.nmonto_nSubTotal,
                            ob.favc_nmonto_neto_exo,
                            ob.cntc_icod_contrato
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaPlanilla(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_FACTURA_CAB_ELIMINAR(
                            ob.favc_icod_factura,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EFacturaDet> listarFacturaDetallePlanilla(int intFactura, int aNIO)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PLANILLA_FACTURA_DET_LISTAR(intFactura, aNIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaDet()
                        {
                            favd_icod_item_factura = item.favd_icod_item_factura,
                            favc_icod_factura = item.favc_icod_factura,
                            favd_iitem_factura = Convert.ToInt32(item.favd_iitem_factura),
                            favd_ncantidad = item.favd_ncantidad,
                            favd_vncantidad = item.favd_ncantidad.ToString(),
                            favd_vdescripcion = item.favd_vdescripcion,
                            favd_nprecio_unitario_item = Convert.ToInt32(item.favd_nprecio_unitario_item),
                            favd_vnprecio_unitario_item = item.favd_nprecio_unitario_item.ToString(),
                            favd_nmonto_impuesto_item = Convert.ToInt32(item.favd_nmonto_impuesto_item),
                            favd_nporcentaje_descuento_item = Convert.ToInt32(item.favd_nporcentaje_descuento_item),
                            favd_nprecio_total_item = Convert.ToInt32(item.favd_nprecio_total_item),
                            favd_vnprecio_total_item = item.favd_nprecio_total_item.ToString(),
                            strMoneda = item.strMoneda,
                            OrdenItemImprimir = 1,
                            favd_nneto_exo = Convert.ToInt32(item.favd_nneto_exo),
                            favd_nobservaciones = item.favd_nobservaciones,

                            NumeroOrdenItem = item.favd_iitem_factura,
                            cantidad = item.favd_ncantidad,
                            unidadMedida = "ZZ",
                            ValorVentaItem = Convert.ToDecimal(item.favd_nprecio_total_item),
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal((item.favd_nprecio_total_item - item.favd_nmonto_impuesto_item)),
                            PorcentajeIGVItem = 18,
                            MontoInafectoItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? Convert.ToDecimal(item.favd_nprecio_total_item) : 0,
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.favd_vdescripcion + " " + item.favd_nobservaciones,
                            codigoItem = "SERV0" + item.favd_iitem_factura,
                            ObservacionesItem = "",
                            ValorUnitarioItem = Convert.ToDecimal(item.favd_nprecio_unitario_item),
                            PrecioVentaUnitarioItem = Convert.ToDecimal((((item.favd_nprecio_total_item) + item.favd_nmonto_impuesto_item) / item.favd_ncantidad)),
                            tipoImpuesto = item.favd_nmonto_impuesto_item == 0 ? "30" : "10",
                            favd_iicod_tipo_pago = Convert.ToInt32(item.favd_iicod_tipo_pago),
                            strTipoServicio = item.strTipoServicio

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
        public void insertarFacturaDetallePlanilla(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_FACTURA_DET_INSERTAR(
                            ob.favc_icod_factura,
                            ob.favd_iitem_factura,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nporcentaje_descuento_item,
                            ob.favd_nprecio_total_item,
                            ob.intUsuario,
                            ob.strPc,
                            ob.favd_nneto_exo,
                            ob.favd_nobservaciones,
                            ob.favd_iicod_tipo_pago
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaDetallePlanilla(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_FACTURA_DET_MODIFICAR(
                            ob.favd_icod_item_factura,
                            ob.favd_iitem_factura,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nporcentaje_descuento_item,
                            ob.favd_nprecio_total_item,
                            ob.intUsuario,
                            ob.strPc,
                            ob.favd_nneto_exo,
                            ob.favd_nobservaciones,
                            ob.favd_iicod_tipo_pago
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaDetallePlanilla(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_FACTURA_DET_ELIMINAR(
                            ob.favd_icod_item_factura,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EBoletaCab> listarBoletaCabPlanilla(int intEjericio)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PLANILLA_BOLETA_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaCab()
                        {
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovc_vnumero_boleta = item.bovc_vnumero_boleta,
                            bovc_sfecha_boleta = item.bovc_sfecha_boleta,
                            bovc_sfecha_vencim_boleta = item.bovc_sfecha_vencim_boleta,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            cliec_cruc = item.cliec_cruc,
                            pdvc_icod_pedido = Convert.ToInt32(item.pdvc_icod_pedido),
                            pdvc_numero_pedido = item.pdvc_numero_pedido,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            bovc_npor_imp_igv = item.bovc_npor_imp_igv,
                            bovc_nmonto_neto = item.bovc_nmonto_neto,
                            bovc_nmonto_imp = item.bovc_nmonto_imp,
                            bovc_nmonto_total = item.bovc_nmonto_total,
                            cliec_vnombre_cliente = item.strDesCliente,
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            bovc_vobservacion = item.bovc_vobservacion,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            cliec_nnumero_dias = Convert.ToInt32(item.cliec_nnumero_dias),
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            bovc_vnombre_cliente = item.bovc_vnombre_cliente,
                            bovc_vdireccion_cliente = item.bovc_vdireccion_cliente,
                            bovc_vDNI_cliente = item.bovc_vDNI_cliente,
                            bfavc_bafecto_igv = Convert.ToBoolean(item.bfavc_bafecto_igv),
                            desc_nFactura_porc = Convert.ToDecimal(item.desc_nFactura_porc),
                            desc_nMonto = Convert.ToDecimal(item.desc_nMonto),
                            nmonto_nDescuento = Convert.ToDecimal(item.nmonto_nDescuento),
                            nmonto_nSubTotal = Convert.ToDecimal(item.nmonto_nSubTotal),
                            bovc_nmonto_neto_exo = Convert.ToInt32(item.bovc_nmonto_neto_exo),
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cliec_vnumero_doc_cli = item.cliec_vnumero_doc_cli
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
        public int insertarBoletaPlanilla(EBoletaCab ob)
        {
            int? bovc_icod_boleta = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_BOLETA_CAB_INSERTAR(
                            ref bovc_icod_boleta,
                            ob.bovc_vnumero_boleta,
                            ob.bovc_sfecha_boleta,
                            ob.bovc_sfecha_vencim_boleta,
                            ob.doxcc_icod_correlativo,
                            ob.cliec_icod_cliente,
                            ob.pdvc_icod_pedido,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.bovc_npor_imp_igv,
                            ob.bovc_nmonto_neto,
                            ob.bovc_nmonto_imp,
                            ob.bovc_nmonto_total,
                            ob.anio,
                            ob.intUsuario,
                            ob.strPc,
                            ob.bovc_vobservacion,
                            ob.remic_icod_remision,
                            ob.bovc_vnombre_cliente,
                            ob.bovc_vdireccion_cliente,
                            ob.bovc_vDNI_cliente,
                            ob.bfavc_bafecto_igv,
                            ob.desc_nFactura_porc,
                            ob.desc_nMonto,
                            ob.nmonto_nDescuento,
                            ob.nmonto_nSubTotal,
                            ob.bovc_nmonto_neto_exo,
                            ob.cntc_icod_contrato
                            );
                }
                return Convert.ToInt32(bovc_icod_boleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaPlanilla(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_BOLETA_CAB_MODIFICAR(
                    ob.bovc_icod_boleta,
                    ob.bovc_vnumero_boleta,
                    ob.bovc_sfecha_boleta,
                    ob.bovc_sfecha_vencim_boleta,
                    ob.cliec_icod_cliente,
                    ob.pdvc_icod_pedido,
                    ob.tablc_iid_tipo_moneda,
                    ob.tablc_iid_forma_pago,
                    ob.bovc_npor_imp_igv,
                    ob.bovc_nmonto_neto,
                    ob.bovc_nmonto_imp,
                    ob.bovc_nmonto_total,
                    ob.intUsuario,
                    ob.strPc,
                    ob.bovc_vobservacion,
                    ob.bovc_vnombre_cliente,
                    ob.bovc_vdireccion_cliente,
                    ob.bovc_vDNI_cliente,
                    ob.bfavc_bafecto_igv,
                    ob.desc_nFactura_porc,
                    ob.desc_nMonto,
                    ob.nmonto_nDescuento,
                    ob.nmonto_nSubTotal,
                    ob.bovc_nmonto_neto_exo,
                    ob.cntc_icod_contrato
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaPlanilla(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_BOLETA_CAB_ELIMINAR(
                            ob.bovc_icod_boleta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EBoletaDet> listarBoletaDetallePlanilla(int intFactura)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PLANILLA_BOLETA_DET_LISTAR(intFactura);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaDet()
                        {
                            bovd_icod_item_boleta = item.bovd_icod_item_boleta,
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovd_iitem_boleta = Convert.ToInt32(item.bovd_iitem_boleta),
                            bovd_ncantidad = item.bovd_ncantidad,
                            bovd_vdescripcion = item.bovd_vdescripcion,
                            bovd_nprecio_unitario_item = item.bovd_nprecio_unitario_item,
                            bovd_nmonto_impuesto_item = Convert.ToInt32(item.bovd_nmonto_impuesto_item),
                            bovd_nporcentaje_descuento_item = Convert.ToInt32(item.bovd_nporcentaje_descuento_item),
                            bovd_nprecio_total_item = item.bovd_nprecio_total_item,
                            strMoneda = item.strMoneda,
                            dblStockDisponible = item.dblStockDisponible,
                            bolvd_vobservaciones = item.bolvd_vobservaciones,
                            bovd_nneto_exo = Convert.ToInt32(item.bovd_nneto_exo),

                            NumeroOrdenItem = item.bovd_iitem_boleta,
                            cantidad = item.bovd_ncantidad,
                            unidadMedida = "ZZ",
                            ValorVentaItem = item.bovd_nprecio_total_item,
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.bovd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal(item.bovd_nmonto_impuesto_item),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.bovd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal(item.bovd_nmonto_impuesto_item),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.bovd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal((item.bovd_nprecio_total_item - item.bovd_nmonto_impuesto_item)),
                            PorcentajeIGVItem = 18,
                            MontoInafectoItem = Convert.ToDecimal(item.bovd_nmonto_impuesto_item) == 0 ? item.bovd_nprecio_total_item : 0,
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.bovd_vdescripcion + " " + item.bolvd_vobservaciones.Trim(),
                            codigoItem = "SERV0" + item.bovd_iitem_boleta,
                            ObservacionesItem = "",
                            ValorUnitarioItem = item.bovd_nprecio_unitario_item,
                            tipoImpuesto = item.bovd_nmonto_impuesto_item == 0 ? "30" : "10",
                            favd_iicod_tipo_pago = Convert.ToInt32(item.favd_iicod_tipo_pago),
                            strTipoServicio = item.strTipoServicio
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
        public void insertarBoletaDetallePlanilla(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_BOLETA_DET_INSERTAR(
                     ob.bovc_icod_boleta
                    , ob.bovd_iitem_boleta
                    , ob.bovd_ncantidad
                    , ob.bovd_vdescripcion
                    , ob.bovd_nprecio_unitario_item
                    , ob.bovd_nmonto_impuesto_item
                    , ob.bovd_nporcentaje_descuento_item
                    , ob.bovd_nprecio_total_item
                    , ob.bolvd_vobservaciones
                    , ob.intUsuario
                    , ob.strPc
                    , ob.bovd_flag_estado
                    , ob.bovd_nneto_exo
                    , ob.favd_iicod_tipo_pago
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaDetallePlanilla(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_BOLETA_DET_MODIFICAR(
                     ob.bovd_icod_item_boleta
                    , ob.bovd_iitem_boleta
                    , ob.bovd_ncantidad
                    , ob.bovd_vdescripcion
                    , ob.bovd_nprecio_unitario_item
                    , ob.bovd_nmonto_impuesto_item
                    , ob.bovd_nporcentaje_descuento_item
                    , ob.bovd_nprecio_total_item
                    , ob.bolvd_vobservaciones
                    , ob.intUsuario
                    , ob.strPc
                    , ob.bovd_nneto_exo
                    , ob.favd_iicod_tipo_pago
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaDetallePlanilla(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLANILLA_BOLETA_DET_ELIMINAR(
                            ob.bovd_icod_item_boleta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public void ActualizarIdDxcFactura(int intFactura, Int64 intDXC)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_FACTURA_CAB_SET_DXC_ICOD(
                            intFactura,
                            intDXC
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarIdDxcBoleta(int intFactura, Int64 intDXC)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_BOLETA_CAB_SET_DXC_ICOD(
                            intFactura,
                            intDXC
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Mantenimiento Lista Precio 
        public List<EListaPrecio> listarListaPrecio()
        {
            List<EListaPrecio> lista = new List<EListaPrecio>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LISTA_PRECIO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EListaPrecio()
                        {
                            lprecc_icod_precio = item.lprecc_icod_precio,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            Unidad = item.Unidad,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            lprecc_nmonto_unitario = Convert.ToDecimal(item.lprecc_nmonto_unitario),
                            lprecc_indicador_rango = Convert.ToBoolean(item.lprecc_indicador_rango),
                            IndicadorRango = item.IndicadorRango
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
        public int insertarListaPrecio(EListaPrecio obe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_LISTA_PRECIO_INSERTAR(
                       ref intIcod,
                       obe.prdc_icod_producto,
                       obe.tablc_iid_tipo_moneda,
                       obe.lprecc_nmonto_unitario,
                       obe.lprecc_indicador_rango,
                       obe.intUsuario,
                       obe.strPc
                       );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_LISTA_PRECIO_MODIFICAR
                        (
                            obe.lprecc_icod_precio,
                            obe.prdc_icod_producto,
                            obe.tablc_iid_tipo_moneda,
                            obe.lprecc_nmonto_unitario,
                            obe.lprecc_indicador_rango,
                            obe.intUsuario,
                            obe.strPc
                            );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_LISTA_PRECIO_ELIMINAR
                        (
                        obe.lprecc_icod_precio,
                        obe.intUsuario,
                        obe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Registro Lista Precio Detalle
        public List<EListaPrecioDetalle> listarListaPrecioDetalle(int lprecc_icod_precio)
        {
            List<EListaPrecioDetalle> lista = new List<EListaPrecioDetalle>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LISTA_PRECIO_DETALLE_LISTAR(lprecc_icod_precio);
                    foreach (var item in query)
                    {
                        lista.Add(new EListaPrecioDetalle()
                        {
                            lprecd_icod_precio_detalle = item.lprecd_icod_precio_detalle,
                            lprecc_icod_precio = Convert.ToInt32(item.lprecc_icod_precio),
                            lprecd_icantidad_inicial = Convert.ToInt32(item.lprecd_icantidad_inicial),
                            lprecd_icantidad_final = Convert.ToInt32(item.lprecd_icantidad_final),
                            lprecd_nmonto_unitario = Convert.ToDecimal(item.lprecd_nmonto_unitario)
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
        public int insertarListaPrecioDetalle(EListaPrecioDetalle obe)
        {

            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_LISTA_PRECIO_DETALLE_INSERTAR(
                    ref intIcod,
                    obe.lprecc_icod_precio,
                    obe.lprecd_icantidad_inicial,
                    obe.lprecd_icantidad_final,
                    obe.lprecd_nmonto_unitario,
                    obe.intUsuario,
                    obe.strPc
                    );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarListaPrecioDetalle(EListaPrecioDetalle obe)
        {
            int? Id = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_LISTA_PRECIO_DETALLE_MODIFICAR(
                        obe.lprecd_icod_precio_detalle,
                        obe.lprecc_icod_precio,
                        obe.lprecd_icantidad_inicial,
                        obe.lprecd_icantidad_final,
                        obe.lprecd_nmonto_unitario,
                        obe.intUsuario,
                        obe.strPc
                       );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_LISTA_PRECIO_DETALLE_ELIMINAR(
                        obe.lprecd_icod_precio_detalle,
                        obe.intUsuario,
                        obe.strPc
                        );
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
            List<ETicketera> lista = new List<ETicketera>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ETicketera>();
                    var query = dc.SGE_TICKETERA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETicketera()
                        {
                            tckc_icod_ticketera = item.tckc_icod_ticketera,
                            tckc_inumero_impresora = Convert.ToInt32(item.tckc_inumero_impresora),
                            tckc_vserie_impresora = item.tckc_vserie_impresora,
                            tckc_vnombre_local = item.tckc_vnombre_local,
                            tckc_vdireccion = item.tckc_vdireccion,
                            tckc_vserie = item.tckc_vserie,
                            tckc_vcorrelativo = item.tckc_vcorrelativo,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            Situacion = item.Situacion
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
        public int insertarTicketera(ETicketera ob)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_TICKETERA_INSERTAR(
                    ref intIcod,
                    ob.tckc_inumero_impresora,
                    ob.tckc_vserie_impresora,
                    ob.tckc_vnombre_local,
                    ob.tckc_vdireccion,
                    ob.tckc_vserie,
                    ob.tckc_vcorrelativo,
                    ob.tablc_iid_situacion,
                    ob.intUsuario,
                    ob.strPc);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTicketera(ETicketera ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TICKETERA_MODIFICAR(
                    ob.tckc_icod_ticketera,
                    ob.tckc_inumero_impresora,
                    ob.tckc_vserie_impresora,
                    ob.tckc_vnombre_local,
                    ob.tckc_vdireccion,
                    ob.tckc_vserie,
                    ob.tckc_vcorrelativo,
                    ob.tablc_iid_situacion,
                    ob.intUsuario,
                    ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TICKETERA_ELIMINAR(
                        oBe.tckc_icod_ticketera,
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
        #region Asignacion Vendedor
        public List<EAsignacionVendedor> listarAsignacionVendedor(int cajac_icod_caja)
        {
            List<EAsignacionVendedor> lista = new List<EAsignacionVendedor>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EAsignacionVendedor>();
                    var query = dc.SGE_ASIGNACION_VENDEDOR_LISTAR(cajac_icod_caja);
                    foreach (var item in query)
                    {
                        lista.Add(new EAsignacionVendedor()
                        {
                            asigc_icod_asignacion = item.asigc_icod_asignacion,
                            cajac_icod_caja = Convert.ToInt32(item.cajac_icod_caja),
                            tablc_iid_turno = Convert.ToInt32(item.tablc_iid_turno),
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            asigc_vpassword_vendedor = item.asigc_vpassword_vendedor,
                            Turno = item.Turno,
                            NomVendedor = item.NomVendedor,
                            DesPedido = item.DesPedido
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
        public int insertarAsignacionVendedor(EAsignacionVendedor ob)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_ASIGNACION_VENDEDOR_INSERTAR(
                    ref intIcod,
                    ob.cajac_icod_caja,
                    ob.tablc_iid_turno,
                    ob.vendc_icod_vendedor,
                    ob.asigc_vpassword_vendedor,
                    ob.intUsuario,
                    ob.strPc);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAsignacionVendedor(EAsignacionVendedor ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ASIGNACION_VENDEDOR_MODIFICAR(
                    ob.asigc_icod_asignacion,
                    ob.cajac_icod_caja,
                    ob.tablc_iid_turno,
                    ob.vendc_icod_vendedor,
                    ob.asigc_vpassword_vendedor,
                    ob.intUsuario,
                    ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ASIGNACION_VENDEDOR_ELIMINAR(
                        oBe.asigc_icod_asignacion,
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
        #region Parametro de Ventas
        public List<EParametro> listarParametroVenta()
        {
            List<EParametro> lista = new List<EParametro>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EParametro>();
                    var query = dc.SGE_PARAMETRO_VENTAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EParametro()
                        {
                            pm_icod_parametro = item.pm_icod_parametro,
                            pmv_icod_almacen = Convert.ToInt32(item.pmv_icod_almacen),
                            DesAlmacen = item.DesAlmacen
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
        public void modificarParametroVenta(EParametro ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PARAMETRO_VENTAS_MODIFICAR(
                    ob.pm_icod_parametro,
                    ob.pmv_icod_almacen,
                    ob.intUsuario);
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
            List<EOrdenDespacho> lista = new List<EOrdenDespacho>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ORDEN_DESPACHO_LISTAR_X_MES(año, mes);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenDespacho()
                        {
                            desac_icod_despacho = item.desac_icod_despacho,
                            desac_ianio = item.desac_ianio,
                            desac_vnumero_despacho = item.desac_vnumero_despacho,
                            desac_sfecha_despacho = Convert.ToDateTime(item.desac_sfecha_despacho),
                            desac_iid_situacion_despacho = item.desac_iid_situacion_despacho,
                            tablc_iid_motivo_despacho = item.tablc_iid_motivo_despacho,
                            almac_icod_almacen_salida = Convert.ToInt32(item.almac_icod_almacen_salida),
                            almac_icod_almacen_salida_OD = item.almac_icod_almacen_salida_OD,
                            AlmacenSalida = item.AlmacenSalida,
                            almac_icod_almacen_ingreso = Convert.ToInt32(item.almac_icod_almacen_ingreso),
                            AlmacenIngreso = item.AlmacenIngreso,
                            desac_vatencion = item.desac_vatencion,
                            desac_ventregar = item.desac_ventregar,
                            desac_ventregar_arroba = item.desac_ventregar.Replace("@", " - "),
                            desac_vreferencia = item.desac_vreferencia,
                            desac_vpartida = item.desac_vpartida,
                            tranc_icod_transportista = item.tranc_icod_transportista,
                            Transportista = item.Transportista,
                            tranc_vnum_matricula = item.tranc_vnum_matricula,
                            tranc_vruc = item.tranc_vruc,
                            Situacion = item.Situacion,
                            desac_icod_despacho_pedido = Convert.ToInt32(item.desac_icod_despacho_pedido),
                            //vNumeroPedido = item.vNumeroPedido,
                            tipo_borden_despacho = Convert.ToBoolean(item.tipo_borden_despacho),
                            desac_vnumero_parte = item.desac_vnumero_parte,
                            desac_sfecha_devolucion = item.desac_sfecha_devolucion,
                            remic_icod_remision = item.remic_icod_remision,
                            desac_vguia_remision = item.desac_vguia_remision,
                            desac_vplaca_vehiculo = item.desac_vplaca_vehiculo,
                            //KardexXRe = Convert.ToDecimal(item.KardexXRe),
                            desac_bautoriza_modif = item.desac_bautoriza_modif,
                            desac_bautoriza_modif_dev = item.desac_bautoriza_modif_dev,
                            desac_bmodi_doc = item.desac_bmodi_doc,
                            Descripcion_Modifica = Convert.ToBoolean(item.desac_bmodi_doc) == true ? "MODIFICADO" : "SIN MODIF"
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
        public int insertarOrdenDespacho(EOrdenDespacho obj)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_ORDEN_DESPACHO_INSERTAR(
                        ref intIcod,
                        obj.desac_ianio,
                        obj.desac_vnumero_despacho,
                        obj.desac_sfecha_despacho,
                        obj.desac_iid_situacion_despacho,
                        obj.tablc_iid_motivo_despacho,
                        obj.almac_icod_almacen_salida,
                        obj.almac_icod_almacen_ingreso,
                        obj.desac_vatencion,
                        obj.desac_ventregar,
                        obj.desac_vreferencia,
                        obj.desac_vpartida,
                        obj.tranc_icod_transportista,
                        obj.tranc_vnum_matricula,
                        obj.tranc_vruc,
                        obj.intUsuario,
                        obj.strPc,
                        obj.desac_icod_despacho_pedido,
                        obj.tipo_borden_despacho,
                        obj.desac_vnumero_parte,
                        obj.desac_sfecha_devolucion,
                        obj.remic_icod_remision,
                        obj.desac_vguia_remision,
                        obj.desac_vplaca_vehiculo,
                        obj.desac_bautoriza_modif,
                        obj.desac_bautoriza_modif_dev,
                        obj.desac_bmodi_doc
                    );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarOrdenDespachoAutoventa(EOrdenDespacho obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ORDEN_DESPACHO_MODIFICAR(
                        obj.desac_icod_despacho,
                        obj.desac_sfecha_despacho,
                        obj.desac_vatencion,
                        obj.desac_ventregar,
                        obj.desac_vreferencia,
                        obj.desac_vpartida,
                        obj.tranc_icod_transportista,
                        obj.tranc_vnum_matricula,
                        obj.tranc_vruc,
                        obj.intUsuario,
                        obj.strPc,
                        obj.desac_iid_situacion_despacho,
                        obj.desac_icod_despacho_pedido,
                        obj.tipo_borden_despacho,
                        obj.almac_icod_almacen_salida,
                        obj.almac_icod_almacen_ingreso,
                        obj.desac_vnumero_parte,
                        obj.desac_sfecha_devolucion,
                        obj.remic_icod_remision,
                        obj.desac_vguia_remision,
                        obj.desac_vplaca_vehiculo,
                        obj.desac_bautoriza_modif,
                        obj.desac_bautoriza_modif_dev,
                        obj.desac_bmodi_doc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarOrdenDespacho(EOrdenDespacho obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ORDEN_DESPACHO_ELIMINAR(
                        obj.desac_icod_despacho);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GENERAR_NUMERO_ORDEN_DESPACHO(año);
                    foreach (var item in query)
                    {
                        numOrdenDespachoAutoventa = item.NumOrdenDespacho.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return numOrdenDespachoAutoventa;
        }
        public EOrdenDespacho listarOrdenDespachoXId(int desac_icod_despacho)
        {
            List<EOrdenDespacho> lista = new List<EOrdenDespacho>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ORDEN_DESPACHO_X_ID(desac_icod_despacho);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenDespacho()
                        {
                            desac_icod_despacho = item.desac_icod_despacho,
                            desac_ianio = item.desac_ianio,
                            desac_vnumero_despacho = item.desac_vnumero_despacho,
                            desac_sfecha_despacho = Convert.ToDateTime(item.desac_sfecha_despacho),
                            desac_iid_situacion_despacho = item.desac_iid_situacion_despacho,
                            tablc_iid_motivo_despacho = item.tablc_iid_motivo_despacho,
                            almac_icod_almacen_salida = Convert.ToInt32(item.almac_icod_almacen_salida),
                            almac_icod_almacen_salida_OD = item.almac_icod_almacen_salida_OD,
                            AlmacenSalida = item.AlmacenSalida,
                            almac_icod_almacen_ingreso = Convert.ToInt32(item.almac_icod_almacen_ingreso),
                            AlmacenIngreso = item.AlmacenIngreso,
                            desac_vatencion = item.desac_vatencion,
                            desac_ventregar = item.desac_ventregar,
                            desac_ventregar_arroba = item.desac_ventregar.Replace("@", " - "),
                            desac_vreferencia = item.desac_vreferencia,
                            desac_vpartida = item.desac_vpartida,
                            tranc_icod_transportista = item.tranc_icod_transportista,
                            Transportista = item.Transportista,
                            tranc_vnum_matricula = item.tranc_vnum_matricula,
                            tranc_vruc = item.tranc_vruc,
                            Situacion = item.Situacion,
                            desac_icod_despacho_pedido = Convert.ToInt32(item.desac_icod_despacho_pedido),
                            //vNumeroPedido = item.vNumeroPedido,
                            tipo_borden_despacho = Convert.ToBoolean(item.tipo_borden_despacho),
                            desac_vnumero_parte = item.desac_vnumero_parte,
                            desac_sfecha_devolucion = item.desac_sfecha_devolucion,
                            remic_icod_remision = item.remic_icod_remision,
                            desac_vguia_remision = item.desac_vguia_remision,
                            desac_vplaca_vehiculo = item.desac_vplaca_vehiculo,
                            //KardexXRe = Convert.ToDecimal(item.KardexXRe),
                            desac_bautoriza_modif = item.desac_bautoriza_modif,
                            desac_bautoriza_modif_dev = item.desac_bautoriza_modif_dev,
                            desac_bmodi_doc = item.desac_bmodi_doc
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista[0];
        }
        #endregion

        #region Registro de Almacenes
        public List<ERegistroParametro> listarRegistroParametro()
        {
            List<ERegistroParametro> lista = new List<ERegistroParametro>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ERegistroParametro>();
                    var query = dc.SGE_REGISTRO_PARAMETRO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERegistroParametro()
                        {
                            rgpmc_icod_registro_parametro = Convert.ToInt32(item.rgpmc_icod_registro_parametro),
                            rgpmc_vcod_registro_parametro = Convert.ToInt32(item.rgpmc_vcod_registro_parametro),
                            rgpmc_vdescripcion = item.rgpmc_vdescripcion,
                            tabl_iid_situacion = Convert.ToInt32(item.tabl_iid_situacion),
                            Situacion = item.Situacion,
                            rgpmc_vserie_factura = item.rgpmc_vserie_factura,
                            rgpmc_icorrelativo_factura = Convert.ToInt32(item.rgpmc_icorrelativo_factura),
                            rgpmc_vserie_boleta = item.rgpmc_vserie_boleta,
                            rgpmc_icorrelativo_boleta = Convert.ToInt32(item.rgpmc_icorrelativo_boleta),
                            rgpmc_vserieF_nota_credito = item.rgpmc_vserieF_nota_credito,
                            rgpmc_vserieB_nota_credito = item.rgpmc_vserieB_nota_credito,
                            rgpmc_icorrelativo_nota_credito = Convert.ToInt32(item.rgpmc_icorrelativo_nota_credito),
                            rgpmc_vserieF_nota_debito = item.rgpmc_vserieF_nota_debito,
                            rgpmc_vserieB_nota_debito = item.rgpmc_vserieB_nota_debito,
                            rgpmc_icorrelativo_nota_debito = Convert.ToInt32(item.rgpmc_icorrelativo_nota_debito),
                            rgpmc_icorrelativo_recibo_caja = Convert.ToInt32(item.rgpmc_icorrelativo_recibo_caja),
                            rgpmc_vserie_recibo_caja = item.rgpmc_vserie_recibo_caja,
                            rgpmc_vserie_contrato = item.rgpmc_vserie_contrato,
                            rgpmc_icorrelativo_contrato = item.rgpmc_icorrelativo_contrato,
                            rgpmc_icorrelativo_solicitud = item.rgpmc_icorrelativo_solicitud,
                            rgpmc_nmonto_mora = Convert.ToDecimal(item.rgpmc_nmonto_mora),
                            rgpmc_idias_mora = Convert.ToInt32(item.rgpmc_idias_mora)

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
        public int insertarRegistroParametro(ERegistroParametro oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_REGISTRO_PARAMETRO_INSERTAR(
                    ref intIcod,
                    oBe.rgpmc_vcod_registro_parametro,
                    oBe.rgpmc_vdescripcion,
                    oBe.tabl_iid_situacion,
                    oBe.rgpmc_vserie_factura,
                    oBe.rgpmc_icorrelativo_factura,
                    oBe.rgpmc_vserie_boleta,
                    oBe.rgpmc_icorrelativo_boleta,
                    oBe.rgpmc_vserieF_nota_credito,
                     oBe.rgpmc_vserieB_nota_credito,
                    oBe.rgpmc_icorrelativo_nota_credito,
                     oBe.rgpmc_vserieF_nota_debito,
                    oBe.rgpmc_vserieB_nota_debito,
                    oBe.rgpmc_icorrelativo_nota_debito,
                    oBe.intUsuario,
                    oBe.strPc
                    );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REGISTRO_PARAMETRO_MODIFICAR(
                    oBe.rgpmc_icod_registro_parametro,
                    oBe.rgpmc_vcod_registro_parametro,
                    oBe.rgpmc_vdescripcion,
                    oBe.tabl_iid_situacion,
                    oBe.rgpmc_vserie_factura,
                    oBe.rgpmc_icorrelativo_factura,
                    oBe.rgpmc_vserie_boleta,
                    oBe.rgpmc_icorrelativo_boleta,
                    oBe.rgpmc_vserieF_nota_credito,
                    oBe.rgpmc_vserieB_nota_credito,
                    oBe.rgpmc_icorrelativo_nota_credito,
                    oBe.rgpmc_vserieF_nota_debito,
                    oBe.rgpmc_vserieB_nota_debito,
                    oBe.rgpmc_icorrelativo_nota_debito,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.rgpmc_vserie_recibo_caja,
                    oBe.rgpmc_icorrelativo_recibo_caja,
                    oBe.rgpmc_vserie_contrato,
                    oBe.rgpmc_icorrelativo_contrato,
                    oBe.rgpmc_icorrelativo_solicitud,
                    oBe.rgpmc_nmonto_mora,
                    oBe.rgpmc_idias_mora
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REGISTRO_PARAMETRO_ELIMINAR(
                        oBe.rgpmc_icod_registro_parametro,
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
        public List<ERegistroParametro> getCorrelativoRP(int intRP)
        {
            string strSerieFactura = "";
            int? intCorrelativoFactura = 0;
            string strSerieBoleta = "";
            int? intCorrelativoBoleta = 0;
            string strSerieFNotaCredito = "";
            string strSerieBNotaCredito = "";
            int? intCorrelativoNotaCredito = 0;
            string strSerieFNotaDebito = "";
            string strSerieBNotaDebito = "";
            int? intCorrelativoNotaDebito = 0;
            List<ERegistroParametro> lst = new List<ERegistroParametro>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_GET_CORRELATIVO_RP(
                    ref strSerieFactura,
                    ref intCorrelativoFactura,
                    ref strSerieBoleta,
                    ref intCorrelativoBoleta,
                    ref strSerieFNotaCredito,
                    ref strSerieBNotaCredito,
                    ref intCorrelativoNotaCredito,
                    ref strSerieFNotaDebito,
                    ref strSerieBNotaDebito,
                    ref intCorrelativoNotaDebito,
                    intRP);

                    lst.Add(new ERegistroParametro()
                    {
                        rgpmc_vserie_factura = strSerieFactura,
                        rgpmc_icorrelativo_factura = Convert.ToInt32(intCorrelativoFactura),
                        rgpmc_vserie_boleta = strSerieBoleta,
                        rgpmc_icorrelativo_boleta = Convert.ToInt32(intCorrelativoBoleta),
                        rgpmc_vserieF_nota_credito = strSerieFNotaCredito,
                        rgpmc_vserieB_nota_credito = strSerieBNotaCredito,
                        rgpmc_icorrelativo_nota_credito = Convert.ToInt32(intCorrelativoNotaCredito),
                        rgpmc_vserieF_nota_debito = strSerieFNotaDebito,
                        rgpmc_vserieB_nota_debito = strSerieBNotaDebito,
                        rgpmc_icorrelativo_nota_debito = Convert.ToInt32(intCorrelativoNotaDebito)
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EVentasVendedor> listarVentasVendedor(int anio, int IcodVendedor, DateTime FInicio, DateTime FFin)
        {
            List<EVentasVendedor> lista = new List<EVentasVendedor>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VENTAS_VENDEDOR(anio, IcodVendedor, FInicio, FFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentasVendedor()
                        {
                            Fecha = Convert.ToDateTime(item.Fecha),
                            TipoDoc = item.TipoDoc,
                            Numero = item.Numero,
                            Cliente = item.Cliente,
                            Producto = item.Producto,
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            Vendedor = item.Vendedor,
                            Situacion = item.Situacion
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

        #region Zonas
        public List<EZona> listarZona()
        {
            List<EZona> lista = new List<EZona>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_ZONAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EZona()
                        {
                            zonc_icod_zona = item.zonc_icod_zona,
                            zonc_iid_zona = Convert.ToInt32(item.zonc_iid_zona),
                            zonc_vdescripcion = item.zonc_vdescripcion,
                            zonc_flag_estado = Convert.ToBoolean(item.zonc_flag_estado),
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
        public int insertarZona(EZona oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ZONAS_INSERTAR(
                        ref intIcod,
                        oBe.zonc_iid_zona,
                        oBe.zonc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.zonc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ZONAS_MODIFICAR(
                        oBe.zonc_icod_zona,
                        oBe.zonc_iid_zona,
                        oBe.zonc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ZONAS_ELIMINAR(
                        oBe.zonc_icod_zona,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<ECategoriaFamilia> listarCategoria_Famili_detalle_Todo()
        //{
        //    List<ECategoriaFamilia> lista = new List<ECategoriaFamilia>();
        //    try
        //    {
        //        using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
        //        {
        //            var query = dc.SGEA_CATEGORIA_FAMILIA_DETALLE_TODO();
        //            foreach (var item in query)
        //            {
        //                lista.Add(new ECategoriaFamilia()
        //                {
        //                    catf_icod_categoria = item.catf_icod_categoria,
        //                    catf_vdescripcion = item.catf_vdescripcion,
        //                    famic_icod_familia = item.famic_icod_familia,
        //                    famic_vdescripcion = item.famic_vdescripcion,
        //                    famid_icod_familia = item.famid_icod_familia,
        //                    famid_vdescripcion = item.famid_vdescripcion

        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        #endregion
        #region Distritos_Zona
        public List<EDistritoZona> listarDistritoZona(int intIcodZona)
        {
            List<EDistritoZona> lista = new List<EDistritoZona>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_DISTRITOS_ZONA_LISTAR(intIcodZona);
                    foreach (var item in query)
                    {
                        lista.Add(new EDistritoZona()
                        {
                            disd_icod_distrito_zona = item.disd_icod_distrito_zona,
                            zonc_icod_zona = Convert.ToInt32(item.zonc_icod_zona),
                            disc_icod_distrito = Convert.ToInt32(item.disc_icod_distrito),
                            disd_flag_estado = Convert.ToBoolean(item.disd_flag_estado),
                            disc_vdescripcion = item.disc_vdescripcion,
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
        public List<EDistritoZona> listarVerificarDistrito(int inticoddistrito)
        {
            List<EDistritoZona> lista = new List<EDistritoZona>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_VERIFICAR_DISTRITOS_LISTAR(inticoddistrito);
                    foreach (var item in query)
                    {
                        lista.Add(new EDistritoZona()
                        {

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
        public int insertarDistritoZona(EDistritoZona oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DISTRITOS_ZONA_INSERTAR(
                        ref intIcod,
                        oBe.zonc_icod_zona,
                        oBe.disc_icod_distrito,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.disd_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDistritoZona(EDistritoZona oBe)
        {

        }
        public void eliminarDistritoZona(EDistritoZona oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DISTRITOS_ZONA_ELIMINAR(
                        oBe.disd_icod_distrito_zona,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<EDistritos> lista = new List<EDistritos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_DISTRITOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EDistritos()
                        {
                            disc_icod_distrito = item.disc_icod_distrito,
                            disc_iid_distrito = Convert.ToInt32(item.disc_iid_distrito),
                            disc_vdescripcion = item.disc_vdescripcion,
                            disc_flag_estado = Convert.ToBoolean(item.disc_flag_estado),
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
        public int insertarDistrito(EDistritos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DISTRITOS_INSERTAR(
                        ref intIcod,
                        oBe.disc_iid_distrito,
                        oBe.disc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.disc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DISTRITOS_MODIFICAR(
                        oBe.disc_icod_distrito,
                        oBe.disc_iid_distrito,
                        oBe.disc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DISTRITOS_ELIMINAR(
                        oBe.disc_icod_distrito,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
            List<EFunerarias> lista = new List<EFunerarias>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FUNERARIAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EFunerarias()
                        {
                            func_icod_funeraria = item.func_icod_funeraria,
                            func_iid_funeraria = Convert.ToInt32(item.func_iid_funeraria),
                            disc_icod_distrito = Convert.ToInt32(item.disc_icod_distrito),
                            func_vrazon_social = item.func_vrazon_social,
                            func_vnombre_comercial = item.func_vnombre_comercial,
                            func_cnumero_docum_fun = item.func_cnumero_docum_fun,
                            func_vruc = item.func_vruc,
                            func_vdireccion = item.func_vdireccion,
                            func_vtelefonos = item.func_vtelefonos,
                            func_vcorreo = item.func_vcorreo,
                            func_vcontacto = item.func_vcontacto,
                            func_flag_estado = Convert.ToBoolean(item.func_flag_estado),
                            disc_vdescripcion = item.disc_vdescripcion,
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
        public int insertarFuneraria(EFunerarias oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FUNERARIAS_INSERTAR(
                        ref intIcod,
                        oBe.func_iid_funeraria,
                        oBe.disc_icod_distrito,
                        oBe.func_vrazon_social,
                        oBe.func_vnombre_comercial,
                        oBe.func_cnumero_docum_fun,
                        oBe.func_vruc,
                        oBe.func_vdireccion,
                        oBe.func_vtelefonos,
                        oBe.func_vcorreo,
                        oBe.func_vcontacto,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.func_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FUNERARIAS_MODIFICAR(
                        oBe.func_icod_funeraria,
                        oBe.func_iid_funeraria,
                        oBe.disc_icod_distrito,
                        oBe.func_vrazon_social,
                        oBe.func_vnombre_comercial,
                        oBe.func_cnumero_docum_fun,
                        oBe.func_vruc,
                        oBe.func_vdireccion,
                        oBe.func_vtelefonos,
                        oBe.func_vcorreo,
                        oBe.func_vcontacto,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FUNERARIAS_ELIMINAR(
                        oBe.func_icod_funeraria,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Reporte de Ventas
        public List<EReporteVentas> listarReporteVentas()
        {
            List<EReporteVentas> lista = new List<EReporteVentas>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_REPORTES_VENTAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EReporteVentas()
                        {
                            revec_icod_reporte_ventas = item.revec_icod_reporte_ventas,
                            revec_iid_reporte_ventas = Convert.ToInt32(item.revec_iid_reporte_ventas),
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            revec_vprimer_nombre = item.revec_vprimer_nombre,
                            revec_vsegundo_nombre = item.revec_vsegundo_nombre,
                            revec_vapellido_paterno = item.revec_vapellido_paterno,
                            revec_vapellido_materno = item.revec_vapellido_materno,
                            tablc_iid_tipo_tabla = Convert.ToInt32(item.tablc_iid_tipo_tabla),
                            disc_icod_distrito = Convert.ToInt32(item.disc_icod_distrito),
                            func_icod_funeraria = Convert.ToInt32(item.func_icod_funeraria),
                            revec_flag_estado = Convert.ToBoolean(item.revec_flag_estado),
                            vendc_vnombre_vendedor = item.vendc_vnombre_vendedor,
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            disc_vdescripcion = item.disc_vdescripcion,
                            func_vnombre_comercial = item.func_vnombre_comercial,
                            func_cnumero_docum_fun = item.func_cnumero_docum_fun,
                            func_vtelefonos = item.func_vtelefonos,
                            func_vcontacto = item.func_vcontacto,
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
        public int insertarReporteVentas(EReporteVentas oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REPORTES_VENTAS_INSERTAR(
                        ref intIcod,
                        oBe.revec_iid_reporte_ventas,
                        oBe.vendc_icod_vendedor,
                        oBe.revec_vprimer_nombre,
                        oBe.revec_vsegundo_nombre,
                        oBe.revec_vapellido_paterno,
                        oBe.revec_vapellido_materno,
                        oBe.tablc_iid_tipo_tabla,
                        oBe.disc_icod_distrito,
                        oBe.func_icod_funeraria,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.revec_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REPORTES_VENTAS_MODIFICAR(
                        oBe.revec_icod_reporte_ventas,
                        oBe.revec_iid_reporte_ventas,
                        oBe.vendc_icod_vendedor,
                        oBe.revec_vprimer_nombre,
                        oBe.revec_vsegundo_nombre,
                        oBe.revec_vapellido_paterno,
                        oBe.revec_vapellido_materno,
                        oBe.tablc_iid_tipo_tabla,
                        oBe.disc_icod_distrito,
                        oBe.func_icod_funeraria,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_REPORTES_VENTAS_ELIMINAR(
                        oBe.revec_icod_reporte_ventas,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Resumen de documentos
        public int insertarSunatResumenDocumentosCab(ESunatResumenDocumentosCab obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_CAB_INSERTAR(
                         ref intIcod,
                         obj.IdDocumento,
                         obj.FechaEmision,
                         obj.FechaGeneracion,
                         obj.NroDocumento,
                         obj.TipoDocumento,
                         obj.NombreLegal,
                         obj.NombreComercial,
                         obj.Ubigeo,
                         obj.Direccion,
                         obj.Urbanizacion,
                         obj.Departamento,
                         obj.Provincia,
                         obj.Distrito,
                         obj.EstadoResumen
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int insertarSunatResumenDocumentosDet(ESunatResumenDocumentosDet obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_DET_INSERTAR(
                        ref intIcod,
                        obj.IdCabecera,
                        obj.Id,
                        obj.TipoDocumento,
                        obj.IdDocumento,
                        obj.TipoDocumentoReceptor,
                        obj.NroDocumentoReceptor,
                        obj.CodigoEstadoItem,
                        obj.DocumentoRelacionado,
                        obj.TipoDocumentoRelacionado,
                        obj.Moneda,
                        obj.TotalVenta,
                        obj.TotalDescuentos,
                        obj.TotalIgv,
                        obj.TotalIsc,
                        obj.TotalIvap,
                        obj.TotalOtrosImpuestos,
                        obj.Gravadas,
                        obj.Exoneradas,
                        obj.Inafectas,
                        obj.Exportacion,
                        obj.Gratuitas,
                        obj.doc_icod_documento
                      );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaResumen(DateTime fechaInicio, DateTime fechaEmision)
        {
            DateTime dateOut;
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_LISTAR_RESUMEN(fechaInicio, fechaEmision);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            IdCabecera = item.IdCabecera,
                            idDocumento = item.idDocumento.Trim(),
                            fechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            //horaEmision = DateTime.ParseExact(item.horaEmision.Trim(), "HH:mm:ss", CultureInfo.InvariantCulture).ToString("HH:mm:ss"),
                            //horaEmision = Convert.ToDateTime(item.horaEmision),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            fechaVencimiento = DateTime.TryParseExact(item.fehaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut) == true ? DateTime.ParseExact(item.fehaVencimiento.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : "",
                            tipoDocumento = item.tipoDocumento.Trim(),
                            moneda = item.moneda.Trim(),
                            cantidadItems = Convert.ToInt32(item.cantidadItems),
                            nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            CodLocalEmisor = Convert.ToInt32(item.CodLocalEmisor),
                            nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            CodMotivoDescuento = Convert.ToInt32(item.CodMotivoDescuento),
                            PorcDescuento = Convert.ToDecimal(item.PorcDescuento),
                            MontoDescuentoGlobal = Convert.ToDecimal(item.MontoDescuentoGlobal),
                            BaseMontoDescuento = Convert.ToDecimal(item.BaseMontoDescuento),
                            MontoTotalImpuesto = Convert.ToDecimal(item.MontoTotalImpuesto),
                            MontoGravadasIGV = Convert.ToDecimal(item.MontoGravadasIGV),
                            CodigoTributo = Convert.ToInt32(item.CodigoTributo),
                            MontoExonerado = Convert.ToDecimal(item.MontoExonerado),
                            MontoInafecto = Convert.ToDecimal(item.MontoInafecto),
                            MontoGratuitoImpuesto = Convert.ToDecimal(item.MontoGratuitoImpuesto),
                            MontoBaseGratuito = Convert.ToDecimal(item.MontoBaseGratuito),
                            totalIgv = Convert.ToDecimal(item.totalIgv),
                            MontoGravadosISC = Convert.ToDecimal(item.MontoGravadosISC),
                            totalIsc = Convert.ToDecimal(item.totalIsc),
                            MontoGravadosOtros = Convert.ToDecimal(item.MontoGravadosOtros),
                            totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            TotalValorVenta = Convert.ToDecimal(item.TotalValorVenta),
                            TotalPrecioVenta = Convert.ToDecimal(item.TotalPrecioVenta),
                            MontoDescuento = Convert.ToDecimal(item.MontoDescuento),
                            MontoTotalCargo = Convert.ToDecimal(item.MontoTotalCargo),
                            MontoTotalAnticipo = Convert.ToDecimal(item.MontoTotalAnticipo),
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            EstadoAnulacion = Convert.ToInt32(item.EstadoAnulacion),
                            EAnulado = item.EAnulado,
                            EstadoSunat = item.EstadoSunat,
                            Mensaje = item.Mensaje,
                            direccionReceptor = item.direccionReceptor,
                            StrTipoDoc = item.StrTipoDoc,
                            NroDocqModifica = item.NroDocqModifica,
                            TipoDocqModifica = item.TipoDocqModifica
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

        public List<ESunatResumenDocumentosDet> listarSunatResumenDocumentosDet(int idCabecera)
        {
            List<ESunatResumenDocumentosDet> lista = new List<ESunatResumenDocumentosDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_DET_LISTAR(idCabecera);
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatResumenDocumentosDet()
                        {
                            IdItems = item.IdItems,
                            IdCabecera = Convert.ToInt32(item.IdCabecera),
                            Id = Convert.ToInt32(item.Id),
                            TipoDocumento = item.TipoDocumento,
                            IdDocumento = item.IdDocumento,
                            TipoDocumentoReceptor = item.TipoDocumentoReceptor,
                            NroDocumentoReceptor = item.NroDocumentoReceptor,
                            CodigoEstadoItem = Convert.ToInt32(item.CodigoEstadoItem),
                            DocumentoRelacionado = item.DocumentoRelacionado,
                            TipoDocumentoRelacionado = item.TipoDocumentoRelacionado,
                            Moneda = item.Moneda,
                            TotalVenta = Convert.ToDecimal(item.TotalVenta),
                            TotalDescuentos = Convert.ToDecimal(item.TotalDescuentos),
                            TotalIgv = Convert.ToDecimal(item.TotalIgv),
                            TotalIsc = Convert.ToDecimal(item.TotalIsc),
                            TotalIvap = Convert.ToDecimal(item.TotalIvap),
                            TotalOtrosImpuestos = Convert.ToDecimal(item.TotalOtrosImpuestos),
                            Gravadas = Convert.ToDecimal(item.Gravadas),
                            Exoneradas = Convert.ToDecimal(item.Exoneradas),
                            Inafectas = Convert.ToDecimal(item.Inafectas),
                            Exportacion = Convert.ToDecimal(item.Exportacion),
                            Gratuitas = Convert.ToDecimal(item.Gratuitas),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            strTipoDoc = item.StrTipoDoc
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

        public void actualizarResumenDocumentosResponse(int id, int estadoSunat)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_ESTADO(
                        id,
                        estadoSunat
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESunatResumenDocumentosCab> listarSunatResumenDocumentosCab(DateTime fechaInicio)
        {
            DateTime dateOut;
            List<ESunatResumenDocumentosCab> lista = new List<ESunatResumenDocumentosCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_CAB_LISTAR(fechaInicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatResumenDocumentosCab()
                        {
                            IdCabecera = item.IdCabecera,
                            IdDocumento = item.IdDocumento.ToString(),
                            FechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            FechaGeneracion = DateTime.ParseExact(item.FechaGeneracion.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            NroDocumento = item.NroDocumento,
                            TipoDocumento = item.TipoDocumento,
                            NombreLegal = item.NombreLegal.Trim(),
                            NombreComercial = item.NombreComercial.Trim(),
                            Ubigeo = item.Ubigeo,
                            Direccion = item.Direccion,
                            Urbanizacion = item.Urbanizacion,
                            Departamento = item.Departamento,
                            Provincia = item.Provincia,
                            Distrito = item.Distrito,
                            EstadoResumen = Convert.ToInt32(item.EstadoResumen),
                            EstadoSunat = item.EstadoSunat,
                            Fecha = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            FechaEnvio = item.FechaEnvio,
                            NroTicket = item.NroTicket,
                            IdResponse = item.Id
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
        public List<ESunatResumenDocumentosCab> listarSunatResumenDocumentosExcel()
        {
            DateTime dateOut;
            List<ESunatResumenDocumentosCab> lista = new List<ESunatResumenDocumentosCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_EXCEL_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatResumenDocumentosCab()
                        {
                            IdItems = item.IdItems,
                            IdCabeceraE = Convert.ToInt32(item.IdCabecera),
                            Id = Convert.ToInt32(item.Id),
                            TipoDocumentoE = item.TipoDocumento,
                            IdDocumentoE = item.IdDocumento,
                            Moneda = item.Moneda,
                            TotalVenta = Convert.ToDecimal(item.TotalVenta),
                            Gravadas = Convert.ToDecimal(item.Gravadas),
                            Inafectas = Convert.ToDecimal(item.Inafectas),
                            NroDoc = item.NroDoc,
                            Fecha = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            strTipoDoc = item.strTipoDoc,
                            NroTicket = item.NroTicket,
                            MensajeError = item.MensajeError
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
        public void eliminarSunatResumenDocumentosDetalle(int icodCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_DETALLE_ELIMINAR(icodCabecera);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void eliminarSunatResumenDocumento(int icodCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_ELIMINAR(icodCabecera);
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
            List<EPlataforma> lista = new List<EPlataforma>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_PLATAFORMA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPlataforma()
                        {
                            pltc_icod_plataforma = item.pltc_icod_plataforma,
                            pltc_iid_plataforma = Convert.ToInt32(item.pltc_iid_plataforma),
                            pltc_vdescripcion = item.pltc_vdescripcion,
                            pltc_flag_estado = Convert.ToBoolean(item.pltc_flag_estado),
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
        public int insertarPlataforma(EPlataforma oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLATAFORMAS_INSERTAR(
                        ref intIcod,
                        oBe.pltc_iid_plataforma,
                        oBe.pltc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pltc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLATAFORMA_MODIFICAR(
                        oBe.pltc_icod_plataforma,
                        oBe.pltc_iid_plataforma,
                        oBe.pltc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_PLATAFORMA_ELIMINAR(
                        oBe.pltc_icod_plataforma,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Contrato


        public List<EContrato> listar_ubicaciones_fallecidos(DateTime inicio, DateTime final)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_LISTAR_UICACIONES_FALLECIDOS(inicio, final);
                    foreach (var item in query)
                    {
                        lista.Add(new EContrato()
                        {
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_vnombre_fallecido = item.cntc_vnombre_fallecido,
                            cntc_vapellido_paterno_fallecido = item.cntc_vapellido_paterno_fallecido,
                            cntc_vapellido_materno_fallecido = item.cntc_vapellido_materno_fallecido,
                            cntc_sfecha_nac_fallecido = item.cntc_sfecha_nac_fallecido,
                            cntc_sfecha_fallecimiento = item.cntc_sfecha_fallecimiento,
                            cntc_icod_manzana = Convert.ToInt32(item.cntc_icod_manzana),
                            cntc_icod_isepultura = Convert.ToInt32(item.cntc_icod_isepultura),
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total.ToString(),
                            cntc_itipo_sepultura = Convert.ToInt32(item.cntc_itipo_sepultura),
                            cntc_itamanio_lapida = Convert.ToInt32(item.cntc_itamanio_lapida),
                            cntc_icod_vendedor = Convert.ToInt32(item.cntc_icod_vendedor),
                            cntc_sfecha_crea = item.cntc_sfecha_crea,
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_sfecha_contrato = Convert.ToDateTime(item.cntc_sfecha_contrato),
                            cntc_icod_contrato_fallecido = item.cntc_icod_contrato_fallecido,
                            cntc_sfecha_accion = item.cntc_sfecha_accion,
                            icod_manzana_contrato = item.icod_manzana_contrato,
                            icod_isepultura_contrato = item.icod_isepultura_contrato,
                            itipo_sepultura_contrato = item.itipo_sepultura_contrato,
                            cntc_vfrase = item.cntc_vfrase,
                            cntc_vpensamiento = item.cntc_vpensamiento,
                            espad_vnivel = item.espad_vnivel,
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            cntc_icod_plataforma = Convert.ToInt32(item.cntc_icod_plataforma),
                            cntc_vnumero_contrato_corr = item.cntc_vnumero_contrato_corr

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

        public List<EContrato> listarContrato(int cntc_iindicador_contr_sol)
        {
            List<EContrato> lista = new List<EContrato>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 99999999;
                    var query = dc.SGEV_CONTRATOS_LISTAR(cntc_iindicador_contr_sol);
                    foreach (var item in query)
                    {
                        lista.Add(new EContrato()
                        {

                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_sfecha_contrato = Convert.ToDateTime(item.cntc_sfecha_contrato),
                            cntc_icod_vendedor = Convert.ToInt32(item.cntc_icod_vendedor),
                            cntc_iorigen_venta = Convert.ToInt32(item.cntc_iorigen_venta),
                            cntc_icod_funeraria = Convert.ToInt32(item.cntc_icod_funeraria),
                            cntc_vnombre_comercial = item.cntc_vnombre_comercial,
                            cntc_vunidad_venta = item.cntc_vunidad_venta,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante == null ? "" : item.cntc_vdni_contratante,
                            cntc_vruc_contratante = item.cntc_vruc_contratante,
                            cntc_sfecha_nacimineto_contratante = Convert.ToDateTime(item.cntc_sfecha_nacimineto_contratante),
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante,
                            cntc_vdireccion_contratante = item.cntc_vdireccion_contratante,
                            cntc_inacionalidad_contratante = Convert.ToInt32(item.cntc_inacionalidad_contratante),
                            cntc_itipo_documento_contratante = Convert.ToInt32(item.cntc_itipo_documento_contratante),
                            cntc_vnombre_representante = item.cntc_vnombre_representante,
                            cntc_vapellido_paterno_representante = item.cntc_vapellido_paterno_representante,
                            cntc_vapellido_materno_representante = item.cntc_vapellido_materno_representante,
                            cntc_vdni_representante = item.cntc_vdni_representante,
                            cntc_ruc_representante = item.cntc_ruc_representante,
                            cntc_sfecha_nacimiento_representante = Convert.ToDateTime(item.cntc_sfecha_nacimiento_representante),
                            cntc_iestado_civil_representante = Convert.ToInt32(item.cntc_iestado_civil_representante),
                            cntc_inacionalidad_respresentante = Convert.ToInt32(item.cntc_inacionalidad_respresentante),
                            cntc_vtelefono_representante = item.cntc_vtelefono_representante,
                            cntc_vdireccion_representante = item.cntc_vdireccion_representante,
                            cntc_vnumero_direccion_representante = item.cntc_vnumero_direccion_representante,
                            cntc_vdepartamento_representante = item.cntc_vdepartamento_representante,
                            cntc_idistrito_representante = Convert.ToInt32(item.cntc_idistrito_representante),
                            cntc_vcodigo_postal_representante = item.cntc_vcodigo_postal_representante,
                            cntc_itipo_documento_representante = Convert.ToInt32(item.cntc_itipo_documento_representante),
                            cntc_vdocumento_respresentante = item.cntc_vdocumento_respresentante,
                            cntc_vnombre_beneficiario = item.cntc_vnombre_beneficiario,
                            cntc_vapellido_paterno_beneficiario = item.cntc_vapellido_paterno_beneficiario,
                            cntc_vapellido_materno_beneficiario = item.cntc_vapellido_materno_beneficiario,
                            cntc_vdni_beneficiario = item.cntc_vdni_beneficiario,
                            cntc_sfecha_nacimiento_beneficiario = Convert.ToDateTime(item.cntc_sfecha_nacimiento_beneficiario),
                            cntc_vdireccion_beneficiario = item.cntc_vdireccion_beneficiario,
                            cntc_inacionalidad = Convert.ToInt32(item.cntc_inacionalidad),
                            cntc_icodigo_plan = Convert.ToInt32(item.cntc_icodigo_plan),
                            cntc_icod_nombre_plan = Convert.ToInt32(item.cntc_icod_nombre_plan),
                            cntc_vnombre_plan = item.cntc_vnombre_plan,
                            cntc_nprecio_lista = Convert.ToDecimal(item.cntc_nprecio_lista),
                            cntc_ndescuento = Convert.ToDecimal(item.cntc_ndescuento),
                            cntc_ninhumacion = Convert.ToDecimal(item.cntc_ninhumacion),
                            cntc_naporte_fondo = Convert.ToDecimal(item.cntc_naporte_fondo),
                            cntc_nIGV = Convert.ToDecimal(item.cntc_nIGV),
                            cntc_nprecio_total = Convert.ToDecimal(item.cntc_nprecio_total),
                            cntc_itipo_sepultura = Convert.ToInt32(item.cntc_itipo_sepultura),
                            cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato,
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total,
                            cntc_vurnas = item.cntc_vurnas,
                            cntc_vservico_inhumacion = item.cntc_vservico_inhumacion,
                            cntc_icod_plataforma = Convert.ToInt32(item.cntc_icod_plataforma),
                            cntc_icod_manzana = Convert.ToInt32(item.cntc_icod_manzana),
                            cntc_icod_isepultura = Convert.ToInt32(item.cntc_icod_isepultura),
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espac_icod_vespacios = item.espac_icod_vespacios,
                            cntc_bnivel1 = item.cntc_bnivel1,
                            cntc_bnivel2 = item.cntc_bnivel2,
                            cntc_bnivel3 = item.cntc_bnivel3,
                            cntc_bnivel4 = item.cntc_bnivel4,
                            cntc_bnivel5 = item.cntc_bnivel5,
                            cntc_bnivel6 = item.cntc_bnivel6,
                            espad_iid_iespacios1 = Convert.ToInt32(item.espad_iid_iespacios1),
                            espad_iid_iespacios2 = Convert.ToInt32(item.espad_iid_iespacios2),
                            espad_iid_iespacios3 = Convert.ToInt32(item.espad_iid_iespacios3),
                            espad_iid_iespacios4 = Convert.ToInt32(item.espad_iid_iespacios4),
                            espad_iid_iespacios5 = Convert.ToInt32(item.espad_iid_iespacios5),
                            espad_iid_iespacios6 = Convert.ToInt32(item.espad_iid_iespacios6),
                            cntc_vcodigo_sepultura = item.cntc_vcodigo_sepultura,
                            cntc_vnumero_reserva = item.cntc_vnumero_reserva,
                            strcodigoplan = item.strcodigoplan,
                            strNombreplan = item.strNombreplan,
                            strorigenventa = item.strorigenventa,
                            strtiposepultura = item.strtiposepultura,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strNombreIEC = item.strNombreIEC,
                            cntc_flag_estado = Convert.ToBoolean(item.cntc_flag_estado),
                            strsepultura = item.strsepultura,
                            strnacionalidad = item.strnacionalidad,
                            strnacionalidadfallec = item.strnacionalidadfallec,
                            strestadocivil = item.strestadocivil,
                            strdistrito = item.strdistrito,
                            cntc_icod_situacion = Convert.ToInt32(item.cntc_icod_situacion),
                            strSituacion = item.strSituacion,
                            cntc_ncuota_inicial = Convert.ToDecimal(item.cntc_ncuota_inicial),
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            cntc_nmonto_cuota = Convert.ToDecimal(item.cntc_nmonto_cuota),
                            cntc_sfecha_cuota = item.cntc_sfecha_cuota,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_vdescripcion_anulacion = item.cntc_vdescripcion_anulacion,
                            cntc_icod_indicador_espacios = Convert.ToInt32(item.cntc_icod_indicador_espacios),
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_flag_verificacion = Convert.ToBoolean(item.cntc_flag_verificacion),
                            cntc_indicador_pre_contrato = Convert.ToInt32(item.cntc_indicador_pre_contrato),
                            strIndicador = item.cntc_indicador_pre_contrato.ToString(),
                            cntc_itipo_pago = Convert.ToInt32(item.cntc_itipo_pago),
                            strTipoPago = item.strTipoPago,
                            cntc_vdireccion_fallecido = item.cntc_vdireccion_fallecido,
                            cntc_itipo_doc_prestamo = Convert.ToInt32(item.cntc_itipo_doc_prestamo),
                            func_icod_funeraria_prestamo = Convert.ToInt32(item.func_icod_funeraria_prestamo),
                            strFunerariaPrestamo = item.strFunerariaPrestamo,
                            strNombreContratante = item.strNombreContratante,
                            cntc_sfecha_crea = item.cntc_sfecha_crea,
                            cntc_nmonto_foma = Convert.ToDecimal(item.cntc_nmonto_foma),
                            flag_indicador = Convert.ToBoolean(item.flag_indicador),
                            strDetalle = item.strDetalle,
                            flag_tit = Convert.ToBoolean(item.flag_tit),
                            cntc_npago_covid19 = Convert.ToDecimal(item.cntc_npago_covid19),
                            cntc_icod_deceso = Convert.ToInt32(item.cntc_icod_deceso),
                            cntc_icod_foma_mante = Convert.ToInt32(item.cntc_icod_foma_mante),
                            monto_contado = (Math.Round(Convert.ToDecimal(item.monto_contado), 2)).ToString(),
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntc_vnombre_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_paterno_contratante.TrimEnd().TrimStart(), item.cntc_vapellido_materno_contratante.TrimEnd().TrimStart()),
                            cntc_nfinanciamientro = Convert.ToDecimal(item.cntc_nfinanciamientro),
                            cntc_nmonto_total_foma = item.cntc_nmonto_total_foma,
                            cntc_nmonto_total_foma_pagado = item.cntc_nmonto_total_foma_pagado,
                            cntc_vnumero_contrato_corr = item.cntc_vnumero_contrato_corr,
                            cntc_iindicador_contr_sol = item.cntc_iindicador_contr_sol,
                            cntc_vnumero_solicitud = item.cntc_vnumero_solicitud,
                            cntc_iestado_sol = item.cntc_iestado_sol,
                            cntc_vobservaciones_sol = item.cntc_vobservaciones_sol,
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante
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



        public EContrato listarContratoPorIcod(int cntc_icod_contrato)
        {
            EContrato obj = new EContrato();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_LISTAR_POR_ICOD(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        obj = new EContrato()
                        {

                            cntc_icod_contrato = item.cntc_icod_contrato,
                            cntc_vnumero_contrato = item.cntc_vnumero_contrato,
                            cntc_sfecha_contrato = Convert.ToDateTime(item.cntc_sfecha_contrato),
                            cntc_icod_vendedor = Convert.ToInt32(item.cntc_icod_vendedor),
                            cntc_iorigen_venta = Convert.ToInt32(item.cntc_iorigen_venta),
                            cntc_icod_funeraria = Convert.ToInt32(item.cntc_icod_funeraria),
                            cntc_vnombre_comercial = item.cntc_vnombre_comercial,
                            cntc_vunidad_venta = item.cntc_vunidad_venta,
                            cntc_vnombre_contratante = item.cntcc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntcc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntcc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntcc_vdni_contratante,
                            cntc_vruc_contratante = item.cntcc_vruc_contratante,
                            cntc_sfecha_nacimineto_contratante = item.cntcc_sfecha_nacimineto_contratante,
                            cntc_vtelefono_contratante = item.cntcc_vtelefono_contratante,
                            cntc_vdireccion_correo_contratante = item.cntcc_vdireccion_correo_contratante,
                            cntc_vdireccion_contratante = item.cntcc_vdireccion_contratante,
                            cntc_inacionalidad_contratante = Convert.ToInt32(item.cntcc_inacionalidad_contratante),
                            //cntc_vnacionalidad_cotratante = item.cntcc_vnacionalidad_cotratante,
                            cntc_itipo_documento_contratante = Convert.ToInt32(item.cntcc_itipo_documento_contratante),
                            //cntc_vdocumento_contratante = item.cntcc_vdocumento_contratante,
                            cntc_vnombre_representante = item.cntc_vnombre_representante,
                            cntc_vapellido_paterno_representante = item.cntc_vapellido_paterno_representante,
                            cntc_vapellido_materno_representante = item.cntc_vapellido_materno_representante,
                            cntc_vdni_representante = item.cntc_vdni_representante,
                            cntc_ruc_representante = item.cntc_ruc_representante,
                            cntc_sfecha_nacimiento_representante = item.cntc_sfecha_nacimiento_representante,
                            cntc_iestado_civil_representante = Convert.ToInt32(item.cntc_iestado_civil_representante),
                            cntc_inacionalidad_respresentante = Convert.ToInt32(item.cntc_inacionalidad_respresentante),
                            cntc_vtelefono_representante = item.cntc_vtelefono_representante,
                            cntc_vdireccion_representante = item.cntc_vdireccion_representante,
                            cntc_vnumero_direccion_representante = item.cntc_vnumero_direccion_representante,
                            cntc_vdepartamento_representante = item.cntc_vdepartamento_representante,
                            cntc_idistrito_representante = Convert.ToInt32(item.cntc_idistrito_representante),
                            cntc_vcodigo_postal_representante = item.cntc_vcodigo_postal_representante,
                            cntc_itipo_documento_representante = Convert.ToInt32(item.cntc_itipo_documento_representante),
                            cntc_vdocumento_respresentante = item.cntc_vdocumento_respresentante,
                            cntc_vnombre_beneficiario = item.cntc_vnombre_beneficiario,
                            cntc_vapellido_paterno_beneficiario = item.cntc_vapellido_paterno_beneficiario,
                            cntc_vapellido_materno_beneficiario = item.cntc_vapellido_materno_beneficiario,
                            cntc_vdni_beneficiario = item.cntc_vdni_beneficiario,
                            cntc_sfecha_nacimiento_beneficiario = Convert.ToDateTime(item.cntc_sfecha_nacimiento_beneficiario),
                            cntc_vdireccion_beneficiario = item.cntc_vdireccion_beneficiario,
                            cntc_inacionalidad = Convert.ToInt32(item.cntc_inacionalidad),
                            cntc_icodigo_plan = Convert.ToInt32(item.cntc_icodigo_plan),
                            cntc_icod_nombre_plan = Convert.ToInt32(item.cntc_icod_nombre_plan),
                            cntc_vnombre_plan = item.cntc_vnombre_plan,
                            cntc_nprecio_lista = Convert.ToDecimal(item.cntc_nprecio_lista),
                            cntc_ndescuento = Convert.ToDecimal(item.cntc_ndescuento),
                            cntc_ninhumacion = Convert.ToDecimal(item.cntc_ninhumacion),
                            cntc_naporte_fondo = Convert.ToDecimal(item.cntc_naporte_fondo),
                            cntc_nIGV = Convert.ToDecimal(item.cntc_nIGV),
                            cntc_nprecio_total = Convert.ToDecimal(item.cntc_nprecio_total),
                            cntc_itipo_sepultura = Convert.ToInt32(item.cntc_itipo_sepultura),
                            cntc_vcapacidad_contrato = item.cntc_vcapacidad_contrato,
                            cntc_vcapacidad_total = item.cntc_vcapacidad_total,
                            cntc_vurnas = item.cntc_vurnas,
                            cntc_vservico_inhumacion = item.cntc_vservico_inhumacion,
                            cntc_icod_plataforma = Convert.ToInt32(item.cntc_icod_plataforma),
                            cntc_icod_manzana = Convert.ToInt32(item.cntc_icod_manzana),
                            cntc_icod_isepultura = Convert.ToInt32(item.cntc_icod_isepultura),
                            espac_iid_iespacios = Convert.ToInt32(item.espac_iid_iespacios),
                            espac_icod_vespacios = item.espac_icod_vespacios,
                            cntc_bnivel1 = item.cntc_bnivel1,
                            cntc_bnivel2 = item.cntc_bnivel2,
                            cntc_bnivel3 = item.cntc_bnivel3,
                            cntc_bnivel4 = item.cntc_bnivel4,
                            cntc_bnivel5 = item.cntc_bnivel5,
                            cntc_bnivel6 = item.cntc_bnivel6,
                            espad_iid_iespacios1 = Convert.ToInt32(item.espad_iid_iespacios1),
                            espad_iid_iespacios2 = Convert.ToInt32(item.espad_iid_iespacios2),
                            espad_iid_iespacios3 = Convert.ToInt32(item.espad_iid_iespacios3),
                            espad_iid_iespacios4 = Convert.ToInt32(item.espad_iid_iespacios4),
                            espad_iid_iespacios5 = Convert.ToInt32(item.espad_iid_iespacios5),
                            espad_iid_iespacios6 = Convert.ToInt32(item.espad_iid_iespacios6),
                            cntc_vcodigo_sepultura = item.cntc_vcodigo_sepultura,
                            cntc_vnumero_reserva = item.cntc_vnumero_reserva,
                            strcodigoplan = item.strcodigoplan,
                            strNombreplan = item.strNombreplan,
                            strorigenventa = item.strorigenventa,
                            strtiposepultura = item.strtiposepultura,
                            strplataforma = item.strplataforma,
                            strmanzana = item.strmanzana,
                            strNombreIEC = item.strNombreIEC,
                            cntc_flag_estado = Convert.ToBoolean(item.cntc_flag_estado),
                            strsepultura = item.strsepultura,
                            strnacionalidad = item.strnacionalidad,
                            strnacionalidadfallec = item.strnacionalidadfallec,
                            strestadocivil = item.strestadocivil,
                            strdistrito = item.strdistrito,
                            cntc_icod_situacion = Convert.ToInt32(item.cntc_icod_situacion),
                            strSituacion = item.strSituacion,
                            cntc_ncuota_inicial = Convert.ToDecimal(item.cntc_ncuota_inicial),
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            cntc_nmonto_cuota = Convert.ToDecimal(item.cntc_nmonto_cuota),
                            cntc_sfecha_cuota = item.cntc_sfecha_cuota,
                            strnivel = item.strNivel1 + " " + item.strNivel2 + " " + item.strNivel3 + " " + item.strNivel4 + " " + item.strNivel5 + " " + item.strNivel6,
                            cntc_vdescripcion_anulacion = item.cntc_vdescripcion_anulacion,
                            cntc_icod_indicador_espacios = Convert.ToInt32(item.cntc_icod_indicador_espacios),
                            cntc_vobservaciones = item.cntc_vobservaciones,
                            cntc_flag_verificacion = Convert.ToBoolean(item.cntc_flag_verificacion),
                            cntc_indicador_pre_contrato = Convert.ToInt32(item.cntc_indicador_pre_contrato),
                            strIndicador = item.cntc_indicador_pre_contrato.ToString(),
                            cntc_itipo_pago = Convert.ToInt32(item.cntc_itipo_pago),
                            strTipoPago = item.strTipoPago,
                            cntc_vdireccion_fallecido = item.cntc_vdireccion_fallecido,
                            cntc_itipo_doc_prestamo = Convert.ToInt32(item.cntc_itipo_doc_prestamo),
                            func_icod_funeraria_prestamo = Convert.ToInt32(item.func_icod_funeraria_prestamo),
                            strFunerariaPrestamo = item.strFunerariaPrestamo,
                            strNombreContratante = item.strNombreContratante,
                            cntc_sfecha_crea = Convert.ToDateTime(item.cntc_sfecha_crea),
                            cntc_nmonto_foma = Convert.ToDecimal(item.cntc_nmonto_foma),
                            flag_indicador = Convert.ToBoolean(item.flag_indicador),
                            strDetalle = item.strDetalle,
                            flag_tit = Convert.ToBoolean(item.flag_tit),
                            cntc_npago_covid19 = Convert.ToDecimal(item.cntc_npago_covid19),
                            cntc_icod_deceso = Convert.ToInt32(item.cntc_icod_deceso),
                            cntc_icod_foma_mante = Convert.ToInt32(item.cntc_icod_foma_mante),
                            monto_contado = (Math.Round(Convert.ToDecimal(item.monto_contado), 2)).ToString(),
                            strNombreCompleto = string.Format("{0} {1} {2}", item.cntcc_vnombre_contratante, item.cntcc_vapellido_paterno_contratante, item.cntcc_vapellido_materno_contratante),
                            cntcc_icod_contratante = Convert.ToInt32(item.cntcc_icod_contratante),
                            cntcc_iid_contratante = Convert.ToInt32(item.cntcc_iid_contratante),
                            cntc_nmonto_total_foma = item.cntc_nmonto_total_foma,
                            cntc_nmonto_total_foma_pagado = item.cntc_nmonto_total_foma_pagado,
                            cntc_nfinanciamientro = Convert.ToDecimal(item.cntc_nfinanciamientro),
                            cntc_vnumero_contrato_corr = item.cntc_vnumero_contrato_corr,
                            cntc_iindicador_contr_sol = item.cntc_iindicador_contr_sol,
                            cntc_vnumero_solicitud = item.cntc_vnumero_solicitud,
                            cntc_iestado_sol = item.cntc_iestado_sol,
                            cntc_vobservaciones_sol = item.cntc_vobservaciones_sol,
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public int insertarContrato(EContrato oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 999999999;
                    dc.SGEV_CONTRATOS_INSERTAR(
                        ref intIcod,
                        oBe.cntc_vnumero_contrato,
                        oBe.cntc_sfecha_contrato,
                        oBe.cntc_icod_vendedor,
                        oBe.cntc_iorigen_venta,
                        oBe.cntc_icod_funeraria,
                        oBe.cntc_vnombre_comercial,
                        oBe.cntc_vunidad_venta,
                        oBe.cntc_vnombre_contratante,
                        oBe.cntc_vapellido_paterno_contratante,
                        oBe.cntc_vapellido_materno_contratante,
                        oBe.cntc_vdni_contratante,
                        oBe.cntc_vruc_contratante,
                        oBe.cntc_sfecha_nacimineto_contratante,
                        oBe.cntc_vtelefono_contratante,
                        oBe.cntc_vdireccion_correo_contratante,
                        oBe.cntc_vdireccion_contratante,
                        oBe.cntc_inacionalidad_contratante,
                        oBe.cntc_vnacionalidad_cotratante,
                        oBe.cntc_itipo_documento_contratante,
                        oBe.cntc_vdocumento_contratante,
                        oBe.cntc_vnombre_representante,
                        oBe.cntc_vapellido_paterno_representante,
                        oBe.cntc_vapellido_materno_representante,
                        oBe.cntc_vdni_representante,
                        oBe.cntc_ruc_representante,
                        oBe.cntc_sfecha_nacimiento_representante,
                        oBe.cntc_iestado_civil_representante,
                        oBe.cntc_inacionalidad_respresentante,
                        oBe.cntc_vtelefono_representante,
                        oBe.cntc_vdireccion_representante,
                        oBe.cntc_vnumero_direccion_representante,
                        oBe.cntc_vdepartamento_representante,
                        oBe.cntc_idistrito_representante,
                        oBe.cntc_vcodigo_postal_representante,
                        oBe.cntc_itipo_documento_representante,
                        oBe.cntc_vdocumento_respresentante,
                        oBe.cntc_inacionalidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cntc_icodigo_plan,
                        oBe.cntc_icod_nombre_plan,
                        oBe.cntc_vnombre_plan,
                        oBe.cntc_nprecio_lista,
                        oBe.cntc_ndescuento,
                        oBe.cntc_ninhumacion,
                        oBe.cntc_naporte_fondo,
                        oBe.cntc_nIGV,
                        oBe.cntc_nprecio_total,
                        oBe.cntc_itipo_sepultura,
                        oBe.cntc_vcapacidad_contrato,
                        oBe.cntc_vcapacidad_total,
                        oBe.cntc_vurnas,
                        oBe.cntc_vservico_inhumacion,
                        oBe.cntc_icod_plataforma,
                        oBe.cntc_icod_manzana,
                        oBe.cntc_icod_isepultura,
                        oBe.espac_iid_iespacios,
                        oBe.cntc_bnivel1,
                        oBe.cntc_bnivel2,
                        oBe.cntc_bnivel3,
                        oBe.cntc_bnivel4,
                        oBe.cntc_bnivel5,
                        oBe.cntc_bnivel6,
                        oBe.espad_iid_iespacios1,
                        oBe.espad_iid_iespacios2,
                        oBe.espad_iid_iespacios3,
                        oBe.espad_iid_iespacios4,
                        oBe.espad_iid_iespacios5,
                        oBe.espad_iid_iespacios6,
                        oBe.cntc_vcodigo_sepultura,
                        oBe.cntc_vnumero_reserva,
                        oBe.cntc_flag_estado,
                        oBe.cntc_icod_situacion,
                        oBe.cntc_ncuota_inicial,
                        oBe.cntc_inro_cuotas,
                        oBe.cntc_nmonto_cuota,
                        oBe.cntc_sfecha_cuota,
                        oBe.cntc_icod_indicador_espacios,
                        oBe.cntc_vobservaciones,
                        oBe.cntc_flag_verificacion,
                        oBe.cntc_indicador_pre_contrato,
                        oBe.cntc_itipo_pago,
                        oBe.cntc_vdireccion_fallecido,
                        oBe.cntc_itipo_doc_prestamo,
                        oBe.func_icod_funeraria_prestamo,
                        oBe.cntc_nmonto_foma,
                        oBe.flag_indicador,
                        oBe.cntc_npago_covid19,
                        oBe.cntc_icod_deceso,
                        oBe.cntc_icod_foma_mante,
                        oBe.cntc_nfinanciamientro,
                        oBe.cntc_vnumero_contrato_corr,
                        oBe.cntc_iindicador_contr_sol,
                        oBe.cntc_vnumero_solicitud,
                        oBe.cntc_iestado_sol,
                        oBe.cntc_vobservaciones_sol,
                        oBe.cntc_vorigen_registro
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarContrato(EContrato oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATO_MODIFICAR(
                         oBe.cntc_icod_contrato,
                        oBe.cntc_vnumero_contrato,
                        oBe.cntc_sfecha_contrato,
                        oBe.cntc_icod_vendedor,
                        oBe.cntc_iorigen_venta,
                        oBe.cntc_icod_funeraria,
                        oBe.cntc_vnombre_comercial,
                        oBe.cntc_vunidad_venta,
                        oBe.cntc_vnombre_contratante,
                        oBe.cntc_vapellido_paterno_contratante,
                        oBe.cntc_vapellido_materno_contratante,
                        oBe.cntc_vdni_contratante,
                        oBe.cntc_vruc_contratante,
                        oBe.cntc_sfecha_nacimineto_contratante,
                        oBe.cntc_vtelefono_contratante,
                        oBe.cntc_vdireccion_correo_contratante,
                        oBe.cntc_vdireccion_contratante,
                        oBe.cntc_inacionalidad_contratante,
                        oBe.cntc_vnacionalidad_cotratante,
                        oBe.cntc_itipo_documento_contratante,
                        oBe.cntc_vdocumento_contratante,
                        oBe.cntc_vnombre_representante,
                        oBe.cntc_vapellido_paterno_representante,
                        oBe.cntc_vapellido_materno_representante,
                        oBe.cntc_vdni_representante,
                        oBe.cntc_ruc_representante,
                        oBe.cntc_sfecha_nacimiento_representante,
                        oBe.cntc_iestado_civil_representante,
                        oBe.cntc_inacionalidad_respresentante,
                        oBe.cntc_vtelefono_representante,
                        oBe.cntc_vdireccion_representante,
                        oBe.cntc_vnumero_direccion_representante,
                        oBe.cntc_vdepartamento_representante,
                        oBe.cntc_idistrito_representante,
                        oBe.cntc_vcodigo_postal_representante,
                        oBe.cntc_itipo_documento_representante,
                        oBe.cntc_vdocumento_respresentante,
                        oBe.cntc_inacionalidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cntc_icodigo_plan,
                        oBe.cntc_icod_nombre_plan,
                        oBe.cntc_vnombre_plan,
                        oBe.cntc_nprecio_lista,
                        oBe.cntc_ndescuento,
                        oBe.cntc_ninhumacion,
                        oBe.cntc_naporte_fondo,
                        oBe.cntc_nIGV,
                        oBe.cntc_nprecio_total,
                        oBe.cntc_itipo_sepultura,
                        oBe.cntc_vcapacidad_contrato,
                        oBe.cntc_vcapacidad_total,
                        oBe.cntc_vurnas,
                        oBe.cntc_vservico_inhumacion,
                        oBe.cntc_icod_plataforma,
                        oBe.cntc_icod_manzana,
                        oBe.cntc_icod_isepultura,
                        oBe.espac_iid_iespacios,
                        oBe.cntc_bnivel1,
                        oBe.cntc_bnivel2,
                        oBe.cntc_bnivel3,
                        oBe.cntc_bnivel4,
                        oBe.cntc_bnivel5,
                        oBe.cntc_bnivel6,
                        oBe.espad_iid_iespacios1,
                        oBe.espad_iid_iespacios2,
                        oBe.espad_iid_iespacios3,
                        oBe.espad_iid_iespacios4,
                        oBe.espad_iid_iespacios5,
                        oBe.espad_iid_iespacios6,
                        oBe.espad_iid_iespaciosT1,
                        oBe.espad_iid_iespaciosT2,
                        oBe.espad_iid_iespaciosT3,
                        oBe.espad_iid_iespaciosT4,
                        oBe.espad_iid_iespaciosT5,
                        oBe.espad_iid_iespaciosT6,
                        oBe.cntc_vcodigo_sepultura,
                        oBe.cntc_vnumero_reserva,
                        oBe.cntc_icod_situacion,
                        oBe.cntc_ncuota_inicial,
                        oBe.cntc_inro_cuotas,
                        oBe.cntc_nmonto_cuota,
                        oBe.cntc_sfecha_cuota,
                        oBe.cntc_icod_indicador_espacios,
                        oBe.cntc_vobservaciones,
                        oBe.cntc_itipo_pago,
                        oBe.cntc_vdireccion_fallecido,
                        oBe.cntc_itipo_doc_prestamo,
                        oBe.func_icod_funeraria_prestamo,
                        oBe.cntc_nmonto_foma,
                        oBe.flag_indicador,
                        oBe.cntc_npago_covid19,
                        oBe.cntc_icod_deceso,
                        oBe.cntc_icod_foma_mante,
                        oBe.cntc_nfinanciamientro,
                        oBe.cntc_iindicador_contr_sol,
                        oBe.cntc_vnumero_solicitud,
                        oBe.cntc_iestado_sol,
                        oBe.cntc_vobservaciones_sol
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_ELIMINAR(
                        oBe.cntc_icod_contrato,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_ANULAR(
                        oBe.cntc_icod_contrato,
                        oBe.cntc_icod_situacion
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Contrato Beneficiario
        public List<EContratoBeneficiario> listarContratoBeneficiario(int cntc_icod_contrato)
        {
            List<EContratoBeneficiario> lista = new List<EContratoBeneficiario>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_BENEFICIARIO_LISTAR(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EContratoBeneficiario()
                        {
                            cntc_icod_contrato_beneficiario = item.cntc_icod_contrato_beneficiario,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnombre_beneficiario = item.cntc_vnombre_beneficiario,
                            cntc_vapellido_paterno_beneficiario = item.cntc_vapellido_paterno_beneficiario,
                            cntc_vapellido_materno_beneficiario = item.cntc_vapellido_materno_beneficiario,
                            cntc_vdni_beneficiario = item.cntc_vdni_beneficiario,
                            cntc_sfecha_nacimiento_beneficiario = Convert.ToDateTime(item.cntc_sfecha_nacimiento_beneficiario),
                            cntc_vdireccion_beneficiario = item.cntc_vdireccion_beneficiario
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
        public int insertarContratoBeneficiario(EContratoBeneficiario oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_BENEFICIARIO_INSERTAR(
                        ref intIcod,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_vnombre_beneficiario,
                        oBe.cntc_vapellido_paterno_beneficiario,
                        oBe.cntc_vapellido_materno_beneficiario,
                        oBe.cntc_vdni_beneficiario,
                        oBe.cntc_sfecha_nacimiento_beneficiario,
                        oBe.cntc_vdireccion_beneficiario,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_BENEFICIARIO_MODIFICAR(
                        oBe.cntc_icod_contrato_beneficiario,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_vnombre_beneficiario,
                        oBe.cntc_vapellido_paterno_beneficiario,
                        oBe.cntc_vapellido_materno_beneficiario,
                        oBe.cntc_vdni_beneficiario,
                        oBe.cntc_sfecha_nacimiento_beneficiario,
                        oBe.cntc_vdireccion_beneficiario,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_BENEFICIARIO_ELIMINAR(
                        oBe.cntc_icod_contrato_beneficiario,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Contrato Fallecido
        public List<EContratoFallecido> listarContratoFallecido(int cntc_icod_contrato)
        {
            List<EContratoFallecido> lista = new List<EContratoFallecido>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_FALLECIDO_LISTAR(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EContratoFallecido()
                        {
                            cntc_icod_contrato_fallecido = item.cntc_icod_contrato_fallecido,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnombre_fallecido = item.cntc_vnombre_fallecido,
                            cntc_vapellido_paterno_fallecido = item.cntc_vapellido_paterno_fallecido,
                            cntc_vapellido_materno_fallecido = item.cntc_vapellido_materno_fallecido,
                            cntc_vdni_fallecido = item.cntc_vdni_fallecido,
                            cntc_sfecha_nac_fallecido = item.cntc_sfecha_nac_fallecido,
                            cntc_sfecha_fallecimiento = item.cntc_sfecha_fallecimiento,
                            cntc_sfecha_entierro = item.cntc_sfecha_entierro,
                            cntc_itipo_documento_fallecido = Convert.ToInt32(item.cntc_itipo_documento_fallecido),
                            cntc_vdocumento_fallecido = item.cntc_vdocumento_fallecido,
                            cntc_inacionalidad = Convert.ToInt32(item.cntc_inacionalidad),
                            cntc_icod_indicador_espacios = Convert.ToInt32(item.cntc_icod_indicador_espacios),
                            cntc_vdireccion_fallecido = item.cntc_vdireccion_fallecido,
                            cntc_icod_religiones = Convert.ToInt32(item.cntc_icod_religiones),
                            cntc_icod_tipo_deceso = Convert.ToInt32(item.cntc_icod_tipo_deceso),
                            cntc_vobservacion = item.cntc_vobservacion,
                            strReligiones = item.strReligiones,
                            strTipoDeceso = item.strTipoDeceso,
                            cntc_icod_tamanio_lapida = Convert.ToInt32(item.cntc_icod_tamanio_lapida),
                            cntc_sfecha_accion = item.cntc_sfecha_accion,
                            espad_vnivel = item.espad_vnivel
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
        public int insertarContratoFallecido(EContratoFallecido oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_FALLECIDO_INSERTAR(
                        ref intIcod,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_vnombre_fallecido,
                        oBe.cntc_vapellido_paterno_fallecido,
                        oBe.cntc_vapellido_materno_fallecido,
                        oBe.cntc_vdni_fallecido,
                        oBe.cntc_sfecha_nac_fallecido,
                        oBe.cntc_sfecha_fallecimiento,
                        oBe.cntc_sfecha_entierro,
                        oBe.cntc_itipo_documento_fallecido,
                        oBe.cntc_vdocumento_fallecido,
                        oBe.cntc_inacionalidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cntc_icod_indicador_espacios,
                        oBe.cntc_vdireccion_fallecido,
                        oBe.cntc_icod_religiones,
                        oBe.cntc_icod_tipo_deceso,
                        oBe.cntc_vobservacion,
                        oBe.cntc_icod_tamanio_lapida,
                        oBe.cntc_sfecha_accion,
                        oBe.cntc_vfrase,
                        oBe.cntc_vpensamiento,
                        oBe.cntc_itipo_sepultura,
                        oBe.cntc_icod_manzana,
                        oBe.cntc_icod_isepultura,
                        oBe.espac_iid_iespacios,
                        oBe.cntc_icod_plataforma
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_FALLECIDO_MODIFICAR(
                        oBe.cntc_icod_contrato_fallecido,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_vnombre_fallecido,
                        oBe.cntc_vapellido_paterno_fallecido,
                        oBe.cntc_vapellido_materno_fallecido,
                        oBe.cntc_vdni_fallecido,
                        oBe.cntc_sfecha_nac_fallecido,
                        oBe.cntc_sfecha_fallecimiento,
                        oBe.cntc_sfecha_entierro,
                        oBe.cntc_itipo_documento_fallecido,
                        oBe.cntc_vdocumento_fallecido,
                        oBe.cntc_inacionalidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cntc_icod_indicador_espacios,
                        oBe.cntc_vdireccion_fallecido,
                        oBe.cntc_icod_religiones,
                        oBe.cntc_icod_tipo_deceso,
                        oBe.cntc_vobservacion,
                        oBe.cntc_icod_tamanio_lapida,
                        oBe.cntc_sfecha_accion,
                        oBe.cntc_vfrase,
                        oBe.cntc_vpensamiento,
                        oBe.cntc_itipo_sepultura,
                        oBe.cntc_icod_manzana,
                        oBe.cntc_icod_isepultura,
                        oBe.espac_iid_iespacios,
                        oBe.cntc_icod_plataforma
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_FALLECIDO_ELIMINAR(
                        oBe.cntc_icod_contrato_fallecido,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Contrato Cuotas

        public Tuple<string, string> ObtenerDocumentos(int cntc_icod_contrato_cuotas)
        {
            Tuple<string, string> tupla = new Tuple<string, string>("", "");
            string Documentos = "";
            string Fechas = "";
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEVC_OBTENER_DOCUMENTOS_PAGOS_CUOTAS(cntc_icod_contrato_cuotas);

                    foreach (var item in query)
                    {
                        Documentos = Documentos + item.plnd_vnumero_doc + "  ";
                        Fechas = Fechas + item.plnd_sfecha_doc.ToString().Substring(0, 10) + "  ";
                    }

                    tupla = new Tuple<string, string>(Documentos, Fechas);
                }

                return tupla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<EContratoCuotas> listarContratoCuotas(int cntc_icod_contrato)
        {
            List<EContratoCuotas> lista = new List<EContratoCuotas>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_CUOTAS_LISTAR(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EContratoCuotas()
                        {
                            cntc_icod_contrato_cuotas = item.cntc_icod_contrato_cuotas,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            cntc_sfecha_cuota = Convert.ToDateTime(item.cntc_sfecha_cuota),
                            strTipo = item.strTipo,
                            cntc_icod_tipo_cuota = Convert.ToInt32(item.cntc_icod_tipo_cuota),
                            cntc_nmonto_cuota = Convert.ToDecimal(item.cntc_nmonto_cuota),
                            cntc_icod_situacion = Convert.ToInt32(item.cntc_icod_situacion),
                            strSituacion = item.strSituacion,
                            cntc_flag_situacion = Convert.ToBoolean(item.cntc_flag_situacion),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            cntc_icod_documento = Convert.ToInt32(item.cntc_icod_documento),
                            flag_multiple = Convert.ToBoolean(item.cntc_flag_multiple),
                            flag_multiple_anterior = Convert.ToBoolean(item.cntc_flag_multiple),
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante == null ? "" : item.cntc_vdni_contratante,
                            cntc_sfecha_pago_cuota = item.cntc_sfecha_pago_cuota,
                            strTipoDoc = item.tdocc_icod_tipo_doc == null ? "" : item.tdocc_icod_tipo_doc == 26 ? "FAV" : "BOV",
                            //NumContrato = item.NumContrato,
                            cntc_itipo_cuota = Convert.ToInt32(item.cntc_icod_tipo_cuota) == 5430 ? 0 : Convert.ToInt32(item.cntc_itipo_cuota),
                            strTipoCredito = (Convert.ToInt32(item.cntc_icod_tipo_cuota) == 5430 ? 0 : Convert.ToInt32(item.cntc_itipo_cuota)) == 0 ? "PRINCIPAL" : "REPROGRAMACION " + item.cntc_itipo_cuota.ToString(),
                            cntc_nsaldo = Convert.ToDecimal(item.cntc_npagado) == 0 ? Convert.ToDecimal(item.cntc_nmonto_cuota) : Convert.ToDecimal(item.cntc_nsaldo),
                            cntc_npagado = Convert.ToDecimal(item.cntc_npagado),
                            cntc_nmonto_mora = Convert.ToDecimal(item.cntc_nmonto_mora),
                            cntc_nmonto_mora_pago = Convert.ToDecimal(item.cntc_nmonto_mora_pago),
                            cntc_bautomatico = item.cntc_bautomatico
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
        public int insertarContratoCuotas(EContratoCuotas oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_CUOTAS_INSERTAR(
                        ref intIcod,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_inro_cuotas,
                        oBe.cntc_sfecha_cuota,
                        oBe.cntc_icod_tipo_cuota,
                        oBe.cntc_nmonto_cuota,
                        oBe.cntc_icod_situacion,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cntc_flag_situacion,
                        oBe.cntc_itipo_cuota,
                        oBe.cntc_nsaldo,
                        oBe.cntc_npagado,
                        oBe.cntc_nmonto_mora,
                        oBe.cntc_nmonto_mora_pago,
                        oBe.cntc_sfecha_pago_cuota,
                        oBe.cntc_bautomatico
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_CUOTAS_MODIFICAR(
                        oBe.cntc_icod_contrato_cuotas,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_inro_cuotas,
                        oBe.cntc_sfecha_cuota,
                        oBe.cntc_icod_tipo_cuota,
                        oBe.cntc_nmonto_cuota,
                        oBe.cntc_icod_situacion,
                        oBe.intUsuario,
                        oBe.strPc,
                        true,
                        oBe.cntc_nsaldo,
                        oBe.cntc_npagado,
                        oBe.cntc_nmonto_mora_pago,
                        oBe.cntc_nmonto_mora,
                        oBe.cntc_itipo_cuota,
                        oBe.cntc_sfecha_pago_cuota,
                        oBe.cntc_bautomatico
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_CUOTAS_ELIMINAR(
                        oBe.cntc_icod_contrato_cuotas,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EContratoCuotas> listarCuotas()
        {
            List<EContratoCuotas> lista = new List<EContratoCuotas>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CUOTAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EContratoCuotas()
                        {
                            cntc_icod_contrato_cuotas = item.cntc_icod_contrato_cuotas,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_inro_cuotas = Convert.ToInt32(item.cntc_inro_cuotas),
                            cntc_sfecha_cuota = Convert.ToDateTime(item.cntc_sfecha_cuota),
                            cntc_icod_tipo_cuota = Convert.ToInt32(item.cntc_icod_tipo_cuota),
                            strTipo = item.strTipo,
                            cntc_nmonto_cuota = Convert.ToDecimal(item.cntc_nmonto_cuota),
                            cntc_icod_situacion = Convert.ToInt32(item.cntc_icod_situacion),
                            strSituacion = item.strSituacion,
                            NumContrato = item.NumContrato,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            cntc_sfecha_pago_cuota = Convert.ToDateTime(item.plnd_sfecha_doc),
                            strTipoCuota = item.strTipoCuota,
                            cntc_npagado = Convert.ToDecimal(item.cntc_npagado),
                            cntc_nsaldo = Convert.ToDecimal(item.cntc_nsaldo),
                            cntc_nmonto_mora = Convert.ToDecimal(item.cntc_nmonto_mora),
                            cntc_itipo_cuota = Convert.ToInt32(item.cntc_itipo_cuota)
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

        public void modificarContratoCuotasSituacion(EContratoCuotas oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_CUOTAS_ACTUALIZAR_SITUACION(
                        oBe.cntc_icod_contrato_cuotas,
                        oBe.cntc_icod_situacion
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_VERIFICACION(
                        oBe.cntc_icod_contrato,
                        oBe.cntc_flag_verificacion
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_CUOTAS_MODIFICAR_DOCUMENTOS(
                        oBe.cntc_icod_contrato_cuotas,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.cntc_icod_documento,
                        oBe.flag_multiple
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Tablas Sunat
        public List<ETablaSunatCab> TablasSunatListar()
        {
            List<ETablaSunatCab> lista = null;

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ETablaSunatCab>();
                    var query = dc.SGE_TABLAS_SUNAT_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaSunatCab()
                        {
                            suntc_icod = item.suntc_icod,
                            suntc_codigo = item.suntc_codigo,
                            suntc_vdescripcion = item.suntc_vdescripcion

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
        //TablasSunatInsertar
        public void TablasSunatInsertar(ETablaSunatCab obj)
        {

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_INSERTAR(
                     obj.suntc_codigo,
                     obj.suntc_vdescripcion,
                     obj.suntc_iusuario_crea,
                     obj.suntc_vpc_crea
                   );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_MODIFICAR(
                        obj.suntc_icod,
                        obj.suntc_vdescripcion,
                        obj.suntc_iusuario_modifica,
                        obj.suntc_vpc_modifica
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_ELIMINAR(
                        obj.suntc_icod,
                        obj.suntc_iusuario_elimina,
                        obj.suntc_vpc_elimina
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ETablaSunatDet>();
                    var query = dc.SGE_TABLAS_SUNAT_DET_LISTAR(icod);
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaSunatDet()
                        {
                            suntc_icod = Convert.ToInt32(item.suntc_icod),
                            suntd_icod = item.suntd_icod,
                            suntd_codigo = item.suntd_codigo,
                            suntd_vdescripcion = item.suntd_vdescripcion

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
        public void TablasSunatDetInsertar(ETablaSunatDet obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_DET_INSERTAR(
                        obj.suntc_icod,
                        obj.suntd_codigo,
                        obj.suntd_vdescripcion,
                        obj.suntd_iusuario_crea,
                        obj.suntd_vpc_crea

                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_DET_MODIFICAR(
                        obj.suntd_icod,
                        obj.suntd_codigo,
                        obj.suntd_vdescripcion,
                        obj.suntd_iusuario_modifica,
                        obj.suntd_vpc_modifica
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_DET_ELIMINAR(
                        obj.suntd_icod,
                        obj.suntd_iusuario_eliminar,
                        obj.suntd_vpc_eliminar
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_MOTIVO_BAJA_ACTUALIZAR(obj.plnc_icod_planilla,
                        obj.favc_descripcion_motivo_baja
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_MOTIVO_BAJA_ACTUALIZAR(obj.plnc_icod_planilla,
                        obj.bovc_descripcion_motivo_baja
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DESCRIPCION_ANULACION_ACTUALIZAR(obj.cntc_icod_contrato,
                        obj.cntc_vdescripcion_anulacion
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EContratoTitular1> listarContratoTitular1(int cntc_icod_contrato)
        {
            List<EContratoTitular1> lista = new List<EContratoTitular1>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_TITULAR1_LISTAR(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EContratoTitular1()
                        {
                            cntc_icod_contrato_titular1 = item.cntc_icod_contrato_titular1,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_inumero = item.cntc_inumero,
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            cntc_vruc_contratante = item.cntc_vruc_contratante,
                            cntc_sfecha_nacimineto_contratante = Convert.ToDateTime(item.cntc_sfecha_nacimineto_contratante),
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_vtelefono_contratante2 = item.cntc_vtelefono_contratante2,
                            cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante,
                            cntc_vdireccion_contratante = item.cntc_vdireccion_contratante,
                            cntc_inacionalidad_contratante = Convert.ToInt32(item.cntc_inacionalidad_contratante),
                            cntc_vnacionalidad_cotratante = item.cntc_vnacionalidad_cotratante,
                            cntc_itipo_documento_contratante = Convert.ToInt32(item.cntc_itipo_documento_contratante),
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante,
                            strTipoDoc = item.strTipoDoc,
                            strNacionalidad = item.strNacionalidad,
                            cntc_flag_compromiso_pago = Convert.ToBoolean(item.cntc_flag_compromiso_pago)
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
        public int insertarContratoTitular1(EContratoTitular1 oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_TITULAR1_INSERTAR(
                        ref intIcod,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_inumero,
                        oBe.cntc_vnombre_contratante,
                        oBe.cntc_vapellido_paterno_contratante,
                        oBe.cntc_vapellido_materno_contratante,
                        oBe.cntc_vdni_contratante,
                        oBe.cntc_vruc_contratante,
                        oBe.cntc_sfecha_nacimineto_contratante,
                        oBe.cntc_vtelefono_contratante,
                        oBe.cntc_vtelefono_contratante2,
                        oBe.cntc_vdireccion_correo_contratante,
                        oBe.cntc_vdireccion_contratante,
                        oBe.cntc_inacionalidad_contratante,
                        oBe.cntc_vnacionalidad_cotratante,
                        oBe.cntc_itipo_documento_contratante,
                        oBe.cntc_vdocumento_contratante,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cntc_flag_compromiso_pago
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_TITULAR1_MODIFICAR(
                        oBe.cntc_icod_contrato_titular1,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_inumero,
                        oBe.cntc_vnombre_contratante,
                        oBe.cntc_vapellido_paterno_contratante,
                        oBe.cntc_vapellido_materno_contratante,
                        oBe.cntc_vdni_contratante,
                        oBe.cntc_vruc_contratante,
                        oBe.cntc_sfecha_nacimineto_contratante,
                        oBe.cntc_vtelefono_contratante,
                        oBe.cntc_vtelefono_contratante2,
                        oBe.cntc_vdireccion_correo_contratante,
                        oBe.cntc_vdireccion_contratante,
                        oBe.cntc_inacionalidad_contratante,
                        oBe.cntc_vnacionalidad_cotratante,
                        oBe.cntc_itipo_documento_contratante,
                        oBe.cntc_vdocumento_contratante,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cntc_flag_compromiso_pago
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_TITULAR1_ELIMINAR(
                        oBe.cntc_icod_contrato_titular1,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EContratoTitular2> listarContratoTitular2(int cntc_icod_contrato)
        {
            List<EContratoTitular2> lista = new List<EContratoTitular2>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_CONTRATOS_TITULAR2_LISTAR(cntc_icod_contrato);
                    foreach (var item in query)
                    {
                        lista.Add(new EContratoTitular2()
                        {
                            cntc_icod_contrato_titular2 = item.cntc_icod_contrato_titular2,
                            cntc_icod_contrato = Convert.ToInt32(item.cntc_icod_contrato),
                            cntc_vnombre_contratante = item.cntc_vnombre_contratante,
                            cntc_vapellido_paterno_contratante = item.cntc_vapellido_paterno_contratante,
                            cntc_vapellido_materno_contratante = item.cntc_vapellido_materno_contratante,
                            cntc_vdni_contratante = item.cntc_vdni_contratante,
                            cntc_vruc_contratante = item.cntc_vruc_contratante,
                            cntc_sfecha_nacimineto_contratante = Convert.ToDateTime(item.cntc_sfecha_nacimineto_contratante),
                            cntc_vtelefono_contratante = item.cntc_vtelefono_contratante,
                            cntc_vtelefono_contratante2 = item.cntc_vtelefono_contratante2,
                            cntc_vdireccion_correo_contratante = item.cntc_vdireccion_correo_contratante,
                            cntc_vdireccion_contratante = item.cntc_vdireccion_contratante,
                            cntc_inacionalidad_contratante = Convert.ToInt32(item.cntc_inacionalidad_contratante),
                            cntc_vnacionalidad_cotratante = item.cntc_vnacionalidad_cotratante,
                            cntc_itipo_documento_contratante = Convert.ToInt32(item.cntc_itipo_documento_contratante),
                            cntc_vdocumento_contratante = item.cntc_vdocumento_contratante
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
        public int insertarContratoTitular2(EContratoTitular2 oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_TITULAR2_INSERTAR(
                        ref intIcod,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_vnombre_contratante,
                        oBe.cntc_vapellido_paterno_contratante,
                        oBe.cntc_vapellido_materno_contratante,
                        oBe.cntc_vdni_contratante,
                        oBe.cntc_vruc_contratante,
                        oBe.cntc_sfecha_nacimineto_contratante,
                        oBe.cntc_vtelefono_contratante,
                        oBe.cntc_vtelefono_contratante2,
                        oBe.cntc_vdireccion_correo_contratante,
                        oBe.cntc_vdireccion_contratante,
                        oBe.cntc_inacionalidad_contratante,
                        oBe.cntc_vnacionalidad_cotratante,
                        oBe.cntc_itipo_documento_contratante,
                        oBe.cntc_vdocumento_contratante,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_TITULAR2_MODIFICAR(
                        oBe.cntc_icod_contrato_titular2,
                        oBe.cntc_icod_contrato,
                        oBe.cntc_vnombre_contratante,
                        oBe.cntc_vapellido_paterno_contratante,
                        oBe.cntc_vapellido_materno_contratante,
                        oBe.cntc_vdni_contratante,
                        oBe.cntc_vruc_contratante,
                        oBe.cntc_sfecha_nacimineto_contratante,
                        oBe.cntc_vtelefono_contratante,
                        oBe.cntc_vtelefono_contratante2,
                        oBe.cntc_vdireccion_correo_contratante,
                        oBe.cntc_vdireccion_contratante,
                        oBe.cntc_inacionalidad_contratante,
                        oBe.cntc_vnacionalidad_cotratante,
                        oBe.cntc_itipo_documento_contratante,
                        oBe.cntc_vdocumento_contratante,
                        oBe.intUsuario,
                        oBe.strPc
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_CONTRATOS_TITULAR2_ELIMINAR(
                        oBe.cntc_icod_contrato_titular2,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
