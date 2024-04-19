using Read.Contracts.Entities;
using Read.Contracts.Events;

namespace Read.Data.Models.Utils;

public static class Map
{
    public static IqModel ToModel(Iq entity)
    {
        return new IqModel
        {
            BuildingName = entity.BuildingName,
            IqId = entity.Id,
            Locks = entity.Locks.Select(c=>new LockModel
            {
                Access = c.Access,
                Id = c.Id
            }).ToList()
        };
    }
    public static Iq? ToDomain(IqModel? iqModel)
    {
        return iqModel is not null
            ? new Iq
            {
                Id = iqModel.IqId,
                BuildingName = iqModel.BuildingName,
                Locks = iqModel.Locks.Select(c => new Lock
                {
                    Id = c.Id,
                    Access = c.Access
                }).ToList()
            }
            : null;
    }
    public static IEnumerable<Iq> ToDomain(IEnumerable<IqModel> models)
    {
        return models.Select(c=> new Iq
        {
            Id = c.IqId,
            Locks = c.Locks.Select(l=>new Lock
            {
                Id = l.Id,
                Access = l.Access
            }).ToList(),
            BuildingName = c.BuildingName
        });
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
            Iqs = entity.Iqs,
            Access = entity.Access
        };
    }
    public static UserAccess? ToDomain(UserAccessModel? model)
    {
        return model is not null
            ? new UserAccess
            {
                UserId = model.Id,
                Iqs = model.Iqs
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

    public static AccessRequestModel ToModel(AccessLevelRequest entity)
    {
        return new AccessRequestModel() { UserId = entity.UserId , IqId = entity.IqId};
    }
    
    public static IEnumerable<AccessLevelRequest> ToDomain(IEnumerable<AccessRequestModel> models)
    {
        return models.Select(c => new AccessLevelRequest() { UserId = c.UserId , IqId = c.IqId});
    }
}