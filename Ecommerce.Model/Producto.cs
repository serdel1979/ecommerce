using System;
using System.Collections.Generic;

namespace Ecommerce.Modelo;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Idcategoria { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Preciooferta { get; set; }

    public int? Cantidad { get; set; }

    public string? Imagen { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Categoria? IdcategoriaNavigation { get; set; }
}
