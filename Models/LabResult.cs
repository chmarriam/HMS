using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class LabResult
{
    public int ResultId { get; set; }

    public int? PatientId { get; set; }

    public int? TestId { get; set; }

    public DateOnly ResultDate { get; set; }

    public string? ResultValue { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual LabTest? Test { get; set; }
}
