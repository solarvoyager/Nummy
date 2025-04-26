using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class RequestLogMapper : Profile
{
    public RequestLogMapper()
    {
        CreateMap<RequestLogToAddDto, RequestLog>();
        CreateMap<RequestLog, RequestLogToListDto>();
    }
}