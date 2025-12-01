using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPOS_1CA_A.CapaEntidades
{
    public  class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public String Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaNacimiento { get; set; }
       
        public string Telefono { get; set; }
        public bool Estado { get; set; }
    }
}
