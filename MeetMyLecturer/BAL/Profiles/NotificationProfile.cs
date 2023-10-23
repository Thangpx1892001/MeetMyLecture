using AutoMapper;
using BAL.DTOs.Notifications;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile() 
        {
            CreateMap<Notification, GetNotification>().ForMember(dept => dept.StudentId, opts => opts.MapFrom(src => src.Booking.StudentId))
                                                      .ForMember(dept => dept.LecturerId, opts => opts.MapFrom(src => src.Booking.Slot.LecturerId)).ReverseMap();
        }
    }
}
