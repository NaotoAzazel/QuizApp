namespace QuizApp.Services.Repositories
{
    public interface IRepository<T>
    {
        void Create(T item);
        T Get(int id);
        void Update(T item);
        void Delete(int id);
    }
}
