using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(string id);
    Task<Property?> GetByIdPropertyAsync(string idProperty);
    Task<IEnumerable<Property>> GetByOwnerIdAsync(string ownerId);
    Task<Property> CreateAsync(Property property);
    Task<Property> UpdateAsync(Property property);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
