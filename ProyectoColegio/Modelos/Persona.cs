using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoColegio.Modelos
{
    public abstract class Persona
    {
        public int Id { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

        public abstract string ObtenerDetalleEspecifico();
    }
}