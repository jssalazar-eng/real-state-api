using RealEstate.Application.DTOs.Owner;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<IEnumerable<OwnerDto>> GetAllAsync()
    {
        var owners = await _ownerRepository.GetAllAsync();
        return owners.Select(MapToDto);
    }

    public async Task<OwnerDto?> GetByIdAsync(string id)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);
        return owner == null ? null : MapToDto(owner);
    }

    public async Task<OwnerDto> CreateAsync(CreateOwnerDto createOwnerDto)
    {
        var owner = new Owner
        {
            Name = createOwnerDto.Name,
            Address = createOwnerDto.Address,
            Photo = createOwnerDto.Photo,
            Birthday = createOwnerDto.Birthday,
        };

        var createdOwner = await _ownerRepository.CreateAsync(owner);
        return MapToDto(createdOwner);
    }

    public async Task<OwnerDto> UpdateAsync(string id, UpdateOwnerDto updateOwnerDto)
    {
        var existingOwner = await _ownerRepository.GetByIdAsync(id);
        if (existingOwner == null)
            throw new ArgumentException("Owner not found", nameof(id));

        existingOwner.Name = updateOwnerDto.Name;
        existingOwner.Address = updateOwnerDto.Address;
        existingOwner.Photo = updateOwnerDto.Photo;
        existingOwner.Birthday = updateOwnerDto.Birthday;

        var updatedOwner = await _ownerRepository.UpdateAsync(existingOwner);
        return MapToDto(updatedOwner);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _ownerRepository.DeleteAsync(id);
    }

    private static OwnerDto MapToDto(Owner owner)
    {
        return new OwnerDto
        {
            Id = owner.Id,
            IdOwner = owner.IdOwner,
            Name = owner.Name,
            Address = owner.Address,
            Photo = owner.Photo,
            Birthday = owner.Birthday,
            CreatedAt = owner.CreatedAt,
            UpdatedAt = owner.UpdatedAt,
        };
    }
}
