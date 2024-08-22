namespace TM.Web.Filters;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

// Esto no esta implementado, lo copie de lo que hicimos en clase 

namespace TM.Web.Filters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class ValidationsFilter : Attribute
    {
        public ValidationsFilter() { }

        public bool IsValid(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return false;
            if (pass.Length < 8) return false;
            var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*\d)(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!regex.IsMatch(pass)) return false; // validacion de la expresion regular

            return true;
        }
    }
}
//pass = pass.ToLower();
//: base(DataType.EmailAddress)
//public ValidationsFilter(string value) { }