using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Bookings
{
    public class CreateByCode
    {
        public int StudentId { get; set; }

        public int SlotId { get; set; }

        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
    }
}
