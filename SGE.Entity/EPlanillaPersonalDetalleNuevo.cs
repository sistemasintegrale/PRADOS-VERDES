using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EPlanillaPersonalDetalleNuevo : EAuditoria
    {

        public int planc_icod_planilla_personal { get; set; }
        public int pland_icod_planilla_personal_det { get; set; }
                     
        public int planc_icod_personal { get; set; }
        public int pland_iid_planilla_personal_det { get; set; }
        public string pland_ape_nom { get; set; }
        public DateTime pland_sfecha_incio { get; set; }
        public string pland_num_doc { get; set; }
        public string strpland_afp { get; set; }
        public int pland_afp { get; set; }
        public string pland_cussp { get; set; }
        public decimal pland_sueldo_basico { get; set; }
        public decimal pland_nasignacion_familiar { get; set; }
        public decimal pland_nferiados { get; set; }
        public decimal pland_Mtardanzas { get; set; }
        public decimal pland_tardanzas { get; set; }
        public decimal pland_reintegro { get; set; }
        public decimal pland_nasignacion_transporte { get; set; }
        public decimal pland_bonos { get; set; }
        public decimal pland_comisiones { get; set; }
        public decimal pland_faltas { get; set; }
        public decimal pland_total_neto { get; set; }
        public decimal pland_obligat { get; set; }
        public decimal pland_seguro { get; set; }
        public decimal pland_porcent { get; set; }
        public decimal pland_desc_renta5 { get; set; }
        public decimal pland_adelanto { get; set; }
        public decimal pland_prestamo { get; set; }
        public decimal pland_descuento { get; set; }
        public decimal pland_regularizar { get; set; }
        public decimal pland_total_pagar { get; set; }
        public decimal pland_aport_essalud9 { get; set; }
        public string pland_cuenta { get; set; }
        public string pland_observaciones { get; set; }
        public int pland_iusuario_crea { get; set; }
        public DateTime pland_vfecha_crea { get; set; }
        public string pland_strpc_crea { get; set; }
        public int pland_iusuario_modifica { get; set; }
        public DateTime pland_vfecha_modifica { get; set; }
        public string pland_strpc_modifica { get; set; }
        public int pland_iusuario_elimina { get; set; }
        public DateTime pland_vfecha_elimina { get; set; }
        public string pland_strpc_elimina { get; set; }
        public int operacion { get; set; }

        public string A { get; set; } //provicional

    }
}
