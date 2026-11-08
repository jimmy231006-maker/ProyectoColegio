using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProyectoColegio.Modelos;
using ProyectoColegio.Archivos;
using ProyectoColegio.Utilidades;

namespace ProyectoColegio.Estudiantes
{
    public static class DesactivarEstudiante
    {
        public static void Ejecutar(List<Estudiante> estudiantes, List<Profesor> profesores, string usuario)
        {
            Console.Clear();
            Console.WriteLine("=== DESACTIVAR ESTUDIANTE (ELIMINACIÓN LÓGICA) ===");
            int id = Validador.LeerEntero("Ingrese ID del estudiante a desactivar: ", 1, int.MaxValue);
            var est = estudiantes.Find(e => e.Id == id);

            if (est == null)
            {
                Console.WriteLine(" [!] Error: No existe un estudiante con ese ID.");
                Console.ReadKey();
                return;
            }

            if (!est.Activo)
            {
                Console.WriteLine(" [!] Error: Este registro ya se encuentra inactivo.");
                Console.ReadKey();
                return;
            }

            if (Validador.PedirConfirmacion($"¿Está seguro de desactivar al estudiante {est.Nombre} {est.Apellido}?"))
            {
                est.Activo = false;
                ArchivoDatos.GuardarTodo(estudiantes, profesores);
                ArchivoLogs.RegistrarAccion(usuario, "DESACTIVAR_ESTUDIANTE", $"ID: {id}");
                Console.WriteLine("\n [✓] Estudiante pasado a estado INACTIVO correctamente.");
            }
            else
            {
                Console.WriteLine("\n [i] Operación cancelada.");
            }
            Console.ReadKey();
        }
    }
}