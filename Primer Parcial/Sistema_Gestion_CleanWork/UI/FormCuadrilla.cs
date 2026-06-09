using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BE;
using BLL;

namespace UI
{
    public partial class FormCuadrilla : Form
    {
        List<BECuadrilla> _BECuadrilla;
        BLLCuadrilla _BLLCuadrilla;

        public FormCuadrilla()
        {
            InitializeComponent();
            _BLLCuadrilla = new BLLCuadrilla();
            _BECuadrilla = new List<BECuadrilla>();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            cbTurno.SelectedIndex = 0;
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            _BECuadrilla = _BLLCuadrilla.ListarTodo();
            dataGridView1.DataSource = _BECuadrilla;
        }

        private BECuadrilla CargarDelFormulario()
        {
            return new BECuadrilla
            {
                NombreSupervisor = txtNombreSupervisor.Text,
                TurnoTrabajo = cbTurno.SelectedItem.ToString(),
                CantidadOperarios = Convert.ToInt32(txtCantidadOperarios.Text)
            };
        }

        private void LimpiarFormulario()
        {
            txtNombreSupervisor.Clear();
            cbTurno.SelectedIndex = 0;
            txtCantidadOperarios.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                BECuadrilla nuevaCuadrilla = CargarDelFormulario();
                var mensaje = $"¿Estás seguro de crear a la cuadrilla con supervisor: {nuevaCuadrilla.NombreSupervisor}?";
                DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _BLLCuadrilla.Guardar(nuevaCuadrilla);
                }
                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
