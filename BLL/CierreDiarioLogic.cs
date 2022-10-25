using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CierreDiarioLogic
    {
        CierreDiarioDao dao = new CierreDiarioDao();

        public List<CierreDiario> ObtenerCierreDiario()
        {
            return dao.ObtenerCierreDiario();
        }
    }
}
