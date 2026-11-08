using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;


namespace ProyectoColegio.Utilidades
{
    public static class Validador
    {
        public static string LeerTextoNoVacio(string mensaje)
        {
            string entrada;
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine(" [!] Error: El campo no puede estar vacío ni contener solo espacios.");
                }
            } while (string.IsNullOrWhiteSpace(entrada));
            return entrada.Trim();
        }

        public static int LeerEntero(string mensaje, int min, int max)
        {
            int numero;
            string entrada;
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine() ?? string.Empty;
                if (!int.TryParse(entrada, out numero) || numero < min || numero > max)
                {
                    Console.WriteLine($" [!] Error: Ingrese un número entero válido entre {min} y {max}.");
                }
            } while (!int.TryParse(entrada, out numero) || numero < min || numero > max);
            return numero;
        }

        public static string LeerCedulaNicaraguense(string mensaje)
        {
            string cedula;
            bool esValida = false;

            do
            {
                Console.Write(mensaje);
                cedula = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                string patron = @"^\d{3}-?\d{6}-?\d{4}[A-Z]$";

                if (!Regex.IsMatch(cedula, patron))
                {
                    Console.WriteLine(" [!] Formato incorrecto. Ejemplo válido: 5650402061001J");
                    continue;
                }

                string cedulaLimpiada = cedula.Replace("-", "");
                string subFecha = cedulaLimpiada.Substring(3, 6);
                string diaStr = subFecha.Substring(0, 2);
                string mesStr = subFecha.Substring(2, 2);
                string anioStr = subFecha.Substring(4, 2);

                if (int.TryParse(diaStr, out int dia) &&
                    int.TryParse(mesStr, out int mes) &&
                    int.TryParse(anioStr, out int anioDosDigitos))
                {
                    int anioCompleto = (anioDosDigitos <= 26) ? 2000 + anioDosDigitos : 1900 + anioDosDigitos;

                    try
                    {
                        DateTime fechaNacimiento = new DateTime(anioCompleto, mes, dia);
                        int anioActual = 2026;
                        int edadCalculada = anioActual - fechaNacimiento.Year;

                        if (DateTime.Now < fechaNacimiento.AddYears(edadCalculada))
                        {
                            edadCalculada--;
                        }

                        if (edadCalculada < 16)
                        {
                            Console.WriteLine($" [!] Error de negocio: La fecha indica {edadCalculada} años. Se requieren al menos 16 años para poseer cédula.");
                            continue;
                        }

                        esValida = true;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine(" [!] Fecha inválida dentro de la cédula.");
                    }
                }
                else
                {
                    Console.WriteLine(" [!] No se pudo interpretar la fecha de la cédula.");
                }

            } while (!esValida);

            return cedula;
        }

        public static bool PedirConfirmacion(string mensaje)
        {
            string respuesta;
            do
            {
                Console.Write($"{mensaje} (S/N): ");
                respuesta = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;
                if (respuesta != "S" && respuesta != "N")
                {
                    Console.WriteLine(" [!] Responda únicamente 'S' para Sí o 'N' para No.");
                }
            } while (respuesta != "S" && respuesta != "N");
            return respuesta == "S";
        }
    }
}