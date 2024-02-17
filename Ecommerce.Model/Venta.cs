using System;
using System.Collections.Generic;

namespace Ecommerce.Modelo;

public partial class Venta
{
    public int Idventa { get; set; }

    public int? Idusuario { get; set; }

    public decimal? Total { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Usuario? IdusuarioNavigation { get; set; }
}
