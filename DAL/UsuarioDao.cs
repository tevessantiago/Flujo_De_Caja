using Entidades;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UsuarioDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;
        public DataTable VerificarUsuario()
        {
            DataTable tabla = new DataTable();
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();
                
                using (var miComando = new SqlCommand(
                    "SELECT * FROM USUARIO", miConexion))
                {
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(miComando);
                    adapter.SelectCommand = miComando;
                    adapter.Fill(tabla);
                    
                }
                miConexion.Close();
            }
            return tabla;
        }

        public int ObtenerUsuarioId(string user, string pass)
        {
            int usuarioId = 0;

            using(var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using(var miComando = new SqlCommand(
                    "SELECT USUARIO_ID FROM USUARIO WHERE USUARIO_USER=@USUARIO_USER AND USUARIO_PASS=@USUARIO_PASS;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@USUARIO_USER", user);
                    miComando.Parameters.AddWithValue("@USUARIO_PASS", pass);

                    using(var adapter = new SqlDataAdapter(miComando))
                    {
                        var dataSet = new DataSet();

                        adapter.Fill(dataSet);

                        usuarioId = int.Parse(dataSet.Tables[0].Rows[0]["USUARIO_ID"].ToString());//Ver de meter alguna medida de seguridad.
                    }                    
                }
                miConexion.Close();
            }
            return usuarioId;
        }
        public string ObtenerAdmin(string user, string pass)
        {
            string admin = "";
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "SELECT USUARIO_ADMIN FROM USUARIO WHERE USUARIO_USER=@USUARIO_USER AND USUARIO_PASS=@USUARIO_PASS;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@USUARIO_USER", user);
                    miComando.Parameters.AddWithValue("@USUARIO_PASS", pass);

                    using (var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                admin = reader["USUARIO_ADMIN"].ToString();
                            }
                        }

                    }
                }
                miConexion.Close();
            }
            return admin;
        }

        public int CrearUsuario(Usuario usuario)
        {
            int usuarioId = 0;
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();
                
                using (var miComando = new SqlCommand("INSERT INTO USUARIO (USUARIO_USER, USUARIO_PASS, USUARIO_ADMIN)"
                    + " VALUES (@USUARIO_USER, @USUARIO_PASS, @USUARIO_ADMIN); "
                    + "SELECT CAST(scope_identity() AS int);", miConexion))
                {
                    miComando.CommandType=System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@USUARIO_USER", usuario.User);
                    miComando.Parameters.AddWithValue("@USUARIO_PASS", usuario.Pass);
                    miComando.Parameters.AddWithValue("@USUARIO_ADMIN", usuario.Admin);

                    usuarioId = (int)miComando.ExecuteScalar();
                }
                miConexion.Close();
            }
            return usuarioId;
        }

        
    }
}
