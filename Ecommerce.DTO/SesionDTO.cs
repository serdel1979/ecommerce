using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class SesionDTO
    {
        public int Idusuario { get; set; }

        public string? Nombrecompleto { get; set; }

        public string? Correo { get; set; }

        public string? Clave { get; set; }

        public string? Rol { get; set; }
    }
}
