using System;
using System.Collections.Generic;

namespace Ecommerce.Modelo;

public partial class Categoria
{
    public int Idcategoria { get; set; }

    public string? Nombre { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
