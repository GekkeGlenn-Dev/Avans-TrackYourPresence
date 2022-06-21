using System;

namespace TrackYourPresence.Models
{
    public class AbsentItem
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}