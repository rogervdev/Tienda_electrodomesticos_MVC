using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Tienda_electrodomesticos_MVC.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Apellido { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }

}
