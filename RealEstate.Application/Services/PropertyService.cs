using RealEstate.Application.DTOs.Property;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IOwnerRepository _ownerRepository;

    public PropertyService(IPropertyRepository propertyRepository, IOwnerRepository ownerRepository)
    {
        _propertyRepository = propertyRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task<IEnumerable<PropertyDto>> GetAllAsync()
    {
        var properties = await _propertyRepository.GetAllAsync();
        return properties.Select(MapToDto);
    }

    public async Task<PropertyDto?> GetByIdAsync(string id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        return property == null ? null : MapToDto(property);
    }

    public async Task<IEnumerable<PropertyDto>> GetByOwnerIdAsync(string ownerId)
    {
        var properties = await _propertyRepository.GetByOwnerIdAsync(ownerId);
        return properties.Select(MapToDto);
    }

    public async Task<PropertyDto> CreateAsync(CreatePropertyDto createPropertyDto)
    {
        // Validate owner exists
        if (!await _ownerRepository.ExistsAsync(createPropertyDto.IdOwner))
            throw new ArgumentException("Owner not found", nameof(createPropertyDto.IdOwner));

        var property = new Property
        {
            Name = createPropertyDto.Name,
            Address = createPropertyDto.Address,
            Price = createPropertyDto.Price,
            CodeInternal = createPropertyDto.CodeInternal,
            Year = createPropertyDto.Year,
            IdOwner = createPropertyDto.IdOwner,
        };

        var createdProperty = await _propertyRepository.CreateAsync(property);
        return MapToDto(createdProperty);
    }

    public async Task<PropertyDto> UpdateAsync(string id, UpdatePropertyDto updatePropertyDto)
    {
        var existingProperty = await _propertyRepository.GetByIdAsync(id);
        if (existingProperty == null)
            throw new ArgumentException("Property not found", nameof(id));

        // Validate owner exists if changed
        if (
            existingProperty.IdOwner != updatePropertyDto.IdOwner
            && !await _ownerRepository.ExistsAsync(updatePropertyDto.IdOwner)
        )
            throw new ArgumentException("Owner not found", nameof(updatePropertyDto.IdOwner));

        existingProperty.Name = updatePropertyDto.Name;
        existingProperty.Address = updatePropertyDto.Address;
        existingProperty.Price = updatePropertyDto.Price;
        existingProperty.CodeInternal = updatePropertyDto.CodeInternal;
        existingProperty.Year = updatePropertyDto.Year;
        existingProperty.IdOwner = updatePropertyDto.IdOwner;

        var updatedProperty = await _propertyRepository.UpdateAsync(existingProperty);
        return MapToDto(updatedProperty);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _propertyRepository.DeleteAsync(id);
    }

    private static PropertyDto MapToDto(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            IdProperty = property.IdProperty,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            CodeInternal = property.CodeInternal,
            Year = property.Year,
            IdOwner = property.IdOwner,
            CreatedAt = property.CreatedAt,
            UpdatedAt = property.UpdatedAt,
        };
    }
}
