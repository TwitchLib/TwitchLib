## Using TwitchLib with Unity (short version)

**Prerequisites:**
- Unity 2017.1+


#### Setup
- Clone or download repository from https://github.com/swiftyspiffy/TwitchLib
- (Download all dependencies of TwitchLib)
- In Unity: Edit > Project Settings > Player
- Scroll down to Api Compatibility Level and select “.NET 4.6” or higher

*See extensive guide to see how to compile DLL and avoid TLS problems.*


##Using TwitchLib with Unity (extensive versio)

**Prerequisites:**
- Unity 2017.1+
- SourceTree
- Visual Studio

#### Step 1 - Clone TwitchLib repository
*Example with SourceTree*

- Get Repository URL by clicking the Clone or Download button. <a href="http://i.imgur.com/a9tqa8y.gifv" target="_blank">See GIF</a>
- Clone in SourceTree <a href="http://i.imgur.com/Xm5VYDs.gif)

#### Step 2 - Open and build project into DLLs

*Example using Visual Studio*

- Open TwitchLib solution in Visual Studio <a href="http://i.imgur.com/BOAiEme.gifv" target="_blank">See GIF</a>
- Set to Release mode <a href="http://i.imgur.com/z98GzUD.gifv" target="_blank">See GIF</a>
- Build TwitchLib solution <a href="http://i.imgur.com/oXNPJeA.gifv" target="_blank">See GIF</a>
- Confirm that you now have these DLL files: <a href="http://i.imgur.com/HDtUZST.gifv" target="_blank">See GIF</a>
	- **Newtonsoft.Json.dll**
	- **TwitchLib.dll**
	- **websocket-sharp.dll**


#### Step 3 - Create Unity Project

- Create new Unity project <a href="http://i.imgur.com/yyMb9xj.gifv" target="_blank">See GIF</a>
- In Unity select: Edit > Project Settings > Player
- Scroll down to Api Compatibility Level and select “.NET 4.6” or higher <a href="http://i.imgur.com/MgnFHD7.gifv" target="_blank">See GIF</a>
- Import TwitchLib DLLs <a href="http://i.imgur.com/k39e2It.gifv" target="_blank">See GIF</a>


#### Step 4 - Setup Twitch App

*Create a twitch account for your bot*

- Navigate to Connections and register your new app <a href="http://i.imgur.com/uzTY9ER.gifv" target="_blank">See GIF</a>
- Create a new Client Secret and copy to notepad for later <a href="http://i.imgur.com/rpdSoFh.gifv" target="_blank">See GIF</a>
- Create new OAuth token with https://twitchtokengenerator.com and copy to notepad for later <a href="http://i.imgur.com/56OCEFU.gifv" target="_blank">See GIF</a>

#### Step 5 - Setup Unity integration

- Create new script <a href="http://i.imgur.com/49AFzhR.gifv" target="_blank">See GIF</a>
- Gather relevant data <a href="http://i.imgur.com/gRF9VrW.gifv" target="_blank">See GIF (part 1)</a>, <a href="http://i.imgur.com/eoPWS73.gifv" target="_blank">See GIF (part 2)</a>
- Try to make connection <a href="http://i.imgur.com/ccRAbLz.gifv" target="_blank">See GIF</a>
- Attempt to run code <a href="http://i.imgur.com/jG1Z24a.gifv" target="_blank">See GIF</a>
- Fix Mono TLS Exception <a href="http://i.imgur.com/qNUqJMH.gifv" target="_blank">See GIF</a>

TLS Fix:

```csharp
public bool CertificateValidationMonoFix(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
{
    bool isOk = true;

    if (sslPolicyErrors == SslPolicyErrors.None)
    {
        return true;
    }

    foreach (X509ChainStatus status in chain.ChainStatus)
    {
        if (status.Status == X509ChainStatusFlags.RevocationStatusUnknown)
        {
            continue;
        }

        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;

        bool chainIsValid = chain.Build((X509Certificate2)certificate);

        if (!chainIsValid)
        {
            isOk = false;
        }
    }

    return isOk;
}

```

#### Step 6 - Making your first call
- Add first Twitch Hook <a href="http://i.imgur.com/5qIEkRJ.gifv" target="_blank">See GIF</a>
- Test <a href="http://i.imgur.com/1Ee8mQ0.gifv" target="_blank">See GIF</a>

*Now it's just a matter of exploring the API's provided by TwitchLib and hooking into the events you need :)*
