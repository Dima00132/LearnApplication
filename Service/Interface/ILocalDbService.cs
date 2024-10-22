﻿using LearnApplication.Model;
using System.Collections.ObjectModel;

namespace LearnApplication.Service.Interface
{
    public interface ILocalDbService
    {
        public void Init();
        public Learn GetLearn();
        public void Create<T>(T value);
        public void Update<T>(T value);
        //public void DeleteFileData();
        public void Delete<T>(T value);
        public void DeleteAndUpdate<TDelete, TUpdate>(TDelete valueDelete, TUpdate valueUpdate);
        public void CreateAndUpdate<TCreate, TUpdate>(TCreate valueCreate, TUpdate valueUpdate);
      

        //public ObservableCollection<CardQuestion> GetById(int id);
    }
}
