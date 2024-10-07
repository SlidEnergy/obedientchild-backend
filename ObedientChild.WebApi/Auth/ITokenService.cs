using System.Threading.Tasks;
using ObedientChild.App;
using ObedientChild.Domain;

namespace ObedientChild.WebApi
{
	public interface ITokenService
	{
		Task<GoogleAccessTokenResponse> CreateAccessAndRefreshTokensAsync(ApplicationUser user, AccessMode accessMode);
		Task<string> CreateTokenAsync(string userId, AuthTokenType type);
		//Task<TokensCortage> RefreshImportToken(string refreshToken);
		Task SetTokenAsync(string userId, string token, AuthTokenType type);

        Task<GoogleAccessTokenResponse> RefreshTokenAsync(string token, string refreshToken);
		Task<GoogleAccessTokenResponse> CheckCredentialsAndGetTokenAsync(string email, string password);
	}
}