using DAL;
using Entidades;

namespace BLL
{
    public class MovimientoLogic
    {
        MovimientoDao dao = new MovimientoDao();
        CajaLogic cajaLogic = new CajaLogic();
        public void CargarMovimiento(Movimiento movimiento)
        {
            Caja caja = new Caja();

            int movimientoID = dao.CargarMovimiento(movimiento);
            caja.MovimientoId = movimientoID;
            caja.Total = movimiento.Importe;
            cajaLogic.CargarCaja(caja);
        }

        public List<Movimiento> ObtenerMovimientos()
        {
            return dao.ObtenerMovimientos();
        }
    }
}
