namespace GlideRush.Leaderboard.Domain.Exceptions
{
    public class ForbiddenException: Exception
    {
        public List<AuthorizationError> Errors { get; }

        public ForbiddenException(List<AuthorizationError> errors)
        {
            Errors = errors;
        }
    }
}
