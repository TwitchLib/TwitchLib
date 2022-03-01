namespace TwitchLib.Client.Models.Builders
{
    public sealed class ErrorEventBuilder : IBuilder<ErrorEvent>
    {
        private string _message;

        private ErrorEventBuilder()
        {
        }

        public ErrorEventBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public static ErrorEventBuilder Create()
        {
            return new ErrorEventBuilder();
        }

        public ErrorEvent Build()
        {
            return new ErrorEvent
            {
                Message = _message
            };
        }
    }
}
