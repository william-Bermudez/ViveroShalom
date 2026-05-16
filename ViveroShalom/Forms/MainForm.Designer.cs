using System.Drawing;
using System.Windows.Forms;

namespace ViveroShalom.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel              panelHeader;
        private Label              lblHeader;
        private Panel              panelNav;
        private Button             btnInicio;
        private Button             btnGestionarTareas;
        private Button             btnRegistrarTarea;
        private Button             btnCerrarSesion;
        private Panel              panelContent;
        private Panel              cardPlantas;
        private Label              lblPlantasIcon;
        private Label              lblPlantasTitle;
        private Label              lblPlantas;
        private Panel              cardPendientes;
        private Label              lblPendientesIcon;
        private Label              lblPendientesTitle;
        private Label              lblPendientes;
        private Label              lblProximas;
        private DataGridView       dgvTareas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblHeader = new Label();
            panelNav = new Panel();
            btnCerrarSesion = new Button();
            panelContent = new Panel();
            lblProximas = new Label();
            dgvTareas = new DataGridView();
            lblPlantasIcon = new Label();
            lblPlantasTitle = new Label();
            lblPlantas = new Label();
            lblPendientesIcon = new Label();
            lblPendientesTitle = new Label();
            lblPendientes = new Label();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            panelHeader.SuspendLayout();
            panelNav.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTareas).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(lblHeader);
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(200, 100);
            panelHeader.TabIndex = 2;
            // 
            // lblHeader
            // 
            lblHeader.Location = new Point(0, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(100, 23);
            lblHeader.TabIndex = 0;
            // 
            // panelNav
            // 
            panelNav.Controls.Add(btnCerrarSesion);
            panelNav.Location = new Point(0, 0);
            panelNav.Name = "panelNav";
            panelNav.Size = new Size(200, 100);
            panelNav.TabIndex = 1;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Location = new Point(0, 0);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(75, 23);
            btnCerrarSesion.TabIndex = 0;
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(lblProximas);
            panelContent.Controls.Add(dgvTareas);
            panelContent.Location = new Point(0, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(200, 100);
            panelContent.TabIndex = 0;
            // 
            // lblProximas
            // 
            lblProximas.Location = new Point(0, 0);
            lblProximas.Name = "lblProximas";
            lblProximas.Size = new Size(100, 23);
            lblProximas.TabIndex = 0;
            // 
            // dgvTareas
            // 
            dgvTareas.ColumnHeadersHeight = 29;
            dgvTareas.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            dgvTareas.Location = new Point(0, 0);
            dgvTareas.Name = "dgvTareas";
            dgvTareas.RowHeadersWidth = 51;
            dgvTareas.Size = new Size(240, 150);
            dgvTareas.TabIndex = 1;
            dgvTareas.CellContentClick += dgvTareas_CellContentClick;
            // 
            // lblPlantasIcon
            // 
            lblPlantasIcon.Location = new Point(0, 0);
            lblPlantasIcon.Name = "lblPlantasIcon";
            lblPlantasIcon.Size = new Size(100, 23);
            lblPlantasIcon.TabIndex = 0;
            // 
            // lblPlantasTitle
            // 
            lblPlantasTitle.Location = new Point(0, 0);
            lblPlantasTitle.Name = "lblPlantasTitle";
            lblPlantasTitle.Size = new Size(100, 23);
            lblPlantasTitle.TabIndex = 0;
            // 
            // lblPlantas
            // 
            lblPlantas.Location = new Point(0, 0);
            lblPlantas.Name = "lblPlantas";
            lblPlantas.Size = new Size(100, 23);
            lblPlantas.TabIndex = 0;
            // 
            // lblPendientesIcon
            // 
            lblPendientesIcon.Location = new Point(0, 0);
            lblPendientesIcon.Name = "lblPendientesIcon";
            lblPendientesIcon.Size = new Size(100, 23);
            lblPendientesIcon.TabIndex = 0;
            // 
            // lblPendientesTitle
            // 
            lblPendientesTitle.Location = new Point(0, 0);
            lblPendientesTitle.Name = "lblPendientesTitle";
            lblPendientesTitle.Size = new Size(100, 23);
            lblPendientesTitle.TabIndex = 0;
            // 
            // lblPendientes
            // 
            lblPendientes.Location = new Point(0, 0);
            lblPendientes.Name = "lblPendientes";
            lblPendientes.Size = new Size(100, 23);
            lblPendientes.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Tipo";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Acción";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Ubicación";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Prioridad";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Fecha";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.Width = 125;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Hora";
            dataGridViewTextBoxColumn6.MinimumWidth = 6;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.Width = 125;
            // 
            // MainForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(902, 553);
            Controls.Add(panelContent);
            Controls.Add(panelNav);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            MinimumSize = new Size(900, 560);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vivero Shalom";
            panelHeader.ResumeLayout(false);
            panelNav.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTareas).EndInit();
            ResumeLayout(false);
        }

        // ── helpers ─────────────────────────────────────────────────────────
        private static Button MakeNavBtn(string text, int top) => new Button
        {
            Text      = text,
            Location  = new Point(10, top),
            Size      = new Size(140, 90),
            BackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 10f),
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand,
            TextAlign = ContentAlignment.MiddleCenter,
        };

        private static Panel MakeCard(int x, int y, int w, int h) => new Panel
        {
            Location  = new Point(x, y),
            Size      = new Size(w, h),
            BackColor = Color.White,
        };

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}
