using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.Models;

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
        public async Task<IActionResult> CurrentWeek()
        {
            var deviceId = GetDeviceId();
            var currentWeek = await GetWorkDayService().GetCurrentWeek();
            return Ok(ToJson(currentWeek));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> All()
        {
            var deviceId = GetDeviceId();
            var allWorkDays = await GetWorkDayService().GetAllAsync();
            return Ok(ToJson(allWorkDays));
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Find(string uuid)
        {
            var deviceId = GetDeviceId();
            var workDay = await GetWorkDayService().FindAsync(uuid);
            return workDay != null ? Ok(ToJson(workDay)) : NotFound();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] WorkDay workDay)
        {
            var deviceId = GetDeviceId();
            var result = await GetWorkDayService().CreateAsync(workDay);
            return Ok(ToJson(result));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] WorkDay workDay)
        {
            var deviceId = GetDeviceId();
            var result = await GetWorkDayService().UpdateAsync(workDay);
            return Ok(ToJson(result));
        }
    }
}