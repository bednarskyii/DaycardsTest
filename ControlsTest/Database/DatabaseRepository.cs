using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        }

        public async Task DeleteDayCardAsync(int id)
        {
            await database.Table<DayCardsModel>().Where(i => i.DayCardId == id).DeleteAsync();
        }

        public async Task DeleteDayCardByDateAsync(DateTime date)
        {
            await database.Table<DayCardsModel>().Where(i => i.Date == date).DeleteAsync();
        }

        public async Task<List<DayCardsModel>> GetDayCardsAsync(DateTime? date = null)
        {
            if (date != null)
            {
                var d = await database.Table<DayCardsModel>().Where(i => i.Date == date).ToListAsync();
                return d;
            }
            else
                return await database.Table<DayCardsModel>().ToListAsync();
        }

        public async Task SaveDayCardAsync(DayCardsModel item)
        {
            await database.InsertAsync(item);
        }
    }
}
