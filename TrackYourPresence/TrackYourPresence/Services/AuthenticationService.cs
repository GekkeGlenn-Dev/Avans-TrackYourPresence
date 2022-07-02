#nullable enable
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using TrackYourPresence.Helpers;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class AuthenticationService : AbstractServiceBase<User>, IAuthenticationService
    {
        private static User? _user;

        public async Task LoginUser()
        {
            var user = new User(CrossDeviceInfo.Current.Id);
            var response = await HttpPost(Api.GetApiUrl("/Authentication/login"), user, null);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    _user = FromJson<User>(await response.Content.ReadAsStringAsync());
                    return;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            else
            {
                Debug.WriteLine("Response Code:" + response.StatusCode);
            }

            _user = user;
        }

        public async Task<User> GetUser()
        {
            if (_user == null)
            {
                await LoginUser();
            }

            return _user;
        }

        public async void UpdateUser(User user)
        {
            var response = await HttpPost(Api.GetApiUrl("/Authentication/login"), user, null);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    _user = FromJson<User>(await response.Content.ReadAsStringAsync());
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            else
            {
                Debug.WriteLine("Status code: " + response.StatusCode);
            }
        }
    }
}