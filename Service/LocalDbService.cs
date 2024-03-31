using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Service
{
    public class LocalDbService
    {
        private const string DB_NAME = "data_learn.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection =new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));        
        }
    }
}
