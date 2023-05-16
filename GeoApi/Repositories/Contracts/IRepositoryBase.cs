using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
      
        IQueryable<T> FindAll(bool trackChanges); // tüm kayıtları bir sorgu nesnesi olarak getirir, trackChanges: değisiklikleri izleyip izlememe kontrolü
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);//  kayıtları, verilen koşula göre bir sorgu nesnesi olarak getirir
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
