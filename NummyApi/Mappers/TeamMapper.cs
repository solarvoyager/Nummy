using AutoMapper;
using NummyApi.Entitites;
using NummyShared.Dtos;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class TeamMapper : Profile
{
    public TeamMapper()
    {
        CreateMap<TeamToAddDto, Team>();
        CreateMap<Team, TeamToListDto>();
    }
}