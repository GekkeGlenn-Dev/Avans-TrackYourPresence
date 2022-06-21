using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Services
{
    public class WorkDayService : IWorkDayService
    {
        private DataContext _context;

        public WorkDayService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkDay>> GetAllAsync()
        {
            return await _context.WorkDays.ToListAsync();
        }

        public async Task<WorkDay?> FindAsync(string id)
        {
            return await _context.WorkDays.SingleOrDefaultAsync(w => w.Uuid.ToString() == id);
        }

        public async Task<WorkDay> CreateAsync(WorkDay workDay)
        {
            workDay.Uuid = Guid.NewGuid();
            var result = await _context.WorkDays.AddAsync(workDay);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<WorkDay> UpdateAsync(WorkDay workDay)
        {
            var result = _context.WorkDays.Update(workDay);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<WorkDay>> GetCurrentWeek()
        {
            var weekRange = GetDateTimeCurrentWeekRange();
            var workDayWeekRange = new List<WorkDay>();

            // Make based on week range the modals.
            weekRange.ForEach(date =>
                workDayWeekRange.Add(new WorkDay()
                {
                    Date = date,
                    StartTime = date,
                    PauseTime = date,
                    StopTime = date
                }));

            var start = weekRange.First();
            var end = weekRange.Last();

            var workDayWeekRangeFromDb = await _context.WorkDays
                .Where(f =>
                    f.Date >= start && f.Date <= end
                ).ToListAsync();
            
            workDayWeekRange.ForEach(wd =>
            {
                WorkDay? result = null;
                try
                {
                    result = workDayWeekRangeFromDb.SingleOrDefault(
                        d => d.Date.Year == wd.Date.Year
                             && d.Date.Month == wd.Date.Month
                             && d.Date.Day == wd.Date.Day
                    );
                }
                catch (Exception e)
                {
                    // ignored
                    Debug.WriteLine(e.StackTrace);
                }

                if (result != null)
                {
                    wd.id = result.id;
                    wd.Uuid = result.Uuid;
                    wd.User = result.User;
                    wd.Date = result.Date;
                    wd.StartTime = result.StartTime;
                    wd.PauseTime = result.PauseTime;
                    wd.StopTime = result.StopTime;
                }
            });

            return workDayWeekRange;
        }

        private static List<DateTime> GetDateTimeCurrentWeekRange()
        {
            var startOfWeek = DateTime.Today.DayOfWeek == DayOfWeek.Sunday
                ? DateTime.Today.AddDays(-7).AddDays(GetSubDayToFirstWeekDay())
                : DateTime.Today.AddDays(GetSubDayToFirstWeekDay());

            List<DateTime> weekDateTimeRange = new();

            for (var i = 0; i < 7; i++)
            {
                var item = new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day + i);
                weekDateTimeRange.Add(item);
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