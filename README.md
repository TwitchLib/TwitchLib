# TwitchLib - Twitch IRC and API C# Library
### Overview
TwitchLib is a C# library that attempts to harness the Twitch IRC and Twitch API into a single package. Using Costura.Fody, all required files are included in a single DLL file that can be imported into a .NET project.  Using TwitchLib, you can connect to a Twitch channel's chat or Twitch's group chat servers to setup chat and whisper bots in just a few lines of code. At the present time, you can also make channel modifications like stream title and game, as well as actions like commercials and and resetting of the stream key. Additionally, the TwitchLib project contains an example project that demonstrates the majority of functionality presented in the library.

### TwitchChatClient
- Initiailized using channel and ConnectionCredentials
- Events:
  * OnConnected - Fires on listening and after joined channel, returns username and channel
  * NewChatMessage - Fires when new chat message arrives, returns ChatMessage
  * NewSubscriber - Fires when new subscriber is announced in chat, returns Subscriber
  * ChannelStateAssigned - Fires when connecting and channel state is received, returns ChannelState
  * ViewerJoined - New viewer/chatter joined the IRC channel room.
  * CommandReceived - Fires when command (uses custom command identifier) is received [untested].
- sendRaw(string message) - Sends RAW IRC message
- sendMessage(string message) - Sends formatted Twitch channel IRC message
- Handled IRC message types

### TwitchWhisperClient
- Initialized using ConnectionCredentials
- Events:
  * OnConnected - Fires on listening and after joined channel, returns username
  * NewWhisperReceived - Fires when a new whisper message is received, returns WhisperMessage
- sendRaw(string message) - Sends RAW IRC message
- sendWhisper(string receiver, string message) - Sends formatted Twitch whisper IRC message

### TwitchAPI
- broadcasterOnline(string channel) - Async function returns bool of whether or not streamer is streaming
- getTwitchChannel(string channel) - Async function returns TwitchCHannel of a specific channel
- userFollowsChannel(string username, string channel) - Async function returns bool if a user follows a channel
- getChatters(string channel) - Aysync function returns list of Chatter objects detailing each chatter in a channel
- updateStreamTitle(string status, string username, string access_token) - Async function that changes stream title
- updateStreamGame(string game, string username, string access_token) - Async function that updates a streams's game
- updateStreamTitleAndGame(string status, string game, string username, string access_token) - Async function that updates a stream's status and game
- resetStreamKey(string username, string access_token) - Async function that resets the stream key of a channel
- getChannelVideos(string channel, [int limit], [int offset], [bool onlyBroadcasts], [bool onlyHLS]) - Async function that returns list of TwitchVIdeo objects
- runCommercial(Valid_Commercial_Lengths length, string username, string access_token) - A sync function that runs a commercial of variable length on a channel

### TwitchLibExample
This project demonstrates a majority of the functionality that TwitchLib allows for.  Includes a basic UI that has textboxes and buttons that allow for required input in the various functions.

### Other Classes
- ChannelState - Contains channel states for: R9K, SubOnly, SlowMode, BroadcasterLanguage, Channel
- ChatMessage - Contains Twitch chat message properties: UserID, Username, DisplayName, ColorHEX, Message, UserType, Channel, Subscriber, Turbo
- ConnectionCredentials - Contains Twitch account credential properties: Host, TwitchUsername, TwitchOAuth, Port
- Subscriber - Contains Subscription announcement properties: Channel, Name, Months
- WhisperMessage - Contains Twitch whisper message properties: ColorHEX, Username, DisplayName, EmoteSet, ThreadID, MessageID, UserID, Turbo, BotUsername, Message
- TwitchAPIClasses/Chatter - Contains chat user properties: Username, UserType
- TwitchAPIClasses/TwitchChannel - Contains Twitch Channel properties: Status, Broadcaster_Language, Display_name, Game, Language, Name, Created_At, Updated_At, Delay, Logo, Background, Profile_Banner, Mature, Partner, ID, Views, Followers
- TwitchAPIClasses/TwitchVideo - Contains a Twitch video properties: Title, Description, Status, ID< Tag_List, Recorded_At, Game, Delete_At, Preview, Broadcast_ID, URL, Length, Views, Is_Muted, FPS, Resolutions, Channel

### Credits and Libraries Utilized
- Costura.Fody / Fody - Takes the projects various DLL files and packages them all in the TwitchLib.dll file, combing and removing potential problems with not having all parts
- Newtonsoft.Json - JSON parsing class.  Used to parse Twitch API calls.
- SmartIRC4Net - Base IRC class.