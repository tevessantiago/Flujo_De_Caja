using Entidades;
using BLL;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UI
{
    public partial class FormPersona : Form
    { 
        PersonaLogic perLogic = new PersonaLogic();

        //Emmanuel: Variables para mover el formulario
        int mov;
        int movX;
        int movY;

        public FormPersona()
        {
            InitializeComponent();
        }

        private void FormPersona_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
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
                Persona persona = new Persona();

                persona.Nombre = txtNombre.Text; //Emmanuel: Asignación del nombre de la persona
                persona.Apellido = txtApellido.Text; //Emmanuel: Asignación del apellido de la persona
                persona.Tipo = txtTipo.Text; //Emmanuel: Asignación del Tipo de la persona
               

                perLogic.CargarPersona(persona);                
                MessageBox.Show("La persona se ha cargado con exito");
                gridPersona.DataSource = null;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtNombre.Clear();
                txtApellido.Clear();    
                txtTipo.Clear();
                gridPersona.DataSource = perLogic.ObtenerPersonas();
            }
        }

        private void personaLogicBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void gridPersona_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                gridPersona.DataSource = perLogic.ObtenerPersonas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbModBajaPersona.Checked)
                {
                    if (MessageBox.Show("Quieres modificar a esta Persona?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (gridPersona.DataSource != null && gridPersona.SelectedCells.Count > 0)
                        {
                            if (!int.TryParse(gridPersona.SelectedCells[0].Value.ToString(), out int personaId))
                            {
                                throw new Exception("Error: No se pudo obtener el PersonaId como un entero.");
                            }
                            Persona persona = new Persona();
                            persona.PersonaId = personaId;
                            persona.Nombre = txtNombre.Text;
                            persona.Apellido = txtApellido.Text;
                            persona.Tipo = txtTipo.Text;
                           

                            perLogic.ModificarPersona(persona);
                            MessageBox.Show("La persona se ha modificado con exito");
                            gridPersona.DataSource = null;
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
                txtApellido.Clear();
                txtTipo.Clear();
                gridPersona.DataSource = perLogic.ObtenerPersonas();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbModBajaPersona.Checked)
                {
                    if (MessageBox.Show("Quieres dar de baja a esta persona?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (gridPersona.DataSource != null && gridPersona.SelectedCells.Count > 0)
                        {
                            if (!int.TryParse(gridPersona.SelectedCells[0].Value.ToString(), out int personaId))
                            {
                                throw new Exception("Error: No se pudo obtener el PersonaId como un entero.");
                            }

                            Persona persona = new Persona();
                            persona.PersonaId= personaId;
                            perLogic.BajaPersona(persona);
                            MessageBox.Show("La persona fue dado de baja");
                            gridPersona.DataSource = null;
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
                txtApellido.Clear();
                txtTipo.Clear();
                gridPersona.DataSource = perLogic.ObtenerPersonas();
            }
        }

        private void gridPersona_SelectionChanged(object sender, EventArgs e)
        {
            if (cbModBajaPersona.Checked)
            {
                DataGridViewRow row = gridPersona.CurrentRow;
                if (row == null)
                    return;

                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtApellido.Text = row.Cells["Apellido"].Value.ToString();
                txtTipo.Text = row.Cells["Tipo"].Value.ToString();

            }
            else
            {
                txtNombre.Clear();
                txtApellido.Clear();
                txtTipo.Clear();
            }
        }

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["FormCrearUsuario"] as FormCrearUsuario) != null)
            {
                MessageBox.Show("La ventana de Crear Usuario ya se encuentra abierta.");
            }
            else
            {
                FormCrearUsuario formCrearUsuario = new FormCrearUsuario();
                formCrearUsuario.Show();
            }
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbModBajaPersona.Checked)
                {
                    if (MessageBox.Show("Quieres recuperar a esta persona?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (gridPersona.DataSource != null && gridPersona.SelectedCells.Count > 0)
                        {
                            if (!int.TryParse(gridPersona.SelectedCells[0].Value.ToString(), out int personaId))
                            {
                                throw new Exception("Error: No se pudo obtener el PersonaId como un entero.");
                            }

                            Persona persona = new Persona();
                            persona.PersonaId = personaId;
                            perLogic.RecuperarPersona(persona);
                            MessageBox.Show("La persona fue recuperada");
                            gridPersona.DataSource = null;
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
                txtApellido.Clear();
                txtTipo.Clear();
                gridPersona.DataSource = perLogic.ObtenerPersonas();
            }
        }
    }
}
