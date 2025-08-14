using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Mappings;

public static class PropertyImageMap
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<PropertyImage>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(c => c.Id));
            map.GetMemberMap(c => c.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
            map.GetMemberMap(c => c.IdPropertyImage).SetElementName("idPropertyImage");
            map.GetMemberMap(c => c.IdProperty).SetElementName("idProperty");
            map.GetMemberMap(c => c.File).SetElementName("file");
            map.GetMemberMap(c => c.Enabled).SetElementName("enabled");
            map.GetMemberMap(c => c.CreatedAt).SetElementName("createdAt");
            map.GetMemberMap(c => c.UpdatedAt).SetElementName("updatedAt");
            map.GetMemberMap(c => c.Property).SetIgnoreIfNull(true);
        });
    }
}
