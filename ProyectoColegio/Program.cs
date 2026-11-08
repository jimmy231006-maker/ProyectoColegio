using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoColegio.Modelos;
using ProyectoColegio.Archivos;
using ProyectoColegio.Login;
using ProyectoColegio.Estudiantes;
using ProyectoColegio.Profesores;
using ProyectoColegio.Utilidades;

namespace ProyectoColegio
{
    class Program
    {
        static void Main(string[] args)
        {
            // Autenticación por Hash SHA256
            Usuario usuarioSesion = Autenticacion.Login();
            if (usuarioSesion == null) return;

            // Carga inicial de datos desde el archivo binario
            ArchivoDatos.CargarTodo(out List<Estudiante> estudiantes, out List<Profesor> profesores);

            // Determinamos el texto del rango de forma limpia para la interfaz
            string rangoUsuario = usuarioSesion.EsAdmin ? "Administrador" : "Usuario Estándar";

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine($"    PROYECTO COLEGIO - MENÚ PRINCIPAL           ");
                Console.WriteLine($"    Usuario: {usuarioSesion.Username} | Rango: {rangoUsuario}");
                Console.WriteLine("=================================================");
                Console.WriteLine(" 1. Registrar Estudiante");
                Console.WriteLine(" 2. Listar Estudiantes");
                Console.WriteLine(" 3. Desactivar Estudiante");
                Console.WriteLine(" 4. Reactivar Estudiante");
                Console.WriteLine(" -----------------------------------------------");
                Console.WriteLine(" 5. Registrar Profesor");
                Console.WriteLine(" 6. Listar Profesores");
                Console.WriteLine(" 7. Desactivar Profesor");
                Console.WriteLine(" 8. Reactivar Profesor");
                Console.WriteLine(" -----------------------------------------------");
                Console.WriteLine(" 9. Exportar Todos los Datos");
                Console.WriteLine("10. Salir del Sistema");
                Console.WriteLine("=================================================");

                int opcion = Validador.LeerEntero(" Seleccione una opción (1-10): ", 1, 10);

                switch (opcion)
                {
                    case 1:
                        AgregarEstudiante.Ejecutar(estudiantes, profesores, usuarioSesion.Username);
                        break;
                    case 2:
                        ListarEstudiante.Ejecutar(estudiantes);
                        break;
                    case 3:
                        DesactivarEstudiante.Ejecutar(estudiantes, profesores, usuarioSesion.Username);
                        break;
                    case 4:
                        ReactivatorEstudiante.Ejecutar(estudiantes, profesores, usuarioSesion.Username);
                        break;
                    case 5:
                        AgregarProfesor.Ejecutar(estudiantes, profesores, usuarioSesion.Username);
                        break;
                    case 6:
                        ListarProfesor.Ejecutar(profesores);
                        break;
                    case 7:
                        DesactivarProfesor.Ejecutar(estudiantes, profesores, usuarioSesion.Username);
                        break;
                    case 8:
                        ReactivarProfesor.Ejecutar(estudiantes, profesores, usuarioSesion.Username);
                        break;
                    case 9:
                        Exportador.ExportarReporte(estudiantes, profesores, usuarioSesion.Username);
                        Console.ReadKey();
                        break;
                    case 10:
                        salir = true;
                        ArchivoLogs.RegistrarAccion(usuarioSesion.Username, "LOGOUT", "Cierre de sesión de usuario.");
                        Console.WriteLine("\n Gracias por usar el Sistema del Proyecto Colegio. ¡Hasta luego!");
                        break;
                }
            }
        }
    }
}