using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Requests
{
    public class GetRequest
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int LecturerId { get; set; }

        public int SubjectId { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }
}
