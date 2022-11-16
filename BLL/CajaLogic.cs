using DAL;
using Entidades;
using System.Transactions;

namespace BLL
{
    public class CajaLogic
    {
        CajaDao dao = new CajaDao();

        public void ActualizarCaja(double monto)
        {
            using(var trx = new TransactionScope())
            {
                dao.ActualizarCaja(monto);

                trx.Complete();
            }            
        }

        public List<Caja> ObtenerCaja()
        {
            return dao.ObtenerCaja();
        }
    }
}