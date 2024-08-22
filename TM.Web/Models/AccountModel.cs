using System.ComponentModel.DataAnnotations;

namespace TM.Web.Models
{
    public class AccountModel
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Apellido { get; set; } = null!;
    }
}
