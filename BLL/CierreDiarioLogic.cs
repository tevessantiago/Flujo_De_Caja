using DAL;
using Entidades;

namespace BLL
{
    public class CierreDiarioLogic
    {
        CierreDiarioDao dao = new CierreDiarioDao();

        public List<CierreDiario> ObtenerCierreDiario()
        {
            return dao.ObtenerCierreDiario();
        }
    }
}
