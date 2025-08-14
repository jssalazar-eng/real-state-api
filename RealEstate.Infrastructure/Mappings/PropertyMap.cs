using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Mappings;

public static class PropertyMap
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<Property>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(c => c.Id));
            map.GetMemberMap(c => c.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
            map.GetMemberMap(c => c.IdProperty).SetElementName("idProperty");
            map.GetMemberMap(c => c.Name).SetElementName("name");
            map.GetMemberMap(c => c.Address).SetElementName("address");
            map.GetMemberMap(c => c.Price).SetElementName("price");
            map.GetMemberMap(c => c.CodeInternal).SetElementName("codeInternal");
            map.GetMemberMap(c => c.Year).SetElementName("year");
            map.GetMemberMap(c => c.IdOwner).SetElementName("idOwner");
            map.GetMemberMap(c => c.CreatedAt).SetElementName("createdAt");
            map.GetMemberMap(c => c.UpdatedAt).SetElementName("updatedAt");
            map.GetMemberMap(c => c.Owner).SetIgnoreIfNull(true);
            map.GetMemberMap(c => c.PropertyImages).SetIgnoreIfNull(true);
            map.GetMemberMap(c => c.PropertyTraces).SetIgnoreIfNull(true);
        });
    }
}
