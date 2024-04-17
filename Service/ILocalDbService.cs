

using LearnApplication.Model;

namespace LearnApplication.Service
{
    public interface ILocalDbService
    {
        public void Init();
        public Learn GetLearn();
        public void Create<T>(T value);
        public void Update<T>(T value);
        public void DeleteFileData();
        public void Delete<T>(T value);
    }
}
