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
    public class AuthenticationController : AbstractBaseController
    {
        public AuthenticationController(DataContext context)
            : base(context)
        {
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Data<object> data)
        {
            var user = await GetAuthenticationService().LoginUser(data.DeviceId);
            return Ok(ToJson(user));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Login([FromBody] Data<User> data)
        {
            var user = await GetAuthenticationService().UpdateUser(data);
            return user != null
                ? Ok(ToJson(user))
                : NotFound();
        }
    }
}