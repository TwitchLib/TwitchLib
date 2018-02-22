<p align="center"> 
<img src="http://swiftyspiffy.com/img/twitchlib.png" style="max-height: 300px;">
</p>

<p align="center">
<a href="https://travis-ci.org/swiftyspiffy/TwitchLib.svg"><img src="https://api.travis-ci.org/swiftyspiffy/TwitchLib.svg?branch=master" style="max-height: 300px;"></a>
<a href="https://www.microsoft.com/net"><img src="https://img.shields.io/badge/.NET%20Framework-4.6.2-orange.svg" style="max-height: 300px;"></a>
<img src="https://img.shields.io/badge/Platform-.NET-lightgrey.svg" style="max-height: 300px;" alt="Platform: iOS">
<a href="https://discord.gg/8NXaEyV"><img src="https://img.shields.io/badge/Discord-TwitchAPI-red.svg" style="max-height: 300px;"></a>
<a href="http://twitter.com/swiftyspiffy"><img src="https://img.shields.io/badge/Twitter-@swiftyspiffy-blue.svg?style=flat" style="max-height: 300px;"></a>
<img src="https://img.shields.io/badge/.NETCore-2.0-ff69b4.svg" style="max-height: 300px;">

</p>

## About
TwitchLib is a powerful C# library that allows for interaction with various Twitch services. Currently supported services are: chat and whisper, API's (v3, v5, helix, undocumented, and third party), PubSub event system, and Twitch Extensions. Below are the descriptions of the core components that make up TwitchLib.

* **[TwitchLib.Client](https://github.com/TwitchLib/TwitchLib.Client)**: Handles chat and whisper Twitch services. Complete with a suite of events that fire for virtually every piece of data received from Twitch. Supports Twitch Rooms. Helper methods also exist for replying to whispers or fetching moderator lists.
* **[TwitchLib.Api](https://github.com/TwitchLib/TwitchLib.Api)**: Complete coverage of v3, v5, and Helix endpoints. The API is now a singleton class. This class allows fetching all publically accessable data as well as modify Twitch services like profiles and streams.
* **[TwitchLib.PubSub](https://github.com/TwitchLib/TwitchLib.PubSub)**: Supports all documented Twitch PubSub topics as well as a few undocumented ones.
* **[TwitchLib.Extension](https://github.com/TwitchLib/TwitchLib.Extension)**: EBS implementation for validating requests, interacting with extension via PubSub and calling Extension endpoints.
* **[TwitchLib.Unity](https://github.com/TwitchLib/TwitchLib.Unity)**: Unity wrapper system for TwitchLib to allow easy usage of TwitchLib in Unity projects!

## Features
* **TwitchLib.Client**:
    * Send formatted or raw messages to Twitch
    * Chat and Whisper command detection and parsing
    * Supports Twitch Rooms
    * Helper methods
        * Timeout, ban, unban users
        * Change chat color and clear chat
        * Invoke stream commercials and hosts
        * Emote only, follower only, subscriber only, and slow mode
        * Reply-to whisper support
	* Handles chat and whisper events:
	    * Connected and Joined channel
	    * Channel and User state changed
	    * Message received/sent
	    * Whisper received/sent
	    * User joined/left
	    * Moderator joined/left
	    * New subscriptions and resubscriptions
	    * Hosting and raid detection
	    * Chat clear, user timeouts, user bans
* **TwitchLib.APi**:
	* Supported Twitch API endpoitns:**v3**, **v5**, **Helix**
	* Supported API sections:
	    * Badges, Bits, Blocks
	    * ChannelFeeds, Channels, Chat, Clips, Collections, Communities,
	    * Follows
	    * Games
	    * Ingests
	    * Root
	    * Search, Streams, Subscriptions
	    * Teams
	    * ThirdParty
	        * Username Changes courtesy of CommanderRoot's [twitch-tools.rootonline.de/](twitch-tools.rootonline.de/)
	        * Moderator Lookup courtesy of 3v's [https://twitchstuff.3v.fi](https://twitchstuff.3v.fi)
	        * Twitch Authenticaiton Flow courtesy of swiftyspiffy's [https://twitchtokengenerator.com/](https://twitchtokengenerator.com/)
	    * Undocumented
	        * ClipChat
	        * TwitchPrimeOffers
	        * ChannelHosts
	        * ChatProperties
	        * ChannelPanels
	        * CSMaps
	        * CSStreams
	        * RecentMessages
	        * Chatters
	        * RecentChannelEvents
	        * ChatUser
	        * UsernameAvailable
	    * User
	    * Videos
	* Services
		* **FollowerService**: Service for detection of new followers in somewhat real time.
		* **LiveStreamMonitor**: Service for detecting when a channel goes online/offline in somewhat real time.
		* **MessageThrottler**: Service to throttle chat messages to abide by Twitch use requirements.
* **TwitchLib.PubSub**:
	* Supported topics:
	    * ChatModeratorActions
	    * BitsEvents
	    * VideoPlayback
	    * Whispers
	    * Subscriptions
* **TwitchLib.Extension**:
	* Developed to be used as part of an EBS (extension back-end service) for a Twitch Extension.
	* Perform API calls related to Extensions (create secret, revoke, channles using extension, etc.)
	* Validation of requests to EBS using extension secret and JWT.
	* Interact with extension via PubSub.

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
    	client.SendMessage($"Hi there {e.WhisperMessage.Username}! You can view all commands using !command");
}
private void onWhisperReceived(object sender, OnWhisperReceivedArgs e) {
	if(e.WhisperMessage.Username == "my_friend")
    	client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
}
private void onNewSubscriber(object sender, OnNewSubscriberArgs e) {
	if(e.Subscriber.IsTwitchPrime)
		client.SendMessage($"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
    else
    	client.SendMessage($"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
}
```
For a complete list of TwitchClient events and calls, click <a href="http://swiftyspiffy.com/TwitchLib/class_twitch_lib_1_1_twitch_client.html" target="_blank">here</a>
#### TwitchAPI
Note: TwitchAPI is now a singleton class that needs to be instantiated with optional clientid and auth token. Please know that failure to provide at least a client id, and sometimes an access token will result in exceptions. The v3 subclass operates almost entirely on Twitch usernames. v5 and Helix operate almost entirely on Twitch user id's. There are methods in all Twitch api versions to get corresponding usernames/userids.

```csharp
using TwitchLib;
using TwitchLib.Models.API;

private static TwitchLib.TwitchAPI api;

api = new TwitchLib.TwitchAPI("client_id", "access_token");

var subscription = await api.Channels.v5.CheckChannelSubscriptionByUserAsync("channel_id", "user_id");
var allSubscriptions = await api.Channels.v5.GetAllSubscribersAsync("channel_id");

var userFollows = await api.Users.v5.GetUserFollowsAsync("user_id");
var channelFollowers = await api.Channels.v5.GetChannelFollowersAsync("channel_id");
bool userFollowsChannel = await api.Users.v5.FollowChannelAsync("user_id", "channel_id");

bool isStreaming = await api.Streams.v5.BroadcasterOnlineAsync("channel_id");]

await api.Channels.v5.UpdateChannelAsync("channel_id", "New stream title", "Stronghold Crusader");
```
For a complete list of TwitchAPI calls, click <a href="http://swiftyspiffy.com/TwitchLib/class_twitch_lib_1_1_twitch_a_p_i.html" target="_blank">here</a>
#### TwitchPubSub
```csharp
using TwitchLib;

TwitchPubSub pubsub = new TwitchPubSub();
pubsub.OnPubSubServiceConnected += onPubSubConnected;
pubsub.OnListenResponse += onPubSubResponse;
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

#### TwitchLib.Extension
See the Extension README <a href="https://github.com/swiftyspiffy/TwitchLib/tree/master/TwitchLib.Extension" target="_blank">here</a>.

## Using TwitchLib with Unity Guide

Björn has kindly created a guide for using TwitchLib with Unity. To view the guide, click <a href="https://github.com/swiftyspiffy/TwitchLib/blob/master/unity.md" target="_blank">here</a>..

## Examples, Applications, Community Work, and Projects

- Recent commits in projects using TwitchLib: [Link](https://github.com/search?o=desc&q=twitchlib&s=indexed&type=Code)
- [Bacon_Donut](https://www.twitch.tv/bacon_donut)'s VOD on building a Twitch bot using TwitchLib: [twitch.tv/videos/115788601](https://www.twitch.tv/videos/115788601)
- Prom3theu5's Conan Exiles Dedicated Server Updater / Service - [Steam](http://steamcommunity.com/app/440900/discussions/6/133256240742484919/) [Github](https://steamcommunity.com/linkfilter/?url=https://github.com/prom3theu5/ConanExilesServerUpdater)
- Von Jan Suchotzki's German Video Tutorial Series - [His Website](http://www.lernmoment.de/csharp-tutorial-deutsch/twitch-client-architektur/) [Youtube](https://www.youtube.com/watch?v=N0OPTdTGgTI)
- DHSean's TwitchAutomator [Reddit](https://www.reddit.com/r/pcgaming/comments/4wfosp/ive_created_an_app_called_twitchautomator_which/) [Github](https://github.com/XenZibe/TwitchUpdater)
- Moerty's Avia Bot, a fully featured bot that is a good example of a built out bot: [https://github.com/Moerty/AivaBot](https://github.com/Moerty/AivaBot)
- [HardlyDifficult](https://www.twitch.tv/hardlydifficult)'s Chat Bot Creation VODs: [#1](https://www.twitch.tv/videos/141096702) [#2](https://www.twitch.tv/videos/141154684) [#3](https://www.twitch.tv/videos/141210422) [#4](https://www.twitch.tv/videos/141535267)
- Prom3theu5's TwitchBotBase - [github.com/prom3theu5/TwitchBotBase](https://github.com/prom3theu5/TwitchBotBase)
- Trump Academi's ASP.NET TwitchLib Implementation - [trumpacademi.com/day-9-twitch-bot-and-mvc-asp-net-implementing-twitchlib/](http://www.trumpacademi.com/day-9-twitch-bot-and-mvc-asp-net-implementing-twitchlib/)
- ubhkid's Zombie Twitch chat game/overlay - [reddit.com/r/Unity3D/comments/6ll10k/i_made_a_game_for_my_twitch_chat/](https://www.reddit.com/r/Unity3D/comments/6ll10k/i_made_a_game_for_my_twitch_chat/)
- FPH SpedV: Virtual Freight Company Tool - [sped-v.de](https://www.sped-v.de/)
- Foglio's Tera Custom Cooldowns - [Tera Custom Cooldowns](https://github.com/Foglio1024/Tera-custom-cooldowns)

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
 * Krutonium ([Krutonium](https://github.com/Krutonium))
 * toffaste1337([toffaste1337](https://github.com/toffaste1337))
 * Mr_Examed ([Mr_Examed](https://www.twitch.tv/mr_examed))
 * XuluniX ([XuluniX](https://github.com/XuluniX))
 * prom3theu5 ([@prom3theu5](https://twitter.com/prom3theu5))
 * Ethan Lu ([elu00](https://github.com/elu00))
 * BeerHawk ([BeerHawk](https://github.com/BeerHawk))
 * Tobias Teske ([Syzuna](https://github.com/Syzuna))
 * LuckyNoS7evin ([luckynos7evin](https://twitch.tv/luckynos7evin))
 * Peter Richter ([DumpsterDoofus](DumpsterDoofus))
 * Mahsaap (@[Mahsaap](https://twitter.com/mahsabludra))

## License

This project is available under the MIT license. See the LICENSE file for more info.

:)
