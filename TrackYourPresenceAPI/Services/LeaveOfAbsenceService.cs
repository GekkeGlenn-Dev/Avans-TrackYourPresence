using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Services
{
    public class LeaveOfAbsenceService : AbstractBaseService, ILeaveOfAbsenceService
    {
        private readonly IAuthenticationService _authenticationService;

        public LeaveOfAbsenceService(DataContext context, IAuthenticationService authenticationService) : base(context)
        {
            this._authenticationService = authenticationService;
        }

        public async Task<IEnumerable<LeaveOfAbsence>> GetAllAsync(Data<LeaveOfAbsence> data)
        {
            return await Context.LeaveOfAbsences.Where(lof => lof.User.DeviceId == data.DeviceId).ToListAsync();
        }

        public async Task<LeaveOfAbsence?> FindAsync(Data<LeaveOfAbsence> data)
        {
            var user = await _authenticationService.Find(data.DeviceId);

            if (user == null)
            {
                throw new Exception();
            }

            return await Context.LeaveOfAbsences.SingleOrDefaultAsync(
                lof => lof.Uuid.ToString() == data.Uuid.ToString() && lof.User.Id == user.Id
            );
        }

        public async Task<LeaveOfAbsence> CreateAsync(Data<LeaveOfAbsence> data)
        {
            var user = await _authenticationService.Find(data.DeviceId);

            if (user == null || data.Entity == null)
            {
                throw new Exception();
            }

            data.Entity.User = user;
            data.Entity.Uuid = Guid.NewGuid();
            var result = await Context.LeaveOfAbsences.AddAsync(data.Entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<LeaveOfAbsence> UpdateAsync(Data<LeaveOfAbsence> data)
        {
            var user = await _authenticationService.Find(data.DeviceId);

            if (user == null || data.Entity == null)
            {
                throw new Exception();
            }

            data.Entity.User = user;
            var result = Context.LeaveOfAbsences.Update(data.Entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}