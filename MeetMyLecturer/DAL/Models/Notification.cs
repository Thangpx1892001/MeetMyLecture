﻿using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int? SendToId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? Time { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
