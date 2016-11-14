using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Enums
{
    /// <summary>Enum class representing the available chat name color presets.</summary>
    public class ChatColorPresets : StringEnum, IEquatable<ChatColorPresets>
    {
        public static ChatColorPresets Blue { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets Coral { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets DodgerBlue { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets SpringGreen { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets YellowGreen { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets Green { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets OrangeRed { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets Red { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets GoldenRod { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets HotPink { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets CadetBlue { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets SeaGreen { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets Chocolate { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets BlueViolet { get; } = new ChatColorPresets("blue");
        public static ChatColorPresets Firebrick { get; } = new ChatColorPresets("blue");

        private ChatColorPresets(string value) : base(value) { }

        public static ChatColorPresets FromString(string value)
        {
            switch(value)
            {
                case null:
                    return null;
                case "blue":
                    return Blue;
                case "coral":
                    return Coral;
                case "dodgerblue":
                    return DodgerBlue;
                case "springgreen":
                    return SpringGreen;
                case "yellowgreen":
                    return YellowGreen;
                case "green":
                    return Green;
                case "orangered":
                    return OrangeRed;
                case "red":
                    return Red;
                case "goldenrod":
                    return GoldenRod;
                case "hotpink":
                    return HotPink;
                case "cadetblue":
                    return CadetBlue;
                case "seagreen":
                    return SeaGreen;
                case "chocolate":
                    return Chocolate;
                case "blueviolet":
                    return BlueViolet;
                case "firebrick":
                    return Firebrick;
                default:
                    return new ChatColorPresets(value);
            }
        }

        public static implicit operator ChatColorPresets(string value) => FromString(value);
        public static bool operator ==(ChatColorPresets a, ChatColorPresets b) => ((object)a == null && (object)b == null) || (a?.Equals(b) ?? false);
        public static bool operator !=(ChatColorPresets a, ChatColorPresets b) => !(a == b);
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => (obj as ChatColorPresets)?.Equals(this) ?? false;
        public bool Equals(ChatColorPresets type) => type != null && type.Value == Value;
    }
}
