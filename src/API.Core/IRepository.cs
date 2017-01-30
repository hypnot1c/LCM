using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace API.Core
{
  public interface IRepository<TEntity> : IDisposable where TEntity : class
  {
    void Add(TEntity entity);
    void Remove(int Id);
    IEnumerable<TEntity> GetAll();
    TEntity Get(int Id);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
  }
}
