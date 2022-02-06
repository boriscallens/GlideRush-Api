using GlideRush.Leaderboard.Domain.Exceptions;

namespace GlideRush.Leaderboard.Service.Authorization.Rules
{
    public class NoOpAuthorizationRule<TRequest> : IAuthorizerRule<TRequest>
    {
        public IList<AuthorizationError> Check(TRequest request)
        {
            return new List<AuthorizationError>();
        }
    }
}
