namespace GlideRush.Leaderboard.Service.Authorization
{
    public interface ICurrentUserService
    {
        string PlayfabId { get; }
        string UserName { get; }
    }
}
