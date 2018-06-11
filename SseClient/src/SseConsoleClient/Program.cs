using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using LaunchDarkly.Client;
using LaunchDarkly.EventSource;

namespace SseConsoleClient
{
    class Program
    {
        private static EventSource _evt;
        private const string TestSdkKey = "sdk-727359b6-4eda-4af1-91b7-5c0e473b238f";
        private const string TestFeatureFlag = "my-setting-enabled";

        static void Main(string[] args)
        {
            // Connect to mock server using LaunchDarkly .NET Client
            ConnectUsingLdClient();

            // Connect to mock server using EventSource from LaunchDarkly
            //ConnectUsingEventSourceAsync().Wait();

            Console.ReadKey();
        }

        private static void ConnectUsingLdClient()
        {
            var props = new Common.Logging.Configuration.NameValueCollection
            {
                { "level", "all" }
            };
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter(props);

            var ldConfig = LaunchDarkly.Client.Configuration
                .Default(TestSdkKey)
                //.WithIsStreamingEnabled(false)
                .WithUri("http://sudawsm:49345/")
                .WithStreamUri("http://sudawsm:49345/")
                .WithEventsUri("http://sudawsm:49345/");

            LdClient client = new LdClient(ldConfig);
            User user = User.WithKey("Default");
            
            for (int i = 0; i < 10; i++)
            {
                var value = client.BoolVariation(TestFeatureFlag, user, false);
                Log(
                    value
                        ? "Showing feature for user {0}, iteration: {1}"
                        : "Not showing feature for user {0}, iteration: {1}", user.Key, i);
                client.Flush();
                Task.Delay(TimeSpan.FromMilliseconds(2000)).Wait();
            }
            Log("Finished, press any key to continue");
            Console.ReadKey();
        }

        private static async Task ConnectUsingEventSourceAsync()
        {
            Log("Starting...");

            //"https://stream.launchdarkly.com/all";
            var url = "http://localhost:49345/all";
            var authKey = $"sdk-{Guid.NewGuid():N}";

            var connectionTimeout =
                TimeSpan.FromMilliseconds(Timeout.Infinite); //Use Timeout.Infinite if you want a connection that does not timeout.
            var requestHeaders = new Dictionary<string, string>
            {
                {"Authorization", TestSdkKey}
            };

            var config = new LaunchDarkly.EventSource.Configuration(
                new Uri(url), 
                null,
                connectionTimeout,
                TimeSpan.FromMilliseconds(1000),
                TimeSpan.FromMinutes(4),
                requestHeaders);

            _evt = new EventSource(config);

            _evt.Opened += Evt_Opened;
            _evt.Error += Evt_Error;
            _evt.CommentReceived += Evt_CommentReceived;
            _evt.MessageReceived += Evt_MessageReceived;
            _evt.Closed += Evt_Closed;

            try
            {
                await _evt.StartAsync();
            }
            catch (Exception ex)
            {
                Log("General Exception: {0}", ex);
            }
        }

        private static void Log(string format, params object[] args)
        {
            Console.WriteLine("{0}: {1}", DateTime.Now, string.Format(format, args));
        }

        private static void Evt_Closed(object sender, StateChangedEventArgs e)
        {
            Log("EventSource Closed. Current State {0}", e.ReadyState);
        }

        private static void Evt_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Log("EventSource Message Received. Event Name: {0}", e.EventName);
            Log("EventSource Message Properties: {0}\tLast Event Id: {1}{0}\tOrigin: {2}{0}\tData: {3}",
                Environment.NewLine, e.Message.LastEventId, e.Message.Origin, e.Message.Data);
        }

        private static void Evt_CommentReceived(object sender, CommentReceivedEventArgs e)
        {
            Log("EventSource Comment Received: {0}", e.Comment);
        }

        private static void Evt_Error(object sender, ExceptionEventArgs e)
        {
            Log("EventSource Error Occurred. Details: {0}", e.Exception.Message);
        }

        private static void Evt_Opened(object sender, StateChangedEventArgs e)
        {
            Log("EventSource Opened. Current State: {0}", e.ReadyState);
        }

    }
}
