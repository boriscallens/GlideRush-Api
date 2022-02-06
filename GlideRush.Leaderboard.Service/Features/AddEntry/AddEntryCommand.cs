using MediatR;

namespace GlideRush.Leaderboard.Service.AddEntry;

public class AddEntryCommand: IRequest<AddHandlerResult>
{
    public Guid BoardId { get; set; }
    public Guid PlayfabId { get; set; }
    public uint Time { get; set; }
}