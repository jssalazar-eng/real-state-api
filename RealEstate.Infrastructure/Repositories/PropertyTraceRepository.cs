using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyTraceRepository : IPropertyTraceRepository
{
    private readonly MongoDbContext _context;

    public PropertyTraceRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PropertyTrace>> GetAllAsync()
    {
        return await _context.PropertyTraces.Find(_ => true).ToListAsync();
    }

    public async Task<PropertyTrace?> GetByIdAsync(string id)
    {
        return await _context.PropertyTraces.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string propertyId)
    {
        return await _context.PropertyTraces.Find(x => x.IdProperty == propertyId).ToListAsync();
    }

    public async Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace)
    {
        propertyTrace.Id = null; // Let MongoDB generate the ID
        propertyTrace.IdPropertyTrace = Guid.NewGuid().ToString();
        propertyTrace.CreatedAt = DateTime.UtcNow;
        propertyTrace.UpdatedAt = DateTime.UtcNow;

        await _context.PropertyTraces.InsertOneAsync(propertyTrace);
        return propertyTrace;
    }

    public async Task<PropertyTrace> UpdateAsync(PropertyTrace propertyTrace)
    {
        propertyTrace.UpdatedAt = DateTime.UtcNow;

        await _context.PropertyTraces.ReplaceOneAsync(x => x.Id == propertyTrace.Id, propertyTrace);
        return propertyTrace;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.PropertyTraces.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.PropertyTraces.Find(x => x.Id == id).AnyAsync();
    }
}
