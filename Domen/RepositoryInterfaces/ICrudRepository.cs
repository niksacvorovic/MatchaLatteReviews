using System.Collections.Generic;

namespace MatchaLatteReviews.Domen.RepositoryInterfaces
{
    public interface ICrudRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        T GetById(string id);
        T Update(T entity);
        void DeleteById(string id);
        void SaveAll(IEnumerable<T> entities);
    }
}
