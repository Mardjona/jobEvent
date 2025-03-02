using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Kabinet
{
    public string? Name { get; set; }

    public int KabinetId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
