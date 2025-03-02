using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Training
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
