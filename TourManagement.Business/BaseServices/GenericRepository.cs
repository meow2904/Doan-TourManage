using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.BaseServices
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly TourManagementContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository()
        {
            Context = new TourManagementContext();
            _dbSet = Context.Set<TEntity>();
        }
        public bool Add(TEntity entity)
        {
                _dbSet.Add(entity);
                return Context.SaveChanges() > 0;
        }

        public void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            Context.SaveChanges();
        }

        public void DeleteByID(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            Context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int Id)
        {
            return _dbSet.Find(Id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.AddOrUpdate(entity);
            Context.SaveChanges();
        }
    }
}
