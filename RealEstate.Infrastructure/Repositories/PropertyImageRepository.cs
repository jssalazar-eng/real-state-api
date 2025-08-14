using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly MongoDbContext _context;

    public PropertyImageRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PropertyImage>> GetAllAsync()
    {
        return await _context.PropertyImages.Find(_ => true).ToListAsync();
    }

    public async Task<PropertyImage?> GetByIdAsync(string id)
    {
        return await _context.PropertyImages.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string propertyId)
    {
        return await _context.PropertyImages.Find(x => x.IdProperty == propertyId).ToListAsync();
    }

    public async Task<PropertyImage> CreateAsync(PropertyImage propertyImage)
    {
        propertyImage.Id = null; // Let MongoDB generate the ID
        propertyImage.IdPropertyImage = Guid.NewGuid().ToString();
        propertyImage.CreatedAt = DateTime.UtcNow;
        propertyImage.UpdatedAt = DateTime.UtcNow;

        await _context.PropertyImages.InsertOneAsync(propertyImage);
        return propertyImage;
    }

    public async Task<PropertyImage> UpdateAsync(PropertyImage propertyImage)
    {
        propertyImage.UpdatedAt = DateTime.UtcNow;

        await _context.PropertyImages.ReplaceOneAsync(x => x.Id == propertyImage.Id, propertyImage);
        return propertyImage;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.PropertyImages.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.PropertyImages.Find(x => x.Id == id).AnyAsync();
    }
}
