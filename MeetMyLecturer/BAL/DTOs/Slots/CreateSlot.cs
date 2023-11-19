using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Slots
{
    public class CreateSlot
    {
        public int LecturerId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "Limit Booking is required.")]
        [RegularExpression("[0-9]*", ErrorMessage = "Limit Booking must be included digit 0-9.")]
        public int LimitBooking { get; set; }

        [Required(ErrorMessage = "Mode is required.")]
        public string Mode { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [ComparationDateValidation(ErrorMessage = "Date must be more than or equals today.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Start Time is required.")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Time is required.")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Repeat is required.")]
        public string Repeat { get; set; }
    }
}
