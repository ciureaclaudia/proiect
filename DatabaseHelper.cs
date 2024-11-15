using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect1
{
    // handles SQLite operations: nserting, retrieving, and deleting data.
    public class DatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MainPageDestinatii>().Wait();
        }

        public Task<List<MainPageDestinatii>> GetDestinationsAsync()
        {
            return _database.Table<MainPageDestinatii>().ToListAsync();
        }

        public Task<int> SaveDestinationAsync(MainPageDestinatii destination)
        {
            return _database.InsertAsync(destination);
        }

        public Task<int> DeleteDestinationAsync(MainPageDestinatii destination)
        {
            return _database.DeleteAsync(destination);
        }
    }


}
