using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application.AuthFlowExample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("AuthFlowExample using TwitchTokenGenerator.com");
            Console.WriteLine();

            // These events fire when authorization status changes
            TwitchLib.TwitchAPI.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetected += onAuthorizationDetected;
            TwitchLib.TwitchAPI.ThirdParty.AuthorizationFlow.OnError += onError;

            // auth link is created with a single call that includes the requested scopes
            Console.WriteLine("Getting auth link...");
            List<TwitchLib.Enums.AuthScopes> scopes = new List<TwitchLib.Enums.AuthScopes>() { TwitchLib.Enums.AuthScopes.Chat_Login, TwitchLib.Enums.AuthScopes.User_Read };
            var createdFlow = TwitchLib.TwitchAPI.ThirdParty.AuthorizationFlow.CreateFlow("AuthFlowExample Test Application", scopes);
            Clipboard.SetText(createdFlow.Url);
            Console.WriteLine($"Go here to authorize account (copied to clipboard): {createdFlow.Url}");

            // every 5 seconds, the library will check if the user has completed authorization
            Console.WriteLine();
            Console.WriteLine("Awaiting authorization...");
            TwitchLib.TwitchAPI.ThirdParty.AuthorizationFlow.BeginPingingStatus(createdFlow.Id, 5000);

            // from here, we just wait for the events to fire
            Console.ReadLine();
        }

        // Fires when library detects that authorization has completed
        private static void onAuthorizationDetected(object sender, TwitchLib.Events.API.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetectedArgs e)
        {
            Console.WriteLine("Authorization detected! Below are the details:");
            Console.WriteLine($"Flow Id: {e.Id}");
            Console.WriteLine($"Username: {e.Username}");
            Console.WriteLine($"Token: {e.Token}");
            Console.WriteLine($"Scopes: {String.Join(", ", e.Scopes)}");
        }

        // Fires when twitchtokengenerator.com says an error has happened
        private static void onError(object sender, TwitchLib.Events.API.ThirdParty.AuthorizationFlow.OnErrorArgs e)
        {
            Console.WriteLine($"[ERROR - {e.Error}] {e.Message}");
        }
    }
}
