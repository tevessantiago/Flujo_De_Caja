using Entidades;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                proveedor.Proveedor_Nombre = txtNombre.Text;
                proveedor.Proveedor_Rubro = txtRubro.Text;
                proveedor.Proveedor_Fecha_Alta = DateTime.Today;
                if (cbBajaProveedor.Checked)
                {
                    proveedor.Proveedor_Fecha_Baja = DateTime.Today;
                }
                else
                {
                    proveedor.Proveedor_Fecha_Baja = null;
                }
                provLogic.CargarProveedor(proveedor);
                gridProveedor.DataSource = provLogic.ObtenerProveedores();

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
