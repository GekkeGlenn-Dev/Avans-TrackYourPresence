using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackYourPresence.Models;
using TrackYourPresenceAPI.Data;

namespace TrackYourPresenceAPI.Services
{
    public class AbsentItemService : IAbsentItemService
    {
        private DataContext _context;

        public AbsentItemService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AbsentItem>> GetAllAsync()
        {
            return await _context.AbsentItems.ToListAsync();
        }

        public async Task<AbsentItem?> FindAsync(string id)
        {
            return await _context.AbsentItems.SingleOrDefaultAsync(w => w.Uuid.ToString() == id);
        }

        public async Task<AbsentItem> CreateAsync(AbsentItem absentItem)
        {
            absentItem.Uuid = Guid.NewGuid();
            var result = await _context.AbsentItems.AddAsync(absentItem);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<AbsentItem> UpdateAsync(AbsentItem absentItem)
        {
            var result = _context.AbsentItems.Update(absentItem);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}