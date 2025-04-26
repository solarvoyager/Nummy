using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class ResponseLogMapper : Profile
{
    public ResponseLogMapper()
    {
        CreateMap<ResponseLogToAddDto, ResponseLog>();
        CreateMap<ResponseLog, ResponseLogToListDto>();
    }
}