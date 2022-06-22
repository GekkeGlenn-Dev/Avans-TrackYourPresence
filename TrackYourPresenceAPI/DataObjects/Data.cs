using System;

namespace TrackYourPresenceAPI.DataObjects
{
    public class Data<T>
    {
        public string DeviceId { get; set; }
        public Guid? Uuid { get; set; }
        public T? Entity { get; set; }
    }
}