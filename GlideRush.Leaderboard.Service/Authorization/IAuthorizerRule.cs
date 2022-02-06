using GlideRush.Leaderboard.Domain.Exceptions;

namespace GlideRush.Leaderboard.Service.Authorization
{
    public interface IAuthorizerRule<in TRequest>
    {
        IList<AuthorizationError> Check(TRequest request);
    }
}
