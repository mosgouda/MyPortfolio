using core.interfaces;
using infrastructure.Reposotry;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericReposotry<T> _entity;

        public UnitOfWork(DataContext context)
        {
           _context = context;
        }
        public IGenericReposotry<T> Entity
        {
            get 
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }

        public void save()
        {
           _context.SaveChanges();
        }
    }
}
