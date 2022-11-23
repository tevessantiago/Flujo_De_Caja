using DAL;
using Entidades;
using System.Data;
using System.Transactions;

namespace BLL
{
    public class ProveedorLogic
    {
        ProveedorDao dao = new ProveedorDao();

        public void CargarProveedor(Proveedor proveedor)
        {
            bool contieneLetra = false;
            if(string.IsNullOrEmpty(proveedor.Nombre) || string.IsNullOrEmpty(proveedor.Rubro) || string.IsNullOrEmpty(proveedor.CUIT))
            {
                throw new Exception("Todos los campos deben estar completos");
            }
            foreach(char c in proveedor.CUIT)
            {
                if (char.IsLetter(c))
                {
                    contieneLetra = true; 
                    break;
                }

            }
            if(contieneLetra) 
            {
                throw new Exception("Solo se deben ingresar numeros");
            }
            if(proveedor.CUIT.Length != 11)
            {
                throw new Exception("El CUIT/CUIL debe tener 11 digitos");
            }
            if (ExisteProveedor(proveedor))
            {
                throw new Exception($"El proveedor {proveedor.Nombre} ya se encuentra registado");
            }
            using (var trx = new TransactionScope())
            {
                dao.CargarProveedor(proveedor);
                trx.Complete();
            }           
        }

        public List<Proveedor> ObtenerProveedores()
        { 
            return dao.ObtenerProveedores();
        }
        public bool ExisteProveedor(Proveedor proveedor)
        {
            DataTable tabla = dao.ExisteProveedor();
            bool existe = false;
            foreach(DataRow dr in tabla.Rows) 
            {
                if(proveedor.CUIT == dr["PROVEEDOR_CUIT"].ToString())
                {
                    existe = true;
                }
            }
            return existe;
            
        }
        public List<Proveedor> ObtenerProveedoresAlta()
        {
            List<Proveedor> lista = dao.ObtenerProveedores();
            List<Proveedor> nuevaLista = new List<Proveedor>();
            foreach(var prov in lista)
            {
                if(!prov.FechaBaja.HasValue)
                {
                    nuevaLista.Add(prov);
                }
            }

            return nuevaLista;
        }

        public void ModificarProveedor(Proveedor proveedor)
        {
            using(var trx = new TransactionScope())
            {
                dao.ModificarProveedor(proveedor);
                trx.Complete();
            }
            
        }

        public void BajaProveedor(Proveedor proveedor)
        {
            proveedor.FechaBaja = DateTime.Now;
            using (var trx = new TransactionScope())
            {
                dao.BajaProveedor(proveedor);
                trx.Complete();
            }
        }

    }
}
