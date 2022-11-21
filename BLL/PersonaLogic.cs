using DAL;
using Entidades;
using System.Transactions;

namespace BLL
{
    public class PersonaLogic
    {
        PersonaDao dao = new PersonaDao();

        public void CargarPersona(Persona persona)
        {
            bool contieneNum = false;
            if(string.IsNullOrEmpty(persona.Nombre) || string.IsNullOrEmpty(persona.Apellido) || string.IsNullOrEmpty(persona.Tipo))
            {
                throw new Exception("Todos los campos deben estar completos");
            }
            foreach(char c in persona.Nombre) 
            {
                if(char.IsDigit(c))
                {
                    contieneNum= true;
                }
            }
            if(contieneNum)
            {
                throw new Exception("El nombre no debe contener numeros");
            }
            foreach (char c in persona.Apellido)
            {
                if (char.IsDigit(c))
                {
                    contieneNum = true;
                }
            }
            if (contieneNum)
            {
                throw new Exception("El apellido no debe contener numeros");
            }


            using (var trx = new TransactionScope())
            {
                dao.CargarPersona(persona);
                trx.Complete();
            }
            
        }

        public List<Persona> ObtenerPersonas()
        {
            return dao.ObtenerPersonas();
        }

        public int ObtenerPersonaId(int usuarioId)
        {
            return dao.ObtenerPersonaId(usuarioId);
        }
    }
}
