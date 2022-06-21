using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrackYourPresence.Models;
using Xamarin.Forms.Internals;

namespace TrackYourPresence.Services
{
    public class WorkDayService : AbstractServiceBase, IWorkDayService
    {
        public async Task<bool> AddItemAsync(WorkDay item)
        {
            var response = await HttpPost("https://10.0.2.2:7013/WorkDay/create", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(WorkDay item)
        {
            var response = await HttpPut("https://10.0.2.2:7013/WorkDay/update", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkDay> GetItemAsync(string id)
        {
            var response = await HttpGet("https://10.0.2.2:7013/WorkDay/find?uuid=" + id);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(JsonConvert.DeserializeObject<WorkDay>(
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
            var response = await HttpGet("https://10.0.2.2:7013/WorkDay/all");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(JsonConvert.DeserializeObject<List<WorkDay>>(
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
            var response = await HttpGet("https://10.0.2.2:7013/WorkDay/currentWeek");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(JsonConvert.DeserializeObject<List<WorkDay>>(
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