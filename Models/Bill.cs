using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public int? PatientId { get; set; }

    public DateOnly BillDate { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual Patient? Patient { get; set; }
}
