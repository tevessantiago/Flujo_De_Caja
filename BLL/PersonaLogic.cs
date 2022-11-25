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
        public void InsertarUsuarioId(int usuarioId, int personaId)
        {
            using (var trx = new TransactionScope())
            {
                dao.InsertarUsuarioId(usuarioId, personaId);
                trx.Complete();
            }
        }

        public List<Persona> ObtenerPersonasinUsuario()
        {
            List<Persona> lista = dao.ObtenerPersonas();
            List<Persona> nuevaLista = new List<Persona>();
            foreach(var per in lista)
            {
                if(per.UsuarioId == 0)
                {
                    nuevaLista.Add(per);
                }
            }
            return nuevaLista;
        }
    }
}
