using GlideRush.Leaderboard.Service.Authorization;
using GlideRush.Leaderboard.Service.Authorization.Rules;

namespace GlideRush.Leaderboard.Service.CreateLeaderboard
{
    public class Authorization : AbstractAuthorizer<CreateLeaderboardCommand>
    {
        public Authorization(ICurrentUserService currentUser)
        {
        }
    }
}
