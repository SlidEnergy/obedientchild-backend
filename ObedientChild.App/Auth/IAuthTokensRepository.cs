using ObedientChild.Domain;
using System.Threading.Tasks;

namespace ObedientChild.App
{
	public interface IAuthTokensRepository: IRepository<AuthToken, int>
	{
		Task<AuthToken> FindAnyToken(string token);
	}
}
