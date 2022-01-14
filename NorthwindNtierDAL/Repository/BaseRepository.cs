using NorthwindNtierDAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindNtierDAL.Repository
{
    public abstract class BaseRepository<T> where T:class
    {
        NorthwindEntities db = new NorthwindEntities();
        public bool Add(T entity)
        {
            try
            {
                Set().Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Delete(T entity)
        {
            try
            {
                Set().Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T Find(int id)
        {
            return Set().Find(id);
        }
        public T Find(string id)
        {
            return Set().Find(id);
        }
        public T Find(int key1, int key2)
        {
            return Set().Find(key1, key2);
        }

        public List<T> List()
        {
            return Set().ToList();
        }

        public DbSet<T> Set()
        {
            return db.Set<T>();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
