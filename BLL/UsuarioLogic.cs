using DAL;
using Entidades;
using System.Data;
using System.Transactions;

namespace BLL
{
    public class UsuarioLogic
    {
        UsuarioDao dao = new UsuarioDao();


        public bool VerificarUsuario(Usuario login)
        {
            DataTable tabla = dao.VerificarUsuario();
            bool existe = false;
            foreach (DataRow dr in tabla.Rows)
            {
                if ((login.User == dr["USUARIO_USER"].ToString()) && login.Pass == dr["USUARIO_PASS"].ToString())
                {

                    existe = true;
                }
            }
            if (!existe)
            {
                return false;
            }
            return true;

        }

        public int ObtenerUsuarioId(string user, string pass)
        {
            return dao.ObtenerUsuarioId(user, pass);
        }
        public string ObtenerAdmin(string user, string pass)
        {
            return dao.ObtenerAdmin(user, pass);
        }
        public int CrearUsuario(Usuario usuario)
        {
            int usuarioId = 0;
            usuario.Admin = "N";
            using (var trx = new TransactionScope())
            {
                usuarioId = dao.CrearUsuario(usuario);
                trx.Complete();
            }
            return usuarioId;
        }
    }
}
