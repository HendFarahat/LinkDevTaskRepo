using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperities = null);
        T GetFirstOrDefault(Func<T, bool> filter, string? IncludeProperities = null, bool Tracked = false);
        void Remove(T entity);
        void Add(T Object);
  


    }
}
