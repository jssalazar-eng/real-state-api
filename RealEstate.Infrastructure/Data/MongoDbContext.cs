using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("MongoDB") ?? "mongodb://localhost:27017";
        var databaseName = configuration["DatabaseSettings:DatabaseName"] ?? "RealEstateDB";

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");

    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("properties");

    public IMongoCollection<PropertyImage> PropertyImages =>
        _database.GetCollection<PropertyImage>("propertyImages");

    public IMongoCollection<PropertyTrace> PropertyTraces =>
        _database.GetCollection<PropertyTrace>("propertyTraces");
}
