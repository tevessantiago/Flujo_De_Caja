using Entidades;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class PersonaDao
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionKiosko"].ConnectionString;
        public void CargarPersona(Persona persona)
        {
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "INSERT INTO PERSONA (PERSONA_NOMBRE, PERSONA_APELLIDO, PERSONA_TIPO, PERSONA_ESTADO)" +
                    "VALUES (@PERSONA_NOMBRE, @PERSONA_APELLIDO, @PERSONA_TIPO, @PERSONA_ESTADO);", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@PERSONA_NOMBRE", persona.Nombre);
                    miComando.Parameters.AddWithValue("@PERSONA_APELLIDO", persona.Apellido);
                    miComando.Parameters.AddWithValue("@PERSONA_TIPO", persona.Tipo);
                    miComando.Parameters.AddWithValue("@PERSONA_ESTADO", persona.Estado);

                    miComando.ExecuteNonQuery();
                }
                miConexion.Close();
            }
        }

        public List<Persona> ObtenerPersonas()
        {
            List<Persona> personas = new List<Persona>();

            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "SELECT * FROM PERSONA;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    using (var reader = miComando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                personas.Add(new Persona
                                {
                                    PersonaId = int.Parse(reader["PERSONA_ID"].ToString()),
                                    Nombre = reader["PERSONA_NOMBRE"].ToString(),
                                    Apellido = reader["PERSONA_APELLIDO"].ToString(),
                                    Tipo = reader["PERSONA_TIPO"].ToString(),
                                    Estado = reader["PERSONA_ESTADO"].ToString()
                                });
                            }
                        }
                    }
                }
                miConexion.Close();
            }
            return personas;
        }


    }
}