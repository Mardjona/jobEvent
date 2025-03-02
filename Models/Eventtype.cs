using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Eventtype
{
    public int EventtypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
