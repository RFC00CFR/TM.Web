using System.ComponentModel.DataAnnotations;
using TM.Web.Filters;

namespace TM.Web.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [ValidationsFilter]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido.")]
        [ValidationsFilter]
        public string Apellido { get; set; } = string.Empty;
    }
}
