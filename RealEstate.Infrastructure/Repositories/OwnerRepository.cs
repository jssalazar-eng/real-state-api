using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly MongoDbContext _context;

    public OwnerRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        return await _context.Owners.Find(_ => true).ToListAsync();
    }

    public async Task<Owner?> GetByIdAsync(string id)
    {
        return await _context.Owners.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Owner?> GetByIdOwnerAsync(string idOwner)
    {
        return await _context.Owners.Find(x => x.IdOwner == idOwner).FirstOrDefaultAsync();
    }

    public async Task<Owner> CreateAsync(Owner owner)
    {
        owner.Id = null; // Let MongoDB generate the ID
        owner.IdOwner = Guid.NewGuid().ToString();
        owner.CreatedAt = DateTime.UtcNow;
        owner.UpdatedAt = DateTime.UtcNow;

        await _context.Owners.InsertOneAsync(owner);
        return owner;
    }

    public async Task<Owner> UpdateAsync(Owner owner)
    {
        owner.UpdatedAt = DateTime.UtcNow;

        await _context.Owners.ReplaceOneAsync(x => x.Id == owner.Id, owner);
        return owner;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Owners.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Owners.Find(x => x.Id == id).AnyAsync();
    }
}
