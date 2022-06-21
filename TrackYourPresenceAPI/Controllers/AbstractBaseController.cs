using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackYourPresenceAPI.Data;

namespace TrackYourPresenceAPI.Controllers
{
    public abstract class AbstractBaseController : ControllerBase
    {
        private DataContext _context;

        protected AbstractBaseController(DataContext context)
        {
            _context = context;
        }

        protected Task<bool> ValidateRequest()
        {
            var apiToken = "emptiness"; 
            var user = GetContext().Users.SingleOrDefaultAsync(u => u.DeviceId.ToString() == apiToken);
            
            return Task.FromResult(user != null);
        }

        protected DataContext GetContext()
        {
            return _context;
        }

        protected string GetDeviceId()
        {
            return Request.Headers["api-token"];
        }
    }
}