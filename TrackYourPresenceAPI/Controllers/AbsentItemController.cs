using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackYourPresence.Models;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;

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
        public async Task<IActionResult> All([FromBody] Data<AbsentItem> data)
        {
            try
            {
                return Ok(ToJson(await GetAbsentItemService().GetAllAsync(data)));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> Find([FromBody] Data<AbsentItem> data)
        {
            try
            {
                var workDay = await GetAbsentItemService().FindAsync(data);
                return workDay != null ? Ok(ToJson(workDay)) : NotFound();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Data<AbsentItem> data)
        {
            try
            {
                return Ok(ToJson(await GetAbsentItemService().CreateAsync(data)));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Data<AbsentItem> data)
        {
            try
            {
                return Ok(ToJson(await GetAbsentItemService().UpdateAsync(data)));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}