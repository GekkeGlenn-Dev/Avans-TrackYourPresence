using System.Collections.Generic;
using System.Threading.Tasks;
using TrackYourPresenceAPI.DataObjects;

namespace TrackYourPresenceAPI.Services
{
    public interface IApiService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(Data<T> data);

        Task<T?> FindAsync(Data<T> data);

        Task<T> CreateAsync(Data<T> data);

        Task<T> UpdateAsync(Data<T> data);
    }
}