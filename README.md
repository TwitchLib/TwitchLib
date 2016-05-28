# TwitchLib - Twitch Chat and API C# Library
### Overview
TwitchLib is a C# library that attempts to harness the Twitch Chat and Twitch API into a single package. Using Costura.Fody, all required files are included in a single DLL file that can be imported into a .NET project.  Using TwitchLib, you can connect to a Twitch channel's chat or Twitch's group chat servers to setup chat and whisper bots in just a few lines of code. At the present time, you can also make channel modifications like stream title and game, as well as actions like commercials and and resetting of the stream key. Additionally, the TwitchLib project contains an example project that demonstrates the majority of functionality presented in the library.

### Availability
Available via Nuget: `Install-Package TwitchLib`

### TwitchChatClient
- Initiailized using channel and ConnectionCredentials
- Events:
  * OnIncorrectLogin - Fires when an invalid login is returned by Twitch
  * OnConnected - Fires on listening and after joined channel, returns username and channel
  * OnMessageReceived - Fires when new chat message arrives, returns ChatMessage
  * OnSubscriber - Fires when new subscriber is announced in chat, returns Subscriber
  * OnChannelStateChanged - Fires when channel state is changed
  * OnViewerJoined - New viewer/chatter joined the chat channel room.
  * OnCommandReceived - Fires when command (uses custom command identifier) is received.
  * OnMessageSent - Fires when a chat message is sent.
  * OnUserStateChanged - Fires when a user state is received.
  * OnModeratorJoined - Fires when a moderator joins chat (not necessarily real time)
  * OnHostLeft - Fires when a hosted channel goes offline
- SendRaw(string message) - Sends RAW IRC message
- SendMessage(string message) - Sends formatted Twitch channel chat message
- Handled chat message types

### TwitchWhisperClient
- Initialized using ConnectionCredentials
- Events:
  * OnIncorrectLogin - Fires when an invalid login is returned by Twitch
  * OnConnected - Fires on listening and after joined channel, returns username
  * OnWhisperReceived - Fires when a new whisper message is received, returns WhisperMessage
  * OnCommandReceived - Fires when command (uses custom command identifier) is received.
  * OnWhisperSent - Fires when a whisper is sent.
- SendRaw(string message) - Sends RAW IRC message
- SendWhisper(string receiver, string message) - Sends formatted Twitch whisper message

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
- GetTwitchFollower(string channel) - Returns asc or desc list of followers from a specific channel, returns list of TwitchFollower objects.
- GetUptime(string channel) - Returns TimeSpan object representing time between creation_at of stream, and now.

### TwitchLibExample
This project demonstrates a majority of the functionality that TwitchLib allows for.  Includes a basic UI that has textboxes and buttons that allow for required input in the various functions. Supports reading twitch account details from credentials.txt (one per line: username, oauth, channel).

### Other Classes
- ChannelState - Contains channel states for: R9K, SubOnly, SlowMode, BroadcasterLanguage, Channel
- ChatMessage - Contains Twitch chat message properties: UserID, Username, DisplayName, ColorHEX, Message, UserType, Channel, Subscriber, Turbo
- ConnectionCredentials - Contains Twitch account credential properties: Host, TwitchUsername, TwitchOAuth, Port
- Subscriber - Contains Subscription announcement properties: Channel, Name, Months
- WhisperMessage - Contains Twitch whisper message properties: ColorHEX, Username, DisplayName, EmoteSet, ThreadID, MessageID, UserID, Turbo, BotUsername, Message
- TwitchAPIClasses/Chatter - Contains chat user properties: Username, UserType
- TwitchAPIClasses/TwitchChannel - Contains Twitch Channel properties: Status, Broadcaster_Language, Display_name, Game, Language, Name, Created_At, Updated_At, Delay, Logo, Background, Profile_Banner, Mature, Partner, ID, Views, Followers
- TwitchAPIClasses/TwitchVideo - Contains a Twitch video properties: Title, Description, Status, ID< Tag_List, Recorded_At, Game, Delete_At, Preview, Broadcast_ID, URL, Length, Views, Is_Muted, FPS, Resolutions, Channel
- TwitchAPIClasses/TwitchTeamMember - Contains Twitch Team Member properties: Name, Description, Title, Meta_Game, Display_Name, Link, Follower_Count, Total_Views, Current_Views,
Status, ImageSizes
- UserState - Contains state of a user that recently connected, properties: ColorHEX, DisplayName, EmoteSet, Channel, Subscriber, Turbo, UserType

### Testing/Parsing Stability
I've recently taken to implementing this class into test applications and connecting them to large Twitch channels to see how the class handles fast moving chat and large TwitchAPI usage.  These are the events/channels I've had the library connected to.
- GamesDoneQuick (several days) - 80,000 - 200,000 concurrent, fixed a number of overflow and outofindex exceptions thrown when TwitchAPI returns service unavailable or TwitchIRC returns incomplete message data

### Credits and Libraries Utilized
- Costura.Fody / Fody - Takes the projects various DLL files and packages them all in the TwitchLib.dll file, combing and removing potential problems with not having all parts
- Newtonsoft.Json - JSON parsing class.  Used to parse Twitch API calls.
- SmartIRC4Net - Base IRC class.

### Contributors
 * Cole ([@swiftyspiffy](http://twitter.com/swiftyspiffy))
 * Nadermane ([@Nadermane](http://twitter.com/nadermane))
 
### License
MIT License. &copy; 2016 Cole