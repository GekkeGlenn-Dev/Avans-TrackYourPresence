using System.Threading.Tasks;
using TrackYourPresenceAPI.Models;
using TrackYourPresenceAPI.DataObjects;

namespace TrackYourPresenceAPI.Services
{
    public interface IAuthenticationService
    {
        Task<User> LoginUser(string deviceId);
        Task<User?> Find(string deviceId);
        Task<User?> UpdateUser(Data<User> data);
    }
}