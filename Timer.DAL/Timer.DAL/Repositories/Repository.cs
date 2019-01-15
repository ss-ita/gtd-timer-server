using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Timer.DAL.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity:class
    {
        internal TimerContext TimerContext;
        internal DbSet<TEntity> dbSet;

        public Repository(TimerContext context)
        {
            this.TimerContext = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAllEntities()
        {
            return this.dbSet;
        }

        public IEnumerable<TEntity> GetAllEntitiesByFilter(Func<TEntity, bool> filter)
        {
            return this.dbSet.Where(filter);
        }

        public TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (this.TimerContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            this.TimerContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Save()
        {
            this.TimerContext.SaveChanges();
        }
    }
}
