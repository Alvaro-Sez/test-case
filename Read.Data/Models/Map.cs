using Read.Contracts.Entities;

namespace Read.Data.Models;

public static class Map
{
    public static IqBuildingNamesModel ToModel(IqName entity)
    {
        return new IqBuildingNamesModel { Name = entity.BuildingName };
    }
}