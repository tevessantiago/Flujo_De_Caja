using BLL;
using Entidades;
using Microsoft.VisualBasic.Logging;
using System.Runtime.InteropServices;

namespace UI
{
    public partial class ConteoCaja : Form
    {
        MovimientoLogic movimientoLogic = new MovimientoLogic();
        CierreDiarioLogic cierreLogic = new CierreDiarioLogic();
        ProveedorLogic provLogic = new ProveedorLogic();
        TotalLogic totalLogic= new TotalLogic();
        CajaLogic cajaLogic = new CajaLogic();
        PersonaLogic personaLogic = new PersonaLogic();
        private int usuarioId;
        //Emmanuel: Variables para mover el formulario
        int mov;
        int movX;
        int movY;

        public ConteoCaja(int usuarioId)
        {
            InitializeComponent();
            this.usuarioId = usuarioId;
        }

        
        private void ConteoCaja_Load(object sender, EventArgs e)
        {

            try
            {                
                gridCaja.DataSource = movimientoLogic.ObtenerMovimientos();
                gridCaja.ClearSelection();
                comboProveedor.ValueMember = "ProveedorId";
                comboProveedor.DisplayMember = "Nombre";                
                comboProveedor.DataSource = provLogic.ObtenerProveedores();
                comboMovimiento.SelectedIndex = 0;
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
                if (!int.TryParse(comboProveedor.SelectedValue.ToString(), out int proveedorId))
                {
                    throw new Exception("Error: No se pudo obtener el ProveedorId como un entero.");
                }
                if (!int.TryParse(personaLogic.ObtenerPersonaId(this.usuarioId).ToString(), out int personaId))//Vulnerable a 2 personas con mismo userId. O Persona sin userId.
                {
                    throw new Exception("Error: No se pudo obtener personaId.");
                }

                Movimiento movimiento = new Movimiento();

                movimiento.PersonaId = personaId;
                movimiento.ProveedorId = proveedorId;
                movimiento.Tipo = comboMovimiento.Text;
                movimiento.Importe = importe; //Santi: Por default, redondea hacia abajo. Ej: 500.575 = 500.57; 500.576 = 500.58;
                movimiento.FechaCreacion = DateTime.Now;
                movimiento.FechaActualizacion = DateTime.Now;
                movimiento.Comentario = richTextBox1.Text;

                movimientoLogic.CargarMovimiento(movimiento, comboProveedor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtImporte.Clear();
                richTextBox1.Clear();                
                gridCaja.DataSource = movimientoLogic.ObtenerMovimientos();
                gridCaja.ClearSelection();
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

        private void comboMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!comboMovimiento.SelectedItem.ToString().Equals("Pago a Proveedor"))
            {
                if(comboProveedor.SelectedIndex != -1) //Por seguridad, en caso de que comboProveedor no tenga valores.
                {
                    comboProveedor.SelectedIndex = 0;
                }

                comboProveedor.Enabled = false;
            }
            else
            {
                comboProveedor.Enabled = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {                
                if (gridCaja.DataSource != null && gridCaja.SelectedCells.Count > 0)
                {
                    if (!int.TryParse(gridCaja.SelectedCells[0].Value.ToString(), out int movimientoId))
                    {
                        throw new Exception("Error: No se pudo obtener el MovimientoId como un entero.");
                    }

                    movimientoLogic.BorrarMovimiento(movimientoId);                    
                }
                else
                {
                    throw new Exception("Por favor, seleccione un registro.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                gridCaja.DataSource = movimientoLogic.ObtenerMovimientos();
                gridCaja.ClearSelection();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridCaja.DataSource != null && gridCaja.SelectedCells.Count > 0)
                {
                    if(!int.TryParse(gridCaja.SelectedCells[0].Value.ToString(), out int movimientoId))
                    {
                        throw new Exception("Error: No se pudo obtener el MovimientoId como un entero.");
                    }
                    if (!int.TryParse(comboProveedor.SelectedValue.ToString(), out int proveedorId))
                    {
                        throw new Exception("Error: No se pudo obtener el ProveedorId como un entero.");
                    }
                    if (!int.TryParse(personaLogic.ObtenerPersonaId(this.usuarioId).ToString(), out int personaId))//Vulnerable a 2 personas con mismo userId. O Persona sin userId.
                    {
                        throw new Exception("Error: No se pudo obtener personaId.");
                    }

                    Movimiento movimiento = new Movimiento();

                    movimiento.PersonaId = personaId;
                    movimiento.ProveedorId = proveedorId;
                    movimiento.Tipo = comboMovimiento.Text;
                    movimiento.Comentario = richTextBox1.Text;
                    movimiento.FechaActualizacion = DateTime.Now;
                    movimiento.MovimientoId = movimientoId;

                    movimientoLogic.ModificarMovimiento(movimiento, comboProveedor.Text);
                }
                else
                {
                    throw new Exception("Por favor, seleccione un registro.");
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtImporte.Clear();
                richTextBox1.Clear();
                gridCaja.DataSource = movimientoLogic.ObtenerMovimientos();
                gridCaja.ClearSelection();
            }
        }
    }
}
