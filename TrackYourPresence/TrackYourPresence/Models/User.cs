namespace TrackYourPresence.Models
{
    public class User
    {
        public string DeviceId { get; }
        public int WorkHours { get; set; }

        public User(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}