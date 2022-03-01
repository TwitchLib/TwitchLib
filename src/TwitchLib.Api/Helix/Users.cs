using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Helix.Models.Users.GetUserActiveExtensions;
using TwitchLib.Api.Helix.Models.Users.GetUserBlockList;
using TwitchLib.Api.Helix.Models.Users.GetUserExtensions;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using TwitchLib.Api.Helix.Models.Users.Internal;
using TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions;

namespace TwitchLib.Api.Helix
{
    public class Users : ApiBase
    {
        public Users(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetUserBlockListResponse> GetUserBlockListAsync(string broadcasterId, int first = 20, string after = null, string accessToken = null)
        {
            if (first > 100)
                throw new BadParameterException($"Maximum allowed objects is 100 (you passed {first})");

            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetUserBlockListResponse>("/users/blocks", ApiVersion.Helix, getParams, accessToken);
        }

        public Task BlockUserAsync(string targetUserId, BlockUserSourceContextEnum? sourceContext = null, BlockUserReasonEnum? reason = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("target_user_id", targetUserId));
            if (sourceContext != null)
                getParams.Add(new KeyValuePair<string, string>("source_context", sourceContext.Value.ToString().ToLower()));
            if (reason != null)
                getParams.Add(new KeyValuePair<string, string>("reason", reason.Value.ToString().ToLower()));

            return TwitchPutAsync("/users/blocks", ApiVersion.Helix, null, getParams, accessToken);
        }

        public Task UnblockUserAsync(string targetUserId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("target_user_id", targetUserId));

            return TwitchDeleteAsync("/user/blocks", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (ids != null && ids.Count > 0)
            {
                foreach (var id in ids)
                    getParams.Add(new KeyValuePair<string, string>("id", id));
            }

            if (logins != null && logins.Count > 0)
            {
                foreach (var login in logins)
                    getParams.Add(new KeyValuePair<string, string>("login", login));
            }

            return TwitchGetGenericAsync<GetUsersResponse>("/users", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString())
                };
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));
            if (before != null)
                getParams.Add(new KeyValuePair<string, string>("before", before));
            if (fromId != null)
                getParams.Add(new KeyValuePair<string, string>("from_id", fromId));
            if (toId != null)
                getParams.Add(new KeyValuePair<string, string>("to_id", toId));

            return TwitchGetGenericAsync<GetUsersFollowsResponse>("/users/follows", ApiVersion.Helix, getParams);
        }

        public Task PutUsersAsync(string description, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("description", description)
                };

            return TwitchPutAsync("/users", ApiVersion.Helix, null, getParams, accessToken);
        }

        public Task<GetUserExtensionsResponse> GetUserExtensionsAsync(string authToken = null)
        {
            return TwitchGetGenericAsync<GetUserExtensionsResponse>("/users/extensions/list", ApiVersion.Helix, accessToken: authToken);
        }

        public Task<GetUserActiveExtensionsResponse> GetUserActiveExtensionsAsync(string userid = null, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (userid != null)
                getParams.Add(new KeyValuePair<string, string>("user_id", userid));

            return TwitchGetGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, getParams, accessToken: authToken);
        }

        public Task<GetUserActiveExtensionsResponse> UpdateUserExtensionsAsync(IEnumerable<ExtensionSlot> userExtensionStates, string authToken = null)
        {
            var panels = new Dictionary<string, UserExtensionState>();
            var overlays = new Dictionary<string, UserExtensionState>();
            var components = new Dictionary<string, UserExtensionState>();

            foreach (var extension in userExtensionStates)
                switch (extension.Type)
                {
                    case ExtensionType.Component:
                        components.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    case ExtensionType.Overlay:
                        overlays.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    case ExtensionType.Panel:
                        panels.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(ExtensionType));
                }

            var p = new UpdateUserExtensionsRequest
            {
                Data = new UpdateUserExtensionsRequestData
                {
                    Component = components.Count > 0 ? components : new Dictionary<string, UserExtensionState>(),
                    Overlay = overlays.Count > 0 ? overlays : new Dictionary<string, UserExtensionState>(),
                    Panel = panels.Count > 0 ? panels : new Dictionary<string, UserExtensionState>()
                }
            };

            return TwitchPutGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, JsonSerializer.Serialize(p), accessToken: authToken);
        }

        public Task CreateUserFollows(string from_id, string to_id, bool? allow_notifications = null, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(from_id))
            {
                throw new BadParameterException("from_id must be set");
            }

            if (string.IsNullOrWhiteSpace(to_id))
            {
                throw new BadParameterException("to_id must be set");
            }

            var payload = new Dictionary<string, object>();
            payload.Add("from_id", from_id);
            payload.Add("to_id", to_id);

            if (allow_notifications.HasValue)
            {
                payload.Add("allow_notifications", allow_notifications.Value);
            }
         
            return TwitchPostAsync("/users/follows", ApiVersion.Helix, JsonSerializer.Serialize(payload), accessToken: authToken);
        }

        public Task DeleteUserFollows(string from_id, string to_id, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(from_id))
            {
                throw new BadParameterException("from_id must be set");
            }

            if (string.IsNullOrWhiteSpace(to_id))
            {
                throw new BadParameterException("to_id must be set");
            }

            var getParams = new List<KeyValuePair<string, string>>();

            getParams.Add(new KeyValuePair<string, string>("from_id", from_id));
            getParams.Add(new KeyValuePair<string, string>("to_id", to_id));

            return TwitchDeleteAsync("/users/follows", ApiVersion.Helix, getParams, accessToken: authToken);
        }
    }
}
