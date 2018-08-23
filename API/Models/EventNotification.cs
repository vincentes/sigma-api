using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class EventNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public DateTime DateSent { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
