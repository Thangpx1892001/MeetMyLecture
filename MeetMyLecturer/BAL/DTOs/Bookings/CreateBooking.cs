using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Bookings
{
    public class CreateBooking
    {
        public int StudentId { get; set; }

        public int SlotId { get; set; }

        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string Status { get; set; }
    }
}
