using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EInasistencia : EAuditoria
    {
        public int  peric_icod_inasist{get; set;}		
        public int  peric_icod_personal{get; set;}		
        public string peric_vobservaciones{get; set;}		
        public DateTime peric_sfecha_anasist{get; set;}		
        public bool peric_flag_estado{get; set;}			
        public int peric_iusuario_crea{get; set;}		
        public DateTime peric_sfecha_crea{get; set;}			
        public string peric_vpc_crea{get; set;}			
        public int  peric_iusuario_modifica{get; set;}	
        public DateTime peric_sfecha_modifica{get; set;}		
        public string peric_vpc_modifica{get; set;}		
        public int peric_iusuario_elimina{get; set;}	
        public DateTime peric_sfecha_elimina{get; set;}		
        public string peric_vpc_elimina{get; set;}	
        
        public string perc_iid_personal { get; set; }
        public string perc_vnum_doc { get; set; }

        public string strNombrePersonal { get; set;  }
    }
}
