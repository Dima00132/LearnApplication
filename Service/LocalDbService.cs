
using LearnApplication.Model;
using SQLite;

using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Service
{
    public class LocalDbService
    {
        private const string DB_NAME = "data_learn_save_6.db3";
        private readonly SQLiteConnection _connection;
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create    |
            SQLiteOpenFlags.SharedCache;

        public LocalDbService()
        {
          
            //File.Delete(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME), Flags);
            _ = _connection.CreateTable<LearnCategory>();
            _ = _connection.CreateTable<LearnQuestion>();
        }

        public  List<LearnCategory> GetLearn()
            =>_connection.GetAllWithChildren<LearnCategory>(recursive: true);


        public void Create<T>(T learnCategory)
        {
            _connection.InsertWithChildren(learnCategory); 
        }

        public T UpdateAndGetById<T>(int id,T update) where T : IDataSelect, new()
        {
            Update(update);
            return _connection.GetAllWithChildren<T>().Where(x => x.Id == id).FirstOrDefault();
        }

        public T GetById<T>(int id) where T : IDataSelect, new()
            =>_connection.GetAllWithChildren<T>().Where(x => x.Id == id).FirstOrDefault(); 
        
        
        public void Update<T>(T value)=>_connection.UpdateWithChildren(value);
        
        public void DeleteAll()
        {
            var learn = GetLearn();
            foreach (var item in learn)
                Delete(item);
            
        }
        public void DeleteFileData()
        {
            _connection.Dispose();
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            
        }

        public void Delete<T>(T value)=>_connection.Delete(value);
        
    }
}
