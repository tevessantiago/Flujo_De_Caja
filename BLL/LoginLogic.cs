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
    public class LoginLogic
    {
        LoginDao dao = new LoginDao();


        public bool VerificarUsuario(Login login)
        {
            DataTable tabla = dao.VerificarUsuario();
            bool existe = false;
            foreach (DataRow dr in tabla.Rows)
            {
                if ((login.User == dr["LOGIN_USER"].ToString()) && login.Pass == dr["LOGIN_PASS"].ToString())
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
    }
}
