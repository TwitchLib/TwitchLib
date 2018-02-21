namespace TwitchLib.Api.Events
{
    public class OnAuthorizationFlowErrorArgs
    {
        public int Error { get; set; }
        public string Message { get; set; }
    }
}
