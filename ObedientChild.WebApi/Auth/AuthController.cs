using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.Infrastructure.GoogleAuth;
using Slid.Auth.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.WebApi.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GoogleAuthOptions _options;
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;
        private readonly IGoogleTokenService _googleTokenService;

        public AuthController(GoogleAuthOptions options, IUsersService usersService, ITokenService tokenService, IGoogleTokenService googleTokenService)
        {
            _options = options;
            _usersService = usersService;
            _tokenService = tokenService;
            _googleTokenService = googleTokenService;
        }

        [HttpGet("google")]
        [Authorize(Policy = Policy.MustBeAllOrRestrictedAccessMode)]
        public async Task<IActionResult> GoogleLogin()
        {
            var userId = User.GetUserId();

            var token = await _tokenService.CreateTokenAsync(userId, Domain.AuthTokenType.ApiKey);

            string stateToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var scopes = "https://www.googleapis.com/auth/calendar";
            var url = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_options.ClientId}&redirect_uri={_options.RedirectUrl}&response_type=code&scope={Uri.EscapeDataString(scopes)}&access_type=offline&state={stateToken}";
            return Redirect(url);
        }

        [HttpPost("google/refreshToken")]
        [Authorize]
        public async Task<ActionResult<string>> GoogleCallback(GoogleAccessTokenResponse tokens)
        {
            var googleAccessToken = await _googleTokenService.RefreshAccessTokenAsync(tokens.RefreshToken);

            return Ok(googleAccessToken);
        }

        [HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback(string code, string state)
        {
            var response = await _googleTokenService.GetAccessTokenAsync(code);

            var token = Encoding.UTF8.GetString(Convert.FromBase64String(state));
            var user = await _usersService.GetByAuthTokenAsync(token, Domain.AuthTokenType.ApiKey);

            await _tokenService.SetTokenAsync(user.Id, token, Domain.AuthTokenType.GoogleAccessToken);

            // Редирект на домашнюю страницу или страницу после логина
            return Redirect($"{_options.FrontendRedirectUrl}?googleAccessToken={response.Token}&googleRefreshToken={response.RefreshToken}&expiresIn={response.ExpiresIn}");
        }
    }
}
