using BLL;
using Entidades;

namespace UI
{
    public partial class ConteoCaja : Form
    {
        MovimientoLogic movLogic = new MovimientoLogic();

        public ConteoCaja()
        {
            InitializeComponent();
        }

        private void ConteoCaja_Load(object sender, EventArgs e)
        {
            try
            {
                gridCaja.DataSource = movLogic.ObtenerMovimientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error con la conexion a la BD: " + ex.Message);
            }
            
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtImporte.Text, out double importe))
                {
                    throw new Exception("El importe debe ser un valor decimal.");
                }

                Movimiento movimiento = new Movimiento();

                movimiento.Persona_Id = 1; //Santi: Hay que determinar de dónde sale este valor.
                movimiento.Proveedor_Id = 1; //Santi: Esto debería salir del combobox de Proveedor.
                movimiento.Movimiento_Tipo = comboMovimiento.Text;
                movimiento.Importe = importe; //Santi: Por default, redondea hacia abajo. Ej: 500.575 = 500.57; 500.576 = 500.58;
                movimiento.Movimiento_Fecha_Creacion = DateTime.Now.Date; //Santi: Esto se puede hacer directamente desde la clase Movimiento.
                movimiento.Movimiento_Fecha_Act = DateTime.Now.Date; //Santi: En C# este valor no puede ser nulo. Queda pendiente esto, aunque seguramente terminemos seteando la fecha de la carga por default.
                movimiento.Movimiento_Comentario = richTextBox1.Text; //Santi: Pendiente lógica para hacerlo obligatorio en updates.

                movLogic.CargarMovimiento(movimiento);
                gridCaja.DataSource = movLogic.ObtenerMovimientos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
    }
}
