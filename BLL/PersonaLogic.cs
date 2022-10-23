using DAL;
using Entidades;

namespace BLL
{
    public class PersonaLogic
    {
        PersonaDao dao = new PersonaDao();

        public void CargarPersona(Persona persona)
        {
            dao.CargarPersona(persona);
        }

        public List<Persona> ObtenerPersonas()
        {
            return dao.ObtenerPersonas();
        }
    }
}
