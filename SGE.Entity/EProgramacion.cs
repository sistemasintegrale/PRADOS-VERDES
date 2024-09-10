using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EProgramacion
    {
        public int rpd_icod_registro_programacion_det { get; set; }
        public int rp_icod_registro_programacion { get; set; }
        public DateTime rp_fecha { get; set; }
        public int rpd_iorden { get; set; }
        public string rpd_vhora_inicio { get; set; }
        public string rpd_vhora_final { get; set; }
        public string rpd_vnombre_fallecido { get; set; }
        public int cntc_icod_contrato { get; set; }
        public int espad_iid_iespacios { get; set; }
        public int rpd_itipo_sepultura { get; set; }
        public int rpd_icod_deceso { get; set; }
        public int rpd_icod_vendedor { get; set; }
        public int rpd_icod_funeraria { get; set; }
        public string NumContrato { get; set; }
        public string strtiposepultura { get; set; }
        public string strDeceso { get; set; }
        public string strNombreVendedor { get; set; }
        public string strFuneraria { get; set; }
        public string strplataforma { get; set; }
        public string strmanzana { get; set; }
        public string strsepultura { get; set; }
        public string Nivel { get; set; }
        public string rpd_vcontrato { get; set; }
        public string rpd_vfuneraria { get; set; }
        public string rpd_vcontratante { get; set; }
        public string rpd_observaciones { get; set; }
        public int rpd_icod_situacion { get; set; }
    }
}
