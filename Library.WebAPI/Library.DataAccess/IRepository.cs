using System.Linq.Expressions;
using Library.DataAccess.Entities;


namespace Library.DataAccess;

public interface IRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
    T? GetById(int id);
    T? GetById(Guid id);
    T Save(T entity);
    void Delete(T entity);
}
