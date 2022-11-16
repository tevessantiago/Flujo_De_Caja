namespace Entidades
{
    public class Movimiento
    {
        public int MovimientoId { get; set; }
        public int PersonaId { get; set; }
        public int ProveedorId { get; set; }
        public string Tipo { get; set; }
        public double Importe { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get { return DateTime.Now.Date; } set {; } }
        public string Comentario { get; set; }
    }
}
