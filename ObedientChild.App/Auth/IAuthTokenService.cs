using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
	public interface IAuthTokenService
	{
		Task<AuthToken> GetTokenAsync(string userId, AuthTokenType type);
        Task AddOrUpdateTokenAsync(string userId, string token, AuthTokenType type);
		Task<AuthToken> FindAnyTokenAsync(string userId, string token, AuthTokenType type);
		Task<AuthToken> UpdateToken(AuthToken oldToken, string newToken);
	}
}