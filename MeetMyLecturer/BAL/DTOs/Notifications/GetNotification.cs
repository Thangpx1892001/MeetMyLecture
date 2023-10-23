using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Notifications
{
    public class GetNotification
    {
        [Key]
        public int Id { get; set; }

        public int BookingId { get; set; }

        public int StudentId { get; set; }

        public int LecturerId { get; set; }

        public string Title { get; set; }

        public DateTime Time { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
