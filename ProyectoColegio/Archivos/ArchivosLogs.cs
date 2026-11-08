using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ProyectoColegio.Archivos
{
    public static class ArchivoLogs
    {
        private static readonly string RutaLogs = "logs.dat";

        public static void RegistrarAccion(string usuario, string accion, string detalle)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(RutaLogs, FileMode.Append, FileAccess.Write)))
                {
                    writer.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    writer.Write(usuario);
                    writer.Write(accion);
                    writer.Write(detalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" [!] Error en registro de logs: {ex.Message}");
            }
        }
    }
}