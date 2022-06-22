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
        private IAuthenticationService _authenticationService;
        private IWorkDayService _workDayService;
        private IAbsentItemService _absentItemService;

        protected AbstractBaseController(DataContext context)
        {
            _context = context;
            _authenticationService = new AuthenticationService(context);
            _workDayService = new WorkDayService(context, GetAuthenticationService());
            _absentItemService = new AbsentItemService(context, GetAuthenticationService());
        }

        protected async Task<bool> ValidateRequest()
        {
            var apiToken = "emptiness";
            var user = await GetContext().Users.SingleOrDefaultAsync(u => u.DeviceId.ToString() == apiToken);

            return user != null;
        }

        protected DataContext GetContext()
        {
            return _context;
        }

        protected IWorkDayService GetWorkDayService()
        {
            return _workDayService;
        }

        protected IAbsentItemService GetAbsentItemService()
        {
            return _absentItemService;
        }

        protected IAuthenticationService GetAuthenticationService()
        {
            return _authenticationService;
        }

        protected string ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            ;
        }
    }
}