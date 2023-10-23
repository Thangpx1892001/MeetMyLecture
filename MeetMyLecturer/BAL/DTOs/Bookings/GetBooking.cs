using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Bookings
{
    public class GetBooking
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int SlotId { get; set; }

        public int LecturerId { get; set; }

        public int SubjectId { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }
}
