using ObedientChild.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObedientChild.Domain.LifeEnergy;

namespace ObedientChild.App
{
    public static class ApplicationDbContextExtensions
	{
		public static async Task<bool> IsAdminAsync(this IApplicationDbContext context, string userId)
		{
			var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == Role.Admin);

			if (role == null)
				throw new Exception("Роль администратора не найдена.");

			var userRole = await context.UserRoles.FirstOrDefaultAsync(x => x.RoleId == role.Id && x.UserId == userId);

			return userRole == null ? false : true;
		}

		public static async Task<LifeEnergyAccount> GetLifeEnergyAccountWithAccessCheckAsync(this IApplicationDbContext context, string userId)
		{
            return await context.Users
                .Where(x => x.Id == userId)
                .Join(context.TrusteeLifeEnergyAccounts, u => u.TrusteeId, t => t.TrusteeId, (u, t) => t)
                .Join(context.LifeEnergyAccounts, t => t.LifeEnergyAccountId, a => a.Id, (t, a) => a)
                .SingleOrDefaultAsync();
        }
    }
}
