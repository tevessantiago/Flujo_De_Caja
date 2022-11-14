﻿using BLL;
using Entidades;
using System.Runtime.InteropServices;

namespace UI
{
    public partial class ConteoCaja : Form
    {
        MovimientoLogic movLogic = new MovimientoLogic();
        CierreDiarioLogic cierreLogic = new CierreDiarioLogic();
        ProveedorLogic provLogic = new ProveedorLogic();

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
                gridCaja.DataSource = cierreLogic.ObtenerCierreDiario();
                comboProveedor.DataSource = provLogic.ObtenerProveedores();
                comboProveedor.DisplayMember = "Nombre";
                comboProveedor.ValueMember = "ProveedorId";
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

                movimiento.PersonaId = 1; //Santi: Hay que determinar de dónde sale este valor.
                movimiento.ProveedorId = (int)comboProveedor.SelectedIndex; //Fran deberiamos acomodar la matriz.
                movimiento.Tipo = comboMovimiento.Text;
                movimiento.Importe = importe; //Santi: Por default, redondea hacia abajo. Ej: 500.575 = 500.57; 500.576 = 500.58;
                movimiento.FechaCreacion = DateTime.Now.Date; //Santi: Esto se puede hacer directamente desde la clase Movimiento.
                movimiento.FechaActualizacion = DateTime.Now.Date; //Santi: En C# este valor no puede ser nulo. Queda pendiente esto, aunque seguramente terminemos seteando la fecha de la carga por default.
                movimiento.Comentario = richTextBox1.Text; //Santi: Pendiente lógica para hacerlo obligatorio en updates.

                movLogic.CargarMovimiento(movimiento);
                gridCaja.DataSource = cierreLogic.ObtenerCierreDiario();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

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
    }
}
