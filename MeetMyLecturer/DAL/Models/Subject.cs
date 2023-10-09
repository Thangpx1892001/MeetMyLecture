using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string SubjectCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Account> Lecturers { get; set; } = new List<Account>();
}
