namespace SGE.DataAccess
{
    public class Helper
    {
        public static string conexion()
        {
            string strConexion;
            /*LOCAL*/
            //strConexion = @"Data Source=SERVER\SERVIDOR; Initial Catalog =parquesdelparaiso_pe_PV; Integrated Security=true ;";
            /*SERVIDOR*/
            strConexion = "Data Source=tcp:95.217.194.247,1433;Initial Catalog=parquesdelparaiso_pe_PV;Persist Security Info=True;User ID=parquesdelparaiso_pe_de;Password=rogola2012";
            return strConexion;
        }


    }
}







