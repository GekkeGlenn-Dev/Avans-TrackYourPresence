using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackYourPresence.Models;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;

namespace TrackYourPresenceAPI.Services
{
    public class AbsentItemService : AbstractBaseService, IAbsentItemService
    {
        private IAuthenticationService authenticationService;

        public AbsentItemService(DataContext context, IAuthenticationService authenticationService) : base(context)
        {
            this.authenticationService = authenticationService;
        }

        public async Task<IEnumerable<AbsentItem>> GetAllAsync(Data<AbsentItem> data)
        {
            return await Context.AbsentItems.Where(i => i.User.DeviceId == data.DeviceId).ToListAsync();
        }

        public async Task<AbsentItem?> FindAsync(Data<AbsentItem> data)
        {
            var user = await authenticationService.Find(data.DeviceId);

            if (user == null)
            {
                throw new Exception();
            }

            return await Context.AbsentItems.SingleOrDefaultAsync(
                w => w.Uuid.ToString() == data.Uuid.ToString() && w.User.Id == user.Id
            );
        }

        public async Task<AbsentItem> CreateAsync(Data<AbsentItem> data)
        {
            var user = await authenticationService.Find(data.DeviceId);

            if (user == null || data.Entity == null)
            {
                Console.WriteLine("ELLOR");
                throw new Exception();
            }
            Console.WriteLine("CONITUNE");

            data.Entity.User = user;
            data.Entity.Uuid = Guid.NewGuid();
            var result = await Context.AbsentItems.AddAsync(data.Entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<AbsentItem> UpdateAsync(Data<AbsentItem> data)
        {
            var user = await authenticationService.Find(data.DeviceId);

            if (user == null || data.Entity == null)
            {
                throw new Exception();
            }

            data.Entity.User = user;
            var result = Context.AbsentItems.Update(data.Entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}