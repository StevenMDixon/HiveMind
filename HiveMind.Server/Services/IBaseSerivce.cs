using System.Linq.Expressions;

namespace HiveMind.Server.Services;
public interface IBaseService<T> : IService
{
    public IQueryable<T> Get(params Expression<Func<T, object>>[] includes);
    public T Create(T entity);
    public void Create(IEnumerable<T> entities);
    public T Update(T entity);
    public void Update(IEnumerable<T> entities);
    public void Delete(T entity);
    public void Delete(IEnumerable<T> entities);
}
