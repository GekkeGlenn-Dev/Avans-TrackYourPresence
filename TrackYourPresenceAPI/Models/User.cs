using System;
using System.Collections.Generic;

namespace TrackYourPresenceAPI.Models
{
    public class User
    {
        public int? Id { get; set; }
        public Guid? DeviceId { get; set; }
        public int? WorkHours { get; set; }

        public ICollection<WorkDay> WorkDays { get; set; }
    }
}