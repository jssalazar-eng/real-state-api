using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoDbContext _context;

    public PropertyRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _context.Properties.Find(_ => true).ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(string id)
    {
        return await _context.Properties.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Property?> GetByIdPropertyAsync(string idProperty)
    {
        return await _context
            .Properties.Find(x => x.IdProperty == idProperty)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Property>> GetByOwnerIdAsync(string ownerId)
    {
        return await _context.Properties.Find(x => x.IdOwner == ownerId).ToListAsync();
    }

    public async Task<Property> CreateAsync(Property property)
    {
        property.Id = null; // Let MongoDB generate the ID
        property.IdProperty = Guid.NewGuid().ToString();
        property.CreatedAt = DateTime.UtcNow;
        property.UpdatedAt = DateTime.UtcNow;

        await _context.Properties.InsertOneAsync(property);
        return property;
    }

    public async Task<Property> UpdateAsync(Property property)
    {
        property.UpdatedAt = DateTime.UtcNow;

        await _context.Properties.ReplaceOneAsync(x => x.Id == property.Id, property);
        return property;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Properties.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Properties.Find(x => x.Id == id).AnyAsync();
    }
}
