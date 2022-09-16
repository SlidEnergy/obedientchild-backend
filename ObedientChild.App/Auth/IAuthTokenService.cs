using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
	public interface IAuthTokenService
	{
		Task AddToken(string userId, string token, AuthTokenType type);
		Task<AuthToken> FindAnyToken(string token);
		Task<AuthToken> UpdateToken(AuthToken oldToken, string newToken);
	}
}