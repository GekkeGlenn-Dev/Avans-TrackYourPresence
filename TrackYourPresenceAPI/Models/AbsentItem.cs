using System;
using System.ComponentModel.DataAnnotations;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresence.Models
{
    public class AbsentItem
    {
        [Key]
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}