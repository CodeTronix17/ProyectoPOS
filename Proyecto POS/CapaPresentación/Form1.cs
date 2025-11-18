using Proyecto_POS.CapaPresentación;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_POS
{
    public partial class MenuScript : Form
    {
        public MenuScript()
        {
            InitializeComponent();
        }

        private void cierreDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void btmVentaRapida_Click(object sender, EventArgs e)
        {

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            // crea una instancia en el formulario frmProducto
            frmProducto frm = new frmProducto();
            // muestra el formulario
            frm.ShowDialog();
        }
    }
}
