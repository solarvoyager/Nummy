using AutoMapper;
using NummyApi.Entitites;
using NummyShared.DTOs;

namespace NummyApi.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserToListDto>();
    }
}