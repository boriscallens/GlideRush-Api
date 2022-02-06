namespace GlideRush.Leaderboard.Service.Authorization
{
    public interface IAuthorizer<TRequest>
    {
        AuthorizationResult CheckAuthorization(TRequest request);
    }
}
