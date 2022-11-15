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
    public class TotalLogic
    {
        TotalDao totalDao = new TotalDao();
        public double ObtenerTotal(Total total)
        {
            DataTable tabla = totalDao.ObtenerTotal();
            double totalImporte = 0;

            foreach (DataRow dr in tabla.Rows) 
            {
                if(total.FechaDesde <= DateTime.Parse(dr["MOVIMIENTO_FECHA_CREACION"].ToString()) && total.FechaHasta >= DateTime.Parse(dr["MOVIMIENTO_FECHA_CREACION"].ToString())) 
                {
                    total.TotalImporte = double.Parse(dr["IMPORTE"].ToString());
                    
                }
                else
                {
                    total.TotalImporte = 0;
                }
                totalImporte =+ total.TotalImporte;
            }

            return totalImporte;
            
        }
    }
}
