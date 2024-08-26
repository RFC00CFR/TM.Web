using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TM.Web.Filters
{
    public class ValidationsFilter : ValidationAttribute
    {
        private readonly Regex _regex = new Regex(@"^[a-zA-Z0-9]+$"); // Solo letras y números

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = value as string;

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult("El campo es requerido.");
            }

            if (!_regex.IsMatch(input))
            {
                return new ValidationResult("El campo solo puede contener letras y números, sin caracteres especiales.");
            }

            return ValidationResult.Success;
        }
    }
}
