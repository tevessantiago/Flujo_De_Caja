using DAL;
using Entidades;

namespace BLL
{
    public class CajaLogic
    {
        CajaDao dao = new CajaDao();

        public void CargarCaja(Caja caja)
        {
            dao.CargarCaja(caja);
        }
    }
}