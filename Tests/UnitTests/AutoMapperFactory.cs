using AutoMapper;
using ObedientChild.WebApi;
using ObedientChild.Infrastructure;

namespace ObedientChild.UnitTests
{
    public class AutoMapperFactory
    {
        public IMapper Create(ApplicationDbContext context)
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile(context)));
            return new Mapper(configuration);
        }
    }
}
