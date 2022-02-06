using GlideRush.Leaderboard.Domain.Exceptions;

namespace GlideRush.Leaderboard.Service.Authorization
{
    public class AuthorizationResult
    {
        public virtual bool IsValid => Errors.Count == 0;
        public List<AuthorizationError> Errors { get; }

        public AuthorizationResult() {
            Errors = new List<AuthorizationError>();
        }
    }
}
