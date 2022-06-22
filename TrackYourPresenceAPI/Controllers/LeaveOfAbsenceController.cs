using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;
using TrackYourPresenceAPI.Models;

namespace TrackYourPresenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaveOfAbsenceController : AbstractBaseController
    {
        public LeaveOfAbsenceController(DataContext context)
            : base(context)
        {
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> All([FromBody] Data<LeaveOfAbsence> data)
        {
            try
            {
                return Ok(ToJson(await GetLeaveOfAbsenceService().GetAllAsync(data)));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] Data<LeaveOfAbsence> data)
        {
            try
            {
                var workDay = await GetLeaveOfAbsenceService().FindAsync(data);
                return workDay != null ? Ok(ToJson(workDay)) : NotFound();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Data<LeaveOfAbsence> data)
        {
            try
            {
                return Ok(ToJson(await GetLeaveOfAbsenceService().CreateAsync(data)));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Data<LeaveOfAbsence> data)
        {
            try
            {
                return Ok(ToJson(await GetLeaveOfAbsenceService().UpdateAsync(data)));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}