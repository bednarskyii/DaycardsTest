using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlsTest.DaycardsModels;
using ControlsTest.Enums;
using ControlsTest.Models;

namespace ControlsTest.Database
{
    public interface IDatabaseRepository
    {
        Task<List<DayCardsModel>> GetDayCardsAsync(DateTime? date = null);
        Task SaveDayCardAsync(DayCardsModel item);
        Task DeleteDayCardAsync(int id);
        Task DeleteDayCardByDateAsync(DateTime id);
        Task<int> GetCountOfCheckedDaycardsAsync(DateTime date, bool isValid);

        Task SaveDaycardAsync(DaycardType type, DateTime date);

        Task<List<AccomplishmentDaycardModel>> GetAccomplishmentDaycardsByDateAsync(DateTime date);
        Task<List<CostDaycardModel>> GetCostsDaycardsByDateAsync(DateTime date);
        Task<List<EquipmentDaycardModel>> GetEquipmentDaycardsByDateAsync(DateTime date);
        Task<List<LaborDaycardModel>> GetLaborDaycardsByDateAsync(DateTime date);
        Task<List<MaterialDaycardModel>> GetMaterialDaycardsByDateAsync(DateTime date);

        Task DeleDaycardByDateAsync(DaycardType type, DateTime date);

        Task SaveUpdatedDaycardAsync(DaycardType type, object daycardItem);
    }
}
