using LearnApplication.Model;
using LearnApplication.Model.Web;
using LearnApplication.Service.Interface;
using Microsoft.VisualBasic;
using SQLite;

using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Service
{

    public sealed class LocalDbService: ILocalDbService
    {
        private const string DB_NAME = "data_learn_save_49.db3";
        private SQLiteConnection _connection;
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create  |
            SQLiteOpenFlags.SharedCache;


        public void Init()
        {
            if (_connection is not null)
                return;
            _connection = new SQLiteConnection(Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, DB_NAME), Flags);

            try
            {
                _ = _connection.CreateTable<Learn>();
                _ = _connection.CreateTable<Subject>();
                _ = _connection.CreateTable<UrlWebValid>();
                _ = _connection.CreateTable<CardQuestion>();
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
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

        public void CreateAndUpdate<TCreate, TUpdate>(TCreate valueCreate, TUpdate valueUpdate)
        {
            Create(valueCreate);
            Update(valueUpdate);
        }
        public void DeleteAndUpdate<TDelete, TUpdate>(TDelete valueDelete, TUpdate valueUpdate)
        {
            Delete(valueDelete);
            Update(valueUpdate);
        }

  

        public void Create<T>(T value)
        {
            try
            {
                Init(); 
                _connection.InsertWithChildren(value, recursive: true);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void Update<T>(T value)
        {
            Init();
            _connection.UpdateWithChildren(value);
        }
        
        public void DeleteFileData()
        {
            Init();
            try
            {
                _connection.Dispose();
                File.Delete(Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, DB_NAME));
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
