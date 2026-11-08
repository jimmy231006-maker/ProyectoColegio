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
    public static class ReactivatorEstudiante
    {
        public static void Ejecutar(List<Estudiante> estudiantes, List<Profesor> profesores, string usuario)
        {
            Console.Clear();
            Console.WriteLine("=== REACTIVAR ESTUDIANTE ===");
            int id = Validador.LeerEntero("Ingrese ID del estudiante a reactivar: ", 1, int.MaxValue);
            var est = estudiantes.Find(e => e.Id == id);

            if (est == null)
            {
                Console.WriteLine(" [!] Error: No existe un estudiante con ese ID.");
                Console.ReadKey();
                return;
            }

            // Regla: No se puede reactivar uno que ya esté activo[cite: 3]
            if (est.Activo)
            {
                Console.WriteLine(" [!] Error: Este registro ya se encuentra ACTIVO.");
                Console.ReadKey();
                return;
            }

            if (Validador.PedirConfirmacion($"¿Desea reactivar al estudiante {est.Nombre} {est.Apellido}?"))
            {
                est.Activo = true;
                ArchivoDatos.GuardarTodo(estudiantes, profesores);
                ArchivoLogs.RegistrarAccion(usuario, "REACTIVAR_ESTUDIANTE", $"ID: {id}");
                Console.WriteLine("\n [✓] Estudiante reactivado correctamente.");
            }
            else
            {
                Console.WriteLine("\n [i] Operación cancelada.");
            }
            Console.ReadKey();
        }
    }
}