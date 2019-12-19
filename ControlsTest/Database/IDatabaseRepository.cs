using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlsTest.Models;

namespace ControlsTest.Database
{
    public interface IDatabaseRepository
    {
        Task<List<DayCardsModel>> GetDayCardsAsync(DateTime? date = null);
        Task SaveDayCardAsync(DayCardsModel item);
        Task DeleteDayCardAsync(int id);
        Task DeleteDayCardByDateAsync(DateTime id);
    }
}
