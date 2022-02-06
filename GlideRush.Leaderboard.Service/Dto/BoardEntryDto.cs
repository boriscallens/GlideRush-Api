namespace GlideRush.Leaderboard.Service.Dto;

public class BoardEntryDto
{
    public Guid Id { get; set; }
    public Guid PlayfabId { get; set; }
    public int Time { get; set; }
    public DateTime CreatedUtc { get; set; }
}