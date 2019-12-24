using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ControlsTest.DaycardsModels;
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
                    await database.InsertAsync(new AccomplishmentDaycardModel { Date = date});
                    break;
                case DaycardType.Cost:
                    await database.InsertAsync(new CostDaycardModel { Date = date});
                    break;
                case DaycardType.Equipment:
                    await database.InsertAsync(new EquipmentDaycardModel { Date = date});
                    break;
                case DaycardType.Labor:
                    await database.InsertAsync(new LaborDaycardModel { Date = date});
                    break;
                case DaycardType.Material:
                    await database.InsertAsync(new MaterialDaycardModel { Date = date});
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
    }
}
