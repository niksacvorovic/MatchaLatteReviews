using System.Collections.Generic;

namespace MatchaLatteReviews.Domen.RepositoryInterfaces
{
    public interface ICrudRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        T GetById(int id);
        T Update(T entity);
        void DeleteById(int id);
        void SaveAll(IEnumerable<T> entities);
    }
}
