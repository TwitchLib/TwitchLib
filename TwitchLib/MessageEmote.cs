using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TwitchLib
{
    /// <summary>Class for maintaining emotes that may be substituted into messages.</summary>
    /// <remarks>
    ///     Also contains helpers to aid in performing actual replacements.
    ///     Expected to be called from the context of <see cref="ChatMessage"/> and <see cref="WhisperMessage"/>.
    /// </remarks>
    public class MessageEmote
    {
        /// <summary>
        ///     Collection of Composite Format Strings which will substitute an
        ///     emote ID to get a URL for an image from the Twitch CDN
        /// </summary>
        /// <remarks>
        ///     These are sorted such that the <see cref="EmoteSize"/> enum can be used as an index,
        ///     eg TwitchEmoteUrls[<see cref="EmoteSize.Small"/>]
        /// </remarks>
        public static readonly ReadOnlyCollection<string> TwitchEmoteUrls = new ReadOnlyCollection<string>(
            new string[3]
            {
                "//static-cdn.jtvnw.net/emoticons/v1/{0}/1.0",
                "//static-cdn.jtvnw.net/emoticons/v1/{0}/2.0",
                "//static-cdn.jtvnw.net/emoticons/v1/{0}/3.0"
            }
        );

        #region Third-Party Emotes
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
            new string[3]
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
            new string[3]
            {
                "//cdn.betterttv.net/emote/{0}/1x",
                "//cdn.betterttv.net/emote/{0}/2x",
                "//cdn.betterttv.net/emote/{0}/3x"
            }
        );
        #endregion Third-Party Emotes

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
            Small,
            /// <summary>
            ///     Medium-sized emotes are not supported by all browsers, and
            ///     FrankerFaceZ does not require emotes to be submitted in this size
            /// </summary>
            Medium,
            /// <summary>
            ///     Large-sized emotes are not supported by all browsers, and
            ///     FrankerFaceZ does not require emotes to be submitted in this size
            ///     </summary>
            Large
        }

        private string _id, _text, _urlOverride, _escapedText, _replacementString;
        private EmoteSource _source;
        private EmoteSize _size;

        /// <summary>
        ///     Emote ID as used by the emote source. Will be provided as {0}
        ///     to be substituted into the indicated URL or <see cref="UrlOverride"/>.
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
        ///     A string which, if provided, will be used instead of any URL.
        ///     "{0}" will be replaced with <see cref="Id"/> wherever it occurs in the string.
        /// </summary>
        /// <remarks>
        ///     While the string may be in Composite Format, note that <see cref="string.Format(string, object[])"/>
        ///     will not have issues if the provided string doesn't utilize the Id parameter.
        /// </remarks>
        public string UrlOverride => _urlOverride;

        /// <summary>
        ///     The string that this emote should be replaced with when it
        ///     appears in a message.
        /// </summary>
        public string ReplacementString => _replacementString;

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
        /// <param name="urlOverride">
        ///     A string (optionally Composite Format with "{0}" representing
        ///     <paramref name="id"/>) which will be used instead of any of the emote URLs.
        ///     Default: null
        /// </param>
        public MessageEmote(string id, string text,
            EmoteSource source = EmoteSource.Twitch,
            EmoteSize size = EmoteSize.Small,
            string urlOverride = null )
        {
            _id = id;
            _text = text;
            _escapedText = Regex.Escape(text);
            _source = source;
            _size = size;
            if (urlOverride != null)
            {
                _replacementString = String.Format(urlOverride, id);
            }
            else
            {
                int sizeIndex = (int)size;
                switch (source)
                {
                    case EmoteSource.BetterTwitchTv:
                        _replacementString = String.Format(BetterTwitchTvEmoteUrls[sizeIndex], id);
                        break;
                    case EmoteSource.FrankerFaceZ:
                        _replacementString = String.Format(FrankerFaceZEmoteUrls[sizeIndex], id);
                        break;
                    default:
                        _replacementString = String.Format(TwitchEmoteUrls[sizeIndex], id);
                        break;
                }
            }
        }
    }

    /// <summary>
    ///     Helper class which maintains a collection of all emotes active for a given channel.
    /// </summary>
    public class MessageEmoteCollection
    {
        private SortedList<string, MessageEmote> _emoteList;
        private const string _basePattern = @"\b{0}\b";
        /// <summary> Do not access directly! Backing field for <see cref="CurrentPattern"/> </summary>
        private string _currentPattern;
        private Regex _regex;
        private EmoteFilterDelegate _preferredFilter;

        /// <summary>
        ///     Property so that we can be confident <see cref="PatternChanged"/>
        ///     always reflects changes to <see cref="CurrentPattern"/>.
        /// </summary>
        private string CurrentPattern
        {
            get
            {
                return _currentPattern;
            }
            set
            {
                if (_currentPattern.Equals(value)) return;
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
                        _regex = new Regex(String.Format(CurrentPattern, ""));
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

        private bool PatternChanged { get; set; } = false;

        private EmoteFilterDelegate CurrentEmoteFilter { get; set; } = AlwaysReplaceEmoteFilter;

        /// <summary>
        ///     Default, empty constructor initializes the list and sets the preferred
        ///     <see cref="EmoteFilterDelegate"/> to <see cref="AlwaysReplaceEmoteFilter(MessageEmote)"/>
        /// </summary>
        public MessageEmoteCollection()
        {
            _emoteList = new SortedList<string, MessageEmote>();
            _preferredFilter = AlwaysReplaceEmoteFilter;
        }

        /// <summary>
        ///     Constructor which specifies a particular preferred <see cref="EmoteFilterDelegate"/>
        /// </summary>
        /// <param name="preferredFilter"></param>
        public MessageEmoteCollection(EmoteFilterDelegate preferredFilter) :this ()
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
            if (_emoteList.ContainsKey(emote.Text)) return;

            _emoteList.Add(emote.Text, emote);
            if (CurrentPattern == null)
            {
                CurrentPattern = String.Format(_basePattern, "(" + emote.EscapedText + "){0}");
            }
            else
            {
                CurrentPattern = String.Format(CurrentPattern, "|(" + emote.EscapedText + "){0}");
            }
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

            // Matches \b(EMOTE)| and \b(EMOTE) without capturing the preceding \b
            // so that the regex still checks for a word boundary before any emote.
            // It's all grouped so that we can OR it with the second pattern.
            string firstEmotePattern = @"((?<=\\b)\(" + emote.EscapedText + @"\)\|?)";
            // Matches |(EMOTE) including the preceding | so that the following emote (if any)
            // merges seamlessly when this section is removed. Again, wrapped in a group.
            string otherEmotePattern = @"(\|\(" + emote.EscapedText + @"\))";
            string newPattern = Regex.Replace(CurrentPattern, firstEmotePattern + "|" + otherEmotePattern, "");
            if (newPattern.Equals(_basePattern)) CurrentPattern = null;
            else CurrentPattern = newPattern;
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
        ///     Replaces all instances of all registered emotes with
        ///     their designated <see cref="MessageEmote.ReplacementString"/>s
        /// </summary>
        /// <param name="originalMessage">
        ///     The original message which needs to be processed for emotes.
        /// </param>
        /// <param name="del">
        ///     An <see cref="EmoteFilterDelegate"/> which returns true if its
        ///     received <see cref="MessageEmote"/> is to be replaced.
        /// </param>
        /// <returns>
        ///     A string where all of the original emote text has been replaced with
        ///     its designated <see cref="MessageEmote.ReplacementString"/>s
        /// </returns>
        public string ReplaceEmotes(string originalMessage, EmoteFilterDelegate del)
        {
            if (del != null) CurrentEmoteFilter = del;
            string newMessage = CurrentRegex.Replace(originalMessage, GetReplacementString);
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
        ///     The default emote filter replaces every <see cref="MessageEmote"/> registered on this list.
        /// </summary>
        /// <param name="emote">An emote which is ignored in this filter.</param>
        /// <returns>true always</returns>
        public static bool AlwaysReplaceEmoteFilter(MessageEmote emote)
        {
            return true;
        }

        /// <summary>
        ///     This emote filter replaces only <see cref="MessageEmote"/>s provided by Twitch.
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
            if (_emoteList.ContainsKey(m.Value))
            {
                MessageEmote emote = _emoteList[m.Value];
                if (CurrentEmoteFilter(emote))
                {
                    return emote.ReplacementString;
                }
            }
            //If the match doesn't exist in the list ("shouldn't happen") or the filter fails, don't replace.
            return m.Value;
        }
    }
}
