using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Venta { get; set; }
        public ML.Zona Zona { get; set; }
        public List<object> CineList { get; set; }
    }
}
