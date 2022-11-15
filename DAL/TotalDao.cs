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
    public class TotalDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;

        public DataTable ObtenerTotal()
        {
            DataTable tablaTotal = new DataTable();
            using (var miConexion = new SqlConnection(connectionString)) 
            {
                miConexion.Open();
                using (var miComando = new SqlCommand("SELECT MOVIMIENTO_FECHA_CREACION,IMPORTE FROM MOVIMIENTO", miConexion))

                {
                    miComando.CommandType = System.Data.CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(miComando);
                    adapter.SelectCommand = miComando;
                    adapter.Fill(tablaTotal);



                };
                miConexion.Close();
            };
            return tablaTotal;
        }

    }
}
