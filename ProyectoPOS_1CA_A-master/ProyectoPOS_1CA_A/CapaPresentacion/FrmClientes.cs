using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProyectoPOS_1CA_A.CapaEntidades;
namespace ProyectoPOS_1CA_A.CapaPresentacion
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }
        private static List<Cliente> listaCliente = new List<Cliente>();
        
        private void DesabiliarBotones()
        {
          
            btnLimpiar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnAgregarCliente.Enabled = true;
        }
        private void HabilitarBotones()
        {
            btnLimpiar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnAgregarCliente.Enabled = false;
        }


        private void Clientes_Load(object sender, EventArgs e)
        {
            if (!listaCliente.Any())
            {
                listaCliente.Add(new Cliente
                {
                    Id = 1,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    CorreoElectronico = "juan.perez@email.com",
                    FechaNacimiento = new DateTime(1990, 5, 15),
                    Telefono = "809-555-1234",
                    Estado = true
                });
                listaCliente.Add(new CapaEntidades.Cliente
                {
                    Id = 2,
                    Nombre = "María",
                    Apellido = "González",
                    CorreoElectronico = "maria.gonzalez@email.com",
                    FechaNacimiento = new DateTime(1985, 8, 20),
                    Telefono = "809-555-5678",
                    Estado = true
                });
                listaCliente.Add(new Cliente
                {
                    Id = 3,
                    Nombre = "Carlos",
                    Apellido = "Rodríguez",
                    CorreoElectronico = "carlos.rodriguez@email.com",
                    FechaNacimiento= new DateTime(1992, 3, 10),
                    Telefono = "809-555-9012",
                    Estado = true
                });
            }
            RefrescarGrid();
            DesabiliarBotones();
        }
        private void RefrescarGrid()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = listaCliente;
        }
        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del cliente es obligatorio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido del cliente es obligatorio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApellido.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtCorreoElectronico.Text))
            {
                MessageBox.Show("El correo electrónico es obligatorio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCorreoElectronico.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El teléfono es obligatorio", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
                return;
            }
            int nuevoId = listaCliente.Any() ? listaCliente.Max(x => x.Id) + 1 : 1;
            var c = new Cliente
            {
                Id = nuevoId,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                CorreoElectronico = txtCorreoElectronico.Text.Trim(),
                FechaNacimiento = dtpFechaNacimiento.Value,
                Telefono = txtTelefono.Text.Trim(),
                Estado = chkEstado.Checked
            };

            listaCliente.Add(c);
            MessageBox.Show("Cliente agregado exitosamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarGrid();
            // limpiar los controles
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCorreoElectronico.Clear();
            txtTelefono.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            chkEstado.Checked = true;
            txtNombre.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Seleccione un cliente válido para editar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var cliente = listaCliente.FirstOrDefault(x => x.Id == id);
            if (cliente == null)
            {
                MessageBox.Show("Cliente no encontrado.", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del cliente es obligatorio", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido del cliente es obligatorio", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApellido.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtCorreoElectronico.Text))
            {
                MessageBox.Show("El correo electrónico es obligatorio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCorreoElectronico.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El teléfono es obligatorio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
                return;
            }
            cliente.Nombre = txtNombre.Text.Trim();
            cliente.Apellido = txtApellido.Text.Trim();
            cliente.CorreoElectronico = txtCorreoElectronico.Text.Trim();
            cliente.FechaNacimiento = dtpFechaNacimiento.Value;
            cliente.Telefono = txtTelefono.Text.Trim();
            cliente.Estado = chkEstado.Checked;

            MessageBox.Show("Cliente actualizado exitosamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarGrid();
            LimpiarCampos();
            DesabiliarBotones();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Seleccione un cliente válido para eliminar.", "Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            var cliente = listaCliente.FirstOrDefault(x => x.Id == id);
            if (cliente == null)
            {
                MessageBox.Show("Cliente no encontrado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("¿Estas seguro de eliminar el cliente " + cliente.Nombre + " " + cliente.Apellido + "?", "Confirmar Eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listaCliente.Remove(cliente);
                MessageBox.Show("Cliente eliminado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefrescarGrid();
                LimpiarCampos();
                DesabiliarBotones();
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.CurrentRow == null) return;
            txtId.Text = dgvClientes.CurrentRow.Cells["Id"].Value.ToString();
            txtNombre.Text = dgvClientes.CurrentRow.Cells["Nombre"].Value.ToString();
            txtApellido.Text = dgvClientes.CurrentRow.Cells["Apellido"].Value.ToString();
            txtCorreoElectronico.Text = dgvClientes.CurrentRow.Cells["CorreoElectronico"].Value.ToString();
            dtpFechaNacimiento.Value = Convert.ToDateTime(dgvClientes.CurrentRow.Cells["FechaNacimiento"].Value);
            txtTelefono.Text = dgvClientes.CurrentRow.Cells["Telefono"].Value.ToString();
            chkEstado.Checked = (bool)dgvClientes.CurrentRow.Cells["Estado"].Value;
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarBotones();
        }
    }
}

        
        
           
            
               
               

                
            
        
    

