using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class ApplicationStackMapper : Profile
{
    public ApplicationStackMapper()
    {
        CreateMap<ApplicationStack, ApplicationStackToListDto>();
    }
}