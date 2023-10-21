using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Requests
{
    public class UpdateRequest
    {
        public int StudentId { get; set; }

        public int LecturerId { get; set; }

        public int SubjectId { get; set; }

        public string Description { get; set; }
    }
}
