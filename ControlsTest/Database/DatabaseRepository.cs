using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ControlsTest.DaycardsModels;
using ControlsTest.DaycardsViewModels;
using ControlsTest.Enums;
using ControlsTest.Models;
using SQLite;

namespace ControlsTest.Database
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly SQLiteAsyncConnection database;
        private readonly string path;

        public DatabaseRepository()
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DayCards.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<DayCardsModel>().Wait();

            database.CreateTableAsync<AccomplishmentDaycardModel>().Wait();
            database.CreateTableAsync<CostDaycardModel>().Wait();
            database.CreateTableAsync<EquipmentDaycardModel>().Wait();
            database.CreateTableAsync<LaborDaycardModel>().Wait();
            database.CreateTableAsync<MaterialDaycardModel>().Wait();
        }

        public async Task DeleteDayCardAsync(int id)
        {
            await database.Table<DayCardsModel>().Where(i => i.DayCardId == id).DeleteAsync();
        }

        public async Task DeleteDayCardByDateAsync(DateTime date)
        {
            await database.Table<DayCardsModel>().Where(i => i.Date == date).DeleteAsync();
        }

        public async Task<int> GetCountOfCheckedDaycardsAsync(DateTime date, bool isValid)
        {
            if(isValid)
                return await database.Table<DayCardsModel>().Where(i => i.IsValid == true && i.Date == date).CountAsync();
            else
                return await database.Table<DayCardsModel>().Where(i => i.IsValid == false && i.Date == date).CountAsync();
        }

        public async Task<List<DayCardsModel>> GetDayCardsAsync(DateTime? date = null)
        {
            if (date != null)
            {
                return await database.Table<DayCardsModel>().Where(i => i.Date == date).ToListAsync();
            }
            else
                return await database.Table<DayCardsModel>().ToListAsync();
        } 

        public async Task SaveDayCardAsync(DayCardsModel item)
        {
            await database.InsertAsync(item);
        }

        //new daycards
        public async Task SaveDaycardAsync(DaycardType type, DateTime date)
        {
            switch (type)
            {
                case DaycardType.Accomplishment:
                    await database.InsertAsync(new AccomplishmentDaycardModel { Date = date, Title = "MockTitle"});
                    break;
                case DaycardType.Cost:
                    await database.InsertAsync(new CostDaycardModel { Date = date, Title = "MockTitle" });
                    break;
                case DaycardType.Equipment:
                    await database.InsertAsync(new EquipmentDaycardModel { Date = date, Title = "MockTitle" });
                    break;
                case DaycardType.Labor:
                    await database.InsertAsync(new LaborDaycardModel { Date = date, TimeReportingCode = TimeReportingCode.Code01, Title = "MockTitle" });
                    break;
                case DaycardType.Material:
                    await database.InsertAsync(new MaterialDaycardModel { Date = date, Title = "MockTitle" });
                    break;
            }
        }

        public async Task<List<AccomplishmentDaycardModel>> GetAccomplishmentDaycardsByDateAsync(DateTime date)
        {
            return await database.Table<AccomplishmentDaycardModel>().Where(i => i.Date == date).ToListAsync();
        }
        public async Task<List<CostDaycardModel>> GetCostsDaycardsByDateAsync(DateTime date)
        {
            return await database.Table<CostDaycardModel>().Where(i => i.Date == date).ToListAsync();
        }
        public async Task<List<EquipmentDaycardModel>> GetEquipmentDaycardsByDateAsync(DateTime date)
        {
            return await database.Table<EquipmentDaycardModel>().Where(i => i.Date == date).ToListAsync();
        }
        public async Task<List<LaborDaycardModel>> GetLaborDaycardsByDateAsync(DateTime date)
        {
            return await database.Table<LaborDaycardModel>().Where(i => i.Date == date).ToListAsync();
        }
        public async Task<List<MaterialDaycardModel>> GetMaterialDaycardsByDateAsync(DateTime date)
        {
            return await database.Table<MaterialDaycardModel>().Where(i => i.Date == date).ToListAsync();
        }

        public async Task DeleDaycardByDateAsync(DaycardType type, DateTime date)
        {
            switch (type)
            {
                case DaycardType.Accomplishment:
                    await database.Table<AccomplishmentDaycardModel>().Where(i => i.Date == date).DeleteAsync();
                    break;
                case DaycardType.Cost:
                    await database.Table<CostDaycardModel>().Where(i => i.Date == date).DeleteAsync();
                    break;
                case DaycardType.Equipment:
                    await database.Table<EquipmentDaycardModel>().Where(i => i.Date == date).DeleteAsync();
                    break;
                case DaycardType.Labor:
                    await database.Table<LaborDaycardModel>().Where(i => i.Date == date).DeleteAsync();
                    break;
                case DaycardType.Material:
                    await database.Table<MaterialDaycardModel>().Where(i => i.Date == date).DeleteAsync();
                    break;
            }
        }

        public async Task SaveUpdatedDaycardAsync(DaycardType type, object daycardItem)
        {
            switch (type)
            {
                case DaycardType.Accomplishment:
                    var model = (AccomplishmentDaycardViewModel)daycardItem;
                    var newModel = new AccomplishmentDaycardModel { Id = model.IdDaycard, Date = model.DayUrl.Date, Quantity = model.Quantity, Title = model.Title};
                    await database.InsertAsync(newModel);
                    break;
                case DaycardType.Cost:
                    var modelCost = (CostDaycardViewModel)daycardItem;
                    var newModelCost = new CostDaycardModel { Id = modelCost.IdDaycard, Date = modelCost.DayUrl.Date, Title = modelCost.Title, TotalCost = modelCost.TotalCost };
                    await database.InsertAsync(newModelCost);
                    break;
                case DaycardType.Equipment:
                    var modelEquipment = (EquipmentDaycardViewModel)daycardItem;
                    var newModelEquipment = new EquipmentDaycardModel { Id = modelEquipment.IdDaycard, Date = modelEquipment.DayUrl.Date, Hours = modelEquipment.Hours, Miles = modelEquipment.Miles, Operator = modelEquipment.Operator, Title = modelEquipment.Title};
                    await database.InsertAsync(newModelEquipment);
                    break;                     
                case DaycardType.Labor:
                    var modelLabor = (LaborDaycardViewModel)daycardItem;
                    var newModelLabor = new LaborDaycardModel { Id = modelLabor.IdDaycard, Date = modelLabor.DayUrl.Date, Title = modelLabor.Title, Hours = modelLabor.Hours, TimeReportingCode = modelLabor.TimeReportingCode };
                    await database.InsertAsync(newModelLabor);
                    break;                     
                case DaycardType.Material:
                    var modelMaterial = (MaterialDaycardViewModel)daycardItem;
                    var newModelMaterial = new MaterialDaycardModel { Id = modelMaterial.IdDaycard, Date = modelMaterial.DayUrl.Date, Title = modelMaterial.Title, Quantity = modelMaterial.Quantity };
                    await database.InsertAsync(newModelMaterial);
                    break;                     
            }
        }

        public async Task<List<IDaycard>> GetDaycardsByDateAndTypeAsync(DaycardType type, DateTime date)
        {
            switch (type)
            {
                case DaycardType.Accomplishment:
                    return new List<IDaycard>(await database.Table<AccomplishmentDaycardModel>().Where(i => i.Date == date).ToListAsync());
                case DaycardType.Cost:
                    return new List<IDaycard>(await database.Table<CostDaycardModel>().Where(i => i.Date == date).ToListAsync());
                case DaycardType.Equipment:
                    return new List<IDaycard>(await database.Table<EquipmentDaycardModel>().Where(i => i.Date == date).ToListAsync());
                case DaycardType.Labor:
                    return new List<IDaycard>(await database.Table<LaborDaycardModel>().Where(i => i.Date == date).ToListAsync());
                case DaycardType.Material:
                    return new List<IDaycard>(await database.Table<MaterialDaycardModel>().Where(i => i.Date == date).ToListAsync());
            }

            return null;
        }

    }
}
