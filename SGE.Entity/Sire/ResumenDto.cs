namespace SGE.Entity.Sire
{
    public class ResumenDto
    {

        public string tipoDoc { get; set; }
        public int totalDocumentos { get; set; }
        public decimal totalGravado { get; set; }
        public decimal totalIGV { get; set; }
        public decimal totalDestinonoGravado { get; set; }
        public decimal totalOtrosTributos { get; set; }
        public decimal totalCP { get; set; }
    }

}
