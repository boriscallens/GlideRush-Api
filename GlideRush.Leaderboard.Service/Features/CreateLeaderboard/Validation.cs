using FluentValidation;

namespace GlideRush.Leaderboard.Service.CreateLeaderboard;

public class Validation: AbstractValidator<CreateLeaderboardCommand>
{
    public Validation()
    {
    }
}