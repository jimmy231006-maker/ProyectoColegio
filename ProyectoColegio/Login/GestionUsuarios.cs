using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoColegio.Login
{
    public static class GestionUsuarios
    {
        public static bool ValidarNombreUsuario(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
            {
                Console.WriteLine(" [!] El nombre de usuario debe tener al menos 3 caracteres.");
                return false;
            }
            return true;
        }
    }
}