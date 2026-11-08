using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoColegio.Modelos
{
    public class Usuario
    {
        public string Username { get; set; } = string.Empty;
        public bool EsAdmin { get; set; } = true;
    }
}