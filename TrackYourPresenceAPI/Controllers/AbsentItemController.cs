using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackYourPresence.Models;
using TrackYourPresenceAPI.Data;

namespace TrackYourPresenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AbsentItemController : AbstractBaseController
    {
        public AbsentItemController(DataContext context)
            : base(context)
        {
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> All()
        {
            var deviceId = GetDeviceId();
            var allWorkDays = await GetAbsentItemService().GetAllAsync();
            return Ok(ToJson(allWorkDays));
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Find(string uuid)
        {
            var deviceId = GetDeviceId();
            var workDay = await GetAbsentItemService().FindAsync(uuid);
            return workDay != null ? Ok(ToJson(workDay)) : NotFound();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] AbsentItem item)
        {
            var deviceId = GetDeviceId();
            var result = await GetAbsentItemService().CreateAsync(item);
            return Ok(ToJson(result));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] AbsentItem item)
        {
            var deviceId = GetDeviceId();
            var result = await GetAbsentItemService().UpdateAsync(item);
            return Ok(ToJson(result));
        }
    }
}