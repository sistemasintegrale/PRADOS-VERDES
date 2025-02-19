﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EContrato : EAuditoria
    {
        public int cntc_icod_contrato { get; set; }
        public int? cntc_iid_contrato { get; set; }
        public string cntc_vnumero_contrato { get; set; }
        public DateTime? cntc_sfecha_contrato { get; set; }
        public int? cntc_icod_vendedor { get; set; }
        public int? cntc_iorigen_venta { get; set; }
        public int cntc_icod_funeraria { get; set; }
        public string cntc_vnombre_comercial { get; set; }
        public string cntc_vunidad_venta { get; set; }
        public string cntc_vnombre_contratante { get; set; }
        public string cntc_vapellido_paterno_contratante { get; set; }
        public string cntc_vapellido_materno_contratante { get; set; }
        public string cntc_vdni_contratante { get; set; }
        public string cntc_vruc_contratante { get; set; }
        public DateTime? cntc_sfecha_nacimineto_contratante { get; set; }
        public string cntc_vtelefono_contratante { get; set; }
        public string cntc_vdireccion_correo_contratante { get; set; }
        public string cntc_vdireccion_contratante { get; set; }
        public int cntc_inacionalidad_contratante { get; set; }
        public string cntc_vnacionalidad_cotratante { get; set; }
        public int cntc_itipo_documento_contratante { get; set; }
        public string cntc_vdocumento_contratante { get; set; }
        public string cntc_vnombre_representante { get; set; }
        public string cntc_vapellido_paterno_representante { get; set; }
        public string cntc_vapellido_materno_representante { get; set; }
        public string cntc_vdni_representante { get; set; }
        public string cntc_ruc_representante { get; set; }
        public DateTime? cntc_sfecha_nacimiento_representante { get; set; }
        public int cntc_iestado_civil_representante { get; set; }
        public int cntc_inacionalidad_respresentante { get; set; }
        public string cntc_vtelefono_representante { get; set; }
        public string cntc_vdireccion_representante { get; set; }
        public string cntc_vnumero_direccion_representante { get; set; }
        public string cntc_vdepartamento_representante { get; set; }
        public int cntc_idistrito_representante { get; set; }
        public string cntc_vcodigo_postal_representante { get; set; }
        public int cntc_itipo_documento_representante { get; set; }
        public string cntc_vdocumento_respresentante { get; set; }
        public string cntc_vnombre_beneficiario { get; set; }
        public string cntc_vapellido_paterno_beneficiario { get; set; }
        public string cntc_vapellido_materno_beneficiario { get; set; }
        public string cntc_vdni_beneficiario { get; set; }
        public DateTime? cntc_sfecha_nacimiento_beneficiario { get; set; }
        public string cntc_vdireccion_beneficiario { get; set; }
        public string cntc_vnombre_fallecido { get; set; }
        public string cntc_vapellido_paterno_fallecido { get; set; }
        public string cntc_vapellido_materno_fallecido { get; set; }
        public string cntc_vdni_fallecido { get; set; }
        public DateTime? cntc_sfecha_nac_fallecido { get; set; }
        public DateTime? cntc_sfecha_fallecimiento { get; set; }
        public DateTime? cntc_sfecha_entierro { get; set; }
        public int cntc_itipo_documento_fallecido { get; set; }
        public string cntc_vdocumento_fallecido { get; set; }
        public int cntc_inacionalidad { get; set; }
        public int? cntc_icodigo_plan { get; set; }
        public int? cntc_icod_nombre_plan { get; set; }
        public string cntc_vnombre_plan { get; set; }
        public decimal? cntc_nprecio_lista { get; set; }
        public decimal cntc_ndescuento { get; set; }
        public decimal cntc_ninhumacion { get; set; }
        public decimal? cntc_naporte_fondo { get; set; }
        public decimal cntc_nIGV { get; set; }
        public decimal? cntc_nprecio_total { get; set; }
        public int? cntc_itipo_sepultura { get; set; }
        public string cntc_vcapacidad_contrato { get; set; }
        public string cntc_vcapacidad_total { get; set; }
        public string cntc_vurnas { get; set; }
        public string cntc_vservico_inhumacion { get; set; }
        public int? cntc_icod_plataforma { get; set; }
        public int? cntc_icod_manzana { get; set; }
        public int? cntc_icod_isepultura { get; set; }
        public int espac_iid_iespacios { get; set; }
        public string espac_icod_vespacios { get; set; }
        public int cntc_icod_nivel { get; set; }
        public string cntc_vcodigo_sepultura { get; set; }
        public string cntc_vnumero_reserva { get; set; }
        public string strcodigoplan { get; set; }
        public string strNombreplan { get; set; }
        public string strorigenventa { get; set; }
        public string strtiposepultura { get; set; }
        public string strplataforma { get; set; }
        public string strmanzana { get; set; }
        public string strnivel { get; set; }
        public string strNombreIEC { get; set; }
        public bool cntc_flag_estado { get; set; }
        public string strsepultura { get; set; }
        public string strnacionalidad { get; set; }
        public string strnacionalidadfallec { get; set; }
        public string strestadocivil { get; set; }
        public string strdistrito { get; set; }
        public Boolean? cntc_bnivel1 { get; set; }
        public Boolean? cntc_bnivel2 { get; set; }
        public Boolean? cntc_bnivel3 { get; set; }
        public Boolean? cntc_bnivel4 { get; set; }
        public Boolean? cntc_bnivel5 { get; set; }
        public Boolean? cntc_bnivel6 { get; set; }

        public int espad_iid_iespacios1 { get; set; }
        public int espad_iid_iespacios2 { get; set; }
        public int espad_iid_iespacios3 { get; set; }
        public int espad_iid_iespacios4 { get; set; }
        public int espad_iid_iespacios5 { get; set; }
        public int espad_iid_iespacios6 { get; set; }

        public int espad_iid_iespaciosT1 { get; set; }
        public int espad_iid_iespaciosT2 { get; set; }
        public int espad_iid_iespaciosT3 { get; set; }
        public int espad_iid_iespaciosT4 { get; set; }
        public int espad_iid_iespaciosT5 { get; set; }
        public int espad_iid_iespaciosT6 { get; set; }


        public int? cntc_icod_situacion { get; set; }
        public string strSituacion { get; set; }
        public decimal? cntc_ncuota_inicial { get; set; }
        public int? cntc_inro_cuotas { get; set; }
        public decimal? cntc_nmonto_cuota { get; set; }
        public DateTime? cntc_sfecha_cuota { get; set; }
        public int cntc_icod_indicador_espacios { get; set; }
        public string cntc_vobservaciones { get; set; }

        public string strNivel1 { get; set; }
        public string strNivel2 { get; set; }
        public string strNivel3 { get; set; }
        public string strNivel4 { get; set; }
        public string strNivel5 { get; set; }
        public string strNivel6 { get; set; }

        public string cntc_vdescripcion_anulacion { get; set; }
        public Boolean cntc_flag_verificacion { get; set; }
        public int cntc_indicador_pre_contrato { get; set; }
        public string strIndicador { get; set; }
        public int? cntc_itipo_pago { get; set; }
        public string strTipoPago { get; set; }
        public string cntc_vdireccion_fallecido { get; set; }

        public int cntc_itipo_doc_prestamo { get; set; }
        public int func_icod_funeraria_prestamo { get; set; }
        public string strFunerariaPrestamo { get; set; }
        public string strNombreContratante { get; set; }

        public Boolean flag_tit { get; set; }
        public DateTime? cntc_sfecha_crea { get; set; }
        public decimal cntc_nmonto_foma { get; set; }
        public bool flag_indicador { get; set; }
        public string strDetalle { get; set; }

        public decimal cntc_npago_covid19 { get; set; }
        public int cntc_icod_deceso { get; set; }
        public int cntc_icod_foma_mante { get; set; }
        public string monto_contado { get; set; }
        public string strNombreCompleto { get; set; }
        public int cntcc_icod_contratante { get; set; }
        public int cntcc_iid_contratante { get; set; }
        public decimal cntc_nfinanciamientro { get; set; }
        public int cntc_itamanio_lapida { get; set; }
        public int cntc_icod_contrato_fallecido { get; set; }
        public DateTime? cntc_sfecha_accion { get; set; }

        public string strNombreCompletoContratante { get { return $"{cntc_vnombre_contratante} {cntc_vapellido_paterno_contratante} {cntc_vapellido_materno_contratante}"; } }
        public string strNombreCompletoFallecido { get { return $"{cntc_vnombre_fallecido} {cntc_vapellido_paterno_fallecido} {cntc_vapellido_materno_fallecido}"; } }

        public int? itipo_sepultura_contrato { get; set; }
        public int? icod_isepultura_contrato { get; set; }
        public int? icod_manzana_contrato { get; set; }
        public string cntc_vfrase { get; set; }
        public string cntc_vpensamiento { get; set; }
        public string espad_vnivel { get; set; }
        public decimal? cntc_nmonto_total_foma { get; set; }
        public decimal? cntc_nmonto_total_foma_pagado { get; set; }
        public string cntc_vnumero_contrato_corr { get; set; }

        //CAMPOS PARA LA SOLICITUD
        public int? cntc_iindicador_contr_sol { get; set; }
        public string cntc_vnumero_solicitud { get; set; }
        public int? cntc_iestado_sol { get; set; }
        public string cntc_vobservaciones_sol { get; set; }
        public string strSerie { get { return cntc_vnumero_contrato.Substring(0,4); } }

        public string EstadoCivilContratante { get; set; }
        public string ParentescoContratante { get; set; }
        public string cntc_vorigen_registro { get; set; }
    }
}
