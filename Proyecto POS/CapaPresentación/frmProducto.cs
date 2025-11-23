using Proyecto_POS.CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_POS.CapaPresentación
{
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }
        //creacion de una lsta que simulara la DB
        private static List<Producto> listaProducto = new List<Producto>();




        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {

        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            // cargar los datos iniciales
            if (!listaProducto.Any())
                // cada vez que se cargue el formulario, si la lista esta vacia 
                // se agregan los productos inciales
                listaProducto.Add(new Producto
                {
                    Id = 1,
                    Nombre = "Café Gurmet",
                    Descripcion = "Importado",
                    precio = 10.5m,
                    Stock = 100,
                    Estado = true
                });

            listaProducto.Add(new Producto
            {
                Id = 2,
                Nombre = "Café Borbon ",
                Descripcion = "Importado",
                precio = 200m,
                Stock = 20,
                Estado = true
            });
            listaProducto.Add(new Producto
            {
                Id = 3,
                Nombre = "Chesscake",
                Descripcion = "Importado",
                precio = 15.75m,
                Stock = 75,
                Estado = true
            });
            RefrescarGrid();
        }




        // boton guardar
        private void RefrescarGrid()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = listaProducto;

        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //validacoines vasicas
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                return;
            }
            if (!Validaciones.EsEntero(txtStock.Text))
            {
                MessageBox.Show("El Stock del producto debe ser un valor entero.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStock.Focus();
                return;
            }
            // crear objeto prodcuti y asignar id incrementr manual mente
            int nuevoId = listaProducto.Any() ? listaProducto.Max(x => x.Id) + 1 : 1;
            var p = new Producto
            {
                Id = nuevoId,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripción.Text,
                precio = decimal.Parse(txtPrecio.Text),
                Stock = int.Parse(txtStock.Text),
                Estado = chkEstado.Checked,

            };
            // agregar lista
            listaProducto.Add(p);
            RefrescarGrid();
            // limpiar los controles
            LimpiarCampos();
        }
        // metodo para lipiar los controles
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripción.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            chkEstado.Checked = true;
            txtNombre.Focus();
        }
        // evento data greviuw
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;
            // Obtener el producto seleccinado
            txtid.Text = dgvProductos.CurrentRow.Cells["Id"].Value.ToString();
            txtNombre.Text = dgvProductos.CurrentRow.Cells["Nombre"].Value.ToString();
            txtDescripción.Text = dgvProductos.CurrentRow.Cells["Descripción"].Value.ToString();
            txtPrecio.Text = dgvProductos.CurrentRow.Cells["Precio"].Value.ToString();
            txtStock.Text = dgvProductos.CurrentRow.Cells["Stock"].Value.ToString();
            chkEstado.Checked = (bool)dgvProductos.CurrentRow.Cells["Estado"].Value;


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Evento para eliminar productos
            if (!int.TryParse(txtid.Text, out int id))
            {

                MessageBox.Show("Seleccione un producto válido para Eliminar .", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var prod = listaProducto.FirstOrDefault(x => x.Id == id);
            if (prod == null)
            {
                MessageBox.Show("Producto no encontrado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("¿Estas seguro de eliminar el produto " + prod.Nombre + "?", "Confirmar Eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listaProducto.Remove(prod);
                MessageBox.Show("Producto eliminado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefrescarGrid();
                LimpiarCampos();
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Evento para editar productos
            if (!int.TryParse(txtid.Text, out int id))
            {
                MessageBox.Show("Seleccione un producto válido para editar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var prod = listaProducto.FirstOrDefault(x => x.Id == id);
            if (prod == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }
            //validaciones basicas
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }
            if (!Validaciones.EsDecimal(txtPrecio.Text))
            {
            
                MessageBox.Show("El precion debe ser un valor decimal.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                return;
            }
            // actualizar los datos del producto
            prod.Nombre = txtNombre.Text.Trim ();
            prod.Descripcion = txtDescripción.Text.Trim();
            prod.precio = decimal.Parse(txtPrecio.Text.Trim());
            prod.Stock = int.Parse(txtStock.Text.Trim());
            prod.Estado = chkEstado.Checked;
            MessageBox.Show("Producto actualizado exitosamente.", "Exito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarGrid();
            LimpiarCampos();
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {

        }
    }
}