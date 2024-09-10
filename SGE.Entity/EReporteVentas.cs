using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EReporteVentas : EAuditoria
    {
        public int revec_icod_reporte_ventas { get; set; }
        public int revec_iid_reporte_ventas { get; set; }
        public int vendc_icod_vendedor { get; set; }
        public string revec_vprimer_nombre { get; set; }
        public string revec_vsegundo_nombre { get; set; }
        public string revec_vapellido_paterno { get; set; }
        public string revec_vapellido_materno { get; set; }
        public int tablc_iid_tipo_tabla { get; set; }
        public int disc_icod_distrito { get; set; }
        public int func_icod_funeraria { get; set; }
        public bool revec_flag_estado { get; set; }
        public string vendc_vnombre_vendedor { get; set; }
        public string tarec_vdescripcion { get; set; }
        public string disc_vdescripcion { get; set; }
        public string func_vnombre_comercial { get; set; }
        public string func_cnumero_docum_fun { get; set; }
        public string func_vtelefonos { get; set; }
        public string func_vcontacto { get; set; }
    }
}
