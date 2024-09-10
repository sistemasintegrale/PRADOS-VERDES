using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EEntregaFormularioDetalle
    {
        public int entfd_icod_entrega { get; set; }
        public int entf_icod_entrega { get; set; }
        public int entfd_iid_entrega { get; set; }
        public string entfd_vdocumento { get; set; }
        public int entfd_iusuario_crea { get; set; }
        public DateTime? entfd_sfecha_crea { get; set; }
        public string entfd_vpc_crea { get; set; }
        public int entfd_iusuario_modifa { get; set; }
        public DateTime? entfd_sfecha_modifica { get; set; }
        public string entfd_vpc_modifica { get; set; }
        public bool entfd_bflag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        public string strEstado { get; set; }
        public string entfd_vestado { get; set; }
    }
}
