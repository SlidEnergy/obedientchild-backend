using ObedientChild.Domain;

namespace ObedientChild.App
{
	public class DataAccessLayer
    {
		public IRepository<ApplicationUser, string> Users { get; }

		public IAuthTokensRepository AuthTokens { get; }

		public DataAccessLayer(
            IRepository<ApplicationUser, string> users,
			IAuthTokensRepository authTokens)
        {
            Users = users;
			AuthTokens = authTokens;
        }
    }
}
