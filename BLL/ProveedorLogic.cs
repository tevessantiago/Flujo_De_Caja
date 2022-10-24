using DAL;
using Entidades;
namespace BLL
{
    public class ProveedorLogic
    {
        ProveedorDao dao = new ProveedorDao();

        public void CargarProveedor(Proveedor proveedor)
        {
            dao.CargarProveedor(proveedor);
        }

        public List<Proveedor> ObtenerProveedores()
        { 
            return dao.ObtenerProveedores();
        }

    }
}
