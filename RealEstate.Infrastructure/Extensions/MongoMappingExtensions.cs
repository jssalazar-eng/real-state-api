using RealEstate.Infrastructure.Mappings;

namespace RealEstate.Infrastructure.Extensions;

public static class MongoMappingExtensions
{
    public static void ConfigureMappings()
    {
        OwnerMap.Configure();
        PropertyMap.Configure();
        PropertyImageMap.Configure();
        PropertyTraceMap.Configure();
    }
}
