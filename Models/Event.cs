using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public string Status { get; set; } = null!;

    public DateTime Startdatetime { get; set; }

    public DateTime Enddatetime { get; set; }

    public string? Description { get; set; }

    public int? Responsibleemployeeid { get; set; }

    public virtual Employee? Responsibleemployee { get; set; }

    public virtual Eventtype TypeNavigation { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
