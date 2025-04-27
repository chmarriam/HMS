using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class LabTest
{
    public int TestId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
}
