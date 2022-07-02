using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Helpers;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class WorkDayService : AbstractServiceBase<WorkDay>, IWorkDayService
    {
        public async Task<bool> AddItemAsync(WorkDay item)
        {
            var response = await HttpPost(Api.GetApiUrl("WorkDay/create"), item, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(WorkDay item)
        {
            var response = await HttpPut(Api.GetApiUrl("WorkDay/update"), item, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkDay> GetItemAsync(string id)
        {
            var response = await HttpGet(Api.GetApiUrl("WorkDay/find"), null, Guid.Parse(id));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(FromJson<WorkDay>(
                        await response.Content.ReadAsStringAsync()
                    ));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return await Task.FromResult(new WorkDay());
        }

        public async Task<IEnumerable<WorkDay>> GetItemsAsync(bool forceRefresh = false)
        {
            var response = await HttpGet(Api.GetApiUrl("WorkDay/all"), null, null);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(FromJson<List<WorkDay>>(
                        await response.Content.ReadAsStringAsync()
                    ));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return await Task.FromResult(new List<WorkDay>());
        }

        public async Task<IEnumerable<WorkDay>> GetCurrentWeekAsync(bool forceRefresh = false)
        {
            var response = await HttpGet(Api.GetApiUrl("WorkDay/currentWeek"), null, null);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(FromJson<List<WorkDay>>(
                        await response.Content.ReadAsStringAsync()
                    ));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return await Task.FromResult(new List<WorkDay>());
        }
    }
}