using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
