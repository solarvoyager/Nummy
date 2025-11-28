using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class RequestLogMapper : Profile
{
    public RequestLogMapper()
    {
        CreateMap<RequestLogToAddDto, RequestLog>().ForMember(dest => dest.Headers, src => src.Ignore());
        CreateMap<RequestLog, RequestLogToListDto>();
    }
}