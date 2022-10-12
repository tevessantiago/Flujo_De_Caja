using DAL;
using Entidades;

namespace BLL
{
    public class MovimientoLogic
    {
        MovimientoDao dao = new MovimientoDao();
        public void CargarMovimiento(Movimiento movimiento)
        {
            dao.CargarMovimiento(movimiento);
        }

        public List<Movimiento> ObtenerMovimientos()
        {
            return dao.ObtenerMovimientos();
        }
    }
}
