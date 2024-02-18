using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Ingresar el correo")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "Confirme la contraseña")]
        public string? Clave { get; set; }

    }
}
