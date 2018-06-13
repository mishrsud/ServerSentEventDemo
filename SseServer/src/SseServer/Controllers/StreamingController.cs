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
        private FeatureFlagStore _featureFlagStore = FeatureFlagStore.GetFeatureFlagStore();
        private static bool _sendUpdate = false;
        private static bool _flagValue;

        public StreamingController()
        {
            _featureFlagStore.OnDataChanged += ProcessDataChange;
        }

        private void ProcessDataChange(object sender, EventArgs e)
        {
            _sendUpdate = true;
            _flagValue = !_flagValue;
        }

        public async Task Get()
        {
            // The EventSource interface requires this MIME type
            Response.ContentType = "text/event-stream";
            var streamWriter = new StreamWriter(Response.Body);
            await streamWriter.WriteLineAsync($"event: put");
            await streamWriter.WriteLineAsync($"data: {GetConnectResponse()}");
            await streamWriter.WriteLineAsync();
            await streamWriter.FlushAsync();
            int version = 1;

            while (true)
            {
                if (!_sendUpdate)
                {
                    await streamWriter.WriteLineAsync(":");
                    await streamWriter.FlushAsync();
                    version++;
                    await Task.Delay(TimeSpan.FromMilliseconds(5000));
                    continue; 
                }
                await streamWriter.WriteLineAsync("event: patch");
                await streamWriter.WriteLineAsync($"data: {GetResponseToStream(version)}");
                // A blank line marks the end of a message
                await streamWriter.WriteLineAsync();

                // Flushing the stream causes reponse to be sent back
                await streamWriter.FlushAsync();
                await Task.Delay(TimeSpan.FromMilliseconds(5000));
                version++;
                _sendUpdate = false;
            }
        }

        private string GetResponseToStream(int version)
        {
            return "{\"path\":\"/flags/my-setting-enabled\",\"data\":{\"key\":\"my-setting-enabled\",\"version\"" +
                   $":{version}" +
                   ",\"on\":" + _flagValue.ToString().ToLowerInvariant() + ",\"prerequisites\":[],\"salt\":\"83c254c8a0ce41c7ac479658aedf6na1a\",\"sel\":\"ace837517d9045fdaac74d4a55fa6dad\",\"targets\":[],\"rules\":[],\"fallthrough\":{\"variation\":0},\"offVariation\":1,\"variations\":[true,false],\"trackEvents\":true,\"debugEventsUntilDate\":null,\"deleted\":false}}";
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
                            version = 1,
                            on = false,
                            prerequisites = new object[0],
                            salt = "83c254c8a0ce41c7ac479658aedf6a1a",
                            sel = "ace837517d9045fdaac74d4a55fa6dad",
                            targets = new object[0],
                            rules = new object[0],
                            fallthrough = new Fallthrough
                            {
                                variation = 0
                            },
                            offVariation = 0,
                            variations = new[] { true, false },
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
