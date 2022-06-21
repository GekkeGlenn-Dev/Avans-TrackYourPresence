using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.Services;

namespace TrackYourPresenceAPI.Controllers
{
    public abstract class AbstractBaseController : ControllerBase
    {
        private DataContext _context;
        private IWorkDayService _workDayService;

        protected AbstractBaseController(DataContext context)
        {
            _context = context;
            _workDayService = new WorkDayService(context);
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

        protected IWorkDayService GetWorkDayService()
        {
            return _workDayService;
        }

        protected string GetDeviceId()
        {
            return Request.Headers["api-token"];
        }

        protected string ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);;
        }
    }
}