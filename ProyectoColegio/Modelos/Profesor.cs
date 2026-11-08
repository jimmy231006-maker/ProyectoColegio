using ProyectoColegio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoColegio.Modelos
{
    public class Profesor : Persona, IPersistible
    {
        public string Materia { get; set; } = string.Empty;

        public override string ObtenerDetalleEspecifico()
        {
            return $"Materia: {Materia}";
        }

        public void MostrarInformacion()
        {
            string estado = Activo ? "ACTIVO" : "INACTIVO";
            Console.WriteLine($"[Profesor]   ID: {Id} | Cédula: {Cedula} | {Nombre} {Apellido} | {ObtenerDetalleEspecifico()} | Estado: {estado}");
        }
    }
}