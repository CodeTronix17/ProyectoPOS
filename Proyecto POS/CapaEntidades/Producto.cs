using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POS.CapaEntidades
{
    public class Producto //power el nivel de acceso publico
    {
        // definir atributos de las clases
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion  { get; set; }
        public decimal precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }


    }
}
