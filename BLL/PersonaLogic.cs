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
            persona.Estado = "ALTA";
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

        public void BajaPersona(Persona persona)
        {
            persona.Estado = "BAJA";
            using(var trx = new TransactionScope())
            {
                dao.BajaPersona(persona);
                trx.Complete();
            }
        }
        public void ModificarPersona(Persona persona)
        {
            using (var trx = new TransactionScope())
            {
                dao.ModificarPersona(persona);
                trx.Complete();
            }
        }
    }
}
