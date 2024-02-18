using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class UsuarioDTO
    {
        public int Idusuario { get; set; }
        [Required(ErrorMessage = "Ingresar nombre completo")]
        public string? Nombrecompleto { get; set; }
        [Required(ErrorMessage = "Ingresar correo completo")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "Ingresar contraseña")]
        public string? Clave { get; set; }
        [Required(ErrorMessage = "Confirme la contraseña")]
        public string? ConfirmarClave { get; set; }

        public string? Rol { get; set; }


    }
}
