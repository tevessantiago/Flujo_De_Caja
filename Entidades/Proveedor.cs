namespace Entidades
{
    public class Proveedor
    {
        public int Proveedor_Id { get; set; }
        public string Proveedor_Nombre { get; set; }
        public string Proveedor_Rubro { get; set; }
        public DateTime Proveedor_Fecha_Alta { get; set; }
        public DateTime? Proveedor_Fecha_Baja { get; set; }
    }
}
