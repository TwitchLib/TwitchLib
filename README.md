# TwitchLib - Twitch Chat and API C# Library
### Overview
TwitchLib is a C# library that attempts to harness Twitch Chat and Twitch API into a single package. Using TwitchLib, you can connect to a Twitch channel's chat and send and receive chat messages as well as send and receive whisper messages! You can also fetch general Twitch API data like followers or user details as well as make authenticated channel modifications like stream title and game, as well as actions like commercials and and resetting of the stream key. Additionally, the TwitchLib project contains an example project that demonstrates the majority of functionality presented in the library.

### Sample Implementation
```
TwitchClient client = new TwitchClient(new ConnectionCredentials("my_username", "my_oauth"), "my_channel");

client.OnConnected += clientConnected;
client.OnMessageReceived += clientMessageReceived;
client.OnWhisperCommandReceived += clientWhsiperReceived;

client.SendMessage("A chat message.");
client.SendWhisper("whisper_receiver", "A whisper message.");

TwitchApi.BroadcasterOnline("my_favorite_streamer");
TwitchApi.GetTwitchFollowers("my_favorite_streamer");
TwitchApi.GetSubscriberCount("my_favorite_streamer", "my_favorite_streamer's_access_token");
TwitchApi.RunCommercial(TwitchApi.CommercialLength.Seconds180, "my_favorite_streamer", "my_favorite_streamer's_access_token");
```

### Availability
Available via Nuget: `Install-Package TwitchLib`

[![NuGet Pre Release](https://img.shields.io/nuget/vpre/TwitchLib.svg)](https://www.nuget.org/packages/TwitchLib)

### TwitchClient
- Initiailized using channel and ConnectionCredentials
- Chat Events:
  * OnIncorrectLogin - Fires when an invalid login is returned by Twitch
  * OnConnected - Fires on listening and after joined channel, returns username and channel
  * OnDisconnected - Fires when TwitchClient disconnects.
  * OnMessageReceived - Fires when new chat message arrives, returns ChatMessage
  * OnNewSubscriber - Fires when new subscriber is announced in chat, returns Subscriber
  * OnReSubscriber - Fires when existing subscriber resubscribes, returns ReSubscriber
  * OnChannelStateChanged - Fires when channel state is changed
  * OnViewerJoined - New viewer/chatter joined the chat channel room.
  * OnViewerLeft - Viewer/chatter left (PARTed) the chat channel.
  * OnChatCommandReceived - Fires when command (uses custom command identifier) is received.
  * OnMessageSent - Fires when a chat message is sent.
  * OnUserStateChanged - Fires when a user state is received.
  * OnModeratorJoined - Fires when a moderator joins chat (not necessarily real time)
  * OnModeratorLeft - Fires when a moderator leaves chat (not necessarily real time)
  * OnHostLeft - Fires when a hosted channel goes offline
  * OnExistingUsersDetected - Fires when list of users message is received from Twitch (generally when entering the room)
  * OnHostingStarted - Fires when someone begins hosting the channel the client is connected to.
  * OnHostingStopped - Fires when someone that is hosting channel that client is connected to, stops.
  * OnChatCleared - Fires when a moderator sends a clear chat command (channel).
  * OnViewerTimedout - Fires when client detects a viewer was timedout (moderator, viewer, timeout duration, timeout reason, channel).
  * OnViewerBanned - Fires when client detects a viewer was banned (moderator, viewer, ban reason, channel).
- Whisper Events:
  * OnWhisperReceived - Fires when a new whisper message is received, returns WhisperMessage
  * OnWhisperCommandReceived - Fires when command (uses custom command identifier) is received.
  * OnWhisperSent - Fires when a whisper is sent.
- SendRaw(string message) - Sends RAW IRC message
- SendMessage(string message) - Sends formatted Twitch channel chat message
- SendWhisper(string receiver, string message) - Sends formatted Twitch whisper message
- Handled chat message types
- Disconnect - Disconnects chat client from Twitch IRC
- Reconnect - Reconnects chat client given existing credentials
- JoinedChannel(string channel) - Client will attempt to join passed in channel.
- LeaveChannel(string channel) - Client will attempt to leave channel.

### TwitchAPI
- BroadcasterOnline(string channel) - Async function returns bool of whether or not streamer is streaming
- GetTwitchChannel(string channel) - Async function returns TwitchCHannel of a specific channel
- UserFollowsChannel(string username, string channel) - Async function returns bool if a user follows a channel
- GetChatters(string channel) - Aysync function returns list of Chatter objects detailing each chatter in a channel
- UpdateStreamTitle(string status, string username, string access_token) - Async function that changes stream title
- UpdateStreamGame(string game, string username, string access_token) - Async function that updates a streams's game
- UpdateStreamTitleAndGame(string status, string game, string username, string access_token) - Async function that updates a stream's status and game
- ResetStreamKey(string username, string access_token) - Async function that resets the stream key of a channel
- GetChannelVideos(string channel, [int limit], [int offset], [bool onlyBroadcasts], [bool onlyHLS]) - Async function that returns list of TwitchVIdeo objects
- RunCommercial(Valid_Commercial_Lengths length, string username, string access_token) - A sync function that runs a commercial of variable length on a channel
- GetChannelHosts(string channel) - Async function that returns a string list of channels hosting a specified channel (undocumented)
- GetTeamMembers(string teamName) - Async function that returns a TwitchTeamMember list of all members in a Twitch team (undocumented)
- ChannelHasUserSubscribed(string username, string channel, string access_token) - Returns true or false on whether or not a user is subscribed to a channel.
- GetTwitchStream(string channel) - Returns TwitchStream object containing API data related to a stream
- GetTwitchStreams(List<string> channels) - Returns list of Stream objects for each channel passed in.
- GetTwitchFollower(string channel) - Returns asc or desc list of followers from a specific channel, returns list of TwitchFollower objects.
- GetUser(string username) - Returns a User object which represents a User object Twitch has.
- GetUptime(string channel) - Returns TimeSpan object representing time between creation_at of stream, and now.
- GetChannelFeed(string channel, int limit = 10, string cursor = null) - Returns a FeedResponse which houses all feed posts, comments, reactions, etc.
- SetClientId(string clientId) - Sets ClientId for inclusion in all API calls per Twitch requirement.
- GetFollowedUsers(string channel, int limit = 25, int offset = 0, Common.SortKey sortKey) - Gets a list of users a user follows.
- SearchChannels(string query, int limit = 25, int offset = 0) - Search uses a term for channels and returns a list of channel objects.
- SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null) - Search uses a term for streams and returns a list of stream objects.
- SearchGames(string query, bool live = false) - Search uses a term for games and returns a list of game objects.
- FollowChannel(string username, string channel, string accessToken) - Follows a specific channel.
- UnfollowChannel(string username, string channel, string accessToken) - Unfollows a specific channel.
- GetBlockedList(string username, string accessToken, int limit = 25, int offset = 0) - Returns a list of Block objects, each featuring a user object and an update date/time.
- BlockUser(string username, string blockedUsername, string accessToken) - Blocks user, returns a Block object.
- UnblockUser(string username, string blockedUsername, string accessToken) - Unblocks user.
- GetChannelEditors(string channel, string accessToken) - Retrieves a list of User objects representing users that are channel editors.
- GetChannelBadges(string channel) - Fetches a list of Badge objects representing each badge available in a channel.

### Twitch Services
- FollowerService - Monitors channel for new followers on custom interval and query count values. Fires event when new followers are detected.
- MessageThrottler - Property object that can be assigned to either Chat or Whisper clients, fires events and blocks sending of messages given a specific time period in order to prevent Twitch ToS violations. (OPTIONAL)

### Testing/Parsing Stability
I've recently taken to implementing this class into test applications and connecting them to large Twitch channels to see how the class handles fast moving chat and large TwitchAPI usage.  These are the events/channels I've had the library connected to.
- GamesDoneQuick (several days) - 80,000 - 200,000 concurrent, fixed a number of overflow and outofindex exceptions thrown when TwitchAPI returns service unavailable or TwitchIRC returns incomplete message data

### Examples and Implementations
- TwitchLibExample - This project is included in this repo as a master example project.
- PFCKrutonium's [TwitchieBot](https://github.com/PFCKrutonium/TwitchieBot) - This project implements the bot using VisualBasic.

### Libraries Utilized
- Newtonsoft.Json - JSON parsing class.  Used to parse Twitch API calls.
- SmartIRC4Net - Base IRC class.

### Support/Discussion
The Twitch Discord server has a #developer channel that has constant discussion about developing for the Twitch platform. You can likely get support and discuss ideas there. A link is below:
https://discord.gg/0gHwecaLRAzrRYWi

### Contributors
 * Cole ([@swiftyspiffy](http://twitter.com/swiftyspiffy))
 * Nadermane ([@Nadermane](http://twitter.com/nadermane))
 * BenWoodford ([BenWoodford](https://github.com/BenWoodford))
 * igor523 ([igor523](https://github.com/igor523))
 * jxlarrea ([jxlarrea](https://github.com/jxlarrea))
 * GlitchHound ([GlitchHound](https://github.com/GlitchHound))
 * PFCKrutonium ([PFCKrutonium](https://github.com/PFCKrutonium))
 * toffaste1337([toffaste1337](https://github.com/toffaste1337))
### License
MIT License. &copy; 2016 Cole
