using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proyecto_POS.CapaEntidades
{
    public static class Validaciones
    {
        // Método para validar si una cadena es un número entero
        public static bool EsEntero(string s)
        {
            int numero;
         return int.TryParse(s, out numero);
        }
        // Método para validar si una cadena es un número decimal
        public static bool EsDecimal(string s)
        {
            decimal numero;
            return decimal.TryParse(s, out numero);
        }
        // Método para validar la cireccion del  correo
        public static bool EscorreoValido(String email) {
            if ( String.IsNullOrWhiteSpace(email)) return false;
            //Expresion regular para validar correo
            var patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
            {

        }
       
        }
    }
}
