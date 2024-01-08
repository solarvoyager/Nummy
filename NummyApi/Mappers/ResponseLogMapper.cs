using AutoMapper;
using NummyApi.Dtos;
using NummyApi.Entitites;

namespace NummyApi.Mappers;

public class ResponseLogMapper : Profile
{
    public ResponseLogMapper()
    {
        CreateMap<ResponseLogToAddDto, ResponseLog>();
        CreateMap<ResponseLog, ResponseLogToListDto>();
    }
}