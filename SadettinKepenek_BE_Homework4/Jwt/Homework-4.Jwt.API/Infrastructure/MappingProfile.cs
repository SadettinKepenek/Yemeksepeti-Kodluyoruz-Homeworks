using AutoMapper;
using Homework_4.Jwt.API.Entities;
using Homework_4.Jwt.API.Models;
using Homework_4.Jwt.API.Models.Derived;

namespace Homework_4.Jwt.API.Infrastructure
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomEntity, Room>()
                .ForMember(dest => dest.Rate, opt =>
                    opt.MapFrom(scr => scr.Rate / 100));

            CreateMap<UserEntity, UserInfo>()
                .ForMember(desc => desc.FullName, opt =>
                    opt.MapFrom(scr => string.Concat(scr.Name, scr.SurName)));
        }
    }
}