# TwitchLib - Twitch Chat and API C# Library
### Overview
TwitchLib is a C# library that attempts to harness the Twitch Chat and Twitch API into a single package. Using Costura.Fody, all required files are included in a single DLL file that can be imported into a .NET project.  Using TwitchLib, you can connect to a Twitch channel's chat or Twitch's group chat servers to setup chat and whisper bots in just a few lines of code. At the present time, you can also make channel modifications like stream title and game, as well as actions like commercials and and resetting of the stream key. Additionally, the TwitchLib project contains an example project that demonstrates the majority of functionality presented in the library.

### TwitchChatClient
- Initiailized using channel and ConnectionCredentials
- Events:
  * OnConnected - Fires on listening and after joined channel, returns username and channel
  * NewChatMessage - Fires when new chat message arrives, returns ChatMessage
  * NewSubscriber - Fires when new subscriber is announced in chat, returns Subscriber
  * ChannelStateAssigned - Fires when connecting and channel state is received, returns ChannelState
  * ViewerJoined - New viewer/chatter joined the chat channel room.
  * CommandReceived - Fires when command (uses custom command identifier) is received.
  * MessageSent - Fires when a chat message is sent.
  * UserStateAssigned - Fires when a user state is received.
- sendRaw(string message) - Sends RAW IRC message
- sendMessage(string message) - Sends formatted Twitch channel chat message
- Handled chat message types

### TwitchWhisperClient
- Initialized using ConnectionCredentials
- Events:
  * OnConnected - Fires on listening and after joined channel, returns username
  * NewWhisperReceived - Fires when a new whisper message is received, returns WhisperMessage
  * CommandReceived - Fires when command (uses custom command identifier) is received.
  * WhisperSent - Fires when a whisper is sent.
- sendRaw(string message) - Sends RAW IRC message
- sendWhisper(string receiver, string message) - Sends formatted Twitch whisper message

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
- getChannelHosts(string channel) - Async function that returns a string list of channels hosting a specified channel (undocumented)
- getTeamMembers(string teamName) - Async function that returns a TwitchTeamMember list of all members in a Twitch team (undocumented)
- channelHasUserSubscribed(string username, string channel, string access_token) - Returns true or false on whether or not a user is subscribed to a channel.
- getTwitchStream(string channel) - Returns TwitchStream object containing API data related to a stream
- getTwitchFollower(string channel) - Returns asc or desc list of followers from a specific channel, returns list of TwitchFollower objects.
- getUptime(string channel) - Returns TimeSpan object representing time between creation_at of stream, and now.

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

### Credits and Libraries Utilized
- Costura.Fody / Fody - Takes the projects various DLL files and packages them all in the TwitchLib.dll file, combing and removing potential problems with not having all parts
- Newtonsoft.Json - JSON parsing class.  Used to parse Twitch API calls.
- SmartIRC4Net - Base IRC class.

### License
The MIT License (MIT)

Copyright (c) 2015 swiftyspiffy

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
