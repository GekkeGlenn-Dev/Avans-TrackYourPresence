using System.Threading.Tasks;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public interface IAuthenticationService
    {
        Task LoginUser();
        Task<User> GetUser();
    }
}