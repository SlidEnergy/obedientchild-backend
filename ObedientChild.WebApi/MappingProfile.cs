using System;
using System.Linq;
using AutoMapper;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
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


            CreateMap<Dto.User, ApplicationUser>()
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<ChildView, Dto.Child>()
                .ForMember(dest => dest.Avatar,
                    opt => opt.MapFrom(src => "data:image/png;base64," + Convert.ToBase64String(src.Avatar)));

            CreateMap<Child, Dto.Child>()
               .ForMember(dest => dest.Avatar,
                   opt => opt.MapFrom(src => "data:image/png;base64," + Convert.ToBase64String(src.Avatar)));

            CreateMap<DeedDto, Deed>()
                .ForMember(dest => dest.CharacterTraitDeeds, opt => opt.Ignore()) // Игнорируем связь, будем работать вручную
                .ForMember(dest => dest.CharacterTraitIds, opt => opt.Ignore()); // Игнорируем связь, будем работать вручную

            CreateMap<Deed, DeedDto>()
                .ForMember(dest => dest.CharacterTraitIds, opt => opt.MapFrom(src => src.CharacterTraitDeeds.Select(ct => ct.CharacterTraitId).ToList()));
        }
    }
}
