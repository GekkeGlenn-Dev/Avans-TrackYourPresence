using System;
using System.Collections.Generic;

namespace TrackYourPresence.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string DeviceId { get; }
        public int WorkHours { get; set; }
        public int VacationDays { get; set; }

        public User()
        {
        }

        public User(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}