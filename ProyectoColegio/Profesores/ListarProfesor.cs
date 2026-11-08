using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoColegio.Modelos;

namespace ProyectoColegio.Profesores
{
    public static class ListarProfesor
    {
        public static void Ejecutar(List<Profesor> profesores)
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE PROFESORES ===");
            if (profesores.Count == 0)
            {
                Console.WriteLine(" [!] No hay registros en el sistema.");
            }
            else
            {
                Console.WriteLine("\n--- REGISTROS ACTIVOS ---");
                var activos = profesores.FindAll(p => p.Activo);
                if (activos.Count == 0) Console.WriteLine(" (No hay profesores activos)");
                foreach (var p in activos) p.MostrarInformacion();

                Console.WriteLine("\n--- REGISTROS INACTIVOS (HISTÓRICO) ---");
                var inactivos = profesores.FindAll(p => !p.Activo);
                if (inactivos.Count == 0) Console.WriteLine(" (No hay profesores inactivos)");
                foreach (var p in inactivos) p.MostrarInformacion();
            }
            Console.WriteLine("\nPresione una tecla para regresar al menú...");
            Console.ReadKey();
        }
    }
}