using RealEstate.Application.DTOs.Property;

namespace RealEstate.Application.Services;

public interface IPropertyService
{
    Task<IEnumerable<PropertyDto>> GetAllAsync();
    Task<PropertyDto?> GetByIdAsync(string id);
    Task<IEnumerable<PropertyDto>> GetByOwnerIdAsync(string ownerId);
    Task<PropertyDto> CreateAsync(CreatePropertyDto createPropertyDto);
    Task<PropertyDto> UpdateAsync(string id, UpdatePropertyDto updatePropertyDto);
    Task<bool> DeleteAsync(string id);
}
