using Entidades;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class CierreDiarioDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public List<CierreDiario> ObtenerCierreDiario()
        {
            List<CierreDiario> cierreDiario = new List<CierreDiario>();

            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "SELECT MOVIMIENTO.MOVIMIENTO_FECHA_ACT AS FECHA, " +
                    "MOVIMIENTO.MOVIMIENTO_TIPO AS TIPO, " +
                    "MOVIMIENTO.IMPORTE AS IMPORTE, " +
                    "PERSONA.PERSONA_NOMBRE AS NOMBRE, " +
                    "PROVEEDOR.PROVEEDOR_NOMBRE AS PROVEEDOR, " +
                    "MOVIMIENTO.MOVIMIENTO_COMENTARIO AS COMENTARIO " +
                    "FROM MOVIMIENTO " +
                    "INNER JOIN PERSONA ON PERSONA.PERSONA_ID = MOVIMIENTO.PERSONA_ID " +
                    "INNER JOIN PROVEEDOR ON PROVEEDOR.PROVEEDOR_ID = MOVIMIENTO.PROVEEDOR_ID " +
                    "ORDER BY MOVIMIENTO_FECHA_ACT DESC;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    using (var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cierreDiario.Add(new CierreDiario
                                {
                                    Fecha = DateTime.Parse(reader["FECHA"].ToString()),
                                    Tipo = reader["TIPO"].ToString(),
                                    Importe = double.Parse(reader["IMPORTE"].ToString()),
                                    Nombre = reader["NOMBRE"].ToString(),
                                    Proveedor = reader["PROVEEDOR"].ToString(),
                                    Comentario = reader["COMENTARIO"].ToString()
                                });
                            }
                        }
                    }
                }
                miConexion.Close();
            }
            return cierreDiario;
        }
    }
}
