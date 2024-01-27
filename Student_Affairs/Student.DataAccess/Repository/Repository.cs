using Microsoft.EntityFrameworkCore;
using Student.DataAccess.Repository.IRepository;
using Students.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private ApplicationDbContext db;
        private DbSet<T> dbSet;
        public Repository(ApplicationDbContext _db)
        {
            db = _db;
            dbSet = db.Set<T>();
        }

        public void Add(T Obj)
        {
            dbSet.Add(Obj);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperities = null)
        {
            IQueryable<T> query = dbSet.AsQueryable();
            if (filter != null)
            {
                query = dbSet.Where(filter).AsQueryable();
            }
            if (IncludeProperities != null)
            {
                foreach (var IncludeProp in IncludeProperities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncludeProp);

                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Func<T, bool> filter, string? IncludeProperities = null, bool Tracked = false)
        {
            IQueryable<T> Result;
            if (Tracked)
            {
                Result = dbSet.Where(filter).AsQueryable();
            }
            else
            {
                Result = dbSet.AsNoTracking().Where(filter).AsQueryable();
            }

            if (IncludeProperities != null)
            {
                foreach (var IncludeProp in IncludeProperities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Result = Result.Include(IncludeProp);

                }
            }
            return Result.FirstOrDefault();

        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

    
    }
}
