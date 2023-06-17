using Domain.Odyssey.Entities.Documents;
using System.Linq.Expressions;

namespace Mobalyz.Data
{
    public interface IDataRepository
    {
        Task<IEnumerable<PdfTemplate>> GetPdfTemplateList(string UserName);
        Task<IEnumerable<HtmlMailTemplate>> GetMailTemplateList(string UserName);
        Task<PdfTemplate> GetPdfTemplateById(int templateId);
        Task<HtmlMailTemplate> GetMailTemplateById(int templateId);
        Task<bool> VerifyTemplateExist(string templateName);


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

        void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity;

        Task SaveAsync();

        void Delete<TEntity>(object id, bool logical = true)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity, bool attached = false, bool logical = true)
            where TEntity : class, IEntity;

        void Save();

        void Update<TEntity>(TEntity entity, bool attached = false, string modifiedBy = null)
                                            where TEntity : class, IEntity;
    }
}
