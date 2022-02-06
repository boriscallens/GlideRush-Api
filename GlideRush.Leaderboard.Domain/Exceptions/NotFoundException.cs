namespace GlideRush.Leaderboard.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(Guid id, string entityName) : base($"Could not find {entityName} with id {id}.") { }
    }
}
