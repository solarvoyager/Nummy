using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Services.Concrete;

public class ApplicationService(NummyDataContext dataContext, IMapper mapper) : IApplicationService
{
    public async Task<ApplicationToListDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var application = await dataContext.Applications.FindAsync([id], cancellationToken);
        if (application == null)
            return null;

        return mapper.Map<ApplicationToListDto>(application);
    }

    public async Task<IEnumerable<ApplicationToListDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var applications = await dataContext.Applications
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ApplicationToListDto>>(applications);
    }

    public async Task<IEnumerable<ApplicationStackToListDto>> GetStackTypeAsync(CancellationToken cancellationToken = default)
    {
        var stackTypes = await dataContext.ApplicationStacks
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ApplicationStackToListDto>>(stackTypes);
    }

    public async Task<IEnumerable<ApplicationHealthCheckerUrlToListDto>> GetHealthCheckerUrlAsync(CancellationToken cancellationToken = default)
    {
        return await dataContext.Applications
            .Where(a => a.HealthCheckerUrl != null)
            .Select(a => new ApplicationHealthCheckerUrlToListDto(a.Id, a.HealthCheckerUrl!))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateIsHealthyAsync(List<ApplicationIsHealthyToUpdateDto> applications, CancellationToken cancellationToken = default)
    {
        var appsDictionary = applications.ToDictionary(a => a.ApplicationId, a => a.IsHealthy);

        var applicationEntities = await dataContext.Applications
            .Where(a => appsDictionary.Keys.Contains(a.Id))
            .ToListAsync(cancellationToken);

        foreach (var application in applicationEntities)
            application.IsHealthy = appsDictionary.GetValueOrDefault(application.Id);

        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ApplicationToListDto> AddAsync(ApplicationToAddDto dto, CancellationToken cancellationToken = default)
    {
        var application = mapper.Map<Application>(dto);

        await dataContext.Applications.AddAsync(application, cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<ApplicationToListDto>(application);
    }

    public async Task<ApplicationToListDto?> UpdateAsync(Guid id, ApplicationToUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var application = await dataContext.Applications.FindAsync([id], cancellationToken);
        if (application == null)
            return null;

        application.Name = dto.Name;
        application.Description = dto.Description;
        application.HealthCheckerUrl = dto.HealthCheckerUrl;
        application.StackId = dto.StackId;

        await dataContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<ApplicationToListDto>(application);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var application = await dataContext.Applications.FindAsync([id], cancellationToken);
        if (application == null) return;

        dataContext.Applications.Remove(application);
        await dataContext.SaveChangesAsync(cancellationToken);
    }
}
