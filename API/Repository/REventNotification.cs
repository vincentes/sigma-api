using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class REventNotification : INotificationRepository<EventNotification>
    {
        private readonly SigmaContext _context;

        public REventNotification(SigmaContext context)
        {
            this._context = context;
        }

        public EventNotification Add(EventNotification item)
        {
            var entityEntry = _context.Add(item);
            _context.SaveChanges();
            var excludeEscritoEntity = entityEntry.Entity;
            return excludeEscritoEntity;
        }

        public IEnumerable<EventNotification> GetAll()
        {
            return _context.EventNotifications.Include(t => t.Event).Include(t => t.User).ToList();
        }
    }
}
