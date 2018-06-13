using Microsoft.AspNetCore.Mvc;

namespace SseServer.Controllers
{
    [Produces("application/json")]
    [Route("sdk/latest-all")]
    public class FlagsController : Controller
    {
        [HttpGet]
        public object Get()
        {
            return GetFlag("my-setting-enabled");
        }

        [HttpGet("{key}")]
        public object Get(string key)
        {
            return GetFlag("my-setting-enabled");
        }

        private object GetFlag(string flagKey)
        {
            return
                "{\"flags\":{\"my-setting-enabled\":{\"key\":\"my-setting-enabled\",\"version\":20,\"on\":true,\"prerequisites\":[],\"salt\":\"83c254c8a0ce41c7ac479658aedf6a1a\",\"sel\":\"ace837517d9045fdaac74d4a55fa6dad\",\"targets\":[],\"rules\":[],\"fallthrough\":{\"variation\":0},\"offVariation\":1,\"variations\":[true,false],\"trackEvents\":true,\"debugEventsUntilDate\":null,\"deleted\":false}},\"segments\":{}}";
            //return new
            //{
            //    flags = new
            //    {
            //        flagKey = new
            //        {
            //            key = flagKey,
            //            version = 1,
            //            on = false,
            //            prerequisites = new [] { new {}},
            //            salt = "83c254c8a0ce41c7ac479658aedf6a1a",
            //            sel = "ace837517d9045fdaac74d4a55fa6dad",
            //            targets = new [] { new {}},
            //            rules = new [] { new {}},
            //            fallthrough = new
            //            {
            //                variations = 0
            //            },
            //            offVariation = 1,
            //            variations = new [] {true,false},
            //            trackEvents = true,
            //            debugEventsUntilDate = (object)null,
            //            deleted = false
            //        }
            //    },
            //    segments = new
            //    {

            //    }
            //};
        }
    }


}