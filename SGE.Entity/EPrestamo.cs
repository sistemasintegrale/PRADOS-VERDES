using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EPrestamo : ECuotasPrestamo
    {
        public int prtpc_icod_prestamo { get; set; }
        public int prtpc_inro_cuotas { get; set; }
        public int prtpc_icod_personal { get; set; }
        public string prtpc_vnumero_prestamo { get; set; }
        public DateTime prtpc_sfecha_prestamo { get; set; }
        public decimal prtpc_nmonto_prestamo { get; set; }
        public decimal prtpc_nmonto_cuota { get; set; }
        public int prtpc_icod_situacion { get; set; }
        public bool prtpc_flag_estado { get; set; }
        public int prtpc_iusuario_crea { get; set; }
        public DateTime prtpc_sfecha_crea { get; set; }
        public string  prtpc_vpc_crea { get; set; }
        public int prtpc_iusuario_modifica { get; set; }
        public DateTime prtpc_sfecha_modifica { get; set; }
        public string prtpc_vpc_modifica { get; set; }
        public int prtpc_iusuario_elimina { get; set; }
        public DateTime  prtpc_sfecha_elimina { get; set; }
        public string prtpc_vpc_elimina { get; set; }
        public string perc_iid_personal { get; set; }
        public string strNombrePersonal { get; set; }
        public string strEstado { get; set; }
        public int intTipoOperacion { get; set; }
        public DateTime prtpc_sfecha_inicio_prest { get; set; }
        public int? prtpc_icod_tipo_pago { get; set; }
        public string dniPersonal { get; set; }
        public string strTipoPagoC { get { return prtpc_icod_tipo_pago == 1 ? "QUINCENAL" : "MENSUAL"; } }
        public DateTime? primerVencimiento { get; set; }
        public DateTime? ultimoVencimiento { get; set; }
    }
}
