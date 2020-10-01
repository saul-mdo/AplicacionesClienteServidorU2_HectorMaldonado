using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio5_ClientePeliculas
{
    public enum Sala { A1 =0, A2=1, B1=2, B2=3};
    public enum Clasificacion { A=0, B15=1, C=2, R=3};
    public enum Idioma { Español=0, Subtitulada=1};
    public class DatosPelicula
    {
        public string Hora { get; set; }
        public string Nombre { get; set; }
        public Sala Sala { get; set; }
        public Clasificacion Clasificacion { get; set; }
        public Idioma Idioma { get; set; }
    }
}
