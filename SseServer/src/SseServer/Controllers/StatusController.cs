using System;
using Microsoft.AspNetCore.Mvc;

namespace SseServer.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        // GET api/values
        [HttpGet]
        public object Get()
        {
            return new
            {
                Status = $"Alive {DateTime.UtcNow} UTC"
            };
        }
    }
}
