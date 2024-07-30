using ObedientChild.Domain;
using ObedientChild.Domain.Habbits;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App.Habbits
{
    public interface IHabbitsService
    {
        Task AddAsync(Habbit model);
        Task SetForChildAsync(int childId, int habbitId);
        Task UnsetForChildAsync(int id, int childId);
        Task DeleteAsync(int id);
        Task<Habbit> GetByIdAsync(int id);
        Task<List<Habbit>> GetListAsync();
        Task<List<DayHabbit>> GetListForDayAsync(int childId, DateOnly day);
        Task<Habbit> UpdateAsync(Habbit model);

        Task<HabbitHistory> SetStatusAsync(int habbitId, int childId, DateOnly day, HabbitHistoryStatus status);
        Task<WeekHabbitStatistic> GetStatisticsAsync(int childId, DateOnly startDay, DateOnly endDay);
    }
}