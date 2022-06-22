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
            Console.WriteLine("CREATE URSER");
            Console.WriteLine(data.DeviceId);
            var user = await GetAuthenticationService().LoginUser(data.DeviceId);
            Console.WriteLine("CREATE URSER");
            return Ok(ToJson(user));
        }
    }
}