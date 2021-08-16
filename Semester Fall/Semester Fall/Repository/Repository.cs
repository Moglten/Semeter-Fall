using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Semester_Fall.Models;
using Microsoft.EntityFrameworkCore;
using Semester_Fall.Repository;

namespace E_Commerce_Store.Repository
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly SemesterDbContext _context;
        private readonly DbSet<T> _dbset;

        public Repository(SemesterDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
       

        public void Add(T entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAllEntities()
        {
            return _dbset.ToList();
        }


        public IQueryable<T> GetAllEntitiesIQueryable()
        {
            return _dbset;
        }

     

        public T GetEntity(int entityID)
        {
            return _dbset.Find(entityID);
        }

    
     
        public int GetNumberOfEntities()
        {
            return _dbset.Count();
        }

       

      
        public void Remove(int entityID)
        {
            _dbset.Remove(GetEntity(entityID));
            _context.SaveChanges();
        }



        public void Update(T entity)
        {
            _dbset.Update(entity);
            _context.SaveChanges();
        }


    }
}
