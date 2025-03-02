using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Absencestype
{
    public int TypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();
}
