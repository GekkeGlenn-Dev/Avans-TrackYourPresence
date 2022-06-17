using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using Xamarin.Forms.Internals;

namespace TrackYourPresence.Services
{
    public class WorkDayService : IWorkDayService
    {
        public async Task<bool> AddItemAsync(WorkDay item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemAsync(WorkDay item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkDay> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkDay>> GetItemsAsync(bool forceRefresh = false)
        {
            // get current week range.
            var weekRange = GetDateTimeCurrentWeekRange();
            var workDayCurrentWeekRange = new List<WorkDay>();

            // Make based on week range the modals.
            weekRange.ForEach(date => workDayCurrentWeekRange.Add(new WorkDay(date)));

            // TODO Do api or DB call.
            // TODO update created modals with data if they exist.

            // return observable collection
            return await Task.FromResult(workDayCurrentWeekRange);
        }

        private static IEnumerable<DateTime> GetDateTimeCurrentWeekRange()
        {
            var startOfWeek = DateTime.Today.AddDays(GetSubDayToFirstWeekDay());
            IList<DateTime> weekDateTimeRange = new List<DateTime>() {startOfWeek};

            for (var i = 1; i < 7; i++)
            {
                weekDateTimeRange.Add(
                    new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day + i)
                );
            }

            return weekDateTimeRange;
        }

        private static int GetSubDayToFirstWeekDay()
        {
            CultureInfo.CurrentCulture = new CultureInfo("nl-NL", false);
            return CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek;
        }
    }
}