using RealEstate.Application.DTOs.PropertyTrace;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyTraceService : IPropertyTraceService
{
    private readonly IPropertyTraceRepository _propertyTraceRepository;
    private readonly IPropertyRepository _propertyRepository;

    public PropertyTraceService(
        IPropertyTraceRepository propertyTraceRepository,
        IPropertyRepository propertyRepository
    )
    {
        _propertyTraceRepository = propertyTraceRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<PropertyTraceDto>> GetAllAsync()
    {
        var propertyTraces = await _propertyTraceRepository.GetAllAsync();
        return propertyTraces.Select(MapToDto);
    }

    public async Task<PropertyTraceDto?> GetByIdAsync(string id)
    {
        var propertyTrace = await _propertyTraceRepository.GetByIdAsync(id);
        return propertyTrace == null ? null : MapToDto(propertyTrace);
    }

    public async Task<IEnumerable<PropertyTraceDto>> GetByPropertyIdAsync(string propertyId)
    {
        var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(propertyId);
        return propertyTraces.Select(MapToDto);
    }

    public async Task<PropertyTraceDto> CreateAsync(CreatePropertyTraceDto createPropertyTraceDto)
    {
        // Validate property exists
        if (!await _propertyRepository.ExistsAsync(createPropertyTraceDto.IdProperty))
            throw new ArgumentException(
                "Property not found",
                nameof(createPropertyTraceDto.IdProperty)
            );

        var propertyTrace = new PropertyTrace
        {
            DateSale = createPropertyTraceDto.DateSale,
            Name = createPropertyTraceDto.Name,
            Value = createPropertyTraceDto.Value,
            Tax = createPropertyTraceDto.Tax,
            IdProperty = createPropertyTraceDto.IdProperty,
        };

        var createdPropertyTrace = await _propertyTraceRepository.CreateAsync(propertyTrace);
        return MapToDto(createdPropertyTrace);
    }

    public async Task<PropertyTraceDto> UpdateAsync(
        string id,
        UpdatePropertyTraceDto updatePropertyTraceDto
    )
    {
        var existingPropertyTrace = await _propertyTraceRepository.GetByIdAsync(id);
        if (existingPropertyTrace == null)
            throw new ArgumentException("Property trace not found", nameof(id));

        // Validate property exists if changed
        if (
            existingPropertyTrace.IdProperty != updatePropertyTraceDto.IdProperty
            && !await _propertyRepository.ExistsAsync(updatePropertyTraceDto.IdProperty)
        )
            throw new ArgumentException(
                "Property not found",
                nameof(updatePropertyTraceDto.IdProperty)
            );

        existingPropertyTrace.DateSale = updatePropertyTraceDto.DateSale;
        existingPropertyTrace.Name = updatePropertyTraceDto.Name;
        existingPropertyTrace.Value = updatePropertyTraceDto.Value;
        existingPropertyTrace.Tax = updatePropertyTraceDto.Tax;
        existingPropertyTrace.IdProperty = updatePropertyTraceDto.IdProperty;

        var updatedPropertyTrace = await _propertyTraceRepository.UpdateAsync(
            existingPropertyTrace
        );
        return MapToDto(updatedPropertyTrace);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _propertyTraceRepository.DeleteAsync(id);
    }

    private static PropertyTraceDto MapToDto(PropertyTrace propertyTrace)
    {
        return new PropertyTraceDto
        {
            Id = propertyTrace.Id,
            IdPropertyTrace = propertyTrace.IdPropertyTrace,
            DateSale = propertyTrace.DateSale,
            Name = propertyTrace.Name,
            Value = propertyTrace.Value,
            Tax = propertyTrace.Tax,
            IdProperty = propertyTrace.IdProperty,
            CreatedAt = propertyTrace.CreatedAt,
            UpdatedAt = propertyTrace.UpdatedAt,
        };
    }
}
