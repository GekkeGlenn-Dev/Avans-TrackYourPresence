using System.Collections.Generic;
using System.Threading.Tasks;
using TrackYourPresenceAPI.Models;
using TrackYourPresenceAPI.DataObjects;

namespace TrackYourPresenceAPI.Services
{
    public interface IWorkDayService : IApiService<WorkDay>
    {
        Task<IEnumerable<WorkDay>> GetCurrentWeek(Data<WorkDay> data);
    }
}