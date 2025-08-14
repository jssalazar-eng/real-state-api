using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Mappings;

public static class PropertyTraceMap
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<PropertyTrace>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(c => c.Id));
            map.GetMemberMap(c => c.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
            map.GetMemberMap(c => c.IdPropertyTrace).SetElementName("idPropertyTrace");
            map.GetMemberMap(c => c.DateSale).SetElementName("dateSale");
            map.GetMemberMap(c => c.Name).SetElementName("name");
            map.GetMemberMap(c => c.Value).SetElementName("value");
            map.GetMemberMap(c => c.Tax).SetElementName("tax");
            map.GetMemberMap(c => c.IdProperty).SetElementName("idProperty");
            map.GetMemberMap(c => c.CreatedAt).SetElementName("createdAt");
            map.GetMemberMap(c => c.UpdatedAt).SetElementName("updatedAt");
            map.GetMemberMap(c => c.Property).SetIgnoreIfNull(true);
        });
    }
}
