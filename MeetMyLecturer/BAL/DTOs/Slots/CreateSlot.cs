﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Slots
{
    public class CreateSlot
    {
        public int LecturerId { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public int LimitBooking { get; set; }

        public string Mode { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}