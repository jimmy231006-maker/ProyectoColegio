using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoColegio.Modelos;

namespace ProyectoColegio.Estudiantes
{
    public static class ListarEstudiante
    {
        public static void Ejecutar(List<Estudiante> estudiantes)
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE ESTUDIANTES ===");
            if (estudiantes.Count == 0)
            {
                Console.WriteLine(" [!] No hay registros en el sistema.");
            }
            else
            {
                Console.WriteLine("\n--- REGISTROS ACTIVOS ---");
                var activos = estudiantes.FindAll(e => e.Activo);
                if (activos.Count == 0) Console.WriteLine(" (No hay estudiantes activos)");
                foreach (var e in activos) e.MostrarInformacion();

                Console.WriteLine("\n--- REGISTROS INACTIVOS (HISTÓRICO) ---");
                var inactivos = estudiantes.FindAll(e => !e.Activo);
                if (inactivos.Count == 0) Console.WriteLine(" (No hay estudiantes inactivos)");
                foreach (var e in inactivos) e.MostrarInformacion();
            }
            Console.WriteLine("\nPresione una tecla para regresar al menú...");
            Console.ReadKey();
        }
    }
}