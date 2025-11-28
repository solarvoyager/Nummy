using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class ResponseLogMapper : Profile
{
    public ResponseLogMapper()
    {
        CreateMap<ResponseLogToAddDto, ResponseLog>().ForMember(dest => dest.Headers, src => src.Ignore());
        CreateMap<ResponseLog, ResponseLogToListDto>();
    }
}