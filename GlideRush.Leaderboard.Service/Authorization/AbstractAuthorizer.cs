namespace GlideRush.Leaderboard.Service.Authorization
{
    public abstract class AbstractAuthorizer<TRequest> : IAuthorizer<TRequest>
    {
        private readonly IList<IAuthorizerRule<TRequest>> _rules;

        public AbstractAuthorizer()
        {
            _rules = new List<IAuthorizerRule<TRequest>>();
        }

        public AuthorizationResult CheckAuthorization(TRequest request)
        {
            var result = new AuthorizationResult();

            foreach (var rule in _rules)
            {
                result.Errors.AddRange(rule.Check(request));
            }

            return result;
        }

        protected void RuleFor(IAuthorizerRule<TRequest> rule)
        {
            _rules.Add(rule);
        }
    }
}
