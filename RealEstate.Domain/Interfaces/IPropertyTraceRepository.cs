using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyTraceRepository
{
    Task<IEnumerable<PropertyTrace>> GetAllAsync();
    Task<PropertyTrace?> GetByIdAsync(string id);
    Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace);
    Task<PropertyTrace> UpdateAsync(PropertyTrace propertyTrace);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
