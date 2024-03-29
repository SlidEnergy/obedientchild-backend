﻿using ObedientChild.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
	public interface IRepositoryWithAccessCheck<T> : IRepository<T, int> where T : class, IUniqueObject<int>
	{
		Task<List<T>> GetListWithAccessCheck(string userId);
	}
}
