using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAlmacen : EAuditoria 
    {
        [DataMember]
        public int almac_icod_almacen { get; set; }
        [DataMember]
        public int almac_iid_almacen { get; set; }
        [DataMember]
        public string almac_vdescripcion { get; set; }
        [DataMember]
        public string almac_vdireccion { get; set; }
        [DataMember]
        public string almac_vrepresentante { get; set; }
        [DataMember]
        public bool? almac_sflag_estado { get; set; }
        [DataMember]
        public Boolean almac_nCant_stock_al { get; set; }

        [DataMember]
        public Boolean almac_bactivo_inac { get; set; }
        [DataMember]
        public int almac_iestado_almacen { get; set; }

        [DataMember]
        public string DescripcionEstaAlmacen { get; set; }

        public int almac_icod_pvt { get; set; }
        public string DesPVT { get; set; }

    }
}
