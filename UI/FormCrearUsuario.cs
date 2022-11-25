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
                if(rbSi.Checked || rbNo.Checked)
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
                        if (rbSi.Checked)
                        {
                            usuario.Admin = "Y";
                        }
                        if (rbNo.Checked)
                        {
                            usuario.Admin = "N";
                        }
                        persona.PersonaId = personaId;
                        usuario.UsuarioId = userLogic.CrearUsuario(usuario);
                        personaLogic.InsertarUsuarioId(usuario, persona);
                        
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar SI o NO");
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
            catch (Exception)
            {

                throw;
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
                cbPersona.DataSource = personaLogic.ObtenerPersonasinUsuario();// Cambie para que me traiga solo los que esten de alta
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo obtener proveedores: " + ex.Message);
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
