using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ApiRestBilling5.Models.DTOs
{
    public class UserLoginDTO
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "El usuario es obligatorio")]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
    }
}
