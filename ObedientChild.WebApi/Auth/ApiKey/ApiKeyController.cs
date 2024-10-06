using ObedientChild.App;
using ObedientChild.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Threading.Tasks;
using Slid.Auth.Core;

namespace ObedientChild.WebApi
{

    [Route("api/v1/[controller]")]
	[ApiController]
	public class ApiKeysController : ControllerBase
	{
		private readonly ITokenService _tokenService;
		private readonly IUsersService _usersService;

		public ApiKeysController(ITokenService tokenService, IUsersService usersService)
		{
			_tokenService = tokenService;
			_usersService = usersService;
		}

		[Authorize(Policy = Policy.MustBeAllAccessMode)]
		[HttpPost()]
		[ProducesResponseType(200)]
		public async Task<ActionResult<string>> GetApiKey()
		{
			var userId = User.GetUserId();

			return await _tokenService.CreateTokenAsync(userId, AuthTokenType.ApiKey);
		}
	}
}
