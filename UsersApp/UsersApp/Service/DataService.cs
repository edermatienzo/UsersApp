using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersApp.Model.Local;

namespace UsersApp.Service
{
    /// <summary>
    /// DataService Object include all methods to manipulate SQLite database
    /// </summary>
    public class DataService
    {

        readonly SQLiteAsyncConnection database;

        public DataService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

        }

        /// <summary>
        /// Initialize database
        /// </summary>
        public void Init()
        {
            database.CreateTableAsync<User>().Wait();
        }
        /// <summary>
        /// Initialize database
        /// </summary>
        public async Task ResetUserAsync()
        {
            await database.DropTableAsync<User>();
            await database.CreateTableAsync<User>();
        }
        /// <summary>
        /// Get all users in table User
        /// </summary>
        public Task<List<User>> GetUsersAsync()
        {
            return database.Table<User>().ToListAsync();
        }

        /// <summary>
        /// Insert a record in table User
        /// </summary>
        public Task<int> InsertUserAsync(User user)
        {
            return database.InsertAsync(user);
        }

    }
}
