# What is this
This repo contains a simple dotnet core Web API that acts as a mock against which an implementation that depends on [LaunchDarkly](https://launchdarkly.com/) client can be tested.
The Web API provides a simple Server Sent Events server implementation as described by the SSE Protocol.

# What's inside
1. SseServer is a Web API that implements the endpoints required by the [LaunchDarkly .NET client](https://github.com/launchdarkly/.net-client) 
2. SseClient is a console application that connects to the mock server above using [LaunchDarkly's EventSource](https://github.com/launchdarkly/dotnet-eventsource) implementation as well as the [LdClient](https://github.com/launchdarkly/.net-client)

# How to see this in action
1. Run the SseServer using the dotnet CLI or Visual Studio (F5)
2. Point the SseClient to the server above and run using dotnet CLI or Visual Studio.

# References
- Mozilla Dev Network docs on [Server Sent Events](https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events)
- Event Stream [format](https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format)
- StreamData.io [blog](https://streamdata.io/blog/server-sent-events/)
