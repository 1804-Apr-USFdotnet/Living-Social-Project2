using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RealEstateCRM.Models
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : IEntity;
        DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : IEntity;
        //void SetModified(object entity);
        int SaveChanges();
    }
}
