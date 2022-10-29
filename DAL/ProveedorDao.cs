using Entidades;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class ProveedorDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public void CargarProveedor(Proveedor proveedor)
        {
            try
            {
                using (var miConexion = new SqlConnection(connectionString))
                {
                    miConexion.Open();

                    using (var miComando = new SqlCommand(
                        "INSERT INTO PROVEEDOR (PROVEEDOR_NOMBRE, PROVEEDOR_RUBRO, PROVEEDOR_FECHA_ALTA, PROVEEDOR_FECHA_BAJA)" +
                        "VALUES (@PROVEEDOR_NOMBRE, @PROVEEDOR_RUBRO, @PROVEEDOR_FECHA_ALTA, @PROVEEDOR_FECHA_BAJA);", miConexion))
                    {
                        miComando.CommandType = System.Data.CommandType.Text;

                        miComando.Parameters.AddWithValue("@PROVEEDOR_NOMBRE", proveedor.Proveedor_Nombre);
                        miComando.Parameters.AddWithValue("@PROVEEDOR_RUBRO", proveedor.Proveedor_Rubro);
                        miComando.Parameters.AddWithValue("@PROVEEDOR_FECHA_ALTA", proveedor.Proveedor_Fecha_Alta);
                        miComando.Parameters.AddWithValue("@PROVEEDOR_FECHA_BAJA", (proveedor.Proveedor_Fecha_Baja.HasValue) ? proveedor.Proveedor_Fecha_Baja : DBNull.Value);                        

                        miComando.ExecuteNonQuery();
                    }
                    miConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error con la conexion a la BD: " + ex.Message);
            }
            finally
            {

            }
        }

        public List<Proveedor> ObtenerProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "SELECT * FROM PROVEEDOR;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    using (var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                proveedores.Add(new Proveedor
                                {
                                    Proveedor_Id = int.Parse(reader["PROVEEDOR_ID"].ToString()),
                                    Proveedor_Nombre = reader["PROVEEDOR_NOMBRE"].ToString(),
                                    Proveedor_Rubro = reader["PROVEEDOR_RUBRO"].ToString(),
                                    Proveedor_Fecha_Alta = (DateTime)reader["PROVEEDOR_FECHA_ALTA"],
                                    Proveedor_Fecha_Baja = (DateTime.TryParse(reader["PROVEEDOR_FECHA_BAJA"].ToString(), out DateTime fechaBaja)) ? fechaBaja : null,
                                });
                            }
                        }
                    }

                }
                miConexion.Close();
            }
            return proveedores;
        }
    }
}
