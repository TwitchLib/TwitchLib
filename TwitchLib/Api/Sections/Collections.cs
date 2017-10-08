namespace TwitchLib
{
    using System.Collections.Generic;
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class Collections
    {
        public Collections(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetCollectionMetadata
            public async Task<Models.API.v5.Collections.CollectionMetadata> GetCollectionMetadataAsync(string collectionId)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Collections.CollectionMetadata>($"https://api.twitch.tv/kraken/collections/{collectionId}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetCollection
            public async Task<Models.API.v5.Collections.Collection> GetCollectionAsync(string collectionId, bool? includeAllItems = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (includeAllItems != null)
                    optionalQuery = $"?include_all_items={includeAllItems}";
                return await Api.GetGenericAsync<Models.API.v5.Collections.Collection>($"https://api.twitch.tv/kraken/collections/{collectionId}/items{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetCollectionsByChannel
            public async Task<Models.API.v5.Collections.CollectionsByChannel> GetCollectionsByChannelAsync(string channelId, long? limit = null, string cursor = null, string containingItem = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for catching a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    datas.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    datas.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (!string.IsNullOrEmpty(containingItem))
                    datas.Add(new KeyValuePair<string, string>("containing_item", (containingItem.StartsWith("video:") ? containingItem : $"video:{containingItem}")));

                string optionalQuery = string.Empty;
                if (datas.Count > 0)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{datas[i].Key}={datas[i].Value}"; }
                        else { optionalQuery += $"&{datas[i].Key}={datas[i].Value}"; }
                    }
                }
                return await Api.GetGenericAsync<Models.API.v5.Collections.CollectionsByChannel>($"https://api.twitch.tv/kraken/channels/{channelId}/collections{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CreateCollection
            public async Task<Models.API.v5.Collections.CollectionMetadata> CreateCollectionAsync(string channelId, string collectionTitle, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for a collection creation. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(collectionTitle)) { throw new Exceptions.API.BadParameterException("The collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.PostGenericAsync<Models.API.v5.Collections.CollectionMetadata>($"https://api.twitch.tv/kraken/channels/{channelId}/collections", "{\"title\": \"" + collectionTitle + "\"}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UpdateCollection
            public async Task UpdateCollectionAsync(string collectionId, string newCollectionTitle, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(newCollectionTitle)) { throw new Exceptions.API.BadParameterException("The new collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync($"https://api.twitch.tv/kraken/collections/{collectionId}", "{\"title\": \"" + newCollectionTitle + "\"}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CreateCollectionThumbnail
            public async Task CreateCollectionThumbnailAsync(string collectionId, string itemId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync($"https://api.twitch.tv/kraken/collections/{collectionId}/thumbnail", "{\"item_id\": \"" + itemId + "\"}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteCollection
            public async Task DeleteCollectionAsync(string collectionId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/collections/{collectionId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region AddItemToCollection
            public async Task<Models.API.v5.Collections.CollectionItem> AddItemToCollectionAsync(string collectionId, string itemId, string itemType, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (itemType != "video") { throw new Exceptions.API.BadParameterException($"The item_type {itemType} is not valid for a collection. Item type MUST be \"video\"."); }
                return await Api.PostGenericAsync<Models.API.v5.Collections.CollectionItem>($"https://api.twitch.tv/kraken/collections/{collectionId}/items", "{\"id\": \"" + itemId + "\", \"type\": \"" + itemType + "\"}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteItemFromCollection
            public async Task DeleteItemFromCollectionAsync(string collectionId, string itemId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/collections/{collectionId}/items/{itemId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region MoveItemWithinCollection
            public async Task MoveItemWithinCollectionAsync(string collectionId, string itemId, int position, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (position < 1) { throw new Exceptions.API.BadParameterException("The position is not valid for a collection. It is not allowed to be less than 1."); }
                await Api.PutAsync($"https://api.twitch.tv/kraken/collections/{collectionId}/items/{itemId}", "{\"position\": \"" + position + "\"}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
        }

    }
}
