﻿using AutoMapper;
using BAL.DTOs.Slots;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class SlotProfile : Profile
    {
        public SlotProfile()
        {
            CreateMap<Slot, GetSlot>().ForMember(dept => dept.BookingId, opts => opts.MapFrom(src => src.Bookings.Select(s => s.Id))).ReverseMap();
        }
    }
}
