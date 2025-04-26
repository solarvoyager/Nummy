using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        CreateMap<ApplicationToAddDto, Application>();
        CreateMap<Application, ApplicationToListDto>();
    }
}