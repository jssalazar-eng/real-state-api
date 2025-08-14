using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(string id);
    Task<Owner?> GetByIdOwnerAsync(string idOwner);
    Task<Owner> CreateAsync(Owner owner);
    Task<Owner> UpdateAsync(Owner owner);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
