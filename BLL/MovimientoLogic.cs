using DAL;
using Entidades;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace BLL
{
    public class MovimientoLogic
    {
        MovimientoDao dao = new MovimientoDao();
        CajaLogic cajaLogic = new CajaLogic();
        public void CargarMovimiento(Movimiento movimiento, string proveedor)
        {
            List<Caja> caja = new List<Caja>();

            caja = cajaLogic.ObtenerCaja();

            if (movimiento.Tipo.Equals("Pago a Proveedor") && proveedor.Equals("N/A"))
            {
                throw new Exception("Por favor, seleccione un proveedor.");
            }            
            if (movimiento.Importe < 0)
            {
                throw new Exception("Por favor, ingrese solo valores positivos en el importe.");
            }            
            if (movimiento.Tipo.Equals("Retiro") || movimiento.Tipo.Equals("Pago a Proveedor"))
            {
                movimiento.Importe *= -1; //Se convierte el importe a negativo.
            }
            
            double total = caja[0].Total + movimiento.Importe;

            using(var trx = new TransactionScope())
            {
                int movimientoID = dao.CargarMovimiento(movimiento);

                cajaLogic.ActualizarCaja(total);

                trx.Complete();
            }            
        }

        public List<Movimiento> ObtenerMovimientos()
        {
            return dao.ObtenerMovimientos();
        }
    }
}
