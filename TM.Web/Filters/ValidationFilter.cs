
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TM.Web.Filters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class ValidationsFilter : Attribute
    {
        public ValidationsFilter() { }

        public bool IsValid(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return false;
            var regex = new Regex(@"^[a-zA-Z0-9]+$"); // Solo letras y números, sin caracteres especiales
            if (!regex.IsMatch(nombre)) return false; // validación de la expresión regular

            return true;
        }
    }
}

//nombre = nombre.ToLower();
//: base(DataType.EmailAddress)
//public ValidationsFilter(string value) { }
