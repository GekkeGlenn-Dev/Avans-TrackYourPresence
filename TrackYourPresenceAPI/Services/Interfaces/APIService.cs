using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackYourPresenceAPI.Services
{
    public interface APIService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T?> FindAsync(string id);

        Task<T> CreateAsync(T workDay);

        Task<T> UpdateAsync(T workDay);
    }
}