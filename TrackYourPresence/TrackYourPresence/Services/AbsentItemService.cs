using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Helpers;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class AbsentItemService : AbstractServiceBase<AbsentItem>, IAbsentItemService
    {
        public async Task<bool> AddItemAsync(AbsentItem item)
        {
            var response = await HttpPost(Api.GetApiUrl("AbsentItem/create"), item, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(AbsentItem item)
        {
            var response = await HttpPut(Api.GetApiUrl("AbsentItem/update"), item, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<AbsentItem> GetItemAsync(string id)
        {
            var response = await HttpGet(Api.GetApiUrl("AbsentItem/find"), null, Guid.Parse(id));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(
                        FromJson<AbsentItem>(await response.Content.ReadAsStringAsync())
                    );
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
            var response = await HttpGet(Api.GetApiUrl("AbsentItem/all"), null, null);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await Task.FromResult(
                        FromJson<List<AbsentItem>>(await response.Content.ReadAsStringAsync())
                    );
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