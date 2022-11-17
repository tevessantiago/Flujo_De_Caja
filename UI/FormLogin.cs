using BLL;
using Entidades;

namespace UI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        UsuarioLogic userLogic = new UsuarioLogic();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario login = new Usuario();
            login.User = txtUser.Text;
            login.Pass = txtPass.Text;

            bool existe = userLogic.VerificarUsuario(login);
            if (existe)
            {
                if(!int.TryParse(userLogic.ObtenerUsuarioId(txtUser.Text, txtPass.Text).ToString(), out int usuarioId))//Vulnerable a 2 usuarios con mismo user y pass.
                {
                    MessageBox.Show("Error: No se pudo obtener el usuarioId.");
                    return;
                }
                
                //Application.Run(new ConteoCaja(usuarioId)); // Qué diferencia hay?
                ConteoCaja abrir = new ConteoCaja(usuarioId);
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
