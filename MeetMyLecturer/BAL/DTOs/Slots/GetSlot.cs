using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Slots
{
    public class GetSlot
    {
        [Key]
        public int Id { get; set; }

        public int LecturerId { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string Code { get; set; }

        public int LimitBooking { get; set; }

        public string Mode { get; set; }

        public DateTime StartDatetime { get; set; }

        public DateTime EndDatetime { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }
}
