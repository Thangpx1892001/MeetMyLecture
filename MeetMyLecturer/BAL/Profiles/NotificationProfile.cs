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
            CreateMap<Notification, GetNotification>().ForMember(dept => dept.Booking, opts => opts.MapFrom(src => src.Booking))
                                                      .ForMember(dept => dept.Slot, opts => opts.MapFrom(src => src.Booking.Slot)).ReverseMap();
        }
    }
}
