using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyImageRepository
{
    Task<IEnumerable<PropertyImage>> GetAllAsync();
    Task<PropertyImage?> GetByIdAsync(string id);
    Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyImage> CreateAsync(PropertyImage propertyImage);
    Task<PropertyImage> UpdateAsync(PropertyImage propertyImage);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
