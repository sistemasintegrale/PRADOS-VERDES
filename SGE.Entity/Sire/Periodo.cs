using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class Periodo
    {
        public string perTributario { get; set; }
        public string codEstado { get; set; }
        public string desEstado { get; set; }
        public string select { get { return $"{GetNombreMes.ObtenerNombreDelMes(Convert.ToInt32(perTributario.Substring(4)))} - {desEstado.ToUpper()}"; } }
    }

    public static class GetNombreMes
    {
        public static string ObtenerNombreDelMes(int numero)
        {

            switch (numero)
            {
                case 1:
                    return "Enero".ToUpper();
                case 2:
                    return "Febrero".ToUpper();
                case 3:
                    return "Marzo".ToUpper();
                case 4:
                    return "Abril".ToUpper();
                case 5:
                    return "Mayo".ToUpper();
                case 6:
                    return "Junio".ToUpper();
                case 7:
                    return "Julio".ToUpper();
                case 8:
                    return "Agosto".ToUpper();
                case 9:
                    return "Septiembre".ToUpper();
                case 10:
                    return "Octubre".ToUpper();
                case 11:
                    return "Noviembre".ToUpper();
                case 12:
                    return "Diciembre".ToUpper();
                default:
                    return "Número de mes inválido";
            }
        }

    }


}

