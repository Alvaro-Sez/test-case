using System.Collections;
using Read.Contracts.Entities;
using Read.Contracts.Events;

namespace Read.Data.Models.Utils;

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
                    Id = c.Id,
                    Locks = c.Locks.Select(l=> new LockModel{ Id = l.Id }) .ToList()
                }).ToList(),
        };
    }
    public static UserAccess? ToDomain(UserAccessModel? model)
    {
        return model is not null
            ? new UserAccess
            {
                UserId = model.Id,
                Iqs = model.Iqs.Select(c => new Iq
                {
                    Id = c.Id,
                    Locks = c.Locks.Select(l => new Lock { Id = l.Id }).ToList()
                }).ToList()
            }
            : null;
    }
    public static EventModel ToModel(EventRecord entity)
    {
        return new EventModel
        {
            UserId = entity.UserId,
            Type = entity.Type,
            IssuedAt = entity.IssuedAt,
            LockId = entity.LockId
        };
    }
    public static IEnumerable<EventRecord> ToDomain(IEnumerable<EventModel?> models)
    {
        return models.Any()
            ? models.Select(c => new EventRecord
            {
                UserId = c.UserId,
                Type = c.Type,
                IssuedAt = c.IssuedAt,
                LockId = c.LockId
            })
            : Enumerable.Empty<EventRecord>();
    }
}