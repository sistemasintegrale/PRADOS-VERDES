using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public  class EOrdenCompraServicio : EOrdenCompraServicioDetalle
    {
       public int ocsc_icod_ocs { get; set; }
       public int ocsc_ianio { get; set; }
       public string ocsc_vnumero_ocs { get; set; }
	   public DateTime ocsc_sfecha_ocs { get; set; }
	   public int proc_icod_proveedor { get; set; }
	   public string prvc_vcod_proveedor { get; set; }
	   public int tablc_iid_situacion_ocs { get; set; }
	   public string ocsc_vtelefono { get; set; }
	   public string ocsc_vcontacto { get; set; }
	   public int tablc_iid_tipo_moneda { get; set; }
	   public decimal ocsc_npor_imp_igv { get; set; }
	   public string ocsc_vreferncia { get; set; }
	   public int ocsc_iid_tipo { get; set; }
	   public int ocsc_iid_proyecto { get; set; }
	   public string ocsc_vforma_pago { get; set; }
	   public string ocsc_vlugar_entrega { get; set; }
	   public string ocsc_vgarantia { get; set; }
	   public string ocsc_vnota_ocs { get; set; }
	   public decimal ocsc_nmonto_neto { get; set; }
	   public decimal ocsc_nmonto_descuento { get; set; }
	   public decimal ocsc_nmonto_impuesto { get; set; }
	   public decimal ocsc_nmonto_total { get; set; }
	   public decimal ocsc_nmonto_sub_total { get; set; }
	   public decimal ocsc_npor_descuento { get; set; }
	   public bool ocsc_bincluye_igv { get; set; }
       public bool ocsc_flag_estado { get; set; }
       public string proc_vruc { get; set; }
       public string proc_vdireccion { get; set; }
       public string proc_vcorreo { get; set; }
       public string proc_vtelefono { get; set; }
       public string strMoneda { get; set; }
       public string proc_vnombrecompleto { get; set; }
       public string str_situacion { get; set; }
       public int? lpedi_icod_proveedor { get; set; }

       public string str_tipo { get; set; }
       public string cecoc_vcodigo_centro_costo { get; set; }
       public int? prdc_icod_producto { get; set; }

       public string cecoc_vdescripcion { get; set; }

       public string ocsc_vcotizacion { get; set; }
       public string ocsc_vciudad { get; set; }
       public string ocsc_vnombre_atencion { get; set;}
       public string ocsc_vcelular_atencion { get; set;}
       public string ocsc_vemail_atencion { get; set;}
       public string ocsc_vdocumento_pago { get; set;}
       public string ocsc_vplazo_entrega { get; set;}
       public string ocsc_vpenalidad { get; set;}
       public string ocsc_vArea { get; set; }
	   public string ocsc_vDestino_Final { get; set; }
	   public string ocsc_vResponsable { get; set; }
	   public string ocsc_vBanco { get; set; }
	   public string ocsc_vNumero_Cuenta { get; set; }
	   public string ocsc_vCCI { get; set; }
	   public string ocsc_vMoneda { get; set; }

    }
}
