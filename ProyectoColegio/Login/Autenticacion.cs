using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoColegio.Modelos;
using ProyectoColegio.Archivos;
using System.Security.Cryptography;

namespace ProyectoColegio.Login
{
    public static class Autenticacion
    {
        // Contraseña en texto plano requerida por la rúbrica: "V122050*/"
        // Generamos su hash SHA256 correspondiente para validación segura
        private static readonly string HashPasswordVictor = HashearPassword("V122050*/");

        public static Usuario Login()
        {
            int intentos = 3;

            while (intentos > 0)
            {
                Console.Clear();
                Console.WriteLine("=============================================");
                Console.WriteLine("    SISTEMA DE GESTIÓN - PROYECTO COLEGIO    ");
                Console.WriteLine("=============================================");
                Console.Write(" Usuario: ");
                string usuario = Console.ReadLine() ?? string.Empty;

                Console.Write(" Contraseña: ");
                string passwordIngresada = LeerPasswordOculto();
                string hashIngresado = HashearPassword(passwordIngresada);

                // Comparamos el hash ingresado con el hash almacenado por seguridad
                if (usuario == "Victor" && hashIngresado == HashPasswordVictor)
                {
                    ArchivoLogs.RegistrarAccion("Victor", "LOGIN", "Inicio de sesión exitoso (con Hash SHA256).");
                    Console.WriteLine("\n [✓] ¡Acceso concedido! Presione una tecla para continuar...");
                    Console.ReadKey();
                    return new Usuario { Username = "Victor", EsAdmin = true };
                }

                intentos--;
                ArchivoLogs.RegistrarAccion(string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario, "LOGIN_FALLIDO", $"Intentos restantes: {intentos}");
                Console.WriteLine($"\n [!] Credenciales incorrectas. Intentos restantes: {intentos}");
                Console.ReadKey();
            }

            Console.WriteLine("\n [X] Ha superado el número de intentos permitidos. Cerrando sistema.");
            return null;
        }

        // Método criptográfico para convertir cualquier texto en Hash SHA256
        private static string HashearPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private static string LeerPasswordOculto()
        {
            string pass = "";
            ConsoleKeyInfo tecla;
            do
            {
                tecla = Console.ReadKey(true);
                if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
                {
                    pass += tecla.KeyChar;
                    Console.Write("*");
                }
                else if (tecla.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Substring(0, pass.Length - 1);
                    Console.Write("\b \b");
                }
            } while (tecla.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }
    }
}