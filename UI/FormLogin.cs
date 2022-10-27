using BLL;
using Entidades;
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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        LoginLogic loginLogic = new LoginLogic();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.User = txtUser.Text;
            login.Pass = txtPass.Text;

            bool existe = loginLogic.VerificarUsuario(login);
            if (existe)
            {
                ConteoCaja abrir = new ConteoCaja();
                this.Hide();
                abrir.Show();
            }
            else if (!existe)
            {
                MessageBox.Show("El usuario y/o contraseña es incorrecto");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }
    }
}
