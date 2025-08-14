using RealEstate.Application.DTOs.PropertyTrace;

namespace RealEstate.Application.Services;

public interface IPropertyTraceService
{
    Task<IEnumerable<PropertyTraceDto>> GetAllAsync();
    Task<PropertyTraceDto?> GetByIdAsync(string id);
    Task<IEnumerable<PropertyTraceDto>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyTraceDto> CreateAsync(CreatePropertyTraceDto createPropertyTraceDto);
    Task<PropertyTraceDto> UpdateAsync(string id, UpdatePropertyTraceDto updatePropertyTraceDto);
    Task<bool> DeleteAsync(string id);
}
