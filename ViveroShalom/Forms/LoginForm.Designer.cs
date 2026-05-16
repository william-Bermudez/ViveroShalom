using System.Drawing;
using System.Windows.Forms;

namespace ViveroShalom.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── controles ──────────────────────────────────────────────────────────
        private Panel   panelLeft;
        private Panel   panelRight;
        private Label   lblAppName;
        private PictureBox picLogo;
        private Label   lblBienvenido;
        private Label   lblUsuarioLbl;
        private TextBox txtUsuario;
        private Label   lblPasswordLbl;
        private TextBox txtPassword;
        private Button  btnMostrarPwd;
        private Button  btnLogin;
        private Label   lblError;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────────
            this.Text            = "Vivero Shalom – Iniciar Sesión";
            this.Size            = new Size(820, 500);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = Color.White;
            this.Font            = new Font("Segoe UI", 10f);

            // ── Panel izquierdo (oscuro) ──────────────────────────────────────
            panelLeft = new Panel
            {
                Dock      = DockStyle.Left,
                Width     = 300,
                BackColor = Color.FromArgb(30, 30, 30),
            };

            picLogo = new PictureBox
            {
                Size      = new Size(90, 90),
                Location  = new Point(105, 140),
                SizeMode  = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
            };
            // SVG leaf dibujado como texto emoji grande
            lblAppName = new Label
            {
                Text      = "🌿\nVivero Shalom",
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 18f, FontStyle.Bold),
                AutoSize  = false,
                Size      = new Size(280, 120),
                Location  = new Point(10, 170),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };

            panelLeft.Controls.Add(lblAppName);

            // ── Panel derecho (formulario) ────────────────────────────────────
            panelRight = new Panel
            {
                Location  = new Point(300, 0),
                Size      = new Size(504, 462),
                BackColor = Color.FromArgb(220, 220, 220),
            };

            // Card interna
            var card = new Panel
            {
                Size      = new Size(400, 300),
                Location  = new Point(52, 80),
                BackColor = Color.FromArgb(225, 225, 225),
            };
            card.Paint += (s, e) =>
            {
                var g    = e.Graphics;
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using var pen = new Pen(Color.Silver);
                g.DrawRectangle(pen, rect);
            };

            lblBienvenido = new Label
            {
                Text      = "Bienvenido",
                Font      = new Font("Segoe UI", 20f, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize  = false,
                Size      = new Size(380, 40),
                Location  = new Point(10, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
            };

            lblUsuarioLbl = new Label
            {
                Text      = "Usuario",
                Location  = new Point(20, 75),
                AutoSize  = true,
                Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.Black,
            };

            txtUsuario = new TextBox
            {
                Location    = new Point(20, 96),
                Size        = new Size(360, 32),
                Font        = new Font("Segoe UI", 11f),
                BackColor   = Color.FromArgb(235, 235, 235),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Introduce tu Usuario",
            };

            lblPasswordLbl = new Label
            {
                Text      = "Contraseña",
                Location  = new Point(20, 138),
                AutoSize  = true,
                Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.Black,
            };

            txtPassword = new TextBox
            {
                Location             = new Point(20, 159),
                Size                 = new Size(326, 32),
                Font                 = new Font("Segoe UI", 11f),
                BackColor            = Color.FromArgb(235, 235, 235),
                BorderStyle          = BorderStyle.FixedSingle,
                UseSystemPasswordChar= true,
                PlaceholderText      = "Ingresa tu contraseña",
            };

            btnMostrarPwd = new Button
            {
                Text      = "👁",
                Location  = new Point(350, 159),
                Size      = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(235, 235, 235),
                Font      = new Font("Segoe UI", 12f),
                Cursor    = Cursors.Hand,
            };
            btnMostrarPwd.FlatAppearance.BorderSize = 0;
            btnMostrarPwd.Click += btnMostrarPwd_Click;

            btnLogin = new Button
            {
                Text      = "Iniciar Sesión",
                Location  = new Point(20, 215),
                Size      = new Size(360, 42),
                BackColor = Color.FromArgb(27, 94, 32),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 12f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand,
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += btnLogin_Click;

            lblError = new Label
            {
                Text      = "",
                ForeColor = Color.Red,
                AutoSize  = true,
                Location  = new Point(20, 265),
                Font      = new Font("Segoe UI", 9f),
                BackColor = Color.Transparent,
            };

            card.Controls.AddRange(new Control[]
            {
                lblBienvenido, lblUsuarioLbl, txtUsuario,
                lblPasswordLbl, txtPassword, btnMostrarPwd,
                btnLogin, lblError
            });

            panelRight.Controls.Add(card);

            txtPassword.KeyDown += txtPassword_KeyDown;

            this.Controls.Add(panelLeft);
            this.Controls.Add(panelRight);

            this.ResumeLayout(false);
        }
    }
}
