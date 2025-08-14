using RealEstate.Application.DTOs.PropertyImage;

namespace RealEstate.Application.Services;

public interface IPropertyImageService
{
    Task<IEnumerable<PropertyImageDto>> GetAllAsync();
    Task<PropertyImageDto?> GetByIdAsync(string id);
    Task<IEnumerable<PropertyImageDto>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyImageDto> CreateAsync(CreatePropertyImageDto createPropertyImageDto);
    Task<PropertyImageDto> UpdateAsync(string id, UpdatePropertyImageDto updatePropertyImageDto);
    Task<bool> DeleteAsync(string id);
}
