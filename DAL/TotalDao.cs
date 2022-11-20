using Entidades;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class TotalDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public double ObtenerTotal(Total total)
        {
            double totalEntreFechas = 0;

            using(var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand("SELECT COALESCE(SUM(IMPORTE),0) AS TOTAL FROM MOVIMIENTO WHERE MOVIMIENTO_FECHA_CREACION BETWEEN @DESDE AND @HASTA;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@DESDE", total.FechaDesde);
                    miComando.Parameters.AddWithValue("@HASTA", total.FechaHasta);

                    using(var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                totalEntreFechas = double.Parse(reader["TOTAL"].ToString());
                            }
                        }
                    }
                }
                miConexion.Close();
            }
            return totalEntreFechas;
        }

    }
}
