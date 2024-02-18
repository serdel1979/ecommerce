using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class DetalleVentaDTO
    {
        public int Iddetalleventa { get; set; }

        public int? Idproducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Total { get; set; }
    }
}
