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
        
        protected bool ValidateRequest()
        {
            var headers = Request.Headers;

            var apiToken = Request.Headers["api-token"];

            // GetContext().Users.FirstOrDefaultAsync(u => u.);
            

            return true;
        }

        protected DataContext GetContext()
        {
            return _context;
        }
    }
}