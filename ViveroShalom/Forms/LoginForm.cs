using System;
using System.Drawing;
using System.Windows.Forms;
using ViveroShalom.Data;
using ViveroShalom.Models;

namespace ViveroShalom.Forms
{
    public partial class LoginForm : Form
    {
        public static Usuario? CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();

            // Inicializar BD al arrancar
            try { DatabaseHelper.Initialize(); }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a MariaDB.\n\n" + ex.Message +
                    "\n\nRevisa la cadena de conexión en Data/DatabaseHelper.cs",
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            { lblError.Text = "Ingresa tu usuario."; return; }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            { lblError.Text = "Ingresa tu contraseña."; return; }

            try
            {
                var user = UsuarioRepository.Login(txtUsuario.Text.Trim(), txtPassword.Text);
                if (user == null)
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    txtPassword.Clear();
                    return;
                }
                CurrentUser = user;
                new MainForm().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al conectar con la base de datos.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin_Click(sender, e);
        }

        private void btnMostrarPwd_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            btnMostrarPwd.Text = txtPassword.UseSystemPasswordChar ? "👁" : "🔒";
        }
    }
}
