using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Services
{
    public class AuthenticationService : AbstractBaseService, IAuthenticationService
    {
        public AuthenticationService(DataContext context) : base(context)
        {
        }

        public async Task<User> LoginUser(string deviceId)
        {
            var user = await Find(deviceId);

            if (user == null)
            {
                return await Create(new User
                {
                    DeviceId = deviceId,
                    WorkHours = 32,
                    VacationDays = 20
                });
            }

            return user;
        }

        public async Task<User?> Find(string deviceId)
        {
            return await Context.Users.SingleOrDefaultAsync(u => u.DeviceId == deviceId);
        }

        public async Task<User?> UpdateUser(Data<User> data)
        {
            var user = await Find(data.DeviceId);
            
            if (user != null && data.Entity != null)
            {
                user.VacationDays = data.Entity.VacationDays;
                user.WorkHours = data.Entity.WorkHours;

                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }

            return user;
        }

        private async Task<User> Create(User user)
        {
            var entityEntry = await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}