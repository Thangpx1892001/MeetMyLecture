using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Slot
{
    public int Id { get; set; }

    public int LecturerId { get; set; }

    public string Title { get; set; } = null!;

    public string SubjectCode { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int LimitBooking { get; set; }

    public DateTime StartDatetime { get; set; }

    public DateTime EndDatetime { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Account Lecturer { get; set; } = null!;
}
