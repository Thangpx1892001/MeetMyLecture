using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Notifications
{
    public class UpdateNotification
    {
        public int BookingId { get; set; }
        public bool IsRead { get; set; }
    }
}
