using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TwitchLib.Client.Internal
{
    /// <summary>
    /// Class detailing Rfc2812 specifications
    /// </summary>
    public sealed class Rfc2812
    {
        // nickname   =  ( letter / special ) *8( letter / digit / special / "-" )
        // letter     =  %x41-5A / %x61-7A       ; A-Z / a-z
        // digit      =  %x30-39                 ; 0-9
        // special    =  %x5B-60 / %x7B-7D
        //                  ; "[", "]", "\", "`", "_", "^", "{", "|", "}"
        /// <summary>
        /// The nickname regex
        /// </summary>
        private static readonly Regex NicknameRegex = new Regex(@"^[A-Za-z\[\]\\`_^{|}][A-Za-z0-9\[\]\\`_\-^{|}]+$", RegexOptions.Compiled);

        /// <summary>
        /// Prevents a default instance of the <see cref="Rfc2812" /> class from being created.
        /// </summary>
        private Rfc2812()
        {
        }

        /// <summary>
        /// Checks if the passed nickname is valid according to the RFC
        /// Use with caution, many IRC servers are not conform with this!
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns><c>true</c> if [is valid nickname] [the specified nickname]; otherwise, <c>false</c>.</returns>
        public static bool IsValidNickname(string nickname)
        {
            return !string.IsNullOrEmpty(nickname) &&
                   NicknameRegex.Match(nickname).Success;
        }

        /// <summary>
        /// Pass message.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>System.String.</returns>
        public static string Pass(string password)
        {
            return $"PASS {password}";
        }

        /// <summary>
        /// Nick message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns>System.String.</returns>
        public static string Nick(string nickname)
        {
            return $"NICK {nickname}";
        }

        /// <summary>
        /// User message.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="usermode">The usermode.</param>
        /// <param name="realname">The realname.</param>
        /// <returns>System.String.</returns>
        public static string User(string username, int usermode, string realname)
        {
            return $"USER {username} {usermode} * :{realname}";
        }

        /// <summary>
        /// Oper message.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <returns>System.String.</returns>
        public static string Oper(string name, string password)
        {
            return $"OPER {name} {password}";
        }

        /// <summary>
        /// Privmsg message.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="message">The message.</param>
        /// <returns>System.String.</returns>
        public static string Privmsg(string destination, string message)
        {
            return $"PRIVMSG {destination} :{message}";
        }

        /// <summary>
        /// Notice message.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="message">The message.</param>
        /// <returns>System.String.</returns>
        public static string Notice(string destination, string message)
        {
            return $"NOTICE {destination} :{message}";
        }

        /// <summary>
        /// Join message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>System.String.</returns>
        public static string Join(string channel)
        {
            return $"JOIN {channel}";
        }

        /// <summary>
        /// Join message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <returns>System.String.</returns>
        public static string Join(string[] channels)
        {
            return $"JOIN {string.Join(",", channels)}";
        }

        /// <summary>
        /// Join message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string Join(string channel, string key)
        {
            return $"JOIN {channel} {key}";
        }

        /// <summary>
        /// Join message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="keys">The keys.</param>
        /// <returns>System.String.</returns>
        public static string Join(string[] channels, string[] keys)
        {
            return $"JOIN {string.Join(",", channels)} {string.Join(",", keys)}";
        }

        /// <summary>
        /// Part message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>System.String.</returns>
        public static string Part(string channel)
        {
            return $"PART {channel}";
        }

        /// <summary>
        /// Part message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <returns>System.String.</returns>
        public static string Part(string[] channels)
        {
            return $"PART {string.Join(",", channels)}";
        }

        /// <summary>
        /// Part message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="partmessage">The partmessage.</param>
        /// <returns>System.String.</returns>
        public static string Part(string channel, string partmessage)
        {
            return $"PART {channel} :{partmessage}";
        }

        /// <summary>
        /// Part message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="partmessage">The partmessage.</param>
        /// <returns>System.String.</returns>
        public static string Part(string[] channels, string partmessage)
        {
            return $"PART {string.Join(",", channels)} :{partmessage}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="nickname">The nickname.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string channel, string nickname)
        {
            return $"KICK {channel} {nickname}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string channel, string nickname, string comment)
        {
            return $"KICK {channel} {nickname} :{comment}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="nickname">The nickname.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string[] channels, string nickname)
        {
            return $"KICK {string.Join(",", channels)} {nickname}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string[] channels, string nickname, string comment)
        {
            return $"KICK {string.Join(",", channels)} {nickname} :{comment}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="nicknames">The nicknames.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string channel, string[] nicknames)
        {
            return $"KICK {channel} {string.Join(",", nicknames)}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="nicknames">The nicknames.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string channel, string[] nicknames, string comment)
        {
            return $"KICK {channel} {string.Join(",", nicknames)} :{comment}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="nicknames">The nicknames.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string[] channels, string[] nicknames)
        {
            return $"KICK {string.Join(",", channels)} {string.Join(",", nicknames)}";
        }

        /// <summary>
        /// Kick message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="nicknames">The nicknames.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>System.String.</returns>
        public static string Kick(string[] channels, string[] nicknames, string comment)
        {
            return $"KICK {string.Join(",", channels)} {string.Join(",", nicknames)} :{comment}";
        }

        /// <summary>
        /// Motd message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Motd()
        {
            return "MOTD";
        }

        /// <summary>
        /// Motd message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Motd(string target)
        {
            return $"MOTD {target}";
        }

        /// <summary>
        /// Luser message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Lusers()
        {
            return "LUSERS";
        }

        /// <summary>
        /// Luser message.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns>System.String.</returns>
        public static string Lusers(string mask)
        {
            return $"LUSER {mask}";
        }

        /// <summary>
        /// Lusers
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Lusers(string mask, string target)
        {
            return $"LUSER {mask} {target}";
        }

        /// <summary>
        /// Version message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Version()
        {
            return "VERSION";
        }

        /// <summary>
        /// Version message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Version(string target)
        {
            return $"VERSION {target}";
        }

        /// <summary>
        /// Stats message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Stats()
        {
            return "STATS";
        }

        /// <summary>
        /// Stats message.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>System.String.</returns>
        public static string Stats(string query)
        {
            return $"STATS {query}";
        }

        /// <summary>
        /// Stats message.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Stats(string query, string target)
        {
            return $"STATS {query} {target}";
        }

        /// <summary>
        /// Links message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Links()
        {
            return "LINKS";
        }

        /// <summary>
        /// Links message.
        /// </summary>
        /// <param name="servermask">The servermask.</param>
        /// <returns>System.String.</returns>
        public static string Links(string servermask)
        {
            return $"LINKS {servermask}";
        }

        /// <summary>
        /// Links message.
        /// </summary>
        /// <param name="remoteserver">The remoteserver.</param>
        /// <param name="servermask">The servermask.</param>
        /// <returns>System.String.</returns>
        public static string Links(string remoteserver, string servermask)
        {
            return $"LINKS {remoteserver} {servermask}";
        }

        /// <summary>
        /// Time message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Time()
        {
            return "TIME";
        }

        /// <summary>
        /// Time message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Time(string target)
        {
            return $"TIME {target}";
        }

        /// <summary>
        /// Connect message.
        /// </summary>
        /// <param name="targetserver">The targetserver.</param>
        /// <param name="port">The port.</param>
        /// <returns>System.String.</returns>
        public static string Connect(string targetserver, string port)
        {
            return $"CONNECT {targetserver} {port}";
        }

        /// <summary>
        /// Connect message.
        /// </summary>
        /// <param name="targetserver">The targetserver.</param>
        /// <param name="port">The port.</param>
        /// <param name="remoteserver">The remoteserver.</param>
        /// <returns>System.String.</returns>
        public static string Connect(string targetserver, string port, string remoteserver)
        {
            return $"CONNECT {targetserver} {port} {remoteserver}";
        }

        /// <summary>
        /// Trace message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Trace()
        {
            return "TRACE";
        }

        /// <summary>
        /// Trace message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Trace(string target)
        {
            return $"TRACE {target}";
        }

        /// <summary>
        /// Admin message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Admin()
        {
            return "ADMIN";
        }

        /// <summary>
        /// Admin message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Admin(string target)
        {
            return $"ADMIN {target}";
        }

        /// <summary>
        /// Info message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Info()
        {
            return "INFO";
        }

        /// <summary>
        /// Info message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Info(string target)
        {
            return $"INFO {target}";
        }

        /// <summary>
        /// Servlist message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Servlist()
        {
            return "SERVLIST";
        }

        /// <summary>
        /// Servlist message.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns>System.String.</returns>
        public static string Servlist(string mask)
        {
            return $"SERVLIST {mask}";
        }

        /// <summary>
        /// Servlist message.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        public static string Servlist(string mask, string type)
        {
            return $"SERVLIST {mask} {type}";
        }

        /// <summary>
        /// Squery message.
        /// </summary>
        /// <param name="servicename">The servicename.</param>
        /// <param name="servicetext">The servicetext.</param>
        /// <returns>System.String.</returns>
        public static string Squery(string servicename, string servicetext)
        {
            return $"SQUERY {servicename} :{servicename}";
        }

        /// <summary>
        /// List message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string List()
        {
            return "LIST";
        }

        /// <summary>
        /// List message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>System.String.</returns>
        public static string List(string channel)
        {
            return $"LIST {channel}";
        }

        /// <summary>
        /// List message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <returns>System.String.</returns>
        public static string List(string[] channels)
        {
            return $"LIST {string.Join(",", channels)}";
        }

        /// <summary>
        /// List message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string List(string channel, string target)
        {
            return $"LIST {channel} {target}";
        }

        /// <summary>
        /// List message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string List(string[] channels, string target)
        {
            return $"LIST {string.Join(",", channels)} {target}";
        }

        /// <summary>
        /// Names message
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Names()
        {
            return "NAMES";
        }

        /// <summary>
        /// Names message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>System.String.</returns>
        public static string Names(string channel)
        {
            return $"NAMES {channel}";
        }

        /// <summary>
        /// Names message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <returns>System.String.</returns>
        public static string Names(string[] channels)
        {
            return $"NAMES {string.Join(",", channels)}";
        }

        /// <summary>
        /// Names message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Names(string channel, string target)
        {
            return $"NAMES {channel} {target}";
        }

        /// <summary>
        /// Names message.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Names(string[] channels, string target)
        {
            return $"NAMES {string.Join(",", channels)} {target}";
        }

        /// <summary>
        /// Topic message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>System.String.</returns>
        public static string Topic(string channel)
        {
            return $"TOPIC {channel}";
        }

        /// <summary>
        /// Topic message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="newtopic">The newtopic.</param>
        /// <returns>System.String.</returns>
        public static string Topic(string channel, string newtopic)
        {
            return $"TOPIC {channel} :{newtopic}";
        }

        /// <summary>
        /// Mode message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Mode(string target)
        {
            return $"MODE {target}";
        }

        /// <summary>
        /// Mode message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="newmode">The newmode.</param>
        /// <returns>System.String.</returns>
        public static string Mode(string target, string newmode)
        {
            return $"MODE {target} {newmode}" + target + " " + newmode;
        }

        /// <summary>
        /// Mode message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="newModes">The new modes.</param>
        /// <param name="newModeParameters">The new mode parameters.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">newModes
        /// or
        /// newModeParameters</exception>
        /// <exception cref="ArgumentException">newModes and newModeParameters must have the same size.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Length - Mode change list is too large (&gt; {maxModeChanges}</exception>
        public static string Mode(string target, string[] newModes, string[] newModeParameters)
        {
            if (newModes == null)
            {
                throw new ArgumentNullException(nameof(newModes));
            }
            if (newModeParameters == null)
            {
                throw new ArgumentNullException(nameof(newModeParameters));
            }
            if (newModes.Length != newModeParameters.Length)
            {
                throw new ArgumentException("newModes and newModeParameters must have the same size.");
            }

            StringBuilder newMode = new StringBuilder(newModes.Length);
            StringBuilder newModeParameter = new StringBuilder();
            // as per RFC 3.2.3, maximum is 3 modes changes at once
            const int maxModeChanges = 3;
            if (newModes.Length > maxModeChanges)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(target.Length),
                    newModes.Length,
                    $"Mode change list is too large (> {maxModeChanges})."
                );
            }

            for (int i = 0; i <= newModes.Length; i += maxModeChanges)
            {
                for (int j = 0; j < maxModeChanges; j++)
                {
                    if (i + j >= newModes.Length)
                    {
                        break;
                    }
                    newMode.Append(newModes[i + j]);
                }

                for (int j = 0; j < maxModeChanges; j++)
                {
                    if (i + j >= newModeParameters.Length)
                    {
                        break;
                    }
                    newModeParameter.Append(newModeParameters[i + j]);
                    newModeParameter.Append(" ");
                }
            }

            if (newModeParameter.Length <= 0) return Mode(target, newMode.ToString());

            // remove trailing space
            newModeParameter.Length--;
            newMode.Append(" ");
            newMode.Append(newModeParameter);

            return Mode(target, newMode.ToString());
        }

        /// <summary>
        /// Service message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <param name="distribution">The distribution.</param>
        /// <param name="info">The information.</param>
        /// <returns>System.String.</returns>
        public static string Service(string nickname, string distribution, string info)
        {
            return $"SERVICE {nickname} * {distribution} * * :{info}";
        }

        /// <summary>
        /// Invite message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <param name="channel">The channel.</param>
        /// <returns>System.String.</returns>
        public static string Invite(string nickname, string channel)
        {
            return $"INVITE {nickname} {channel}";
        }

        /// <summary>
        /// Who message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Who()
        {
            return "WHO";
        }

        /// <summary>
        /// Who message.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns>System.String.</returns>
        public static string Who(string mask)
        {
            return $"WHO {mask}";
        }

        /// <summary>
        /// Who message.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <param name="ircop">if set to <c>true</c> [ircop].</param>
        /// <returns>System.String.</returns>
        public static string Who(string mask, bool ircop)
        {
            return ircop ? $"WHO {mask} o" : $"WHO {mask}";
        }

        /// <summary>
        /// Whois message.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns>System.String.</returns>
        public static string Whois(string mask)
        {
            return $"WHOIS {mask}";
        }

        /// <summary>
        /// Whois message.
        /// </summary>
        /// <param name="masks">The masks.</param>
        /// <returns>System.String.</returns>
        public static string Whois(string[] masks)
        {
            return $"WHOIS {string.Join(",", masks)}";
        }

        /// <summary>
        /// Whois message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="mask">The mask.</param>
        /// <returns>System.String.</returns>
        public static string Whois(string target, string mask)
        {
            return $"WHOIS {target} {mask}";
        }

        /// <summary>
        /// Whois message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="masks">The masks.</param>
        /// <returns>System.String.</returns>
        public static string Whois(string target, string[] masks)
        {
            return $"WHOIS {target} {string.Join(",", masks)}";
        }

        /// <summary>
        /// Whowas message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns>System.String.</returns>
        public static string Whowas(string nickname)
        {
            return $"WHOWAS {nickname}";
        }

        /// <summary>
        /// Whowas message.
        /// </summary>
        /// <param name="nicknames">The nicknames.</param>
        /// <returns>System.String.</returns>
        public static string Whowas(string[] nicknames)
        {
            return $"WHOWAS {string.Join(",", nicknames)}";
        }

        /// <summary>
        /// Whowas message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.String.</returns>
        public static string Whowas(string nickname, string count)
        {
            return $"WHOWAS {nickname} {count} ";
        }

        /// <summary>
        /// Whowas message.
        /// </summary>
        /// <param name="nicknames">The nicknames.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.String.</returns>
        public static string Whowas(string[] nicknames, string count)
        {
            return $"WHOWAS {string.Join(",", nicknames)} {count} ";
        }

        /// <summary>
        /// Whowas message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <param name="count">The count.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Whowas(string nickname, string count, string target)
        {
            return $"WHOWAS {nickname} {count} {target}";
        }

        /// <summary>
        /// Whowas message.
        /// </summary>
        /// <param name="nicknames">The nicknames.</param>
        /// <param name="count">The count.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Whowas(string[] nicknames, string count, string target)
        {
            return $"WHOWAS {string.Join(",", nicknames)} {count} {target}";
        }

        /// <summary>
        /// Kill message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>System.String.</returns>
        public static string Kill(string nickname, string comment)
        {
            return $"KILL {nickname} :{comment}";
        }

        /// <summary>
        /// Ping message.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns>System.String.</returns>
        public static string Ping(string server)
        {
            return $"PING {server}";
        }

        /// <summary>
        /// Ping message.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="server2">The server2.</param>
        /// <returns>System.String.</returns>
        public static string Ping(string server, string server2)
        {
            return $"PING {server} {server2}";
        }

        /// <summary>
        /// Pong message.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns>System.String.</returns>
        public static string Pong(string server)
        {
            return $"PONG {server}";
        }

        /// <summary>
        /// Pong message.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="server2">The server2.</param>
        /// <returns>System.String.</returns>
        public static string Pong(string server, string server2)
        {
            return $"PONG {server} {server2}";
        }

        /// <summary>
        /// Error message.
        /// </summary>
        /// <param name="errormessage">The errormessage.</param>
        /// <returns>System.String.</returns>
        public static string Error(string errormessage)
        {
            return $"ERROR :{errormessage}";
        }

        /// <summary>
        /// Away message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Away()
        {
            return "AWAY";
        }

        /// <summary>
        /// Away message.
        /// </summary>
        /// <param name="awaytext">The awaytext.</param>
        /// <returns>System.String.</returns>
        public static string Away(string awaytext)
        {
            return $"AWAY :{awaytext}";
        }

        /// <summary>
        /// Rehash message
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Rehash()
        {
            return "REHASH";
        }

        /// <summary>
        /// Die message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Die()
        {
            return "DIE";
        }

        /// <summary>
        /// Restart message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Restart()
        {
            return "RESTART";
        }

        /// <summary>
        /// Summon message.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>System.String.</returns>
        public static string Summon(string user)
        {
            return $"SUMMON {user}";
        }

        /// <summary>
        /// Summon message.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Summon(string user, string target)
        {
            return $"SUMMON {user} {target}" + user + " " + target;
        }

        /// <summary>
        /// Summon message.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="target">The target.</param>
        /// <param name="channel">The channel.</param>
        /// <returns>System.String.</returns>
        public static string Summon(string user, string target, string channel)
        {
            return $"SUMMON {user} {target} {channel}";
        }

        /// <summary>
        /// Users message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Users()
        {
            return "USERS";
        }

        /// <summary>
        /// Users message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>System.String.</returns>
        public static string Users(string target)
        {
            return $"USERS {target}";
        }

        /// <summary>
        /// Wallops message.
        /// </summary>
        /// <param name="wallopstext">The wallopstext.</param>
        /// <returns>System.String.</returns>
        public static string Wallops(string wallopstext)
        {
            return $"WALLOPS :{wallopstext}";
        }

        /// <summary>
        /// Userhost message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns>System.String.</returns>
        public static string Userhost(string nickname)
        {
            return $"USERHOST {nickname}";
        }

        /// <summary>
        /// Userhost message.
        /// </summary>
        /// <param name="nicknames">The nicknames.</param>
        /// <returns>System.String.</returns>
        public static string Userhost(string[] nicknames)
        {
            return $"USERHOST {string.Join(" ", nicknames)}";
        }

        /// <summary>
        /// Ison message.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns>System.String.</returns>
        public static string Ison(string nickname)
        {
            return $"ISON {nickname}";
        }

        /// <summary>
        /// Ison message.
        /// </summary>
        /// <param name="nicknames">The nicknames.</param>
        /// <returns>System.String.</returns>
        public static string Ison(string[] nicknames)
        {
            return $"ISON {string.Join(" ", nicknames)}";
        }

        /// <summary>
        /// Quit message.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string Quit()
        {
            return "QUIT";
        }

        /// <summary>
        /// Quit message.
        /// </summary>
        /// <param name="quitmessage">The quitmessage.</param>
        /// <returns>System.String.</returns>
        public static string Quit(string quitmessage)
        {
            return $"QUIT :{quitmessage}";
        }

        /// <summary>
        /// Squit message.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>System.String.</returns>
        public static string Squit(string server, string comment)
        {
            return $"SQUIT {server} :{comment}";
        }
    }
}