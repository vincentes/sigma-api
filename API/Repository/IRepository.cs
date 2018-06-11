using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IRepository<T>
    {
        T Add(T item);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Delete(T item);
        void Update(T item);
    }
}
