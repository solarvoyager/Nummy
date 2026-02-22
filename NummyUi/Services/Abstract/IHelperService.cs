using NummyShared.DTOs.Domain;

namespace NummyUi.Services.Abstract;

public interface IHelperService
{
    Task<ServiceUrlResponseDto> GetServiceUrl();
}
