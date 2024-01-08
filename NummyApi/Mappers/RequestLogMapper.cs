using AutoMapper;
using NummyApi.Dtos;
using NummyApi.Entitites;

namespace NummyApi.Mappers;

public class RequestLogMapper : Profile
{
    public RequestLogMapper()
    {
        CreateMap<RequestLogToAddDto, RequestLog>();
        CreateMap<RequestLog, RequestLogToListDto>();
    }
}