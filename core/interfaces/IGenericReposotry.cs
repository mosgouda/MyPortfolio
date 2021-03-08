using System;
using System.Collections.Generic;
using System.Text;

namespace core.interfaces
{
    public interface IGenericReposotry<T>where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id );
    }
}
