namespace SGE.Entity
{
    public class EPlanNecesidadSepulturaDetalle : EAuditoria
    {
        public int id { get; set; }
        public int id_cab { get; set; }
        public int icantidad_cuotas { get; set; }
        public decimal nmonto { get; set; }
    }
}
