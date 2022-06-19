using System;
using Xamarin.Forms;

namespace TrackYourPresence.Models
{
    public class WorkDay
    {
        public string Uuid { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime StopTime { get; set; }

        public WorkDay() : this(DateTime.Today)
        {
        }

        public WorkDay(DateTime date)
        {
            Date = date;
            StartTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            PauseTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            StopTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public void SetBeginTimeFromTimeSpan(TimeSpan timeSpan)
        {
            var date = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);
            StartTime = date + timeSpan;
        }

        public void SetStopTimeFromTimeSpan(TimeSpan timeSpan)
        {
            var date = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);
            StopTime = date + timeSpan;
        }

        public void SetPauseTimeFromTimeSpan(TimeSpan timeSpan)
        {
            var date = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);
            PauseTime = date + timeSpan;
        }

        public TimeSpan GetCalculatedTime()
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0)
                   + (StopTime - StartTime)
                   - PauseTime;
        }

        public static TimeSpan GetTimeSpanFromDateTime(DateTime dateTime)
        {
            return TimeSpan.FromMinutes(dateTime.Hour * 60 + dateTime.Minute);
        }

        private string OnCalculateTimeToString()
        {
            var time = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0) + GetCalculatedTime();
            return $"{time: HH:mm}";
        }
    }
}