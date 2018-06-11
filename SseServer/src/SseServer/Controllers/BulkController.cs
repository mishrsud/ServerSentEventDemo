using Microsoft.AspNetCore.Mvc;
using SseServer.Model;

namespace SseServer.Controllers
{
    [Produces("application/json")]
    [Route("bulk")]
    public class BulkController : Controller
    {
        [HttpPost]
        public void Post(BulkRequest body)
        {
            Response.StatusCode = 202;
            Response.Body.Flush();
        }
    }
}