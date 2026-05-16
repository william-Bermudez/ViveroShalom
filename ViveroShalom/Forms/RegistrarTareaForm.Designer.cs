using System.Drawing;
using System.Windows.Forms;

namespace ViveroShalom.Forms
{
    partial class RegistrarTareaForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel       panelHeader;
        private Label       lblHeader;
        private Panel       panelNav;
        private Panel       panelContent;
        private Panel       cardForm;

        private TextBox     txtNombrePlanta;
        private TextBox     txtEspecie;
        private TextBox     txtTipoAccion;
        private ComboBox    cmbIcono;
        private ComboBox    cmbPrioridad;
        private TextBox     txtUbicacion;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private ComboBox    cmbRepeticion;
        private DateTimePicker dtpHora;
        private TextBox     txtDescripcion;
        private Button      btnSeleccionarFoto;
        private FlowLayoutPanel flowFotos;
        private Label       lblFotos;
        private Button      btnGuardar;

        private Label lblErrorNombre;
        private Label lblErrorAccion;
        private Label lblErrorUbicacion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text            = "Registrar Tarea – Vivero Shalom";
            this.Size            = new Size(780, 680);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = Color.White;
            this.Font            = new Font("Segoe UI", 10f);
            this.AutoScroll      = true;

            // Header
            panelHeader = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(27,94,32) };
            lblHeader   = new Label { Text = "🌿  Vivero Shalom", ForeColor = Color.White,
                Font = new Font("Segoe UI",18f,FontStyle.Bold), Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(15,0,0,0) };
            panelHeader.Controls.Add(lblHeader);

            // Nav
            panelNav = SharedNav.Build(
                onInicio: () => Close(),
                onGestionar: () => { new GestorTareasForm().ShowDialog(); Close(); },
                onRegistrar: () => {},
                activeBtn: "registrar");

            // Content area
            panelContent = new Panel
            {
                Location  = new Point(160, 55),
                Size      = new Size(604, 595),
                BackColor = Color.FromArgb(235,235,235),
                AutoScroll= true,
            };

            // Card formulario
            cardForm = new Panel
            {
                Location  = new Point(10, 10),
                Size      = new Size(574, 550),
                BackColor = Color.White,
                Padding   = new Padding(20),
            };

            var lblTitle = Lbl("Registra Tarea", 0, 5, 534, 30,
                new Font("Segoe UI",14f,FontStyle.Bold), ContentAlignment.MiddleCenter);

            // Campos
            int y = 45;
            txtNombrePlanta  = Field("Ingrese El Nombre De La Planta",   0, y, 534, ref y);
            lblErrorNombre   = ErrLbl(y); y+=18;
            txtEspecie       = Field("Ingrese Especie De la Planta (Opcional)", 0, y, 534, ref y);

            // Tipo de acción + Icono
            y += 8;
            cardForm.Controls.Add(Lbl("Tipo de Acción e Icono",0,y,200,20,null,ContentAlignment.MiddleLeft)); y+=20;
            txtTipoAccion = new TextBox { Location=new Point(0,y), Size=new Size(340,28), PlaceholderText="Ingrese El Tipo De Accion", BorderStyle=BorderStyle.FixedSingle, BackColor=System.Drawing.Color.FromArgb(240,240,240) };
            cmbIcono = new ComboBox { Location=new Point(348,y), Size=new Size(186,28), DropDownStyle=ComboBoxStyle.DropDownList };
            cmbIcono.Items.AddRange(new object[]{"🌿 Abonar","💧 Riego","✂️ Poda","🌱 Siembra","🪣 Fertilizar","🔍 Inspección","💊 Tratamiento"});
            cmbIcono.SelectedIndex = 0;
            cardForm.Controls.AddRange(new Control[]{txtTipoAccion, cmbIcono});
            y += 34;
            lblErrorAccion = ErrLbl(y); y+=18;

            // Prioridad
            cardForm.Controls.Add(Lbl("Prioridad",0,y,100,20,null,ContentAlignment.MiddleLeft)); y+=20;
            cmbPrioridad = new ComboBox { Location=new Point(0,y), Size=new Size(200,28), DropDownStyle=ComboBoxStyle.DropDownList };
            cmbPrioridad.Items.AddRange(new object[]{"Alta","Media","Baja"});
            cmbPrioridad.SelectedIndex = 1;
            cardForm.Controls.Add(cmbPrioridad); y+=34;

            // Ubicación
            txtUbicacion = Field("Ingrese La Ubicación", 0, y, 534, ref y);
            lblErrorUbicacion = ErrLbl(y); y+=18;

            // Fechas
            cardForm.Controls.Add(Lbl("Fecha de Inicio",0,y,200,20,null,ContentAlignment.MiddleLeft));
            cardForm.Controls.Add(Lbl("Fecha de Finalización",270,y,264,20,null,ContentAlignment.MiddleLeft));
            y+=20;
            dtpFechaInicio = new DateTimePicker { Location=new Point(0,y), Size=new Size(260,28), Format=DateTimePickerFormat.Short };
            dtpFechaFin    = new DateTimePicker { Location=new Point(270,y), Size=new Size(264,28), Format=DateTimePickerFormat.Short };
            cardForm.Controls.AddRange(new Control[]{dtpFechaInicio,dtpFechaFin}); y+=38;

            // Repetición + Hora
            cardForm.Controls.Add(Lbl("Repetición",0,y,200,20,null,ContentAlignment.MiddleLeft));
            cardForm.Controls.Add(Lbl("Hora de Recuerdo",270,y,264,20,null,ContentAlignment.MiddleLeft));
            y+=20;
            cmbRepeticion = new ComboBox { Location=new Point(0,y), Size=new Size(260,28), DropDownStyle=ComboBoxStyle.DropDownList };
            cmbRepeticion.Items.AddRange(new object[]{"Sin repetición","Cada día","Cada 3 días","Cada 8 días","Cada semana","Cada 15 días","Cada mes"});
            cmbRepeticion.SelectedIndex = 0;
            dtpHora = new DateTimePicker { Location=new Point(270,y), Size=new Size(264,28), Format=DateTimePickerFormat.Time, ShowUpDown=true };
            cardForm.Controls.AddRange(new Control[]{cmbRepeticion,dtpHora}); y+=38;

            // Descripción
            txtDescripcion = new TextBox { Location=new Point(0,y), Size=new Size(534,60),
                PlaceholderText="Ingrese Una Descripción (Opcional)",
                BorderStyle=BorderStyle.FixedSingle, BackColor=System.Drawing.Color.FromArgb(240,240,240),
                Multiline=true };
            cardForm.Controls.Add(txtDescripcion); y+=68;

            // Fotos
            cardForm.Controls.Add(Lbl("Fotos (Opcional)",0,y,200,20,null,ContentAlignment.MiddleLeft)); y+=20;
            btnSeleccionarFoto = new Button
            {
                Text="\U0001F4F7 Seleccionar Fotos", Location=new Point(0,y), Size=new Size(160,36),
                BackColor=System.Drawing.Color.FromArgb(235,235,235), FlatStyle=FlatStyle.Flat, Cursor=Cursors.Hand,
            };
            btnSeleccionarFoto.Click += btnSeleccionarFoto_Click;
            lblFotos = Lbl("Sin fotos",168,y+8,100,20,null,ContentAlignment.MiddleLeft);
            flowFotos = new FlowLayoutPanel { Location=new Point(0,y+40), Size=new Size(534,70), AutoScroll=true };
            cardForm.Controls.AddRange(new Control[]{btnSeleccionarFoto,lblFotos,flowFotos}); y+=115;

            // Botón guardar
            btnGuardar = new Button
            {
                Text="Registrar Tarea", Location=new Point(0,y), Size=new Size(534,40),
                BackColor=System.Drawing.Color.FromArgb(27,94,32), ForeColor=System.Drawing.Color.White,
                Font=new Font("Segoe UI",12f,FontStyle.Bold), FlatStyle=FlatStyle.Flat, Cursor=Cursors.Hand,
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += btnGuardar_Click;
            cardForm.Controls.Add(btnGuardar);

            cardForm.Controls.AddRange(new Control[]{lblTitle,lblErrorNombre,lblErrorAccion,lblErrorUbicacion});

            cardForm.Height = y + 60;
            panelContent.Controls.Add(cardForm);

            this.Controls.Add(panelContent);
            this.Controls.Add(panelNav);
            this.Controls.Add(panelHeader);

            this.ResumeLayout(false);
        }

        // helpers
        private TextBox Field(string placeholder, int x, int y, int w, ref int newY)
        {
            var tb = new TextBox
            {
                Location=new Point(x,y), Size=new Size(w,28),
                PlaceholderText=placeholder, BorderStyle=BorderStyle.FixedSingle,
                BackColor=System.Drawing.Color.FromArgb(240,240,240),
            };
            cardForm.Controls.Add(tb);
            newY = y + 34;
            return tb;
        }

        private Label ErrLbl(int y) => new Label
        {
            Text="Este campo es obligatorio.", ForeColor=System.Drawing.Color.Red,
            Location=new Point(0,y), AutoSize=true, Font=new Font("Segoe UI",8.5f),
            Visible=false,
        };

        private static Label Lbl(string text, int x, int y, int w, int h, Font? f, ContentAlignment align) =>
            new Label
            {
                Text=text, Location=new Point(x,y), Size=new Size(w,h),
                Font=f ?? new Font("Segoe UI",9f,FontStyle.Bold),
                TextAlign=align, AutoSize=false,
            };
    }
}
