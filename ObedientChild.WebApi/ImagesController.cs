using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.Infrastructure.SearchImages;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISearchImageService _searchImageService;

        public ImagesController(IMapper mapper, ISearchImageService searchImageService)
        {
            _mapper = mapper;
            _searchImageService = searchImageService;
        }

        [HttpGet("search")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<string>>> GetList(string q)
        {
            var result = await _searchImageService.SearchAsync(q);

            return result.Images_results.Select(x => x.Thumbnail).ToArray();
        }
    }
}
