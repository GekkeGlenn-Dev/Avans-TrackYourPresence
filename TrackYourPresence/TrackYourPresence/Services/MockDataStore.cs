using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackYourPresence.Models;

namespace TrackYourPresence.Services
{
    public class MockDataStore : IDataStore<AbsentItem>
    {
        readonly List<AbsentItem> items;

        public MockDataStore()
        {
            items = new List<AbsentItem>()
            {
                new AbsentItem { Uuid = Guid.NewGuid().ToString(), Date = DateTime.Today, Description="This is an item description." },
                new AbsentItem { Uuid = Guid.NewGuid().ToString(), Date = DateTime.Today, Description="This is an item description." },
                new AbsentItem { Uuid = Guid.NewGuid().ToString(), Date = DateTime.Today, Description="This is an item description." },
                new AbsentItem { Uuid = Guid.NewGuid().ToString(), Date = DateTime.Today, Description="This is an item description." },
                new AbsentItem { Uuid = Guid.NewGuid().ToString(), Date = DateTime.Today, Description="This is an item description." },
                new AbsentItem { Uuid = Guid.NewGuid().ToString(), Date = DateTime.Today, Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(AbsentItem absentItem)
        {
            items.Add(absentItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(AbsentItem absentItem)
        {
            var oldItem = items.Where((AbsentItem arg) => arg.Uuid == absentItem.Uuid).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(absentItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((AbsentItem arg) => arg.Uuid == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<AbsentItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Uuid == id));
        }

        public async Task<IEnumerable<AbsentItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}