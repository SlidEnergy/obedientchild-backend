using Microsoft.AspNetCore.Identity;
using ObedientChild.Domain;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ObedientChild.App
{
	public class AuthTokenService : IAuthTokenService
	{
		private readonly IApplicationDbContext _context;

		public AuthTokenService(IApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<AuthToken> GetTokenAsync(string userId, AuthTokenType type)
        {
            return await _context.AuthTokens.SingleOrDefaultAsync(x => x.UserId == userId && x.Type == type);
        }

        public async Task AddOrUpdateTokenAsync(string userId, string token, AuthTokenType type)
		{
			var user = await _context.Users.FindAsync(userId);

			var existToken = await _context.AuthTokens.SingleOrDefaultAsync(x => x.UserId == userId && x.Token == token && x.Type == type);

			if (existToken == null)
			{
				_context.AuthTokens.Add(new AuthToken("any", token, type) { User = user});
				await _context.SaveChangesAsync();
			}
			else
			{
				await UpdateToken(existToken, token);
			}
		}

		public async Task<AuthToken> FindAnyTokenAsync(string userId, string token, AuthTokenType type)
		{
			return await _context.AuthTokens.SingleOrDefaultAsync(x => x.UserId == userId && x.Token == token && x.Type == type);
		}

		public async Task<AuthToken> UpdateToken(AuthToken oldToken, string newToken)
		{
			oldToken.Update("any", newToken);
            _context.Entry(oldToken).State = EntityState.Modified;
            await _context.SaveChangesAsync();

			return oldToken;
		}
	}
}
