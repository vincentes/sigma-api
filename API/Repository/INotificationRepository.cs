using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface INotificationRepository<T>
    {
        T Add(T item);
        IEnumerable<T> GetAll();
    }
}
