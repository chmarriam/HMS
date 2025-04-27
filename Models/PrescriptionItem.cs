using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class PrescriptionItem
{
    public int ItemId { get; set; }

    public int? PrescriptionId { get; set; }

    public int? MedicationId { get; set; }

    public int? Quantity { get; set; }

    public string? Instructions { get; set; }

    public virtual Medication? Medication { get; set; }

    public virtual Prescription? Prescription { get; set; }
}
