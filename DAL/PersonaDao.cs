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
            try
            {
                using (var miConexion = new SqlConnection(connectionString))
                {
                    miConexion.Open();

                    using (var miComando = new SqlCommand(
                        "INSERT INTO PERSONA (PERSONA_NOMBRE, PERSONA_APELLIDO, PERSONA_TIPO, PERSONA_ESTADO)" +
                        "VALUES (@PERSONA_NOMBRE, @PERSONA_APELLIDO, @PERSONA_TIPO, @PERSONA_ESTADO);", miConexion))
                    {
                        miComando.CommandType = System.Data.CommandType.Text;

                        miComando.Parameters.AddWithValue("@PERSONA_NOMBRE", persona.Persona_Nombre);
                        miComando.Parameters.AddWithValue("@PERSONA_APELLIDO", persona.Persona_Apellido);
                        miComando.Parameters.AddWithValue("@PERONA_TIPO", persona.Persona_Tipo);
                        miComando.Parameters.AddWithValue("@PERSONA_ESTADO", persona.Persona_Estado);

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
                                    Persona_Id = int.Parse(reader["PERSONA_ID"].ToString()),
                                    Persona_Nombre = reader["PERSONA_NOMBRE"].ToString(),
                                    Persona_Apellido = reader["PERSONA_APELLIDO"].ToString(),
                                    Persona_Tipo = reader["PERSONA_TIPO"].ToString(),
                                    Persona_Estado = reader["PERSONA_ESTADO"].ToString()
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