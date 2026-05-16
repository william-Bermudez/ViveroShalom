using System.Drawing;
using System.Windows.Forms;

namespace ViveroShalom.Forms
{
    partial class GestorTareasForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel       panelHeader;
        private Label       lblHeader;
        private Panel       panelNav;
        private Panel       panelContent;
        private TextBox     txtBuscar;
        private ComboBox    cmbFiltro;
        private ListView    listTareas;
        private Panel       panelDetalle;
        private Label       lblInfoTitle;
        private Label       lblTituloDetalle;
        private Label       lblFILabel; private Label lblFechaInicio;
        private Label       lblFFLabel; private Label lblFechaFin;
        private Label       lblRepLabel; private Label lblRepeticion;
        private Label       lblHorLabel; private Label lblHorario;
        private Label       lblUbLabel;  private Label lblUbicacion;
        private Label       lblDescLabel;private Label lblDescripcion;
        private Label       lblPriorLabel; private Label lblPrioridadDetalle;
        private Button      btnMarcarCompletada;
        private Button      btnEditar;
        private Button      btnEliminar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text            = "Gestor de Tareas – Vivero Shalom";
            this.Size            = new Size(1000, 640);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = Color.White;
            this.Font            = new Font("Segoe UI", 10f);

            // Header
            panelHeader = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(27,94,32) };
            lblHeader   = new Label { Text = "🌿  Vivero Shalom", ForeColor = Color.White,
                Font = new Font("Segoe UI",18f,FontStyle.Bold), Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(15,0,0,0) };
            panelHeader.Controls.Add(lblHeader);

            // Nav (left)
            panelNav = SharedNav.Build(
                onInicio: () => { this.Close(); },
                onGestionar: () => { },
                onRegistrar: () => { new RegistrarTareaForm().ShowDialog(); LoadTareas(); },
                activeBtn: "gestionar");

            // Content
            panelContent = new Panel
            {
                Location  = new Point(160, 55),
                Size      = new Size(824, 555),
                BackColor = Color.FromArgb(235,235,235),
                Padding   = new Padding(15),
            };

            // Título
            var lblTitulo = new Label
            {
                Text     = "Gestor De Tareas",
                Location = new Point(10, 10),
                AutoSize = true,
                Font     = new Font("Segoe UI", 16f, FontStyle.Bold),
            };

            // Filtros
            txtBuscar = new TextBox { Location = new Point(10,52), Size = new Size(200,28), PlaceholderText = "🔍 Buscar..." };
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            cmbFiltro = new ComboBox
            {
                Location      = new Point(220, 52),
                Size          = new Size(160, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            cmbFiltro.Items.AddRange(new object[] { "Pendiente", "Completada", "Cancelada", "Todas" });
            cmbFiltro.SelectedIndex = 0;
            cmbFiltro.SelectedIndexChanged += cmbFiltro_SelectedIndexChanged;

            // Lista de tareas
            listTareas = new ListView
            {
                Location         = new Point(10, 92),
                Size             = new Size(310, 430),
                View             = View.Details,
                FullRowSelect    = true,
                GridLines        = true,
                MultiSelect      = false,
                BackColor        = Color.White,
                BorderStyle      = BorderStyle.FixedSingle,
                Font             = new Font("Segoe UI", 9.5f),
                HideSelection    = false,
            };
            listTareas.Columns.Add("", 30);
            listTareas.Columns.Add("Tarea",    155);
            listTareas.Columns.Add("Prioridad", 65);
            listTareas.Columns.Add("Fecha",     55);
            listTareas.SelectedIndexChanged += listTareas_SelectedIndexChanged;

            // Panel detalle
            panelDetalle = new Panel
            {
                Location  = new Point(330, 92),
                Size      = new Size(480, 430),
                BackColor = Color.White,
                Padding   = new Padding(15),
            };

            lblInfoTitle = new Label
            {
                Text     = "Información",
                Location = new Point(10,8),
                AutoSize = true,
                Font     = new Font("Segoe UI",14f,FontStyle.Bold),
            };

            lblTituloDetalle = MkLbl("", 10, 40, 450, 28, new Font("Segoe UI",11f,FontStyle.Bold), Color.FromArgb(27,94,32));

            // Campos
            int y = 80;
            lblFILabel   = MkLbl("Fecha de Inicio",    10, y,     160, 20, new Font("Segoe UI",9f,FontStyle.Bold), Color.Black);
            lblFFLabel   = MkLbl("Fecha de Finalización",180,y,   270, 20, new Font("Segoe UI",9f,FontStyle.Bold), Color.Black);
            y+=20;
            lblFechaInicio  = MkLbl("-", 10, y, 160, 22, new Font("Segoe UI",9.5f), Color.DimGray);
            lblFechaFin     = MkLbl("-", 180, y,270, 22, new Font("Segoe UI",9.5f), Color.DimGray);
            y+=30;
            lblRepLabel  = MkLbl("Repetición",     10,y,160,20,new Font("Segoe UI",9f,FontStyle.Bold),Color.Black);
            lblHorLabel  = MkLbl("Horario",       180,y,270,20,new Font("Segoe UI",9f,FontStyle.Bold),Color.Black);
            y+=20;
            lblRepeticion= MkLbl("-", 10,y,160,22,new Font("Segoe UI",9.5f),Color.DimGray);
            lblHorario   = MkLbl("-",180,y,270,22,new Font("Segoe UI",9.5f),Color.DimGray);
            y+=30;
            lblUbLabel   = MkLbl("Ubicación",10,y,450,20,new Font("Segoe UI",9f,FontStyle.Bold),Color.Black);
            y+=20;
            lblUbicacion = MkLbl("-", 10,y,450,22,new Font("Segoe UI",9.5f),Color.DimGray);
            y+=30;
            lblDescLabel = MkLbl("Descripción",10,y,450,20,new Font("Segoe UI",9f,FontStyle.Bold),Color.Black);
            y+=20;
            lblDescripcion=MkLbl("-",10,y,450,40,new Font("Segoe UI",9.5f),Color.DimGray);
            y+=50;
            lblPriorLabel = MkLbl("Prioridad",10,y,90,20,new Font("Segoe UI",9f,FontStyle.Bold),Color.Black);
            y+=20;
            lblPrioridadDetalle=MkLbl("-",10,y,90,22,new Font("Segoe UI",10f,FontStyle.Bold),Color.Green);

            // Botones de acción
            btnMarcarCompletada = MkBtn("Marcar Completado", Color.FromArgb(27,94,32), 10, 370);
            btnEditar           = MkBtn("Editar Tarea",      Color.FromArgb(70,130,180), 160, 370);
            btnEliminar         = MkBtn("Eliminar Tarea",    Color.Red,                  310, 370);
            btnMarcarCompletada.Click += btnMarcarCompletada_Click;
            btnEditar.Click           += btnEditar_Click;
            btnEliminar.Click         += btnEliminar_Click;

            panelDetalle.Controls.AddRange(new Control[]
            {
                lblInfoTitle, lblTituloDetalle,
                lblFILabel, lblFFLabel, lblFechaInicio, lblFechaFin,
                lblRepLabel, lblHorLabel, lblRepeticion, lblHorario,
                lblUbLabel, lblUbicacion,
                lblDescLabel, lblDescripcion,
                lblPriorLabel, lblPrioridadDetalle,
                btnMarcarCompletada, btnEditar, btnEliminar,
            });

            panelContent.Controls.AddRange(new Control[]
            { lblTitulo, txtBuscar, cmbFiltro, listTareas, panelDetalle });

            this.Controls.Add(panelContent);
            this.Controls.Add(panelNav);
            this.Controls.Add(panelHeader);

            this.ResumeLayout(false);
        }

        // helpers
        private static Label MkLbl(string txt, int x, int y, int w, int h, Font f, Color fg) =>
            new Label { Text=txt, Location=new Point(x,y), Size=new Size(w,h), Font=f, ForeColor=fg, AutoSize=false };

        private static Button MkBtn(string text, Color bg, int x, int y) => new Button
        {
            Text=text, Location=new Point(x,y), Size=new Size(138,32),
            BackColor=bg, ForeColor=Color.White,
            Font=new Font("Segoe UI",9f,FontStyle.Bold),
            FlatStyle=FlatStyle.Flat, Cursor=Cursors.Hand,
        };
    }
}
