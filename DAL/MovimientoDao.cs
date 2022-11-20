using Entidades;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MovimientoDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public int CargarMovimiento(Movimiento movimiento)
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

                    miComando.Parameters.AddWithValue("@PERSONA_ID", movimiento.PersonaId);
                    miComando.Parameters.AddWithValue("@PROVEEDOR_ID", movimiento.ProveedorId);
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_TIPO", movimiento.Tipo);
                    miComando.Parameters.AddWithValue("@IMPORTE", movimiento.Importe);
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_FECHA_CREACION", movimiento.FechaCreacion);
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_FECHA_ACT", movimiento.FechaActualizacion);
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_COMENTARIO", movimiento.Comentario);

                    return (int)miComando.ExecuteScalar();
                }
                miConexion.Close(); //Está quedando la conexión abierta?
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
                                    MovimientoId = int.Parse(reader["MOVIMIENTO_ID"].ToString()),
                                    PersonaId = int.Parse(reader["PERSONA_ID"].ToString()),
                                    ProveedorId = int.Parse(reader["PROVEEDOR_ID"].ToString()),
                                    Tipo = reader["MOVIMIENTO_TIPO"].ToString(),
                                    Importe = double.Parse(reader["IMPORTE"].ToString()),
                                    FechaCreacion = DateTime.Parse(reader["MOVIMIENTO_FECHA_CREACION"].ToString()),
                                    FechaActualizacion = DateTime.Parse(reader["MOVIMIENTO_FECHA_ACT"].ToString()),
                                    Comentario = reader["MOVIMIENTO_COMENTARIO"].ToString()
                                });
                            }
                        }
                    }
                }
                miConexion.Close();
            }
            return movimientos;
        }

        public void BorrarMovimiento(int movId)
        {
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                        "DELETE FROM MOVIMIENTO WHERE MOVIMIENTO_ID=@MOVIMIENTO_ID", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@MOVIMIENTO_ID", movId);

                    miComando.ExecuteNonQuery();
                }
                miConexion.Close();
            }
        }

        public void ModificarMovimiento(Movimiento movimiento)
        {
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "UPDATE MOVIMIENTO SET " +
                    "PERSONA_ID=@PERSONA_ID, " +
                    "PROVEEDOR_ID=@PROVEEDOR_ID, " +
                    "MOVIMIENTO_TIPO=@MOVIMIENTO_TIPO, " +
                    //"IMPORTE=@IMPORTE, " +
                    "MOVIMIENTO_FECHA_ACT=@MOVIMIENTO_FECHA_ACT, " +
                    "MOVIMIENTO_COMENTARIO=@MOVIMIENTO_COMENTARIO " +
                    "WHERE MOVIMIENTO_ID=@MOVIMIENTO_ID;"
                        , miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@PERSONA_ID", movimiento.PersonaId);
                    miComando.Parameters.AddWithValue("@PROVEEDOR_ID", movimiento.ProveedorId);
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_TIPO", movimiento.Tipo);
                    //miComando.Parameters.AddWithValue("@IMPORTE", movimiento.Importe); //Aclarar que hacemos con esto.
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_FECHA_ACT", movimiento.FechaActualizacion);
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_COMENTARIO", movimiento.Comentario);
                    miComando.Parameters.AddWithValue("@MOVIMIENTO_ID", movimiento.MovimientoId);

                    miComando.ExecuteNonQuery();
                }
                miConexion.Close();
            }                
        }

        public double CalcularCaja()
        {
            double caja = 0;

            using(var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using(var miComando = new SqlCommand(
                    "SELECT COALESCE(SUM(IMPORTE),0) AS CAJA FROM MOVIMIENTO;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    using(var adapter = new SqlDataAdapter(miComando))
                    {
                        var dataSet = new DataSet();

                        adapter.Fill(dataSet);

                        caja = double.Parse(dataSet.Tables[0].Rows[0]["CAJA"].ToString());//Ver de meter alguna medida de seguridad.
                    }
                }
                miConexion.Close();
            }
            return caja;
        }

    }
}
