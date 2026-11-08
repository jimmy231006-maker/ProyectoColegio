using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoColegio.Modelos;
using ProyectoColegio.Archivos;
using ProyectoColegio.Utilidades;
using System.Text.RegularExpressions;

namespace ProyectoColegio.Profesores
{
    public static class AgregarProfesor
    {
        public static void Ejecutar(List<Estudiante> estudiantes, List<Profesor> profesores, string usuario)
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR PROFESOR ===");
            int id = Validador.LeerEntero("Ingrese ID único: ", 1, int.MaxValue);

            if (profesores.Exists(p => p.Id == id))
            {
                Console.WriteLine(" [!] Error: Ya existe un profesor registrado con este ID.");
                Console.ReadKey();
                return;
            }

            string cedula = string.Empty;
            int edadCalculada = 0;
            bool cedulaValida = false;

            do
            {
                Console.Write("Ingrese Cédula nicaragüense (Ej: 5650402061001J): ");
                cedula = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                string patron = @"^\d{3}-?\d{6}-?\d{4}[A-Z]$";
                if (!Regex.IsMatch(cedula, patron))
                {
                    Console.WriteLine(" [!] Formato incorrecto. Intente nuevamente.");
                    continue;
                }

                string cedulaLimpiada = cedula.Replace("-", "");
                string subFecha = cedulaLimpiada.Substring(3, 6);
                int.TryParse(subFecha.Substring(0, 2), out int dia);
                int.TryParse(subFecha.Substring(2, 2), out int mes);
                int.TryParse(subFecha.Substring(4, 2), out int anioDosDigitos);

                int anioCompleto = (anioDosDigitos <= 26) ? 2000 + anioDosDigitos : 1900 + anioDosDigitos;

                try
                {
                    DateTime fechaNacimiento = new DateTime(anioCompleto, mes, dia);

                    // Validación estricta: Rechaza fechas del futuro (aún no ha nacido)
                    if (fechaNacimiento > DateTime.Now)
                    {
                        Console.WriteLine(" [!] Error: La cédula pertenece a una fecha futura (aún no ha nacido).");
                        cedulaValida = false;
                        continue;
                    }

                    int anioActual = 2026;
                    edadCalculada = anioActual - fechaNacimiento.Year;

                    if (DateTime.Now < fechaNacimiento.AddYears(edadCalculada))
                    {
                        edadCalculada--;
                    }

                    if (edadCalculada < 16)
                    {
                        Console.WriteLine($" [!] Restricción legal: La cédula indica {edadCalculada} años. Se requieren al menos 16 años para poseer cédula.");
                        cedulaValida = false;
                        continue;
                    }

                    cedulaValida = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine(" [!] Fecha inválida dentro de los dígitos de la cédula.");
                    cedulaValida = false;
                }

            } while (!cedulaValida);

            string nombre = Validador.LeerTextoNoVacio("Ingrese Nombre: ");
            string apellido = Validador.LeerTextoNoVacio("Ingrese Apellido: ");
            string materia = Validador.LeerTextoNoVacio("Ingrese Materia que imparte: ");

            var nuevo = new Profesor { Id = id, Cedula = cedula, Nombre = nombre, Apellido = apellido, Materia = materia, Activo = true };
            profesores.Add(nuevo);

            ArchivoDatos.GuardarTodo(estudiantes, profesores);
            ArchivoLogs.RegistrarAccion(usuario, "AGREGAR_PROFESOR", $"ID: {id} - {nombre} {apellido}");

            Console.WriteLine("\n [✓] Profesor registrado exitosamente.");
            Console.ReadKey();
        }
    }
}