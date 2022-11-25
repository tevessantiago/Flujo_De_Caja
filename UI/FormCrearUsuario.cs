using BLL;
using Entidades;

namespace UI
{
    public partial class FormCrearUsuario : Form
    {
        public FormCrearUsuario()
        {
            InitializeComponent();
        }
        int mov;
        int movX;
        int movY;
        UsuarioLogic userLogic = new UsuarioLogic();
        PersonaLogic personaLogic= new PersonaLogic();
        private void btnCrearUser_Click(object sender, EventArgs e)
        {
            try
            {
                if(personaLogic.ObtenerPersonasinUsuario().Count != 0)
                {
                    if (MessageBox.Show("Quieres crear este Usuario?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (!int.TryParse(cbPersona.SelectedValue.ToString(), out int personaId))
                        {
                            throw new Exception("Error: No se pudo obtener el PersonaId como un entero.");
                        }
                        Usuario usuario = new Usuario();
                        Persona persona = new Persona();

                        usuario.User = txtUser.Text;
                        usuario.Pass = txtPass.Text;
                        int usuarioId = userLogic.CrearUsuario(usuario);
                        personaLogic.InsertarUsuarioId(usuarioId, personaId);

                    }
                }
                else
                {
                    MessageBox.Show("No hay personas para generar un Usuario");
                }
                                                   
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtUser.Clear();
                txtPass.Clear();
                actualizarComboPersona();
            }
        }

        private void FormCrearUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                actualizarComboPersona();
                this.TopMost = true;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void flowLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void cbPersona_DropDown(object sender, EventArgs e)
        {
            try
            {
                cbPersona.DataSource = personaLogic.ObtenerPersonasinUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo obtener personas: " + ex.Message);
            }
        }

        private void actualizarComboPersona()
        {
            if (personaLogic.ObtenerPersonasinUsuario().Count() > 0)
            {
                cbPersona.ValueMember = "PersonaId";
                cbPersona.DisplayMember = "Nombre";
                cbPersona.DataSource = personaLogic.ObtenerPersonasinUsuario();
                cbPersona.SelectedIndex = 0;
            }
            else
            {
                cbPersona.SelectedIndex = -1;
                cbPersona.Enabled = false;
            }
        }
    }
}
