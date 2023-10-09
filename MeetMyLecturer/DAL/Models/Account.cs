using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Role { get; set; } = null!;

    public virtual Booking? Booking { get; set; }

    public virtual ICollection<Request> RequestLecturers { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestStudents { get; set; } = new List<Request>();

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
