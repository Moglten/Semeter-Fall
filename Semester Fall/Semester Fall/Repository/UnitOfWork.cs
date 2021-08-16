using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Store.Repository;
using Semester_Fall.Models;

namespace Semester_Fall.Repository
{
    public class UnitOfWork 
    {
        private readonly SemesterDbContext DbEntity = new SemesterDbContext();

        //Get Generic Context
        public IRepository<T> GetRepositoryInstance<T>() where T : class
        {
            return new Repository<T>(DbEntity);
        }

        //Get Normal Context
        public SemesterDbContext getContext()
        {
            return DbEntity;
        }

        public void Savechanges()
        {
            DbEntity.SaveChanges();
        }


    }
}
