﻿using ObedientChild.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public interface ICoinHistoryService
    {
        Task<List<CoinHistory>> GetListAsync(int childId);

        Task<CoinHistory> GetByIdAsync(int id);
        Task RevertAsync(int id);
    }

}
