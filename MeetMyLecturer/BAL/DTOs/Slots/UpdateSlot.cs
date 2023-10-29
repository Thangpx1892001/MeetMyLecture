﻿using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Slots
{
    public class UpdateSlot
    {
        public int LecturerId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "Limit Booking is required.")]
        public int LimitBooking { get; set; }

        [Required(ErrorMessage = "Mode is required.")]
        public string Mode { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Start Time is required.")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Time is required.")]
        public DateTime EndDateTime { get; set; }
    }
}
