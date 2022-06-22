using System;
using System.ComponentModel.DataAnnotations;

namespace TrackYourPresenceAPI.Models
{
    public class WorkDay
    {
        [Key]
        public int? id { get; set; }
        public Guid? Uuid { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime StopTime { get; set; }
    }
}