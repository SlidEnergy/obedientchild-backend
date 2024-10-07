using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ObedientChild.App;
using ObedientChild.Domain;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Slid.Auth.Core;

namespace ObedientChild.WebApi
{
	public class TokenService : ITokenService
	{
		private readonly ITokenGenerator _tokenGenerator;
		private readonly AuthSettings _authSettings;
		private IAuthTokenService _authTokenService;
		private readonly UserManager<ApplicationUser> _userManager;

		public TokenService(ITokenGenerator tokenGenerator, AuthSettings authSettings, IAuthTokenService authTokenService, UserManager<ApplicationUser> userManager)
		{
			_tokenGenerator = tokenGenerator;
			_authSettings = authSettings;
			_authTokenService = authTokenService;
			_userManager = userManager;
		}

		//public async Task<TokensCortage> RefreshImportToken(string refreshToken)
		//{
		//	var savedToken = await _authTokenService.FindAnyToken(refreshToken);

		//	if (savedToken == null || savedToken.Type != AuthTokenType.ImportToken)
		//		throw new SecurityTokenException("Invalid refresh token");

		//	var roles = await _userManager.GetRolesAsync(savedToken.User);

		//	var newToken = _tokenGenerator.GenerateAccessToken(savedToken.User, roles, AccessMode.Import);
		//	var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

		//	await _authTokenService.UpdateToken(savedToken, newRefreshToken);

		//	return new TokensCortage() { Token = newToken, RefreshToken = newRefreshToken };
		//}

		public async Task<GoogleAccessTokenResponse> RefreshTokenAsync(string token, string refreshToken)
		{
			var principal = GetPrincipalFromExpiredToken(token);
			var userId = principal.GetUserId();
			var savedToken = await _authTokenService.FindAnyTokenAsync(userId, refreshToken, AuthTokenType.RefreshToken);

			if (savedToken == null)
				throw new SecurityTokenException("Invalid refresh token");

			var newToken = _tokenGenerator.GenerateAccessToken(principal.Claims);
			var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

			await _authTokenService.UpdateToken(savedToken, newRefreshToken);

			return new GoogleAccessTokenResponse() { Token = newToken, RefreshToken = newRefreshToken };
		}

		public async Task<GoogleAccessTokenResponse> CreateAccessAndRefreshTokensAsync(ApplicationUser user, AccessMode accessMode)
		{
			var refreshToken = _tokenGenerator.GenerateRefreshToken();
			await _authTokenService.AddOrUpdateTokenAsync(user.Id, refreshToken, AuthTokenType.RefreshToken);

			var roles = await _userManager.GetRolesAsync(user);

			return new GoogleAccessTokenResponse()
			{
				Token = _tokenGenerator.GenerateAccessToken(user, roles, accessMode),
				RefreshToken = refreshToken
			};
		}

		public async Task<string> CreateTokenAsync(string userId, AuthTokenType type)
		{
			var importToken = _tokenGenerator.GenerateRefreshToken();
			await _authTokenService.AddOrUpdateTokenAsync(userId, importToken, type);

			return importToken;
		}

		public async Task SetTokenAsync(string userId, string token, AuthTokenType type)
		{
			await _authTokenService.AddOrUpdateTokenAsync(userId, token, type);
		}

		private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = _authSettings.GetSymmetricSecurityKey(),
				ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
			var jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");

			return principal;
		}

		public async Task<GoogleAccessTokenResponse> CheckCredentialsAndGetTokenAsync(string email, string password)
		{
			var user = await _userManager.FindByNameAsync(email);

			if (user == null)
				throw new AuthenticationException();

			var checkResult = await _userManager.CheckPasswordAsync(user, password);

			if (!checkResult)
				throw new AuthenticationException();

			return await CreateAccessAndRefreshTokensAsync(user, AccessMode.All);
		}
	}
}
