using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Helpers;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Enums;

namespace NummyApi.Services.Concrete;

public class ApplicationService(NummyDataContext dataContext, IMapper mapper) : IApplicationService
{
    public async Task<ApplicationToListDto?> GetAsync(Guid id)
    {
        var application = await dataContext.Applications.FindAsync(id);
        if (application == null)
            return null;

        var mapped = mapper.Map<ApplicationToListDto>(application);

        return mapped;
    }

    public async Task<IEnumerable<ApplicationToListDto>> GetAsync()
    {
        var applications = await dataContext.Applications
            .OrderByDescending(t=> t.CreatedAt)
            .ToListAsync();

        var mappeds = mapper.Map<List<ApplicationToListDto>>(applications);

        return mappeds;
    }

    public async Task<IEnumerable<ApplicationStackToListDto>> GetStackTypeAsync()
    {
        var stackTypes = await dataContext.ApplicationStacks
            .OrderByDescending(t=> t.CreatedAt)
            .ToListAsync();

        var mappeds = mapper.Map<List<ApplicationStackToListDto>>(stackTypes);

        return mappeds;
    }

    public async Task<ApplicationToListDto> AddAsync(ApplicationToAddDto dto)
    {
        var application = mapper.Map<Application>(dto);

        await dataContext.Applications.AddAsync(application);
        await dataContext.SaveChangesAsync();

        var mapped = mapper.Map<ApplicationToListDto>(application);

        return mapped;
    }

    public async Task<ApplicationToListDto?> UpdateAsync(Guid id, ApplicationToUpdateDto dto)
    {
        var application = await dataContext.Applications.FindAsync(id);
        if (application == null)
            return null;

        application.Name = dto.Name;
        application.Description = dto.Description;

        await dataContext.SaveChangesAsync();

        var mapped = mapper.Map<ApplicationToListDto>(application);

        return mapped;
    }

    public async Task DeleteAsync(Guid id)
    {
        var application = await dataContext.Applications.FindAsync(id);
        if (application == null) return;

        dataContext.Applications.Remove(application);
        await dataContext.SaveChangesAsync();
    }
}