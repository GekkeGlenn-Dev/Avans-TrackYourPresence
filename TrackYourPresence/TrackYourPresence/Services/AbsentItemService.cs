using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class AbsentItemService : AbstractServiceBase, IAbsentItemService
    {
        public async Task<bool> AddItemAsync(AbsentItem item)
        {
            var response = await HttpPost("https://10.0.2.2:7013/AbsentItem/create", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(AbsentItem item)
        {
            var response = await HttpPut("https://10.0.2.2:7013/AbsentItem/update", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AbsentItem> GetItemAsync(string id)
        {
            var response = await HttpGet("https://10.0.2.2:7013/AbsentItem/find?uuid=" + id);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(JsonConvert.DeserializeObject<AbsentItem>(
                        await response.Content.ReadAsStringAsync()
                    ));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return await Task.FromResult(new AbsentItem());
        }

        public async Task<IEnumerable<AbsentItem>> GetItemsAsync(bool forceRefresh = false)
        {
            var response = await HttpGet("https://10.0.2.2:7013/AbsentItem/all");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(JsonConvert.DeserializeObject<List<AbsentItem>>(
                        await response.Content.ReadAsStringAsync()
                    ));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return await Task.FromResult(new List<AbsentItem>());
        }
    }
}