using System.Threading.Tasks;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Services
{
    public interface IAuthenticationService
    {
        Task<User> LoginUser(string deviceId);
        Task<User?> Find(string deviceId);
    }
}