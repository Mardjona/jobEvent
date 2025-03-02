using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Absence
{
    public int Id { get; set; }

    public int Employeeid { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public int Type { get; set; }

    public int? Substituteemployeeid { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee? Substituteemployee { get; set; }

    public virtual Absencestype TypeNavigation { get; set; } = null!;
}
