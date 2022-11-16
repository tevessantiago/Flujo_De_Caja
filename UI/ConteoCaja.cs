using BLL;
using Entidades;
using System.Runtime.InteropServices;

namespace UI
{
    public partial class ConteoCaja : Form
    {
        MovimientoLogic movLogic = new MovimientoLogic();
        CierreDiarioLogic cierreLogic = new CierreDiarioLogic();
        ProveedorLogic provLogic = new ProveedorLogic();
        TotalLogic totalLogic= new TotalLogic();
        CajaLogic cajaLogic = new CajaLogic();
        //Emmanuel: Variables para mover el formulario
        int mov;
        int movX;
        int movY;

        public ConteoCaja()
        {
            InitializeComponent();
        }

        
        private void ConteoCaja_Load(object sender, EventArgs e)
        {

            try
            {
                gridCaja.DataSource = movLogic.ObtenerMovimientos();
                comboProveedor.ValueMember = "ProveedorId";
                comboProveedor.DisplayMember = "Nombre";                
                comboProveedor.DataSource = provLogic.ObtenerProveedores();
                ActualizarLabelCaja();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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

                movimiento.PersonaId = 1; //Santi: Hay que determinar de dónde sale este valor.
                movimiento.ProveedorId = int.Parse(comboProveedor.SelectedValue.ToString());
                movimiento.Tipo = comboMovimiento.Text;
                movimiento.Importe = importe; //Santi: Por default, redondea hacia abajo. Ej: 500.575 = 500.57; 500.576 = 500.58;
                movimiento.FechaCreacion = DateTime.Now.Date; //Santi: Esto se puede hacer directamente desde la clase Movimiento.
                movimiento.FechaActualizacion = DateTime.Now.Date; //Santi: En C# este valor no puede ser nulo. Queda pendiente esto, aunque seguramente terminemos seteando la fecha de la carga por default.
                movimiento.Comentario = richTextBox1.Text; //Santi: Pendiente lógica para hacerlo obligatorio en updates.

                movLogic.CargarMovimiento(movimiento);                        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtImporte.Clear();
                richTextBox1.Clear();                
                gridCaja.DataSource = movLogic.ObtenerMovimientos();
                ActualizarLabelCaja();          
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); //Emma: Salida del programa
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //Emmanuel: Asignación de valores en las variables de movimiento de Mouse
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //Emmanuel: Validación de valores aplicados en el movimiento de Mouse
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            //Emmanuel: Asignación de valor de movimiento a 0
            mov = 0;
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Emmanuel: Generar instancia de Form Proveedor y mostrarlo si corresponde.
            
            if ((Application.OpenForms["FormProveedor"] as FormProveedor) != null)
            {
                MessageBox.Show("La ventana de Proveedor ya se encuentra abierta.");
            }
            else
            {
                FormProveedor formProveedor = new FormProveedor();
                formProveedor.Show();
            }

        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Emmanuel: Generar instancia de Form Proveedor y mostrarlo si corresponde.

            if ((Application.OpenForms["FormPersona"] as FormPersona) != null)
            {
                MessageBox.Show("La ventana de Persona ya se encuentra abierta.");
            }
            else
            {
                FormPersona formPersona = new FormPersona();
                formPersona.Show();
            }
        }

        private void btnSumarTotal_Click(object sender, EventArgs e)
        {
            Total total = new Total();
            total.FechaDesde = dtpDesde.Value;
            total.FechaHasta = dtpHasta.Value;


            lblSumaTotal.Text = totalLogic.ObtenerTotal(total).ToString();
        }

        private void ActualizarLabelCaja()
        {
            try
            {
                List<Caja> caja = new List<Caja>();
                caja = cajaLogic.ObtenerCaja();
                lblCaja.Text = "$" + caja[0].Total.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al obtener el total de la Caja: " + ex.Message);
            }
        }
        
    }
}
