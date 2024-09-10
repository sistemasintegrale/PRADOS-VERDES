using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EProyeccionVendedor
    {
        public int proyc_icod_proyeccion { get; set; }
        public int vendc_icod_vendedor { get; set; }
        public int anioc_iid_anio_ejercicio { get; set; }
        public int proyc_imes { get; set; }
        public int proyc_icantidad_estimada { get; set; }
        public int proyc_iusuario { get; set; }
        public string proyc_vpc { get; set; }

        //reporte

        public string vendc_vnombre_vendedor { get; set; }
        public string vendc_vcod_vendedor { get; set; }
        public int cant_necesidad_futura { get; set; }
        public int cant_necesidad_inmediata { get; set; }
        public int cant_credito { get; set; }
        public int cant_contado { get; set; }
    }
}
