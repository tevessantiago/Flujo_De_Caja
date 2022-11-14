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
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "INSERT INTO PROVEEDOR (PROVEEDOR_NOMBRE, PROVEEDOR_RUBRO, PROVEEDOR_FECHA_ALTA, PROVEEDOR_FECHA_BAJA)" +
                    "VALUES (@PROVEEDOR_NOMBRE, @PROVEEDOR_RUBRO, @PROVEEDOR_FECHA_ALTA, @PROVEEDOR_FECHA_BAJA);", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@PROVEEDOR_NOMBRE", proveedor.Nombre);
                    miComando.Parameters.AddWithValue("@PROVEEDOR_RUBRO", proveedor.Rubro);
                    miComando.Parameters.AddWithValue("@PROVEEDOR_FECHA_ALTA", proveedor.FechaAlta);
                    miComando.Parameters.AddWithValue("@PROVEEDOR_FECHA_BAJA", (proveedor.FechaBaja.HasValue) ? proveedor.FechaBaja : DBNull.Value);

                    miComando.ExecuteNonQuery();
                }
                miConexion.Close();
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
                                    ProveedorId = int.Parse(reader["PROVEEDOR_ID"].ToString()),
                                    Nombre = reader["PROVEEDOR_NOMBRE"].ToString(),
                                    Rubro = reader["PROVEEDOR_RUBRO"].ToString(),
                                    FechaAlta = (DateTime)reader["PROVEEDOR_FECHA_ALTA"],
                                    FechaBaja = (DateTime.TryParse(reader["PROVEEDOR_FECHA_BAJA"].ToString(), out DateTime fechaBaja)) ? fechaBaja : null,
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
