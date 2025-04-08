using AutoMapper;
using NummyApi.Entitites;
using NummyShared.Dtos;

namespace NummyApi.Mappers;

public class CodeLogMapper : Profile
{
    public CodeLogMapper()
    {
        CreateMap<CodeLogToAddDto, CodeLog>();
        CreateMap<CodeLog, CodeLogToListDto>();
    }
}