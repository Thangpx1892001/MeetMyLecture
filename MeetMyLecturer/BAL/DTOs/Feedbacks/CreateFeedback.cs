﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Feedbacks
{
    public class CreateFeedback
    {
        public int BookingId { get; set; }

        public int Star { get; set; }

        public string Comment { get; set; }
    }
}
