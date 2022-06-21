#nullable enable
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private static User? _user;

        private async Task LoginUser()
        {
            // api call with this device id.
            // returns object for user.

            _user = new User(CrossDeviceInfo.Current.Id);
        }

        public async Task<User> GetUser()
        {
            if (_user != null)
            {
                return _user;
            }

            await LoginUser();
            return _user;
        }
    }
}