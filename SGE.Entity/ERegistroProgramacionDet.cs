using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class ERegistroProgramacionDet : EAuditoria
    {
        public int rpd_icod_registro_programacion_det { get; set; }
        public int rp_icod_registro_programacion { get; set; }
        public int rpd_iorden { get; set; }
        public string rpd_vhora_inicio { get; set; }
        public string rpd_vhora_final { get; set; }
        public string rpd_vnombre_fallecido { get; set; }
        public int cntc_icod_contrato { get; set; }
        public int espad_iid_iespacios { get; set; }
        public string NumContrato { get; set; }
        public string strtiposepultura { get; set; }
        public string strplataforma { get; set; }
        public string strmanzana { get; set; }
        public string Nivel { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
