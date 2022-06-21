using System.Collections.Generic;
using System.Threading.Tasks;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Services
{
    public interface IWorkDayService : APIService<WorkDay>
    {
        Task<IEnumerable<WorkDay>> GetCurrentWeek();
    }
}