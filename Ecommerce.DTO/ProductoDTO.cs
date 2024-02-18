using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class ProductoDTO
    {
        public int Idproducto { get; set; }
        [Required(ErrorMessage = "Ingresar un nombre")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "Ingresar una descripción")]
        public string? Descripcion { get; set; }

        public int? Idcategoria { get; set; }
        [Required(ErrorMessage = "Ingresar precio")]
        public decimal? Precio { get; set; }
        [Required(ErrorMessage = "Ingresar precio de oferta")]
        public decimal? Preciooferta { get; set; }
        [Required(ErrorMessage = "Ingresar una cantidad")]
        public int? Cantidad { get; set; }
        [Required(ErrorMessage = "Ingresar una imágen")]
        public string? Imagen { get; set; }

        public DateTime? Fechacreacion { get; set; }
        public virtual CategoriaDTO? IdcategoriaNavigation { get; set; }
    }
}
