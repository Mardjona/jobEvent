using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Resume
{
    public int Id { get; set; }

    public string Candidatename { get; set; } = null!;

    public string Direction { get; set; } = null!;

    public DateOnly Applicationdate { get; set; }

    public string? Resumetext { get; set; }
}
