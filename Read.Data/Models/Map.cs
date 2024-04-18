using Read.Contracts.Entities;

namespace Read.Data.Models;

public static class Map
{
    public static IqBuildingNamesModel ToModel(IqName entity)
    {
        return new IqBuildingNamesModel { Name = entity.BuildingName };
    }
    public static IEnumerable<IqName> ToModel(IEnumerable<IqBuildingNamesModel> models)
    {
        return models.Select(c => new IqName { BuildingName = c.Name});
    }
    public static IEnumerable<BindIqRequest> ToModel(IEnumerable<BindRequestModel> models)
    {
        return models.Select(c=> new BindIqRequest(c.UserRequestingAccessId, c.BuildingName));
    }
    public static BindRequestModel ToModel(BindIqRequest entity)
    {
        return new BindRequestModel()
        {
            BuildingName = entity.IqBuildingName,
            UserRequestingAccessId = entity.AuthorId
        };
    }
}