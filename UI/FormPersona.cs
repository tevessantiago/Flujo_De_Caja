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
                if (cbBajaPersona.Checked) //Emmanuel: Validación de si la Persona  es Nueva o se la da de BAJA
                {
                    persona.Estado = "BAJA";
                }
                else
                {
                    persona.Estado = "ALTA";
                }

                perLogic.CargarPersona(persona);
                gridPersona.DataSource = perLogic.ObtenerPersonas();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void personaLogicBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void gridPersona_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
