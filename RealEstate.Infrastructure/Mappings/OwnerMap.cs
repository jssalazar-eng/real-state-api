using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Mappings;

public static class OwnerMap
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<Owner>(map =>
        {
            map.AutoMap();
            map.SetIdMember(map.GetMemberMap(c => c.Id));
            map.GetMemberMap(c => c.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
            map.GetMemberMap(c => c.IdOwner).SetElementName("idOwner");
            map.GetMemberMap(c => c.Name).SetElementName("name");
            map.GetMemberMap(c => c.Address).SetElementName("address");
            map.GetMemberMap(c => c.Photo).SetElementName("photo");
            map.GetMemberMap(c => c.Birthday).SetElementName("birthday");
            map.GetMemberMap(c => c.CreatedAt).SetElementName("createdAt");
            map.GetMemberMap(c => c.UpdatedAt).SetElementName("updatedAt");
            map.GetMemberMap(c => c.Properties).SetIgnoreIfNull(true);
        });
    }
}
