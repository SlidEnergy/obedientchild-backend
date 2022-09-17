using System;
using AutoMapper;
using ObedientChild.Domain;
using ObedientChild.Infrastructure;

namespace ObedientChild.WebApi
{
    public class MappingProfile : Profile
	{
        public MappingProfile(ApplicationDbContext context)
        {
            CreateMap<ApplicationUser, Dto.User>()
                .ForMember(dest => dest.IsAdmin,
                    opt => opt.Ignore());


            CreateMap<Dto.User, ApplicationUser>();

            CreateMap<Child, Dto.Child>()
                .ForMember(dest => dest.Avatar,
                    opt => opt.MapFrom(src => "data:image/png;base64," + Convert.ToBase64String(src.Avatar)));
        }
    }
}
