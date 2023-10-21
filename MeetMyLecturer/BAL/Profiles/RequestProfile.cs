using AutoMapper;
using BAL.DTOs.Requests;
using BAL.DTOs.Slots;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile() 
        {
            CreateMap<Request, GetRequest>().ReverseMap();
        }
    }
}
