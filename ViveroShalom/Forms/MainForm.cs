using System;
using System.Drawing;
using System.Windows.Forms;
using ViveroShalom.Data;

namespace ViveroShalom.Forms
{
    public partial class MainForm : Form
    {
        private NotifyIcon _notifyIcon = null!;

        public MainForm()
        {
            InitializeComponent();
            SetupTrayIcon();
            LoadDashboard();
        }

        private void SetupTrayIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Text = "Vivero Shalom",
                Visible = true,
            };
            var menu = new ContextMenuStrip();
            menu.Items.Add("Abrir", null, (s, e) => { Show(); WindowState = FormWindowState.Normal; });
            menu.Items.Add("Salir", null, (s, e) => Application.Exit());
            _notifyIcon.ContextMenuStrip = menu;
            _notifyIcon.DoubleClick += (s, e) => { Show(); WindowState = FormWindowState.Normal; };

            NotificationService.Start(_notifyIcon);
        }

        private void LoadDashboard()
        {
            try
            {
                int totalPlantas = PlantaRepository.CountTotal();
                int tareasPend = TareaRepository.CountPendientes();

                lblPlantas.Text = $"[{totalPlantas}]";
                lblPendientes.Text = $"[{tareasPend}]";
                lblPendientes.ForeColor = tareasPend > 0 ? Color.Red : Color.Green;

                var proximas = TareaRepository.GetProximas(30);
                dgvTareas.Rows.Clear();
                foreach (var t in proximas)
                {
                    string hora = t.HoraRecuerdo.HasValue
                        ? DateTime.Today.Add(t.HoraRecuerdo.Value).ToString("h:mm tt")
                        : "-";
                    int idx = dgvTareas.Rows.Add(
                        t.Icono, t.TipoAccion, t.Ubicacion, t.Prioridad,
                        t.FechaInicio.ToString("dd/MM/yyyy"), hora);
                    var row = dgvTareas.Rows[idx];
                    row.DefaultCellStyle.ForeColor = t.Prioridad switch
                    {
                        "Alta" => Color.Red,
                        "Media" => Color.FromArgb(204, 153, 0),
                        _ => Color.FromArgb(27, 94, 32),
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        // ── Navegación ──────────────────────────────────────────────────────
        private void BtnInicio_Click(object sender, EventArgs e)
        {
            HighlightNav(btnInicio);
            LoadDashboard();
        }

        private void BtnGestionarTareas_Click(object sender, EventArgs e)
        {
            HighlightNav(btnGestionarTareas);
            new GestorTareasForm().ShowDialog();
            LoadDashboard();
        }

        private void BtnRegistrarTarea_Click(object sender, EventArgs e)
        {
            HighlightNav(btnRegistrarTarea);
            new RegistrarTareaForm().ShowDialog();
            LoadDashboard();
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            NotificationService.Stop();
            _notifyIcon.Visible = false;
            new LoginForm().Show();
            this.Close();
        }

        private void HighlightNav(Button active)
        {
            foreach (Control c in panelNav.Controls)
                if (c is Button b)
                    b.BackColor = Color.FromArgb(30, 30, 30);
            active.BackColor = Color.FromArgb(70, 130, 180);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            NotificationService.Stop();
            _notifyIcon.Visible = false;
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void dgvTareas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
