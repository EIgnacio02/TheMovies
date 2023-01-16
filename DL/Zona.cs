using System;
using System.Collections.Generic;

namespace DL;

public partial class Zona
{
    public int IdZona { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Cine> Cines { get; } = new List<Cine>();
}
