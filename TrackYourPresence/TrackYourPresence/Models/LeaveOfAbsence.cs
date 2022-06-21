using System;
using System.Diagnostics;

namespace TrackYourPresence.Models
{
    public class LeaveOfAbsence
    {
        public string Uuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOfDays { get; set; }

        private static int _totalHoursInWeek = 24 * 7;
        private static int _workHoursInWeek = 32;
        private static int _hoursOfWorkDay = 8;

        /*
         * FROM HERE START OF CALCULATION FOR VACATION DAYS.
         */
        public static DateTime FooCalculate(int days, DateTime beginDate)
        {
            var vacationAmountInHours = days * _hoursOfWorkDay;
            var hoursToEndDate = GetTotalHours(vacationAmountInHours);

            Debug.WriteLine(vacationAmountInHours);
            Debug.WriteLine(hoursToEndDate);

            return beginDate.AddHours(hoursToEndDate);
        }

        private static int GetTotalHours(int amount, int result = 0)
        {
            if (amount <= 0)
            {
                return result;
            }

            if (amount >= _workHoursInWeek)
            {
                return GetTotalHours(amount - _workHoursInWeek, result + _totalHoursInWeek);
            }

            var daysToAdd = amount / 8;
            var hours = daysToAdd * 24;
            
            return GetTotalHours(amount - hours, result + hours);
        }
        /*
         * FROM HERE END OF CALCULATION FOR VACATION DAYS.
         */
    }
}