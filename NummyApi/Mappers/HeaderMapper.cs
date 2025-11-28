using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class HeaderMapper : Profile
{
    public HeaderMapper()
    {
        CreateMap<HeaderToAddDto, Header>();
        CreateMap<Header, HeaderToListDto>();
    }
}