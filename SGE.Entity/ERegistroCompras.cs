using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SGE.Entity.Sire;

namespace SGE.Entity
{
    public class ERegistroCompras
    {
        [DataMember]
        public long doxpc_icod_correlativo { get; set; }
        [DataMember]
        public string doxpc_viid_correlativo { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public int? tdodc_iid_correlativo { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_doc { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public string simboloMoneda { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }

        //montos
        [DataMember]
        public decimal? doxpc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_gravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_mixto { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_referencial_cif { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_servicio_no_domic { get; set; }
        [DataMember]
        public decimal? valor_compra { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_gravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_mixto { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_isc { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_pagado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_saldo { get; set; }
        //montos necesarios para el reporte
      
        [DataMember]
        public string situacion_documento { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_vencimiento_doc { get; set; }
        [DataMember]
        public string doxpc_vnro_deposito_detraccion { get; set; }
        [DataMember]
        public DateTime? doxpc_sfec_deposito_detraccion { get; set; }

        //variables para el reporte
        [DataMember]
        public string str_doxpc_icod_correlativo { get; set; }
        [DataMember]
        public string str_doxpc_sfecha_doc { get; set; }
        [DataMember]
        public string str_doxpc_sfecha_vencimiento_doc { get; set; }
        [DataMember]
        public string tdocc_vcodigo_tipo_doc_sunat { get; set; }
        [DataMember]
        public string tip_doc_proveedor { get; set; }
        [DataMember]
        public string num_doc_proveedor { get; set; }
        [DataMember]
        public string str_doxpc_sfec_deposito_detraccion { get; set; }
        [DataMember]
        public string tdocc_vdescripcion { get; set; }

        //montos reporte        
        [DataMember]
        public string str_doxpc_nmonto_destino_gravado { get; set; } //Base Imponible Oper.Gravada
        [DataMember]
        public string str_doxpc_nmonto_destino_mixto { get; set; } //Base Imponible Oper.Comun
        [DataMember]
        public string str_doxpc_nmonto_destino_nogravado { get; set; } //Oper.Destino No gravado
        [DataMember]
        public string str_doxpc_nmonto_nogravado { get; set; } //Adq.No Gravada 
        [DataMember]
        public string str_doxpc_nmonto_referencial_cif { get; set; }
        [DataMember]
        public string str_doxpc_nmonto_imp_destino_gravado { get; set; } //igv op. gravada
        [DataMember]
        public string str_doxpc_nmonto_imp_destino_mixto { get; set; } //igv op. común
        [DataMember]
        public string str_doxpc_nmonto_imp_destino_nogravado { get; set; } //igv op. no gravada
        [DataMember]
        public string str_doxpc_nmonto_isc { get; set; }
  
        [DataMember]
        public string str_doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public string str_doxpc_nmonto_tipo_cambio { get; set; }



        //variables referencia de nota de crédito
        [DataMember]
        public string nc_dxc_tipodoc { get; set; }
        //[DataMember]
        //public string nc_dxc_numdoc { get; set; }
        //[DataMember]
        //public string nc_dxc_fecha { get; set; }


        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int? doxpc_numdoc_tipo { get; set; }


        [DataMember]
        public int? doxpc_tipo_comprobante_referencia { get; set; }
        [DataMember]
        public string doxpc_num_serie_referencia { get; set; }
        [DataMember]
        public string doxpc_num_comprobante_referencia { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_emision_referencia { get; set; }
        public string CUO { get; set; }
        public string doxpc_vnumero_serio { get; set; }
        public string doxpc_vnumero_correlativo { get; set; }
        [DataMember]
        public string vcocc_numero_vcontable { get; set; }
        [DataMember]
        public int subdi_icod_subdiario { get; set; }
        [DataMember]
        public string ViddAdquisicion { get; set; }

        public int doxpc_iid_correlativo { get; set; }
        /*DUA*/
        public string doxpc_codigo_aduana { get; set; }
        public string doxpc_anio { get; set; }
        public string doxpc_numero_declaracion { get; set; }
        public decimal MontoDUA { get; set; }
        public decimal MontoTotalDUA { get; set; }
        /*PROVEEDOR*/
        public int proc_pais_nodomic { get; set; }
        public string proc_vdireccion { get; set; }
        public string proc_vdni { get; set; }
        public int doxpc_vclasific_doc { get; set; }
        public string strClasificacion { get; set; }
        public string strNumDocRef { get; set; }

}
    public class RegistroComprasConvert
    {

        public static List<RegistroComprasDTO> Convertir(List<ERegistroCompras> listaOrigen, string RUC, string razonSocial, string periodo)
        {
            int index = 0;
            var lista = new List<RegistroComprasDTO>();
            try
            {
                listaOrigen.ForEach(x =>
                {
                    index += 1;
                    if (index == 68)
                    {

                    }
                    var newdata = new RegistroComprasDTO();
                    newdata.NumRuc = RUC;
                    newdata.NomRazonSocial = razonSocial;
                    newdata.CodCar = $"{x.num_doc_proveedor}{x.tdocc_vcodigo_tipo_doc_sunat}{x.doxpc_vnumero_doc}";
                    newdata.CodTipoCdp = x.tdocc_vcodigo_tipo_doc_sunat;
                    newdata.NumSerieCdp = x.doxpc_vnumero_serio;
                    newdata.NumCdp = x.doxpc_numdoc_tipo == 1 ? x.doxpc_vnumero_correlativo.TrimStart('0') : x.doxpc_vnumero_doc.Replace("-", "").Replace(" ", "");
                    newdata.FecEmision = x.doxpc_sfecha_doc.Value.ToString("dd/MM/yyyy");
                    newdata.FecVencPag = x.doxpc_sfecha_vencimiento_doc.Value.ToString("dd/MM/yyyy");
                    //NumCdpRangoFinal = x.NumCdpRangoFinal,
                    newdata.CodTipoDocIdentidadProveedor = Convert.ToInt32(x.tip_doc_proveedor);
                    newdata.NumDocIdentidadProveedor = x.num_doc_proveedor;
                    newdata.NomRazonSocialProveedor = x.proc_vnombrecompleto;
                    newdata.PerTributario = Convert.ToInt32(periodo);
                    newdata.NumCorrelativo = int.Parse(x.doxpc_viid_correlativo.TrimStart('0'));
                    newdata.MtoOtrosTrib = 0;//x.doxpc_otros_impuestos;
                    newdata.MtoTotalCp = x.doxpc_nmonto_total_documento;
                    newdata.MtoIsc = x.doxpc_nmonto_isc;
                    newdata.FecEmisionMod = x.doxpc_sfecha_emision_referencia.HasValue ? x.doxpc_sfecha_emision_referencia.Value.ToString("dd/MM/yyyy") : "";
                    newdata.CodTipoCdpMod = x.nc_dxc_tipodoc;
                    newdata.NumSerieCdpMod = x.doxpc_num_serie_referencia;
                    newdata.NumCdpMod = x.doxpc_num_comprobante_referencia.TrimStart('0').Length != 0 ? int.Parse(x.doxpc_num_comprobante_referencia.TrimStart('0')) : 0;


                    lista.Add(new RegistroComprasDTO
                    {

                        NumRuc = RUC,

                        NomRazonSocial = razonSocial,
                        CodCar = $"{x.num_doc_proveedor}{x.tdocc_vcodigo_tipo_doc_sunat}00{x.doxpc_vnumero_doc}",
                        CodTipoCdp = x.tdocc_vcodigo_tipo_doc_sunat,
                        NumSerieCdp = x.doxpc_numdoc_tipo == 1 ? x.doxpc_vnumero_serio : "",
                        NumCdp = x.doxpc_numdoc_tipo == 1 ? x.doxpc_vnumero_correlativo.TrimStart('0') : x.doxpc_vnumero_doc.Replace("-", "").Replace(" ", ""),
                        FecEmision = x.doxpc_sfecha_doc.Value.ToString("dd/MM/yyyy"),
                        FecVencPag = x.doxpc_sfecha_vencimiento_doc.Value.ToString("dd/MM/yyyy"),
                        CodTipoDocIdentidadProveedor = Convert.ToInt32(x.tip_doc_proveedor),
                        NumDocIdentidadProveedor = x.num_doc_proveedor,
                        NomRazonSocialProveedor = x.proc_vnombrecompleto,
                        CodMoneda = x.tablc_iid_tipo_moneda == 3 ? "PEN" : "USD",
                        PerTributario = Convert.ToInt32(periodo),
                        NumCorrelativo = int.Parse(x.doxpc_viid_correlativo.TrimStart('0')),
                        PorTasaIgv = 0,
                        IndCargaTipoCambio = 0,
                        MtoCambioMonedaExtranjera = 0,
                        MtoCambioMonedaDolares = 0,
                        MtoTipoCambio = x.doxpc_nmonto_tipo_cambio,
                        // Montos Montos               
                        MtoBiGravadaDg = x.MontoDUA,
                        MtoIgvIpmDg = x.doxpc_nmonto_imp_destino_gravado,
                        MtoBiGravadaDgng = 0,
                        MtoIgvIpmDgng = 0,
                        MtoBiGravadaDng = 0,
                        MtoIgvIpmDng = 0,
                        MtoValorAdqNg = x.doxpc_nmonto_nogravado,
                        MtoIcbp = 0,
                        MtoOtrosTrib = 0,//x.doxpc_otros_impuestos,
                        MtoTotalCp = x.doxpc_nmonto_total_documento,
                        MtoIsc = x.doxpc_nmonto_isc,
                        MtoImb = 0,
                        MtoBiGravadaDgOriginal = 0,
                        MtoIgvIpmDgOriginal = 0,
                        // List<LisDocumentosMod> LisDocumentosMod 
                        FecEmisionMod = x.doxpc_sfecha_emision_referencia.HasValue ? x.doxpc_sfecha_emision_referencia.Value.ToString("dd/MM/yyyy") : "",
                        CodTipoCdpMod = x.nc_dxc_tipodoc,
                        NumSerieCdpMod = x.doxpc_num_serie_referencia,
                        NumCdpMod = x.doxpc_num_comprobante_referencia.TrimStart('0').Length != 0 ? int.Parse(x.doxpc_num_comprobante_referencia.TrimStart('0')) : 0,
                        //CamposLibres = x.CamposLibres
                        NumeroDetraccion = x.doxpc_vnro_deposito_detraccion,
                        FechaDetraccion = x.doxpc_sfec_deposito_detraccion

                    });
                });
            }
            catch (Exception ex)
            {
                index = index;
                throw ex;
            }
            return lista;

        }
    }
}
