﻿using AutoMapper;
using NummyApi.Entitites;
using NummyShared.Dtos;

namespace NummyApi.Mappers;

public class ResponseLogMapper : Profile
{
    public ResponseLogMapper()
    {
        CreateMap<ResponseLogToAddDto, ResponseLog>();
        CreateMap<ResponseLog, ResponseLogToListDto>();
    }
}