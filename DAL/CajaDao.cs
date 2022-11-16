using Entidades;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class CajaDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public List<Caja> ObtenerCaja()
        {
            List<Caja> caja = new List<Caja>();

            using(var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "SELECT * FROM CAJA;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    using(var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                caja.Add(new Caja
                                {
                                    CajaId = int.Parse(reader["CAJA_ID"].ToString()),
                                    Total = double.Parse(reader["CAJA_TOTAL"].ToString())
                                });
                            }
                        }
                    }
                }
                miConexion.Close();
            }
            return caja;
        }

        public void ActualizarCaja(double monto)
        {
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "UPDATE CAJA SET CAJA_TOTAL=@Monto WHERE CAJA_ID=1;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;
                    
                    miComando.Parameters.AddWithValue("@Monto", monto);

                    miComando.ExecuteNonQuery();
                }
                miConexion.Close();
            }
        }

    }
}