using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class CodeLogMapper : Profile
{
    public CodeLogMapper()
    {
        CreateMap<CodeLogToAddDto, CodeLog>();
        CreateMap<CodeLog, CodeLogToListDto>();
    }
}