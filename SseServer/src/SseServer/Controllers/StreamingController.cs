using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SseServer.Model;

namespace SseServer.Controllers
{
    [Route("all")]
    [Produces("text/event-stream")]
    public class StreamingController : Controller
    {
        public async Task Get()
        {
            // The EventSource interface requires this MIME type
            Response.ContentType = "text/event-stream";
            var streamWriter = new StreamWriter(Response.Body);
            await streamWriter.WriteLineAsync($"event: put");
            await streamWriter.WriteLineAsync($"data: {GetConnectResponse()}");
            await streamWriter.WriteLineAsync();
            await streamWriter.FlushAsync();
            int version = 0;

            while (true)
            {
                await streamWriter.WriteLineAsync("event: patch");
                await streamWriter.WriteLineAsync($"data: {GetResponseToStream(version)}");
                // A blank line marks the end of a message
                await streamWriter.WriteLineAsync();

                // Flushing the stream causes reponse to be sent back
                await streamWriter.FlushAsync();
                await Task.Delay(TimeSpan.FromMilliseconds(2000));
                version++;
            }
        }

        private string GetResponseToStream(int version)
        {
            var streamingResponse = new StreamingResponse
            {
                path = "/flags/my-setting-enabled",
                data = new Data
                {
                    key = "my-setting-enabled",
                    version = version,
                    on = version % 2 == 0,
                    prerequisites = new[] { new object() },
                    salt = "83c254c8a0ce41c7ac479658aedf6a1a",
                    sel = "ace837517d9045fdaac74d4a55fa6dad",
                    targets = new[] { new object() },
                    rules = new[] { new object() },
                    fallthrough = new Fallthrough
                    {
                        variation = 0
                    },
                    offVariation = 1,
                    variations = new[] { true, false },
                    trackEvents = true,
                    debugEventsUntilDate = null,
                    deleted = false
                }
            };

            return JsonConvert.SerializeObject(streamingResponse);
        }

        private string GetConnectResponse()
        {
            var connectResponse = new ConnectResponse
            {
                path = "/",
                data = new Data1
                {
                    segments = new Segments(),
                    flags = new Flags
                    {
                        mysettingenabled = new MySettingEnabled
                        {
                            key = "my-setting-enabled",
                            version = 13,
                            on = false,
                            prerequisites = new []{ new object(), },
                            salt = "83c254c8a0ce41c7ac479658aedf6a1a",
                            sel = "ace837517d9045fdaac74d4a55fa6dad",
                            targets = new []{ new object(), },
                            rules = new []{ new object(), },
                            fallthrough = new Fallthrough
                            {
                                variation = 0
                            },
                            offVariation = 0,
                            variations = new []{true,false},
                            trackEvents = true,
                            debugEventsUntilDate = null,
                            deleted = false
                        }
                    }
                }
            };

            return JsonConvert.SerializeObject(connectResponse);
        }
    }
}
