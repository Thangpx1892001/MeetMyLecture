using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SlotId { get; set; }

    public int SubjectId { get; set; }

    public string Description { get; set; } = null!;

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Slot Slot { get; set; } = null!;

    public virtual Account Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
