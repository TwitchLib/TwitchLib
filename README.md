<p align="center">
<img src="http://swiftyspiffy.com/img/twitchlib.png" style="max-height: 300px;">
</p>

<p align="center">
<a href="https://travis-ci.org/swiftyspiffy/TwitchLib.svg"><img src="https://api.travis-ci.org/swiftyspiffy/TwitchLib.svg?branch=master" style="max-height: 300px;"></a>
<a href="https://www.microsoft.com/net"><img src="https://img.shields.io/badge/.NET%20Framework-4.5-orange.svg" style="max-height: 300px;"></a>
<img src="https://img.shields.io/badge/Platform-.NET-lightgrey.svg" style="max-height: 300px;" alt="Platform: iOS">
<a href="https://discord.gg/0gHwecaLRAzrRYWi"><img src="https://img.shields.io/badge/Discord-Twitch-red.svg" style="max-height: 300px;"></a>
<a href="http://twitter.com/swiftyspiffy"><img src="https://img.shields.io/badge/Twitter-@swiftyspiffy-blue.svg?style=flat" style="max-height: 300px;"></a>

</p>

## About
TwitchLib is a powerful C# library that allows for interaction with various Twitch services like chat, whispers, API, and PubSub event system. Below are the descriptions of the core components that make up TwitchLib.

* **TwitchClient**: Handles chat and whisper Twitch services. Complete witha suite of events that fire for virtually every piece of data received from Twitch.
* **TwitchApi**: With complete V3 and increasing coverage of V5 endpoints, TwitchApi is a static class that allows for modifying of virtually all Twitch account properties and fetching of Twitch data.
* **TwitchPubSub**: Covers the relatively new Twitch PubSub event system. Currently both topics Twitch supports are supported via this static class.


In addition to the above core components, additional parts of the library include services like FollowerService and MessageThrottler, as well as extensions for TwitchClient which contain wrappers for commonly used client funcitonality like changing color and timing users out.


## Implementing
Below are basic examples of how to utilize each of the core components of TwitchLib. These are C# examples, but this library can also be used in Visual Basic.
#### TwitchClient
```csharp
using TwitchLib;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;

TwitchClient client;
ConnectionCredentials credentials = new ConnectionCredentials("twitch_username", "access_token");

client = new TwitchClient(credentials, "channel_to_join");
client.OnJoinedChannel += onJoinedChannel;
client.OnMessageReceived += onMessageReceived;
client.OnWhisperReceived += onWhisperReceived;
client.OnNewSubscriber += onNewSubscriber;

client.Connect();

private void onJoinedChannel(object sender, OnJoinedChannelArgs e) {
	client.SendMessage("Hey guys! I am a bot connected via TwitchLib!");
}
private void onMessageReceived(object sender, OnMessageReceivedArgs e) {
	if(e.ChatMessage.Message.Contains("badword"))
    	client.TimeoutUser(e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
}
private void onCommandReceived(object sender, OnWhisperCommandReceivedArgs e) {
	if(e.Command == "help")
    	client.SendMessage($"Hi there {e.ChatMessage.Username}! You can view all commands using !command");
}
private void onWhisperReceived(object sender, OnWhisperReceivedArgs e) {
	if(e.WhisperMessage.Username == "my_friend")
    	client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
}
private void onNewSubscriber(object sender, OnNewSubscriberArgs e) {
	if(e.Subscriber.IsTwitchPrime)
		client.SendMessage($"Welcome {} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
    else
    	client.SendMessage($"Welcome {} to the substers! You just earned 500 points!");
}
```
For a complete list of TwitchClient events and calls, click <a href="http://swiftyspiffy.com/TwitchLib/client.html" target="_blank">here</a>
#### TwitchApi
Note: All calls below are synchronous, but TwitchApi supports both synchronous and asynchronous. To access asynch calls, simply append Async. For example, ChannelHasUserSubscribedAsync.
```csharp
using TwitchLib;
using TwitchLib.Models.API;

TwitchApi.SetClientId("my_client_id");
TwitchApi.SetAccessToken("channel_access_token");

bool isSubbed = TwitchApi.Subscriptions.ChannelHasUserSubscribed("user", "channel");
List<Subscription> allSubs = TwitchApi.Subscriptions.GetChannelSubscribers("channel");

List<Follow> follows = TwitchApi.Follows.GetFollowedUsers("channel");
Follow resp = TwitchApi.Follows.FollowChannel("user", "channel");

bool isStreaming = TwitchApi.Streams.BroadcasterOnline("channel");
Channel resp = TwitchApi.Streams.UpdateStreamTitleAndGame("new status", "new game", "channel");

List<User> editors = TwitchApi.Channels.GetChannelEditors("channel");
PostToChannelFeedResponse resp  =TwitchApi.Channels.PostToChannelFeed("This is a new feed post.", true, "channel");

Clip clip = TwitchApi.Clips.GetClipInformation("channel", "ChannelSlugHere");
List<Clip> topClips = TwitchApi.Clips.GetTopClips().Clips;

string communityId = TwitchApi.Communities.CreateCommunity("community_name", "community summary", "community description", "community rules");
TwitchApi.Communities.BanCommunityMember("community_id", "user_id");
```
For a complete list of TwitchApi calls, click <a href="http://swiftyspiffy.com/TwitchLib/api.html" target="_blank">here</a>
#### TwitchPubSub
```csharp
using TwitchLib;

TwitchPubSub pubsub = new TwitchPubSub();
pubsub.OnPubSubServiceConnected += onPubSubConnected;
pubsub.OnListResponse += onPubSubResponse;
pubsub.OnBitsReceived += onPubSubBitsReceived;

pubsub.Connect();

private void onPubSubConnected(object sender, object e) {
	// MY ACCOUNT ID, MY OAUTH
    pubsub.ListenToWhispers(0, "oauth_token");
}
private void onPubSubResponse(object sender, OnListenResponseArgs e) {
	if (e.Successful)
    	MessageBox.Show($"Successfully verified listening to topic: {e.Topic}");
    else
        MessageBox.Show($"Failed to listen! Error: {e.Response.Error}");	
}
private void onPubSubBitsReceived() {
	MessageBox.Show($"Just received {e.BitsUsed} bits from {e.Username}. That brings their total to {e.TotalBitsUsed} bits!");
}
```
For a complete list of TwitchPubSub functionality, click <a href="http://swiftyspiffy.com/TwitchLib/pubsub.html" target="_blank">here</a>

## Installation

### [NuGet](https://www.nuget.org/packages/TwitchLib/)

To install this library via NuGet via NuGet console, use:
```
Install-Package TwitchLib
```
and via Package Manager, simply search:
```
TwitchLib
```
You are also more than welcome to clone/fork this repo and build the library yourself!

## Dependencies

* Newtonsoft.Json 7.0.1+ ([nuget link](https://www.nuget.org/packages/Newtonsoft.Json/7.0.1))
* WebSocketSharp-NonPreRelease ([nuget link](https://www.nuget.org/packages/WebSocketSharp-NonPreRelease/))

## Contributors
 * Cole ([@swiftyspiffy](http://twitter.com/swiftyspiffy))
 * Nadermane ([@Nadermane](http://twitter.com/nadermane))
 * BenWoodford ([BenWoodford](https://github.com/BenWoodford))
 * igor523 ([igor523](https://github.com/igor523))
 * jxlarrea ([jxlarrea](https://github.com/jxlarrea))
 * GlitchHound ([GlitchHound](https://github.com/GlitchHound))
 * PFCKrutonium ([PFCKrutonium](https://github.com/PFCKrutonium))
 * toffaste1337([toffaste1337](https://github.com/toffaste1337))
 * Mr_Examed ([Mr_Examed](https://www.twitch.tv/mr_examed))
 * XuluniX ([XuluniX](https://github.com/XuluniX))
 * prom3theu5 ([@prom3theu5](https://twitter.com/prom3theu5))

## License

This project is available under the MIT license. See the LICENSE file for more info.