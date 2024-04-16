

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
        private const string DB_NAME = "data_learn_save_18.db3";
        private SQLiteConnection _connection;
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create  |
            SQLiteOpenFlags.SharedCache;


        private void Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME), Flags);
            _ = _connection.CreateTable<Learn>();
            _ = _connection.CreateTable<Category>();
            _ = _connection.CreateTable<СardQuestion>();
        }

        public Learn GetLearn()
        {
            Init();

            var learn = _connection.GetAllWithChildren<Learn>(recursive: true).FirstOrDefault();

            if (learn is null)
            {
                learn = new Learn();
                Create(learn);
            }
            return learn;
        }

       


        public void Create<T>(T value)
        {
            Init();
            _connection.InsertWithChildren(value, recursive: true);
        }

        //public T UpdateAndGetById<T>(int id,T update) where T : IDataSelect, new()
        //{
        //    Update(update);
        //    return _connection.GetAllWithChildren<T>().Where(x => x.Id == id).FirstOrDefault();
        //}

        //public T GetById<T>(int id) where T : IDataSelect, new()
        //    =>_connection.GetAllWithChildren<T>().Where(x => x.Id == id).FirstOrDefault(); 

        public void Update<T>(T value)
        {
            Init();
            _connection.UpdateWithChildren(value);
        }
        
        public void DeleteAll()
        {
            //var learn = GetLearn();
            //foreach (var item in learn.FirstOrDefault().LearnCategories)
            //    learn.Delete(item);
                
            
        }
        public void DeleteFileData()
        {
            Init();
            try
            {
                _connection.Dispose();
                File.Delete(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
        }

        public void Delete<T>(T value)
        {
            Init();
            try
            {
                _connection.Delete(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
