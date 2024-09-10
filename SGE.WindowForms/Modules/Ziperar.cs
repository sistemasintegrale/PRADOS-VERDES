using System.IO;
using System.IO.Compression;

namespace SGE.WindowForms.Modules
{
    public static class Ziperar
    {

        public static void CrearArchivoZip(string archivoPlano, string rutaArchivoZip)
        {
            // Creamos un nuevo archivo ZIP y lo abrimos para escritura                                        
            using (var zipFile = new FileStream(rutaArchivoZip, FileMode.Create))
            {
                // Creamos el archivo ZIP usando ZipArchive
                using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
                {
                    // Agregamos el archivo plano al archivo ZIP
                    var entry = archive.CreateEntry(Path.GetFileName(archivoPlano));
                    using (var entryStream = entry.Open())
                    {
                        // Leemos el contenido del archivo plano y lo escribimos en el archivo ZIP
                        byte[] contenidoArchivo = File.ReadAllBytes(archivoPlano);
                        entryStream.Write(contenidoArchivo, 0, contenidoArchivo.Length);
                    }
                }
            }

        }
    }
}
