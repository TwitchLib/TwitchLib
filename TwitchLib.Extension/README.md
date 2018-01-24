<p align="center"> 
<img src="http://swiftyspiffy.com/img/twitchlib.png" style="max-height: 300px;">
</p>



</p>

## About
TwitchLib is a powerful C# library that allows for interaction with various Twitch services. Currently supported services are: chat and whisper, API's (v3, v5, helix, undocumented, and third party), and PubSub event system. Below are the descriptions of the core components that make up TwitchLib.

* **TwitchClient**: Handles chat and whisper Twitch services. Complete with a suite of events that fire for virtually every piece of data received from Twitch. Helper methods also exist for replying to whispers or fetching moderator lists.
* **TwitchAPI**: Complete coverage of v3, v5, and Helix endpoints. The API is now a singleton class. This class allows fetching all publically accessable data as well as modify Twitch services like profiles and streams.
* **TwitchPubSub**: Supports all documented Twitch PubSub topics as well as a few undocumented ones.

## Implementing
Below are basic examples of how to utilize TwitchLib.Extension. These are C# examples, but this library can also be used in Visual Basic.
This is meant to be utilized as part of a web based frontend, with that in mind the following examples are based on new Web applications built in Full framework 4.5.2 and above or dotnet core 2.0 and above.


#### Basics - full framework or dotnet core
```csharp
using TwitchLib.Extension;


Extension extension = new Extension(new ExtensionConfiguration {
		Id = "{{INSERT_YOUR_EXTENSION_CLIENTID}}",
		OwnerId= "{{THE_TWITCH_ID_OF_EXTENSION_OWNER}}",
		VersionNumber ="{{VERSION_NUMBER_YOU_ARE_USING}}",//e.g. 0.0.1
		SecretHandler = new StaticSecretHandler("{{YOUR_EXTENSION_SECRET}}") 
});


//Verify a JWT
string jwt = Request.Headers["x-extension-jwt"];
ClaimsPrincipal user = extension.Verify(jwt, out var validTokenOverlay);
if (user == null) throw new Exception("Not valid");

//Creates a new secret and returns a list of the current available secrets
//It's not recommended to use this method outside of an Extension instance.
//It's recommended that a SecretHandler is created/used, we have created two for you
//StaticSecretHandler or RotatedSecretHandler you can also implement the abstract SecretHandler class
var secrets = await extension.CreateExtensionSecretAsync();

//Gets the current secret from the Extensions secret handler
var currentSecret = extension.GetCurrentSecret();

//Gets the current list of secrets for the Extension from twitch
 secrets = await extension.GetExtensionSecretAsync();

//FOR EMERGENCY USE ONLY - deletes all extension secrets from twitch, your extension will no longer work
var complete = await extension.RevokeExtensionSecretAsync();


var channels = await extension.GetLiveChannelsWithExtensionActivatedAsync(null);

var channelId = "{{BROADCASTER_CHANNEL_ID}}"; //the channel id can be received from the current verified user principal;
//var channelId = user.Claims.FirstOrDefault(y => y.Type == "channel_id").Value


//Within your extension version management -> Extension Capabilities -> Required Configurations
//When a braodcaster has "setup" the extension to your liking and it requires no further mandatory config
//send the channelId and the string set in you version settings to signify this to twitch
complete = await extension.SetExtensionRequiredConfigurationAsync(channelId, "{{WHATEVER_STRING_YOU_SET_IN_VERSION_MANAGEMENT}}");


//Within your extension version management -> Extension Capabilities -> Required Broadcaster Abilities
//An OAuth process may occur, set your scopes in this section under Version Management
//The broadcaster will go through the OAuth process set your Oauth callback in 
//Extension -> Settings-> General -> OAuth Redirect URI
//Then follow the standard/twitch procedure to get an access_token
//when complete pass true or when failed pass false using the method below
complete = await extension.SetExtensionBroadcasterOAuthReceiptAsync(channelId,  false);


//You can send pubsub messages to your extension
//see the twitch example for receiving these message
//the below method send the message
//It is up to you to determine permisions to send messages, the EBS or the current user can send messages
//check https://dev.twitch.tv/docs/extensions/reference#jwt-schema (pubsub_perms) for more info
//if a jwt is not passed it creates one based on the EBS sending the message
complete = await extension.SendExtensionPubSubMessageAsync(channelId, new TwitchLib.Extension.Models.ExtensionPubSubRequest {
	Content_Type = "application/json",
	Targets = [ "broadcast"],
	Message = "{\"foo\":\"bar\"}"
});



```



#### .Net Core
There is currently additional support for .Net core.

#### Startup.cs
```csharp
using TwitchLib.Extension;
using TwitchLib.Extension.Core.Authentication;
using TwitchLib.Extension.Core.ExtensionsManager;

public void ConfigureServices(IServiceCollection services)
{
.....
services.AddAuthentication(options =>
			{
			/*Options removed for space*/
			})
			.AddTwitchExtensionAuth();//<---- this is the bit you're after
			
//Add the extension manager functionality
services.AddTwitchExtensionManager();
.....
}


public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
{
.....
//This allows for multiple extensions.
app.UseTwitchExtensionManager(serviceProvider, new ExtensionsConfiguration
{
	Extensions = new List<ExtensionConfiguration>()
	{
		new ExtensionConfiguration{
                    Id = "{{INSERT_YOUR_EXTENSION_CLIENTID}}",
                    OwnerId= "{{THE_TWITCH_ID_OF_EXTENSION_OWNER}}",
                    VersionNumber ="{{VERSION_NUMBER_YOU_ARE_USING}}",//e.g. 0.0.1
                    SecretHandler = new StaticSecretHandler("{{YOUR_EXTENSION_SECRET}}") 
		},
		new ExtensionConfiguration{
                    Id = "{{INSERT_YOUR_EXTENSION_CLIENTID}}",
                    OwnerId= "{{THE_TWITCH_ID_OF_EXTENSION_OWNER}}",
                    VersionNumber ="{{VERSION_NUMBER_YOU_ARE_USING}}",//e.g. 0.0.1
                    SecretHandler = new StaticSecretHandler("{{YOUR_EXTENSION_SECRET}}") 
		}
	}
});
.....
}
```
#### Example Controller
```csharp
[Produces("application/json")]
[Route("api/[Controller]")]
[Authorize(AuthenticationSchemes = "TwitchExtensionAuth")]
public class ExampleController : Controller
{
	private readonly ExtensionManager _extensionManager;

	public ExampleController(ExtensionManager extensionManager)
	{
		_extensionManager = extensionManager;
	}
	
	[HttpGet]
	[Route("[Action]")]
	public async Task<PuzzleResultModel> Puzzle()
	{
		//as you added the AddTwitchExtensionAuth in Startup and Authorize attribute on the controller the user is Authenticated and Authorized for you
		var user = User;
		
		//User Id - if user is not null but user_id is the user has not given permision to share their user_id
		user.Claims.FirstOrDefault(y => y.Type == "user_id") 
		
		//Channel Id
		user.Claims.FirstOrDefault(y => y.Type == "channel_id")
		
		//Extension Id - This is added at Authorization and is the client id of the extension you were authorized against (if you have multiple extensions)
		user.Claims.FirstOrDefault(y => y.Type == "extension_id")
		
		//Other Claims - "exp", "opaque_user_id", "role", "pubsub_perms"
		
		
		var extensionId = user.Claims.FirstOrDefault(y => y.Type == "extension_id").Value;
		//call an extension method
		_extensionManager.GetExtension(extensionId).GetCurrentSecret(); //All Extension API methods available
	}
}
	
	

```
