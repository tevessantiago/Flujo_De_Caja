namespace Entidades
{
    public class Movimiento
    {
        public int Movimiento_Id { get; set; }
        public int Persona_Id { get; set; }
        public int Proveedor_Id { get; set; }
        public string Movimiento_Tipo { get; set; }
        public double Importe { get; set; }
        public DateTime Movimiento_Fecha_Creacion { get; set; }
        public DateTime Movimiento_Fecha_Act { get; set; }
        public string Movimiento_Comentario { get; set; }
    }
}
