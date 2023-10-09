using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Request
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int LecturerId { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account Lecturer { get; set; } = null!;

    public virtual Account Student { get; set; } = null!;
}
