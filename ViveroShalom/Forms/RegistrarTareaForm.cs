using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using ViveroShalom.Data;
using ViveroShalom.Models;

namespace ViveroShalom.Forms
{
    public partial class RegistrarTareaForm : Form
    {
        private readonly Tarea? _editTarea;
        private List<string> _fotoPaths = new();

        // Modo NUEVO
        public RegistrarTareaForm()
        {
            InitializeComponent();
            this.Text = "Registrar Tarea – Vivero Shalom";
        }

        // Modo EDITAR
        public RegistrarTareaForm(Tarea tarea)
        {
            InitializeComponent();
            this.Text  = "Editar Tarea – Vivero Shalom";
            _editTarea = tarea;
            btnGuardar.Text = "Actualizar Tarea";
            LoadEditData(tarea);
        }

        private void LoadEditData(Tarea t)
        {
            txtNombrePlanta.Text  = t.NombrePlanta;
            txtEspecie.Text       = t.EspeciePlanta;
            txtTipoAccion.Text    = t.TipoAccion;
            cmbIcono.Text         = t.Icono;
            cmbPrioridad.Text     = t.Prioridad;
            txtUbicacion.Text     = t.Ubicacion;
            dtpFechaInicio.Value  = t.FechaInicio;
            if (t.FechaFin.HasValue) dtpFechaFin.Value = t.FechaFin.Value;
            cmbRepeticion.Text    = t.Repeticion;
            if (t.HoraRecuerdo.HasValue)
                dtpHora.Value = DateTime.Today.Add(t.HoraRecuerdo.Value);
            txtDescripcion.Text   = t.Descripcion;
            if (!string.IsNullOrEmpty(t.FotosJson))
            {
                _fotoPaths = JsonSerializer.Deserialize<List<string>>(t.FotosJson) ?? new();
                RefreshFotos();
            }
        }

        private bool ValidarCampos()
        {
            lblErrorNombre.Visible   = string.IsNullOrWhiteSpace(txtNombrePlanta.Text);
            lblErrorAccion.Visible   = string.IsNullOrWhiteSpace(txtTipoAccion.Text);
            lblErrorUbicacion.Visible= string.IsNullOrWhiteSpace(txtUbicacion.Text);
            return !lblErrorNombre.Visible && !lblErrorAccion.Visible && !lblErrorUbicacion.Visible;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var t = new Tarea
            {
                Id           = _editTarea?.Id ?? 0,
                NombrePlanta = txtNombrePlanta.Text.Trim(),
                EspeciePlanta= txtEspecie.Text.Trim(),
                TipoAccion   = txtTipoAccion.Text.Trim(),
                Icono        = cmbIcono.Text.Split(' ')[0],
                Prioridad    = cmbPrioridad.Text,
                Ubicacion    = txtUbicacion.Text.Trim(),
                FechaInicio  = dtpFechaInicio.Value.Date,
                FechaFin     = dtpFechaFin.Value.Date > dtpFechaInicio.Value.Date
                               ? dtpFechaFin.Value.Date : (DateTime?)null,
                Repeticion   = cmbRepeticion.Text,
                HoraRecuerdo = dtpHora.Value.TimeOfDay,
                Descripcion  = txtDescripcion.Text.Trim(),
                FotosJson    = _fotoPaths.Count > 0 ? JsonSerializer.Serialize(_fotoPaths) : "",
                Estado       = "Pendiente",
            };

            try
            {
                if (_editTarea == null)
                    TareaRepository.Insert(t);
                else
                    TareaRepository.Update(t);

                NotificationService.Notify(
                    _editTarea == null ? "✅ Tarea registrada" : "✏️ Tarea actualizada",
                    $"{t.Icono} {t.TipoAccion} – {t.NombrePlanta}");

                MessageBox.Show(
                    _editTarea == null ? "Tarea registrada correctamente." : "Tarea actualizada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title     = "Seleccionar foto",
                Filter    = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Multiselect = true,
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            foreach (var f in dlg.FileNames)
                if (!_fotoPaths.Contains(f))
                    _fotoPaths.Add(f);
            RefreshFotos();
        }

        private void RefreshFotos()
        {
            flowFotos.Controls.Clear();
            foreach (var path in _fotoPaths)
            {
                if (!File.Exists(path)) continue;
                var pb = new PictureBox
                {
                    Size    = new Size(80, 60),
                    SizeMode= PictureBoxSizeMode.Zoom,
                    Margin  = new Padding(3),
                };
                try { pb.Image = Image.FromFile(path); } catch { }
                flowFotos.Controls.Add(pb);
            }
            lblFotos.Text = _fotoPaths.Count > 0 ? $"{_fotoPaths.Count} foto(s)" : "Sin fotos";
        }
    }
}
