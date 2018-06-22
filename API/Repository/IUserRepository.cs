using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IUserRepository<T>
    {
        T Add(T item);
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Delete(T item);
        void Update(T item);
    }
}
