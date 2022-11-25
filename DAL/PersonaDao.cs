using Entidades;
using System.Configuration;
using System.Data;
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
                                    UsuarioId = (int.TryParse(reader["USUARIO_ID"].ToString(), out int usuarioid)) ? usuarioid : 0,
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

        public int ObtenerPersonaId(int usuarioId)
        {
            int personaId = 0;

            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand(
                    "SELECT PERSONA_ID FROM PERSONA WHERE USUARIO_ID=@USUARIO_ID;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@USUARIO_ID", usuarioId);

                    using (var adapter = new SqlDataAdapter(miComando))
                    {
                        var dataSet = new DataSet();

                        adapter.Fill(dataSet);

                        personaId = int.Parse(dataSet.Tables[0].Rows[0]["PERSONA_ID"].ToString());//Ver de meter alguna medida de seguridad.
                    }                
                }
                miConexion.Close();
            }
            return personaId;
        }
        public void BajaPersona(Persona persona)
        {
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand("UPDATE PERSONA SET "
                    + "PERSONA_ESTADO=@PERSONA_ESTADO "
                    + "WHERE PERSONA_ID=@PERSONA_ID;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;
                    miComando.Parameters.AddWithValue("@PERSONA_ID", persona.PersonaId);
                    miComando.Parameters.AddWithValue("@PERSONA_ESTADO", persona.Estado);

                    miComando.ExecuteNonQuery();

                }
                miConexion.Close();
            }
        }
        public void ModificarPersona(Persona persona)
        {
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand("UPDATE PERSONA SET "
                    + "PERSONA_NOMBRE=@PERSONA_NOMBRE, "
                    + "PERSONA_APELLIDO=@PERSONA_APELLIDO, "
                    + "PERSONA_TIPO=@PERSONA_TIPO "
                    + "WHERE PERSONA_ID=@PERSONA_ID;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;
                    miComando.Parameters.AddWithValue("@PERSONA_ID", persona.PersonaId);
                    miComando.Parameters.AddWithValue("@PERSONA_NOMBRE", persona.Nombre);
                    miComando.Parameters.AddWithValue("@PERSONA_APELLIDO", persona.Apellido);
                    miComando.Parameters.AddWithValue("@PERSONA_TIPO", persona.Tipo);

                    miComando.ExecuteNonQuery();
                }
                miConexion.Close();
            }
        }

        public void InsertarUsuarioId(int usuarioId, int personaId)
        {
            using (var miConexion = new SqlConnection(connectionString))
            {
                miConexion.Open();

                using (var miComando = new SqlCommand("UPDATE PERSONA SET "
                    + "USUARIO_ID=@USUARIO_ID "
                    + "WHERE PERSONA_ID=@PERSONA_ID ;", miConexion))
                {
                    miComando.CommandType = System.Data.CommandType.Text;

                    miComando.Parameters.AddWithValue("@USUARIO_ID", usuarioId);
                    miComando.Parameters.AddWithValue("@PERSONA_ID", personaId);

                    miComando.ExecuteNonQuery();
                }
                miConexion.Close();
            }
        }

    }
}