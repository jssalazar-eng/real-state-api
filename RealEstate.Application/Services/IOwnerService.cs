using RealEstate.Application.DTOs.Owner;

namespace RealEstate.Application.Services;

public interface IOwnerService
{
    Task<IEnumerable<OwnerDto>> GetAllAsync();
    Task<OwnerDto?> GetByIdAsync(string id);
    Task<OwnerDto> CreateAsync(CreateOwnerDto createOwnerDto);
    Task<OwnerDto> UpdateAsync(string id, UpdateOwnerDto updateOwnerDto);
    Task<bool> DeleteAsync(string id);
}
