﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Data.Identity;
using Web.Cars.Models;

namespace Web.Cars.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<AppUser, UserItemViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x=> "/images/" + x.Photo));
        }
    }
}
