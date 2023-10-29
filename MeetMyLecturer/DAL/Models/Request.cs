using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Request
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int LecturerId { get; set; }

    public int SubjectId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StartDatetime { get; set; }

    public DateTime EndDatetime { get; set; }

    public virtual Account Lecturer { get; set; } = null!;

    public virtual Account Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
