using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using Xamarin.Forms.Internals;

namespace TrackYourPresence.Services
{
    public class WorkDayService : IWorkDayService
    {
        private IEnumerable<WorkDay> currentWeek;
        private List<WorkDay> workDays = new();

        public WorkDayService()
        {
            // todo REMOVE WHEN WORKING WITH API
            // get current week range.
            var weekRange = GetDateTimeCurrentWeekRange();
            var workDayCurrentWeekRange = new List<WorkDay>();

            // Make based on week range the modals.
            weekRange.ForEach(date =>
                workDayCurrentWeekRange.Add(new WorkDay(date) {Uuid = Guid.NewGuid().ToString()}));
            currentWeek = workDayCurrentWeekRange;
            // todo END REMOVE WHEN WORKING WITH API
        }

        public async Task<bool> AddItemAsync(WorkDay item)
        {
            if (String.IsNullOrEmpty(item.Uuid))
            {
                item.Uuid = Guid.NewGuid().ToString();
            }

            workDays.Add(item);
            return true;
        }

        public async Task<bool> UpdateItemAsync(WorkDay item)
        {
            var workDay = await GetItemAsync(item.Uuid);

            workDay.Date = item.Date;
            workDay.StartTime = item.StartTime;
            workDay.StopTime = item.StopTime;
            workDay.PauseTime = item.PauseTime;
            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkDay> GetItemAsync(string id)
        {
            return await Task.FromResult(
                workDays.First(wd => wd.Uuid == id)
            );
        }

        public async Task<IEnumerable<WorkDay>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(workDays);
        }

        public async Task<IEnumerable<WorkDay>> GetCurrentWeekAsync(bool forceRefresh = false)
        {
            var currentWeekNew = new List<WorkDay>();

            currentWeek.ForEach(wd =>
            {
                WorkDay result = null;
                try
                {
                    result = workDays.FirstOrDefault(
                        d => d.Date.Year == wd.Date.Year
                             && d.Date.Month == wd.Date.Month
                             && d.Date.Day == wd.Date.Day
                    );
                }
                catch (Exception e)
                {
                    // ignored
                }

                currentWeekNew.Add(result ?? wd);
            });
            
            return await Task.FromResult(currentWeekNew);

            // todo lateron when working with api:

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
            var startOfWeek = DateTime.Today.DayOfWeek == DayOfWeek.Sunday
                ? DateTime.Today.AddDays(-7).AddDays(GetSubDayToFirstWeekDay())
                : DateTime.Today.AddDays(GetSubDayToFirstWeekDay());

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