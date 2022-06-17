using System;
using Xamarin.Forms;

namespace TrackYourPresence.Models
{
    public class WorkDay
    {
        public Guid? Uuid { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime StopTime { get; set; }
        public Command GetCalculatedTimeCommand { get; }

        public WorkDay() : this(DateTime.Today)
        {
        }

        public WorkDay(DateTime date)
        {
            Date = date;
            StartTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            PauseTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            StopTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            GetCalculatedTimeCommand = new Command(() => OnCalculateTimeToString());
        }
        
        public string GetFormattedStartTime()
        {
            return $"{StartTime: HH:mm}";
        }

        public string GetFormattedPauseTime()
        {
            return $"{PauseTime: HH:mm}";
        }

        public string GetFormattedStopTime()
        {
            return $"{StopTime: HH:mm}";
        }

        public TimeSpan GetCalculatedTime()
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0) 
                + (StopTime - StartTime) 
                - PauseTime;
        }

        private string OnCalculateTimeToString()
        {
            var time = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0) + GetCalculatedTime();
            return $"{time: HH:mm}";
        }
    }
}