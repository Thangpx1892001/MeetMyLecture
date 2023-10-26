using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
