using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class TeamMapper : Profile
{
    public TeamMapper()
    {
        CreateMap<TeamToAddDto, Team>();
        CreateMap<Team, TeamToListDto>()
            .ConstructUsing((src, ctx) => new TeamToListDto(
                src.Id,
                src.Name,
                src.Description,
                src.CreatedAt,
                ctx.Mapper.Map<List<UserToListDto>>(src.TeamUsers.Select(tu => tu.User)),
                ctx.Mapper.Map<List<ApplicationToListDto>>(src.TeamApplications.Select(ta => ta.Application))
            ));

        // CreateMap<TeamApplication, ApplicationToListDto>()
        //     .ConstructUsing((src, ctx) => ctx.Mapper.Map<ApplicationToListDto>(src.Application));

        // CreateMap<TeamUser, UserToListDto>()
        //     .ConstructUsing((src, ctx) => ctx.Mapper.Map<UserToListDto>(src.User));
    }
}