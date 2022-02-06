using GlideRush.Leaderboard.Service.Authorization;

namespace GlideRush.Leaderboard.Service.ListLeaderboards
{
    public class Authorization : AbstractAuthorizer<ListLeaderboardQuery>
    {
        public Authorization(ICurrentUserService currentUser)
        {
        }
    }
}
