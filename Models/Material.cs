using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Approvaldate { get; set; }

    public DateOnly Modificationdate { get; set; }

    public string Status { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Area { get; set; } = null!;

    public int? Authorid { get; set; }

    public virtual Employee? Author { get; set; }
}
