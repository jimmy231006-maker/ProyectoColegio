using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ProyectoColegio.Modelos;

namespace ProyectoColegio.Archivos
{
    public static class Exportador
    {
        public static void ExportarReporte(List<Estudiante> estudiantes, List<Profesor> profesores, string usuarioActual)
        {
            try
            {
                string ruta = "reporte_colegio.txt";
                using (StreamWriter sw = new StreamWriter(ruta))
                {
                    sw.WriteLine("==================================================");
                    sw.WriteLine("          REPORTE OFICIAL - PROYECTO COLEGIO      ");
                    sw.WriteLine($"          Generado por: {usuarioActual}           ");
                    sw.WriteLine($"          Fecha: {DateTime.Now}                   ");
                    sw.WriteLine("==================================================\n");

                    sw.WriteLine("----------------- ESTUDIANTES --------------------");
                    foreach (var e in estudiantes)
                    {
                        string estado = e.Activo ? "ACTIVO" : "INACTIVO";
                        sw.WriteLine($"[ID: {e.Id}] Cédula: {e.Cedula} | {e.Nombre} {e.Apellido} | Edad: {e.Edad} | Estado: {estado}");
                    }

                    sw.WriteLine("\n------------------ PROFESORES --------------------");
                    foreach (var p in profesores)
                    {
                        string estado = p.Activo ? "ACTIVO" : "INACTIVO";
                        sw.WriteLine($"[ID: {p.Id}] Cédula: {p.Cedula} | {p.Nombre} {p.Apellido} | Materia: {p.Materia} | Estado: {estado}");
                    }
                }
                ArchivoLogs.RegistrarAccion(usuarioActual, "EXPORTACION", "Reporte formal generado exitosamente.");
                Console.WriteLine($"\n [✓] Reporte generado con éxito en:");
                Console.WriteLine($"     {Path.GetFullPath(ruta)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" [!] Error al exportar: {ex.Message}");
            }
        }
    }
}