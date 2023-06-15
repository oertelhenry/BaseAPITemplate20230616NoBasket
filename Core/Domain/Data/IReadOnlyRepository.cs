using System.Linq.Expressions;

namespace Mobalyz.Data
{
    public interface IReadOnlyRepository
    {
        void DetachEnity<TEntity>(TEntity entity)
             where TEntity : class, IEntity;

        IEnumerable<TEntity> Get<TEntity>(
                    Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        TEntity GetById<TEntity>(int id, bool activeOnly = true, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        Task<TEntity> GetByIdAsync<TEntity>(int id, bool activeOnly = true, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
            where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
            where TEntity : class, IEntity;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
            where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
            where TEntity : class, IEntity;

        TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity;

        IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includes = null,
            int? skip = null,
            int? take = null,
            bool activeOnly = true)
            where TEntity : class, IEntity;

        void Load<TEntity>(TEntity entity, Expression<Func<TEntity, object>> includes)
            where TEntity : class, IEntity;

        Task LoadAsync<TEntity>(TEntity entity, Expression<Func<TEntity, object>> includes)
            where TEntity : class, IEntity;
    }
}