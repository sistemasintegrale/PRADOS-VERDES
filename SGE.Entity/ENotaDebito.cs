using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ENotaDebito : EAuditoria
    {
        public int ndebc_icod_debito { get; set; }
        public string ndebc_vnumero_debito { get; set; }
        public DateTime ndebc_sfecha_debito { get; set; }
        public int cliec_icod_cliente { get; set; }
        public int ndebc_ianio { get; set; }
        public string ndebc_vreferencia { get; set; }
        public int? tdocc_icod_tipo_doc { get; set; }
        public int? tablc_iid_tipo_nota_credito_venta { get; set; }
        public int? tdodc_iid_correlativo { get; set; }
        public string ndebc_vnumero_documento { get; set; }
        public DateTime ndebc_sfecha_documento { get; set; }
        public int? vendc_icod_vendedor { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal ndebc_nmonto_neto { get; set; }
        public decimal ndebc_npor_imp_igv { get; set; }
        public decimal ndebc_nmonto_imp { get; set; }
        public decimal ndebc_nmonto_total { get; set; }
        public int ndebc_iid_situacion_debito { get; set; }
        public int? almac_icod_almacen { get; set; }
        public decimal ndebc_tipo_cambio_fecha_doc_venta { get; set; }
        public decimal ndebc_nmonto_iva { get; set; }
        public decimal ndebc_nmonto_pagado { get; set; }
        public Int64 ndebc_icod_dxc { get; set; }
        public Boolean? ndebc_bincluye_igv { get; set; }
        /**/
        public string strSituacion { get; set; }
        public int ubicc_icod_ubicacion { get; set; }
        public string strDesCliente { get; set; }
        public string strRuc { get; set; } 
        public string strTipoDoc { get; set; }        
        public string strMoneda { get; set; }
        public int ndebc_iclase_doc { get; set; }
        public string StrClaseDocumento { get; set; }
        public int ndebc_tipo_nota_debito { get; set; }

        public Boolean ndebc_bind_arroz { get; set; }
        public decimal ndebc_npor_imp_ivap { get; set; }
        public decimal ndebc_nmonto_ivap { get; set; }
        public string NomVendedor { get; set; }
        public decimal ndebc_nmonto_neto_ivap { get; set; }
        public decimal ndebc_nmonto_neto_exo { get; set; }

        /*Datos Factura Electronica*/
        public String nombreLegalEmisor { get; set; }
        public string nroDocumentoEmisior { get; set; }
        public string nombreComercialEmisor { get; set; }
        public string direccionEmisor { get; set; }
        public string nroDocumentoReceptor { get; set; }
        public string nombreLegalReceptor { get; set; }
        public string nombreComercialReceptor { get; set; }
        public string direccionReceptor { get; set; }

        public string tipoDocRef { get; set; }
        public string numDocRef { get; set; }
        public int codigoMotivoRef { get; set; }
        public string desMotivoRef { get; set; }
        public int doc_icod_documento { get; set; }
        public int tablc_iid_tipo_motivo_sunat { get; set; }
        public string DesTipoSunat { get; set; }
        public int IdCabezera { get; set; }

    }
}
