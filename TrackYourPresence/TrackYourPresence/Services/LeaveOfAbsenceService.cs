using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class LeaveOfAbsenceService : ILeaveOfAbsenceService
    {
        private List<LeaveOfAbsence> _leaveOfAbsences = new();

        public LeaveOfAbsenceService()
        {
            _leaveOfAbsences.Add(new()
            {
                Uuid = Guid.NewGuid().ToString(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                TotalOfDays = 1
            });
        }

        public async Task<bool> AddItemAsync(LeaveOfAbsence item)
        {
           _leaveOfAbsences.Add(item);
            return true;
        }

        public async Task<bool> UpdateItemAsync(LeaveOfAbsence item)
        {
            var leaveOfAbsence = await GetItemAsync(item.Uuid);

            if (leaveOfAbsence != null)
            {
                leaveOfAbsence.StartDate = item.StartDate;
                leaveOfAbsence.EndDate = item.EndDate;
                leaveOfAbsence.TotalOfDays = item.TotalOfDays;
                return true;
            }
            
            await AddItemAsync(item);
            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<LeaveOfAbsence> GetItemAsync(string id)
        {
            return _leaveOfAbsences.First(i => i.Uuid == id);
        }

        public async Task<IEnumerable<LeaveOfAbsence>> GetItemsAsync(bool forceRefresh = false)
        {
            return _leaveOfAbsences;
        }
    }
}