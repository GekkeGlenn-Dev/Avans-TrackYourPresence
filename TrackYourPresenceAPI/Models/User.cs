using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TrackYourPresenceAPI.Models
{
    public class User
    {
        [Key] public int? Id { get; set; }
        public string DeviceId { get; set; }
        public int WorkHours { get; set; }
        public int VacationDays { get; set; }

        [JsonIgnore]
        public ICollection<WorkDay> WorkDays { get; set; }
    }
}