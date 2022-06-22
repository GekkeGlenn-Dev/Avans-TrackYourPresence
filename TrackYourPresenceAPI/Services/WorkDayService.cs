using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Services
{
    public class WorkDayService : AbstractBaseService, IWorkDayService
    {
        private readonly IAuthenticationService authenticationService;

        public WorkDayService(DataContext context, IAuthenticationService authenticationService) : base(context)
        {
            this.authenticationService = authenticationService;
        }

        public async Task<IEnumerable<WorkDay>> GetAllAsync(Data<WorkDay> data)
        {
            return await Context.WorkDays.ToListAsync();
        }

        public async Task<WorkDay?> FindAsync(Data<WorkDay> data)
        {
            return await Context.WorkDays.SingleOrDefaultAsync(
                w => w.Uuid.ToString() == data.Uuid.ToString() && w.User.DeviceId == data.DeviceId
            );
        }

        public async Task<WorkDay> CreateAsync(Data<WorkDay> data)
        {
            var user = await authenticationService.Find(data.DeviceId);
            
            Console.WriteLine(data);
            Console.WriteLine(user);
            Console.WriteLine(data.Entity);
            Console.WriteLine(data.DeviceId);
            
            if (user == null || data.Entity == null)
            {
                throw new Exception();
            }

            data.Entity.Uuid = Guid.NewGuid();
            data.Entity.User = user;
            var result = await Context.WorkDays.AddAsync(data.Entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<WorkDay> UpdateAsync(Data<WorkDay> data)
        {
            var user = await authenticationService.Find(data.DeviceId);
            if (user == null || data.Entity == null)
            {
                throw new Exception();
            }

            data.Entity.User = user;
            var result = Context.WorkDays.Update(data.Entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<WorkDay>> GetCurrentWeek(Data<WorkDay> data)
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

            var workDayWeekRangeFromDb = await Context.WorkDays
                .Where(wd =>
                    wd.Date >= start && wd.Date <= end && wd.User.DeviceId == data.DeviceId
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