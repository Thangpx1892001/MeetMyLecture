using AutoMapper;
using BAL.DTOs.Accounts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, GetAccount>()/*.ForMember(dept => dept.SubjectName, opts => opts.MapFrom(src => src.Subjects.Select(s => s.Name)))*/.ReverseMap();
        }
    }
}
