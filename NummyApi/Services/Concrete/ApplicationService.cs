using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Helpers;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
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

    public async Task<IEnumerable<ApplicationHealthCheckerUrlToListDto>> GetHealthCheckerUrlAsync()
    {
        var healthCheckerUrls = await dataContext.Applications
            .Where(a => a.HealthCheckerUrl != null)
            .Select(a => new ApplicationHealthCheckerUrlToListDto(a.Id, a.HealthCheckerUrl!))
            .ToListAsync();

        return healthCheckerUrls;
    }
    
    public async Task UpdateIsHealthyAsync(List<ApplicationIsHealthyToUpdateDto> applications)
    {
        var appsDictionary = applications.ToDictionary(a=> a.ApplicationId,  a => a.IsHealthy);
        
        var applicationEntities = await dataContext.Applications
            .Where(a => appsDictionary.Keys.Contains(a.Id))
            .ToListAsync();

        foreach (var application in applicationEntities)
        {
            application.IsHealthy = appsDictionary.GetValueOrDefault(application.Id);
        }

        await dataContext.SaveChangesAsync();
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
        application.HealthCheckerUrl = dto.HealthCheckerUrl;
        application.StackId = dto.StackId;

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