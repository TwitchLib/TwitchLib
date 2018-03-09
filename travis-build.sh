dotnet restore
dotnet build ./TwitchLib/TwitchLib.Api/TwitchLib.Api/ --framework netstandard2.0
dotnet build ./TwitchLib/TwitchLib.Client/TwitchLib.Client/ --framework netstandard2.0
dotnet build ./TwitchLib/TwitchLib.PubSub/TwitchLib.PubSub/ --framework netstandard2.0