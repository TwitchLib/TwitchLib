using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{ 
    public class ExtensionSlot
    {
        public ExtensionType Type;
        public string Slot;
        public UserExtensionState UserExtensionState;

        public ExtensionSlot(ExtensionType type, string slot, UserExtensionState userExtensionState)
        {
            Type = type;
            Slot = slot;
            UserExtensionState = userExtensionState;
        }
    }
}
