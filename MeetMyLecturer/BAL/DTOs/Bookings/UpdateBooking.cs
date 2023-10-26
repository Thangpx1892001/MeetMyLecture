﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Bookings
{
    public class UpdateBooking
    {
        public int StudentId { get; set; }

        public int SlotId { get; set; }

        public int SubjectId { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
    }
}