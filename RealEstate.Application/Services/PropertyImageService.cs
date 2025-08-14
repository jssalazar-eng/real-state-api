using RealEstate.Application.DTOs.PropertyImage;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyImageService : IPropertyImageService
{
    private readonly IPropertyImageRepository _propertyImageRepository;
    private readonly IPropertyRepository _propertyRepository;

    public PropertyImageService(
        IPropertyImageRepository propertyImageRepository,
        IPropertyRepository propertyRepository
    )
    {
        _propertyImageRepository = propertyImageRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<PropertyImageDto>> GetAllAsync()
    {
        var propertyImages = await _propertyImageRepository.GetAllAsync();
        return propertyImages.Select(MapToDto);
    }

    public async Task<PropertyImageDto?> GetByIdAsync(string id)
    {
        var propertyImage = await _propertyImageRepository.GetByIdAsync(id);
        return propertyImage == null ? null : MapToDto(propertyImage);
    }

    public async Task<IEnumerable<PropertyImageDto>> GetByPropertyIdAsync(string propertyId)
    {
        var propertyImages = await _propertyImageRepository.GetByPropertyIdAsync(propertyId);
        return propertyImages.Select(MapToDto);
    }

    public async Task<PropertyImageDto> CreateAsync(CreatePropertyImageDto createPropertyImageDto)
    {
        // Validate property exists
        if (!await _propertyRepository.ExistsAsync(createPropertyImageDto.IdProperty))
            throw new ArgumentException(
                "Property not found",
                nameof(createPropertyImageDto.IdProperty)
            );

        var propertyImage = new PropertyImage
        {
            IdProperty = createPropertyImageDto.IdProperty,
            File = createPropertyImageDto.File,
            Enabled = createPropertyImageDto.Enabled,
        };

        var createdPropertyImage = await _propertyImageRepository.CreateAsync(propertyImage);
        return MapToDto(createdPropertyImage);
    }

    public async Task<PropertyImageDto> UpdateAsync(
        string id,
        UpdatePropertyImageDto updatePropertyImageDto
    )
    {
        var existingPropertyImage = await _propertyImageRepository.GetByIdAsync(id);
        if (existingPropertyImage == null)
            throw new ArgumentException("Property image not found", nameof(id));

        existingPropertyImage.File = updatePropertyImageDto.File;
        existingPropertyImage.Enabled = updatePropertyImageDto.Enabled;

        var updatedPropertyImage = await _propertyImageRepository.UpdateAsync(
            existingPropertyImage
        );
        return MapToDto(updatedPropertyImage);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _propertyImageRepository.DeleteAsync(id);
    }

    private static PropertyImageDto MapToDto(PropertyImage propertyImage)
    {
        return new PropertyImageDto
        {
            Id = propertyImage.Id,
            IdPropertyImage = propertyImage.IdPropertyImage,
            IdProperty = propertyImage.IdProperty,
            File = propertyImage.File,
            Enabled = propertyImage.Enabled,
            CreatedAt = propertyImage.CreatedAt,
            UpdatedAt = propertyImage.UpdatedAt,
        };
    }
}
