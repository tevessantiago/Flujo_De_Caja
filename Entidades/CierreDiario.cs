using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CierreDiario
    {
        public DateTime Fecha { get; set; }        
        public string Tipo { get; set; }
        public double Importe { get; set; }
        public double Total { get; set; }
        public string Nombre { get; set; }
    }
}
