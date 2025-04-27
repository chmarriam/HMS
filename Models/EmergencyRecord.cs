using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class EmergencyRecord
{
    public int RecordId { get; set; }

    public int? PatientId { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string? Complaint { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public virtual Patient? Patient { get; set; }
}
