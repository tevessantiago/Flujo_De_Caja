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
    public class LoginDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;
        public DataTable VerificarUsuario()
        {
            DataTable tabla = new DataTable();
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();
                
                using (var miComando = new SqlCommand(
                    "SELECT * FROM LOGIN", miConexion))
                {
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(miComando);
                    adapter.SelectCommand = miComando;
                    adapter.Fill(tabla);
                    
                }
                miConexion.Close();
            }
            return tabla;
        }
    }
}
