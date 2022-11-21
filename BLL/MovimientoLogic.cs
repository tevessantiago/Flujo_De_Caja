using DAL;
using Entidades;
using System.Transactions;

namespace BLL
{
    public class MovimientoLogic
    {
        MovimientoDao dao = new MovimientoDao();
        PersonaLogic personaLogic = new PersonaLogic();
        public void CargarMovimiento(Movimiento movimiento, string proveedor, int usuarioId)
        {
            if (!int.TryParse(personaLogic.ObtenerPersonaId(usuarioId).ToString(), out int personaId))//Vulnerable a 2 personas con mismo userId. O Persona sin userId.
            {
                throw new Exception("Error: No se pudo obtener personaId.");
            }
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

            movimiento.PersonaId = personaId;

            using(var trx = new TransactionScope())
            {
                int movimientoID = dao.CargarMovimiento(movimiento);

                trx.Complete();
            }            
        }

        public List<Movimiento> ObtenerMovimientos()
        {
            return dao.ObtenerMovimientos();
        }

        public void BorrarMovimiento(int movId)
        {
            using(var trx = new TransactionScope())
            {
                dao.BorrarMovimiento(movId);

                trx.Complete();
            }
        }

        public void ModificarMovimiento(Movimiento movimiento, string proveedor, int usuarioId)
        {
            if (!int.TryParse(personaLogic.ObtenerPersonaId(usuarioId).ToString(), out int personaId))//Vulnerable a 2 personas con mismo userId. O Persona sin userId.
            {
                throw new Exception("Error: No se pudo obtener personaId.");
            }
            if (movimiento.Tipo.Equals("Pago a Proveedor") && proveedor.Equals("N/A"))
            {
                throw new Exception("Por favor, seleccione un proveedor.");
            }
            if (string.IsNullOrEmpty(movimiento.Comentario))
            {
                throw new Exception("Por favor, comente el motivo de la modificación.");
            }

            movimiento.PersonaId = personaId;

            using (var trx = new TransactionScope())
            {
                dao.ModificarMovimiento(movimiento);

                trx.Complete();
            }
        }

        public double CalcularCaja()
        {
            return dao.CalcularCaja();
        }

        public double TotalEntreFechas(DateTime desde, DateTime hasta)
        {
            return dao.TotalEntreFechas(desde, hasta);
        }

    }
}
