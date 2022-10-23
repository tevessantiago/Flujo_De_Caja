﻿using Entidades;
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

                persona.Persona_Nombre = txtNombre.Text;
                persona.Persona_Apellido = txtApellido.Text;
                persona.Persona_Tipo = txtTipo.Text;

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
    }
}