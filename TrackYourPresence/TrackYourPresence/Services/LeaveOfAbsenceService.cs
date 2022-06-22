using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class LeaveOfAbsenceService : AbstractServiceBase<LeaveOfAbsence>, ILeaveOfAbsenceService
    {
        public async Task<bool> AddItemAsync(LeaveOfAbsence item)
        {
            var result = await HttpPost(App.GetApiUrl("LeaveOfAbsence/create"), item, null);
            Debug.WriteLine(result.StatusCode);
            Debug.WriteLine(await result.Content.ReadAsStringAsync());
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(LeaveOfAbsence item)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<LeaveOfAbsence> GetItemAsync(string id)
        {
            var response = await HttpGet(App.GetApiUrl("LeaveOfAbsence/find"), null, Guid.Parse(id));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(FromJson<LeaveOfAbsence>(
                        await response.Content.ReadAsStringAsync()
                    ));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return await Task.FromResult(new LeaveOfAbsence());
        }

        public async Task<IEnumerable<LeaveOfAbsence>> GetItemsAsync(bool forceRefresh = false)
        {
            var response = await HttpGet(App.GetApiUrl("LeaveOfAbsence/all"), null, null);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(FromJson<List<LeaveOfAbsence>>(
                        await response.Content.ReadAsStringAsync()
                    ));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return await Task.FromResult(new List<LeaveOfAbsence>());
        }
    }
}