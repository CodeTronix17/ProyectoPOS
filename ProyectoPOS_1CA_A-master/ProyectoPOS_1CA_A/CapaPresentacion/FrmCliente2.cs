using ProyectoPOS_1CA_A.CapaEntidades;
using ProyectoPOS_1CA_A.CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPOS_1CA_A.CapaPresentacion
{
    public partial class FrmCliente2 : Form
    {
        int ClienteId = 0;
        ClienteBLL bll = new ClienteBLL();
        private object dgvClientes2;

        

        public FrmCliente2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dvgCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmCliente2_Load(object sender, EventArgs e)
        {
            CargarDatos();

            Limpiar();
        }
        public void CargarDatos()
        {
            dgvClientes.DataSource = bll.Listar();
        }
        
         void Limpiar()
        {
            txtNombre.Clear();
            txtDui.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            chkEstado.Checked = true;
            txtBuscar.Clear();
            txtNombre.Focus();

            ClienteId = 0; 
        }

        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente2 c = new Cliente2 // si es cero es un nuevo registro si ya hay uno de modifica
                {
                    Id = ClienteId,
                    Nombre = txtNombre.Text,
                    Dui = txtDui.Text,
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text,
                    Estado = chkEstado.Checked
                };
                int Id = bll.Guardar(c);
                MessageBox.Show("cliente guardado con exito ", "Notificación",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                Limpiar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0 )
            {
                ClienteId = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["id"].Value);
                txtNombre.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtDui.Text = dgvClientes.Rows[e.RowIndex].Cells["Dui"].Value . ToString();
                txtTelefono.Text = dgvClientes.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                txtCorreo.Text = dgvClientes.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(dgvClientes.Rows[e.RowIndex].Cells["Estado"].Value);
                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (ClienteId ==0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Esta seguro de eliminar el cliente seleccionado?","Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==DialogResult.Yes)
            {
                bll.Eliminar(ClienteId);
                CargarDatos();
                Limpiar();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = bll.Buscar(txtBuscar.Text);
        }
    }
    }




        

        


   


    
            
    

