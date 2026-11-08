using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoColegio.Interfaces
{
    public interface IPersistible
    {
        // Método que obliga a cualquier clase que implemente la interfaz 
        // a definir cómo mostrar su propia información por consola.
        void MostrarInformacion();
    }
}