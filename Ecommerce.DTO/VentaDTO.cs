using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class VentaDTO
    {
        public int Idventa { get; set; }

        public int? Idusuario { get; set; }

        public decimal? Total { get; set; }

        public virtual ICollection<DetalleVentaDTO> Detalleventa { get; set; } = new List<DetalleVentaDTO>();

    }
}
