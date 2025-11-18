using Proyecto_POS.CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }
    }
}
