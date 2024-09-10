using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EContratoCuotas : EAuditoria
    {
        public int cntc_icod_contrato_cuotas { get; set; }
        public int cntc_icod_contrato { get; set; }
        public int cntc_inro_cuotas { get; set; }
        public DateTime cntc_sfecha_cuota { get; set; }
        public int cntc_icod_tipo_cuota { get; set; }
        public decimal cntc_nmonto_cuota { get; set; }
        public int cntc_icod_situacion { get; set; }
        public string strSituacion { get; set; }

        public int intTipoOperacion { get; set; }

        public string strTipo { get; set; }


        public string NumContrato { get; set; }


        public string cntc_vnombre_contratante { get; set; }
        public string cntc_vdni_contratante { get; set; }

        public Boolean cntc_flag_situacion { get; set; }
        public Boolean flag_multiple { get; set; }
        public Boolean flag_multiple_anterior { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public int cntc_icod_documento { get; set; }
        public string plnd_vnumero_doc { get; set; }
   
        public string strTipoDoc { get; set; }
        public int cntc_itipo_cuota { get; set; }
        public string strTipoCredito { get; set; }
        public int numero_cuotas { get; set; }
        public decimal monto_total { get; set; }
        public decimal monto_cuota { get; set; }
        public string strTipoCuota { get; set; }
        public decimal cntc_nsaldo { get; set; }
        public decimal cntc_npagado { get; set; }
        public decimal monto_pagar { get; set;  }
        public int pgc_icod_pago { get; set; }
        public decimal cntc_nmonto_mora_pago { get; set; }
        public string strfechaDocumentos { get; set; }
        public decimal cntc_nmonto_mora { get; set; }

        //IMPRESION
        public string tipo { get; set; }
        public bool eliminar { get; set; }

        public DateTime? cntc_sfecha_pago_cuota { get; set; }
        public bool cntc_bautomatico { get; set; }
    }
}
