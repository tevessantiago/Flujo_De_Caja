using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    /*using(var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                usuarioId = int.Parse(reader["USUARIO_ID"].ToString()); //Se queda con el último, tengo que buscar una forma más óptima.
                            }
                        }
                    }*/
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
    }
}
