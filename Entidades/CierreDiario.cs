namespace Entidades
{
    public class CierreDiario
    {
        public DateTime Fecha { get; set; }        
        public string Tipo { get; set; }
        public double Importe { get; set; }
        public string Nombre { get; set; }

        public string Proveedor { get; set; }
        public string Comentario { get; set; }
    }
}
