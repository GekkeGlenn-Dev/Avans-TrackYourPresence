using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkDayController : AbstractBaseController
    {
        public WorkDayController(DataContext context)
            : base(context)
        {
        }

        [HttpGet]
        [Route("currentWeek")]
        public async Task<IActionResult> CurrentWeek([FromBody] Data<WorkDay> data)
        {
            var currentWeek = await GetWorkDayService().GetCurrentWeek(data);
            return Ok(ToJson(currentWeek));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> All([FromBody] Data<WorkDay> data)
        {
            var allWorkDays = await GetWorkDayService().GetAllAsync(data);
            return Ok(ToJson(allWorkDays));
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] Data<WorkDay> data)
        {
            var workDay = await GetWorkDayService().FindAsync(data);
            return workDay != null ? Ok(ToJson(workDay)) : NotFound();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Data<WorkDay> data)
        {
            var result = await GetWorkDayService().CreateAsync(data);
            return Ok(ToJson(result));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Data<WorkDay> data)
        {
            var result = await GetWorkDayService().UpdateAsync(data);
            return Ok(ToJson(result));
        }
    }
}