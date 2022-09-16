using AutoMapper;
using ObedientChild.Domain;
using ObedientChild.Infrastructure;
using ObedientChild.WebApi.Dto;

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
		}
	}
}
