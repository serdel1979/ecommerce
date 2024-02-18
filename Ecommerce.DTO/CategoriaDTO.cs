using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class CategoriaDTO
    {
        public int Idcategoria { get; set; }
        [Required(ErrorMessage = "Ingresar nombre")]
        public string? Nombre { get; set; }

    }
}
