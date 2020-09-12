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
TwitchLib is a powerful C# library that allows for interaction with various Twitch services. Currently supported services are: chat and whisper, API's (v5, helix, undocumented, and third party), PubSub event system, and Twitch Extensions. Below are the descriptions of the core components that make up TwitchLib.

Talk directly with us on Discord. https://discord.gg/8NXaEyV

* **[TwitchLib.Client](https://github.com/TwitchLib/TwitchLib.Client)**: Handles chat and whisper Twitch services. Complete with a suite of events that fire for virtually every piece of data received from Twitch. Helper methods also exist for replying to whispers or fetching moderator lists.
* **[TwitchLib.Api](https://github.com/TwitchLib/TwitchLib.Api)**: Complete coverage of v5, and Helix endpoints. The API is now a singleton class. This class allows fetching all publicly accessible data as well as modify Twitch services like profiles and streams.
* **[TwitchLib.PubSub](https://github.com/TwitchLib/TwitchLib.PubSub)**: Supports all documented Twitch PubSub topics as well as a few undocumented ones.
* **[TwitchLib.Extension](https://github.com/TwitchLib/TwitchLib.Extension)**: EBS implementation for validating requests, interacting with extension via PubSub and calling Extension endpoints.
* **[TwitchLib.Unity](https://github.com/TwitchLib/TwitchLib.Unity)**: Unity wrapper system for TwitchLib to allow easy usage of TwitchLib in Unity projects!
* **[TwitchLib.Webhook](https://github.com/TwitchLib/TwitchLib.Webhook)**: Implements ASP.NET Core Webhook Receiver with TwitchLib. [Requires DotNet Core 2.1+]

## Features
* **TwitchLib.Client**:
    * Send formatted or raw messages to Twitch
    * Chat and Whisper command detection and parsing
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
	* Supported Twitch API endpoints:**v5**, **Helix**
	* Supported API sections:
	    * Badges, Bits, Blocks
	    * ChannelFeeds, Channels, Chat, Clips, Collections, Communities,
	    * Follows
	    * Games
	    * HypeTrain
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
	    * (Dev) Channel Points
* **TwitchLib.Extension**:
	* Developed to be used as part of an EBS (extension back-end service) for a Twitch Extension.
	* Perform API calls related to Extensions (create secret, revoke, channels using extension, etc.)
	* Validation of requests to EBS using extension secret and JWT.
	* Interact with extension via PubSub.

## Documentation
#### Doxygen
For complete library documentation, view the doxygen docs <a href="http://swiftyspiffy.com/TwitchLib/index.html" target="_blank">here</a>.
	
## Implementing
Below are basic examples of how to utilize each of the core components of TwitchLib. These are C# examples. 
*NOTE: Twitchlib.API currently does not support Visual Basic. UPDATE: PR'd Visual Basic fix but requires testing by someone that uses it.*

#### Twitchlib.Client - CSharp
```csharp
using System;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Console.ReadLine();
        }
    }

    class Bot
    {
        TwitchClient client;
	
        public Bot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials("twitch_username", "access_token");
	    var clientOptions = new ClientOptions
                {
                    MessagesAllowedInPeriod = 750,
                    ThrottlingPeriod = TimeSpan.FromSeconds(30)
                };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, "channel");

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }
  
        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }
  
        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }
  
        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.Contains("badword"))
                client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
        }
        
        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username == "my_friend")
                client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }
        
        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            else
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }
    }
}
```

#### Twitchlib.Client - Visual Basic
```vb
Imports System
Imports TwitchLib.Client
Imports TwitchLib.Client.Enums
Imports TwitchLib.Client.Events
Imports TwitchLib.Client.Extensions
Imports TwitchLib.Client.Models

Module Module1

    Sub Main()
        Dim bot As New Bot()
        Console.ReadLine()
    End Sub

    Friend Class Bot
        Private client As TwitchClient

        Public Sub New()
            Dim credentials As New ConnectionCredentials("twitch_username", "Token")

            client = New TwitchClient()
            client.Initialize(credentials, "Channel")

            AddHandler client.OnJoinedChannel, AddressOf onJoinedChannel
            AddHandler client.OnMessageReceived, AddressOf onMessageReceived
            AddHandler client.OnWhisperReceived, AddressOf onWhisperReceived
            AddHandler client.OnNewSubscriber, AddressOf onNewSubscriber
            AddHandler client.OnConnected, AddressOf Client_OnConnected

            client.Connect()
        End Sub
        Private Sub Client_OnConnected(ByVal sender As Object, ByVal e As OnConnectedArgs)
            Console.WriteLine($"Connected to {e.AutoJoinChannel}")
        End Sub
        Private Sub onJoinedChannel(ByVal sender As Object, ByVal e As OnJoinedChannelArgs)
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!")
            client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!")
        End Sub

        Private Sub onMessageReceived(ByVal sender As Object, ByVal e As OnMessageReceivedArgs)
            If e.ChatMessage.Message.Contains("badword") Then
                client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!")
            End If
        End Sub
        Private Sub onWhisperReceived(ByVal sender As Object, ByVal e As OnWhisperReceivedArgs)
            If e.WhisperMessage.Username = "my_friend" Then
                client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!")
            End If
        End Sub
        Private Sub onNewSubscriber(ByVal sender As Object, ByVal e As OnNewSubscriberArgs)
            If e.Subscriber.SubscriptionPlan = SubscriptionPlan.Prime Then
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!")
            Else
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!")
            End If
        End Sub
    End Class


End Module
```


For a complete list of TwitchClient events and calls, click <a href="https://swiftyspiffy.com/TwitchLib/Client/index.html" target="_blank">here</a>
#### Twitchlib.API - CSharp
Note: TwitchAPI is now a singleton class that needs to be instantiated with optional clientid and auth token. Please know that failure to provide at least a client id, and sometimes an access token will result in exceptions. The v3 subclass operates almost entirely on Twitch usernames. v5 and Helix operate almost entirely on Twitch user id's. There are methods in all Twitch api versions to get corresponding usernames/userids.

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;

using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.V5.Models.Subscriptions;

namespace Example
{
    class Program
    {
        private static TwitchAPI api;

        private void Main()
        {
            api = new TwitchAPI();
            api.Settings.ClientId = "client_id";
            api.Settings.AccessToken = "access_token";
        }

        private async Task ExampleCallsAsync()
        {
            //Checks subscription for a specific user and the channel specified.
            Subscription subscription = await api.V5.Channels.CheckChannelSubscriptionByUserAsync("channel_id", "user_id");

            //Gets a list of all the subscritions of the specified channel.
            List<Subscription> allSubscriptions = await api.V5.Channels.GetAllSubscribersAsync("channel_id");

            //Get channels a specified user follows.
            GetUsersFollowsResponse userFollows = await api.Helix.Users.GetUsersFollowsAsync("user_id");

            //Get Specified Channel Follows
            var channelFollowers = await api.V5.Channels.GetChannelFollowersAsync("channel_id");

            //Return bool if channel is online/offline.
            bool isStreaming = await api.V5.Streams.BroadcasterOnlineAsync("channel_id");

            //Update Channel Title/Game
            await api.V5.Channels.UpdateChannelAsync("channel_id", "New stream title", "Stronghold Crusader");
        }
    }
}
```

#### Twitchlib.API - Visual Basic
```vb
Imports System.Collections.Generic
Imports System.Threading.Tasks

Imports TwitchLib.Api
Imports TwitchLib.Api.Models.Helix.Users.GetUsersFollows
Imports TwitchLib.Api.Models.Helix.Users.GetUsers
Imports TwitchLib.Api.Models.v5.Subscriptions

Module Module1

    Public api As TwitchAPI

    Sub Main()
        api = New TwitchAPI()
        api.Settings.ClientId = "Client_id"
        api.Settings.AccessToken = "access_token"
        streaming().Wait()
        getchanfollows().Wait()
        getusersubs().Wait()
        getnumberofsubs().Wait()
        getsubstochannel().Wait()
        Console.ReadLine()
    End Sub

    Private Async Function getusersubs() As Task
        'Checks subscription for a specific user and the channel specified.
        Dim subscription As Subscription = Await api.Channels.v5.CheckChannelSubscriptionByUserAsync("channel_id", "user_id")
        Console.WriteLine("User subed: " + subscription.User.Name.ToString)
    End Function

    Private Async Function streaming() As Task
        'shows if the channel is streaming or not (true/false)
        Dim isStreaming As Boolean = Await api.Streams.v5.BroadcasterOnlineAsync("channel_id")
        If isStreaming = True Then
            Console.WriteLine("Streaming")
        Else
            Console.WriteLine("Not Streaming")
        End If
    End Function

    Private Async Function chanupdate() As Task
        'Update Channel Title/Game
        'not used this yet
        Await api.Channels.v5.UpdateChannelAsync("channel_id", "New stream title", "Stronghold Crusader")
    End Function

    Private Async Function getchanfollows() As Task
        'Get Specified Channel Follows
        Dim channelFollowers = Await api.Channels.v5.GetChannelFollowersAsync("channel_id")
        Console.WriteLine(channelFollowers.Total.ToString)
    End Function

    Private Async Function getchanuserfollow() As Task
        'Get channels a specified user follows.
        Dim userFollows As GetUsersFollowsResponse = Await api.Users.helix.GetUsersFollowsAsync("user_id")
        Console.WriteLine(userFollows.TotalFollows.ToString)
    End Function

    Private Async Function getnumberofsubs() As Task
        'Get the number of subs to your channel
        Dim numberofsubs = Await api.Channels.v5.GetChannelSubscribersAsync("channel_id")
        Console.WriteLine(numberofsubs.Total.ToString)
    End Function

    Private Async Function getsubstochannel() As Task
        'Gets a list of all the subscritions of the specified channel.
        Dim allSubscriptions As List(Of Subscription) = Await api.Channels.v5.GetAllSubscribersAsync("channel_id")
        Dim num As Integer
        For num = 0 To allSubscriptions.Count - 1
            Console.WriteLine(allSubscriptions.Item(num).User.Name.ToString)
        Next num
    End Function

End Module
```
For a complete list of TwitchAPI calls, click <a href="https://swiftyspiffy.com/TwitchLib/Api/index.html" target="_blank">here</a>
#### Twitchlib.PubSub
```csharp
using System;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;

namespace TwitchLibPubSubExample
{
    class Program
    {
        private TwitchPubSub client;
        
        static void Main(string[] args)
        {
            Run();
        }
        
        private void Run()
        {
            client = new TwitchPubSub();

            client.OnPubSubServiceConnected += onPubSubServiceConnected;
            client.OnListenResponse += onListenResponse;
            client.OnStreamUp += onStreamUp;
            client.OnStreamDown += onStreamDown;

            client.ListenToVideoPlayback("channelUsername");
            client.ListenToBitsEvents("channelTwitchID");
            
            client.Connect();
        }
        

        private void onPubSubServiceConnected(object sender, EventArgs e)
        {
            // SendTopics accepts an oauth optionally, which is necessary for some topics
            client.SendTopics();
        }
        
        private void onListenResponse(object sender, OnListenResponseArgs e)
        {
            if (!e.Successful)
                throw new Exception($"Failed to listen! Response: {e.Response}");
        }

        private void onStreamUp(object sender, OnStreamUpArgs e)
        {
            Console.WriteLine($"Stream just went up! Play delay: {e.PlayDelay}, server time: {e.ServerTime}");
        }

        private void onStreamDown(object sender, OnStreamDownArgs e)
        {
            Console.WriteLine($"Stream just went down! Server time: {e.ServerTime}");
        }
    }
}
```
For a complete list of TwitchPubSub functionality, click <a href="https://swiftyspiffy.com/TwitchLib/PubSub/index.html" target="_blank">here</a>

#### TwitchLib.Extension
See the Extension README <a href="https://github.com/TwitchLib/TwitchLib.Extension" target="_blank">here</a>.

## Examples, Applications, Community Work, and Projects

*NOTE: Use these projects as a reference, they are NOT guaranteed to be up-to-date.*

*If you have any issues using these examples, please indicate what example you are referencing in the Issue or in Discord.*

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
 * Syzuna ([Syzuna](https://github.com/Syzuna))
 * LuckyNoS7evin ([luckynos7evin](https://twitch.tv/luckynos7evin))
 * Peter Richter ([DumpsterDoofus](DumpsterDoofus))
 * Mahsaap (@[Mahsaap](https://twitter.com/mahsabludra))

## Credits and Other Project Licenses
 * TwitchChatSharp - https://github.com/3ventic/TwitchChatSharp
   * We used a good portion of the parsing in this project to improve our parsing. Special thanks to [@3ventic](https://twitter.com/3ventic).

## License

This project is available under the MIT license. See the LICENSE file for more info.

:)
