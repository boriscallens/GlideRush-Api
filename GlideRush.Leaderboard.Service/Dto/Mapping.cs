using AutoMapper;

using GlideRush.Leaderboard.Domain;

namespace GlideRush.Leaderboard.Service.Dto;

public class Mapping: Profile
{
    public Mapping()
    {
        this.CreateMap<BoardEntry, BoardEntryDto>();
    }
}