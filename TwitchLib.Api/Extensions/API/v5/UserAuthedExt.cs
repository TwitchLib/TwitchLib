namespace TwitchLib.Api.Extensions.API.v5
{
    public static class UserAuthedExt
    {
        public static Models.v5.Users.User ToUser(this Models.v5.Users.UserAuthed authed)
        {
            return new Models.v5.Users.User
            {
                Id = authed.Id,
                Bio = authed.Bio,
                CreatedAt = authed.CreatedAt,
                DisplayName = authed.DisplayName,
                Logo = authed.Logo,
                Name = authed.Name,
                Type = authed.Type,
                UpdatedAt = authed.UpdatedAt
            };
        }
    }
}
