using ProyectoPOS_1CA_A.CapaDatos;
using ProyectoPOS_1CA_A.CapaEntidades;
using ProyectoPOS_1CA_A.CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoPOS_1CA_A.CapaPresentacion
{
    public partial class FrmRegistrodeVentas : Form
    {
        public FrmRegistrodeVentas()
        {
            InitializeComponent();
        }

        private void FrmRegistrodeVentas_Load(object sender, EventArgs e)
        {
            // --- CLIENTES --- 
            cboCliente.DataSource = ClienteDAL.ListarActivos();
            cboCliente.DisplayMember = "Nombre";
            cboCliente.ValueMember = "Id";
            // --- TIPO PAGO --- 
            cboTipoPago.DataSource = TipoPagoDAL.Listar(); //asiganmos datos al desplegable 
            cboTipoPago.DisplayMember = "Nombre"; //lo que muestra 
            cboTipoPago.ValueMember = "Id"; //el valor que nos importa ID 
                                            // --- FECHA --- 
            dtpFecha.Value = DateTime.Now;//obtiene la fecha de ahora 
                                          // --- CONFIGURAR COLUMNAS DEL DETALLE --- 
            CargarProductos(string.Empty);
            ConfigurarTablaDetalles();
        }
        //Meétodo que trae los datos al dgv, colocado fuera del LOAD 
        private void CargarProductos(string filtro)
        {
            //Obtenemos la lista desde DAL 
            var tabla = ProductoDAL.Listar(); // ya lo creamos en Paso 2 
                                              // Filtrar si hay texto 
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var dv = tabla.DefaultView;
                dv.RowFilter = $"Nombre LIKE '%{filtro}%'";
                dgvProductos.DataSource = dv;
            }
            else
            {
                dgvProductos.DataSource = tabla;
            }
            dgvProductos.Columns["Id"].Visible = false;
        }





        //Definimos un método para crear las columnas para el DataGrid de detalles
        private void ConfigurarTablaDetalles()
        {
            dgvDetalles.Columns.Clear();
            // ID PRODUCTO 
            DataGridViewTextBoxColumn colIdProd = new DataGridViewTextBoxColumn();
            colIdProd.Name = "Id_Producto";
            colIdProd.HeaderText = "ID";
            colIdProd.Visible = false;
            dgvDetalles.Columns.Add(colIdProd);
            // NOMBRE PRODUCTO 
            dgvDetalles.Columns.Add("NombreProducto", "Producto");
            // CANTIDAD 
            DataGridViewTextBoxColumn colCant = new DataGridViewTextBoxColumn();
            colCant.Name = "Cantidad";
            colCant.HeaderText = "Cant.";
            dgvDetalles.Columns.Add(colCant);
            // PRECIO UNITARIO 
            DataGridViewTextBoxColumn colPrecio = new DataGridViewTextBoxColumn();
            colPrecio.Name = "PrecioUnitario";
            colPrecio.HeaderText = "Precio Unitario";
            dgvDetalles.Columns.Add(colPrecio);
            // SUBTOTAL 
            DataGridViewTextBoxColumn colSub = new DataGridViewTextBoxColumn();
            colSub.Name = "SubTotal";
            colSub.HeaderText = "Subtotal";
            colSub.ReadOnly = true;
            dgvDetalles.Columns.Add(colSub);
            // Asegurar permisos de edición 
            dgvDetalles.ReadOnly = false;
            // Columnas NO editables 
            dgvDetalles.Columns["SubTotal"].ReadOnly = true;
            dgvDetalles.Columns["PrecioUnitario"].ReadOnly = true;
            dgvDetalles.Columns["NombreProducto"].ReadOnly = true;
            dgvDetalles.Columns["Id_Producto"].ReadOnly = true;
            // ÚNICA columna editable: 
            dgvDetalles.Columns["Cantidad"].ReadOnly = false;
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarProducto.Text.Trim();
            CargarProductos(texto);

        }



        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }
            DataGridViewRow row = dgvProductos.SelectedRows[0];
            int idProducto = Convert.ToInt32(row.Cells["Id"].Value);
            string nombre = row.Cells["Nombre"].Value.ToString();
            decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
            // Cantidad inicial = 1 
            int cantidad = 1;
            decimal subTotal = precio * cantidad;
            // Agregar al detalle 
            dgvDetalles.Rows.Add(
            idProducto,
            nombre,
            cantidad,
            precio, 
            subTotal
            );
            RecalcularTotal();//dará error, mas adelante se creará el método, comenta esta linea cuando ejecutes 
        }

        

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarProductos(txtBuscarProducto.Text.Trim());
            }

        }
        private void RecalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                total += Convert.ToDecimal(row.Cells["SubTotal"].Value);
            }
            lblTotal.Text = $"Total:$" + total.ToString("0.00");
        }

        private void dgvDetalles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Si editaron la columna Cantidad 
            if (dgvDetalles.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                DataGridViewRow row = dgvDetalles.Rows[e.RowIndex];
                int cantidad;
                bool ok = int.TryParse(row.Cells["Cantidad"].Value?.ToString(), out cantidad);
                if (!ok || cantidad <= 0)
                {
                    MessageBox.Show("Cantidad inválida.");
                    row.Cells["Cantidad"].Value = 1;
                    cantidad = 1;
                }
                decimal precio = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value);
                decimal subTotal = cantidad * precio;
                row.Cells["SubTotal"].Value = subTotal;
                // Recalcular total general 
                RecalcularTotal();
            }
        }
        //Programamnos el boton quitar
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para quitar.");
                return;
            }
            dgvDetalles.Rows.RemoveAt(dgvDetalles.SelectedRows[0].Index);
            RecalcularTotal();
        }

        private void btnLimpiarDetalle_Click(object sender, EventArgs e)
        {
            dgvDetalles.Rows.Clear();
            RecalcularTotal();
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalles.Rows.Count == 0)
                {
                    MessageBox.Show("La venta no tiene productos,", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //_________________
                //CREAR OBJETO VENTA
                //__________________
                Venta venta = new Venta()
                {
                    Fecha = dtpFecha.Value,
                    MontoTotal = ObtenerTotalVenta(), // lo creamos abajo 
                    Id_Cliente = Convert.ToInt32(cboCliente.SelectedValue),
                    Id_TipoPago = Convert.ToInt32(cboTipoPago.SelectedValue)
                };

                //__________
                //CREAR LISTA DE LOS DETALLES
                //_______________
                List<DetalleVenta> detalles = new List<DetalleVenta>();
                foreach (DataGridViewRow row in dgvDetalles.Rows)
                {
                    detalles.Add(new DetalleVenta()
                    {
                        Id_Producto = Convert.ToInt32(row.Cells["Id_Producto"].Value),
                        Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                        PrecioUnitario = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value),
                        SubTotal = Convert.ToDecimal(row.Cells["SubTotal"].Value)
                    });
                }
                // --------------------------------------------------- 
                // 3) VALIDAR EN BLL 
                // --------------------------------------------------- 
                var validacion = VentaBLL.ValidarVenta(venta, detalles);
                if (!validacion.Exito)
                {
                    MessageBox.Show(validacion.Mensaje, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                


                // --------------------------------------------------- 
                // 4) GUARDAR EN BASE DE DATOS (TRANSACCIÓN) 
                // --------------------------------------------------- 
                var resultado = VentaDAL.RegistrarVentaTransaccional(venta, detalles);
                if (resultado.Exito)
                {
                    MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Error inesperado: " + ex.Message);
            }
        }
         //metodo para calcular el total de la venta
         private decimal ObtenerTotalVenta()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvDetalles.Rows)
                total += Convert.ToDecimal(row.Cells["SubTotal"].Value);
            return total;
        }
        private void LimpiarFormulario()
        {
            dgvDetalles.Rows.Clear();
            lblTotal.Text = "Total: $0.00";
            txtBuscarProducto.Clear();
            CargarProductos(string.Empty); // recarga lista completa
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregarProducto_Click(sender, e);
        }
    }
    }

    
    


