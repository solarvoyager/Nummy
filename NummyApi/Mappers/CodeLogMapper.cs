using AutoMapper;
using NummyApi.Dtos;
using NummyApi.Entitites;

namespace NummyApi.Mappers;

public class CodeLogMapper : Profile
{
    public CodeLogMapper()
    {
        CreateMap<CodeLogToAddDto, CodeLog>();
    }
}