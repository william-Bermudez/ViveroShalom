using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ViveroShalom.Data;
using ViveroShalom.Models;

namespace ViveroShalom.Forms
{
    public partial class GestorTareasForm : Form
    {
        private List<Tarea> _allTareas = new();
        private Tarea?      _selected;

        public GestorTareasForm()
        {
            InitializeComponent();
            LoadTareas();
        }

        private void LoadTareas(string filtroEstado = "Pendiente", string buscar = "")
        {
            _allTareas = TareaRepository.GetAll(filtroEstado);
            if (!string.IsNullOrWhiteSpace(buscar))
                _allTareas = _allTareas
                    .Where(t => t.NombrePlanta.Contains(buscar, StringComparison.OrdinalIgnoreCase)
                             || t.TipoAccion.Contains(buscar, StringComparison.OrdinalIgnoreCase)
                             || t.Ubicacion.Contains(buscar, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            listTareas.Items.Clear();
            foreach (var t in _allTareas)
            {
                var item = new ListViewItem(new[]
                {
                    t.Icono,
                    $"{t.TipoAccion} – {t.NombrePlanta}",
                    t.Prioridad,
                    t.FechaInicio.ToString("dd/MM/yyyy"),
                })
                { Tag = t };

                item.ForeColor = t.Prioridad switch
                {
                    "Alta"  => Color.Red,
                    "Media" => Color.FromArgb(204, 153, 0),
                    _       => Color.FromArgb(27, 94, 32),
                };
                listTareas.Items.Add(item);
            }

            if (listTareas.Items.Count > 0)
                listTareas.Items[0].Selected = true;
        }

        private void listTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTareas.SelectedItems.Count == 0) return;
            _selected = listTareas.SelectedItems[0].Tag as Tarea;
            if (_selected == null) return;
            ShowDetail(_selected);
        }

        private void ShowDetail(Tarea t)
        {
            lblTituloDetalle.Text  = $"{t.Icono} {t.TipoAccion} – {t.NombrePlanta}";
            lblFechaInicio.Text    = t.FechaInicio.ToString("dd/MM/yyyy");
            lblFechaFin.Text       = t.FechaFin.HasValue ? t.FechaFin.Value.ToString("dd/MM/yyyy") : "-";
            lblRepeticion.Text     = string.IsNullOrEmpty(t.Repeticion) ? "-" : t.Repeticion;
            lblHorario.Text        = t.HoraRecuerdo.HasValue
                ? DateTime.Today.Add(t.HoraRecuerdo.Value).ToString("h:mm tt")
                : "-";
            lblUbicacion.Text      = t.Ubicacion;
            lblDescripcion.Text    = string.IsNullOrEmpty(t.Descripcion) ? "-" : t.Descripcion;
            lblPrioridadDetalle.Text = t.Prioridad;
            lblPrioridadDetalle.ForeColor = t.Prioridad switch
            {
                "Alta"  => Color.Red,
                "Media" => Color.FromArgb(204, 153, 0),
                _       => Color.FromArgb(27, 94, 32),
            };
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = cmbFiltro.SelectedItem?.ToString() ?? "";
            if (filtro == "Todas") filtro = "";
            LoadTareas(filtro, txtBuscar.Text);
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = cmbFiltro.SelectedItem?.ToString() ?? "Pendiente";
            if (filtro == "Todas") filtro = "";
            LoadTareas(filtro, txtBuscar.Text);
        }

        private void btnMarcarCompletada_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            if (MessageBox.Show("¿Marcar tarea como completada?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            TareaRepository.MarcarCompletada(_selected.Id);
            NotificationService.Notify("✅ Tarea completada", _selected.TipoAccion + " – " + _selected.NombrePlanta);
            LoadTareas();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            var frm = new RegistrarTareaForm(_selected);
            frm.ShowDialog();
            LoadTareas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            if (MessageBox.Show($"¿Eliminar la tarea '{_selected.TipoAccion}'?\nEsta acción no se puede deshacer.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            TareaRepository.Delete(_selected.Id);
            LoadTareas();
        }
    }
}
