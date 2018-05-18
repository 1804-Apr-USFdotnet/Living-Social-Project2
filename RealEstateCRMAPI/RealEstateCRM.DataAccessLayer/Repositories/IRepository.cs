using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateCRM.Models;

namespace RealEstateCRM.DataAccessLayer.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        T GetByID(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
        void Save();
        void Dispose();
    }
}
