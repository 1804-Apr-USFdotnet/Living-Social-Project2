using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using RealEstateCRM.Models;
using System.Data.Entity.Infrastructure;

namespace RealEstateCRM.DataAccessLayer
{
    public class TestCRMContext : DbContext, IDbContext
    {
        public DbSet<Lead> Leads { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RealEstateAgent> RealEstateAgents { get; set; }



        public TestCRMContext() : base("TestCRM")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("Created").CurrentValue = DateTime.Now;
            });

            var ModifiedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            ModifiedEntities.ForEach(E =>
            {
                E.Property("Modified").CurrentValue = DateTime.Now;
            });
            return base.SaveChanges();
        }

        public new DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : IEntity
        {
            return base.Entry(entity);
        }


        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

    }
}
