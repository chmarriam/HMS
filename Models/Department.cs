using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
