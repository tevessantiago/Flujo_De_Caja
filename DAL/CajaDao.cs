using Entidades;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class CajaDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public void CargarCaja(Caja caja)
        {
            try
            {
                using (var miConexion = new SqlConnection(connectionString))
                {
                    miConexion.Open();

                    using (var miComando = new SqlCommand(
                        "INSERT INTO CAJA (MOVIMIENTO_ID, CAJA_TOTAL)" +
                        " VALUES (@MOVIMIENTO_ID, @CAJA_TOTAL);", miConexion))
                    {
                        miComando.CommandType = System.Data.CommandType.Text;

                        miComando.Parameters.AddWithValue("@MOVIMIENTO_ID", caja.Movimiento_Id);
                        miComando.Parameters.AddWithValue("@CAJA_TOTAL", caja.Caja_Total);

                        miComando.ExecuteNonQuery();
                    }
                    miConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error con la conexion a la BD: " + ex.Message);
            }
        }

    }
}