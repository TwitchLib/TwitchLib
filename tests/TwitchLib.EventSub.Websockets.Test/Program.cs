using TwitchLib.EventSub.Websockets.Extensions;

namespace TwitchLib.EventSub.Websockets.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddTwitchLibEventSubWebsockets();

                    services.AddHostedService<WebsocketHostedService>();
                });
    }
}
