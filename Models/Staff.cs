using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string Name { get; set; } = null!;

    public string? Role { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
