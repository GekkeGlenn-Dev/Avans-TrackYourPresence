using System;

namespace TrackYourPresenceAPI.Models
{
    public class LeaveOfAbsence
    {
        public int? Id { get; set; }
        public Guid? Uuid { get; set; }
        public User? User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOfDays { get; set; }
    }
}