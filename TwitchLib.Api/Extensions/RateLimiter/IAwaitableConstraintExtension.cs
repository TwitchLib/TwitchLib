namespace TwitchLib.Api.RateLimiter
{
    public static class IAwaitableConstraintExtension
    {
        public static IAwaitableConstraint Compose(this IAwaitableConstraint ac1, IAwaitableConstraint ac2)
        {
            return ac1 == ac2 ? ac1 : new ComposedAwaitableConstraint(ac1, ac2);
        }
    }
}
