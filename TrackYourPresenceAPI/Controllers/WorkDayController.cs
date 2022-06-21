using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackYourPresenceAPI.Data;

namespace TrackYourPresenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkDayController : AbstractBaseController
    {
        public WorkDayController(DataContext context) : base(context)
        {
        }
        
        [HttpGet]
        [Route("currentWeek")]
        public string CurrentWeek()
        {
            var deviceId = GetDeviceId();
            
            
           
            
            
            
            return "Test";
        }
    }
}