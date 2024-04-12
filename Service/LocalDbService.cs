
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
        private const string DB_NAME = "data_learn_save_15.db3";
        private readonly SQLiteConnection _connection;
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create  |
            SQLiteOpenFlags.SharedCache;

        public LocalDbService()
        {
          
            //File.Delete(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME), Flags);



           // _ = _connection.CreateTable<Category>();
            try
            {
                _ = _connection.CreateTable<Learn>();
                _ = _connection.CreateTable<Category>();
                _ = _connection.CreateTable<СardQuestion>();
               
                //_ = _connection.CreateTables< Category,СardQuestion, Learn>();
                //_ = _connection.CreateTable<СardQuestion>();
            }
            catch (Exception Ex)
            {

                var t = Ex;
                throw;
            }


           
        }

        private Learn _learn;
        public void UpdateAll()
        {
            if (_learn is null)
                return;
            _connection.Update(_learn);
        }

        public Learn GetLearn()
        {
            _learn = _connection.GetAllWithChildren<Learn>(recursive: true).FirstOrDefault();
            // var table = _connection.GetTableInfo("learn");
            if (_learn is null)
            {
                _learn = new Learn();
                Create(_learn);
                return new Learn();
            }
            return _learn;
        }


        public void Create<T>(T learnCategory)
        {
            try
            {
                _connection.InsertWithChildren(learnCategory);
            }
            catch (Exception Ex)
            {

                var t = Ex;
                throw; 
            }
            
        }

        //public T UpdateAndGetById<T>(int id,T update) where T : IDataSelect, new()
        //{
        //    Update(update);
        //    return _connection.GetAllWithChildren<T>().Where(x => x.Id == id).FirstOrDefault();
        //}

        //public T GetById<T>(int id) where T : IDataSelect, new()
        //    =>_connection.GetAllWithChildren<T>().Where(x => x.Id == id).FirstOrDefault(); 
        
        
        public void Update<T>(T value)=>_connection.UpdateWithChildren(value);
        
        public void DeleteAll()
        {
            //var learn = GetLearn();
            //foreach (var item in learn.FirstOrDefault().LearnCategories)
            //    learn.Delete(item);
                
            
        }
        public void DeleteFileData()
        {
            _connection.Dispose();
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            
        }

        public void Delete<T>(T value)=>_connection.Delete(value);
        
    }
}
