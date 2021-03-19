using AutoMapper;
using Homework_4.Blog.Domain.Entities;
using Homework_4.Blog.Domain.Models;
using static System.String;

namespace Homework_4.Blog.Domain.Mapping_Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<PostDto, Post>();
            CreateMap<Post, PostDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => Concat(src.Firstname,src.Lastname)));
        }
    }
}