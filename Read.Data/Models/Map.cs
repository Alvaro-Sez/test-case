using Read.Contracts.Entities;

namespace Read.Data.Models;

public static class Map
{
    public static IqBuildingNamesModel ToModel(IqName entity)
    {
        return new IqBuildingNamesModel { Name = entity.BuildingName };
    }
    public static IEnumerable<IqName> ToDomain(IEnumerable<IqBuildingNamesModel> models)
    {
        return models.Select(c => new IqName { BuildingName = c.Name});
    }
    public static IEnumerable<BindIqRequest> ToDomain(IEnumerable<BindRequestModel> models)
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
    
    public static UserAccessModel ToModel(UserAccess entity)
    {
        return new UserAccessModel
        {
            Id = entity.UserId,
            Iqs = entity.Iqs .Select(c=> 
                    new IqModel 
                    {
                        BuildingName = c.BuildingName,
                        Id = c.Id,
                        Locks = c.Locks.Select(l=> new LockModel{ Id = l.Id }) .ToList()
                    }).ToList(),
        };
    }
    public static UserAccess ToDomain(UserAccessModel model)
    {
        return new UserAccess
        {
            UserId = model.Id,
            Iqs = model.Iqs.Select(c=> new Iq
            {
                BuildingName = c.BuildingName,
                Id = c.Id,
                Locks = c.Locks.Select(l=> new Lock { Id = l.Id }).ToList()
            }).ToList()
        };
    }
}