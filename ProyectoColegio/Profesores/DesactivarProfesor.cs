using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoColegio.Modelos;
using ProyectoColegio.Archivos;
using ProyectoColegio.Utilidades;

namespace ProyectoColegio.Profesores
{
    public static class DesactivarProfesor
    {
        public static void Ejecutar(List<Estudiante> estudiantes, List<Profesor> profesores, string usuario)
        {
            Console.Clear();
            Console.WriteLine("=== DESACTIVAR PROFESOR (ELIMINACIÓN LÓGICA) ===");
            int id = Validador.LeerEntero("Ingrese ID del profesor a desactivar: ", 1, int.MaxValue);
            var prof = profesores.Find(p => p.Id == id);

            if (prof == null)
            {
                Console.WriteLine(" [!] Error: No existe un profesor con ese ID.");
                Console.ReadKey();
                return;
            }

            if (!prof.Activo)
            {
                Console.WriteLine(" [!] Error: Este registro ya se encuentra inactivo.");
                Console.ReadKey();
                return;
            }

            if (Validador.PedirConfirmacion($"¿Está seguro de desactivar al profesor {prof.Nombre} {prof.Apellido}?"))
            {
                prof.Activo = false;
                ArchivoDatos.GuardarTodo(estudiantes, profesores);
                ArchivoLogs.RegistrarAccion(usuario, "DESACTIVAR_PROFESOR", $"ID: {id}");
                Console.WriteLine("\n [✓] Profesor pasado a estado INACTIVO correctamente.");
            }
            else
            {
                Console.WriteLine("\n [i] Operación cancelada.");
            }
            Console.ReadKey();
        }
    }
}