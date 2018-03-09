dotnet restore
dotnet build ./TwitchLib/TwitchLib.Api/TwitchLib.Api/TwitchLib.Api.csproj --framework netstandard2.0
dotnet build ./TwitchLib/TwitchLib.Client/TwitchLib.Client/TwitchLib.Client.csproj --framework netstandard2.0
dotnet build ./TwitchLib/TwitchLib.PubSub/TwitchLib.PubSub/TwitchLib.PubSub.csproj --framework netstandard2.0