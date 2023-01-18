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
        //CINE ZONA

        public int? Total { get; set; }
        public int? Norte { get; set; }
        public int? Sur { get; set; }
        public int? Este { get; set; }
        public int? Oeste { get; set; }

        public ML.Zona Zona { get; set; }
        public List<object> CineList { get; set; }
    }
}
