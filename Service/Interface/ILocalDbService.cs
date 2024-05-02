using LearnApplication.Model;
using System.Collections.ObjectModel;

namespace LearnApplication.Service.Interface
{
    public interface ILocalDbService
    {
        public void Init();
        public Learn GetLearn();
        public void Create<T>(T value);
        public void Update<T>(T value);
        public void DeleteFileData();
        public void Delete<T>(T value);
        public void DeleteAndUpdate<TD, TU>(TD valueDelete, TU valueUpdate);
        public void CreateAndUpdate<TC, TU>(TC valueCreate, TU valueUpdate);

        //public ObservableCollection<СardQuestion> GetById(int id);
    }
}
