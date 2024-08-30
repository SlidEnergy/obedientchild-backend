using ObedientChild.Domain;
using ObedientChild.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App.Habits
{
    public interface IChildTasksService
    {
        Task AddAsync(ChildTask model);
        Task DeleteAsync(int id);
        Task<ChildTask> GetByIdAsync(int id);
        Task<List<ChildTask>> GetListAsync();
        Task<List<ChildTask>> GetListForDayAsync(int childId, DateOnly day);
        Task<ChildTask> SetStatusAsync(int id, int childId, ChildTaskStatus status);
        Task<ChildTask> UpdateAsync(ChildTask model);
    }
}