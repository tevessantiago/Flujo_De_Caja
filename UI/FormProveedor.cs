using Entidades;
using BLL;

namespace UI
{
    public partial class FormProveedor : Form
    {
        ProveedorLogic provLogic = new ProveedorLogic();

        //Emmanuel: Variables para mover el formulario
        int mov;
        int movX;
        int movY;

        public FormProveedor()
        {
            InitializeComponent();
        }

        private void FormProveedor_Load(object sender, EventArgs e)
        {
            this.TopLevel = true;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbModBajaProveedor.Checked)
                {
                    if (MessageBox.Show("Quieres dar de baja a este proveedor?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (gridProveedor.DataSource != null && gridProveedor.SelectedCells.Count > 0)
                        {
                            if (!int.TryParse(gridProveedor.SelectedCells[0].Value.ToString(), out int proveedorId))
                            {
                                throw new Exception("Error: No se pudo obtener el ProveedorId como un entero.");
                            }

                            Proveedor proveedor = new Proveedor();
                            proveedor.ProveedorId = proveedorId;
                            provLogic.BajaProveedor(proveedor);
                            MessageBox.Show("El Proveedor fue dado de baja");
                        }
                    }                        
                }
                else
                {
                    MessageBox.Show("Debe marcar Modificara/Baja para realizar la baja");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtNombre.Clear();
                txtRubro.Clear();
                txtCUIT.Clear();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); //Emma: Salida del Formulario
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //Emmanuel: Asignación de valores en las variables de movimiento de Mouse
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //Emmanuel: Validación de valores aplicados en el movimiento de Mouse
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            //Emmanuel: Asignación de valor de movimiento a 0
            mov = 0;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor proveedor = new Proveedor();

                proveedor.Nombre = txtNombre.Text;
                proveedor.Rubro = txtRubro.Text;
                proveedor.CUIT = txtCUIT.Text;
                proveedor.FechaAlta = DateTime.Today;
                if (cbModBajaProveedor.Checked)
                {
                    proveedor.FechaBaja = DateTime.Today;
                }
                else
                {
                    proveedor.FechaBaja = null;
                }
                provLogic.CargarProveedor(proveedor);
                MessageBox.Show("El Proveedor se ha cargado con exito");
                gridProveedor.DataSource = provLogic.ObtenerProveedores();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtNombre.Clear();
                txtRubro.Clear();
                txtCUIT.Clear();

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbModBajaProveedor.Checked)
                {
                    if (MessageBox.Show("Quieres modificar a este proveedor?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (gridProveedor.DataSource != null && gridProveedor.SelectedCells.Count > 0)
                        {
                            if (!int.TryParse(gridProveedor.SelectedCells[0].Value.ToString(), out int proveedorId))
                            {
                                throw new Exception("Error: No se pudo obtener el ProveedorId como un entero.");
                            }
                            Proveedor proveedor = new Proveedor();

                            proveedor.Nombre = txtNombre.Text;
                            proveedor.Rubro = txtRubro.Text;
                            proveedor.CUIT = txtCUIT.Text;
                            proveedor.ProveedorId = proveedorId;

                            provLogic.ModificarProveedor(proveedor);
                            MessageBox.Show("El proveedor se ha modificado con exito");
                            gridProveedor.DataSource = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe marcar Modificara/Baja para realizar la modificacion");
                }
                   
                    
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtNombre.Clear();
                txtRubro.Clear();
                txtCUIT.Clear();
                gridProveedor.DataSource = provLogic.ObtenerProveedores();
            }
            

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                gridProveedor.DataSource = provLogic.ObtenerProveedores();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void gridProveedor_SelectionChanged(object sender, EventArgs e)
        {
            if (cbModBajaProveedor.Checked)
            {
                DataGridViewRow row = gridProveedor.CurrentRow;
                if (row == null)
                    return;

                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtRubro.Text = row.Cells["Rubro"].Value.ToString();
                txtCUIT.Text = row.Cells["CUIT"].Value.ToString();

            }
            else
            {
                txtNombre.Clear();
                txtRubro.Clear();
                txtCUIT.Clear();
            }

        }
    }
}
