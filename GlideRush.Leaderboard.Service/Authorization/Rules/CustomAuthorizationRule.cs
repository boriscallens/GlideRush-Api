using GlideRush.Leaderboard.Domain.Exceptions;

namespace GlideRush.Leaderboard.Service.Authorization.Rules
{
    // Unused but left in place fur future reference
    public class CustomAuthorizationRule<TRequest> : IAuthorizerRule<TRequest>
    {
        private readonly Func<TRequest, IList<AuthorizationError>> _func;

        public CustomAuthorizationRule(Func<TRequest, IList<AuthorizationError>> func)
        {
            _func = func;
        }

        public IList<AuthorizationError> Check(TRequest request)
        {
            return new List<AuthorizationError>(_func.Invoke(request));
        }
    }
}
