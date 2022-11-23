namespace UI
{
    partial class FormCrearUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPersona = new System.Windows.Forms.ComboBox();
            this.btnCrearUser = new System.Windows.Forms.Button();
            this.rbSi = new System.Windows.Forms.RadioButton();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.flowLayoutPanel1.Controls.Add(this.menuStrip1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(450, 24);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseDown);
            this.flowLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseMove);
            this.flowLayoutPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(49, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(112, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "CREAR USUARIO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Usuario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Contraseña";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(138, 140);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(204, 23);
            this.txtUser.TabIndex = 7;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(138, 179);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(204, 23);
            this.txtPass.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Persona";
            // 
            // cbPersona
            // 
            this.cbPersona.FormattingEnabled = true;
            this.cbPersona.Location = new System.Drawing.Point(138, 105);
            this.cbPersona.Name = "cbPersona";
            this.cbPersona.Size = new System.Drawing.Size(204, 23);
            this.cbPersona.TabIndex = 10;
            // 
            // btnCrearUser
            // 
            this.btnCrearUser.Location = new System.Drawing.Point(187, 250);
            this.btnCrearUser.Name = "btnCrearUser";
            this.btnCrearUser.Size = new System.Drawing.Size(75, 23);
            this.btnCrearUser.TabIndex = 11;
            this.btnCrearUser.Text = "Crear";
            this.btnCrearUser.UseVisualStyleBackColor = true;
            this.btnCrearUser.Click += new System.EventHandler(this.btnCrearUser_Click);
            // 
            // rbSi
            // 
            this.rbSi.AutoSize = true;
            this.rbSi.Location = new System.Drawing.Point(164, 219);
            this.rbSi.Name = "rbSi";
            this.rbSi.Size = new System.Drawing.Size(34, 19);
            this.rbSi.TabIndex = 12;
            this.rbSi.TabStop = true;
            this.rbSi.Text = "SI";
            this.rbSi.UseVisualStyleBackColor = true;
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(276, 219);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(43, 19);
            this.rbNo.TabIndex = 13;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "NO";
            this.rbNo.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Admin";
            // 
            // FormCrearUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(450, 327);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbNo);
            this.Controls.Add(this.rbSi);
            this.Controls.Add(this.btnCrearUser);
            this.Controls.Add(this.cbPersona);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCrearUsuario";
            this.Text = "FormCrearUsuario";
            this.Load += new System.EventHandler(this.FormCrearUsuario_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem salirToolStripMenuItem;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtUser;
        private TextBox txtPass;
        private Label label4;
        private ComboBox cbPersona;
        private Button btnCrearUser;
        private RadioButton rbSi;
        private RadioButton rbNo;
        private Label label5;
    }
}