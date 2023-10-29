using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Account> Lecturers { get; set; } = new List<Account>();
}
