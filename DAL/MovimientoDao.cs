using Entidades;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class MovimientoDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public int CargarMovimiento(Movimiento movimiento)
        {
            try
            {
                using (var miConexion = new SqlConnection(connectionString))
                {
                    miConexion.Open();

                    using (var miComando = new SqlCommand(
                        "INSERT INTO MOVIMIENTO (PERSONA_ID, PROVEEDOR_ID, MOVIMIENTO_TIPO, IMPORTE, MOVIMIENTO_FECHA_CREACION, MOVIMIENTO_FECHA_ACT, MOVIMIENTO_COMENTARIO)" +
                        " VALUES (@PERSONA_ID, @PROVEEDOR_ID, @MOVIMIENTO_TIPO, @IMPORTE, @MOVIMIENTO_FECHA_CREACION, @MOVIMIENTO_FECHA_ACT, @MOVIMIENTO_COMENTARIO);" +
                        " SELECT CAST(scope_identity() AS int);", miConexion))
                    {
                        miComando.CommandType = System.Data.CommandType.Text;

                        miComando.Parameters.AddWithValue("@PERSONA_ID", movimiento.Persona_Id);
                        miComando.Parameters.AddWithValue("@PROVEEDOR_ID", movimiento.Proveedor_Id);
                        miComando.Parameters.AddWithValue("@MOVIMIENTO_TIPO", movimiento.Movimiento_Tipo);
                        miComando.Parameters.AddWithValue("@IMPORTE", movimiento.Importe);
                        miComando.Parameters.AddWithValue("@MOVIMIENTO_FECHA_CREACION", movimiento.Movimiento_Fecha_Creacion);
                        miComando.Parameters.AddWithValue("@MOVIMIENTO_FECHA_ACT", movimiento.Movimiento_Fecha_Act);
                        miComando.Parameters.AddWithValue("@MOVIMIENTO_COMENTARIO", movimiento.Movimiento_Comentario);

                        return (int)miComando.ExecuteScalar();
                    }
                    miConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error con la conexion a la BD: " + ex.Message);
            }
        }

        public List<Movimiento> ObtenerMovimientos()
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            using(var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using(var miComando = new SqlCommand(
                    "SELECT * FROM MOVIMIENTO;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    using(var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                movimientos.Add(new Movimiento
                                {
                                    Movimiento_Id = int.Parse(reader["MOVIMIENTO_ID"].ToString()),
                                    Persona_Id = int.Parse(reader["PERSONA_ID"].ToString()),
                                    Proveedor_Id = int.Parse(reader["PROVEEDOR_ID"].ToString()),
                                    Movimiento_Tipo = reader["MOVIMIENTO_TIPO"].ToString(),
                                    Importe = double.Parse(reader["IMPORTE"].ToString()),
                                    Movimiento_Fecha_Creacion = DateTime.Parse(reader["MOVIMIENTO_FECHA_CREACION"].ToString()),
                                    Movimiento_Fecha_Act = DateTime.Parse(reader["MOVIMIENTO_FECHA_ACT"].ToString()),
                                    Movimiento_Comentario = reader["MOVIMIENTO_COMENTARIO"].ToString()
                                });
                            }
                        }
                    }
                }
                miConexion.Close();
            }
            return movimientos;
        }
    }
}
