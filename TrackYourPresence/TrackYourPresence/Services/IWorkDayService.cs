using System.Collections.Generic;
using System.Threading.Tasks;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public interface IWorkDayService : IDataStore<WorkDay>
    {
        Task<IEnumerable<WorkDay>> GetCurrentWeekAsync(bool forceRefresh = false);
    }
}