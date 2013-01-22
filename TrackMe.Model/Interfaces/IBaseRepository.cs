using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace TrackMe.Model.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Insert(ICollection<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Delete(ICollection<TEntity> entitiesToDelete);
        void Update(TEntity entityToUpdate);
        void Update(ICollection<TEntity> entitiesToUpdate);
        IEnumerable<TEntity> GetAll();
    }
}
