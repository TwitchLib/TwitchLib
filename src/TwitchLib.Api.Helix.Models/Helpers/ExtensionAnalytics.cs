namespace TwitchLib.Api.Helix.Models.Helpers
{
    public class ExtensionAnalytics
    {
        public string Date { get; protected set; }
        public string ExtensionName { get; protected set; }
        public string ExtensionClientId { get; protected set; }
        public int Installs { get; protected set; }
        public int Uninstalls { get; protected set; }
        public int Activations { get; protected set; }
        public int UniqueActiveChannels { get; protected set; }
        public int Renders { get; protected set; }
        public int UniqueRenders { get; protected set; }
        public int Views { get; protected set; }
        public int UniqueViewers { get; protected set; }
        public int UniqueInteractors { get; protected set; }
        public int Clicks { get; protected set; }
        public double ClicksPerInteractor { get; protected set; }
        public double InteractionRate { get; protected set; }

        public ExtensionAnalytics(string row)
        {
            var p = row.Split(',');
            Date = p[0];
            ExtensionName = p[1];
            ExtensionClientId = p[2];
            Installs = int.Parse(p[3]);
            Uninstalls = int.Parse(p[4]);
            Activations = int.Parse(p[5]);
            UniqueActiveChannels = int.Parse(p[6]);
            Renders = int.Parse(p[7]);
            UniqueRenders = int.Parse(p[8]);
            Views = int.Parse(p[9]);
            UniqueViewers = int.Parse(p[10]);
            UniqueInteractors = int.Parse(p[11]);
            Clicks = int.Parse(p[12]);
            ClicksPerInteractor = double.Parse(p[13]);
            InteractionRate = double.Parse(p[14]);
        }
    }
}
