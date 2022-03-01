using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

// TODO: Missing builder
namespace TwitchLib.Client.Models
{
    /// <summary>Class for maintaining emotes that may be substituted into messages.</summary>
    /// <remarks>
    ///     Also contains helpers to aid in performing actual replacements.
    ///     Expected to be called from the context of <see cref="ChatMessage"/> and <see cref="WhisperMessage"/>.
    /// </remarks>
    public class MessageEmote
    {
        /// <summary>
        ///     Delegate allowing Emotes to handle their replacement text on a case-by-case basis.
        /// </summary>
        /// <returns>The string for the calling emote to be replaced with.</returns>
        public delegate string ReplaceEmoteDelegate(MessageEmote caller);

        /// <summary>
        ///     Collection of Composite Format Strings which will substitute an
        ///     emote ID to get a URL for an image from the Twitch CDN
        /// </summary>
        /// <remarks>
        ///     These are sorted such that the <see cref="EmoteSize"/> enum can be used as an index,
        ///     eg TwitchEmoteUrls[<see cref="EmoteSize.Small"/>]
        /// </remarks>
        public static readonly ReadOnlyCollection<string> TwitchEmoteUrls = new ReadOnlyCollection<string>(
            new[]
            {
                "https://static-cdn.jtvnw.net/emoticons/v1/{0}/1.0",
                "https://static-cdn.jtvnw.net/emoticons/v1/{0}/2.0",
                "https://static-cdn.jtvnw.net/emoticons/v1/{0}/3.0"
            }
        );

        #region Third-Party Emote URLs
        //As this is a Twitch Library these could understandably be removed, but they are rather handy

        /// <summary>
        ///     Collection of Composite Format Strings which will substitute an
        ///     emote ID to get a URL for an image from the FFZ CDN
        /// </summary>
        /// <remarks>
        ///     These are sorted such that the <see cref="EmoteSize"/> enum can be used as an index,
        ///     eg FrankerFaceZEmoteUrls[<see cref="EmoteSize.Small"/>]
        ///     WARNING: FrankerFaceZ does not require users to submit all sizes,
        ///     so using something other than Small images may result in broken links!
        /// </remarks>
        public static readonly ReadOnlyCollection<string> FrankerFaceZEmoteUrls = new ReadOnlyCollection<string>(
            new[]
            {
                "//cdn.frankerfacez.com/emoticon/{0}/1",
                "//cdn.frankerfacez.com/emoticon/{0}/2",
                "//cdn.frankerfacez.com/emoticon/{0}/4"
            }
        );

        /// <summary>
        ///     Collection of Composite Format Strings which will substitute
        ///     an emote ID to get a URL for an image from the BTTV CDN
        ///     </summary>
        /// <remarks>
        ///     These are sorted such that the <see cref="EmoteSize"/> enum can be used as an index,
        ///     eg BetterTwitchTvEmoteUrls[<see cref="EmoteSize.Small"/>]
        /// </remarks>
        public static readonly ReadOnlyCollection<string> BetterTwitchTvEmoteUrls = new ReadOnlyCollection<string>(
            new[]
            {
                "//cdn.betterttv.net/emote/{0}/1x",
                "//cdn.betterttv.net/emote/{0}/2x",
                "//cdn.betterttv.net/emote/{0}/3x"
            }
        );
        #endregion Third-Party Emote URLs

        /// <summary>
        ///     A delegate which attempts to match the calling <see cref="MessageEmote"/> with its
        ///     <see cref="EmoteSource"/> and pulls the <see cref="EmoteSize.Small">small</see> version
        ///     of the URL.
        /// </summary>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static string SourceMatchingReplacementText(MessageEmote caller)
        {
            var sizeIndex = (int)caller.Size;
            switch (caller.Source)
            {
                case EmoteSource.BetterTwitchTv:
                    return string.Format(BetterTwitchTvEmoteUrls[sizeIndex], caller.Id);
                case EmoteSource.FrankerFaceZ:
                    return string.Format(FrankerFaceZEmoteUrls[sizeIndex], caller.Id);
                case EmoteSource.Twitch:
                    return string.Format(TwitchEmoteUrls[sizeIndex], caller.Id);
            }
            return caller.Text;
        }

        /// <summary> Enum supplying the supported sites which provide Emote images.</summary>
        public enum EmoteSource
        {
            /// <summary>Emotes hosted by Twitch.tv natively</summary>
            Twitch,

            /// <summary>Emotes hosted by FrankerFaceZ.com</summary>
            FrankerFaceZ,

            /// <summary>Emotes hosted by BetterTTV.net</summary>
            BetterTwitchTv
        }

        /// <summary> Enum denoting the emote sizes</summary>
        public enum EmoteSize
        {
            /// <summary>
            ///     Best support
            ///     Small-sized emotes are the standard size used in the Twitch web chat.
            /// </summary>
            Small = 0,

            /// <summary>
            ///     Medium-sized emotes are not supported by all browsers, and
            ///     FrankerFaceZ does not require emotes to be submitted in this size
            /// </summary>
            Medium = 1,

            /// <summary>
            ///     Large-sized emotes are not supported by all browsers, and
            ///     FrankerFaceZ does not require emotes to be submitted in this size
            ///     </summary>
            Large = 2
        }

        private readonly string _id, _text, _escapedText;
        private readonly EmoteSource _source;
        private readonly EmoteSize _size;

        /// <summary>
        ///     Emote ID as used by the emote source. Will be provided as {0}
        ///     to be substituted into the indicated URL if needed.
        /// </summary>
        public string Id => _id;

        /// <summary>
        ///     Emote text which appears in a message and is meant to be replaced by the emote image.
        /// </summary>
        public string Text => _text;

        /// <summary>
        ///     The specified <see cref="EmoteSource"/> for this emote.
        /// </summary>
        public EmoteSource Source => _source;

        /// <summary>
        ///     The specified <see cref="EmoteSize"/> for this emote.
        /// </summary>
        public EmoteSize Size => _size;

        /// <summary>
        ///    The string to substitute emote text for.
        /// </summary>
        public string ReplacementString => ReplacementDelegate(this);

        /// <summary>
        ///     The desired <see cref="ReplaceEmoteDelegate"/> to use for replacing text in a given emote.
        ///     Default: <see cref="SourceMatchingReplacementText(MessageEmote)"/>
        /// </summary>
        public static ReplaceEmoteDelegate ReplacementDelegate { get; set; } = SourceMatchingReplacementText;

        /// <summary>
        ///     The emote text <see cref="Regex.Escape(string)">regex-escaped</see>
        ///     so that it can be embedded into a regex pattern.
        /// </summary>
        public string EscapedText => _escapedText;

        /// <summary>
        ///     Constructor for a new MessageEmote instance.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier which the emote provider uses to generate CDN URLs.
        /// </param>
        /// <param name="text">
        ///     The string which users type to create this emote in chat.
        /// </param>
        /// <param name="source">
        ///     An <see cref="EmoteSource"/> where an image can be found for this emote.
        ///     Default: <see cref="EmoteSource.Twitch"/>
        /// </param>
        /// <param name="size">
        ///     An <see cref="EmoteSize"/> to pull for this image.
        ///     Default: <see cref="EmoteSize.Small"/>
        /// </param>
        /// <param name="replacementDelegate">
        ///     A string (optionally Composite Format with "{0}" representing
        ///     <paramref name="id"/>) which will be used instead of any of the emote URLs.
        ///     Default: null
        /// </param>
        public MessageEmote(
            string id,
            string text,
            EmoteSource source = EmoteSource.Twitch,
            EmoteSize size = EmoteSize.Small,
            ReplaceEmoteDelegate replacementDelegate = null)
        {
            _id = id;
            _text = text;
            _escapedText = Regex.Escape(text);
            _source = source;
            _size = size;
            if (replacementDelegate != null)
            {
                ReplacementDelegate = replacementDelegate;
            }
        }
    }

    /// <summary>
    ///     Helper class which maintains a collection of all emotes active for a given channel.
    /// </summary>
    public class MessageEmoteCollection
    {
        private readonly SortedList<string, MessageEmote> _emoteList;
        private const string BasePattern = @"(\b{0}\b)";

        /// <summary> Do not access directly! Backing field for <see cref="CurrentPattern"/> </summary>
        private string _currentPattern;
        private Regex _regex;
        private readonly EmoteFilterDelegate _preferredFilter;

        /// <summary>
        ///     Property so that we can be confident <see cref="PatternChanged"/>
        ///     always reflects changes to <see cref="CurrentPattern"/>.
        /// </summary>
        private string CurrentPattern
        {
            get => _currentPattern;
            set
            {
                if (_currentPattern != null && _currentPattern.Equals(value)) return;
                _currentPattern = value;
                PatternChanged = true;
            }
        }

        private Regex CurrentRegex
        {
            get
            {
                if (PatternChanged)
                {
                    if (CurrentPattern != null)
                    {
                        _regex = new Regex(string.Format(CurrentPattern, ""));
                        PatternChanged = false;
                    }
                    else
                    {
                        _regex = null;
                    }
                }
                return _regex;
            }
        }

        private bool PatternChanged { get; set; }

        private EmoteFilterDelegate CurrentEmoteFilter { get; set; } = AllInclusiveEmoteFilter;

        /// <summary>
        ///     Default, empty constructor initializes the list and sets the preferred
        ///     <see cref="EmoteFilterDelegate"/> to <see cref="AllInclusiveEmoteFilter(MessageEmote)"/>
        /// </summary>
        public MessageEmoteCollection()
        {
            _emoteList = new SortedList<string, MessageEmote>();
            _preferredFilter = AllInclusiveEmoteFilter;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor which specifies a particular preferred <see cref="T:TwitchLib.Models.Client.MessageEmoteCollection.EmoteFilterDelegate" />
        /// </summary>
        /// <param name="preferredFilter"></param>
        public MessageEmoteCollection(EmoteFilterDelegate preferredFilter) : this()
        {
            _preferredFilter = preferredFilter;
        }

        /// <summary>
        ///     Adds an <see cref="MessageEmote"/> to the collection. Duplicate emotes
        ///     (judged by <see cref="MessageEmote.Text"/>) are ignored.
        /// </summary>
        /// <param name="emote">The <see cref="MessageEmote"/> to add to the collection.</param>
        public void Add(MessageEmote emote)
        {
            if (!_emoteList.TryGetValue(emote.Text, out var _))
            {
                _emoteList.Add(emote.Text, emote);
            }

            if (CurrentPattern == null)
            {
                //string i = String.Format(_basePattern, "(" + emote.EscapedText + "){0}");
                CurrentPattern = string.Format(BasePattern, emote.EscapedText);
            }
            else
            {
                CurrentPattern = CurrentPattern + "|" + string.Format(BasePattern, emote.EscapedText);
            }
        }

        /// <summary>
        ///     Adds every <see cref="MessageEmote"/> from an <see cref="IEnumerable{T}">enumerable</see>
        ///     collection to the internal collection.
        ///     Duplicate emotes (judged by <see cref="MessageEmote.Text"/>) are ignored.
        /// </summary>
        /// <param name="emotes">A collection of <see cref="MessageEmote"/>s.</param>
        public void Merge(IEnumerable<MessageEmote> emotes)
        {
            var enumerator = emotes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Add(enumerator.Current);
            }

            enumerator.Dispose();
        }

        /// <summary>
        ///     Removes the specified <see cref="MessageEmote"/> from the collection.
        /// </summary>
        /// <param name="emote">The <see cref="MessageEmote"/> to remove.</param>
        public void Remove(MessageEmote emote)
        {
            if (!_emoteList.ContainsKey(emote.Text)) return;

            _emoteList.Remove(emote.Text);

            // These patterns look a lot scarier than they are because we have to look for
            // a lot of regex characters, which means we do a lot of escaping!

            // Matches ^(\bEMOTE\b)| and ^(\bEMOTE\b)
            // It's all grouped so that we can OR it with the second pattern.
            var firstEmotePattern = @"(^\(\\b" + emote.EscapedText + @"\\b\)\|?)";
            // Matches |(\bEMOTE\b) including the preceding | so that the following | and emote (if any)
            // merge seamlessly when this section is removed. Again, wrapped in a group.
            var otherEmotePattern = @"(\|\(\\b" + emote.EscapedText + @"\\b\))";
            var newPattern = Regex.Replace(CurrentPattern, firstEmotePattern + "|" + otherEmotePattern, "");
            CurrentPattern = newPattern.Equals("") ? null : newPattern;
        }

        /// <summary>
        ///     Removes all <see cref="MessageEmote"/>s from the collection.
        /// </summary>
        public void RemoveAll()
        {
            _emoteList.Clear();
            CurrentPattern = null;
        }

        /// <summary>
        ///     Replaces all instances of all registered emotes passing the provided
        ///     <see cref="EmoteFilterDelegate"/> with their designated
        ///     <see cref="MessageEmote.ReplacementString"/>s
        /// </summary>
        /// <param name="originalMessage">
        ///     The original message which needs to be processed for emotes.
        /// </param>
        /// <param name="del">
        ///     An <see cref="EmoteFilterDelegate"/> which returns true if its
        ///     received <see cref="MessageEmote"/> is to be replaced.
        ///     Defaults to <see cref="CurrentEmoteFilter"/>.
        /// </param>
        /// <returns>
        ///     A string where all of the original emote text has been replaced with
        ///     its designated <see cref="MessageEmote.ReplacementString"/>s
        /// </returns>
        public string ReplaceEmotes(string originalMessage, EmoteFilterDelegate del = null)
        {
            if (CurrentRegex == null) return originalMessage;
            if (del != null && del != CurrentEmoteFilter) CurrentEmoteFilter = del;
            var newMessage = CurrentRegex.Replace(originalMessage, GetReplacementString);
            CurrentEmoteFilter = _preferredFilter;
            return newMessage;
        }

        /// <summary>
        ///     A delegate function which, when given a <see cref="MessageEmote"/>,
        ///     determines whether it should be replaced.
        /// </summary>
        /// <param name="emote">The <see cref="MessageEmote"/> to be considered</param>
        /// <returns>true if the <see cref="MessageEmote"/> should be replaced.</returns>
        public delegate bool EmoteFilterDelegate(MessageEmote emote);

        /// <summary>
        ///     The default emote filter includes every <see cref="MessageEmote"/> registered on this list.
        /// </summary>
        /// <param name="emote">An emote which is ignored in this filter.</param>
        /// <returns>true always</returns>
        public static bool AllInclusiveEmoteFilter(MessageEmote emote)
        {
            return true;
        }

        /// <summary>
        ///     This emote filter includes only <see cref="MessageEmote"/>s provided by Twitch.
        /// </summary>
        /// <param name="emote">
        ///     A <see cref="MessageEmote"/> which will be replaced if its
        ///     <see cref="MessageEmote.Source">Source</see> is <see cref="MessageEmote.EmoteSource.Twitch"/>
        /// </param>
        /// <returns>true always</returns>
        public static bool TwitchOnlyEmoteFilter(MessageEmote emote)
        {
            return emote.Source == MessageEmote.EmoteSource.Twitch;
        }

        private string GetReplacementString(Match m)
        {
            if (!_emoteList.ContainsKey(m.Value)) return m.Value;

            var emote = _emoteList[m.Value];
            return CurrentEmoteFilter(emote) ? emote.ReplacementString : m.Value;
            //If the match doesn't exist in the list ("shouldn't happen") or the filter excludes it, don't replace.
        }
    }
}
