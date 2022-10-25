using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    "SELECT MOVIMIENTO.MOVIMIENTO_FECHA_ACT AS FECHA, PROVEEDOR.PROVEEDOR_RUBRO AS RUBRO, MOVIMIENTO.MOVIMIENTO_TIPO AS TIPO, MOVIMIENTO.IMPORTE AS IMPORTE, CAJA.CAJA_TOTAL AS TOTAL, PERSONA.PERSONA_NOMBRE AS NOMBRE " +
                    "FROM MOVIMIENTO " +
                    "INNER JOIN PROVEEDOR ON PROVEEDOR.PROVEEDOR_ID = MOVIMIENTO.PROVEEDOR_ID " +
                    "INNER JOIN CAJA ON CAJA.MOVIMIENTO_ID = MOVIMIENTO.MOVIMIENTO_ID " +
                    "INNER JOIN PERSONA ON PERSONA.PERSONA_ID = MOVIMIENTO.PERSONA_ID " +
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
                                    Rubro = reader["RUBRO"].ToString(),
                                    Tipo = reader["TIPO"].ToString(),
                                    Importe = double.Parse(reader["IMPORTE"].ToString()),
                                    Total= double.Parse(reader["TOTAL"].ToString()),
                                    Nombre = reader["NOMBRE"].ToString()                                    
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
