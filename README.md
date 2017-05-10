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

* **TwitchClient**: Handles chat and whisper Twitch services. Complete with a suite of events that fire for virtually every piece of data received from Twitch.
* **TwitchAPI**: With complete v3 and v5 endpoints, TwitchAPI is a static asynchronous class that allows for modifying of virtually all Twitch account properties and fetching of Twitch data. The class also sports undocumented endpoints and thirdparty endpoints.
* **TwitchPubSub**: Covers the relatively new Twitch PubSub event system. Currently both topics Twitch supports are supported via this static class.

## Features
* **TwitchClient**:
	* Handles chat interactions and functionality.
	* Events for channel being hosted, chat being cleared, moderators/users entering/leaving chat, etc.
	* Reply to/send chat messages and whispers.
	* Native support for commands and customizable command identifiers (default is "!")
	* Ability to timeout/ban/unban users, change username color, clear chat, play commercials (for partnered streams), turn on emote/follower/sub only mode, and retrive list of moderators/followers.
	* Message throttling handling.
* **Services**:
	* **FollowerService**: Service for detection of new followers.
	* **LiveStreamMonitor**: Service for detecting when a channel goes online/offline
* **TwitchAPI**:
	* Retrieve uptime/current status of stream, posts in channel feed, etc.
	* Retrieve followed channels/check is particular user is a follower or not
	* Search games by viewcount, name, etc.
	* Update stream title/current game
	* Reset stream key, set stream delay
	* Follow/unfollow channels
	* (Partnerd streams only) Retrieve subscriber count/list
	* (Partnered streams) Run commercials
	* Create/moderate/update/search for communities
* **TwitchPubSub**:
	* Chat interactions through Twitch's PubSub system  

## Documentation
#### Doxygen
For complete library documentation, view the doxygen docs <a href="http://swiftyspiffy.com/TwitchLib/index.html" target="_blank">here</a>.
	
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
For a complete list of TwitchClient events and calls, click <a href="http://swiftyspiffy.com/TwitchLib/class_twitch_lib_1_1_twitch_client.html" target="_blank">here</a>
#### TwitchAPI
Note: TwitchAPI is an asynchronous static class. Each subclass of TwitchAPI represents a part of the TwitchAPI. Each part of the TwitchAPI has a v3 and v5 static class. These classes represent Twitch's v3 and v5 API iterations. v5 ONLY uses UserID's, unless specified, and v3 ONLY uses Usernames, unless specified. If a part does not have v3 and v5, this indicates that the part only exists in one of the two versions. Look at the method arguments to determine if the method wants a userid or username.
```csharp
using TwitchLib;
using TwitchLib.Models.API;

TwitchAPI.Settings.ClientId = "my-client-id";
TwitchAPI.Settings.AccessToken = "my-oauth-token";

bool isSubbed = await TwitchAPI.Channels.v5.CheckChannelSubscriptionByUser("channel-id", "user-id");

List<TwitchLib.Models.API.v5.Subscriptions.Subscription> allSubs = await TwitchAPI.Channels.v5.GetAllSubscribers("channel-id");

TwitchLib.Models.API.v5.Channels.ChannelFollowers followers = await TwitchAPI.Channels.v5.GetChannelFollowers("channel-id");

TwitchLib.Models.API.v5.Users.UserFollow follow = await TwitchAPI.Users.v5.FollowChannel("user-id", "channel-id");

bool isStreaming = await TwitchAPI.Streams.v5.BroadcasterOnline("channel-id");

TwitchLib.Models.API.v5.Channels.Channel channel = await TwitchAPI.Channels.v5.UpdateChannel("channel-id", "New status here", "Game here");
```
For a complete list of TwitchAPI calls, click <a href="http://swiftyspiffy.com/TwitchLib/class_twitch_lib_1_1_twitch_a_p_i.html" target="_blank">here</a>
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
For a complete list of TwitchPubSub functionality, click <a href="http://swiftyspiffy.com/TwitchLib/class_twitch_lib_1_1_twitch_pub_sub.html" target="_blank">here</a>

## Examples, Applications, and Community Work

- TwitchLib-API-Tester: Repo testing application for TwitchAPI and Services: [Link](https://github.com/swiftyspiffy/TwitchLib/tree/master/TwitchLib-API-Tester)
- TwitchLib-Client-PubSub-Tester: Repo testing application for TwitchClient and TwitchPubSub: [Link](https://github.com/swiftyspiffy/TwitchLib/tree/master/TwitchLib-Client-PubSub-Tester)
- [Bacon_Donut](https://www.twitch.tv/bacon_donut)'s VOD on building a Twitch bot using TwitchLib: [twitch.tv/videos/115788601](https://www.twitch.tv/videos/115788601)
- Prom3theu5's Conan Exiles Dedicated Server Updater / Service - [Steam](http://steamcommunity.com/app/440900/discussions/6/133256240742484919/) [Github](https://steamcommunity.com/linkfilter/?url=https://github.com/prom3theu5/ConanExilesServerUpdater)
- Von Jan Suchotzki's German Video Tutorial Series - [His Website](http://www.lernmoment.de/csharp-tutorial-deutsch/twitch-client-architektur/) [Youtube](https://www.youtube.com/watch?v=N0OPTdTGgTI)
- DHSean's TwitchAutomator [Reddit](https://www.reddit.com/r/pcgaming/comments/4wfosp/ive_created_an_app_called_twitchautomator_which/) [Github](https://github.com/XenZibe/TwitchUpdater)
- PFCKrutonium's [TwitchieBot](https://github.com/PFCKrutonium/TwitchieBot) - This project implements the bot using VisualBasic.
- Moerty's Avia Bot, a fully featured bot that is a good example of a built out bot: [https://github.com/Moerty/AivaBot](https://github.com/Moerty/AivaBot)
- [HardlyDifficult](https://www.twitch.tv/hardlydifficult)'s Chat Bot Creation VODs: [#1](https://www.twitch.tv/videos/141096702) [#2](https://www.twitch.tv/videos/141154684) [#3](https://www.twitch.tv/videos/141210422) [#4](https://www.twitch.tv/videos/141535267)

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

* Newtonsoft.Json 7.0.1+ ([nuget link](https://www.nuget.org/packages/Newtonsoft.Json/7.0.1)) ([GitHub](https://github.com/JamesNK/Newtonsoft.Json))
* WebSocketSharp-NonPreRelease ([nuget link](https://www.nuget.org/packages/WebSocketSharp-NonPreRelease/)) ([GitHub](https://github.com/sta/websocket-sharp))

## Contributors
 * Cole ([@swiftyspiffy](http://twitter.com/swiftyspiffy))
 * SkyHuk ([@SkyHukTV](http://twitter.com/SkyHukTV))
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
 * Ethan Lu ([elu00](https://github.com/elu00))
 * BeerHawk ([BeerHawk](https://github.com/BeerHawk))
 * Tobias Teske ([Syzuna](https://github.com/Syzuna))

## License

This project is available under the MIT license. See the LICENSE file for more info.
