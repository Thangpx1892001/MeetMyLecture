using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Subjects
{
    public class GetSubject
    {
        [Key]
        public int Id { get; set; }

        public string SubjectCode { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Status { get; set; }
    }
}
