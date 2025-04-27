using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Admission
{
    public int AdmissionId { get; set; }

    public int? PatientId { get; set; }

    public DateOnly AdmissionDate { get; set; }

    public DateOnly? DischargeDate { get; set; }

    public int? BedNumber { get; set; }

    public int? DoctorId { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
