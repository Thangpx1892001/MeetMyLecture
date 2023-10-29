using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Feedbacks
{
    public class CreateFeedback
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        public string Comment { get; set; }
    }
}
