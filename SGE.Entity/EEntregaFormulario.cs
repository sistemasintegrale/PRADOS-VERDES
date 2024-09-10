using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EEntregaFormulario
    {
        public int entf_icod_entrega { get; set; }
        public int entf_iid_entrega { get; set; }
        public DateTime? entf_sfecha_entrega { get; set; }
        public int entf_icod_vendedor { get; set; }
        public string entf_vobservaciones { get; set; }
        public int entf_iusuario_crea { get; set; }
        public DateTime? entf_sfecha_crea { get; set; }
        public string entf_vpc_crea { get; set; }
        public int entf_iusuario_modifica { get; set; }
        public DateTime? entf_sfecha_modifica { get; set; }
        public string entf_vpc_modifica { get; set; }
        public bool entf_bflag_estado { get; set; }
        public string vendc_vnombre_vendedor { get; set; }
        public int entf_icod_estado { get; set; }
        public string entf_vnumero_formulario { get; set; }
        public string cntc_vnombre_contratante { get; set; }
        public string cntc_vapellido_paterno_contratante { get; set; }
        public string cntc_vapellido_materno_contratante { get; set; }
        public string strContratante { get; set; }
        public string strFormulario { get { return entf_vnumero_formulario.Insert(4, "-"); } }
    }
}
