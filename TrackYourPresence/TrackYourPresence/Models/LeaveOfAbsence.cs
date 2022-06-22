using System;
using System.Diagnostics;

namespace TrackYourPresence.Models
{
    public class LeaveOfAbsence
    {
        public int? Id { get; set; }
        public string Uuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOfDays { get; set; }
    }
}