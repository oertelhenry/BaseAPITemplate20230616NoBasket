using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructue.Data;
using mobalyz.ErrorHandling;
using Domain.Odyssey.Entities.Documents;
using Mobalyz.Odyssey.Data;

namespace Mobalyz.Data
{
    public class DataRepository : IDataRepository
    {
        public readonly StoreContext _context;
        public readonly OdysseyContext _dataContext;
        public readonly IMapper _mapper;
        public DataRepository(StoreContext context, IMapper mapper, OdysseyContext dataContext)
        {
            _context = context;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<PdfTemplate>> GetPdfTemplateList(string UserName)
        {
            List<PdfTemplate> PdfTemplateList = new List<PdfTemplate>();
            try
            {
                PdfTemplateList = await _dataContext.PdfTemplate
                .Where(b => b.UserName.Contains(UserName))
                .Where(a => a.IsActive == true).OrderBy(p => p.TemplateName).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return PdfTemplateList;
        }

        public async Task<IEnumerable<HtmlMailTemplate>> GetMailTemplateList(string UserName)
        {
            List<HtmlMailTemplate> MailTemplateList = new List<HtmlMailTemplate>();
            try
            {
                MailTemplateList = await _dataContext.HtmlMailTemplate
                .Where(b => b.UserName == UserName)
                .Where(a => a.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MailTemplateList;
        }

        public async Task<bool> VerifyTemplateExist(string templateName)
        {
            PdfTemplate pdfTemplate;
            try
            {
                pdfTemplate = _dataContext.PdfTemplate
                .Where(b => b.TemplateName == templateName)
                .Where(a => a.IsActive == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pdfTemplate == null ? false : true;
        }

        public async Task<PdfTemplate> GetPdfTemplateById(int templateId)
        {
            PdfTemplate pdfTemplate = new PdfTemplate();
            try
            {
                pdfTemplate = _dataContext.PdfTemplate
                .Where(b => b.Id == templateId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pdfTemplate;
        }
        public async Task<HtmlMailTemplate> GetMailTemplateById(int templateId)
        {
            HtmlMailTemplate mailTemplate = new HtmlMailTemplate();
            try
            {
                mailTemplate = _dataContext.HtmlMailTemplate
                .Where(b => b.Id == templateId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mailTemplate;
        }

        public async Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, activeOnly: activeOnly).AnyAsync();
        }


        void IDataRepository.DetachEnity<TEntity>(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    int? skip = null,
    int? take = null,
    bool activeOnly = true,
    params Expression<Func<TEntity, object>>[] includes)
    where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, includes, skip, take, activeOnly).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
             bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(null, orderBy, includes, skip, take, activeOnly).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           int? skip = null,
           int? take = null,
            bool activeOnly = true,
           params Expression<Func<TEntity, object>>[] includes)
           where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(null, orderBy, includes, skip, take, activeOnly).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
             bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, includes, skip, take, activeOnly).ToListAsync();
        }

        public virtual TEntity GetById<TEntity>(int id, bool activeOnly = true, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return this.GetFirst<TEntity>(e => e.Id == id, includes: includes, activeOnly: activeOnly);
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(int id, bool activeOnly = true, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return this.GetFirstAsync<TEntity>(e => e.Id == id, includes: includes, activeOnly: activeOnly);
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, activeOnly: activeOnly).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, activeOnly: activeOnly).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null, bool activeOnly = true)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, activeOnly: activeOnly).Any();
        }

        public virtual TEntity GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool activeOnly = true,
           params Expression<Func<TEntity, object>>[] includes)
           where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, includes, activeOnly: activeOnly).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, includes, activeOnly: activeOnly).FirstOrDefaultAsync();
        }

        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
             bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, null, includes, activeOnly: activeOnly).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
             bool activeOnly = true,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, null, includes, activeOnly: activeOnly).SingleOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<Expression<Func<TEntity, object>>> includes = null,
            int? skip = null,
            int? take = null,
            bool activeOnly = true)
            where TEntity : class, IEntity
        {
            if (!_context.Database.CanConnect())
            {
                throw new DatabaseUnavailableException("Unable to connect to the SQL database");
            }

            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (activeOnly)
            {
                query = query.Where(r => r.IsActive);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual void Load<TEntity>(TEntity entity, Expression<Func<TEntity, object>> includes)
            where TEntity : class, IEntity
        {
            _context.Entry<TEntity>(entity).Reference(includes).Load();
        }

        public virtual async Task LoadAsync<TEntity>(TEntity entity, Expression<Func<TEntity, object>> includes)
            where TEntity : class, IEntity
        {
            await _context.Entry<TEntity>(entity).Reference(includes).LoadAsync();
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
        where TEntity : class, IEntity
        {
            if (entity is IAuditEntity auditEntity)
            {
                auditEntity.CreatedDate = DateTime.Now;
                auditEntity.ModifiedDate = DateTime.Now;
            }

            _context.Set<TEntity>().Add(entity);
        }

        public async virtual Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (ValidationsException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        protected virtual void ThrowEnhancedValidationException(ValidationsException e)
        {
            var errorMessages = e.ValidationResults
                    .Select(x => x.ErrorMessage);

            throw new ModelValidationException(e.Message, errorMessages.ToList());
        }

        public virtual void Delete<TEntity>(object id, bool logical = true)
            where TEntity : class, IEntity
        {
            TEntity entity = _context.Set<TEntity>().Find(id);

            if (logical)
            {
                this.LogicalDelete(entity, true);
            }
            else
            {
                this.PhysicalDelete(entity);
            }
        }

        public virtual void Delete<TEntity>(TEntity entity, bool attached = false, bool logical = true)
            where TEntity : class, IEntity
        {
            if (logical)
            {
                this.LogicalDelete(entity, attached);
            }
            else
            {
                this.PhysicalDelete(entity);
            }
        }

        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (ValidationsException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }



        public virtual void Update<TEntity>(TEntity entity, bool attached = false, string modifiedBy = null)
                                            where TEntity : class, IEntity
        {
            if (entity is IAuditEntity auditEntity)
            {
                auditEntity.ModifiedDate = DateTime.Now;
            }

            if (!attached)
            {
                var existing = _context.Set<TEntity>().FirstOrDefault(b => b.Id == entity.Id);

                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(entity);
                }

            }
        }

        protected virtual void LogicalDelete<TEntity>(TEntity entity, bool attached = false)
                                    where TEntity : class, IEntity
        {
            entity.IsActive = false;
            Update(entity, attached);
        }

        protected virtual void PhysicalDelete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

    }
}
