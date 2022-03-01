using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Collections;

namespace TwitchLib.Api.V5
{
    public class Collections : ApiBase
    {
        public Collections(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCollectionMetadata

        public Task<CollectionMetadata> GetCollectionMetadataAsync(string collectionId)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<CollectionMetadata>($"/collections/{collectionId}", ApiVersion.V5);
        }

        #endregion

        #region GetCollection

        public Task<Collection> GetCollectionAsync(string collectionId, bool? includeAllItems = null)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (includeAllItems.HasValue)
                getParams.Add(new KeyValuePair<string, string>("include_all_items", ((bool)includeAllItems).ToString()));

            return TwitchGetGenericAsync<Collection>($"/collections/{collectionId}/items", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetCollectionsByChannel

        public Task<CollectionsByChannel> GetCollectionsByChannelAsync(string channelId, long? limit = null, string cursor = null, string containingItem = null)
        {
            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid for catching a collection. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            if (!string.IsNullOrWhiteSpace(cursor))
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
            if (!string.IsNullOrWhiteSpace(containingItem))
                getParams.Add(new KeyValuePair<string, string>("containing_item", containingItem.StartsWith("video:") ? containingItem : $"video:{containingItem}"));

            return TwitchGetGenericAsync<CollectionsByChannel>($"/channels/{channelId}/collections", ApiVersion.V5, getParams);
        }

        #endregion

        #region CreateCollection

        public Task<CollectionMetadata> CreateCollectionAsync(string channelId, string collectionTitle, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid for a collection creation. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(collectionTitle))
                throw new BadParameterException("The collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"title\": \"" + collectionTitle + "\"}";
            return TwitchPostGenericAsync<CollectionMetadata>($"/channels/{channelId}/collections", ApiVersion.V5, payload, accessToken: authToken);
        }

        #endregion

        #region UpdateCollection

        public Task UpdateCollectionAsync(string collectionId, string newCollectionTitle, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(newCollectionTitle))
                throw new BadParameterException("The new collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"title\": \"" + newCollectionTitle + "\"}";
            return TwitchPutAsync($"/collections/{collectionId}", ApiVersion.V5, payload, accessToken: authToken);
        }

        #endregion

        #region CreateCollectionThumbnail

        public Task CreateCollectionThumbnailAsync(string collectionId, string itemId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(itemId))
                throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"item_id\": \"" + itemId + "\"}";
            return TwitchPutAsync($"/collections/{collectionId}/thumbnail", ApiVersion.V5, payload, accessToken: authToken);
        }

        #endregion

        #region DeleteCollection

        public Task DeleteCollectionAsync(string collectionId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchDeleteAsync($"/collections/{collectionId}", ApiVersion.V5, accessToken: authToken);
        }

        #endregion

        #region AddItemToCollection

        public Task<CollectionItem> AddItemToCollectionAsync(string collectionId, string itemId, string itemType, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(itemId))
                throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            if (itemType != "video")
                throw new BadParameterException($"The item_type {itemType} is not valid for a collection. Item type MUST be \"video\".");

            var payload = "{\"id\": \"" + itemId + "\", \"type\": \"" + itemType + "\"}";
            return TwitchPostGenericAsync<CollectionItem>($"/collections/{collectionId}/items", ApiVersion.V5, payload, accessToken: authToken);
        }

        #endregion

        #region DeleteItemFromCollection

        public Task DeleteItemFromCollectionAsync(string collectionId, string itemId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(itemId))
                throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchDeleteAsync($"/collections/{collectionId}/items/{itemId}", ApiVersion.V5, accessToken: authToken);
        }

        #endregion

        #region MoveItemWithinCollection

        public Task MoveItemWithinCollectionAsync(string collectionId, string itemId, int position, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(collectionId))
                throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(itemId))
                throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces.");

            if (position < 1)
                throw new BadParameterException("The position is not valid for a collection. It is not allowed to be less than 1.");

            var payload = "{\"position\": \"" + position + "\"}";
            return TwitchPutAsync($"/collections/{collectionId}/items/{itemId}", ApiVersion.V5, payload, accessToken: authToken);
        }

        #endregion
    }

}