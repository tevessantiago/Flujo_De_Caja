using DAL;
using Entidades;

namespace BLL
{
    public class TotalLogic
    {
        TotalDao totalDao = new TotalDao();        

        public double ObtenerTotal(Total total)
        {
            return totalDao.ObtenerTotal(total);
        }
    }
}
