using ProyectoColegio.Interfaces;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoColegio.Modelos
{
    public class Estudiante : Persona, IPersistible
    {
        public int Edad { get; set; }

        public override string ObtenerDetalleEspecifico()
        {
            return $"Edad: {Edad} años";
        }

        public void MostrarInformacion()
        {
            string estado = Activo ? "ACTIVO" : "INACTIVO";
            Console.WriteLine($"[Estudiante] ID: {Id} | Cédula: {Cedula} | {Nombre} {Apellido} | {ObtenerDetalleEspecifico()} | Estado: {estado}");
        }
    }
}