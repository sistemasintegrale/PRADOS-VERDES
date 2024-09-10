using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EPlanillaModeloCont : EAuditoria
    {
        public int plcc_pland_icod { get; set; }
        public int plcd_iid { get; set; }
        public string plcd_vdescrpcion { get; set; }
        public decimal plcd_montos_enero { get; set; }
        public decimal plcd_montos_febrero { get; set; }
        public decimal plcd_montos_marzo { get; set; }
        public decimal plcd_montos_abril { get; set; }
        public decimal plcd_montos_mayo { get; set; }
        public decimal plcd_montos_junio { get; set; }
        public decimal plcd_montos_julio { get; set; }
        public decimal plcd_montos_agosto { get; set; }
        public decimal plcd_montos_setiembre { get; set; }
        public decimal plcd_montos_octubre { get; set; }
        public decimal plcd_montos_noviembre { get; set; }
        public decimal plcd_montos_diciembre { get; set; }
        public decimal Basicodelmes { get; set; }
        public decimal Comisiones_Mes { get; set; }
        public decimal Gratificacion_Ordinaria { get; set; }
        public decimal Remuneracion_Anteriores { get; set; }
        public decimal Comisiones { get; set; }
        public decimal txtHasta_5_UIT { get; set; }
        public int plcd_iusuario_crea { get; set; }
        public string plcd_vpc_crea { get; set; }
        public int plcd_iusuario_modifica { get; set; }
        public string plcd_vpc_modifica { get; set; }
        public int rnt_5ta_icod { get; set; }
        public int plcd_icod_personal { get; set; }
        public string strValores { get; set; }
        public string strNombrePersonal { get; set; }

    }
}
