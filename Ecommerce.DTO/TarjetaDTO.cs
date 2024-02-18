using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class TarjetaDTO
    {
        [Required(ErrorMessage ="Ingresar titular")]
        public string? Titular { get; set; }
        [Required(ErrorMessage = "Ingresar número de tarjeta")]
        public string? Numero { get; set; }
        [Required(ErrorMessage = "Ingresar Fecha de vencimiento")]
        public string? Vigencia { get; set; }
        [Required(ErrorMessage = "Ingresar código CVV")]
        public string? CVV { get; set; }
    }
}
